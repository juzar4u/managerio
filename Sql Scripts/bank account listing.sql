 select ReceiptsPayments.ReceiptPaymentDate, ReceiptsPayments.Name, ReceiptsPayments.Payee, ReceiptsPayments.TransectionType, Description.invoicekey, Description.Amount  from ReceiptsPayments

inner join Description on ReceiptsPayments.ReceiptPaymentID = Description.EntityID
inner join Accounts on ReceiptsPayments.AccountKey = Accounts.AccountKey
 where ReceiptsPayments.BusinessID = 82 and ReceiptsPayments.IsActive = 1 
 and Accounts.BusinessID = 82 and Accounts.IsActive = 1 and Accounts.AccountType = 2 and
 description.EntityTypeID = 2 -- payment
  and ReceiptsPayments.isbalanceclear = 1 -- when balace is clear
order by ReceiptsPayments.ReceiptPaymentDate desc




  select ReceiptsPayments.ReceiptPaymentDate, ReceiptsPayments.Name, ReceiptsPayments.Payee, ReceiptsPayments.TransectionType, Description.invoicekey, Description.Amount, Description.Qty, Description.Account  from ReceiptsPayments

inner join Description on ReceiptsPayments.ReceiptPaymentID = Description.EntityID
inner join Accounts on ReceiptsPayments.AccountKey = Accounts.AccountKey
 where ReceiptsPayments.BusinessID = 83 and ReceiptsPayments.IsActive = 1 
 and Accounts.BusinessID = 83 and Accounts.IsActive = 1 and Accounts.AccountType = 5 and ReceiptsPayments.TransectionType = 1 and
 description.EntityTypeID = 2 -- payment-- when balace is clear
order by ReceiptsPayments.ReceiptPaymentDate desc