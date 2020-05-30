using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.ApiModels;
using ManagerIO.DataModels.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataServices.Services
{
    public class CustomersApiServices
    {
        #region Define as Singleton
        private static CustomersApiServices _Instance;
        public static CustomersApiServices Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CustomersApiServices();
                }

                return (_Instance);
            }
        }
        #endregion

        public List<InvoiceCustomerModel> getCustomerInvoices(decimal businessID)
        {
            List<InvoiceCustomerModel> customerInvoices = new List<InvoiceCustomerModel>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("cus.CustomerID, cus.CustomerKey, cus.CustomerName, cus.Code,inv.InvoiceID,inv.InvoiceKey, inv.InvoiceBillNo, inv.IssueDate,inv.InvoiceSummary, inv.IsRounding, inv.RoundingMethod,inv.IsDiscount, inv.DiscountType, inv.IsAmountsIncludeTax, d.DescriptionID, d.Qty, d.Amount, d.TaxCode, d.Discount, d.DiscountType,d.Sequence,d.EntityID, inv.PurchaseOrderNo as OrderNo")
                           .From("Customers cus")
                           .InnerJoin("Invoice inv").On("cus.CustomerKey = inv.PartyKey")
                           .InnerJoin("Description d").On("inv.InvoiceID = d.EntityID")
                           .Where("cus.BusinessID = @0 and cus.IsActive = 1 and inv.IsActive = 1 and inv.BusinessID = @0 and inv.InvoiceType = @1 and d.EntityTypeID = 1 and d.IsActive = 1", businessID, (int)ObjectEnum.SalesInvoice);
                    customerInvoices = repo.Fetch<InvoiceCustomerModel>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return customerInvoices;
        }

        public List<TransectionCustomerModel> getCustomerTransections(decimal businessID)
        {
            List<TransectionCustomerModel> customerTransections = new List<TransectionCustomerModel>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("d.Description, d.Qty, d.Amount, d.Account,d.Discount, d.DiscountType, r.ReceiptPaymentID, d.InvoiceKey,r.ReceiptPaymentDate, r.AccountKey as BankAccount, r.Name as ReceiptPaymentName, r.Payee,r.TransectionType, r.Reference, c.CustomerID")
                           .From("Description d")
                           .InnerJoin("ReceiptsPayments r").On("d.EntityID = r.ReceiptPaymentID")
                           .InnerJoin("Customers c").On("d.Account = c.CustomerKey")
                           .Where("d.IsActive = 1 and r.BusinessID = @0 and r.IsActive = 1 and c.BusinessID = @0 and c.IsActive = 1", businessID);
                    customerTransections = repo.Fetch<TransectionCustomerModel>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return customerTransections;
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

        public List<Customer> getAllCustomers(decimal businessID)
        {
            List<Customer> customers = new List<Customer>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("c.*")
                           .From("Customers c")
                           .Where("c.BusinessID = @0 and c.IsActive = 1", businessID);
                    customers = repo.Fetch<Customer>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return customers;
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
