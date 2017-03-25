using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace eVi.abi.lib.pcl
{
   public class IDControllerService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'attr','type':'address'}],'name':'addAttribute','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'getWatchDogs','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'deleteID','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newContract','type':'address'}],'name':'setWatchDogs','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newOwner','type':'address'}],'name':'changeOwner','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'getID','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'}],'name':'removeAttribute','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'}],'name':'getAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'inputs':[{'name':'_id','type':'address'}],'payable':false,'type':'constructor'}]";

        private Contract contract;

        public IDControllerService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionAddAttribute() {
            return contract.GetFunction("addAttribute");
        }
        public Function GetFunctionGetWatchDogs() {
            return contract.GetFunction("getWatchDogs");
        }
        public Function GetFunctionDeleteID() {
            return contract.GetFunction("deleteID");
        }
        public Function GetFunctionOwner() {
            return contract.GetFunction("owner");
        }
        public Function GetFunctionSetWatchDogs() {
            return contract.GetFunction("setWatchDogs");
        }
        public Function GetFunctionChangeOwner() {
            return contract.GetFunction("changeOwner");
        }
        public Function GetFunctionGetID() {
            return contract.GetFunction("getID");
        }
        public Function GetFunctionRemoveAttribute() {
            return contract.GetFunction("removeAttribute");
        }
        public Function GetFunctionGetAttribute() {
            return contract.GetFunction("getAttribute");
        }


        public Task<bool> AddAttributeAsyncCall(byte[] key, string attr) {
            var function = GetFunctionAddAttribute();
            return function.CallAsync<bool>(key, attr);
        }
        public Task<string> GetWatchDogsAsyncCall() {
            var function = GetFunctionGetWatchDogs();
            return function.CallAsync<string>();
        }
        public Task<string> OwnerAsyncCall() {
            var function = GetFunctionOwner();
            return function.CallAsync<string>();
        }
        public Task<string> GetIDAsyncCall() {
            var function = GetFunctionGetID();
            return function.CallAsync<string>();
        }
        public Task<string> GetAttributeAsyncCall(byte[] key) {
            var function = GetFunctionGetAttribute();
            return function.CallAsync<string>(key);
        }

        public Task<string> AddAttributeAsync(string addressFrom, byte[] key, string attr, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddAttribute();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, key, attr);
        }
        public Task<string> GetWatchDogsAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionGetWatchDogs();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount);
        }
        public Task<string> DeleteIDAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionDeleteID();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount);
        }
        public Task<string> SetWatchDogsAsync(string addressFrom, string newContract, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionSetWatchDogs();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, newContract);
        }
        public Task<string> ChangeOwnerAsync(string addressFrom, string newOwner, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionChangeOwner();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, newOwner);
        }
        public Task<string> GetIDAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionGetID();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount);
        }
        public Task<string> RemoveAttributeAsync(string addressFrom, byte[] key, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRemoveAttribute();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, key);
        }
        public Task<string> GetAttributeAsync(string addressFrom, byte[] key, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionGetAttribute();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, key);
        }



    }



}

