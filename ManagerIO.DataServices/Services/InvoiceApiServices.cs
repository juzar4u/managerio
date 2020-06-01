using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.InvoiceApiModel;
using ManagerIO.DataModels.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataServices.Services
{
    public class InvoiceApiServices
    {
        #region Define as Singleton
        private static InvoiceApiServices _Instance;
        public static InvoiceApiServices Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new InvoiceApiServices();
                }

                return (_Instance);
            }
        }
        #endregion
        public List<InvoicePartyModel> getPartyInvoices(decimal businessID, int invoiceTypeId)
        {
            List<InvoicePartyModel> Invoices = new List<InvoicePartyModel>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    if (invoiceTypeId != (int)ObjectEnum.PurchaseInvoice)
                    {
                        var sql = PetaPoco.Sql.Builder.Select("cus.CustomerID as PartyID, cus.CustomerKey as PartyKey, cus.CustomerName as PartyName, cus.Code,inv.InvoiceID,inv.InvoiceKey, inv.InvoiceBillNo, inv.IssueDate,inv.InvoiceSummary, inv.IsRounding, inv.RoundingMethod,inv.IsDiscount, inv.DiscountType, inv.IsAmountsIncludeTax, d.DescriptionID, d.Qty, d.Amount, d.TaxCode, d.Discount, d.DiscountType,d.Sequence,d.EntityID, inv.PurchaseOrderNo as OrderNo")
                               .From("Customers cus")
                               .InnerJoin("Invoice inv").On("cus.CustomerKey = inv.PartyKey")
                               .InnerJoin("Description d").On("inv.InvoiceID = d.EntityID")
                               .Where("cus.BusinessID = @0 and cus.IsActive = 1 and inv.IsActive = 1 and inv.BusinessID = @0 and inv.InvoiceType = @1 and d.EntityTypeID = 1 and d.IsActive = 1", businessID, invoiceTypeId);
                        Invoices = repo.Fetch<InvoicePartyModel>(sql);
                    }
                    else
                    {
                        var sql = PetaPoco.Sql.Builder.Select("sup.SupplierID as PartyID, sup.SupplierKey as PartyKey, sup.Name as PartyName,inv.InvoiceID,inv.InvoiceKey, inv.InvoiceBillNo, inv.IssueDate,inv.InvoiceSummary, inv.IsRounding, inv.RoundingMethod,inv.IsDiscount, inv.DiscountType, inv.IsAmountsIncludeTax, d.DescriptionID, d.Qty, d.Amount, d.TaxCode, d.Discount, d.DiscountType,d.Sequence,d.EntityID")
                          .From("Suppliers sup")
                          .InnerJoin("Invoice inv").On("sup.SupplierKey = inv.PartyKey")
                          .InnerJoin("Description d").On("inv.InvoiceID = d.EntityID")
                          .Where("sup.BusinessID = @0 and sup.IsActive = 1 and inv.IsActive = 1 and inv.BusinessID = @0 and inv.InvoiceType = @1 and d.EntityTypeID = 1 and d.IsActive = 1", businessID, invoiceTypeId);
                        Invoices = repo.Fetch<InvoicePartyModel>(sql);
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return Invoices;
        }

        public List<TransectionModel> getPartyTransections(decimal businessID, int invoiceTypeId)
        {
            List<TransectionModel> Transections = new List<TransectionModel>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    if (invoiceTypeId != (int)ObjectEnum.PurchaseInvoice)
                    {
                        var sql = PetaPoco.Sql.Builder.Select("d.Description, d.Qty, d.Amount, d.Account,d.Discount, d.DiscountType, r.ReceiptPaymentID, d.InvoiceKey,r.ReceiptPaymentDate, r.AccountKey as BankAccount, r.Name as ReceiptPaymentName, r.Payee,r.TransectionType, r.Reference, c.CustomerID")
                               .From("Description d")
                               .InnerJoin("ReceiptsPayments r").On("d.EntityID = r.ReceiptPaymentID")
                               .InnerJoin("Customers c").On("d.Account = c.CustomerKey")
                               .Where("d.IsActive = 1 and r.BusinessID = @0 and r.IsActive = 1 and c.BusinessID = @0 and c.IsActive = 1", businessID);
                        Transections = repo.Fetch<TransectionModel>(sql);
                    }
                    else
                    {
                        var sql = PetaPoco.Sql.Builder.Select("d.Description, d.Qty, d.Amount, d.Account,d.Discount, d.DiscountType, r.ReceiptPaymentID, d.InvoiceKey,r.ReceiptPaymentDate, r.AccountKey as BankAccount, r.Name as ReceiptPaymentName, r.Payee,r.TransectionType, r.Reference, s.SupplierID")
                          .From("Description d")
                          .InnerJoin("ReceiptsPayments r").On("d.EntityID = r.ReceiptPaymentID")
                          .InnerJoin("Suppliers s").On("d.Account = s.SupplierKey")
                          .Where("d.IsActive = 1 and r.BusinessID = @0 and r.IsActive = 1 and s.BusinessID = @0 and s.IsActive = 1", businessID);
                        Transections = repo.Fetch<TransectionModel>(sql);
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return Transections;
        }

        public List<TaxCodeModel> getTaxCodes(decimal businessID)
        {
            List<TaxCodeModel> TaxCodes = new List<TaxCodeModel>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("t.Componentkey,t.TaxCodeName, c.Name, c.Rate")
                           .From("TaxCodes t")
                           .InnerJoin("Component c").On("t.TaxCodeID = c.TaxCodeID")
                           .Where("t.BusinessID = @0 and t.IsActive = 1", businessID);
                    TaxCodes = repo.Fetch<TaxCodeModel>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return TaxCodes;
        }

        public List<Attachment> getAttachmentsByBusiness(decimal businessID)
        {
            List<Attachment> attachments = new List<Attachment>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("a.AttachmentID, a.AttachmentKey, a.AttachmentDate, a.Name, a.InnerObjectKey, a.AttachmentUrl")
                           .From("Attachment a")
                           .Where("a.IsActive = 1 and a.BusinessID = @0", businessID);
                    attachments = repo.Fetch<Attachment>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return attachments;
        }
    }
}
