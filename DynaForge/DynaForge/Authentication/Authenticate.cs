using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;

namespace Authentication
{
    public class Authenticate
    {
        //Empty contstructor to avoid import in Dynamo
        private Authenticate() { }

        /// <summary>
        /// Basic information to can access your app.
        /// </summary>
        /// <param name="ClientId">Client ID</param>
        /// <param name="ClientSecret">Client Secret</param>
        /// <param name="scope">scope option to properties</param>
        /// <returns name="token">token auth</returns>
        public static string Auth(string ClientId, string ClientSecret, string scope)
        {
            string token = String.Empty;
            var client = new RestClient("https://developer.api.autodesk.com/authentication/v1/authenticate");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);

            var FORGE_CLIENT_ID = ClientId;
            var FORGE_CLIENT_SECRET = ClientSecret;
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", FORGE_CLIENT_ID);
            request.AddParameter("client_secret", FORGE_CLIENT_SECRET);
            request.AddParameter("scope", scope);
            {
                IRestResponse response = client.Execute(request);
                Token deserializedProduct = JsonConvert.DeserializeObject<Token>(response.Content);
                token = "Bearer " + deserializedProduct.access_token;
            }
            return token;
        }
    }

    class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
