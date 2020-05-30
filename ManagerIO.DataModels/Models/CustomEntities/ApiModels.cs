using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataModels.Models.CustomEntities.ApiModels
{
    public class BankTabApiModel
    {
        public string Name { get; set; }
        public decimal ClearBalance { get; set; }
        public decimal PendingDeposit { get; set; }
        public decimal PendingWithdrawal { get; set; }
        public decimal ActualBalance { get; set; }
        public List<AccountApiModel> List { get; set; }
    }
    public class ReceiptandPaymentApiModel
    {
        public DateTime ReceiptPaymentDate { get; set; }
        public string Name { get; set; }
        public string Payee { get; set; }
        public int TransectionType { get; set; }
        public string InvoiceKey { get; set; }
        public decimal Amount { get; set; }
        public decimal Qty { get; set; }
        public decimal Balance { get; set; }
        public string BankAccount { get; set; }
        public string ChartAccountKey { get; set; }
        public string ChartAccountName { get; set; }
        public int IsParty { get; set; }
        public bool IsBalanceClear { get; set; }
        public string BankAccountKey { get; set; }
        public int Sequence { get; set; }
        public int ReceiptPaymentID { get; set; }
        public int DescriptionID { get; set; }
        public string Reference { get; set; }
    }

    public class GroupDescription
    {
        public int EntityID { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class DateObject
    {
        public DateTime Date { get; set; }
    }

    [DataContract]
    public class AccountApiModel
    {
        public DateTime ReceiptPaymentDate { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Amount { get; set; }
        [DataMember]
        public decimal Balance { get; set; }
        [DataMember]
        public int ReceiptPaymentID { get; set; }
    }
    public class PartyModel
    {
        public string PartyKey { get; set; }
        public string PartyName { get; set; }
        public string Payee { get; set; }
    }


    [DataContract]
    public class ReceiptPaymentListApiModel
    {
        public DateTime ReceiptPaymentDate { get; set; }
        public int TransectionTypeID { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Reference { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Payee { get; set; }
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string TransectionType { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public int ReceiptPaymentID { get; set; }
    }

    public class ReceiptsPaymentMasterModel
    {
        public decimal TotalPayments { get; set; }
        public decimal TotalReceipts { get; set; }
        public List<ReceiptPaymentListApiModel> List { get; set; }
    }

    public class CustomerApiMasterModel
    {
        public int TotalInvoices { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CustomerApiModel> Customers { get; set; }
    }

    public class CustomerApiModel
    {
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public int InvoiceCount { get; set; }
        public decimal AccountReceivable { get; set; }
        public List<InvoiceCountListModel> CustomerInvoices { get; set; }
        public List<InvoiceReceivableListModel> CustomerReceivableInvoices { get; set; }
    }
    public class InvoiceCountListModel
    {
        public int CustomerID { get; set; }
        public List<InvoiceModel> Invoices { get; set; }
        public decimal InvoiceTotal { get; set; }
        public decimal BalanceTotal { get; set; }
    }

    [DataContract]
    public class InvoiceModel
    {
        [DataMember]
        public bool IsAttachment { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceKey { get; set; }
        [DataMember]
        public string InvoiceDate { get; set; }
        private string _reference;
        private string _orderNo;
        [DataMember]
        public string InvoiceNo { get { return _reference ?? string.Empty; } set { _reference = value; } }
        [DataMember]
        public string OrderNo { get { return _orderNo ?? string.Empty; } set { _orderNo = value; } }
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public decimal InvoiceTotal { get; set; }
        [DataMember]
        public decimal BalanceDue { get; set; }
        [DataMember]
        public string Status { get; set; }
    }


    public class InvoiceReceivableListModel
    {
        public int CustomerID { get; set; }
        public string Header { get; set; }
        public List<AccountReceivableInvoiceModel> Invoices { get; set; }
        public decimal BalanceAmount { get; set; }
    }

    [DataContract]
    public class AccountReceivableInvoiceModel
    {
        public DateTime Date { get; set; }
        [DataMember]
        public string InvoiceDate { get; set; }
        [DataMember]
        public string Transection { get; set; }
        public int TransectionType { get; set; }
        [DataMember]
        public string Description { get; set; }
        public string Contact { get; set; }
        public string BillNo { get; set; }
        [DataMember]
        public string Amount { get; set; }
        [DataMember]
        public decimal Balance { get; set; }
    }

    public class InvoiceCustomerModel
    {
        public int CustomerID { get; set; }
        public string CustomerKey { get; set; }
        public string CustomerName { get; set; }
        public string Code { get; set; }
        public int InvoiceID { get; set; }
        public string InvoiceKey { get; set; }
        public string InvoiceBillNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string InvoiceSummary { get; set; }
        public bool IsRounding { get; set; }
        public int RoundingMethod { get; set; }
        public bool IsDiscount { get; set; }
        public int DiscountType { get; set; }
        public bool IsAmountsIncludeTax { get; set; }
        public int DescriptionID { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public string TaxCode { get; set; }
        public decimal Discount { get; set; }
        public int Sequence { get; set; }
        public int EntityID { get; set; }
        public decimal ProcessedAmount { get; set; }
        public string OrderNo { get; set; }
    }

    public class TransectionCustomerModel
    {
        public string Description { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public string CustomerKey { get; set; }
        public decimal Discount { get; set; }
        public int DiscountType { get; set; }
        public int ReceiptPaymentID { get; set; }
        public string InvoiceKey { get; set; }
        public DateTime ReceiptPaymentDate { get; set; }
        public string BankAccount { get; set; }
        public string ReceiptPaymentName { get; set; }
        public string Payee { get; set; }
        public int TransectionType { get; set; }
        public string Reference { get; set; }
        public int CustomerID { get; set; }
    }

    public class TaxCodeModel
    {
        public string ComponentKey { get; set; }
        public string TaxCodeName { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}

