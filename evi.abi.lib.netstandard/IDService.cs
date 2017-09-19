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

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'','type':'uint256'}],'name':'attributesKeys','outputs':[{'name':'','type':'bytes32'}],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'kill','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owningAttribute','type':'address'}],'name':'createCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'cert','type':'address'}],'name':'revokeCertificate','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'}],'name':'addAttribute','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newOwner','type':'address'}],'name':'changeOwner','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'','type':'bytes32'}],'name':'attributes','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'attributeCount','outputs':[{'name':'','type':'uint256'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'}],'name':'removeAttribute','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'key','type':'bytes32'}],'name':'getAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'anonymous':false,'inputs':[{'indexed':true,'name':'_issuingAddress','type':'address'},{'indexed':true,'name':'_associatedAttribute','type':'address'},{'indexed':false,'name':'_certAddress','type':'address'}],'name':'ReturnCertificate','type':'event'}]";

        public static string BYTE_CODE = "0x60606040525b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b611837806100576000396000f300606060405236156100c3576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680631ab807f8146100c5578063277c94671461010857806341c0e1b5146101445780634f23eb86146101565780635f893bfa14610252578063781dd722146102885780638da5cb5b146102dd578063a3f510d41461032f578063a6f9dae11461037d578063b115ce0d146103b3578063d75ab44814610417578063e7996f071461043d578063eb43e03314610461575bfe5b34156100cd57fe5b61010660048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506104c5565b005b341561011057fe5b6101266004808035906020019091905050610534565b60405180826000191660001916815260200191505060405180910390f35b341561014c57fe5b610154610559565b005b341561015e57fe5b610210600480803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610645565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561025a57fe5b610286600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610888565b005b341561029057fe5b6102db600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610900565b005b34156102e557fe5b6102ed610ac3565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561033757fe5b610363600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610ae9565b604051808215151515815260200191505060405180910390f35b341561038557fe5b6103b1600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610e86565b005b34156103bb57fe5b6103d5600480803560001916906020019091905050610f23565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561041f57fe5b610427610f56565b6040518082815260200191505060405180910390f35b341561044557fe5b61045f600480803560001916906020019091905050610f64565b005b341561046957fe5b610483600480803560001916906020019091905050611072565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141561052f5761052d61052783611072565b82610900565b5b5b5b5050565b60028181548110151561054357fe5b906000526020600020900160005b915090505481565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141561064257600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141561064057600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16ff5b5b5b5b565b60006000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141561087f578484846106aa6110b8565b8080602001806020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200183810383528681815181526020019150805190602001908083836000831461072c575b80518252602083111561072c57602082019150602081019050602083039250610708565b505050905090810190601f1680156107585780820380516001836020036101000a031916815260200191505b508381038252858181518152602001915080519060200190808383600083146107a0575b8051825260208311156107a05760208201915060208101905060208303925061077c565b505050905090810190601f1680156107cc5780820380516001836020036101000a031916815260200191505b5095505050505050604051809103906000f08015156107e757fe5b90508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167f30622e57bff7cf1c7cf6ef9d13fdec4534ef26598e915bb06384a5310968f0e183604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a38091505b5b5b509392505050565b8073ffffffffffffffffffffffffffffffffffffffff1663b6549f756040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050600060405180830381600087803b15156108eb57fe5b6102c65a03f115156108f957fe5b5050505b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610abe573073ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b15156109d957fe5b6102c65a03f115156109e757fe5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff16141515610a145760006000fd5b8173ffffffffffffffffffffffffffffffffffffffff16637c6ebde9826040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050600060405180830381600087803b1515610aab57fe5b6102c65a03f11515610ab957fe5b5050505b5b5b5050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610e80573073ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b1515610bc457fe5b6102c65a03f11515610bd257fe5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff16141515610bff5760006000fd5b81600160008473ffffffffffffffffffffffffffffffffffffffff16637284e4166000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b1515610c7057fe5b6102c65a03f11515610c7e57fe5b505050604051805190506000191660001916815260200190815260200160002060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555060028054806001018281610cf091906110c8565b916000526020600020900160005b8473ffffffffffffffffffffffffffffffffffffffff16637284e4166000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b1515610d6a57fe5b6102c65a03f11515610d7857fe5b50505060405180519050909190915090600019169055508173ffffffffffffffffffffffffffffffffffffffff16600160008473ffffffffffffffffffffffffffffffffffffffff16637284e4166000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b1515610e1657fe5b6102c65a03f11515610e2457fe5b505050604051805190506000191660001916815260200190815260200160002060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff161490505b5b5b919050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610f1f5780600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b5b50565b60016020528060005260406000206000915054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b600060028054905090505b90565b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141561106d5760016000836000191660001916815260200190815260200160002060006101000a81549073ffffffffffffffffffffffffffffffffffffffff0219169055600090505b60028054905081101561106b57816000191660028281548110151561102057fe5b906000526020600020900160005b505460001916141561105d5760028181548110151561104957fe5b906000526020600020900160005b50600090555b5b8080600101915050610fff565b5b5b5b5050565b600060016000836000191660001916815260200190815260200160002060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1690505b919050565b6040516106f28061111a83390190565b8154818355818115116110ef578183600052602060002091820191016110ee91906110f4565b5b505050565b61111691905b808211156111125760008160009055506001016110fa565b5090565b90560060606040526000600360006101000a81548160ff021916908315150217905550341561002757fe5b6040516106f23803806106f2833981016040528080518201919060200180518201919060200180519060200190919050505b5b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b82600190805190602001906100b2929190610114565b5081600290805190602001906100c9929190610114565b5080600360016101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5050506101b9565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061015557805160ff1916838001178555610183565b82800160010185558215610183579182015b82811115610182578251825591602001919060010190610167565b5b5090506101909190610194565b5090565b6101b691905b808211156101b257600081600090555060010161019a565b5090565b90565b61052a806101c86000396000f30060606040523615610076576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806309bd5a6014610078578063232533b214610111578063516f279e1461016357806363d256ce146101fc5780638da5cb5b14610226578063b6549f7514610278575bfe5b341561008057fe5b61008861028a565b60405180806020018281038252838181518152602001915080519060200190808383600083146100d7575b8051825260208311156100d7576020820191506020810190506020830392506100b3565b505050905090810190601f1680156101035780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b341561011957fe5b610121610328565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561016b57fe5b61017361034e565b60405180806020018281038252838181518152602001915080519060200190808383600083146101c2575b8051825260208311156101c25760208201915060208101905060208303925061019e565b505050905090810190601f1680156101ee5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b341561020457fe5b61020c6103ec565b604051808215151515815260200191505060405180910390f35b341561022e57fe5b6102366103ff565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561028057fe5b610288610425565b005b60028054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103205780601f106102f557610100808354040283529160200191610320565b820191906000526020600020905b81548152906001019060200180831161030357829003601f168201915b505050505081565b600360019054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103e45780601f106103b9576101008083540402835291602001916103e4565b820191906000526020600020905b8154815290600101906020018083116103c757829003601f168201915b505050505081565b600360009054906101000a900460ff1681565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156104fb576001600360006101000a81548160ff0219169083151502179055507fb6fa8b8bd5eab60f292eca876e3ef90722275b785309d84b1de113ce0b8c4e7433604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5b5b5600a165627a7a72305820b2e2cf314aa8b874faa080715e4fb50327d3d0573c9ac291297843188554227d0029a165627a7a72305820744e819c14942c943988301dc1c28748126873e7cfc80f9cfc1f9f420dab81f80029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI);
            ITransactionService transactionService = new ConstantGasTransactionService(keyFrom, web3);
            return await transactionService.SignAndSendTransaction(data, "", new HexBigInteger(0), gasPrice);
        }

        private Contract contract;
        private ITransactionService _transactionService;

        public string GetAddress()
        {
            return contract.Address;
        }

        public IDService(Web3 web3, string key, string address)
        {
            this.web3 = web3;
            this._transactionService = new ConstantGasTransactionService(key, web3);
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionAddCertificate()
        {
            return contract.GetFunction("addCertificate");
        }
        public Function GetFunctionAttributesKeys()
        {
            return contract.GetFunction("attributesKeys");
        }
        public Function GetFunctionKill()
        {
            return contract.GetFunction("kill");
        }
        public Function GetFunctionCreateCertificate()
        {
            return contract.GetFunction("createCertificate");
        }
        public Function GetFunctionRevokeCertificate()
        {
            return contract.GetFunction("revokeCertificate");
        }
        public Function GetFunctionOwner()
        {
            return contract.GetFunction("owner");
        }
        public Function GetFunctionAddAttribute()
        {
            return contract.GetFunction("addAttribute");
        }
        public Function GetFunctionChangeOwner()
        {
            return contract.GetFunction("changeOwner");
        }
        public Function GetFunctionAttributes()
        {
            return contract.GetFunction("attributes");
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

        public Event GetEventReturnCertificate()
        {
            return contract.GetEvent("ReturnCertificate");
        }

        public Task<byte[]> AttributesKeysAsyncCall(BigInteger a)
        {
            var function = GetFunctionAttributesKeys();
            return function.CallAsync<byte[]>(a);
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
        public Task<string> AttributesAsyncCall(byte[] a)
        {
            var function = GetFunctionAttributes();
            return function.CallAsync<string>(a);
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
        public async Task<string> KillAsync(HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionKill();
            string data = function.GetData();
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
        public async Task<string> AddAttributeAsync(string attr, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionAddAttribute();
            string data = function.GetData(attr);
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


    public class ReturnCertificateEventDTO
    {
        [Parameter("address", "_issuingAddress", 1, true)]
        public string _issuingAddress { get; set; }

        [Parameter("address", "_associatedAttribute", 2, true)]
        public string _associatedAttribute { get; set; }

        [Parameter("address", "_certAddress", 3, false)]
        public string _certAddress { get; set; }

    }


}

