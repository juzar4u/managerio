using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.SyncApiModels;
using ManagerIO.DataModels.Models.Enumerations;
using ManagerIO.DataServices.Helper;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataServices.Services
{
    public class SyncApiService
    {
        #region Define as Singleton
        private static SyncApiService _Instance;
        public static SyncApiService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SyncApiService();
                }

                return (_Instance);
            }
        }
        #endregion

        public void UpdateBusinessIsActive(string baseUrl, bool isActive)
        {
            int value = isActive ? 1 : 0;
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = string.Format("update Business set IsActive = {0} where BaseApiUrl = '{1}'", value, baseUrl);
                repo.Execute(sql);
            }
        }
        public List<Business> InsertBusiness(List<BusinessApiModel> businessApiModel, string baseUrl, bool isActive)
        {
            List<Business> result = new List<Business>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in businessApiModel)
                {
                    sql.Append("insert into Business values(@0,@1,@2,@3)", i.Key, i.Name, isActive, baseUrl);
                }

                repo.Execute(sql);
            }
            return result = FetchApiService.Instance.getInsertedBusiness(baseUrl);
        }

        public void InsertInnerObjects(List<string> innerObjKeys, string parentObjName, int parentObjId, string parentObjKey, decimal businessID, string businessKey)
        {
            try
            {
                List<string> tempList = new List<string>();
                int pageSize = Constants.InnerObjectPageSize;
                if (innerObjKeys.Count > pageSize)
                {

                    int totalItems = innerObjKeys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(innerObjKeys, currentPage, pageSize);
                        currentPage++;
                        insertInnerObjectKeys(tempList, parentObjName, parentObjId, parentObjKey, businessID, businessKey);
                    }
                }
                else
                {
                    insertInnerObjectKeys(innerObjKeys, parentObjName, parentObjId, parentObjKey, businessID, businessKey);
                }
                
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertInnerObjectKeys(List<string> innerObjKeys, string parentObjName, int parentObjId, string parentObjKey, decimal businessID, string businessKey)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in innerObjKeys)
                {
                    sql.Append("insert into innerobject values(@0,@1,@2,@3,@4,@5,@6)", i, parentObjName, parentObjId, parentObjKey, businessID, businessKey, true);
                }

                repo.Execute(sql);
            }
        }

        #region Define as BusinessLogo
        public void InsertBusinessLogo(List<BusinessLogoApiModel> keys)
        {
            try
            {
                List<BusinessLogoApiModel> tempList = new List<BusinessLogoApiModel>();
                int pageSize = Constants.InnerObjectPageSize;
                if (keys.Count > pageSize)
                {

                    int totalItems = keys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(keys, currentPage, pageSize);
                        currentPage++;
                        insertBusinessLogoKeys(tempList);
                    }
                }
                else
                {
                    insertBusinessLogoKeys(keys);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertBusinessLogoKeys(List<BusinessLogoApiModel> keys)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in keys)
                {
                    sql.Append("insert into BusinessLogo values(@0,@1,@2,@3,@4,@5)", i.ApiKey, i.ContentType, i.Content, i.BusinessID, i.BusinessKey, i.IsActive);
                }

                repo.Execute(sql);
            }
        }
        #endregion

        #region Define as BusinessDetail
        public void InsertBusinessDetail(List<BusinessDetailApiModel> keys)
        {
            try
            {
                List<BusinessDetailApiModel> tempList = new List<BusinessDetailApiModel>();
                int pageSize = Constants.InnerObjectPageSize;
                if (keys.Count > pageSize)
                {

                    int totalItems = keys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(keys, currentPage, pageSize);
                        currentPage++;
                        insertBusinessDetailKeys(tempList);
                    }
                }
                else
                {
                    insertBusinessDetailKeys(keys);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertBusinessDetailKeys(List<BusinessDetailApiModel> keys)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in keys)
                {
                    sql.Append("insert into BusinessDetail values(@0,@1,@2,@3,@4,@5)", i.ApiKey, i.Name, i.Address, i.BusinessID, i.BusinessKey, i.IsActive);
                }

                repo.Execute(sql);
            }
        }
        #endregion

        #region Define as Account
        public void InsertAccount(List<AccountApiModel> keys)
        {
            try
            {
                List<AccountApiModel> tempList = new List<AccountApiModel>();
                int pageSize = Constants.InnerObjectPageSize;
                if (keys.Count > pageSize)
                {

                    int totalItems = keys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(keys, currentPage, pageSize);
                        currentPage++;
                        insertAccountKeys(tempList);
                    }
                }
                else
                {
                    insertAccountKeys(keys);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertAccountKeys(List<AccountApiModel> keys)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in keys)
                {
                    sql.Append("insert into Accounts values(@0,@1,@2,@3,@4,@5,@6,@7)", i.ApiKey, i.Name, i.AccountType, i.BusinessID, i.BusinessKey, i.IsActive, i.CreditLimit, i.Code);
                }

                repo.Execute(sql);
            }
        }
        #endregion

        #region Define as Party
        public void InsertParty(List<PartyApiModel> keys, int ObjectID)
        {
            try
            {
                List<PartyApiModel> tempList = new List<PartyApiModel>();
                int pageSize = Constants.InnerObjectPageSize;
                if (keys.Count > pageSize)
                {

                    int totalItems = keys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(keys, currentPage, pageSize);
                        currentPage++;
                        insertPartyKeys(tempList, ObjectID);
                    }
                }
                else
                {
                    insertPartyKeys(keys, ObjectID);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertPartyKeys(List<PartyApiModel> keys, int ObjectID)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in keys)
                {
                    if (ObjectID == (int)ObjectEnum.Customer)
                    {
                        sql.Append("insert into Customers values(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9)", i.ApiKey, i.Name, i.BusinessIdentifier,i.BillingAddress, i.BusinessID, i.BusinessKey, i.IsActive, i.Email,  i.Code, i.CreditLimit);
                    }
                    else if (ObjectID == (int)ObjectEnum.Supplier)
                    {
                        sql.Append("insert into Suppliers values(@0,@1,@2,@3,@4,@5,@6,@7,@8)", i.ApiKey, i.Name, i.Email, i.Address, i.BusinessID, i.BusinessKey, i.IsActive, i.Code, i.CreditLimit);
                    }
                }

                repo.Execute(sql);
            }
        }
        #endregion

        #region Define as Folder
        public void InsertFolder(List<FolderApiModel> keys)
        {
            try
            {
                List<FolderApiModel> tempList = new List<FolderApiModel>();
                int pageSize = Constants.InnerObjectPageSize;
                if (keys.Count > pageSize)
                {

                    int totalItems = keys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(keys, currentPage, pageSize);
                        currentPage++;
                        insertFolderKeys(tempList);
                    }
                }
                else
                {
                    insertFolderKeys(keys);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertFolderKeys(List<FolderApiModel> keys)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in keys)
                {
                    sql.Append("insert into Folders values(@0,@1,@2,@3,@4)", i.ApiKey, i.Description, i.BusinessID, i.BusinessKey, i.IsActive);
                }

                repo.Execute(sql);
            }
        }
        #endregion

        #region Define as Inventory
        public void InsertInventory(List<InventoryApiModel> keys)
        {
            try
            {
                List<InventoryApiModel> tempList = new List<InventoryApiModel>();
                int pageSize = Constants.InnerObjectPageSize;
                if (keys.Count > pageSize)
                {

                    int totalItems = keys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(keys, currentPage, pageSize);
                        currentPage++;
                        insertInventoryKeys(tempList);
                    }
                }
                else
                {
                    insertInventoryKeys(keys);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertInventoryKeys(List<InventoryApiModel> keys)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in keys)
                {
                    sql.Append("insert into InventoryItem values(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13)", i.ApiKey,i.Name, i.Description, i.BusinessID, i.BusinessKey, i.IsActive, i.Code, i.UnitName, i.PurchasePrice, i.SalePrice, i.TaxCode, i.IncomeAccount, i.Inactive, i.ExpenseAccount);
                }

                repo.Execute(sql);
            }
        }
        #endregion

        #region Define as ChartOfAccounts
        public void InsertChartOfAccounts(List<ChartofAccApiModel> keys)
        {
            try
            {
                List<ChartofAccApiModel> tempList = new List<ChartofAccApiModel>();
                int pageSize = Constants.InnerObjectPageSize;
                if (keys.Count > pageSize)
                {

                    int totalItems = keys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(keys, currentPage, pageSize);
                        currentPage++;
                        insertChartOfAccountsKeys(tempList);
                    }
                }
                else
                {
                    insertChartOfAccountsKeys(keys);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertChartOfAccountsKeys(List<ChartofAccApiModel> keys)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in keys)
                {
                    sql.Append("insert into ChartofAccounts values(@0,@1,@2,@3,@4,@5,@6,@7)", i.ApiKey, i.Name, i.ParentAccountID, i.Group, i.BusinessID, i.BusinessKey, i.IsActive, i.Position);
                }

                repo.Execute(sql);
            }
        }
        #endregion

        #region Define as TaxCodes
        public int insertTaxCodeKey(TaxCode key)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                return (int)repo.Insert(key);
            }
        }
        public void InsertComponents(List<ComponentApiModel> keys)
        {
            try
            {
                List<ComponentApiModel> tempList = new List<ComponentApiModel>();
                int pageSize = Constants.InnerObjectPageSize;
                if (keys.Count > pageSize)
                {

                    int totalItems = keys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(keys, currentPage, pageSize);
                        currentPage++;
                        insertComponentKeys(tempList);
                    }
                }
                else
                {
                    insertComponentKeys(keys);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertComponentKeys(List<ComponentApiModel> keys)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in keys)
                {
                    sql.Append("insert into Component values(@0,@1,@2,@3,@4,@5,@6)", i.Name, i.Rate, i.Account, i.BusinessID, i.BusinessKey, i.IsActive, i.TaxCodeID);
                }

                repo.Execute(sql);
            }
        }
        #endregion

        #region Define as Attachment
        public void InsertAttachment(List<AttachmentApiModel> keys)
        {
            try
            {
                List<AttachmentApiModel> tempList = new List<AttachmentApiModel>();
                int pageSize = Constants.InnerObjectPageSize;
                if (keys.Count > pageSize)
                {

                    int totalItems = keys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(keys, currentPage, pageSize);
                        currentPage++;
                        insertAttachmentKeys(tempList);
                    }
                }
                else
                {
                    insertAttachmentKeys(keys);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertAttachmentKeys(List<AttachmentApiModel> keys)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in keys)
                {
                    sql.Append("insert into Attachment values(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9)", i.ApiKey,i.Date, i.Name, i.ContentType, i.Size, i.Object, i.BusinessID, i.BusinessKey, i.IsActive, i.AttachmentUrl);
                }

                repo.Execute(sql);
            }
        }
        #endregion

        #region Define as ReceiptPayment
        public void InsertReceiptPayment(List<ReceiptAndPaymentApiModel> keys)
        {
            try
            {
                List<ReceiptAndPaymentApiModel> tempList = new List<ReceiptAndPaymentApiModel>();
                int pageSize = Constants.InnerObjectPageSize;
                if (keys.Count > pageSize)
                {

                    int totalItems = keys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(keys, currentPage, pageSize);
                        currentPage++;
                        insertReceiptPaymentKeys(tempList);
                    }
                }
                else
                {
                    insertReceiptPaymentKeys(keys);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertReceiptPaymentKeys(List<ReceiptAndPaymentApiModel> keys)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in keys)
                {
                    sql.Append("insert into ReceiptsPayments values(@0,@1,@2,@3,@4,@5,@6,@7,@8)", i.ApiKey, i.Date, i.BankAccount, i.Description, i.Contact, i.TransectionType, i.BusinessID, i.BusinessKey, i.IsActive);
                }

                repo.Execute(sql);
            }
        }
        #endregion

        #region Define as LineDescriptions
        public void InsertLineDescriptions(List<DescriptionApiModel> keys)
        {
            try
            {
                List<DescriptionApiModel> tempList = new List<DescriptionApiModel>();
                int pageSize = Constants.InnerObjectPageSize;
                if (keys.Count > pageSize)
                {

                    int totalItems = keys.Count;
                    decimal totalPages = totalItems / pageSize;
                    totalPages = Math.Round(totalPages) + 1;
                    int currentPage = 1;
                    for (int i = 0; totalPages >= currentPage; i++)
                    {
                        tempList = Constants.GetPage(keys, currentPage, pageSize);
                        currentPage++;
                        insertLineDescriptionsKeys(tempList);
                    }
                }
                else
                {
                    insertLineDescriptionsKeys(keys);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void insertLineDescriptionsKeys(List<DescriptionApiModel> keys)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                var sql = PetaPoco.Sql.Builder;
                foreach (var i in keys)
                {
                    sql.Append("insert into Description values(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12)", i.Description, i.Qty, i.Item, i.Amount, i.Account, i.TaxCode, i.IsActive, i.Discount, i.DiscountType, i.EntityID, i.EntityTypeID, i.Invoice, i.Sequence);
                }

                repo.Execute(sql);
            }
        }
        #endregion

        #region Define as Invoice
        public int insertInvoiceKey(Invoice key)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                return (int)repo.Insert(key);
            }
        }
        #endregion
        #region Define as ReceiptsPayments
        public int insertReceiptPaymentKey(ReceiptsPayment key)
        {
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                return (int)repo.Insert(key);
            }
        }
        #endregion
    }
}
