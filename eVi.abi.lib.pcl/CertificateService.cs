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

        public static string ABI = @"[{'constant':true,'inputs':[],'name':'hash','outputs':[{'name':'','type':'string'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owningAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'location','outputs':[{'name':'','type':'string'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'revoked','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'revoke','outputs':[],'payable':false,'type':'function'},{'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owningAttribute','type':'address'}],'payable':false,'type':'constructor'},{'anonymous':false,'inputs':[{'indexed':false,'name':'_sender','type':'address'}],'name':'Revoked','type':'event'}]";

        public static string BYTE_CODE = "0x60606040526000600360006101000a81548160ff021916908315150217905550341561002757fe5b6040516106f23803806106f2833981016040528080518201919060200180518201919060200180519060200190919050505b5b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b82600190805190602001906100b2929190610114565b5081600290805190602001906100c9929190610114565b5080600360016101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5050506101b9565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061015557805160ff1916838001178555610183565b82800160010185558215610183579182015b82811115610182578251825591602001919060010190610167565b5b5090506101909190610194565b5090565b6101b691905b808211156101b257600081600090555060010161019a565b5090565b90565b61052a806101c86000396000f30060606040523615610076576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806309bd5a6014610078578063232533b214610111578063516f279e1461016357806363d256ce146101fc5780638da5cb5b14610226578063b6549f7514610278575bfe5b341561008057fe5b61008861028a565b60405180806020018281038252838181518152602001915080519060200190808383600083146100d7575b8051825260208311156100d7576020820191506020810190506020830392506100b3565b505050905090810190601f1680156101035780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b341561011957fe5b610121610328565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561016b57fe5b61017361034e565b60405180806020018281038252838181518152602001915080519060200190808383600083146101c2575b8051825260208311156101c25760208201915060208101905060208303925061019e565b505050905090810190601f1680156101ee5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b341561020457fe5b61020c6103ec565b604051808215151515815260200191505060405180910390f35b341561022e57fe5b6102366103ff565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561028057fe5b610288610425565b005b60028054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103205780601f106102f557610100808354040283529160200191610320565b820191906000526020600020905b81548152906001019060200180831161030357829003601f168201915b505050505081565b600360019054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103e45780601f106103b9576101008083540402835291602001916103e4565b820191906000526020600020905b8154815290600101906020018083116103c757829003601f168201915b505050505081565b600360009054906101000a900460ff1681565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156104fb576001600360006101000a81548160ff0219169083151502179055507fb6fa8b8bd5eab60f292eca876e3ef90722275b785309d84b1de113ce0b8c4e7433604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5b5b5600a165627a7a723058207e8e60a143f1603b821cfdb595b0513bb3c44d6bfe9fb8ca67b2ef59c6fdca8d0029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom, string _location, string _hash, string _owningAttribute, HexBigInteger gas = null, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI , _location, _hash, _owningAttribute);
            TransactionService transactionService = new TransactionService(keyFrom, web3);
            return await transactionService.SignAndSendTransaction(data, "", gasPrice, gas);
        }

        private Contract contract;
        private TransactionService _transactionService;

        public string GetAddress()
        {
            return contract.Address;
        }

        public CertificateService(Web3 web3, string key, string address)
        {
            this.web3 = web3;
            this._transactionService = new TransactionService(key, web3);
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

        public Event GetEventRevoked() {
            return contract.GetEvent("Revoked");
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

        public async Task<string> RevokeAsync( HexBigInteger gas = null, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRevoke();
	        string data = function.GetData();
	        return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice, gas);
        }



    }



}

