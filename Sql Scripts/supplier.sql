select * from Suppliers where BusinessID = 91 and IsActive = 1 order by Name


select sup.SupplierID, sup.SupplierKey, sup.Name as SupplierName,inv.InvoiceID,inv.InvoiceKey, inv.InvoiceBillNo, inv.IssueDate,inv.InvoiceSummary, inv.IsRounding, inv.RoundingMethod,inv.IsDiscount, inv.DiscountType, inv.IsAmountsIncludeTax, d.DescriptionID, d.Qty, d.Amount, d.TaxCode, d.Discount, d.DiscountType,d.Sequence,d.EntityID
from Suppliers sup
inner join Invoice inv on sup.SupplierKey = inv.PartyKey
inner join Description d on inv.InvoiceID = d.EntityID
where sup.BusinessID = 93 and sup.IsActive = 1 and inv.IsActive = 1 and inv.BusinessID = 93 and inv.InvoiceType = 11 and d.EntityTypeID = 1 and d.IsActive = 1 and sup.SupplierID = 134

select d.Description, d.Qty, d.Amount, d.Account,d.Discount, d.DiscountType, r.ReceiptPaymentID, d.InvoiceKey,r.ReceiptPaymentDate, r.AccountKey as BankAccount, r.Name as ReceiptPaymentName, r.Payee,r.TransectionType, r.Reference, s.SupplierID
 from Description d
inner join ReceiptsPayments r on d.EntityID = r.ReceiptPaymentID
inner join Suppliers s on d.Account = s.SupplierKey
where d.IsActive = 1 and r.BusinessID = 93 and r.IsActive = 1 and s.SupplierID = 134



