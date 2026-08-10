using Newtonsoft.Json;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Helpers
{
    public class JsonDataManipulator
    {
        public static JsonViewModel GetImagePathToImageFile(JsonViewModel jsonData)
        {
            try
            {
                dynamic jsonObj = JsonConvert.DeserializeObject(jsonData.data);
                string filePath = "", nextFilePath = "";
                foreach (var item in jsonObj)
                {
                    nextFilePath = item.imageUrl;
                    if (!string.IsNullOrWhiteSpace(nextFilePath))
                    {
                        if (filePath != nextFilePath)
                        {
                            filePath = nextFilePath;
                            byte[] imageArray = System.IO.File.Exists(filePath) ? System.IO.File.ReadAllBytes(filePath) : new byte[] { };

                            string[] array = filePath.Split(".");
                            if (imageArray.Length > 0)
                            {
                                string fileType = array[2];//array[(array.Length - 1)];
                                string img = $"data:image/{fileType};base64,{Convert.ToBase64String(imageArray)}";
                                item.imageFile = img;
                            }
                        }
                    }
                }
                jsonData.data = JsonConvert.SerializeObject(jsonObj);
                return jsonData;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static JsonViewModel GetImagePathToImageFileRepeat(JsonViewModel jsonData)
        {
            try
            {
                dynamic jsonObj = JsonConvert.DeserializeObject(jsonData.data);
                string filePath = "", nextFilePath = "";
                foreach (var item in jsonObj)
                {
                    nextFilePath = item.imageUrl;
                    if (!string.IsNullOrWhiteSpace(nextFilePath))
                    {
                        //if (filePath != nextFilePath)
                        //{
                        filePath = nextFilePath;
                        byte[] imageArray = System.IO.File.Exists(filePath) ? System.IO.File.ReadAllBytes(filePath) : new byte[] { };


                        string[] array = filePath.Split(".");
                        if (imageArray.Length > 0)
                        {
                            string fileType = array[2];//array[(array.Length - 1)];
                            string img = $"data:image/{fileType};base64,{Convert.ToBase64String(imageArray)}";
                            item.imageFile = img;
                        }
                        //}
                    }
                }
                jsonData.data = JsonConvert.SerializeObject(jsonObj);
                return jsonData;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
