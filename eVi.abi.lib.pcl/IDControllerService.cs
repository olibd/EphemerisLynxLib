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

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'getWatchDogs','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'i','type':'uint256'}],'name':'getAttributeKey','outputs':[{'name':'','type':'bytes32'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owningAttribute','type':'address'}],'name':'createCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'cert','type':'address'}],'name':'revokeCertificate','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'deleteID','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'}],'name':'addAttribute','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newContract','type':'address'}],'name':'setWatchDogs','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newOwner','type':'address'}],'name':'changeOwner','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'getID','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'attributeCount','outputs':[{'name':'','type':'uint256'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'}],'name':'removeAttribute','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'key','type':'bytes32'}],'name':'getAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'inputs':[{'name':'_id','type':'address'}],'payable':false,'type':'constructor'}]";

        public static string BYTE_CODE = "0x6060604052341561000c57fe5b6040516020806115d5833981016040528080519060200190919050505b5b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b80600160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b505b611516806100bf6000396000f300606060405236156100d9576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680631ab807f8146100db5780631ff8df681461011e57806325346c7e146101705780634f23eb86146101ac5780635f893bfa146102a8578063781dd722146102de5780637ca307b4146103335780638da5cb5b14610345578063a3f510d414610397578063a54c0810146103e5578063a6f9dae11461041b578063ab9dbd0714610451578063d75ab448146104a3578063e7996f07146104c9578063eb43e033146104ed575bfe5b34156100e357fe5b61011c60048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610551565b005b341561012657fe5b61012e6106e0565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561017857fe5b61018e600480803590602001909190505061070b565b60405180826000191660001916815260200191505060405180910390f35b34156101b457fe5b610266600480803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506107c3565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156102b057fe5b6102dc600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610a4f565b005b34156102e657fe5b610331600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610b1d565b005b341561033b57fe5b610343610cd0565b005b341561034d57fe5b610355610e53565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561039f57fe5b6103cb600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610e79565b604051808215151515815260200191505060405180910390f35b34156103ed57fe5b610419600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061100d565b005b341561042357fe5b61044f600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050611102565b005b341561045957fe5b6104616111f7565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156104ab57fe5b6104b3611222565b6040518082815260200191505060405180910390f35b34156104d157fe5b6104eb6004808035600019169060200190919050506112d0565b005b34156104f557fe5b61050f60048080356000191690602001909190505061142a565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806105fa5750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b156106db57600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16631ab807f883836040518363ffffffff167c01000000000000000000000000000000000000000000000000000000000281526004018083600019166000191681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200192505050600060405180830381600087803b15156106c857fe5b6102c65a03f115156106d657fe5b5050505b5b5b5050565b6000600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1690505b90565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663277c9467836000604051602001526040518263ffffffff167c010000000000000000000000000000000000000000000000000000000002815260040180828152602001915050602060405180830381600087803b15156107a357fe5b6102c65a03f115156107b157fe5b5050506040518051905090505b919050565b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16148061086e5750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b15610a4757600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16634f23eb868585856000604051602001526040518463ffffffff167c01000000000000000000000000000000000000000000000000000000000281526004018080602001806020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200183810383528681815181526020019150805190602001908083836000831461096d575b80518252602083111561096d57602082019150602081019050602083039250610949565b505050905090810190601f1680156109995780820380516001836020036101000a031916815260200191505b508381038252858181518152602001915080519060200190808383600083146109e1575b8051825260208311156109e1576020820191506020810190506020830392506109bd565b505050905090810190601f168015610a0d5780820380516001836020036101000a031916815260200191505b5095505050505050602060405180830381600087803b1515610a2b57fe5b6102c65a03f11515610a3957fe5b5050506040518051905090505b5b5b9392505050565b600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16635f893bfa826040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050600060405180830381600087803b1515610b0857fe5b6102c65a03f11515610b1657fe5b5050505b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610bc65750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b15610ccb57600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663781dd72283836040518363ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200192505050600060405180830381600087803b1515610cb857fe5b6102c65a03f11515610cc657fe5b5050505b5b5b5050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610d795750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b15610e5057600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff166341c0e1b56040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050600060405180830381600087803b1515610e0357fe5b6102c65a03f11515610e1157fe5b505050600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16ff5b5b5b565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161480610f245750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561100757600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663a3f510d4836000604051602001526040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050602060405180830381600087803b1515610feb57fe5b6102c65a03f11515610ff957fe5b5050506040518051905090505b5b5b919050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806110b65750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b156110fe5780600260006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b5b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806111ab5750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b156111f35780600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b5b50565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1690505b90565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663d75ab4486000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b15156112b257fe5b6102c65a03f115156112c057fe5b5050506040518051905090505b90565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614806113795750600260009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16145b1561142657600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663e7996f07826040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808260001916600019168152602001915050600060405180830381600087803b151561141357fe5b6102c65a03f1151561142157fe5b5050505b5b5b50565b6000600160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663eb43e033836000604051602001526040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808260001916600019168152602001915050602060405180830381600087803b15156114ca57fe5b6102c65a03f115156114d857fe5b5050506040518051905090505b9190505600a165627a7a72305820b42a82aac97ef61f7ce9e97792f6dc95629f8db19114a51344fdc114853271ea0029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom, string _id, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI, _id);
            TransactionService transactionService = new TransactionService(keyFrom, web3);
            return await transactionService.SignAndSendTransaction(data, "", new HexBigInteger(0), gasPrice);
        }

        private Contract contract;
        private TransactionService _transactionService;

        public string GetAddress()
        {
            return contract.Address;
        }

        public IDControllerService(Web3 web3, string key, string address)
        {
            this.web3 = web3;
            this._transactionService = new TransactionService(key, web3);
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
            return function.CallAsync<string>();
        }
        public Task<byte[]> GetAttributeKeyAsyncCall(BigInteger i)
        {
            var function = GetFunctionGetAttributeKey();
            return function.CallAsync<byte[]>(i);
        }
        public Task<string> CreateCertificateAsyncCall(string _location, string _hash, string _owningAttribute)
        {
            var function = GetFunctionCreateCertificate();
            return function.CallAsync<string>(_location, _hash, _owningAttribute);
        }
        public Task<string> OwnerAsyncCall()
        {
            var function = GetFunctionOwner();
            return function.CallAsync<string>();
        }
        public Task<bool> AddAttributeAsyncCall(string attr)
        {
            var function = GetFunctionAddAttribute();
            return function.CallAsync<bool>(attr);
        }
        public Task<string> GetIDAsyncCall()
        {
            var function = GetFunctionGetID();
            return function.CallAsync<string>();
        }
        public Task<BigInteger> AttributeCountAsyncCall()
        {
            var function = GetFunctionAttributeCount();
            return function.CallAsync<BigInteger>();
        }
        public Task<string> GetAttributeAsyncCall(byte[] key)
        {
            var function = GetFunctionGetAttribute();
            return function.CallAsync<string>(key);
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
        public async Task<string> RemoveAttributeAsync(byte[] key, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionRemoveAttribute();
            string data = function.GetData(key);
            return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }



    }



}

