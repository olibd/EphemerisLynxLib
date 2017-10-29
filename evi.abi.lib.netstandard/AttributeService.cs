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

        public static string ABI = @"[{'constant':true,'inputs':[{'name':'','type':'address'}],'name':'certificates','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'hash','outputs':[{'name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'name':'','type':'uint256'}],'name':'certificateKeys','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'location','outputs':[{'name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'certificateCount','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'description','outputs':[{'name':'','type':'bytes32'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'name':'_issuer','type':'address'}],'name':'getCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'inputs':[{'name':'_location','type':'string'},{'name':'_description','type':'bytes32'},{'name':'_hash','type':'string'},{'name':'_owner','type':'address'}],'payable':false,'stateMutability':'nonpayable','type':'constructor'}]";

        public static string BYTE_CODE = "0x6060604052341561000f57600080fd5b604051610b87380380610b8783398101604052808051820191906020018051906020019091908051820191906020018051906020019091905050336000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff160217905550836001908051906020019061009f92919061010b565b5081600290805190602001906100b692919061010b565b50806000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055508260058160001916905550505050506101b0565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061014c57805160ff191683800117855561017a565b8280016001018555821561017a579182015b8281111561017957825182559160200191906001019061015e565b5b509050610187919061018b565b5090565b6101ad91905b808211156101a9576000816000905550600101610191565b5090565b90565b6109c8806101bf6000396000f300606060405260043610610098576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806216e5261461009d57806309bd5a60146101165780634fab056b146101a4578063516f279e1461020757806352d7b89c146102955780637284e416146102be5780637c6ebde9146102ef5780638da5cb5b14610328578063fd531e931461037d575b600080fd5b34156100a857600080fd5b6100d4600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506103f6565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561012157600080fd5b610129610429565b6040518080602001828103825283818151815260200191508051906020019080838360005b8381101561016957808201518184015260208101905061014e565b50505050905090810190601f1680156101965780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34156101af57600080fd5b6101c560048080359060200190919050506104c7565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561021257600080fd5b61021a610506565b6040518080602001828103825283818151815260200191508051906020019080838360005b8381101561025a57808201518184015260208101905061023f565b50505050905090810190601f1680156102875780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34156102a057600080fd5b6102a86105a4565b6040518082815260200191505060405180910390f35b34156102c957600080fd5b6102d16105b1565b60405180826000191660001916815260200191505060405180910390f35b34156102fa57600080fd5b610326600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506105b7565b005b341561033357600080fd5b61033b6108bd565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561038857600080fd5b6103b4600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506108e2565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b60036020528060005260406000206000915054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60028054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156104bf5780601f10610494576101008083540402835291602001916104bf565b820191906000526020600020905b8154815290600101906020018083116104a257829003601f168201915b505050505081565b6004818154811015156104d657fe5b90600052602060002090016000915054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60018054600181600116156101000203166002900480601f01602080910402602001604051908101604052809291908181526020018280546001816001161561010002031660029004801561059c5780601f106105715761010080835404028352916020019161059c565b820191906000526020600020905b81548152906001019060200180831161057f57829003601f168201915b505050505081565b6000600480549050905090565b60055481565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156108ba573073ffffffffffffffffffffffffffffffffffffffff168173ffffffffffffffffffffffffffffffffffffffff1663232533b26000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401602060405180830381600087803b151561068f57600080fd5b6102c65a03f115156106a057600080fd5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff161415156106cc57600080fd5b80600360008373ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401602060405180830381600087803b151561073d57600080fd5b6102c65a03f1151561074e57600080fd5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff160217905550600480548060010182816107e4919061094b565b916000526020600020900160008373ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401602060405180830381600087803b151561085d57600080fd5b6102c65a03f1151561086e57600080fd5b50505060405180519050909190916101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff160217905550505b50565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b6000600360008373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff169050919050565b815481835581811511610972578183600052602060002091820191016109719190610977565b5b505050565b61099991905b8082111561099557600081600090555060010161097d565b5090565b905600a165627a7a723058203321aea97595f8ee37ae22864d102141168be0ed07eaa538361ced0c11b699990029";

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
        public Task<byte[]> DescriptionAsyncCall() {
            var function = GetFunctionDescription();
            return function.CallAsync<byte[]>();
        }
        public Task<string> OwnerAsyncCall() {
            var function = GetFunctionOwner();
            return function.CallAsync<string>();
        }
        public Task<string> GetCertificateAsyncCall(string _issuer) {
            var function = GetFunctionGetCertificate();
            return function.CallAsync<string>(_issuer);
        }

        public async Task<string> AddCertificateAsync(string _cert, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddCertificate();
                string data = function.GetData(_cert);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }



    }



}

