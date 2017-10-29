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

        public static string ABI = @"[{'constant':true,'inputs':[],'name':'hash','outputs':[{'name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'owningAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'location','outputs':[{'name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'revoked','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[],'name':'revoke','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owningAttribute','type':'address'}],'payable':false,'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':false,'name':'_sender','type':'address'}],'name':'Revoked','type':'event'}]";

        public static string BYTE_CODE = "0x60606040526000600360006101000a81548160ff021916908315150217905550341561002a57600080fd5b6040516106e73803806106e783398101604052808051820191906020018051820191906020018051906020019091905050336000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555082600190805190602001906100b1929190610112565b5081600290805190602001906100c8929190610112565b5080600360016101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505050506101b7565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061015357805160ff1916838001178555610181565b82800160010185558215610181579182015b82811115610180578251825591602001919060010190610165565b5b50905061018e9190610192565b5090565b6101b491905b808211156101b0576000816000905550600101610198565b5090565b90565b610521806101c66000396000f300606060405260043610610078576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806309bd5a601461007d578063232533b21461010b578063516f279e1461016057806363d256ce146101ee5780638da5cb5b1461021b578063b6549f7514610270575b600080fd5b341561008857600080fd5b610090610285565b6040518080602001828103825283818151815260200191508051906020019080838360005b838110156100d05780820151818401526020810190506100b5565b50505050905090810190601f1680156100fd5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b341561011657600080fd5b61011e610323565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561016b57600080fd5b610173610349565b6040518080602001828103825283818151815260200191508051906020019080838360005b838110156101b3578082015181840152602081019050610198565b50505050905090810190601f1680156101e05780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34156101f957600080fd5b6102016103e7565b604051808215151515815260200191505060405180910390f35b341561022657600080fd5b61022e6103fa565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561027b57600080fd5b61028361041f565b005b60028054600181600116156101000203166002900480601f01602080910402602001604051908101604052809291908181526020018280546001816001161561010002031660029004801561031b5780601f106102f05761010080835404028352916020019161031b565b820191906000526020600020905b8154815290600101906020018083116102fe57829003601f168201915b505050505081565b600360019054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103df5780601f106103b4576101008083540402835291602001916103df565b820191906000526020600020905b8154815290600101906020018083116103c257829003601f168201915b505050505081565b600360009054906101000a900460ff1681565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156104f3576001600360006101000a81548160ff0219169083151502179055507fb6fa8b8bd5eab60f292eca876e3ef90722275b785309d84b1de113ce0b8c4e7433604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5600a165627a7a723058200be8ab5ca05b62c9320f861df2a1f1fb8a5880930a7eda4ade6489c2d84c43610029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom, string _location, string _hash, string _owningAttribute, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI , _location, _hash, _owningAttribute);
            ITransactionService transactionService = new ConstantGasTransactionService(keyFrom, web3);
            return await transactionService.SignAndSendTransaction(data, "", new HexBigInteger(0), gasPrice);
        }

        private Contract contract;
        private ITransactionService _transactionService;

        public string GetAddress()
        {
            return contract.Address;
        }

        public CertificateService(Web3 web3, string key, string address)
        {
            this.web3 = web3;
            this._transactionService = new ConstantGasTransactionService(key, web3);
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

        public async Task<string> RevokeAsync( HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRevoke();
                string data = function.GetData();
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }



    }



}

