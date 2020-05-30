using ManagerIO.DataModels.Models.CustomEntities.FetchApiModels;
using ManagerIO.DataModels.Models.CustomEntities.SyncApiModels;
using ManagerIO.DataServices.Helper;
using ManagerIO.DataServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
              SyncApiHelper.StartBusinessSyncFromApi();
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
