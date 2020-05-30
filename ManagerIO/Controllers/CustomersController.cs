using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.ApiModels;
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
    public class CustomersController : ApiController
    {
        public CustomerApiMasterModel GetCustomerDetails(decimal businessId)
        {
            CustomerApiMasterModel result = new CustomerApiMasterModel();
            List<InvoiceCustomerModel> CustomerInvoices = CustomersApiServices.Instance.getCustomerInvoices(businessId);
            List<TaxCodeModel> TaxCodes = CustomersApiServices.Instance.getTaxCodes(businessId);
            List<TransectionCustomerModel> CustomerTransections = CustomersApiServices.Instance.getCustomerTransections(businessId);
            List<Customer> customers = CustomersApiServices.Instance.getAllCustomers(businessId);
            List<Attachment> attachments = CustomersApiServices.Instance.getAttachmentsByBusiness(businessId);
            result.Customers = new List<CustomerApiModel>();
            foreach (var item in customers.OrderBy(x=>x.CustomerName))
            {
                CustomerApiModel customerModel = new CustomerApiModel();
                List<InvoiceCustomerModel> customerInvoices = CustomerInvoices.Where(x => x.CustomerID == item.CustomerID).ToList();
                List<TransectionCustomerModel> customerTransections = CustomerTransections.Where(x => x.CustomerID == item.CustomerID).ToList();
                customerModel = CustomerHelper.getCustomerApiModel(customerInvoices, customerTransections, TaxCodes, item, attachments);
                result.Customers.Add(customerModel);
            }
            result.TotalInvoices = result.Customers.Select(x => x.InvoiceCount).Sum();
            result.TotalAmount = result.Customers.Select(x => x.AccountReceivable).Sum();
            return result;
        }
    }
}
