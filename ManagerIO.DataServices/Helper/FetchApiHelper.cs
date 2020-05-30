using ManagerIO.DataModels.Models.CustomEntities.SyncApiModels;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ManagerIO.DataServices.Helper
{
    public static class FetchApiHelper
    {
        public static List<BusinessApiModel> GetBusinessDataFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<BusinessApiModel> retVal = new List<BusinessApiModel>();
            ApiAuthModel apiAuth = new ApiAuthModel();
            
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            BusinessApiJsonModel businessApiJson = new BusinessApiJsonModel();
            if (!GetJSONResponseFromManagerIO(url, ref stringJson, ref businessApiJson, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }
            return businessApiJson.Data;
        }
      
        public static List<string> GetInnerObjectsKeysFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            List<string> innerObjKeys = new List<string>();
            if (!GetJSONResponseForInnerObjectKeys(url, ref stringJson, ref innerObjKeys, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }
            
            return innerObjKeys;
        }

        private static bool GetJSONResponseForInnerObjectKeys(string url, ref JsonModel stringJson, ref List<string> innerObjKeys, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    innerObjKeys = serializer.Deserialize<List<string>>(responseFromServer);
                    if (innerObjKeys == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }
        private static bool GetJSONResponseFromManagerIO(string url, ref JsonModel stringJson, ref BusinessApiJsonModel businessApiJson, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    businessApiJson.Data = serializer.Deserialize<List<BusinessApiModel>>(responseFromServer);
                    if (businessApiJson.Data == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        private static string getErrorMsg(string errMsg)
        {
            if (errMsg != null && errMsg.Length > 9)
            {
                errMsg = errMsg.Substring(9);
            }
            else
            {
                errMsg = "Error";
            }
            return errMsg;
        }

        public static AttachmentApiModel GetAttachmentFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            AttachmentApiModel attachment = new AttachmentApiModel();
            if (!GetJSONResponseForAttachment(url, ref stringJson, ref attachment, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }

            return attachment;
        }
        private static bool GetJSONResponseForAttachment(string url, ref JsonModel stringJson, ref AttachmentApiModel attachment, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    attachment = serializer.Deserialize<AttachmentApiModel>(responseFromServer);
                    if (attachment == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        public static AccountApiModel GetAccountFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            AccountApiModel account = new AccountApiModel();
            if (!GetJSONResponseForAccount(url, ref stringJson, ref account, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }

            return account;
        }
        private static bool GetJSONResponseForAccount(string url, ref JsonModel stringJson, ref AccountApiModel account, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    account = serializer.Deserialize<AccountApiModel>(responseFromServer);
                    if (account == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        public static BusinessLogoApiModel GetBusinessLogoFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            BusinessLogoApiModel logo = new BusinessLogoApiModel();
            if (!GetJSONResponseForBusinessLogo(url, ref stringJson, ref logo, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }

            return logo;
        }
        private static bool GetJSONResponseForBusinessLogo(string url, ref JsonModel stringJson, ref BusinessLogoApiModel logo, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    logo = serializer.Deserialize<BusinessLogoApiModel>(responseFromServer);
                    if (logo == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        public static BusinessDetailApiModel GetBusinessDetailFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            BusinessDetailApiModel detail = new BusinessDetailApiModel();
            if (!GetJSONResponseForBusinessDetail(url, ref stringJson, ref detail, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }

            return detail;
        }
        private static bool GetJSONResponseForBusinessDetail(string url, ref JsonModel stringJson, ref BusinessDetailApiModel detail, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    detail = serializer.Deserialize<BusinessDetailApiModel>(responseFromServer);
                    if (detail == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        public static PartyApiModel GetPartyFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            PartyApiModel party = new PartyApiModel();
            if (!GetJSONResponseForParty(url, ref stringJson, ref party, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }

            return party;
        }
        private static bool GetJSONResponseForParty(string url, ref JsonModel stringJson, ref PartyApiModel party, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    party = serializer.Deserialize<PartyApiModel>(responseFromServer);
                    if (party == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        public static FolderApiModel GetFolderFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            FolderApiModel folder = new FolderApiModel();
            if (!GetJSONResponseForFolder(url, ref stringJson, ref folder, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }

            return folder;
        }
        private static bool GetJSONResponseForFolder(string url, ref JsonModel stringJson, ref FolderApiModel folder, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    folder = serializer.Deserialize<FolderApiModel>(responseFromServer);
                    if (folder == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        public static InventoryApiModel GetInventoryFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            InventoryApiModel inventory = new InventoryApiModel();
            if (!GetJSONResponseForInventory(url, ref stringJson, ref inventory, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }

            return inventory;
        }
        private static bool GetJSONResponseForInventory(string url, ref JsonModel stringJson, ref InventoryApiModel inventory, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    inventory = serializer.Deserialize<InventoryApiModel>(responseFromServer);
                    if (inventory == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        public static ChartofAccApiModel GetChartAccountFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            ChartofAccApiModel chart = new ChartofAccApiModel();
            if (!GetJSONResponseForChartAccount(url, ref stringJson, ref chart, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }

            return chart;
        }
        private static bool GetJSONResponseForChartAccount(string url, ref JsonModel stringJson, ref ChartofAccApiModel chart, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    chart = serializer.Deserialize<ChartofAccApiModel>(responseFromServer);
                    if (chart == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        public static InvoiceApiModel GetInvoiceFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            InvoiceApiModel invoice = new InvoiceApiModel();
            if (!GetJSONResponseForInvoice(url, ref stringJson, ref invoice, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }

            return invoice;
        }
        private static bool GetJSONResponseForInvoice(string url, ref JsonModel stringJson, ref InvoiceApiModel invoice, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    invoice = serializer.Deserialize<InvoiceApiModel>(responseFromServer);
                    if (invoice == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        public static ReceiptAndPaymentApiModel GetReceiptsPaymentsFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            ReceiptAndPaymentApiModel receiptsPayments = new ReceiptAndPaymentApiModel();
            if (!GetJSONResponseForReceiptsPayments(url, ref stringJson, ref receiptsPayments, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }

            return receiptsPayments;
        }
        private static bool GetJSONResponseForReceiptsPayments(string url, ref JsonModel stringJson, ref ReceiptAndPaymentApiModel receiptsPayments, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    receiptsPayments = serializer.Deserialize<ReceiptAndPaymentApiModel>(responseFromServer);
                    if (receiptsPayments == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        public static TaxCodeApiModel GetTaxcodeFromApi(string apiurl, string username, string password)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ApiAuthModel apiAuth = new ApiAuthModel();
            string url = apiurl;
            apiAuth.Username = username;
            apiAuth.Password = password;
            JsonModel stringJson = new JsonModel();
            TaxCodeApiModel tax = new TaxCodeApiModel();
            if (!GetJSONResponseForTaxcode(url, ref stringJson, ref tax, apiAuth))
            {
                // errMsg = getErrorMsg(stringJson.Data);
            }

            return tax;
        }
        private static bool GetJSONResponseForTaxcode(string url, ref JsonModel stringJson, ref TaxCodeApiModel tax, ApiAuthModel apiAuth)
        {

            bool isSuccess = false;
            string responseFromServer = GetResponseFromServer(url, apiAuth);
            if (responseFromServer != string.Empty)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                if (responseFromServer.Contains("##error##"))
                {
                    stringJson = serializer.Deserialize<JsonModel>(responseFromServer);
                }
                else
                {
                    isSuccess = true;
                    tax = serializer.Deserialize<TaxCodeApiModel>(responseFromServer);
                    if (tax == null)
                    {
                        isSuccess = false;
                        stringJson.Data = "##error##Error";
                    }
                }

            }

            return isSuccess;
        }

        public static string GetResponseFromServer(string url, ApiAuthModel apiAuth)
        {
            string responseFromServer = "";
            HttpWebResponse response = null;
            StreamReader reader = null;
            Stream dataStream = null;

            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(apiAuth.Username + ":" + apiAuth.Password));


            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Basic " + svcCredentials);
                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                responseFromServer = responseFromServer.Trim();
            }
            catch
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (dataStream != null)
                {
                    dataStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }

            return responseFromServer;
        }

    }
}
