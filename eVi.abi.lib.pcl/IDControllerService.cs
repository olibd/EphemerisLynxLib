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

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'attr','type':'address'}],'name':'addAttribute','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'getWatchDogs','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'i','type':'uint256'}],'name':'getAttributeKey','outputs':[{'name':'','type':'bytes32'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owningAttribute','type':'address'}],'name':'createCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'cert','type':'address'}],'name':'revokeCertificate','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'deleteID','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newContract','type':'address'}],'name':'setWatchDogs','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newOwner','type':'address'}],'name':'changeOwner','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'getID','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'attributeCount','outputs':[{'name':'','type':'uint256'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'}],'name':'removeAttribute','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'key','type':'bytes32'}],'name':'getAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'inputs':[{'name':'_id','type':'address'}],'payable':false,'type':'constructor'}]";

        public static string BYTE_CODE = "0x6060604052341561000c57fe5b6040516020806115f3833981016040528080519060200190919050505b5b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b80600160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b505b611534806100bf6000396000f300606060405236156100d9576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806302859d60146100db5780631ab807f8146101365780631ff8df681461017957806325346c7e146101cb5780634f23eb86146102075780635f893bfa14610303578063781dd722146103395780637ca307b41461038e5780638da5cb5b146103a0578063a54c0810146103f2578063a6f9dae114610428578063ab9dbd071461045e578063d75ab448146104b0578063e7996f07146104d6578063eb43e033146104fa575bfe5b34156100e357fe5b61011c60048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061055e565b604051808215151515815260200191505060405180910390f35b341561013e57fe5b61017760048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610703565b005b341561018157fe5b610189610892565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156101d357fe5b6101e960048080359060200190919050506108bd565b60405180826000191660001916815260200191505060405180910390f35b341561020f57fe5b6102c1600480803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610975565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561030b57fe5b610337600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610c01565b005b341561034157fe5b61038c600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610ccf565b005b341561039657fe5b61039e610e82565b005b34156103a857fe5b6103b0611005565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156103fa57fe5b610426600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061102b565b005b341561043057fe5b61045c600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050611120565b005b341561046657fe5b61046e611215565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156104b857fe5b6104c0611240565b6040518082815260200191505060405180910390f35b34156104de57fe5b6104f86004808035600019169060200190919050506112ee565b005b341561050257fe5b61051c600480803560001916906020019091905050611448565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806106095750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b156106fc57600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff166302859d6084846000604051602001526040518363ffffffff167c01000000000000000000000000000000000000000000000000000000000281526004018083600019166000191681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200192505050602060405180830381600087803b15156106e057fe5b6102c65a03f115156106ee57fe5b5050506040518051905090505b5b5b92915050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806107ac5750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561088d57600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16631ab807f883836040518363ffffffff167c01000000000000000000000000000000000000000000000000000000000281526004018083600019166000191681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200192505050600060405180830381600087803b151561087a57fe5b6102c65a03f1151561088857fe5b5050505b5b5b5050565b6000600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1690505b90565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663277c9467836000604051602001526040518263ffffffff167c010000000000000000000000000000000000000000000000000000000002815260040180828152602001915050602060405180830381600087803b151561095557fe5b6102c65a03f1151561096357fe5b5050506040518051905090505b919050565b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610a205750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b15610bf957600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16634f23eb868585856000604051602001526040518463ffffffff167c01000000000000000000000000000000000000000000000000000000000281526004018080602001806020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001838103835286818151815260200191508051906020019080838360008314610b1f575b805182526020831115610b1f57602082019150602081019050602083039250610afb565b505050905090810190601f168015610b4b5780820380516001836020036101000a031916815260200191505b50838103825285818151815260200191508051906020019080838360008314610b93575b805182526020831115610b9357602082019150602081019050602083039250610b6f565b505050905090810190601f168015610bbf5780820380516001836020036101000a031916815260200191505b5095505050505050602060405180830381600087803b1515610bdd57fe5b6102c65a03f11515610beb57fe5b5050506040518051905090505b5b5b9392505050565b600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16635f893bfa826040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050600060405180830381600087803b1515610cba57fe5b6102c65a03f11515610cc857fe5b5050505b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610d785750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b15610e7d57600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663781dd72283836040518363ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200192505050600060405180830381600087803b1515610e6a57fe5b6102c65a03f11515610e7857fe5b5050505b5b5b5050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610f2b5750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561100257600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff166341c0e1b56040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050600060405180830381600087803b1515610fb557fe5b6102c65a03f11515610fc357fe5b505050600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16ff5b5b5b565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806110d45750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561111c5780600260006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b5b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806111c95750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b156112115780600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b5b50565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1690505b90565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663d75ab4486000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b15156112d057fe5b6102c65a03f115156112de57fe5b5050506040518051905090505b90565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806113975750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561144457600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663e7996f07826040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808260001916600019168152602001915050600060405180830381600087803b151561143157fe5b6102c65a03f1151561143f57fe5b5050505b5b5b50565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663eb43e033836000604051602001526040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808260001916600019168152602001915050602060405180830381600087803b15156114e857fe5b6102c65a03f115156114f657fe5b5050506040518051905090505b9190505600a165627a7a72305820a8ca8e0b5eb2277b4491742b839c5c93558a6dc9c1f13beeb018ca9fc783a3970029";

        public static Task<string> DeployContractAsync(Web3 web3, string addressFrom, string _id, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            return web3.Eth.DeployContract.SendRequestAsync(ABI, BYTE_CODE, addressFrom, gas, valueAmount , _id);
        }

        private Contract contract;

        public string GetAddress()
        {
            return contract.Address;
        }

        public IDControllerService(Web3 web3, string address)
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
        public Function GetFunctionGetWatchDogs() {
            return contract.GetFunction("getWatchDogs");
        }
        public Function GetFunctionGetAttributeKey() {
            return contract.GetFunction("getAttributeKey");
        }
        public Function GetFunctionCreateCertificate() {
            return contract.GetFunction("createCertificate");
        }
        public Function GetFunctionRevokeCertificate() {
            return contract.GetFunction("revokeCertificate");
        }
        public Function GetFunctionAddCertificate() {
            return contract.GetFunction("addCertificate");
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
        public Function GetFunctionAttributeCount() {
            return contract.GetFunction("attributeCount");
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
        public Task<byte[]> GetAttributeKeyAsyncCall(BigInteger i) {
            var function = GetFunctionGetAttributeKey();
            return function.CallAsync<byte[]>(i);
        }
        public Task<string> CreateCertificateAsyncCall(string _location, string _hash, string _owningAttribute) {
            var function = GetFunctionCreateCertificate();
            return function.CallAsync<string>(_location, _hash, _owningAttribute);
        }
        public Task<string> OwnerAsyncCall() {
            var function = GetFunctionOwner();
            return function.CallAsync<string>();
        }
        public Task<string> GetIDAsyncCall() {
            var function = GetFunctionGetID();
            return function.CallAsync<string>();
        }
        public Task<BigInteger> AttributeCountAsyncCall() {
            var function = GetFunctionAttributeCount();
            return function.CallAsync<BigInteger>();
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
        public Task<string> GetAttributeKeyAsync(string addressFrom, BigInteger i, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionGetAttributeKey();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, i);
        }
        public Task<string> CreateCertificateAsync(string addressFrom, string _location, string _hash, string _owningAttribute, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionCreateCertificate();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _location, _hash, _owningAttribute);
        }
        public Task<string> RevokeCertificateAsync(string addressFrom, string cert, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRevokeCertificate();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, cert);
        }
        public Task<string> AddCertificateAsync(string addressFrom, string attr, string cert, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddCertificate();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, attr, cert);
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
        public Task<string> RemoveAttributeAsync(string addressFrom, byte[] key, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRemoveAttribute();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, key);
        }



    }



}

