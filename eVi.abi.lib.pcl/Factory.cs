using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace eVi.abi.lib.pcl
{
   public class FactoryService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':false,'inputs':[],'name':'createID','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'anonymous':false,'inputs':[{'indexed':true,'name':'_from','type':'address'},{'indexed':false,'name':'_controllerAddress','type':'address'}],'name':'ReturnIDController','type':'event'}]";

        private Contract contract;

        public FactoryService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionCreateID() {
            return contract.GetFunction("createID");
        }
        public Function GetFunctionOwner() {
            return contract.GetFunction("owner");
        }

        public Event GetEventReturnIDController() {
            return contract.GetEvent("ReturnIDController");
        }

        public Task<string> CreateIDAsyncCall() {
            var function = GetFunctionCreateID();
            return function.CallAsync<string>();
        }
        public Task<string> OwnerAsyncCall() {
            var function = GetFunctionOwner();
            return function.CallAsync<string>();
        }

        public Task<string> CreateIDAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionCreateID();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount);
        }



    }


    public class ReturnIDControllerEventDTO
    {
        [Parameter("address", "_from", 1, true)]
        public string _from {get; set;}

        [Parameter("address", "_controllerAddress", 2, false)]
        public string _controllerAddress {get; set;}

    }


}

