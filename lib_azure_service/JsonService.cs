using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lib_azure_service
{
    public class JsonService
    {
        public void PostJson(string endPointUrl, string apiKey, Azure_user_model json_model)
        {
            string json_string = JsonConvert.SerializeObject(json_model);
            byte[] bytes = Encoding.UTF8.GetBytes(json_string);
            string json = Encoding.UTF8.GetString(bytes);

            using (var client = new WebClientEx())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers.Add("ApiKey", apiKey);
                client.Encoding = Encoding.UTF8;
                client.UploadString(endPointUrl, "POST", json);
                var result = client.ResponseHeaders;
            }
        }
    }

    public class WebClientEx : WebClient
    {
        public int Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            request.Timeout = 900000;
            return request;
        }
    }
}
