using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace eVi.abi.lib.pcl
{
   public class IDService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'attr','type':'address'}],'name':'addAttribute','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'','type':'uint256'}],'name':'attributesKeys','outputs':[{'name':'','type':'bytes32'}],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'kill','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newOwner','type':'address'}],'name':'changeOwner','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'','type':'bytes32'}],'name':'attributes','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'}],'name':'removeAttribute','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'key','type':'bytes32'}],'name':'getAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'}]";

        private Contract contract;

        public IDService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionAddAttribute() {
            return contract.GetFunction("addAttribute");
        }
        public Function GetFunctionAddCertificate() {
            return contract.GetFunction("addCertificate");
        }
        public Function GetFunctionAttributesKeys() {
            return contract.GetFunction("attributesKeys");
        }
        public Function GetFunctionKill() {
            return contract.GetFunction("kill");
        }
        public Function GetFunctionAddCertificate() {
            return contract.GetFunction("addCertificate");
        }
        public Function GetFunctionOwner() {
            return contract.GetFunction("owner");
        }
        public Function GetFunctionChangeOwner() {
            return contract.GetFunction("changeOwner");
        }
        public Function GetFunctionAttributes() {
            return contract.GetFunction("attributes");
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
        public Task<byte[]> AttributesKeysAsyncCall(BigInteger a) {
            var function = GetFunctionAttributesKeys();
            return function.CallAsync<byte[]>(a);
        }
        public Task<string> OwnerAsyncCall() {
            var function = GetFunctionOwner();
            return function.CallAsync<string>();
        }
        public Task<string> AttributesAsyncCall(byte[] a) {
            var function = GetFunctionAttributes();
            return function.CallAsync<string>(a);
        }
        public Task<string> GetAttributeAsyncCall(byte[] key) {
            var function = GetFunctionGetAttribute();
            return function.CallAsync<string>(key);
        }

        public Task<string> AddAttributeAsync(string addressFrom, byte[] key, string attr, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddAttribute();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, key, attr);
        }
        public Task<string> AddCertificateAsync(string addressFrom, byte[] key, string cert, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddCertificate();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, key, cert);
        }
        public Task<string> KillAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionKill();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount);
        }
        public Task<string> AddCertificateAsync(string addressFrom, string attr, string cert, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddCertificate();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, attr, cert);
        }
        public Task<string> ChangeOwnerAsync(string addressFrom, string newOwner, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionChangeOwner();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, newOwner);
        }
        public Task<string> RemoveAttributeAsync(string addressFrom, byte[] key, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRemoveAttribute();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, key);
        }



    }



}

