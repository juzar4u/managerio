using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.FetchApiModels;
using ManagerIO.DataModels.Models.CustomEntities.SyncApiModels;
using ManagerIO.DataModels.Models.Enumerations;
using ManagerIO.DataServices.Services;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ManagerIO.DataServices.Helper
{

    public static class SyncApiHelper
    {
        public static void StartBusinessSyncFromApi()
        {
            ApiAuthModel apiAuth = new ApiAuthModel();

            string url = string.Format("{0}{1}", Constants.Api_Url, "/index.json");
            apiAuth.Username = Constants.Api_Username;
            apiAuth.Password = Constants.Api_Password;
            FetchApiService.Instance.DeactivateExistingBusiness(Constants.Base_Url);
            List<BusinessApiModel> businessApiModel = FetchApiHelper.GetBusinessDataFromApi(url, apiAuth.Username, apiAuth.Password);
            if (businessApiModel != null)
            {
                List<Business> Business = SyncApiService.Instance.InsertBusiness(businessApiModel, Constants.Base_Url, true);
                List<ObjectModel> objectModel = FetchApiService.Instance.getObjectModel();
                InnerObjectSyncFromApi(objectModel, Business, apiAuth);
            }
        }

        public static void InnerObjectSyncFromApi(List<ObjectModel> objectModel, List<Business> businessApiModel, ApiAuthModel apiAuth)
        {
            foreach (var item in businessApiModel)
            {
                List<ObjectAPIModel> innerObjApiModel = new List<ObjectAPIModel>();
                foreach (var obj in objectModel)
                {
                    string url = string.Format("{0}/{1}/{2}/{3}", Constants.Api_Url, item.BusinessKey, obj.ObjectKey, "index.json");
                    ObjectAPIModel objmodel = new ObjectAPIModel();
                    objmodel.ObjectID = obj.ObjectID;
                    objmodel.ObjectKey = obj.ObjectKey;
                    objmodel.ObjectName = obj.ObjectName;
                    objmodel.innerObjApiUrl = url;
                    innerObjApiModel.Add(objmodel);
                }
                foreach (var api in innerObjApiModel)
                {
                    List<string> InnerObjectKeys = FetchApiHelper.GetInnerObjectsKeysFromApi(api.innerObjApiUrl, apiAuth.Username, apiAuth.Password);
                    if (InnerObjectKeys.Count > 0)
                    {
                        SyncApiService.Instance.InsertInnerObjects(InnerObjectKeys, api.ObjectName, api.ObjectID, api.ObjectKey, item.BusinessID, item.BusinessKey);
                        
                        if (api.ObjectID == (int)ObjectEnum.BusinessLogo)
                        {
                            List<BusinessLogoApiModel> businessLogoApiList = new List<BusinessLogoApiModel>();
                            BusinessLogoApiModel businessLogo = new BusinessLogoApiModel();
                            foreach (var logo in InnerObjectKeys)
                            {
                                string apiurl = string.Format("{0}/{1}/{2}{3}", Constants.Api_Url, item.BusinessKey, logo, ".json");
                                businessLogo = FetchApiHelper.GetBusinessLogoFromApi(apiurl, apiAuth.Username, apiAuth.Password);
                                if (businessLogo != null)
                                {
                                    businessLogo.ApiKey = logo;
                                    businessLogo.BusinessID = item.BusinessID;
                                    businessLogo.BusinessKey = item.BusinessKey;
                                    businessLogo.IsActive = true;
                                    businessLogoApiList.Add(businessLogo);
                                }
                            }
                            if(businessLogoApiList.Count > 0)
                                SyncApiService.Instance.InsertBusinessLogo(businessLogoApiList);
                        }
                        else if (api.ObjectID == (int)ObjectEnum.BusinessDetails)
                        {
                            List<BusinessDetailApiModel> businessDetailApiList = new List<BusinessDetailApiModel>();
                            BusinessDetailApiModel businessDetail = new BusinessDetailApiModel();
                            foreach (var detail in InnerObjectKeys)
                            {
                                string apiurl = string.Format("{0}/{1}/{2}{3}", Constants.Api_Url, item.BusinessKey, detail, ".json");
                                businessDetail = FetchApiHelper.GetBusinessDetailFromApi(apiurl, apiAuth.Username, apiAuth.Password);
                                if (businessDetail != null)
                                {
                                    businessDetail.ApiKey = detail;
                                    businessDetail.BusinessID = item.BusinessID;
                                    businessDetail.BusinessKey = item.BusinessKey;
                                    businessDetail.IsActive = true;
                                    businessDetailApiList.Add(businessDetail);
                                }
                            }
                            if (businessDetailApiList.Count > 0)
                                SyncApiService.Instance.InsertBusinessDetail(businessDetailApiList);
                        }
                        else if (api.ObjectID == (int)ObjectEnum.BankAccount || api.ObjectID == (int)ObjectEnum.CashAccount)
                        {
                            List<AccountApiModel> accountApiList = new List<AccountApiModel>();
                            AccountApiModel account = new AccountApiModel();
                            foreach (var acc in InnerObjectKeys)
                            {
                                string apiurl = string.Format("{0}/{1}/{2}{3}", Constants.Api_Url, item.BusinessKey, acc, ".json");
                                account = FetchApiHelper.GetAccountFromApi(apiurl, apiAuth.Username, apiAuth.Password);
                                if (account != null)
                                {
                                    account.ApiKey = acc;
                                    account.AccountType = api.ObjectID == (int)ObjectEnum.BankAccount ? (int)ObjectEnum.BankAccount : (int)ObjectEnum.CashAccount;
                                    account.BusinessID = item.BusinessID;
                                    account.BusinessKey = item.BusinessKey;
                                    account.IsActive = true;
                                    accountApiList.Add(account);
                                }
                            }
                            if (accountApiList.Count > 0)
                                SyncApiService.Instance.InsertAccount(accountApiList);
                        }
                        else if (api.ObjectID == (int)ObjectEnum.Customer || api.ObjectID == (int)ObjectEnum.Supplier)
                        {
                            List<PartyApiModel> partyApiList = new List<PartyApiModel>();
                            PartyApiModel partyModel = new PartyApiModel();
                            foreach (var party in InnerObjectKeys)
                            {
                                string apiurl = string.Format("{0}/{1}/{2}{3}", Constants.Api_Url, item.BusinessKey, party, ".json");
                                partyModel = FetchApiHelper.GetPartyFromApi(apiurl, apiAuth.Username, apiAuth.Password);
                                if (partyModel != null)
                                {
                                    partyModel.ApiKey = party;
                                    partyModel.BusinessID = item.BusinessID;
                                    partyModel.BusinessKey = item.BusinessKey;
                                    partyModel.IsActive = true;
                                    partyModel.PartyTypeID = api.ObjectID == (int)ObjectEnum.Customer ? (int)PartyEnum.Customer : (int)PartyEnum.Supplier;
                                    partyApiList.Add(partyModel);
                                }
                            }
                            if (partyApiList.Count > 0)
                                SyncApiService.Instance.InsertParty(partyApiList , api.ObjectID);
                        }
                        else if (api.ObjectID == (int)ObjectEnum.Folder)
                        {
                            List<FolderApiModel> folderApiList = new List<FolderApiModel>();
                            FolderApiModel folderModel = new FolderApiModel();
                            foreach (var folder in InnerObjectKeys)
                            {
                                string apiurl = string.Format("{0}/{1}/{2}{3}", Constants.Api_Url, item.BusinessKey, folder, ".json");
                                folderModel = FetchApiHelper.GetFolderFromApi(apiurl, apiAuth.Username, apiAuth.Password);
                                if (folderModel != null)
                                {
                                    folderModel.ApiKey = folder;
                                    folderModel.BusinessID = item.BusinessID;
                                    folderModel.BusinessKey = item.BusinessKey;
                                    folderModel.IsActive = true;
                                    folderApiList.Add(folderModel);
                                }
                            }
                            if (folderApiList.Count > 0)
                                SyncApiService.Instance.InsertFolder(folderApiList);
                        }
                        else if (api.ObjectID == (int)ObjectEnum.InventoryItem)
                        {
                            List<InventoryApiModel> inventoryApiList = new List<InventoryApiModel>();
                            InventoryApiModel inventory = new InventoryApiModel();
                            foreach (var inv in InnerObjectKeys)
                            {
                                string apiurl = string.Format("{0}/{1}/{2}{3}", Constants.Api_Url, item.BusinessKey, inv, ".json");
                                inventory = FetchApiHelper.GetInventoryFromApi(apiurl, apiAuth.Username, apiAuth.Password);
                                if (inventory != null)
                                {
                                    inventory.ApiKey = inv;
                                    inventory.BusinessID = item.BusinessID;
                                    inventory.BusinessKey = item.BusinessKey;
                                    inventory.IsActive = true;
                                    inventoryApiList.Add(inventory);
                                }
                            }
                            if (inventoryApiList.Count > 0)
                                SyncApiService.Instance.InsertInventory(inventoryApiList);
                        }
                        else if (api.ObjectID == (int)ObjectEnum.ChartofAccountGroup || api.ObjectID == (int)ObjectEnum.ChartofAccount)
                        {
                            List<ChartofAccount> parentAccount = new List<ChartofAccount>();
                            List<ChartofAccApiModel> chartApiList = new List<ChartofAccApiModel>();
                            ChartofAccApiModel chart = new ChartofAccApiModel();
                            if(api.ObjectID == (int)ObjectEnum.ChartofAccount)
                                   parentAccount = FetchApiService.Instance.getChartofAccountGroupModel(item.BusinessID, item.BusinessKey);
                            foreach (var acc in InnerObjectKeys)
                            {
                                string apiurl = string.Format("{0}/{1}/{2}{3}", Constants.Api_Url, item.BusinessKey, acc, ".json");
                                chart = FetchApiHelper.GetChartAccountFromApi(apiurl, apiAuth.Username, apiAuth.Password);
                                if (chart != null)
                                {
                                    if(api.ObjectID == (int)ObjectEnum.ChartofAccount)
                                    {
                                        chart.ParentAccountID = parentAccount.Where(x => x.AccountKey == chart.Group).FirstOrDefault().AccountID;
                                    }
                                    chart.ApiKey = acc;
                                    chart.BusinessID = item.BusinessID;
                                    chart.BusinessKey = item.BusinessKey;
                                    chart.IsActive = true;
                                    chart.IsAccountGroup = !string.IsNullOrEmpty(chart.Group) ? true : false;
                                    chartApiList.Add(chart);
                                }
                            }
                            if (chartApiList.Count > 0)
                                SyncApiService.Instance.InsertChartOfAccounts(chartApiList);
                        }
                        else if (api.ObjectID == (int)ObjectEnum.TaxCode)
                        {
                            List<TaxCodeApiModel> taxcodeList = new List<TaxCodeApiModel>();
                            TaxCodeApiModel taxcode = new TaxCodeApiModel();
                            foreach (var tax in InnerObjectKeys)
                            {
                                TaxCode taxModel = new TaxCode();
                                int taxcodeId = 0;
                                string apiurl = string.Format("{0}/{1}/{2}{3}", Constants.Api_Url, item.BusinessKey, tax, ".json");
                                taxcode = FetchApiHelper.GetTaxcodeFromApi(apiurl, apiAuth.Username, apiAuth.Password);
                                if (taxcode != null)
                                {
                                    taxModel.ComponentKey = tax;
                                    taxModel.TaxCodeName = taxcode.Name;
                                    taxModel.TaxRate = taxcode.TaxRate;
                                    taxModel.TaxRateType = taxcode.TaxRateType;
                                    taxModel.Account = taxcode.Account;
                                    taxModel.SalesInvoiceTitle = taxcode.SalesInvoiceTitle;
                                    taxModel.CustomSalesInvoiceTitle = taxcode.CustomSalesInvoiceTitle;
                                    taxModel.BusinessID = item.BusinessID;
                                    taxModel.BusinessKey = item.BusinessKey;
                                    taxModel.IsActive = true;
                                    taxcodeId = SyncApiService.Instance.insertTaxCodeKey(taxModel);
                                    
                                    foreach (var comp in taxcode.Components)
                                    {
                                        comp.TaxCodeID = taxcodeId;
                                        comp.BusinessID = item.BusinessID;
                                        comp.BusinessKey = item.BusinessKey;
                                        comp.IsActive = true;
                                    }
                                    if (taxcode.Components.Count > 0)
                                        SyncApiService.Instance.InsertComponents(taxcode.Components);
                                }
                            }
                               
                        }
                        else if (api.ObjectID == (int)ObjectEnum.PurchaseInvoice || api.ObjectID == (int)ObjectEnum.SalesInvoice || api.ObjectID == (int)ObjectEnum.SalesOrder || api.ObjectID == (int)ObjectEnum.SalesQuote)
                        {
                            Invoice invoiceModel = new Invoice();
                            InvoiceApiModel invoice = new InvoiceApiModel();
                            //InnerObjectKeys = new List<string>();
                            //InnerObjectKeys.Add("2758ccc2-e085-4677-b4f0-0db836bd2fb8");
                            foreach (var inv in InnerObjectKeys)
                            {
                                int invoiceID = 0;
                                string apiurl = string.Format("{0}/{1}/{2}{3}", Constants.Api_Url, item.BusinessKey, inv, ".json");
                                invoice = FetchApiHelper.GetInvoiceFromApi(apiurl, apiAuth.Username, apiAuth.Password);
                                //code for ByPassing API with no Data that i.e not existing
                                if ((invoice != null ) && 
                                     (!string.IsNullOrEmpty(invoice.IssueDate) || !string.IsNullOrEmpty(invoice.Date) || !string.IsNullOrEmpty(invoice.Reference)
                                     || !string.IsNullOrEmpty(invoice.Customer) || !string.IsNullOrEmpty(invoice.From) || !string.IsNullOrEmpty(invoice.To)
                                     || !string.IsNullOrEmpty(invoice.Description) || !string.IsNullOrEmpty(invoice.Summary) || !string.IsNullOrEmpty(invoice.QuoteSummary) || !string.IsNullOrEmpty(invoice.InvoiceSummary)
                                     || !string.IsNullOrEmpty(invoice.BillingAddress) || invoice.Discount || !string.IsNullOrEmpty(invoice.DiscountType)
                                     || invoice.Rounding || !string.IsNullOrEmpty(invoice.RoundingMethod) || !string.IsNullOrEmpty(invoice.PurchaseOrderNumber) || !string.IsNullOrEmpty(invoice.SalesQuoteNumber)
                                     ))
                                {
                                    invoiceModel.InvoiceKey = inv;
                                    invoiceModel.InvoiceBillNo = invoice.Reference;
                                    if (string.IsNullOrEmpty(invoice.IssueDate) && string.IsNullOrEmpty(invoice.Date))
                                    {
                                        invoiceModel.IssueDate = null;
                                    }
                                    else
                                    {
                                        invoiceModel.IssueDate = !string.IsNullOrEmpty(invoice.IssueDate) ? Convert.ToDateTime(invoice.IssueDate) : Convert.ToDateTime(invoice.Date);
                                    }
                                    if (api.ObjectID == (int)ObjectEnum.PurchaseInvoice)
                                    {
                                        invoiceModel.PartyKey = invoice.From;
                                        invoiceModel.InvoiceSummary = invoice.Description;
                                        invoiceModel.PartyType = (int)PartyEnum.Supplier;
                                    }
                                    else if (api.ObjectID == (int)ObjectEnum.SalesInvoice)
                                    {
                                        invoiceModel.PartyKey = invoice.Customer;
                                        invoiceModel.InvoiceSummary = invoice.InvoiceSummary;
                                        invoiceModel.PartyType = (int)PartyEnum.Customer;
                                    }
                                    else
                                    {
                                        invoiceModel.PartyKey = !string.IsNullOrEmpty(invoice.Customer) ? invoice.Customer : invoice.To;
                                        invoiceModel.InvoiceSummary = !string.IsNullOrEmpty(invoice.Summary) ? invoice.Summary : invoice.QuoteSummary;
                                        invoiceModel.PartyType = (int)PartyEnum.Customer;
                                    }
                                    if (invoice.Rounding)
                                    {
                                        invoiceModel.IsRounding = invoice.Rounding;
                                        if (invoice.RoundingMethod == "RoundDown")
                                            invoiceModel.RoundingMethod = (int)RoundingMethodEnum.RoundDown;
                                        else
                                            invoiceModel.RoundingMethod = (int)RoundingMethodEnum.RoundToNearest;
                                    }
                                    if (invoice.Discount)
                                    {
                                        invoiceModel.IsDiscount = invoice.Discount;
                                        invoiceModel.DiscountType = !string.IsNullOrEmpty(invoice.DiscountType) ? (int)DiscountTypeEnumcs.ExactAmount : (int)DiscountTypeEnumcs.Percentage;
                                    }
                                    invoiceModel.InvoiceType = api.ObjectID;
                                    invoiceModel.BusinessID = item.BusinessID;
                                    invoiceModel.BusinessKey = item.BusinessKey;
                                    invoiceModel.IsActive = true;
                                    invoiceModel.IsAmountsIncludeTax = invoice.AmountsIncludeTax;
                                    invoiceModel.PurchaseOrderNo = invoice.PurchaseOrderNumber;
                                    invoiceModel.SalesQuoteNo = invoice.SalesQuoteNumber;
                                    invoiceModel.Address = invoice.BillingAddress;
                                    invoiceID = SyncApiService.Instance.insertInvoiceKey(invoiceModel);
                                    if (invoice.Lines != null && invoice.Lines.Count > 0)
                                    {
                                        int sequence = 0;
                                        foreach (var desc in invoice.Lines)
                                        {
                                            sequence++;
                                            desc.EntityID = invoiceID;
                                            desc.EntityTypeID = (int)EntityTypeEnum.InvoiceTypeEntity;
                                            desc.Sequence = sequence;
                                            desc.IsActive = true;
                                            desc.DiscountType = desc.Discount > 0 ? (int)DiscountTypeEnumcs.Percentage : 0;
                                            desc.DiscountType = desc.DiscountAmount > 0 ? (int)DiscountTypeEnumcs.ExactAmount : 0;
                                            if (desc.DiscountType > 0)
                                                desc.Discount = desc.Discount > 0 ? desc.Discount : desc.DiscountAmount;
                                        }
                                        SyncApiService.Instance.InsertLineDescriptions(invoice.Lines);
                                        sequence = 0;
                                    }
                                }
                            }
                        }
                        else if (api.ObjectID == (int)ObjectEnum.ReceiptOrPayment)
                        {

                            ReceiptsPayment model = new ReceiptsPayment();
                            ReceiptAndPaymentApiModel receiptandpayment = new ReceiptAndPaymentApiModel();
                            foreach (var trans in InnerObjectKeys)
                            {
                                string apiurl = string.Format("{0}/{1}/{2}{3}", Constants.Api_Url, item.BusinessKey, trans, ".json");
                                receiptandpayment = FetchApiHelper.GetReceiptsPaymentsFromApi(apiurl, apiAuth.Username, apiAuth.Password);
                                if (receiptandpayment != null)
                                {
                                    int ReceiptPaymentID = 0;
                                    model.ReceiptPaymentKey = trans;
                                    if(!string.IsNullOrEmpty(receiptandpayment.Date))
                                        model.ReceiptPaymentDate = Convert.ToDateTime(receiptandpayment.Date);
                                    model.AccountKey = receiptandpayment.BankAccount;
                                    model.Name = receiptandpayment.Description;
                                    model.Payee = receiptandpayment.Contact;
                                    if (!string.IsNullOrEmpty(receiptandpayment.Type))
                                        model.TransectionType = !string.IsNullOrEmpty(receiptandpayment.Type) ? (int)TransectionTypeEnum.Payment : (int)TransectionTypeEnum.Receipt;
                                    else
                                        model.TransectionType = (int)TransectionTypeEnum.Receipt;
                                    model.BusinessID = item.BusinessID;
                                    model.BusinessKey = item.BusinessKey;
                                    model.IsActive = true;
                                    model.IsAmountsIncludeTax = receiptandpayment.AmountsIncludeTax;
                                    model.Reference = receiptandpayment.Reference;
                                    model.IsBalanceClear = !string.IsNullOrEmpty(receiptandpayment.BankClearStatus) ? true : false;
                                    ReceiptPaymentID = SyncApiService.Instance.insertReceiptPaymentKey(model);
                                    if (receiptandpayment.Lines != null && receiptandpayment.Lines.Count > 0)
                                    {
                                        int sequence = 0;
                                        foreach (var desc in receiptandpayment.Lines)
                                        {
                                            sequence++;
                                            desc.EntityID = ReceiptPaymentID;
                                            desc.EntityTypeID = (int)EntityTypeEnum.ReceiptsandPaymentsTypeEntity;
                                            desc.Sequence = sequence;
                                            desc.IsActive = true;
                                            desc.DiscountType = desc.Discount > 0 ? (int)DiscountTypeEnumcs.Percentage : 0;
                                            desc.DiscountType = desc.DiscountAmount > 0 ? (int)DiscountTypeEnumcs.ExactAmount : 0;
                                            if (desc.DiscountType > 0)
                                                desc.Discount = desc.Discount > 0 ? desc.Discount : desc.DiscountAmount;
                                        }
                                        SyncApiService.Instance.InsertLineDescriptions(receiptandpayment.Lines);
                                        sequence = 0;
                                    }
                                }
                            }
                        }
                        else if (api.ObjectID == (int)ObjectEnum.Attachment)
                        {
                            //List<InnerObject> innerObj = new List<InnerObject>();
                            //innerObj = FetchApiService.Instance.getInnerObjectModel(item.BusinessID, item.BusinessKey);
                            List<AttachmentApiModel> attachmentApiList = new List<AttachmentApiModel>();
                            AttachmentApiModel attachment = new AttachmentApiModel();
                            foreach (var att in InnerObjectKeys)
                            {
                                string apiurl = string.Format("{0}/{1}/{2}{3}", Constants.Api_Url, item.BusinessKey, att, ".json");
                                attachment = FetchApiHelper.GetAttachmentFromApi(apiurl, apiAuth.Username, apiAuth.Password);
                                if (attachment != null)
                                {
                                    //attachment.InnerObjectID = innerObj.Where(x => x.InnerObjectKey == attachment.Object).FirstOrDefault().InnerObjectID;
                                    attachment.ApiKey = att;
                                    attachment.BusinessID = item.BusinessID;
                                    attachment.BusinessKey = item.BusinessKey;
                                    attachment.AttachmentUrl = string.Format("{0}/view-attachment?Key={1}&FileID={2}", Constants.Base_Url, attachment.ApiKey, item.BusinessKey);
                                    attachment.IsActive = true;
                                    attachmentApiList.Add(attachment);
                                }   
                            }
                            if (attachmentApiList.Count > 0)
                                SyncApiService.Instance.InsertAttachment(attachmentApiList);
                        }
                    }
                }
            }
        }


    }
}
