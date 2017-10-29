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

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'getWatchDogs','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'i','type':'uint256'}],'name':'getAttributeKey','outputs':[{'name':'','type':'bytes32'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owningAttribute','type':'address'}],'name':'createCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'cert','type':'address'}],'name':'revokeCertificate','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[],'name':'takeOwnership','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[],'name':'deleteID','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'}],'name':'addAttribute','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'newContract','type':'address'}],'name':'setWatchDogs','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'newOwner','type':'address'}],'name':'changeOwner','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'newRegistry','type':'address'}],'name':'setRegistry','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'getID','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'attributeCount','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'}],'name':'removeAttribute','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'name':'key','type':'bytes32'}],'name':'getAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'inputs':[{'name':'_id','type':'address'}],'payable':false,'stateMutability':'nonpayable','type':'constructor'}]";

        public static string BYTE_CODE = "0x6060604052341561000f57600080fd5b6040516020806118f583398101604052808051906020019091905050336000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555080600160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555050611839806100bc6000396000f3006060604052600436106100f1576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680631ab807f8146100f65780631ff8df681461013c57806325346c7e146101915780634f23eb86146101d05780635f893bfa146102cf5780636053617214610308578063781dd7221461031d5780637ca307b4146103755780638da5cb5b1461038a578063a3f510d4146103df578063a54c081014610430578063a6f9dae114610469578063a91ee0dc146104a2578063ab9dbd07146104db578063d75ab44814610530578063e7996f0714610559578063eb43e03314610580575b600080fd5b341561010157600080fd5b61013a60048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506105e7565b005b341561014757600080fd5b61014f610779565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561019c57600080fd5b6101b260048080359060200190919050506107a3565b60405180826000191660001916815260200191505060405180910390f35b34156101db57600080fd5b61028d600480803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610860565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156102da57600080fd5b610306600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610ad3565b005b341561031357600080fd5b61031b610ba6565b005b341561032857600080fd5b610373600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610d48565b005b341561038057600080fd5b610388610efe565b005b341561039557600080fd5b61039d611080565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156103ea57600080fd5b610416600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506110a5565b604051808215151515815260200191505060405180910390f35b341561043b57600080fd5b610467600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061123c565b005b341561047457600080fd5b6104a0600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061132e565b005b34156104ad57600080fd5b6104d9600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061141f565b005b34156104e657600080fd5b6104ee611511565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561053b57600080fd5b61054361153b565b6040518082815260200191505060405180910390f35b341561056457600080fd5b61057e6004808035600019169060200190919050506115eb565b005b341561058b57600080fd5b6105a5600480803560001916906020019091905050611748565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16148061068f5750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561077557600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16631ab807f883836040518363ffffffff167c01000000000000000000000000000000000000000000000000000000000281526004018083600019166000191681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200192505050600060405180830381600087803b151561076057600080fd5b6102c65a03f1151561077157600080fd5b5050505b5050565b6000600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff16905090565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663277c9467836000604051602001526040518263ffffffff167c010000000000000000000000000000000000000000000000000000000002815260040180828152602001915050602060405180830381600087803b151561083e57600080fd5b6102c65a03f1151561084f57600080fd5b505050604051805190509050919050565b60008060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16148061090a5750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b15610acc57600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16634f23eb868585856000604051602001526040518463ffffffff167c01000000000000000000000000000000000000000000000000000000000281526004018080602001806020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001838103835286818151815260200191508051906020019080838360005b838110156109fa5780820151818401526020810190506109df565b50505050905090810190601f168015610a275780820380516001836020036101000a031916815260200191505b50838103825285818151815260200191508051906020019080838360005b83811015610a60578082015181840152602081019050610a45565b50505050905090810190601f168015610a8d5780820380516001836020036101000a031916815260200191505b5095505050505050602060405180830381600087803b1515610aae57600080fd5b6102c65a03f11515610abf57600080fd5b5050506040518051905090505b9392505050565b600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16635f893bfa826040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050600060405180830381600087803b1515610b8f57600080fd5b6102c65a03f11515610ba057600080fd5b50505050565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610c4e5750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b15610d4657600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663e30081a0600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff166040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050600060405180830381600087803b1515610d3157600080fd5b6102c65a03f11515610d4257600080fd5b5050505b565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610df05750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b15610efa57600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663781dd72283836040518363ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200192505050600060405180830381600087803b1515610ee557600080fd5b6102c65a03f11515610ef657600080fd5b5050505b5050565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610fa65750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561107e57600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff166341c0e1b56040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401600060405180830381600087803b151561103057600080fd5b6102c65a03f1151561104157600080fd5b5050506000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16ff5b565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60008060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16148061114f5750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561123757600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663a3f510d4836000604051602001526040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050602060405180830381600087803b151561121957600080fd5b6102c65a03f1151561122a57600080fd5b5050506040518051905090505b919050565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806112e45750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561132b5780600360006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b50565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806113d65750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561141c57806000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b50565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806114c75750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561150e5780600260006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b50565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff16905090565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663d75ab4486000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401602060405180830381600087803b15156115cb57600080fd5b6102c65a03f115156115dc57600080fd5b50505060405180519050905090565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806116935750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561174557600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663e7996f07826040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808260001916600019168152602001915050600060405180830381600087803b151561173057600080fd5b6102c65a03f1151561174157600080fd5b5050505b50565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663eb43e033836000604051602001526040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808260001916600019168152602001915050602060405180830381600087803b15156117eb57600080fd5b6102c65a03f115156117fc57600080fd5b5050506040518051905090509190505600a165627a7a72305820d3df4a98427414448c1c690ad41607c3ce45ba0ebf8e5915d7ecf5ee381e8b780029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom, string _id, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI , _id);
            ITransactionService transactionService = new ConstantGasTransactionService(keyFrom, web3);
            return await transactionService.SignAndSendTransaction(data, "", new HexBigInteger(0), gasPrice);
        }

        private Contract contract;
        private ITransactionService _transactionService;

        public string GetAddress()
        {
            return contract.Address;
        }

        public IDControllerService(Web3 web3, string key, string address)
        {
            this.web3 = web3;
            this._transactionService = new ConstantGasTransactionService(key, web3);
            this.contract = web3.Eth.GetContract(ABI, address);
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
        public Function GetFunctionTakeOwnership() {
            return contract.GetFunction("takeOwnership");
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
        public Function GetFunctionAddAttribute() {
            return contract.GetFunction("addAttribute");
        }
        public Function GetFunctionSetWatchDogs() {
            return contract.GetFunction("setWatchDogs");
        }
        public Function GetFunctionChangeOwner() {
            return contract.GetFunction("changeOwner");
        }
        public Function GetFunctionSetRegistry() {
            return contract.GetFunction("setRegistry");
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
        public Task<bool> AddAttributeAsyncCall(string attr) {
            var function = GetFunctionAddAttribute();
            return function.CallAsync<bool>(attr);
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

        public async Task<string> GetAttributeKeyAsync(BigInteger i, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionGetAttributeKey();
                string data = function.GetData(i);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> CreateCertificateAsync(string _location, string _hash, string _owningAttribute, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionCreateCertificate();
                string data = function.GetData(_location, _hash, _owningAttribute);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> RevokeCertificateAsync(string cert, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRevokeCertificate();
                string data = function.GetData(cert);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> TakeOwnershipAsync( HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionTakeOwnership();
                string data = function.GetData();
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> AddCertificateAsync(string attr, string cert, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddCertificate();
                string data = function.GetData(attr, cert);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> DeleteIDAsync( HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionDeleteID();
                string data = function.GetData();
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> AddAttributeAsync(string attr, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddAttribute();
                string data = function.GetData(attr);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> SetWatchDogsAsync(string newContract, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionSetWatchDogs();
                string data = function.GetData(newContract);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> ChangeOwnerAsync(string newOwner, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionChangeOwner();
                string data = function.GetData(newOwner);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> SetRegistryAsync(string newRegistry, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionSetRegistry();
                string data = function.GetData(newRegistry);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> RemoveAttributeAsync(byte[] key, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRemoveAttribute();
                string data = function.GetData(key);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }



    }



}

