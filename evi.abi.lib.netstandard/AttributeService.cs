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

        public static string ABI = @"[{'constant':true,'inputs':[{'name':'','type':'address'}],'name':'certificates','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'hash','outputs':[{'name':'','type':'string'}],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'','type':'uint256'}],'name':'certificateKeys','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'location','outputs':[{'name':'','type':'string'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'certificateCount','outputs':[{'name':'','type':'uint256'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'description','outputs':[{'name':'','type':'bytes32'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'_issuer','type':'address'}],'name':'getCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'inputs':[{'name':'_location','type':'string'},{'name':'_description','type':'bytes32'},{'name':'_hash','type':'string'},{'name':'_owner','type':'address'}],'payable':false,'type':'constructor'}]";

        public static string BYTE_CODE = "0x6060604052341561000c57fe5b604051610b86380380610b86833981016040528080518201919060200180519060200190919080518201919060200180519060200190919050505b5b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b83600190805190602001906100a092919061010e565b5081600290805190602001906100b792919061010e565b5080600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555082600581600019169055505b505050506101b3565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061014f57805160ff191683800117855561017d565b8280016001018555821561017d579182015b8281111561017c578251825591602001919060010190610161565b5b50905061018a919061018e565b5090565b6101b091905b808211156101ac576000816000905550600101610194565b5090565b90565b6109c4806101c26000396000f30060606040523615610096576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806216e5261461009857806309bd5a601461010e5780634fab056b146101a7578063516f279e1461020757806352d7b89c146102a05780637284e416146102c65780637c6ebde9146102f45780638da5cb5b1461032a578063fd531e931461037c575bfe5b34156100a057fe5b6100cc600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506103f2565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561011657fe5b61011e610425565b604051808060200182810382528381815181526020019150805190602001908083836000831461016d575b80518252602083111561016d57602082019150602081019050602083039250610149565b505050905090810190601f1680156101995780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34156101af57fe5b6101c560048080359060200190919050506104c3565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561020f57fe5b610217610503565b6040518080602001828103825283818151815260200191508051906020019080838360008314610266575b80518252602083111561026657602082019150602081019050602083039250610242565b505050905090810190601f1680156102925780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34156102a857fe5b6102b06105a1565b6040518082815260200191505060405180910390f35b34156102ce57fe5b6102d66105af565b60405180826000191660001916815260200191505060405180910390f35b34156102fc57fe5b610328600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506105b5565b005b341561033257fe5b61033a6108b7565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561038457fe5b6103b0600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506108dd565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b60036020528060005260406000206000915054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60028054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156104bb5780601f10610490576101008083540402835291602001916104bb565b820191906000526020600020905b81548152906001019060200180831161049e57829003601f168201915b505050505081565b6004818154811015156104d257fe5b906000526020600020900160005b915054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156105995780601f1061056e57610100808354040283529160200191610599565b820191906000526020600020905b81548152906001019060200180831161057c57829003601f168201915b505050505081565b600060048054905090505b90565b60055481565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156108b3573073ffffffffffffffffffffffffffffffffffffffff168173ffffffffffffffffffffffffffffffffffffffff1663232533b26000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b151561068e57fe5b6102c65a03f1151561069c57fe5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff161415156106c95760006000fd5b80600360008373ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b151561073a57fe5b6102c65a03f1151561074857fe5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff160217905550600480548060010182816107de9190610947565b916000526020600020900160005b8373ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b151561085857fe5b6102c65a03f1151561086657fe5b50505060405180519050909190916101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff160217905550505b5b5b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b6000600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1690505b919050565b81548183558181151161096e5781836000526020600020918201910161096d9190610973565b5b505050565b61099591905b80821115610991576000816000905550600101610979565b5090565b905600a165627a7a7230582028393c4f9842348310d7e807b01aa8535dfe46e77894bd646fdab37d0009fce50029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom, string _location, byte[] _description, string _hash, string _owner, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI , _location, _description, _hash, _owner);
            ITransactionService transactionService = new ConstantGasTransactionService(keyFrom, web3);
            return await transactionService.SignAndSendTransaction(data, "", new HexBigInteger(0), gasPrice);
        }

        private Contract contract;
        private ITransactionService _transactionService;

        public string GetAddress()
        {
            return contract.Address;
        }

        public AttributeService(Web3 web3, string key, string address)
        {
            this.web3 = web3;
            this._transactionService = new ConstantGasTransactionService(key, web3);
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
        public Function GetFunctionDescription() {
            return contract.GetFunction("description");
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
            try{
                return function.CallAsync<string>(a);
            }catch(Exception e){
                throw new CallFailed(e);
            }
        }
        public Task<string> HashAsyncCall() {
            var function = GetFunctionHash();
            try{
                return function.CallAsync<string>();
            }catch(Exception e){
                throw new CallFailed(e);
            }
        }
        public Task<string> CertificateKeysAsyncCall(BigInteger a) {
            var function = GetFunctionCertificateKeys();
            try{
                return function.CallAsync<string>(a);
            }catch(Exception e){
                throw new CallFailed(e);
            }
        }
        public Task<string> LocationAsyncCall() {
            var function = GetFunctionLocation();
            try{
                return function.CallAsync<string>();
            }catch(Exception e){
                throw new CallFailed(e);
            }
        }
        public Task<BigInteger> CertificateCountAsyncCall() {
            var function = GetFunctionCertificateCount();
            try{
                return function.CallAsync<BigInteger>();
            }catch(Exception e){
                throw new CallFailed(e);
            }
        }
        public Task<byte[]> DescriptionAsyncCall() {
            var function = GetFunctionDescription();
            try{
                return function.CallAsync<byte[]>();
            }catch(Exception e){
                throw new CallFailed(e);
            }
        }
        public Task<string> OwnerAsyncCall() {
            var function = GetFunctionOwner();
            try{
                return function.CallAsync<string>();
            }catch(Exception e){
                throw new CallFailed(e);
            }
        }
        public Task<string> GetCertificateAsyncCall(string _issuer) {
            var function = GetFunctionGetCertificate();
            try{
                return function.CallAsync<string>(_issuer);
            }catch(Exception e){
                throw new CallFailed(e);
            }
        }

        public async Task<string> AddCertificateAsync(string _cert, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddCertificate();
                string data = function.GetData(_cert);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }



    }



}

