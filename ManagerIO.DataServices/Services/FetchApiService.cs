using ManagerIO.DataModels;
using ManagerIO.DataModels.Models.CustomEntities.FetchApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataServices.Services
{
    public class FetchApiService
    {
        #region Define as Singleton
        private static FetchApiService _Instance;
        public static FetchApiService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FetchApiService();
                }

                return (_Instance);
            }
        }
        #endregion
        public List<ChartofAccount> getChartofAccountGroupModel(decimal BusinessID, string BusinessKey)
        {
            List<ChartofAccount> result = new List<ChartofAccount>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("ca.AccountID, ca.AccountKey, ca.Position")
                           .From("ChartofAccounts ca")
                           .Where("ca.IsActive = 1 and ca.BusinessID = @0 and ca.BusinessKey = @1",BusinessID, BusinessKey);
                    result = repo.Fetch<ChartofAccount>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return result;
        }
        public List<InnerObject> getInnerObjectModel(decimal BusinessID, string BusinessKey)
        {
            List<InnerObject> result = new List<InnerObject>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("inr.InnerObjectID, inr.InnerObjectKey, inr.Name")
                           .From("InnerObject inr")
                           .Where("inr.IsActive = 1 and inr.BusinessID = @0 and inr.BusinessKey = @1", BusinessID, BusinessKey);
                    result = repo.Fetch<InnerObject>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return result;
        }
        public List<ObjectModel> getObjectModel()
        {
            List<ObjectModel> result = new List<ObjectModel>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("ob.ObjectID, ob.ObjectKey, ob.ObjectName")
                           .From("Object ob")
                           .Where("ob.IsActive = 1")
                           .OrderBy("ob.OrderBy");
                    result = repo.Fetch<ObjectModel>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return result;
        }

        public List<Business> getInsertedBusiness(string baseApiUrl)
        {
            List<Business> result = new List<Business>();
            using (ManagerIORepository repo = RepositoryHelper.Instance.GetRepository())
            {
                try
                {
                    var sql = PetaPoco.Sql.Builder.Select("b.BusinessID, b.BusinessKey, b.BusinessName")
                           .From("Business b")
                           .Where("b.IsActive = 1 and b.BaseApiUrl = @0", baseApiUrl);
                    result = repo.Fetch<Business>(sql);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return result;
        }

        public void DeactivateExistingBusiness(string baseApiUrl)
        {
            List<Business> result = getInsertedBusiness(baseApiUrl);
            if (result.Count > 0)
               SyncApiService.Instance.UpdateBusinessIsActive(baseApiUrl, false);

        }
    }
}
