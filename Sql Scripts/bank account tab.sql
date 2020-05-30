

 select sum(Description.Amount) as TotalReceipts from Description

inner join ReceiptsPayments on Description.EntityID = ReceiptsPayments.ReceiptPaymentID
inner join Accounts on ReceiptsPayments.AccountKey = Accounts.AccountKey
 where ReceiptsPayments.BusinessID = 82 and ReceiptsPayments.IsActive = 1 
 and Accounts.BusinessID = 82 and Accounts.IsActive = 1 and Accounts.AccountType = 2 and
 description.EntityTypeID = 2 and ReceiptsPayments.TransectionType = 2
 --order by ReceiptsPayments.ReceiptPaymentDate desc

 select sum(Description.Amount) as TotalPayments from Description

inner join ReceiptsPayments on Description.EntityID = ReceiptsPayments.ReceiptPaymentID
inner join Accounts on ReceiptsPayments.AccountKey = Accounts.AccountKey
 where ReceiptsPayments.BusinessID = 82 and ReceiptsPayments.IsActive = 1 
 and Accounts.BusinessID = 82 and Accounts.IsActive = 1 and Accounts.AccountType = 2 and
 description.EntityTypeID = 2 and ReceiptsPayments.TransectionType = 1
 --order by ReceiptsPayments.ReceiptPaymentDate desc
 
 
   select sum(amount) as PendingWithdrawal  from Description

inner join ReceiptsPayments on Description.EntityID = ReceiptsPayments.ReceiptPaymentID
inner join Accounts on ReceiptsPayments.AccountKey = Accounts.AccountKey
 where ReceiptsPayments.BusinessID = 82 and ReceiptsPayments.IsActive = 1 
 and Accounts.BusinessID = 82 and Accounts.IsActive = 1 and Accounts.AccountType = 2 and
 description.EntityTypeID = 2 and ReceiptsPayments.TransectionType = 1 and ReceiptsPayments.isbalanceclear = 0 -- when balace is clear
 --order by ReceiptsPayments.ReceiptPaymentDate desc

  select sum(amount) as PendingDeposit  from Description

inner join ReceiptsPayments on Description.EntityID = ReceiptsPayments.ReceiptPaymentID
inner join Accounts on ReceiptsPayments.AccountKey = Accounts.AccountKey
 where ReceiptsPayments.BusinessID = 82 and ReceiptsPayments.IsActive = 1 
 and Accounts.BusinessID = 82 and Accounts.IsActive = 1 and Accounts.AccountType = 2 and
 description.EntityTypeID = 2 and ReceiptsPayments.TransectionType = 2 and ReceiptsPayments.isbalanceclear = 0 -- when balace is clear
 --order by ReceiptsPayments.ReceiptPaymentDate desc
