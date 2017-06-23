using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace eVi.abi.lib.pcl
{
   public class AttributeService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':true,'inputs':[{'name':'','type':'address'}],'name':'certificates','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'hash','outputs':[{'name':'','type':'string'}],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'','type':'uint256'}],'name':'certificateKeys','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'location','outputs':[{'name':'','type':'string'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'certificateCount','outputs':[{'name':'','type':'uint256'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'_issuer','type':'address'}],'name':'getCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owner','type':'address'}],'payable':false,'type':'constructor'}]";

        public static string BYTE_CODE = "0x6060604052341561000c57fe5b604051610b32380380610b32833981016040528080518201919060200180518201919060200180519060200190919050505b5b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b82600190805190602001906100979291906100f9565b5081600290805190602001906100ae9291906100f9565b5080600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b50505061019e565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061013a57805160ff1916838001178555610168565b82800160010185558215610168579182015b8281111561016757825182559160200191906001019061014c565b5b5090506101759190610179565b5090565b61019b91905b8082111561019757600081600090555060010161017f565b5090565b90565b610985806101ad6000396000f3006060604052361561008b576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806216e5261461008d57806309bd5a60146101035780634fab056b1461019c578063516f279e146101fc57806352d7b89c146102955780637c6ebde9146102bb5780638da5cb5b146102f1578063fd531e9314610343575bfe5b341561009557fe5b6100c1600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506103b9565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561010b57fe5b6101136103ec565b6040518080602001828103825283818151815260200191508051906020019080838360008314610162575b8051825260208311156101625760208201915060208101905060208303925061013e565b505050905090810190601f16801561018e5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34156101a457fe5b6101ba600480803590602001909190505061048a565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561020457fe5b61020c6104ca565b604051808060200182810382528381815181526020019150805190602001908083836000831461025b575b80518252602083111561025b57602082019150602081019050602083039250610237565b505050905090810190601f1680156102875780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b341561029d57fe5b6102a5610568565b6040518082815260200191505060405180910390f35b34156102c357fe5b6102ef600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610576565b005b34156102f957fe5b610301610878565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561034b57fe5b610377600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061089e565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b60036020528060005260406000206000915054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60028054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156104825780601f1061045757610100808354040283529160200191610482565b820191906000526020600020905b81548152906001019060200180831161046557829003601f168201915b505050505081565b60048181548110151561049957fe5b906000526020600020900160005b915054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156105605780601f1061053557610100808354040283529160200191610560565b820191906000526020600020905b81548152906001019060200180831161054357829003601f168201915b505050505081565b600060048054905090505b90565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610874573073ffffffffffffffffffffffffffffffffffffffff168173ffffffffffffffffffffffffffffffffffffffff1663232533b26000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b151561064f57fe5b6102c65a03f1151561065d57fe5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff1614151561068a5760006000fd5b80600360008373ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b15156106fb57fe5b6102c65a03f1151561070957fe5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055506004805480600101828161079f9190610908565b916000526020600020900160005b8373ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b151561081957fe5b6102c65a03f1151561082757fe5b50505060405180519050909190916101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff160217905550505b5b5b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b6000600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1690505b919050565b81548183558181151161092f5781836000526020600020918201910161092e9190610934565b5b505050565b61095691905b8082111561095257600081600090555060010161093a565b5090565b905600a165627a7a72305820c627fba2e87aca7cf26426643053ab34df11114259def1f904a523e7a2e5482d0029";

        public static Task<string> DeployContractAsync(Web3 web3, string addressFrom, string _location, string _hash, string _owner, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            return web3.Eth.DeployContract.SendRequestAsync(ABI, BYTE_CODE, addressFrom, gas, valueAmount , _location, _hash, _owner);
        }

        private Contract contract;

        public string GetAddress()
        {
            return contract.Address;
        }

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
        public Function GetFunctionCertificateCount() {
            return contract.GetFunction("certificateCount");
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
        public Task<BigInteger> CertificateCountAsyncCall() {
            var function = GetFunctionCertificateCount();
            return function.CallAsync<BigInteger>();
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



    }



}

