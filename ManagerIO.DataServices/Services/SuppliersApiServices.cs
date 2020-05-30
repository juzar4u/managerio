using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.SupplierApiModelcs;
using ManagerIO.DataModels.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataServices.Services
{
    public class SuppliersApiServices
    {
        #region Define as Singleton
        private static SuppliersApiServices _Instance;
        public static SuppliersApiServices Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SuppliersApiServices();
                }

                return (_Instance);
            }
        }
        #endregion
        public List<InvoiceSupplierModel> getSupplierInvoices(decimal businessID)
        {
            List<InvoiceSupplierModel> supplierInvoices = new List<InvoiceSupplierModel>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("sup.SupplierID, sup.SupplierKey, sup.Name as SupplierName,inv.InvoiceID,inv.InvoiceKey, inv.InvoiceBillNo, inv.IssueDate,inv.InvoiceSummary, inv.IsRounding, inv.RoundingMethod,inv.IsDiscount, inv.DiscountType, inv.IsAmountsIncludeTax, d.DescriptionID, d.Qty, d.Amount, d.TaxCode, d.Discount, d.DiscountType,d.Sequence,d.EntityID")
                           .From("Suppliers sup")
                           .InnerJoin("Invoice inv").On("sup.SupplierKey = inv.PartyKey")
                           .InnerJoin("Description d").On("inv.InvoiceID = d.EntityID")
                           .Where("sup.BusinessID = @0 and sup.IsActive = 1 and inv.IsActive = 1 and inv.BusinessID = @0 and inv.InvoiceType = @1 and d.EntityTypeID = 1 and d.IsActive = 1", businessID, (int)ObjectEnum.PurchaseInvoice);
                    supplierInvoices = repo.Fetch<InvoiceSupplierModel>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return supplierInvoices;
        }

        public List<TransectionSupplierModel> getSupplierTransections(decimal businessID)
        {
            List<TransectionSupplierModel> supplierTransections = new List<TransectionSupplierModel>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("d.Description, d.Qty, d.Amount, d.Account,d.Discount, d.DiscountType, r.ReceiptPaymentID, d.InvoiceKey,r.ReceiptPaymentDate, r.AccountKey as BankAccount, r.Name as ReceiptPaymentName, r.Payee,r.TransectionType, r.Reference, s.SupplierID")
                           .From("Description d")
                           .InnerJoin("ReceiptsPayments r").On("d.EntityID = r.ReceiptPaymentID")
                           .InnerJoin("Suppliers s").On("d.Account = s.SupplierKey")
                           .Where("d.IsActive = 1 and r.BusinessID = @0 and r.IsActive = 1 and s.BusinessID = @0 and s.IsActive = 1", businessID);
                    supplierTransections = repo.Fetch<TransectionSupplierModel>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return supplierTransections;
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

        public List<Supplier> getAllSuppliers(decimal businessID)
        {
            List<Supplier> supplier = new List<Supplier>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("s.*")
                           .From("Suppliers s")
                           .Where(" s.BusinessID = @0 and s.IsActive = 1", businessID);
                    supplier = repo.Fetch<Supplier>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return supplier;
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
