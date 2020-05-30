

 select count(io.InnerObjectName), io.InnerObjectName from InnerObject io
 inner join Object ob on io.ParentObjectID = ob.ObjectID
 where BusinessID = 83
 Group by io.InnerObjectName


select count(*) as bankaccount from Accounts where AccountType = 2 and BusinessID = 83 and IsActive = 1
select count(*) as cashaccount from Accounts where AccountType = 5 and BusinessID = 83 and IsActive = 1
select count(*) as receiptsandpayments from ReceiptsPayments where BusinessID = 83 and IsActive = 1
select count(*) as attachment from attachment where BusinessID = 83 and IsActive = 1
select count(*) as BusinessDetail from BusinessDetail where BusinessID = 83 and IsActive = 1
select count(*) as BusinessLogo from BusinessLogo where BusinessID = 83 and IsActive = 1
select count(*) as ChartofAccounts from ChartofAccounts where BusinessID = 83 and IsActive = 1 and ParentAccountID is not null
select count(*) as ChartofAccountGroup from ChartofAccounts where BusinessID = 83 and IsActive = 1 and ParentAccountID is null
select count(*) as customer from Customers where BusinessID = 83 and IsActive = 1 
select count(*) as folder from Folders where BusinessID = 83 and IsActive = 1 
select count(*) as InventoryItem from InventoryItem where BusinessID = 83 and IsActive = 1 
select count(*) as PurchaseInvoice from Invoice where BusinessID = 83 and IsActive = 1 and InvoiceType = 11
select count(*) as Salesinvoice from Invoice where BusinessID = 83 and IsActive = 1 and InvoiceType = 13
select count(*) as SalesOrder from Invoice where BusinessID = 83 and IsActive = 1 and InvoiceType = 14
select count(*) as SalesQuote from Invoice where BusinessID = 83 and IsActive = 1 and InvoiceType = 17
select count(*) as taxcode from taxcodes where BusinessID = 83 and IsActive = 1
select count(*) as Suppliers from Suppliers where BusinessID = 83 and IsActive = 1