using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.SupplierApiModelcs;
using ManagerIO.DataModels.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataServices.Helper
{
    public static class SupplierHelper
    {
        public static SupplierApiModel getSupplierApiModel(List<InvoiceSupplierModel> supplierInvoices, List<TransectionSupplierModel> supplierTransections, List<TaxCodeModel> taxCodes, Supplier supplier, List<Attachment> attachments)
        {
            SupplierApiModel model = new SupplierApiModel();
            model.SupplierInvoices = new List<InvoiceCountListModel>();
            model.SupplierReceivableInvoices = new List<InvoiceReceivableListModel>();
            model.Code = string.IsNullOrEmpty(supplier.Code) ? string.Empty : supplier.Code;
            model.SupplierName = supplier.Name;
            if (supplierInvoices.Count > 0 || supplierTransections.Count > 0)
            {
                List<InvoiceSupplierModel> processedList = supplierInvoices.Where(x => x.Sequence == 1).OrderBy(x => x.IssueDate).ToList();
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
                    countListModel.SupplierID = invoice.SupplierID;
                    accountReceivableModel.SupplierID = invoice.SupplierID;
                    accountReceivableModel.Header = string.IsNullOrEmpty(invoice.Code) ? string.Format("{0} — Account Payable", invoice.SupplierName) : string.Format("{0} - {1} — Account Payable", invoice.Code, invoice.SupplierName);
                    InvoiceModel invoiceModel = new InvoiceModel();
                    AccountReceivableInvoiceModel receivableModel = new AccountReceivableInvoiceModel();
                    invoiceModel.Date = invoice.IssueDate;
                    invoiceModel.InvoiceKey = invoice.InvoiceKey;
                    invoiceModel.InvoiceDate = invoice.IssueDate.ToString("dd/MM/yyyy");
                    invoiceModel.Supplier = string.IsNullOrEmpty(invoice.Code) ? invoice.SupplierName : string.Format("{0} - {1}", invoice.Code, invoice.SupplierName);
                    invoiceModel.Description = invoice.InvoiceSummary;
                    invoiceModel.InvoiceNo = invoice.InvoiceBillNo;
                    invoiceModel.OrderNo = invoice.OrderNo;
                    invoiceModel.IsAttachment = attachments.Where(x => x.InnerObjectKey == invoice.InvoiceKey).ToList().Count > 0 ? true : false;
                    receivableModel.Date = invoice.IssueDate;
                    receivableModel.InvoiceDate = invoice.IssueDate.ToString("dd/MM/yyyy");
                    receivableModel.Transection = "Purchase Invoice";
                    receivableModel.TransectionType = receivableModel.Transection.Contains("Purchase Invoice") ? (int)TransectionTypeEnum.Payment : (int)TransectionTypeEnum.Receipt;
                    receivableModel.Description = invoice.InvoiceSummary;
                    receivableModel.Contact = invoice.SupplierName;
                    receivableModel.BillNo = !string.IsNullOrEmpty(invoice.InvoiceBillNo) ? invoice.InvoiceBillNo : string.Empty;
                    if (invoice.Qty > 0)
                        amount = invoice.Qty * invoice.Amount;
                    else
                        amount = invoice.Amount;
                    foreach (var tax in taxCodes.Where(x => x.ComponentKey == invoice.TaxCode).ToList())
                    {
                        taxAmount += (tax.Rate / 100) * amount;
                    }
                    totalAmount = amount + taxAmount;

                    foreach (var innerInv in supplierInvoices.Where(x => x.EntityID == invoice.EntityID).Where(x => x.Sequence > 1).ToList())
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
                    invoiceModel.InvoiceTotal = totalAmount;
                    receivableModel.Amount = Math.Round(totalAmount).ToString();
                    invoice.ProcessedAmount = invoiceModel.InvoiceTotal;
                    countListModel.Invoices.Add(invoiceModel);
                    accountReceivableModel.Invoices.Add(receivableModel);
                }
                countListModel.InvoiceTotal = countListModel.Invoices.Select(x => x.InvoiceTotal).Sum();
                foreach (var transection in supplierTransections.OrderBy(x => x.ReceiptPaymentDate))
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
                    receivableModel.BillNo = !string.IsNullOrEmpty(transection.Reference) ? transection.Reference : string.Empty;
                    if (transection.Amount.ToString().Contains('-') && transection.TransectionType == (int)TransectionTypeEnum.Payment)
                    {
                        decimal amt = Math.Round(System.Math.Abs(transection.Amount));
                        receivableModel.Amount = amt.ToString();
                    }
                    else
                    {
                        receivableModel.Amount = transection.TransectionType == (int)TransectionTypeEnum.Payment ? string.Format("-{0}", Math.Round(transection.Amount)) : Math.Round(transection.Amount).ToString();
                    }
                    if (transection.TransectionType == (int)TransectionTypeEnum.Payment)
                        totalReceivable += transection.Amount;
                    else
                        totalReceivable -= transection.Amount;
                    
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
                
                decimal receivableCase2 = 0;
                bool IsSubtracted = false;
                receivableCase2 = supplierTransections.Where(x => string.IsNullOrEmpty(x.InvoiceKey)).Select(x => x.Amount).Sum();
                foreach (var invoice in countListModel.Invoices.OrderBy(x => x.Date))
                {
                    decimal receivableCase1 = 0;
                    bool isReceived = false;
                    if (supplierTransections.Count > 0)
                    {
                        List<TransectionSupplierModel> transections = supplierTransections.Where(x => x.InvoiceKey == invoice.InvoiceKey).ToList();
                        foreach (var item in transections)
                        {
                            if (item.TransectionType == (int)TransectionTypeEnum.Payment)
                                receivableCase1 += item.Amount;
                            else
                                receivableCase1 -= item.Amount;
                        }
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
                        else if (receivableCase2 == 0 && supplierTransections.Where(x => string.IsNullOrEmpty(x.InvoiceKey)).ToList().Count > 0)
                        {
                            invoice.BalanceDue = 0;
                            invoice.Status = "Paid in full";
                        }
                        if (supplierTransections.Where(x => string.IsNullOrEmpty(x.InvoiceKey)).ToList().Count > 0 && isReceived == false)
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
                //if (countListModel.Invoices.OrderByDescending(x => x.Date).FirstOrDefault().BalanceDue == 0 && countListModel.Invoices.Where(x => x.BalanceDue > 0).ToList().Count > 0)

                   //countListModel.Invoices = countListModel.Invoices.OrderByDescending(x => x.BalanceDue).ThenByDescending(x => x.Date).ToList();
                //else
                //    countListModel.Invoices = countListModel.Invoices.OrderByDescending(x => x.Date).ThenByDescending(x => x.InvoiceNo).ThenByDescending(x => x.BalanceDue).ToList();
                   List<InvoiceModel> ProcessedInvoices = new List<InvoiceModel>();
                //processedList.AddRange(countListModel.Invoices.Where(x=>x.)
                countListModel.BalanceTotal = countListModel.InvoiceTotal - totalReceivable;
                accountReceivableModel.BalanceAmount = countListModel.BalanceTotal;
                model.AccountPayable = countListModel.BalanceTotal;
                model.SupplierInvoices.Add(countListModel);
                accountReceivableModel.Invoices = accountReceivableModel.Invoices.OrderBy(x => x.Date).ThenBy(x => x.TransectionType).ThenBy(x => x.BillNo).ToList();
                accountReceivableModel.Invoices.Reverse();
                model.SupplierReceivableInvoices.Add(accountReceivableModel);
            }
            return model;
        }
    }
}
