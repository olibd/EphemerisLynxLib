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

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'name':'','type':'uint256'}],'name':'attributesKeys','outputs':[{'name':'','type':'bytes32'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[],'name':'kill','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owningAttribute','type':'address'}],'name':'createCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'cert','type':'address'}],'name':'revokeCertificate','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'}],'name':'addAttribute','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'newOwner','type':'address'}],'name':'changeOwner','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'name':'','type':'bytes32'}],'name':'attributes','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'attributeCount','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'}],'name':'removeAttribute','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'name':'key','type':'bytes32'}],'name':'getAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'anonymous':false,'inputs':[{'indexed':true,'name':'_issuingAddress','type':'address'},{'indexed':true,'name':'_associatedAttribute','type':'address'},{'indexed':false,'name':'_certAddress','type':'address'}],'name':'ReturnCertificate','type':'event'}]";

        public static string BYTE_CODE = "0x6060604052336000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555061182d806100536000396000f3006060604052600436106100c5576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680631ab807f8146100ca578063277c94671461011057806341c0e1b51461014f5780634f23eb86146101645780635f893bfa14610263578063781dd7221461029c5780638da5cb5b146102f4578063a3f510d414610349578063a6f9dae11461039a578063b115ce0d146103d3578063d75ab4481461043a578063e7996f0714610463578063eb43e0331461048a575b600080fd5b34156100d557600080fd5b61010e60048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506104f1565b005b341561011b57600080fd5b610131600480803590602001909190505061055d565b60405180826000191660001916815260200191505060405180910390f35b341561015a57600080fd5b610162610581565b005b341561016f57600080fd5b610221600480803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610668565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561026e57600080fd5b61029a600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061088e565b005b34156102a757600080fd5b6102f2600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610908565b005b34156102ff57600080fd5b610307610ad0565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561035457600080fd5b610380600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610af5565b604051808215151515815260200191505060405180910390f35b34156103a557600080fd5b6103d1600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610e99565b005b34156103de57600080fd5b6103f8600480803560001916906020019091905050610f32565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561044557600080fd5b61044d610f65565b6040518082815260200191505060405180910390f35b341561046e57600080fd5b610488600480803560001916906020019091905050610f72565b005b341561049557600080fd5b6104af600480803560001916906020019091905050611074565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156105595761055861055283611074565b82610908565b5b5050565b60028181548110151561056c57fe5b90600052602060002090016000915090505481565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610666576000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610665576000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16ff5b5b565b6000806000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610886578484846106cb6110b9565b8080602001806020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001838103835286818151815260200191508051906020019080838360005b8381101561073e578082015181840152602081019050610723565b50505050905090810190601f16801561076b5780820380516001836020036101000a031916815260200191505b50838103825285818151815260200191508051906020019080838360005b838110156107a4578082015181840152602081019050610789565b50505050905090810190601f1680156107d15780820380516001836020036101000a031916815260200191505b5095505050505050604051809103906000f08015156107ef57600080fd5b90508273ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff167f30622e57bff7cf1c7cf6ef9d13fdec4534ef26598e915bb06384a5310968f0e183604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a38091505b509392505050565b8073ffffffffffffffffffffffffffffffffffffffff1663b6549f756040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401600060405180830381600087803b15156108f157600080fd5b6102c65a03f1151561090257600080fd5b50505050565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610acc573073ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401602060405180830381600087803b15156109e057600080fd5b6102c65a03f115156109f157600080fd5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff16141515610a1d57600080fd5b8173ffffffffffffffffffffffffffffffffffffffff16637c6ebde9826040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050600060405180830381600087803b1515610ab757600080fd5b6102c65a03f11515610ac857600080fd5b5050505b5050565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60008060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610e94573073ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401602060405180830381600087803b1515610bcf57600080fd5b6102c65a03f11515610be057600080fd5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff16141515610c0c57600080fd5b81600160008473ffffffffffffffffffffffffffffffffffffffff16637284e4166000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401602060405180830381600087803b1515610c7d57600080fd5b6102c65a03f11515610c8e57600080fd5b505050604051805190506000191660001916815260200190815260200160002060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555060028054806001018281610d0091906110c9565b916000526020600020900160008473ffffffffffffffffffffffffffffffffffffffff16637284e4166000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401602060405180830381600087803b1515610d7957600080fd5b6102c65a03f11515610d8a57600080fd5b50505060405180519050909190915090600019169055508173ffffffffffffffffffffffffffffffffffffffff16600160008473ffffffffffffffffffffffffffffffffffffffff16637284e4166000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401602060405180830381600087803b1515610e2857600080fd5b6102c65a03f11515610e3957600080fd5b505050604051805190506000191660001916815260200190815260200160002060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff161490505b919050565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610f2f57806000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b50565b60016020528060005260406000206000915054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b6000600280549050905090565b60008060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156110705760016000836000191660001916815260200190815260200160002060006101000a81549073ffffffffffffffffffffffffffffffffffffffff0219169055600090505b60028054905081101561106f57816000191660028281548110151561102d57fe5b9060005260206000209001546000191614156110625760028181548110151561105257fe5b9060005260206000209001600090555b808060010191505061100c565b5b5050565b600060016000836000191660001916815260200190815260200160002060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff169050919050565b6040516106e78061111b83390190565b8154818355818115116110f0578183600052602060002091820191016110ef91906110f5565b5b505050565b61111791905b808211156111135760008160009055506001016110fb565b5090565b90560060606040526000600360006101000a81548160ff021916908315150217905550341561002a57600080fd5b6040516106e73803806106e783398101604052808051820191906020018051820191906020018051906020019091905050336000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555082600190805190602001906100b1929190610112565b5081600290805190602001906100c8929190610112565b5080600360016101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505050506101b7565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061015357805160ff1916838001178555610181565b82800160010185558215610181579182015b82811115610180578251825591602001919060010190610165565b5b50905061018e9190610192565b5090565b6101b491905b808211156101b0576000816000905550600101610198565b5090565b90565b610521806101c66000396000f300606060405260043610610078576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806309bd5a601461007d578063232533b21461010b578063516f279e1461016057806363d256ce146101ee5780638da5cb5b1461021b578063b6549f7514610270575b600080fd5b341561008857600080fd5b610090610285565b6040518080602001828103825283818151815260200191508051906020019080838360005b838110156100d05780820151818401526020810190506100b5565b50505050905090810190601f1680156100fd5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b341561011657600080fd5b61011e610323565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561016b57600080fd5b610173610349565b6040518080602001828103825283818151815260200191508051906020019080838360005b838110156101b3578082015181840152602081019050610198565b50505050905090810190601f1680156101e05780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34156101f957600080fd5b6102016103e7565b604051808215151515815260200191505060405180910390f35b341561022657600080fd5b61022e6103fa565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561027b57600080fd5b61028361041f565b005b60028054600181600116156101000203166002900480601f01602080910402602001604051908101604052809291908181526020018280546001816001161561010002031660029004801561031b5780601f106102f05761010080835404028352916020019161031b565b820191906000526020600020905b8154815290600101906020018083116102fe57829003601f168201915b505050505081565b600360019054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103df5780601f106103b4576101008083540402835291602001916103df565b820191906000526020600020905b8154815290600101906020018083116103c257829003601f168201915b505050505081565b600360009054906101000a900460ff1681565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156104f3576001600360006101000a81548160ff0219169083151502179055507fb6fa8b8bd5eab60f292eca876e3ef90722275b785309d84b1de113ce0b8c4e7433604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5600a165627a7a723058200be8ab5ca05b62c9320f861df2a1f1fb8a5880930a7eda4ade6489c2d84c43610029a165627a7a723058205fa00844ea434572acc22f310f1cbedb5769d4e199d898517339bab071d66de80029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom,  HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI );
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

        public Function GetFunctionAttributesKeys() {
            return contract.GetFunction("attributesKeys");
        }
        public Function GetFunctionKill() {
            return contract.GetFunction("kill");
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
        public Function GetFunctionOwner() {
            return contract.GetFunction("owner");
        }
        public Function GetFunctionAddAttribute() {
            return contract.GetFunction("addAttribute");
        }
        public Function GetFunctionChangeOwner() {
            return contract.GetFunction("changeOwner");
        }
        public Function GetFunctionAttributes() {
            return contract.GetFunction("attributes");
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

        public Event GetEventReturnCertificate() {
            return contract.GetEvent("ReturnCertificate");
        }

        public Task<byte[]> AttributesKeysAsyncCall(BigInteger a) {
            var function = GetFunctionAttributesKeys();
            return function.CallAsync<byte[]>(a);
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
        public Task<string> AttributesAsyncCall(byte[] a) {
            var function = GetFunctionAttributes();
            return function.CallAsync<string>(a);
        }
        public Task<BigInteger> AttributeCountAsyncCall() {
            var function = GetFunctionAttributeCount();
            return function.CallAsync<BigInteger>();
        }
        public Task<string> GetAttributeAsyncCall(byte[] key) {
            var function = GetFunctionGetAttribute();
            return function.CallAsync<string>(key);
        }

        public async Task<string> AddCertificateAsync(byte[] key, string cert, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddCertificate();
                string data = function.GetData(key, cert);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> KillAsync( HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionKill();
                string data = function.GetData();
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
        public async Task<string> AddCertificateAsync(string attr, string cert, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddCertificate();
                string data = function.GetData(attr, cert);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> AddAttributeAsync(string attr, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddAttribute();
                string data = function.GetData(attr);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> ChangeOwnerAsync(string newOwner, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionChangeOwner();
                string data = function.GetData(newOwner);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> RemoveAttributeAsync(byte[] key, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRemoveAttribute();
                string data = function.GetData(key);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }



    }


    public class ReturnCertificateEventDTO
    {
        [Parameter("address", "_issuingAddress", 1, true)]
        public string _issuingAddress {get; set;}

        [Parameter("address", "_associatedAttribute", 2, true)]
        public string _associatedAttribute {get; set;}

        [Parameter("address", "_certAddress", 3, false)]
        public string _certAddress {get; set;}

    }


}

