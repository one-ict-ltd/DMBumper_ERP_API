

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONEERP.Areas.Attendance.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.Models;
using ONEERP.Models.Dashboard;

namespace ONEERP.Helpers
{
    public class Tokens
    {

        #region Basic
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactoryService jwtFactory, string userName, JsonViewModel profile, JsonSerializerSettings serializerSettings)
        {


            var response = new
            {
                success = true,
                status = true,
                message = "User found ‍successfully.",
                userName = userName,
                profile = profile,
                id = identity.FindFirst("Id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = 14400000
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactoryService jwtFactory, string userName, JsonViewModel profile, JsonViewModel menu, JsonSerializerSettings serializerSettings)
        {


            var response = new
            {
                success = true,
                status = true,
                message = "User found ‍successfully.",
                userName = userName,
                profile = profile,
                menu = menu,
                id = identity.FindFirst("Id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = 14400000
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> GenerateoutJwt(JsonSerializerSettings serializerSettings)
        {


            var response = new
            {
                success = true,
                status = true,
                message = "Logout ‍successfully.",

            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> deleteChemistplanFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                status = false,
                success = false,
                message = "Customer visit plan has not deleted successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> deleteChemistplanSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                status = true,
                success = true,
                message = "Customer visit plan has deleted successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> GenerateconnectionJwt(JsonSerializerSettings serializerSettings)
        {


            var response = new
            {
                status = true,
                success = true,
                message = "Data Saved ‍successfully.",

            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> GenerateJwtFail(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {

                success = false,
                status = false,
                message = "Your credential does not match, Please try again",




            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> GenerateJwtLicenseFail()
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Your application license expired! Please contact ONE ICT Ltd.",
            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        public static async Task<string> TokenExpire(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Your Token has expired. Please Login again."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> changePasswordJwt(bool status, string actionresult, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = status,
                status = status,
                message = actionresult,
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> SetJwtTokenStatus(bool status)
        {
            var response = new
            {
                success = status,
                status = status,
                message = status ? "Valid Token." : "Invalid Token. Please Login again."
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        public static async Task<string> setJwt(JsonSerializerSettings serializerSettings, string strMsg, bool resStatus)
        {
            var response = new
            {
                status = resStatus,
                success = resStatus,
                message = strMsg
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setJwt(JsonSerializerSettings serializerSettings, string text, bool statusData, int MasterId)
        {
            var response = new
            {
                status = statusData,
                success = statusData,
                message = text,
                MasterId = MasterId
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> setJwtWithStatus(JsonSerializerSettings serializerSettings, string text, bool status)
        {
            var response = new
            {
                success = status,
                status = status,
                message = text
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getData(string data, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(data);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = textArray,
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> GetJsonResponse(string strMessage, bool resStatus)
        {
            var response = new
            {
                status = "Ok",
                success = resStatus,
                message = strMessage,
            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        public static async Task<string> getData(string data, string data2, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(data);
            JArray textArray2 = JArray.Parse(data2);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = textArray,
                data2 = textArray2,
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getDataDouble(string data1, string data2, JsonSerializerSettings serializerSettings)
        {
            JArray textArray1 = JArray.Parse(data1);
            JArray textArray2 = JArray.Parse(data2);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = textArray1,
                data1 = textArray2,
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getDataTripple(string data1, string data2, string data3, JsonSerializerSettings serializerSettings)
        {
            JArray textArray1 = JArray.Parse(data1);
            JArray textArray2 = JArray.Parse(data2);
            JArray textArray3 = JArray.Parse(data3);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = textArray1,
                data1 = textArray2,
                data2 = textArray3
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getDataWithStatus(string data, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(data);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = textArray,
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getDataWithStatusAndMessage(string data, JsonSerializerSettings serializerSettings, string message)
        {
            JArray textArray = JArray.Parse(data);
            var response = new
            {
                success = true,
                status = true,
                message = message,
                data = textArray,
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getDashboard(string data, string attn, JsonSerializerSettings serializerSettings, string message)
        {
            JArray textArray = JArray.Parse(data);
            JArray textAtta = JArray.Parse(attn);
            var response = new
            {
                success = true,
                status = true,
                message = message,
                data = textArray,
                attndence = textAtta
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getDashboardDaily(string data, JsonSerializerSettings serializerSettings, string message)
        {
            JArray textArray = JArray.Parse(data);
            //JArray textAtta = JArray.Parse(attn);
            var response = new
            {
                success = true,
                status = true,
                message = message,
                data = textArray,
                //attndence = textAtta
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getDashboardMonthly(string data1, string data2, string data3, JsonSerializerSettings serializerSettings, string message)
        {
            JArray textArray1 = JArray.Parse(data1);
            JArray textArray2 = JArray.Parse(data2);
            JArray textArray3 = JArray.Parse(data3);
            //JArray textAtta = JArray.Parse(attn);
            var response = new
            {
                success = true,
                status = true,
                message = message,
                data1 = textArray1,
                data2 = textArray2,
                data3 = textArray3,
                //attndence = textAtta
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }  
        public static async Task<string> getDashboardMonthlyproductvity( string data3, JsonSerializerSettings serializerSettings, string message)
        {
          
            JArray textArray3 = JArray.Parse(data3);
            //JArray textAtta = JArray.Parse(attn);
            var response = new
            {
                success = true,
                status = true,
                message = message,
              
                data3 = textArray3,
                //attndence = textAtta
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getDashboardAtt(string attn, JsonSerializerSettings serializerSettings, string message)
        {
            //  JArray textArray = JArray.Parse(data);
            JArray textAtta = JArray.Parse(attn);
            var response = new
            {
                success = true,
                status = true,
                message = message,
                // data = textArray,
                attndence = textAtta
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }


        public static async Task<string> getDataWithSKUNumber(string data, string SKUNumber, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(data);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = textArray,
                skuNumber = SKUNumber
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getMultipleData(string master, string detail, string multi, string image, JsonSerializerSettings serializerSettings)
        {
            JArray masterdata = JArray.Parse(master);
            JArray detaildata = JArray.Parse(detail);
            JArray multidata = JArray.Parse(multi);
            JArray imagedata = JArray.Parse(image);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                master = masterdata,
                lstdetailmodel = detaildata,
                lstmultimodel = multidata,
                lstimagemodel = imagedata,
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getTwoDataWithStatus(string data, string mode, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(data);
            JArray modeArray = JArray.Parse(mode);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = textArray,
                mode = modeArray,
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        #endregion


        #region Field Frce Tracking-----------
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactoryService jwtFactory, string userName, AspNetUsersProfileViewModel profile, JsonSerializerSettings serializerSettings)
        {


            var response = new
            {
                success = true,
                status = true,
                message = "User found ‍successfully.",
                id = identity.FindFirst("Id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = 10368000000,
                profile = profile
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> DoctorlistSuccessJwt(List<DoctorListAPIViewModel> doctorlist, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Doctors are found successfully.",

                doctors = doctorlist
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> MarketlistSuccessJwt(List<MarketListAPIViewModel> doctorlist, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Markets are found successfully.",

                markets = doctorlist
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> MarketlistPlanSuccessJwt(List<MarketListAPIPlanViewModel> doctorlist, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Markets are found successfully.",

                markets = doctorlist
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> ChemistlistSuccessJwt(List<ChemistListAPIViewModel> chemistlist, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Chemists are found successfully.",

                chemists = chemistlist
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> DoctorlistfailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Doctors are not found successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> MarketlistfailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Markets are not found successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> ChemistlistfailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Chemists are not found successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }



        public static async Task<string> setDocplanSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Doctor visit plan has created successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setMarketplanSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Market visit plan has created successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setDocSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Doctor  has created successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setMarketSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Market  has created successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setChemistSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Chemist  has created successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setChemistplanSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Chemist visit plan has created successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setDocplanFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Doctor visit plan has not created successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setMarketplanFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Market visit plan has not created successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setDocFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Doctor  has not created successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setMarketFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Market  has not created successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setChemistplanFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Chemist visit plan has not created successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setChemistFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Chemist  has not created successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> setChemistWithConversionCodeFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Chemist list has not updated successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> setChemistWithConversionCodeSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Chemist list has updated successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> updateDocplanFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Doctor visit plan has not updated successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> updateEmpplanFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Employee visit plan execution has not created successfully."


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> updateDocplanSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Doctor visit plan has updated successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> updateEmpplanSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Employee visit plan execution has created successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> updateChemistplanFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Chemist visit plan has not updated successfully."
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> updateRxUploadplanFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Rx has not uploaded successfully."
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> salesOrderFailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "Sales Order has not created."
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> updateChemistplanSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Chemist visit plan has updated successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> updateRxUploadplanSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Rx Uploaded successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> commonMesageForAll(JsonSerializerSettings serializerSettings, string text, bool statusData)
        {
            var response = new
            {
                success = statusData,
                status = statusData,
                message = text
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getDoctorSchedulelistaftersetfail(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                status = false,
                success = false,
                message = "No schedule for doctor's visit is found",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getDoctorSchedulelistaftersetsuccess(List<DoctorScheduleListViewModel> doctorScheduleListViewModels, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Doctor's schedules are found successfully.",

                doctors = doctorScheduleListViewModels


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getChemistSchedulelistaftersetfail(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "No schedule for Chemist's visit is found",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getChemistSchedulelistaftersetsuccess(List<ChemistScheduleListViewModel> chemistScheduleListViewModels, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Chemist's schedules are found successfully.",

                chemists = chemistScheduleListViewModels


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> setLocationSuccessJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "data has saved successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }


        public static async Task<string> setLocationfailJwt(JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = false,
                status = false,
                message = "data has not saved successfully",


            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getParamDataforReport(string Emp_Id, string Emp_Name, string zoneListViewModels, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(zoneListViewModels);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                id = Emp_Id,
                name = Emp_Name,
                zones = textArray,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getMarketPlanData(string Emp_Id, string Emp_Name, string zoneListViewModels, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(zoneListViewModels);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                id = Emp_Id,
                name = Emp_Name,
                Schedules = textArray,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getEmployeeDynamicData(string Emp_Id, string Emp_Name, string zoneListViewModels, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(zoneListViewModels);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                id = Emp_Id,
                name = Emp_Name,
                Employees = textArray,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getDoctorDynamicData(string Emp_Id, string Emp_Name, string zoneListViewModels, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(zoneListViewModels);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                id = Emp_Id,
                name = Emp_Name,
                Doctors = textArray,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getChemistDynamicData(string Emp_Id, string Emp_Name, string zoneListViewModels, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(zoneListViewModels);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                id = Emp_Id,
                name = Emp_Name,
                Chemists = textArray,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getDoctorVisitReport(List<VisitReportDoctorViewModel> lstdata, JsonSerializerSettings serializerSettings)
        {

            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = lstdata,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getSearchfor(string Emp_Id, string Emp_Name, string zoneListViewModels, List<SearchForViewModel> lstdata, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(zoneListViewModels);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                Id = Emp_Id,
                Name = Emp_Name,
                Search = lstdata,
                zones = textArray



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getCheckInOut(string Emp_Id, string Emp_Name, string zoneListViewModels, string zoneListViewModelsSummary, string zoneListViewModelsHistory, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(zoneListViewModels);
            JArray textArraysummary = JArray.Parse(zoneListViewModelsSummary);
            JArray textArrayHistory = JArray.Parse(zoneListViewModelsHistory);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                Id = Emp_Id,
                Name = Emp_Name,

                Data = textArray,
                Summary = textArraysummary,
                History = textArrayHistory,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getCheckInOutSummary(string Emp_Id, string Emp_Name, string zoneListViewModels, JsonSerializerSettings serializerSettings)
        {
            JArray textArray = JArray.Parse(zoneListViewModels);
            // JArray textArraysummary = JArray.Parse(zoneListViewModelsSummary);
            // JArray textArrayHistory = JArray.Parse(zoneListViewModelsHistory);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                Id = Emp_Id,
                Name = Emp_Name,

                Data = textArray,
                //   Summary = textArraysummary,
                // History = textArrayHistory,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }


        public static async Task<string> getCheckInOutHistory(string Emp_Id, string Emp_Name, string zoneListViewModelsHistory, JsonSerializerSettings serializerSettings)
        {
            //   JArray textArray = JArray.Parse(zoneListViewModels);
            // JArray textArraysummary = JArray.Parse(zoneListViewModelsSummary);
            JArray textArrayHistory = JArray.Parse(zoneListViewModelsHistory);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                Id = Emp_Id,
                Name = Emp_Name,

                //   Data = textArray,
                // Summary = textArraysummary,
                History = textArrayHistory,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getCheckInOutdetail(string Emp_Id, string Emp_Name, string zoneListViewModelsHistory, string zoneListViewModelsHistorysummary, JsonSerializerSettings serializerSettings)
        {

            JArray textArrayHistory = JArray.Parse(zoneListViewModelsHistory);
            JArray textArrayHistorysummary = JArray.Parse(zoneListViewModelsHistorysummary);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                Id = Emp_Id,
                Name = Emp_Name,


                History = textArrayHistory,
                Summary = textArrayHistorysummary,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getChemistVisitReport(List<VisitReportChemistViewModel> lstdata, JsonSerializerSettings serializerSettings)
        {

            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = lstdata,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getMIOWiseTrackingReport(List<MIOCurrentLocationViewModel> lstdata, JsonSerializerSettings serializerSettings)
        {

            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = lstdata,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getChemistWiseVisitReport(List<ChemistWiseVisitReportViewModel> lstdata, JsonSerializerSettings serializerSettings)
        {

            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = lstdata,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        public static async Task<string> getDoctorWiseVisitReport(List<DoctorWiseVisitReportViewModel> lstdata, JsonSerializerSettings serializerSettings)
        {

            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = lstdata,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getVisitSummary(string Emp_Id, string Emp_Name, List<ChemistWiseVisitReportViewModel> chemistWiseVisitReportViewModels, List<DoctorWiseVisitReportViewModel> doctorWiseVisitReportViewModels, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                Id = Emp_Id,
                Name = Emp_Name,

                TotalCollection = chemistWiseVisitReportViewModels.Sum(x => x.collectionAmount),
                TotalInvoice = chemistWiseVisitReportViewModels.Sum(x => x.invoiceAmount),
                TotalDoctor = doctorWiseVisitReportViewModels.Count(),
                TotalChemist = chemistWiseVisitReportViewModels.Count(),
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getFFTDashboardData(int? totalChemist, int? totalDoctor, List<ChemistWiseVisitReportViewModel> chemistWiseVisitReportViewModels, List<DoctorWiseVisitReportViewModel> doctorWiseVisitReportViewModels, List<LoginInfoDataViewModel> logOutData, List<LoginInfoDataViewModel> logInData, List<LoginInfoDataViewModel> notLocationData, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Success",

                todayTotalInvoice = chemistWiseVisitReportViewModels.Sum(x => x.invoiceAmount),
                todayTotalCollection = chemistWiseVisitReportViewModels.Sum(x => x.collectionAmount),

                todayVisitedCustomer = chemistWiseVisitReportViewModels.Count(),
                todayVisitedDoctor = doctorWiseVisitReportViewModels.Count(),

                totalDoctor = totalDoctor,
                totalCustomer = totalChemist,

                totalLoggedOut = logOutData.Count(),
                totalLoggedIn = logInData.Count(),
                totalNotLocation = notLocationData.Count(),

            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        internal static async Task<string> getDailyattendenceData(string data, JsonSerializerSettings serializerSettings)
        {
            //JArray textArray = await JsonParser(data);
            //var response = new
            //{
            //    status = true,
            //    message = "Success",
            //    data = textArray,

            //};
            //return JsonConvert.SerializeObject(response, jsonSerializerSettings);
            JArray textArray = JArray.Parse(data);
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = textArray,
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }
        internal static async Task<string> getFFTDashboardDataBarChart(List<ChemistWiseVisitReportViewModel> lists, JsonSerializerSettings jsonSerializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = lists,

            };
            return JsonConvert.SerializeObject(response, jsonSerializerSettings);
        }

        internal static async Task<string> getLogInOutInfoData(List<LoginInfoDataViewModel> lists, JsonSerializerSettings jsonSerializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = lists,

            };
            return JsonConvert.SerializeObject(response, jsonSerializerSettings);
        }




        public static async Task<string> ObjToJson(object obj)//MOSTAFA
        {
            //var jsondata = $"[{JsonConvert.SerializeObject(obj)}]";
            //JArray textArray = JArray.Parse(jsondata);

            JArray textArray = await JsonParser(obj);
            var response = new
            {
                status = true,
                success = true,
                message = "Success",
                data = textArray,// textArray.Count > 0 ? textArray[0] : textArray,
            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        public static async Task<JArray> JsonParser(object obj)//MOSTAFA
        {
            JArray textArray;
            var jsonStr = string.Empty;
            try
            {
                //var jsondata = $"[{JsonConvert.SerializeObject(obj)}]";

                jsonStr = JsonConvert.SerializeObject(obj);
                textArray = JArray.Parse(jsonStr);
            }
            catch (Exception ex)
            {
                textArray = JArray.Parse($"[{jsonStr}]");
            }
            return textArray;
        }

        public static async Task<string> GetJwt(string jsonData)//MOSTAFA
        {
            JArray textArray = JArray.Parse(jsonData);
            var response = new
            {
                status = true,
                success = true,
                message = "Success",
                data = textArray,
            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        public static async Task<string> GetRxJwt(string empData, string doctorData, string itemData)//MOSTAFA
        {
            JArray textEmpArray = JArray.Parse(empData);
            JArray textDoctorArray = JArray.Parse(doctorData);
            JArray textItemArray = JArray.Parse(itemData);

            var response = new
            {
                status = true,
                success = true,
                message = "Success",
                empData = textEmpArray,
                doctorData = textDoctorArray,
                itemData = textItemArray,
            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        public static async Task<string> GetRxImage(string empData)//MOSTAFA
        {
            JArray textEmpArray = JArray.Parse(empData);


            var response = new
            {
                status = true,
                success = true,
                message = "Success",
                empData = textEmpArray,

            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        public static async Task<string> GetJwt(string jsonData, string message)//MOSTAFA
        {
            JArray textArray = JArray.Parse(jsonData);
            var response = new
            {
                status = true,
                success = true,
                message = string.IsNullOrWhiteSpace(message) ? "Success" : message,
                data = textArray,
            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        public static async Task<string> GetFailedJwt(bool status, string message)//MOSTAFA
        {
            var response = new
            {
                status = status,
                success = status,
                message = message,
                data = "[]",
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        #endregion

        public static async Task<string> PartySuccessJwt(_JsonViewModel jsondata, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = jsondata.data
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getEmpVisitReport(List<VisitReportEmployeeViewModel> lstdata, JsonSerializerSettings serializerSettings)
        {

            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = lstdata,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> getEmployeeData(List<EmployeeViewModel> lstdata, JsonSerializerSettings serializerSettings)
        {

            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = lstdata,



            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        internal static async Task<string> getCalenderData(List<CalenderViewModel> lstdata, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                success = true,
                status = true,
                message = "Success",
                data = lstdata,
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public static async Task<string> GenerateJwtPasswordExpired(ClaimsIdentity identity, IJwtFactoryService jwtFactory, string userName, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {

                success = false,
                status = false,
                passwordExpired = true,
                userName = userName,
                token = await jwtFactory.GenerateEncodedToken(userName, identity),
                message = "Your password is expired, Please reset password",

            };
            var output = "";
            await Task.Run(() =>
            {
                output = JsonConvert.SerializeObject(response, serializerSettings);
            });

            return output;
        }
        public static async Task<string> GenerateJwtDummyPassword(ClaimsIdentity identity, IJwtFactoryService jwtFactory, string userName, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {

                success = false,
                status = false,
                passwordExpired = true,
                userName = userName,
                token = await jwtFactory.GenerateEncodedToken(userName, identity),
                message = "This password is invalid!, Please reset password",

            };
            var output = "";
            await Task.Run(() =>
            {
                output = JsonConvert.SerializeObject(response, serializerSettings);
            });

            return output;
        }

        public static async Task<string> GetJwtResponse(int masterId)
        {
            string strMsg;
            bool resStatus;
            if (masterId > 0) { resStatus = true; strMsg = "Data saved successfully!"; }
            else if (masterId == 0) { resStatus = false; strMsg = "Data saved process failed!"; }
            else { resStatus = false; strMsg = "Something went wrong!"; }

            var response = new
            {
                status = resStatus,
                success = resStatus,
                message = strMsg
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        public static async Task<string> GetJwtResponse(bool result)
        {
            string strMsg;
            bool resStatus;
            if (result) { resStatus = true; strMsg = "Data deleted successfully!"; }
            else { resStatus = false; strMsg = "Data delete process failed!"; }

            var response = new
            {
                status = resStatus,
                success = resStatus,
                message = strMsg
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        public static async Task<string> GetJwtResponse(string data)
        {
            JArray textArray = JArray.Parse(data);
            var response = new
            {
                success = true,
                status = true,
                message = data == "[]" ? "No data found" : "Success",
                data = textArray,
            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
