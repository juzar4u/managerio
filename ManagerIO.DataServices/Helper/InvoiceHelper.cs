using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.InvoiceApiModel;
using ManagerIO.DataModels.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataServices.Helper
{
    public static class InvoiceHelper
    {
        public static List<InvoiceApiModel> getInvoiceApiModel(List<InvoicePartyModel> invoices, List<TransectionModel> transections, List<TaxCodeModel> taxCodes, List<Attachment> attachments, int invoiceTypeId)
        {
            List<InvoiceApiModel> modelList = new List<InvoiceApiModel>();
            if (invoices.Count > 0 || transections.Count > 0)
            {
                List<InvoicePartyModel> processedList = invoices.Where(x => x.Sequence == 1).OrderBy(x => x.IssueDate).ToList();
                foreach (var invoice in processedList.OrderBy(x => x.IssueDate))
                {
                    InvoiceApiModel model = new InvoiceApiModel();
                    decimal totalAmount = 0;
                    decimal amount = 0;
                    decimal taxAmount = 0;
                    
                    model.Date = invoice.IssueDate;
                    model.InvoiceKey = invoice.InvoiceKey;
                    model.InvoiceDate = invoice.IssueDate.ToString("dd/MM/yyyy");
                    model.Party = string.IsNullOrEmpty(invoice.Code) ? invoice.PartyName : string.Format("{0} - {1}", invoice.Code, invoice.PartyName);
                    model.Description = invoice.InvoiceSummary;
                    model.InvoiceNo = invoice.InvoiceBillNo;
                    model.OrderNo = invoice.OrderNo;
                    model.IsAttachment = attachments.Where(x => x.InnerObjectKey == invoice.InvoiceKey).ToList().Count > 0 ? true : false;
                   
                    if (invoice.Qty > 0)
                        amount = invoice.Qty * invoice.Amount;
                    else
                        amount = invoice.Amount;
                    foreach (var tax in taxCodes.Where(x => x.ComponentKey == invoice.TaxCode).ToList())
                    {
                        taxAmount += (tax.Rate / 100) * amount;
                    }
                    totalAmount = amount + taxAmount;

                    foreach (var innerInv in invoices.Where(x => x.EntityID == invoice.EntityID).Where(x => x.Sequence > 1).ToList())
                    {
                        decimal inrTotalAmount = 0;
                        decimal inrAmount = 0;
                        decimal inrTaxAmount = 0;
                        if (innerInv.Qty > 0)
                            inrAmount = innerInv.Qty * innerInv.Amount;
                        else
                            inrAmount = innerInv.Amount;
                        foreach (var tax in taxCodes.Where(x => x.ComponentKey == innerInv.TaxCode).ToList())
                        {
                            inrTaxAmount += (tax.Rate / 100) * inrAmount;
                        }
                        inrTotalAmount = inrAmount + inrTaxAmount;
                        totalAmount += inrTotalAmount;
                    }
                    if (invoice.IsRounding)
                    {
                        totalAmount = invoice.RoundingMethod == (int)RoundingMethodEnum.RoundToNearest ? Math.Round(totalAmount, MidpointRounding.AwayFromZero) : Math.Floor(totalAmount);
                    }
                    model.InvoiceTotal = totalAmount;
                    modelList.Add(model);
                }
                if (invoiceTypeId == (int)ObjectEnum.SalesInvoice || invoiceTypeId == (int)ObjectEnum.PurchaseInvoice)
                {
                    foreach (var transection in transections.OrderBy(x => x.ReceiptPaymentDate))
                    {
                       
                    }
                }
            }
            return modelList;
        }
    }
}
