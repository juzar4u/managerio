using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.ApiModels;
using ManagerIO.DataModels.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataServices.Helper
{
    public static class CustomerHelper
    {
        public static CustomerApiModel getCustomerApiModel(List<InvoiceCustomerModel> customerInvoices, List<TransectionCustomerModel> customerTransections, List<TaxCodeModel> taxCodes, Customer customer, List<Attachment> attachments)
        {
            CustomerApiModel model = new CustomerApiModel();
            model.CustomerInvoices = new List<InvoiceCountListModel>();
            model.CustomerReceivableInvoices = new List<InvoiceReceivableListModel>();
            model.Code = string.IsNullOrEmpty(customer.Code) ? string.Empty : customer.Code;
            model.CustomerName = customer.CustomerName;
            if (customerInvoices.Count > 0 || customerTransections.Count > 0)
            {
                List<InvoiceCustomerModel> processedList = customerInvoices.Where(x => x.Sequence == 1).OrderBy(x => x.IssueDate).ToList();
                model.InvoiceCount = processedList.Count();
                decimal totalReceivable = 0;
                InvoiceCountListModel countListModel = new InvoiceCountListModel();
                countListModel.Invoices = new List<InvoiceModel>();
                InvoiceReceivableListModel accountReceivableModel = new InvoiceReceivableListModel();
                accountReceivableModel.Invoices = new List<AccountReceivableInvoiceModel>();
                decimal balance = 0;
                foreach (var invoice in processedList.OrderBy(x => x.IssueDate))
                {
                    decimal totalAmount = 0;
                    decimal amount = 0;
                    decimal taxAmount = 0;
                    countListModel.CustomerID = invoice.CustomerID;
                    accountReceivableModel.CustomerID = invoice.CustomerID;
                    accountReceivableModel.Header = string.IsNullOrEmpty(invoice.Code) ? string.Format("{0} — Account Receivable", invoice.CustomerName) : string.Format("{0} - {1} — Account Receivable", invoice.Code, invoice.CustomerName);
                    InvoiceModel invoiceModel = new InvoiceModel();
                    AccountReceivableInvoiceModel receivableModel = new AccountReceivableInvoiceModel();
                    invoiceModel.Date = invoice.IssueDate;
                    invoiceModel.InvoiceKey = invoice.InvoiceKey;
                    invoiceModel.InvoiceDate = invoice.IssueDate.ToString("dd/MM/yyyy");
                    invoiceModel.Customer = string.IsNullOrEmpty(invoice.Code) ? invoice.CustomerName : string.Format("{0} - {1}", invoice.Code, invoice.CustomerName);
                    invoiceModel.Description = invoice.InvoiceSummary;
                    invoiceModel.InvoiceNo = invoice.InvoiceBillNo;
                    invoiceModel.OrderNo = invoice.OrderNo;
                    invoiceModel.IsAttachment = attachments.Where(x => x.InnerObjectKey == invoice.InvoiceKey).ToList().Count > 0 ? true : false;
                    receivableModel.Date = invoice.IssueDate;
                    receivableModel.InvoiceDate = invoice.IssueDate.ToString("dd/MM/yyyy");
                    receivableModel.Transection = string.IsNullOrEmpty(invoice.InvoiceBillNo) ? "Sales Invoice" : string.Format("Sales Invoice — {0}", invoice.InvoiceBillNo);
                    receivableModel.TransectionType = receivableModel.Transection.Contains("Sales Invoice") ? (int)TransectionTypeEnum.Payment : (int)TransectionTypeEnum.Receipt;
                    receivableModel.Description = invoice.InvoiceSummary;
                    receivableModel.Contact = invoice.CustomerName;
                    receivableModel.BillNo = invoice.InvoiceBillNo;
                    if (invoice.Qty > 0)
                        amount = invoice.Qty * invoice.Amount;
                    else
                        amount = invoice.Amount;
                    foreach (var tax in taxCodes.Where(x => x.ComponentKey == invoice.TaxCode).ToList())
                    {
                        taxAmount += (tax.Rate / 100) * amount;
                    }
                    totalAmount = amount + taxAmount;

                    foreach (var innerInv in customerInvoices.Where(x => x.EntityID == invoice.EntityID).Where(x => x.Sequence > 1).ToList())
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
                        totalAmount = invoice.RoundingMethod == (int)RoundingMethodEnum.RoundToNearest ? Math.Round(totalAmount,MidpointRounding.AwayFromZero) : Math.Floor(totalAmount);
                    }
                    invoiceModel.InvoiceTotal = totalAmount;
                    receivableModel.Amount = totalAmount.ToString();
                    invoice.ProcessedAmount = invoiceModel.InvoiceTotal;
                    countListModel.Invoices.Add(invoiceModel);
                    accountReceivableModel.Invoices.Add(receivableModel);
                }
                countListModel.InvoiceTotal = countListModel.Invoices.Select(x => x.InvoiceTotal).Sum();
                foreach (var transection in customerTransections.OrderBy(x => x.ReceiptPaymentDate))
                {
                    AccountReceivableInvoiceModel receivableModel = new AccountReceivableInvoiceModel();
                    receivableModel.Date = transection.ReceiptPaymentDate;
                    receivableModel.InvoiceDate = transection.ReceiptPaymentDate.ToString("dd/MM/yyyy");
                    if (transection.TransectionType > 0)
                    {
                        receivableModel.Transection = transection.TransectionType == (int)TransectionTypeEnum.Receipt ? "Receipt" : "Payment";
                        receivableModel.TransectionType = receivableModel.Transection == "Receipt" ? (int)TransectionTypeEnum.Receipt : (int)TransectionTypeEnum.Payment;
                    }
                    receivableModel.Description = string.IsNullOrEmpty(transection.ReceiptPaymentName) ? string.Empty : transection.ReceiptPaymentName;
                    receivableModel.Contact = transection.Payee;
                    if (transection.Amount.ToString().Contains('-') && transection.TransectionType == (int)TransectionTypeEnum.Receipt)
                    {
                        decimal amt =  Math.Round(System.Math.Abs(transection.Amount));
                        receivableModel.Amount = amt.ToString();
                    }
                    else
                    {
                        receivableModel.Amount = transection.TransectionType == (int)TransectionTypeEnum.Receipt ? string.Format("-{0}", Math.Round(transection.Amount)) : transection.Amount.ToString();
                    }
                    totalReceivable += transection.Amount;
                    accountReceivableModel.Invoices.Add(receivableModel);
                }
                foreach (var receivables in accountReceivableModel.Invoices.OrderBy(x => x.Date).ThenBy(x => x.TransectionType).ThenBy(x => x.BillNo))
                {
                    bool isPayment = receivables.Amount.Contains('-') ? true : false;
                    if (isPayment)
                        balance += Convert.ToDecimal(receivables.Amount);
                    else
                        balance += Convert.ToDecimal(receivables.Amount);

                    receivables.Balance = balance;
                }
                decimal receivableCase1 = 0;
                decimal receivableCase2 = 0;
                bool IsSubtracted = false;
                receivableCase2 = customerTransections.Where(x => string.IsNullOrEmpty(x.InvoiceKey)).Select(x => x.Amount).Sum();
                foreach (var invoice in countListModel.Invoices.OrderBy(x => x.Date))
                {
                    bool isReceived = false;
                    if (customerTransections.Count > 0)
                    {
                        receivableCase1 = customerTransections.Where(x => x.InvoiceKey == invoice.InvoiceKey).Select(x => x.Amount).Sum();
                        if (receivableCase1 > 0)
                        {
                            isReceived = true;
                            invoice.BalanceDue = invoice.InvoiceTotal - receivableCase1;
                            invoice.Status = invoice.BalanceDue == 0 ? "Paid in full" : string.Format("{0} days overdue", Math.Round((DateTime.Now - invoice.Date).TotalDays));
                        }
                        else if (receivableCase2 > 0)
                        {
                            if (receivableCase2 > invoice.InvoiceTotal)
                            {
                                invoice.BalanceDue = 0;
                                invoice.Status = "Paid in full";
                            }
                            else
                            {
                                invoice.BalanceDue = invoice.InvoiceTotal - receivableCase2;
                                invoice.Status = invoice.BalanceDue == 0 ? "Paid in full" : string.Format("{0} days overdue", Math.Round((DateTime.Now - invoice.Date).TotalDays));
                            }
                        }
                        else if (receivableCase2 < 0)
                        {
                            if (IsSubtracted == false)
                            {
                                IsSubtracted = true;
                                invoice.BalanceDue = invoice.InvoiceTotal - System.Math.Abs(receivableCase2);
                                invoice.Status = string.Format("{0} days overdue", Math.Round((DateTime.Now - invoice.Date).TotalDays));
                            }
                            else
                            {
                                invoice.BalanceDue = invoice.InvoiceTotal;
                                invoice.Status = string.Format("{0} days overdue", Math.Round((DateTime.Now - invoice.Date).TotalDays));
                            }
                        }
                        else if (receivableCase2 == 0 && receivableCase1 == 0)
                        {
                            IsSubtracted = true;
                            invoice.BalanceDue = invoice.InvoiceTotal;
                            invoice.Status = string.Format("{0} days overdue", Math.Round((DateTime.Now - invoice.Date).TotalDays));
                        }
                        else if (receivableCase2 == 0 && customerTransections.Where(x => string.IsNullOrEmpty(x.InvoiceKey)).ToList().Count > 0)
                        {
                            invoice.BalanceDue = 0;
                            invoice.Status = "Paid in full";
                        }
                        if (customerTransections.Where(x => string.IsNullOrEmpty(x.InvoiceKey)).ToList().Count > 0 && isReceived == false)
                        {
                            if (receivableCase2 > invoice.InvoiceTotal)
                            {
                                receivableCase2 -= invoice.InvoiceTotal;
                            }
                            else
                            {
                                receivableCase2 = 0;
                            }
                        }
                    }
                    else
                    {
                        invoice.BalanceDue = invoice.InvoiceTotal;
                        invoice.Status = string.Format("{0} days overdue", Math.Round((DateTime.Now - invoice.Date).TotalDays));
                    }
                }
                if (countListModel.Invoices.OrderByDescending(x => x.Date).FirstOrDefault().BalanceDue == 0 && countListModel.Invoices.Where(x => x.BalanceDue > 0).ToList().Count > 0)

                    countListModel.Invoices = countListModel.Invoices.OrderByDescending(x => x.BalanceDue).ThenByDescending(x => x.Date).ToList();
                else
                    countListModel.Invoices = countListModel.Invoices.OrderByDescending(x => x.Date).ThenByDescending(x => x.InvoiceNo).ThenByDescending(x => x.BalanceDue).ToList();
                countListModel.BalanceTotal = countListModel.InvoiceTotal - totalReceivable;
                accountReceivableModel.BalanceAmount = countListModel.BalanceTotal;
                model.AccountReceivable = countListModel.BalanceTotal;
                model.CustomerInvoices.Add(countListModel);
                accountReceivableModel.Invoices = accountReceivableModel.Invoices.OrderBy(x => x.Date).ThenBy(x => x.TransectionType).ThenBy(x => x.BillNo).ToList();
                accountReceivableModel.Invoices.Reverse();
                model.CustomerReceivableInvoices.Add(accountReceivableModel);
            }
            return model;
        }
    }
}
