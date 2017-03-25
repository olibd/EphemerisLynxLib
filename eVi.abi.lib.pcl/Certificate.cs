using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace eVi.abi.lib.pcl
{
   public class CertificateService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':true,'inputs':[],'name':'hash','outputs':[{'name':'','type':'string'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owningAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'location','outputs':[{'name':'','type':'string'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'revoked','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'revoke','outputs':[],'payable':false,'type':'function'},{'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owningAttribute','type':'address'}],'payable':false,'type':'constructor'}]";

        private Contract contract;

        public CertificateService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionHash() {
            return contract.GetFunction("hash");
        }
        public Function GetFunctionOwningAttribute() {
            return contract.GetFunction("owningAttribute");
        }
        public Function GetFunctionLocation() {
            return contract.GetFunction("location");
        }
        public Function GetFunctionRevoked() {
            return contract.GetFunction("revoked");
        }
        public Function GetFunctionOwner() {
            return contract.GetFunction("owner");
        }
        public Function GetFunctionRevoke() {
            return contract.GetFunction("revoke");
        }


        public Task<string> HashAsyncCall() {
            var function = GetFunctionHash();
            return function.CallAsync<string>();
        }
        public Task<string> OwningAttributeAsyncCall() {
            var function = GetFunctionOwningAttribute();
            return function.CallAsync<string>();
        }
        public Task<string> LocationAsyncCall() {
            var function = GetFunctionLocation();
            return function.CallAsync<string>();
        }
        public Task<bool> RevokedAsyncCall() {
            var function = GetFunctionRevoked();
            return function.CallAsync<bool>();
        }
        public Task<string> OwnerAsyncCall() {
            var function = GetFunctionOwner();
            return function.CallAsync<string>();
        }

        public Task<string> RevokeAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRevoke();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount);
        }



    }



}
