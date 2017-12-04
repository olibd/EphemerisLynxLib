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

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'getWatchDogs','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'i','type':'uint256'}],'name':'getAttributeKey','outputs':[{'name':'','type':'bytes32'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owningAttribute','type':'address'}],'name':'createCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'cert','type':'address'}],'name':'revokeCertificate','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'takeOwnership','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'deleteID','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'}],'name':'addAttribute','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newContract','type':'address'}],'name':'setWatchDogs','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newOwner','type':'address'}],'name':'changeOwner','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newRegistry','type':'address'}],'name':'setRegistry','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'getID','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'attributeCount','outputs':[{'name':'','type':'uint256'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'}],'name':'removeAttribute','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'key','type':'bytes32'}],'name':'getAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'inputs':[{'name':'_id','type':'address'}],'payable':false,'type':'constructor'}]";

        public static string BYTE_CODE = "0x6060604052341561000c57fe5b6040516020806118c7833981016040528080519060200190919050505b5b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b80600160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b505b611808806100bf6000396000f300606060405236156100ef576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680631ab807f8146100f15780631ff8df681461013457806325346c7e146101865780634f23eb86146101c25780635f893bfa146102be57806360536172146102f4578063781dd722146103065780637ca307b41461035b5780638da5cb5b1461036d578063a3f510d4146103bf578063a54c08101461040d578063a6f9dae114610443578063a91ee0dc14610479578063ab9dbd07146104af578063d75ab44814610501578063e7996f0714610527578063eb43e0331461054b575bfe5b34156100f957fe5b61013260048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506105af565b005b341561013c57fe5b61014461073e565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561018e57fe5b6101a46004808035906020019091905050610769565b60405180826000191660001916815260200191505060405180910390f35b34156101ca57fe5b61027c600480803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610821565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156102c657fe5b6102f2600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610aad565b005b34156102fc57fe5b610304610b7b565b005b341561030e57fe5b610359600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610d1a565b005b341561036357fe5b61036b610ecd565b005b341561037557fe5b61037d611050565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156103c757fe5b6103f3600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050611076565b604051808215151515815260200191505060405180910390f35b341561041557fe5b610441600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061120a565b005b341561044b57fe5b610477600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506112ff565b005b341561048157fe5b6104ad600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506113f4565b005b34156104b757fe5b6104bf6114e9565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561050957fe5b610511611514565b6040518082815260200191505060405180910390f35b341561052f57fe5b6105496004808035600019169060200190919050506115c2565b005b341561055357fe5b61056d60048080356000191690602001909190505061171c565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806106585750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561073957600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16631ab807f883836040518363ffffffff167c01000000000000000000000000000000000000000000000000000000000281526004018083600019166000191681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200192505050600060405180830381600087803b151561072657fe5b6102c65a03f1151561073457fe5b5050505b5b5b5050565b6000600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1690505b90565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663277c9467836000604051602001526040518263ffffffff167c010000000000000000000000000000000000000000000000000000000002815260040180828152602001915050602060405180830381600087803b151561080157fe5b6102c65a03f1151561080f57fe5b5050506040518051905090505b919050565b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806108cc5750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b15610aa557600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16634f23eb868585856000604051602001526040518463ffffffff167c01000000000000000000000000000000000000000000000000000000000281526004018080602001806020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018381038352868181518152602001915080519060200190808383600083146109cb575b8051825260208311156109cb576020820191506020810190506020830392506109a7565b505050905090810190601f1680156109f75780820380516001836020036101000a031916815260200191505b50838103825285818151815260200191508051906020019080838360008314610a3f575b805182526020831115610a3f57602082019150602081019050602083039250610a1b565b505050905090810190601f168015610a6b5780820380516001836020036101000a031916815260200191505b5095505050505050602060405180830381600087803b1515610a8957fe5b6102c65a03f11515610a9757fe5b5050506040518051905090505b5b5b9392505050565b600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16635f893bfa826040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050600060405180830381600087803b1515610b6657fe5b6102c65a03f11515610b7457fe5b5050505b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610c245750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b15610d1757600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663e30081a0600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff166040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050600060405180830381600087803b1515610d0457fe5b6102c65a03f11515610d1257fe5b5050505b5b5b565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610dc35750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b15610ec857600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663781dd72283836040518363ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200192505050600060405180830381600087803b1515610eb557fe5b6102c65a03f11515610ec357fe5b5050505b5b5b5050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610f765750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561104d57600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff166341c0e1b56040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050600060405180830381600087803b151561100057fe5b6102c65a03f1151561100e57fe5b505050600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16ff5b5b5b565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806111215750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561120457600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663a3f510d4836000604051602001526040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050602060405180830381600087803b15156111e857fe5b6102c65a03f115156111f657fe5b5050506040518051905090505b5b5b919050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806112b35750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b156112fb5780600360006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b5b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806113a85750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b156113f05780600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b5b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16148061149d5750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b156114e55780600260006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b5b50565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1690505b90565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663d75ab4486000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b15156115a457fe5b6102c65a03f115156115b257fe5b5050506040518051905090505b90565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16148061166b5750600360009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561171857600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663e7996f07826040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808260001916600019168152602001915050600060405180830381600087803b151561170557fe5b6102c65a03f1151561171357fe5b5050505b5b5b50565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663eb43e033836000604051602001526040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808260001916600019168152602001915050602060405180830381600087803b15156117bc57fe5b6102c65a03f115156117ca57fe5b5050506040518051905090505b9190505600a165627a7a72305820a5eff3d4be4d09595d21aaa8ef026f6034e4ec627e09cb4a3bd962fd7f2890370029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom, string _id, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI, _id);
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

        public Function GetFunctionAddCertificate()
        {
            return contract.GetFunction("addCertificate");
        }
        public Function GetFunctionGetWatchDogs()
        {
            return contract.GetFunction("getWatchDogs");
        }
        public Function GetFunctionGetAttributeKey()
        {
            return contract.GetFunction("getAttributeKey");
        }
        public Function GetFunctionCreateCertificate()
        {
            return contract.GetFunction("createCertificate");
        }
        public Function GetFunctionRevokeCertificate()
        {
            return contract.GetFunction("revokeCertificate");
        }
        public Function GetFunctionTakeOwnership()
        {
            return contract.GetFunction("takeOwnership");
        }
        public Function GetFunctionDeleteID()
        {
            return contract.GetFunction("deleteID");
        }
        public Function GetFunctionOwner()
        {
            return contract.GetFunction("owner");
        }
        public Function GetFunctionAddAttribute()
        {
            return contract.GetFunction("addAttribute");
        }
        public Function GetFunctionSetWatchDogs()
        {
            return contract.GetFunction("setWatchDogs");
        }
        public Function GetFunctionChangeOwner()
        {
            return contract.GetFunction("changeOwner");
        }
        public Function GetFunctionSetRegistry()
        {
            return contract.GetFunction("setRegistry");
        }
        public Function GetFunctionGetID()
        {
            return contract.GetFunction("getID");
        }
        public Function GetFunctionAttributeCount()
        {
            return contract.GetFunction("attributeCount");
        }
        public Function GetFunctionRemoveAttribute()
        {
            return contract.GetFunction("removeAttribute");
        }
        public Function GetFunctionGetAttribute()
        {
            return contract.GetFunction("getAttribute");
        }


        public Task<string> GetWatchDogsAsyncCall()
        {
            var function = GetFunctionGetWatchDogs();
            try
            {
                return function.CallAsync<string>();
            }
            catch (Exception e)
            {
                throw new CallFailed(e);
            }
        }
        public Task<byte[]> GetAttributeKeyAsyncCall(BigInteger i)
        {
            var function = GetFunctionGetAttributeKey();
            try
            {
                return function.CallAsync<byte[]>(i);
            }
            catch (Exception e)
            {
                throw new CallFailed(e);
            }
        }
        public Task<string> CreateCertificateAsyncCall(string _location, string _hash, string _owningAttribute)
        {
            var function = GetFunctionCreateCertificate();
            try
            {
                return function.CallAsync<string>(_location, _hash, _owningAttribute);
            }
            catch (Exception e)
            {
                throw new CallFailed(e);
            }
        }
        public Task<string> OwnerAsyncCall()
        {
            var function = GetFunctionOwner();
            try
            {
                return function.CallAsync<string>();
            }
            catch (Exception e)
            {
                throw new CallFailed(e);
            }
        }
        public Task<bool> AddAttributeAsyncCall(string attr)
        {
            var function = GetFunctionAddAttribute();
            try
            {
                return function.CallAsync<bool>(attr);
            }
            catch (Exception e)
            {
                throw new CallFailed(e);
            }
        }
        public Task<string> GetIDAsyncCall()
        {
            var function = GetFunctionGetID();
            try
            {
                return function.CallAsync<string>();
            }
            catch (Exception e)
            {
                throw new CallFailed(e);
            }
        }
        public Task<BigInteger> AttributeCountAsyncCall()
        {
            var function = GetFunctionAttributeCount();
            try
            {
                return function.CallAsync<BigInteger>();
            }
            catch (Exception e)
            {
                throw new CallFailed(e);
            }
        }
        public Task<string> GetAttributeAsyncCall(byte[] key)
        {
            var function = GetFunctionGetAttribute();
            try
            {
                return function.CallAsync<string>(key);
            }
            catch (Exception e)
            {
                throw new CallFailed(e);
            }
        }

        public async Task<string> AddCertificateAsync(byte[] key, string cert, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionAddCertificate();
            string data = function.GetData(key, cert);
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> GetAttributeKeyAsync(BigInteger i, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetAttributeKey();
            string data = function.GetData(i);
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> CreateCertificateAsync(string _location, string _hash, string _owningAttribute, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionCreateCertificate();
            string data = function.GetData(_location, _hash, _owningAttribute);
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> RevokeCertificateAsync(string cert, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionRevokeCertificate();
            string data = function.GetData(cert);
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> TakeOwnershipAsync(HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionTakeOwnership();
            string data = function.GetData();
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> AddCertificateAsync(string attr, string cert, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionAddCertificate();
            string data = function.GetData(attr, cert);
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> DeleteIDAsync(HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionDeleteID();
            string data = function.GetData();
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> AddAttributeAsync(string attr, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionAddAttribute();
            string data = function.GetData(attr);
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> SetWatchDogsAsync(string newContract, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionSetWatchDogs();
            string data = function.GetData(newContract);
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> ChangeOwnerAsync(string newOwner, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionChangeOwner();
            string data = function.GetData(newOwner);
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> SetRegistryAsync(string newRegistry, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionSetRegistry();
            string data = function.GetData(newRegistry);
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> RemoveAttributeAsync(byte[] key, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionRemoveAttribute();
            string data = function.GetData(key);
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }



    }



}

