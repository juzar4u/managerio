using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.SupplierApiModelcs;
using ManagerIO.DataServices.Helper;
using ManagerIO.DataServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ManagerIO.Controllers
{
    public class SuppliersController : ApiController
    {
        public SupplierApiMasterModel GetSupplierDetails(decimal businessId)
        {
            SupplierApiMasterModel result = new SupplierApiMasterModel();
            List<InvoiceSupplierModel> SupplierInvoices = SuppliersApiServices.Instance.getSupplierInvoices(businessId);
            List<TaxCodeModel> TaxCodes = SuppliersApiServices.Instance.getTaxCodes(businessId);
            List<TransectionSupplierModel> SupplierTransections = SuppliersApiServices.Instance.getSupplierTransections(businessId);
            List<Supplier> suppliers = SuppliersApiServices.Instance.getAllSuppliers(businessId);
            List<Attachment> attachments = CustomersApiServices.Instance.getAttachmentsByBusiness(businessId);
            result.Suppliers = new List<SupplierApiModel>();
            foreach (var item in suppliers.Where(x=>x.SupplierID == 134).OrderBy(x => x.Name))
            {
                SupplierApiModel supplierModel = new SupplierApiModel();
                List<InvoiceSupplierModel> supplierInvoices = SupplierInvoices.Where(x => x.SupplierID == item.SupplierID).ToList();
                List<TransectionSupplierModel> supplierTransections = SupplierTransections.Where(x => x.SupplierID == item.SupplierID).ToList();
                supplierModel = SupplierHelper.getSupplierApiModel(supplierInvoices, supplierTransections, TaxCodes, item, attachments);
                result.Suppliers.Add(supplierModel);
            }
            result.TotalInvoices = result.Suppliers.Select(x => x.InvoiceCount).Sum();
            result.TotalAmount = result.Suppliers.Select(x => x.AccountPayable).Sum();
            return result;
        }
    }
}
