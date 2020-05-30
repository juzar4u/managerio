using ManagerIO.DataModels.Models.CustomEntities.SyncApiModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataServices.Helper
{
    public class Constants
    {
        public static string Base_Url
        {
            get
            {
                return ConfigurationManager.AppSettings["Base_Url"];
            }
        }
        public static string Api_Url
        {
            get
            {
                return ConfigurationManager.AppSettings["Api_Url"];
            }
        }
        public static string Api_Username
        {
            get
            {
                return ConfigurationManager.AppSettings["Api_Username"];
            }
        }
        public static string Api_Password
        {
            get
            {
                return ConfigurationManager.AppSettings["Api_Password"];
            }
        }
        public static int InnerObjectPageSize
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["InnerObjectPageSize"]);
            }
        }

        public static List<string> GetPage(List<string> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<BusinessLogoApiModel> GetPage(List<BusinessLogoApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<BusinessDetailApiModel> GetPage(List<BusinessDetailApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<AccountApiModel> GetPage(List<AccountApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<PartyApiModel> GetPage(List<PartyApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<FolderApiModel> GetPage(List<FolderApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<InventoryApiModel> GetPage(List<InventoryApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<ChartofAccApiModel> GetPage(List<ChartofAccApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<TaxCodeApiModel> GetPage(List<TaxCodeApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<AttachmentApiModel> GetPage(List<AttachmentApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<ReceiptAndPaymentApiModel> GetPage(List<ReceiptAndPaymentApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<DescriptionApiModel> GetPage(List<DescriptionApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<InvoiceApiModel> GetPage(List<InvoiceApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public static List<ComponentApiModel> GetPage(List<ComponentApiModel> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
