﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;
using RestSharp;

namespace TokenFlex
{
    public class ContractNumber
    {
        private ContractNumber() { }

        /// <summary>
        /// Get list of Forge Contracts
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static List<string> Get(string Token)
        {

            var client = new RestClient("https://developer.api.autodesk.com/tokenflex/v1/contract");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddHeader("Cookie", "PF=dA1dOj5zo1AhsRo55Ccip1");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            RootobjectContractNumber deserializedProduct = JsonConvert.DeserializeObject<RootobjectContractNumber>(response.Content);

            if (deserializedProduct != null)
            {
                List < string > result = deserializedProduct.Property1.Select(val => val.contractNumber).ToList();


                return result;
            }
            else
            {
                return null;
            }

        }
    }


    class RootobjectContractNumber
    {
        public ContractsNumber[] Property1 { get; set; }
    }

    class ContractsNumber
    {
        public string contractNumber { get; set; }
        public string contractName { get; set; }
        public string contractStartDate { get; set; }
        public string contractEndDate { get; set; }
        public bool isActive { get; set; }
    }


}
