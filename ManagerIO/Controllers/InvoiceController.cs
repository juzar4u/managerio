using System;
using ManagerIO.DataModels.Models.CustomEntities.InvoiceApiModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ManagerIO.DataServices.Services;
using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.Enumerations;
using ManagerIO.DataServices.Helper;

namespace ManagerIO.Controllers
{
    public class InvoiceController : ApiController
    {
        public InvoiceMasterModel GetInvoices(decimal businessId, int invoiceTypeId)
        {
            InvoiceMasterModel result = new InvoiceMasterModel();
            List<InvoicePartyModel> Invoices = InvoiceApiServices.Instance.getPartyInvoices(businessId, invoiceTypeId);
            List<TaxCodeModel> TaxCodes = InvoiceApiServices.Instance.getTaxCodes(businessId);
            List<TransectionModel> PartyTransections = new List<TransectionModel>();
            if (invoiceTypeId == (int)ObjectEnum.SalesInvoice || invoiceTypeId == (int)ObjectEnum.PurchaseInvoice)
            {
               PartyTransections = InvoiceApiServices.Instance.getPartyTransections(businessId, invoiceTypeId);
            }
            List<Attachment> attachments = InvoiceApiServices.Instance.getAttachmentsByBusiness(businessId);
            result.Invoices = InvoiceHelper.getInvoiceApiModel(Invoices, PartyTransections, TaxCodes, attachments, invoiceTypeId);
            return result;
        }
    }
}
