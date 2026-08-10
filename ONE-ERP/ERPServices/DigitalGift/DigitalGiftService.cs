using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ONEERP.Areas.TaskManagement.Models;
using ONEERP.Areas.DigitalGift.Models;
using ONEERP.Data;
using ONEERP.ERPServices.DigitalGift.Interfaces;
using ONEERP.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;

namespace ONEERP.ERPServices.DigitalGift
{
    public class DigitalGiftService : IDigitalGiftService
    {
        private readonly ERPDbContext _context;
        public DigitalGiftService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<JsonViewModel> ValidateRequestedInfo(int? userId, DigitalGiftModels model)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"GetDigitalGiftCouponInfo {userId}, {model.CouponCode}, {model.Name}, {model.MobileNumber}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                JsonViewModel jvm = new JsonViewModel();
                jvm.data = "[]";
                return jvm;
            }

        }
        public async Task<int> DigitalGiftDisburseLog(DigitalGiftModels model)
        {
            try
            {
                // Set Log
                var log = string.Concat("TerritoryCode: ", model.TerritoryCode, " | CouponCode: ", model.CouponCode);
               var res= await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburseLog {model.MobileNumber}, {log}").AsNoTracking().FirstOrDefaultAsync();
                return res.isSuccess;
            }
            catch (Exception ex)
            {                
                return 0;
            }

        }
        public async Task<JsonViewModel> DigitalGiftDisburse(int? userId, DigitalGiftModels model)
        {
            try
            {
                JsonViewModel jsonRes = new JsonViewModel();

                var oAuthApiBody = new Dictionary<string, string>();
                oAuthApiBody.Add("client_id", "IWACVfu7IDjaN8rfcQyxrQybUSyVurcm");
                oAuthApiBody.Add("client_secret", "Ygwd6PmOEIlLZb2E");
                oAuthApiBody.Add("grant_type", "client_credentials");

                string authUrl = string.Format("https://apigw.grameenphone.com/oauth/v1/token");

                OAuthResponse authData = new OAuthResponse();
                using (var httpClient = new HttpClient())
                {
                    using (var content = new FormUrlEncodedContent(oAuthApiBody))
                    {
                        HttpResponseMessage response = await httpClient.PostAsync(authUrl, content);
                        int StatusCode = (int)response.StatusCode;
                        response.EnsureSuccessStatusCode();

                        if (StatusCode == 200)
                        {
                            authData = JsonConvert.DeserializeObject<OAuthResponse>(await response.Content.ReadAsStringAsync());
                        }
                        else
                        {
                            jsonRes.data = $"False: GP Bulk Data API Authentication Failed !";
                            return jsonRes;
                        }
                    }
                }


                string authStatus = "";// authData.status;
                string accessToken = "";//"Bearer " + authData.accessToken;

                if (authData != null && !string.IsNullOrWhiteSpace(authData.accessToken))
                {
                    authStatus = authData.status;
                    accessToken = "Bearer " + authData.accessToken;
                }


                if (string.IsNullOrWhiteSpace(authStatus) || authStatus != "approved")
                {
                    jsonRes.data = $"False: Invalid Response from GP Bulk Data Authentication API!";
                    return jsonRes;
                }
                else
                {
                    string GetPackListUrl = string.Format("https://apigw.grameenphone.com/bulkdata/v4/productOfferingQualificationManagement/productOfferingQualification/56543");

                    var PackListBodyData = new Dictionary<string, string>();
                    //oAuthApiBody.Add("Authorization", accessToken);

                    PackListModel packListModel = new PackListModel();

                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("Authorization", $"{accessToken}");
                        //using (var content = new FormUrlEncodedContent(PackListBodyData))
                        //{
                        HttpResponseMessage response = await httpClient.GetAsync(GetPackListUrl);
                        int StatusCode = (int)response.StatusCode;
                        response.EnsureSuccessStatusCode();

                        if (StatusCode == 200)
                        {
                            packListModel = JsonConvert.DeserializeObject<PackListModel>(await response.Content.ReadAsStringAsync());
                        }
                        else
                        {
                            jsonRes.data = $"False: Failed! Digital Gift Pack not found.";
                            return jsonRes;
                        }
                        //}
                    }

                    if (packListModel != null && packListModel.success == true && packListModel.data.pack_list.Count > 0)
                    {
                        //Pack_list packList = packListModel.data.pack_list[0]; //10GB
                        //Pack_list packList = packListModel.data.pack_list[2]; //5GB

                        Pack_list packList = packListModel.data.pack_list.Where(item => item.pack_id == 33).SingleOrDefault(); //33=5GB//34=10GB


                        string giftName = packList.pack_name;
                        int pack_id = packList.pack_id;
                        int volume_mb = packList.volume_mb;
                        int channel_id = packList.channel_id;
                        int current_balance = packList.current_balance;
                        string status = packList.status;

                        if (current_balance > 0)
                        {
                            GiftPackDisburseViewModel disburseModel = new GiftPackDisburseViewModel();
                            disburseModel.id = model.MobileNumber;// MSISDN
                            disburseModel.externalId = model.CouponCode;// DateTime.Now.ToString("yyMMddHHmmss"); // Transaction id "118" 
                            disburseModel.description = "Digital Gift From One Pharma";


                            OrderItem lstOrderItem = new OrderItem();
                            List<OrderItem> lstOrderItem2 = new List<OrderItem>();

                            Product product = new Product();

                            Characteristic characteristic = new Characteristic();
                            characteristic.value = pack_id;
                            characteristic.name = giftName;

                            lstOrderItem.product = product;
                            product.characteristic = characteristic;

                            lstOrderItem2.Add(lstOrderItem);
                            disburseModel.orderItem = lstOrderItem2;


                            string jsonBody = JsonConvert.SerializeObject(disburseModel);


                            /**/
                            ProductOrderResponseModel productOrderResponseModel = new ProductOrderResponseModel();

                            string url = string.Format("https://apigw.grameenphone.com/bulkdata/v4/productOrderingManagement/productOrder/{0}", model.MobileNumber);

                            //HttpClient client = new HttpClient();
                            //client.DefaultRequestHeaders.Add("Authorization", $"{accessToken}");
                            //HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                            //HttpResponseMessage response = await client.PostAsync(url, httpContent);
                            //response.EnsureSuccessStatusCode();
                            //productOrderResponseModel = JsonConvert.DeserializeObject<ProductOrderResponseModel>(await response.Content.ReadAsStringAsync());
                            //response.Dispose();
                            //client.Dispose();


                            string data = "";
                            using (var postClient = new HttpClient())
                            {
                                postClient.DefaultRequestHeaders.Add("Authorization", $"{accessToken}");
                                HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                                HttpResponseMessage response = await postClient.PostAsync(url, httpContent);
                                //response.EnsureSuccessStatusCode();
                                productOrderResponseModel = JsonConvert.DeserializeObject<ProductOrderResponseModel>(await response.Content.ReadAsStringAsync());
                                data = await response.Content.ReadAsStringAsync();
                            }

                            // Set Log
                            await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburseLog {model.MobileNumber}, {data}").AsNoTracking().FirstOrDefaultAsync();

                            string GiftPackState = "", TrxID = productOrderResponseModel.id;


                            await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburse {userId}, {model.CouponCode}, {model.Name}, {model.MobileNumber}, {giftName}, {TrxID}, {GiftPackState}, {model.Question}, {model.Answer},{model.TerritoryCode}").AsNoTracking().FirstOrDefaultAsync();

                            jsonRes.data = $"Congratulations! you have received '{giftName}'.";
                            return jsonRes;


                            // bellow code are no need;
                            #region bellow code are no need;

                            /*
                            if (productOrderResponseModel != null && productOrderResponseModel.status != "rejected")
                            {
                                // Set Log
                                await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburseLog {model.MobileNumber}, 'before disburse status check'").AsNoTracking().FirstOrDefaultAsync();

                                string strCheckStatusResponseLog = "";
                                int PackStatusCode = 0;

                                string GP_TrxID = productOrderResponseModel.id;
                                //string GP_TrxID = "1181636031572834582";

                                dynamic checkStatusResponseData;
                                string checkStatusUrl = string.Format("https://apigw.grameenphone.com/bulkdata/v4/productOfferingQualificationManagement/status/{0}", TrxID);

                                using (var httpClient2 = new HttpClient())
                                {
                                    httpClient2.DefaultRequestHeaders.Add("Authorization", $"{accessToken}");
                                    HttpResponseMessage checkStatusResponse2 = await httpClient2.GetAsync(checkStatusUrl);
                                   // await Task.Delay(1000);
                                    PackStatusCode = (int)checkStatusResponse2.StatusCode;
                                    //checkStatusResponse2.EnsureSuccessStatusCode();

                                    //strCheckStatusResponseLog = await checkStatusResponse.Content.ReadAsStringAsync();
                                    checkStatusResponseData = JsonConvert.DeserializeObject(await checkStatusResponse2.Content.ReadAsStringAsync());

                                    if (PackStatusCode != 200)
                                    {
                                        jsonRes.data = $"False: Digital Gift Pack status check failed!";
                                        return jsonRes;
                                    }

                                }

                                await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburseLog {model.MobileNumber}, 'disburse status check done'").AsNoTracking().FirstOrDefaultAsync();


                                // Set Log
                                //await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburseLog {model.MobileNumber}, {checkStatusResponseData.ToString()}").AsNoTracking().FirstOrDefaultAsync();

                                string GpGiftPackState = checkStatusResponseData.productOfferingQualificationItem[0].state;
                                if (GpGiftPackState != "failed") //if (GpGiftPackState == "success")
                                {
                                    await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburse {userId}, {model.CouponCode}, {model.Name}, {model.MobileNumber}, {giftName}, {TrxID}, {GiftPackState}, {model.Question}, {model.Answer}").AsNoTracking().FirstOrDefaultAsync();
                                    jsonRes.data = $"Congratulations! you have received '{giftName}'.";
                                    return jsonRes;
                                }
                                else
                                {
                                    var updateCouponRes = await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburse {userId}, {model.CouponCode}, {model.Name}, {""}, {model.MobileNumber + "; " + giftName}, {GP_TrxID}, {GpGiftPackState}").AsNoTracking().FirstOrDefaultAsync();

                                    jsonRes.data = $"False: Your Digital Gift Pack disbursement rejected from GP server!";
                                    return jsonRes;
                                }

                            }
                            else
                            {
                                jsonRes.data = $"False: Digital Gift Disburse Rejected!";
                                return jsonRes;
                            }
                            */

                            #endregion
                        }
                        else
                        {
                            jsonRes.data = $"False: Digital Gift Pack current balance is zero (0).";
                            return jsonRes;
                        }
                    }
                    else
                    {
                        jsonRes.data = $"False: Failed! Digital Gift Pack list not found.";
                        return jsonRes;
                    }
                }
            }
            catch (Exception ex)
            {
                // Set Log
                await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburseLog {model.MobileNumber}, {ex.Message}").AsNoTracking().FirstOrDefaultAsync();

                JsonViewModel jvm = new JsonViewModel();
                jvm.data = $"False: Error occurred in Digital Gift Processing";
                return jvm;
            }

        }

        public async Task<JsonViewModel> DigitalGiftDisburseV2(int? userId, DigitalGiftModels model)
        {
            try
            {
                JsonViewModel jsonRes = new JsonViewModel();

                var oAuthApiBody = new Dictionary<string, string>();
                oAuthApiBody.Add("client_id", "IWACVfu7IDjaN8rfcQyxrQybUSyVurcm");
                oAuthApiBody.Add("client_secret", "Ygwd6PmOEIlLZb2E");
                oAuthApiBody.Add("grant_type", "client_credentials");

                string authUrl = string.Format("https://apigw.grameenphone.com/oauth/v1/token");

                OAuthResponse authData = new OAuthResponse();
                using (var httpClient = new HttpClient())
                {
                    using (var content = new FormUrlEncodedContent(oAuthApiBody))
                    {
                        HttpResponseMessage response = await httpClient.PostAsync(authUrl, content);
                        int StatusCode = (int)response.StatusCode;
                        response.EnsureSuccessStatusCode();

                        if (StatusCode == 200)
                        {
                            authData = JsonConvert.DeserializeObject<OAuthResponse>(await response.Content.ReadAsStringAsync());
                        }
                        else
                        {
                            jsonRes.data = $"False: GP Bulk Data API Authentication Failed !";
                            return jsonRes;
                        }
                    }
                }


                string authStatus = "";// authData.status;
                string accessToken = "";//"Bearer " + authData.accessToken;

                if (authData != null && !string.IsNullOrWhiteSpace(authData.accessToken))
                {
                    authStatus = authData.status;
                    accessToken = "Bearer " + authData.accessToken;
                }


                if (string.IsNullOrWhiteSpace(authStatus) || authStatus != "approved")
                {
                    jsonRes.data = $"False: Invalid Response from GP Bulk Data Authentication API!";
                    return jsonRes;
                }
                else
                {
                    string GetPackListUrl = string.Format("https://apigw.grameenphone.com/bulkdata/v4/productOfferingQualificationManagement/productOfferingQualification/56543");

                    var PackListBodyData = new Dictionary<string, string>();
                    oAuthApiBody.Add("Authorization", accessToken);

                    PackListModel packListModel = new PackListModel();

                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("Authorization", $"{accessToken}");
                        //using (var content = new FormUrlEncodedContent(PackListBodyData))
                        //{
                        HttpResponseMessage response = await httpClient.GetAsync(GetPackListUrl);
                        int StatusCode = (int)response.StatusCode;
                        response.EnsureSuccessStatusCode();

                        if (StatusCode == 200)
                        {
                            packListModel = JsonConvert.DeserializeObject<PackListModel>(await response.Content.ReadAsStringAsync());
                        }
                        else
                        {
                            jsonRes.data = $"False: Failed! Digital Gift Pack not found.";
                            return jsonRes;
                        }
                        //}
                    }

                    if (packListModel != null && packListModel.success == true && packListModel.data.pack_list.Count > 0)
                    {
                        Pack_list packList = packListModel.data.pack_list[0];

                        string giftName = packList.pack_name;
                        int pack_id = packList.pack_id;
                        int volume_mb = packList.volume_mb;
                        int channel_id = packList.channel_id;
                        int current_balance = packList.current_balance;
                        string status = packList.status;


                        if (current_balance > 0)
                        {
                            GiftPackDisburseViewModel disburseModel = new GiftPackDisburseViewModel();
                            disburseModel.id = model.MobileNumber;// MSISDN
                            disburseModel.externalId = model.CouponCode;// DateTime.Now.ToString("yyMMddHHmmss"); // Transaction id "118" 
                            disburseModel.description = "Digital Gift From One Pharma";


                            OrderItem lstOrderItem = new OrderItem();
                            List<OrderItem> lstOrderItem2 = new List<OrderItem>();

                            Product product = new Product();

                            Characteristic characteristic = new Characteristic();
                            characteristic.value = pack_id;
                            characteristic.name = giftName;

                            lstOrderItem.product = product;
                            product.characteristic = characteristic;

                            lstOrderItem2.Add(lstOrderItem);
                            disburseModel.orderItem = lstOrderItem2;


                            string jsonBody = JsonConvert.SerializeObject(disburseModel);
                            /**/
                            ProductOrderResponseModel productOrderResponseModel = new ProductOrderResponseModel();

                            string url = string.Format("https://apigw.grameenphone.com/bulkdata/v4/productOrderingManagement/productOrder/{0}", model.MobileNumber);

                            HttpClient client = new HttpClient();
                            client.DefaultRequestHeaders.Add("Authorization", $"{accessToken}");
                            HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = await client.PostAsync(url, httpContent);
                            response.EnsureSuccessStatusCode();

                            productOrderResponseModel = JsonConvert.DeserializeObject<ProductOrderResponseModel>(await response.Content.ReadAsStringAsync());


                            // Set Log
                            string data = await response.Content.ReadAsStringAsync();
                            await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburseLog {model.MobileNumber}, {data}").AsNoTracking().FirstOrDefaultAsync();




                            // Added on 2024-12-14 03:14 PM for test
                            string GiftPackState = "", TrxID = productOrderResponseModel.id;
                            var res = await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburse {userId}, {model.CouponCode}, {model.Name}, {model.MobileNumber}, {giftName}, {TrxID}, {GiftPackState}, {TrxID}, {GiftPackState}").AsNoTracking().FirstOrDefaultAsync();

                            //jsonRes.data = $"Congratulations! you have received '{giftName}'.";
                            //return jsonRes;
                            // Added on 2024-12-14 03:14 PM for test



                            //if ((int)response.StatusCode == 200 && productOrderResponseModel != null && productOrderResponseModel.status != "rejected")
                            if (productOrderResponseModel != null && productOrderResponseModel.status != "rejected")
                            {
                                string GP_TrxID = productOrderResponseModel.id;
                                //string GP_TrxID = "1181636031572834582";

                                dynamic checkStatusResponseData;
                                string checkStatusUrl = string.Format("https://apigw.grameenphone.com/bulkdata/v4/productOfferingQualificationManagement/status/{0}", GP_TrxID);

                                //var checkStatusApiBodyData = new Dictionary<string, string>();
                                //checkStatusApiBodyData.Add("Authorization", accessToken);

                                // Set Log
                                //await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburseLog {model.MobileNumber}, 'before disburse status check'").AsNoTracking().FirstOrDefaultAsync();

                                string strCheckStatusResponseLog = "";
                                using (var httpClient = new HttpClient())
                                {
                                    httpClient.DefaultRequestHeaders.Add("Authorization", $"{accessToken}");
                                    HttpResponseMessage checkStatusResponse = await httpClient.GetAsync(checkStatusUrl);

                                    //int StatusCode = (int)checkStatusResponse.StatusCode;
                                    //checkStatusResponse.EnsureSuccessStatusCode();

                                    strCheckStatusResponseLog = await checkStatusResponse.Content.ReadAsStringAsync();

                                    checkStatusResponseData = JsonConvert.DeserializeObject(await checkStatusResponse.Content.ReadAsStringAsync());
                                    /*
                                    if (StatusCode == 200)
                                    {
                                        checkStatusResponseData = JsonConvert.DeserializeObject(await checkStatusResponse.Content.ReadAsStringAsync());
                                    }
                                    else
                                    {
                                        jsonRes.data = $"False: Failed! Digital Gift Pack not found.";
                                        return jsonRes;
                                    }
                                    */
                                }


                                // Set Log
                                await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburseLog {model.MobileNumber}, {strCheckStatusResponseLog}").AsNoTracking().FirstOrDefaultAsync();

                                string GpGiftPackState = checkStatusResponseData.productOfferingQualificationItem.state;
                                if (GpGiftPackState != "failed") //if (GpGiftPackState == "success")
                                {
                                    var updateCouponRes = await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburse {userId}, {model.CouponCode}, {model.Name}, {model.MobileNumber}, {giftName}, {GP_TrxID}, {GpGiftPackState}").AsNoTracking().FirstOrDefaultAsync();

                                    jsonRes.data = $"Congratulations! you have received '{giftName}'.";
                                    return jsonRes;
                                }
                                else
                                {
                                    var updateCouponRes = await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburse {userId}, {model.CouponCode}, {model.Name}, {""}, {model.MobileNumber + "; " + giftName}, {GP_TrxID}, {GpGiftPackState}").AsNoTracking().FirstOrDefaultAsync();

                                    jsonRes.data = $"False: Your Digital Gift Pack disbursement rejected from GP server!";
                                    return jsonRes;
                                }
                            }
                            else
                            {
                                jsonRes.data = $"False: Digital Gift Disburse Rejected!";
                                return jsonRes;
                            }
                        }
                        else
                        {
                            jsonRes.data = $"False: Digital Gift Pack current balance is zero (0).";
                            return jsonRes;
                        }
                    }
                    else
                    {
                        jsonRes.data = $"False: Failed! Digital Gift Pack list not found.";
                        return jsonRes;
                    }
                }
            }
            catch (Exception ex)
            {
                // Set Log
                await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburseLog {model.MobileNumber}, {ex.Message}").AsNoTracking().FirstOrDefaultAsync();

                JsonViewModel jvm = new JsonViewModel();
                jvm.data = $"False: Error occurred in Digital Gift Processing";
                return jvm;
            }

        }

        public async Task<OAuthResponse> GetBulkOAuthResponse()
        {
            OAuthResponse authData = new OAuthResponse();
            try
            {
                var oAuthApiBody = new Dictionary<string, string>();
                oAuthApiBody.Add("client_id", "IWACVfu7IDjaN8rfcQyxrQybUSyVurcm");
                oAuthApiBody.Add("client_secret", "Ygwd6PmOEIlLZb2E");
                oAuthApiBody.Add("grant_type", "client_credentials");

                string authUrl = string.Format("https://apigw.grameenphone.com/oauth/v1/token");

                using (var httpClient = new HttpClient())
                {
                    using (var content = new FormUrlEncodedContent(oAuthApiBody))
                    {
                        HttpResponseMessage response = await httpClient.PostAsync(authUrl, content);
                        int StatusCode = (int)response.StatusCode;
                        response.EnsureSuccessStatusCode();
                        authData = JsonConvert.DeserializeObject<OAuthResponse>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return authData;
        }

        public async Task<PackListModel> GetPackList(OAuthResponse model)
        {
            PackListModel packListModel = new PackListModel();
            try
            {
                string GetPackListUrl = string.Format("https://apigw.grameenphone.com/bulkdata/v4/productOfferingQualificationManagement/productOfferingQualification/56543");

                var PackListBodyData = new Dictionary<string, string>();
                //oAuthApiBody.Add("Authorization", model.accessToken);


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"{model.accessToken}");
                    HttpResponseMessage response = await httpClient.GetAsync(GetPackListUrl);
                    //int StatusCode = (int)response.StatusCode;
                    //response.EnsureSuccessStatusCode();
                    packListModel = JsonConvert.DeserializeObject<PackListModel>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return packListModel;
        }
        public async Task<ProductOrderResponseModel> DigitalGiftPackDisburse(int? userId, string accessToken, DigitalGiftModels model, Pack_list packList)//PackListModel packListModel)
        {
            ProductOrderResponseModel productOrderResponseModel = new ProductOrderResponseModel();
            try
            {
                //Pack_list packList = packListModel.data.pack_list[0];

                string giftName = packList.pack_name;
                int pack_id = packList.pack_id;
                int volume_mb = packList.volume_mb;
                int channel_id = packList.channel_id;
                int current_balance = packList.current_balance;
                string status = packList.status;

                GiftPackDisburseViewModel disburseModel = new GiftPackDisburseViewModel();
                disburseModel.id = model.MobileNumber;// MSISDN
                disburseModel.externalId = model.CouponCode;
                disburseModel.description = "Digital Gift From One Pharma";

                OrderItem lstOrderItem = new OrderItem();
                List<OrderItem> lstOrderItem2 = new List<OrderItem>();

                Product product = new Product();

                Characteristic characteristic = new Characteristic();
                characteristic.value = pack_id;
                characteristic.name = giftName;

                lstOrderItem.product = product;
                product.characteristic = characteristic;

                lstOrderItem2.Add(lstOrderItem);
                disburseModel.orderItem = lstOrderItem2;

                string jsonBody = JsonConvert.SerializeObject(disburseModel);


                string url = string.Format("https://apigw.grameenphone.com/bulkdata/v4/productOrderingManagement/productOrder/{0}", model.MobileNumber);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"{accessToken}");
                HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, httpContent);
                response.EnsureSuccessStatusCode();

                productOrderResponseModel = JsonConvert.DeserializeObject<ProductOrderResponseModel>(await response.Content.ReadAsStringAsync());


                // Set Log
                string data = await response.Content.ReadAsStringAsync();
                await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburseLog {model.MobileNumber}, {data}").AsNoTracking().FirstOrDefaultAsync();



                // Gift Pack Status set for a Mobile.
                string GiftPackState = "", TrxID = productOrderResponseModel.id;
                var res = await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburse {userId}, {model.CouponCode}, {model.Name}, {model.MobileNumber}, {giftName}, {TrxID}, {GiftPackState}, {TrxID}, {GiftPackState}").AsNoTracking().FirstOrDefaultAsync();

                //jsonRes.data = $"Congratulations! you have received '{giftName}'.";
                //return jsonRes;
                // Added on 2024-12-14 03:14 PM for test


            }
            catch (Exception ex)
            {
                throw;
            }
            return productOrderResponseModel;
        }

        public async Task<JsonViewModel> UpdateDigitalGiftPackDisburseStatus(int? userId, string packName, DigitalGiftModels model, ProductOrderResponseModel disburseResponseModel)
        {
            JsonViewModel jsonRes = new JsonViewModel();
            try
            {
                string GiftPackState = "", TrxID = "";
                var res = await _context.saveUpdateValueViewModels.FromSql($"SetDigitalGiftDisburse {userId}, {model.CouponCode}, {model.Name}, {model.MobileNumber}, {packName}, {TrxID}, {GiftPackState}, {TrxID}, {GiftPackState}").AsNoTracking().FirstOrDefaultAsync();

                jsonRes.data = $"Congratulations! you have received '{packName}'!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return jsonRes;
        }
    }
}
