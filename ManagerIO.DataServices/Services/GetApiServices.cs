using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.ApiModels;
using ManagerIO.DataModels.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataServices.Services
{
    public class GetApiServices
    {
        #region Define as Singleton
        private static GetApiServices _Instance;
        public static GetApiServices Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GetApiServices();
                }

                return (_Instance);
            }
        }
        #endregion

        public List<ReceiptandPaymentApiModel> getDescriptionListing(decimal businessID, int accountTypeId, int entityTypeId)
        {
            List<ReceiptandPaymentApiModel> result = new List<ReceiptandPaymentApiModel>();
            List<PartyModel> model = new List<PartyModel>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("distinct ReceiptsPayments.ReceiptPaymentDate, ReceiptsPayments.Name, ReceiptsPayments.Payee, Accounts.AccountName as BankAccount, Accounts.AccountKey as BankAccountKey, ReceiptsPayments.TransectionType, Description.invoicekey, Description.Amount, Description.Qty, Description.Account as ChartAccountKey, ac.AccountName as ChartAccountName, ReceiptsPayments.IsBalanceClear, Description.DescriptionID, Description.Sequence, ReceiptsPayments.ReceiptPaymentID")
                           .From("ReceiptsPayments")
                           .InnerJoin("Description").On("ReceiptsPayments.ReceiptPaymentID = Description.EntityID")
                           .InnerJoin("Accounts").On("ReceiptsPayments.AccountKey = Accounts.AccountKey")
                           .LeftJoin("ChartofAccounts ac").On("Description.Account = ac.AccountKey")
                           .Where("ReceiptsPayments.BusinessID = @0 and ReceiptsPayments.IsActive = 1 and Accounts.BusinessID = @0 and Accounts.IsActive = 1 and Accounts.AccountType = @1 and description.EntityTypeID = @2", businessID, accountTypeId, entityTypeId)
                           .OrderBy("ReceiptsPayments.ReceiptPaymentDate desc");
                    result = repo.Fetch<ReceiptandPaymentApiModel>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            if (result.Count > 0)
            {
                List<Customer> customers = new List<Customer>();
                List<Supplier> supplier = new List<Supplier>();

                List<InnerObject> InnerObject = new List<InnerObject>();
                List<int> objectIds = new List<int>();
                objectIds.Add((int)ObjectEnum.Customer);
                objectIds.Add((int)ObjectEnum.Supplier);
                objectIds.Add((int)ObjectEnum.ReceiptOrPayment);
                objectIds.Add((int)ObjectEnum.CashAccount);
                objectIds.Add((int)ObjectEnum.BankAccount);
                string innerObjectSql = string.Format("select * from InnerObject where BusinessID = {0} and IsActive = 1 and ParentObjectID in ({1})", businessID, string.Join(",",objectIds));
                string customerSql = string.Format("select * from Customers where IsActive = 1 and BusinessID = {0}", businessID);
                string supplierSql = string.Format("select * from Suppliers where IsActive = 1 and BusinessID = {0}", businessID);
                using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
                {
                    customers = repo.Fetch<Customer>(customerSql);
                    supplier = repo.Fetch<Supplier>(supplierSql);
                    InnerObject = repo.Fetch<InnerObject>(innerObjectSql);
                }
                List<ReceiptandPaymentApiModel> partyKey = result.Where(x => x.ChartAccountName == null).ToList();
                foreach (var item in partyKey)
                {
                    var sql = string.Empty;
                    string chartAccountName = string.Empty;
                    string accountName = string.Empty;
                    if (InnerObject.Where(x => x.InnerObjectKey == item.ChartAccountKey).Select(x => x.ParentObjectID).FirstOrDefault() == (int)ObjectEnum.Customer)
                    {
                        chartAccountName = customers.Where(x => x.CustomerKey == item.ChartAccountKey).Select(x => x.CustomerName).FirstOrDefault();
                        accountName = "Account Receivable";
                    }
                    else if (InnerObject.Where(x => x.InnerObjectKey == item.ChartAccountKey).Select(x => x.ParentObjectID).FirstOrDefault() == (int)ObjectEnum.Supplier)
                    {
                        chartAccountName = supplier.Where(x => x.SupplierKey == item.ChartAccountKey).Select(x => x.Name).FirstOrDefault();
                        accountName = "Account Payable";
                    }
                    else
                    {
                        item.ChartAccountName = "Suspense";
                    }
                    if (!string.IsNullOrEmpty(chartAccountName))
                    {
                        item.ChartAccountName = chartAccountName;
                        if (!string.IsNullOrEmpty(item.ChartAccountName))
                        {
                            item.ChartAccountName = string.Format("{0} — {1}", accountName, item.ChartAccountName);
                        }
                    }
                }
            }
            List<ReceiptandPaymentApiModel> processedList = new List<ReceiptandPaymentApiModel>();
            processedList = result.Where(x => x.Sequence == 1).ToList();
            List<GroupDescription> groupEntities = getGroupEntities(businessID, entityTypeId);
            if (groupEntities.Count > 0)
            {
                List<int> entityIds = new List<int>();
                foreach (var ReceiptPaymentID in result.Select(x => x.ReceiptPaymentID).Distinct().ToList())
                {
                    List<string> chartOfAccounts = new List<string>();
                    chartOfAccounts.AddRange(result.Where(x => x.ReceiptPaymentID == ReceiptPaymentID).Select(x => x.ChartAccountName).Distinct().ToList());
                    if (result.Where(x => x.ReceiptPaymentID == ReceiptPaymentID).Where(x => x.Qty > 0).ToList().Count > 0)
                    {
                        decimal totalAmount = 0;
                        foreach (var item in result.Where(x => x.ReceiptPaymentID == ReceiptPaymentID).ToList())
                        {
                            item.Amount = item.Qty > 0 ? item.Amount * item.Qty : item.Amount;
                            totalAmount += item.Amount;
                        }
                        processedList.Find(x => x.ReceiptPaymentID == ReceiptPaymentID).Amount = totalAmount;
                    }
                    else
                    {
                        processedList.Find(x => x.ReceiptPaymentID == ReceiptPaymentID).Amount = groupEntities.Where(x => x.EntityID == ReceiptPaymentID).Select(x => x.TotalAmount).FirstOrDefault();
                    }
                    if (chartOfAccounts.Count > 0)
                        processedList.Find(x => x.ReceiptPaymentID == ReceiptPaymentID).ChartAccountName = string.Join(", ", chartOfAccounts);
                }
                result = processedList;
            }
            return result;
        }


        public List<Account> getAccountForBusiness(decimal BusinessID, int accoutTypeID)
        {
            List<Account> result = new List<Account>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("*")
                           .From("Accounts")
                           .Where("BusinessID = @0 and IsActive = 1 and AccountType = @1", BusinessID, accoutTypeID);
                    result = repo.Fetch<Account>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return result;
        }

        public List<GroupDescription> getGroupEntities(decimal businessID, int entityTypeId)
        {
            List<GroupDescription> groupEntities = new List<GroupDescription>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("Description.EntityID, sum(Description.Amount) as TotalAmount")
                           .From("ReceiptsPayments")
                           .InnerJoin("Description").On("ReceiptsPayments.ReceiptPaymentID = Description.EntityID")
                           .Where("ReceiptsPayments.BusinessID = @0 and ReceiptsPayments.IsActive = 1 and description.EntityTypeID = @1", businessID, entityTypeId)
                           .GroupBy("Description.EntityID");
                    groupEntities = repo.Fetch<GroupDescription>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return groupEntities;
        }

        public List<ReceiptPaymentListApiModel> getReceiptsPaymentForBusiness(decimal BusinessID)
        {
            List<ReceiptPaymentListApiModel> result = new List<ReceiptPaymentListApiModel>();
            List<ReceiptandPaymentApiModel> receiptspayments = new List<ReceiptandPaymentApiModel>();
            List<Account> accounts = new List<Account>();
            List<ReceiptandPaymentApiModel> processedList = new List<ReceiptandPaymentApiModel>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("distinct r.ReceiptPaymentID, r.ReceiptPaymentDate, r.Name, r.Payee, r.Reference,r.TransectionType, r.AccountKey as BankAccountKey, d.Qty , d.Amount,d.DescriptionID,d.Sequence")
                           .From("ReceiptsPayments r")
                           .InnerJoin("Description d").On("r.ReceiptPaymentID = d.EntityID")
                           .Where("r.BusinessID = @0 and r.IsActive = 1 and d.EntityTypeID = 2 and d.IsActive = 1", BusinessID)
                           .OrderBy("r.ReceiptPaymentDate");
                    receiptspayments = repo.Fetch<ReceiptandPaymentApiModel>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                if (receiptspayments.Count > 0)
                {
                    var sql = PetaPoco.Sql.Builder.Select("*")
                           .From("Accounts")
                           .Where("BusinessID = @0 and IsActive = 1", BusinessID);
                    accounts = repo.Fetch<Account>(sql);
                }

                processedList = receiptspayments.Where(x => x.Sequence == 1).ToList();
                List<GroupDescription> groupEntities = getGroupEntities(BusinessID, (int)EntityTypeEnum.ReceiptsandPaymentsTypeEntity);
                if (groupEntities.Count > 0)
                {
                    List<int> entityIds = new List<int>();
                    foreach (var ReceiptPaymentID in receiptspayments.Select(x => x.ReceiptPaymentID).Distinct().ToList())
                    {
                        if (receiptspayments.Where(x => x.ReceiptPaymentID == ReceiptPaymentID).Where(x => x.Qty > 0).ToList().Count > 0)
                        {
                            decimal totalAmount = 0;
                            foreach (var item in receiptspayments.Where(x => x.ReceiptPaymentID == ReceiptPaymentID).ToList())
                            {
                                item.Amount = item.Qty > 0 ? item.Amount * item.Qty : item.Amount;
                                totalAmount += item.Amount;
                            }
                            processedList.Find(x => x.ReceiptPaymentID == ReceiptPaymentID).Amount = totalAmount;
                        }
                        else
                        {
                            processedList.Find(x => x.ReceiptPaymentID == ReceiptPaymentID).Amount = groupEntities.Where(x => x.EntityID == ReceiptPaymentID).Select(x => x.TotalAmount).FirstOrDefault();
                        }
                    }
                    result = getProcessedReceiptsPaymentList(processedList, accounts);
                }
            }
            return result;
        }


        private List<ReceiptPaymentListApiModel> getProcessedReceiptsPaymentList(List<ReceiptandPaymentApiModel> model, List<Account> accounts)
        {
            List<ReceiptPaymentListApiModel> rpModel = new List<ReceiptPaymentListApiModel>();
            foreach (var item in model)
            {
                string accountName = string.Empty;
                if (item.Amount.ToString().Contains('-'))
                {
                    item.TransectionType = item.TransectionType == (int)TransectionTypeEnum.Payment ? (int)TransectionTypeEnum.Receipt : (int)TransectionTypeEnum.Payment;
                }
                if(!string.IsNullOrEmpty(item.BankAccountKey))
                   accountName = !string.IsNullOrEmpty(accounts.Where(x=>x.AccountKey == item.BankAccountKey).Select(x=>x.AccountName).FirstOrDefault().ToString()) ? accounts.Where(x=>x.AccountKey == item.BankAccountKey).Select(x=>x.AccountName).FirstOrDefault() : "Suspense";
                else
                   accountName = "Suspense";
                rpModel.Add(new ReceiptPaymentListApiModel()
                {
                    ReceiptPaymentID = item.ReceiptPaymentID,
                    Date = item.ReceiptPaymentDate.ToString("dd/MM/yyyy"),
                    Reference = item.Reference,
                    Description = item.Name,
                    Payee = item.Payee,
                    TransectionTypeID = item.TransectionType,
                    Amount = item.Amount.ToString().Contains('-') ? System.Math.Abs(item.Amount) : item.Amount,
                    TransectionType = item.TransectionType == (int)TransectionTypeEnum.Payment ? "Payment" : "Receipt",
                    Account = accountName

                });
            }
            return rpModel;
        }

       
    }
}
