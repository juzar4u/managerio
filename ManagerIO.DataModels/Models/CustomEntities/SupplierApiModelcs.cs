using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataModels.Models.CustomEntities.SupplierApiModelcs
{
    public class SupplierApiMasterModel
    {
        public int TotalInvoices { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SupplierApiModel> Suppliers { get; set; }
    }

    public class SupplierApiModel
    {
        public string Code { get; set; }
        public string SupplierName { get; set; }
        public int InvoiceCount { get; set; }
        public decimal AccountPayable { get; set; }
        public List<InvoiceCountListModel> SupplierInvoices { get; set; }
        public List<InvoiceReceivableListModel> SupplierReceivableInvoices { get; set; }
    }
    public class InvoiceCountListModel
    {
        public int SupplierID { get; set; }
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
        public string Supplier { get; set; }
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
        public int SupplierID { get; set; }
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
        [DataMember]
        public string BillNo { get; set; }
        public int TransectionType { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Contact { get; set; }
        [DataMember]
        public string Amount { get; set; }
        [DataMember]
        public decimal Balance { get; set; }
    }

    public class InvoiceSupplierModel
    {
        public int SupplierID { get; set; }
        public string SupplierKey { get; set; }
        public string SupplierName { get; set; }
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

    public class TransectionSupplierModel
    {
        public string Description { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public string SupplierKey { get; set; }
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
        public int SupplierID { get; set; }
    }

    public class TaxCodeModel
    {
        public string ComponentKey { get; set; }
        public string TaxCodeName { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}
