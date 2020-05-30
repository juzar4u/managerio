using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.ApiModels;
using ManagerIO.DataModels.Models.Enumerations;
using ManagerIO.DataServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace ManagerIO.Controllers
{
    public class AccountController : ApiController
    {
        public List<BankTabApiModel> GetAccount(int businessId, int accountTypeId)
        {
            List<BankTabApiModel> result = new List<BankTabApiModel>();
            List<ReceiptandPaymentApiModel> list = GetApiServices.Instance.getDescriptionListing(businessId, accountTypeId, (int)EntityTypeEnum.ReceiptsandPaymentsTypeEntity);
            List<Account> monetaryAccounts = GetApiServices.Instance.getAccountForBusiness(businessId, accountTypeId);
            List<ReceiptandPaymentApiModel> processedList = new List<ReceiptandPaymentApiModel>();
            List<AccountApiModel> orderedList = new List<AccountApiModel>();

            foreach (var acc in monetaryAccounts)
            {
                List<AccountApiModel> accountList = new List<AccountApiModel>();
                decimal clearBal = 0;
                decimal pendingDeposit = 0;
                decimal pendingWithdrawal = 0;
                decimal ActualBalance = 0;
                foreach (var item in list.Where(x => x.BankAccountKey == acc.AccountKey).OrderBy(x=>x.ReceiptPaymentDate).ToList())
                {
                    AccountApiModel accountModel = new AccountApiModel();
                    accountModel.ReceiptPaymentID = item.ReceiptPaymentID;
                    accountModel.ReceiptPaymentDate = item.ReceiptPaymentDate;
                    accountModel.Date = item.ReceiptPaymentDate.ToString("dd/MM/yyyy");
                    accountModel.Account = item.ChartAccountName;
                    accountModel.Description = string.Format("{0} — {1}", item.Name, item.Payee);
                    accountModel.Amount = item.TransectionType == (int)TransectionTypeEnum.Payment ? string.Format("-{0}", item.Amount) : item.Amount.ToString();
                    if (item.IsBalanceClear && item.TransectionType == (int)TransectionTypeEnum.Receipt)
                    {
                        clearBal += item.Amount;
                    }
                    else if (item.IsBalanceClear && item.TransectionType == (int)TransectionTypeEnum.Payment)
                    {
                        clearBal -= item.Amount;
                    }
                    else if (!item.IsBalanceClear && item.TransectionType == (int)TransectionTypeEnum.Receipt)
                    {
                        pendingDeposit += item.Amount;
                    }
                    else if (!item.IsBalanceClear && item.TransectionType == (int)TransectionTypeEnum.Payment)
                    {
                        pendingWithdrawal += item.Amount;
                    }

                    accountModel.Balance = clearBal;
                    accountList.Add(accountModel);
                    ActualBalance = clearBal + pendingDeposit - pendingWithdrawal;

                }
                accountList.Reverse();
                result.Add(new BankTabApiModel()
                {
                    Name = acc.AccountName,
                    ClearBalance = clearBal,
                    PendingDeposit = pendingDeposit,
                    PendingWithdrawal = pendingWithdrawal,
                    ActualBalance = ActualBalance,
                    List = accountList
                });
            }

            return result;
        }

        public ReceiptsPaymentMasterModel GetReceiptsPayments(decimal businessId)
        {
            ReceiptsPaymentMasterModel result = new ReceiptsPaymentMasterModel();
            List<ReceiptPaymentListApiModel> model = GetApiServices.Instance.getReceiptsPaymentForBusiness(businessId);
            if (model.Count > 0)
            {
                result.TotalPayments = model.Where(x => x.TransectionTypeID == (int)TransectionTypeEnum.Payment).Select(x => x.Amount).ToList().Sum();
                result.TotalReceipts = model.Where(x => x.TransectionTypeID == (int)TransectionTypeEnum.Receipt).Select(x => x.Amount).ToList().Sum();
                result.List = model.OrderBy(x=>x.ReceiptPaymentDate).ToList();
                result.List.Reverse();
            }
            return result;
        }

    }
}
