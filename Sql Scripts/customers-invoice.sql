
select cus.CustomerID, cus.CustomerKey, cus.CustomerName, cus.Code,inv.InvoiceID,inv.InvoiceKey, inv.InvoiceBillNo, inv.IssueDate,inv.InvoiceSummary, inv.IsRounding, inv.RoundingMethod,inv.IsDiscount, inv.DiscountType, inv.IsAmountsIncludeTax, d.DescriptionID, d.Qty, d.Amount, d.TaxCode, d.Discount, d.DiscountType,d.Sequence
from Customers cus 

inner join Invoice inv on cus.CustomerKey = inv.PartyKey
inner join Description d On inv.InvoiceID = d.EntityID
where cus.BusinessID = 89 and cus.IsActive = 1 and inv.IsActive = 1 and inv.BusinessID = 89 and inv.InvoiceType = 13 and d.EntityTypeID = 1 and d.IsActive = 1 and cus.CustomerID = 157



select cus.Code,cus.CustomerID, cus.CustomerName, count(inv.InvoiceID) as InvoiceCount from Customers cus
inner join  Invoice inv on cus.CustomerKey = inv.PartyKey 
where inv.BusinessID = 89 and inv.IsActive = 1 and cus.BusinessID = 89 and cus.IsActive = 1 and inv.InvoiceType = 13
group by cus.CustomerName, cus.Code, cus.CustomerID
order by cus.CustomerName



select cus.CustomerID, cus.CustomerKey, cus.CustomerName, cus.Code,inv.InvoiceID,inv.InvoiceKey, inv.InvoiceBillNo, inv.IssueDate,inv.InvoiceSummary, inv.IsRounding, inv.RoundingMethod,inv.IsDiscount, inv.DiscountType, inv.IsAmountsIncludeTax, d.DescriptionID, d.Qty, d.Amount, d.TaxCode, d.Discount, d.DiscountType,d.Sequence,d.EntityID
from Customers cus 

inner join Invoice inv on cus.CustomerKey = inv.PartyKey
inner join Description d On inv.InvoiceID = d.EntityID
where cus.BusinessID = 89 and cus.IsActive = 1 and inv.IsActive = 1 and inv.BusinessID = 89 and inv.InvoiceType = 13 and d.EntityTypeID = 1 and d.IsActive = 1 and cus.CustomerID = 175



select d.Description, d.Qty, d.Amount, d.Account,d.Discount, d.DiscountType, r.ReceiptPaymentID, d.InvoiceKey,r.ReceiptPaymentDate, r.AccountKey as BankAccount, r.Name as ReceiptPaymentName, r.Payee,r.TransectionType, r.Reference, c.CustomerID
 from Description d
inner join ReceiptsPayments r on d.EntityID = r.ReceiptPaymentID
inner join Customers c on d.Account = c.CustomerKey
where d.IsActive = 1 and r.BusinessID = 89 and r.IsActive = 1


select t.componentkey,t.TaxCodeName, c.Name, c.Rate from TaxCodes t

inner join Component c on t.TaxCodeID = c.TaxCodeID
where t.BusinessID = 89 and t.IsActive = 1



select * from Invoice inv
inner join Description d on inv.InvoiceID = d.EntityID 
where BusinessID = 89 and inv.PartyKey = '05b1c2b7-1fdd-4a47-8911-fb291e945b94' and d.EntityTypeID = 1
select * from ReceiptsPayments r
inner join Description d on r.ReceiptPaymentID = d.EntityID
inner join Customers c on d.Account = c.CustomerKey
 where r.BusinessID = 89 and r.IsActive = 1 and d.IsActive = 1 and c.BusinessID = 89 and c.IsActive = 1 and c.CustomerKey = '05b1c2b7-1fdd-4a47-8911-fb291e945b94'
