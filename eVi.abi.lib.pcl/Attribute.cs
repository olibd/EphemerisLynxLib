using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace DefaultNamespace
{
   public class AttributeService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':true,'inputs':[{'name':'','type':'address'}],'name':'certificates','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'hash','outputs':[{'name':'','type':'string'}],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'','type':'uint256'}],'name':'certificateKeys','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'location','outputs':[{'name':'','type':'string'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_issuer','type':'address'}],'name':'getCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owner','type':'address'}],'payable':false,'type':'constructor'}]";

        private Contract contract;

        public AttributeService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionCertificates() {
            return contract.GetFunction("certificates");
        }
        public Function GetFunctionHash() {
            return contract.GetFunction("hash");
        }
        public Function GetFunctionCertificateKeys() {
            return contract.GetFunction("certificateKeys");
        }
        public Function GetFunctionLocation() {
            return contract.GetFunction("location");
        }
        public Function GetFunctionAddCertificate() {
            return contract.GetFunction("addCertificate");
        }
        public Function GetFunctionOwner() {
            return contract.GetFunction("owner");
        }
        public Function GetFunctionGetCertificate() {
            return contract.GetFunction("getCertificate");
        }


        public Task<string> CertificatesAsyncCall(string a) {
            var function = GetFunctionCertificates();
            return function.CallAsync<string>(a);
        }
        public Task<string> HashAsyncCall() {
            var function = GetFunctionHash();
            return function.CallAsync<string>();
        }
        public Task<string> CertificateKeysAsyncCall(BigInteger a) {
            var function = GetFunctionCertificateKeys();
            return function.CallAsync<string>(a);
        }
        public Task<string> LocationAsyncCall() {
            var function = GetFunctionLocation();
            return function.CallAsync<string>();
        }
        public Task<string> OwnerAsyncCall() {
            var function = GetFunctionOwner();
            return function.CallAsync<string>();
        }
        public Task<string> GetCertificateAsyncCall(string _issuer) {
            var function = GetFunctionGetCertificate();
            return function.CallAsync<string>(_issuer);
        }

        public Task<string> AddCertificateAsync(string addressFrom, string _cert, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddCertificate();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _cert);
        }
        public Task<string> GetCertificateAsync(string addressFrom, string _issuer, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionGetCertificate();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _issuer);
        }



    }



}

