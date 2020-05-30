using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataModels.Models.CustomEntities.SyncApiModels
{
    
    [Serializable]
    public class JsonModel
    {
        public string ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public string Data { get; set; }
        public int JsonRequestBehavior { get; set; }
    }
    [Serializable]
    public class BusinessApiJsonModel
    {
        public string ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public List<BusinessApiModel> Data { get; set; }
        public int JsonRequestBehavior { get; set; }
    }

    public class BusinessApiModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
    }
    public class ApiAuthModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class AttachmentApiModel
    {
        public string ApiKey { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public decimal Size { get; set; }
        public string Object { get; set; }
        public string IsLocal { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
        public string  AttachmentUrl { get; set; }
    }
    public class AccountApiModel
    {
        public string ApiKey { get; set; }
        public string Name { get; set; }
        public decimal CreditLimit { get; set; }
        public string Code { get; set; }
        public int AccountType { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
    }
    public class BusinessLogoApiModel
    {
        public string ApiKey { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
    }
    public class BusinessDetailApiModel
    {
        public string ApiKey { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
    }
    public class PartyApiModel
    {
        public string ApiKey { get; set; }
        public string Name { get; set; }
        //incase of customer
        public string BillingAddress { get; set; }
        //incase of Supplier
        public string Address { get; set; }
        public string Email { get; set; }
        public string BusinessIdentifier { get; set; }
        public string Code { get; set; }
        public decimal CreditLimit { get; set; }
        public int PartyTypeID { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
    }
    public class FolderApiModel
    {
        public string ApiKey { get; set; }
        public string Description { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
    }
    public class InventoryApiModel
    {
        public string ApiKey { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Inactive { get; set; }
        public string Description { get; set; }
        public string UnitName { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public string TaxCode { get; set; }
        public string IncomeAccount { get; set; }
        public string ExpenseAccount { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
    }
    public class ChartofAccApiModel
    {
        public string ApiKey { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public int? ParentAccountID { get; set; }
        public int Position { get; set; }
        public bool IsAccountGroup { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
    }
    public class InvoiceApiModel
    {
        public string ApiKey { get; set; }
        public string IssueDate { get; set; }
        //incase of Sales order
        public string Date { get; set; }
        public string Reference { get; set; }
        public int PartyID { get; set; }
        public int PartyType { get; set; }
        public string PartyKey { get; set; }
        //incase of salesinvoice and sales order
        public string Customer { get; set; }
        //incase of purchaseinvoice
        public string From { get; set; }
        //incase of sales quote
        public string To { get; set; }
        public List<DescriptionApiModel> Lines { get; set; }
        public string InvoiceSummaryField { get; set; }
        //Description in case of purchase invoice
        public string Description { get; set; }
        //Summary in case of sales order
        public string Summary { get; set; }
        //Summary in case of sales quote
        public string QuoteSummary { get; set; }
        //InvoiceSummary in case of sales invoice
        public string InvoiceSummary { get; set; }
        //BillingAddress in case of sales invoice and sales order
        public string BillingAddress { get; set; }
        public bool Discount { get; set; }
        //Type Empty = Percentage else ExactAmount
        public string DiscountType { get; set; }
        public int DiscountTypeEnum { get; set; }
        public bool AmountsIncludeTax { get; set; }
        //incase of sales invoice
        public bool Rounding { get; set; }
        public string RoundingMethod { get; set; }
        public int? RoundingMethodEnum { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string SalesQuoteNumber { get; set; }
        public int InvoiceTypeID { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
    }
    public class DescriptionApiModel
    {
        public string Description { get; set; }
        public string Account { get; set; }
        public string TaxCode { get; set; }
        public decimal Qty { get; set; }
        public string Item { get; set; }
        public decimal Amount { get; set; }
        //Discount value if in Percentage
        public decimal Discount { get; set; }
        //DiscountAmount value if in ExactAmount
        public decimal DiscountAmount { get; set; }
        public bool IsActive { get; set; }
        public int? DiscountType { get; set; }
        public int? EntityID { get; set; }
        public int? EntityTypeID { get; set; }
        public string Invoice { get; set; }
        public int Sequence { get; set; }

    }
    public class ReceiptAndPaymentApiModel
    {
        public string ApiKey { get; set; }
        public string Date { get; set; }
        public string BankAccount { get; set; }
        public string Description { get; set; } 
        public List<DescriptionApiModel> Lines { get; set; }
        public string Contact { get; set; }
        public bool AmountsIncludeTax { get; set; }
        public string Type { get; set; }
        public string Reference { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
        public int TransectionType { get; set; }
        public string BankClearStatus { get; set; }
    }
    public class TaxCodeApiModel
    {
        public string ApiKey { get; set; }
        public string Name { get; set; }
        public List<ComponentApiModel> Components { get; set; }
        public string TaxRate { get; set; }
        public string TaxRateType { get; set; }
        public string Account { get; set; }
        public bool CustomSalesInvoiceTitle { get; set; }
        public string SalesInvoiceTitle { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
    }
    public class ComponentApiModel
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public string Account { get; set; }
        public decimal BusinessID { get; set; }
        public string BusinessKey { get; set; }
        public bool IsActive { get; set; }
        public int TaxCodeID { get; set; }
    }

}
