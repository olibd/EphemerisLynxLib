using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace eVi.abi.lib.pcl
{
   public class MultiOwnedService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'_addr','type':'address'}],'name':'isMultisigOwner','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'m_numOwners','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'name':'ownerIndex','type':'uint256'}],'name':'getMultisigOwner','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_owner','type':'address'}],'name':'removeMultisigOwner','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'m_required','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_from','type':'address'},{'name':'_to','type':'address'}],'name':'changeMultisigOwner','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'_owner','type':'address'}],'name':'addMultisigOwner','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_operation','type':'bytes32'}],'name':'revoke','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'_newRequired','type':'uint256'}],'name':'changeRequirement','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'name':'_operation','type':'bytes32'},{'name':'_owner','type':'address'}],'name':'hasConfirmed','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'inputs':[{'name':'_owners','type':'address[]'},{'name':'_required','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':false,'name':'owner','type':'address'},{'indexed':false,'name':'operation','type':'bytes32'}],'name':'Confirmation','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'owner','type':'address'},{'indexed':false,'name':'operation','type':'bytes32'}],'name':'Revoke','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'oldOwner','type':'address'},{'indexed':false,'name':'newOwner','type':'address'}],'name':'OwnerChanged','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'newOwner','type':'address'}],'name':'OwnerAdded','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'oldOwner','type':'address'}],'name':'OwnerRemoved','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'newRequirement','type':'uint256'}],'name':'RequirementChanged','type':'event'}]";

        public static string BYTE_CODE = "0x6060604052341561000f57600080fd5b604051610f04380380610f04833981016040528080518201919060200180519060200190919050506000336000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555060018351016002819055503373ffffffffffffffffffffffffffffffffffffffff1660036001610100811015156100ac57fe5b0181905550600161010360003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002081905550600090505b82518110156101895782818151811015156100fd57fe5b9060200190602002015173ffffffffffffffffffffffffffffffffffffffff166003826002016101008110151561013057fe5b0181905550806002016101036000858481518110151561014c57fe5b9060200190602002015173ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055508060010190506100e6565b81600181905550505050610d62806101a26000396000f3006060604052600436106100af576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680633612d48e146100b45780634123cb6b146101055780634dc2d4921461012e5780636fd4502714610191578063746c9171146101ca5780637a2cc6d3146101f357806384105ee01461024b5780638da5cb5b14610284578063b75c7dc6146102d9578063ba51a6df14610300578063c2cf732614610323575b600080fd5b34156100bf57600080fd5b6100eb600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610381565b604051808215151515815260200191505060405180910390f35b341561011057600080fd5b6101186103b7565b6040518082815260200191505060405180910390f35b341561013957600080fd5b61014f60048080359060200190919050506103bd565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561019c57600080fd5b6101c8600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506103db565b005b34156101d557600080fd5b6101dd610541565b6040518082815260200191505060405180910390f35b34156101fe57600080fd5b610249600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610547565b005b341561025657600080fd5b610282600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061071d565b005b341561028f57600080fd5b61029761088c565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156102e457600080fd5b6102fe6004808035600019169060200190919050506108b1565b005b341561030b57600080fd5b61032160048080359060200190919050506109c5565b005b341561032e57600080fd5b61036760048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610a74565b604051808215151515815260200191505060405180910390f35b60008061010360008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002054119050919050565b60025481565b6000600360018301610100811015156103d257fe5b01549050919050565b60008060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141561053d5761010360008373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020549050600081141561046d5761053c565b60016002540360015411156104815761053c565b60006003826101008110151561049357fe5b0181905550600061010360008473ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055506104d0610af4565b6104d8610baa565b7f58619076adf5bb0943d100ef88d52d7c3fd691b19d3a9071b555b651fbf418da82604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5b5050565b60015481565b60008060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610718576105a782610381565b156105b157610717565b61010360008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002054905060008114156105ec57610717565b6105f4610af4565b8173ffffffffffffffffffffffffffffffffffffffff166003826101008110151561061b57fe5b0181905550600061010360008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055508061010360008473ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055507fb532073b38c83145e3e5135377a08bf9aab55bc0fd7c1179cd4fb995d2a5159c8383604051808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019250505060405180910390a15b5b505050565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156108895761077b81610381565b1561078557610888565b61078d610af4565b60fa6002541015156107a2576107a1610baa565b5b60fa6002541015156107b357610888565b6002600081548092919060010191905055508073ffffffffffffffffffffffffffffffffffffffff166003600254610100811015156107ee57fe5b018190555060025461010360008373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055507f994a936646fe87ffe4f1e469d3d6aa417d6b855598397f323de5b449f765f0c381604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5b50565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b600080600061010360003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002054925060008314156108f1576109bf565b8260020a915061010460008560001916600019168152602001908152602001600020905060008282600101541611156109be5780600001600081548092919060010191905055508181600101600082825403925050819055507fc7fb647e59b18047309aa15aad418e5d7ca96d173ad704f1031a2c3d7591734b3385604051808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200182600019166000191681526020019250505060405180910390a15b5b50505050565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610a7157600254811115610a2957610a70565b80600181905550610a38610af4565b7facbdb084c721332ac59f9b8e392196c9eb0e4932862da8eb9beaf0dad4f550da816040518082815260200191505060405180910390a15b5b50565b60008060008061010460008760001916600019168152602001908152602001600020925061010360008673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000205491506000821415610ad75760009350610aeb565b8160020a9050600081846001015416141593505b50505092915050565b600080610105805490509150600090505b81811015610b9757600060010261010582815481101515610b2257fe5b90600052602060002090015460001916141515610b8c57610104600061010583815481101515610b4e57fe5b906000526020600020900154600019166000191681526020019081526020016000206000808201600090556001820160009055600282016000905550505b806001019050610b05565b6101056000610ba69190610cf0565b5050565b6000600190505b600254811015610ced575b60025481108015610bdf5750600060038261010081101515610bda57fe5b015414155b15610bf1578080600101915050610bbc565b5b6001600254118015610c1757506000600360025461010081101515610c1357fe5b0154145b15610c345760026000815480929190600190039190505550610bf2565b60025481108015610c5957506000600360025461010081101515610c5457fe5b015414155b8015610c765750600060038261010081101515610c7257fe5b0154145b15610ce857600360025461010081101515610c8d57fe5b015460038261010081101515610c9f57fe5b018190555080610103600060038461010081101515610cba57fe5b01548152602001908152602001600020819055506000600360025461010081101515610ce257fe5b01819055505b610bb1565b50565b5080546000825590600052602060002090810190610d0e9190610d11565b50565b610d3391905b80821115610d2f576000816000905550600101610d17565b5090565b905600a165627a7a7230582072b070d0e72c51cc7b9de8ff314c054665f798d05cb7a1733d5904871f96c0210029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom, string[] _owners, BigInteger _required, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI , _owners, _required);
            ITransactionService transactionService = new ConstantGasTransactionService(keyFrom, web3);
            return await transactionService.SignAndSendTransaction(data, "", new HexBigInteger(0), gasPrice);
        }

        private Contract contract;
        private ITransactionService _transactionService;

        public string GetAddress()
        {
            return contract.Address;
        }

        public MultiOwnedService(Web3 web3, string key, string address)
        {
            this.web3 = web3;
            this._transactionService = new ConstantGasTransactionService(key, web3);
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionIsMultisigOwner() {
            return contract.GetFunction("isMultisigOwner");
        }
        public Function GetFunctionM_numOwners() {
            return contract.GetFunction("m_numOwners");
        }
        public Function GetFunctionGetMultisigOwner() {
            return contract.GetFunction("getMultisigOwner");
        }
        public Function GetFunctionRemoveMultisigOwner() {
            return contract.GetFunction("removeMultisigOwner");
        }
        public Function GetFunctionM_required() {
            return contract.GetFunction("m_required");
        }
        public Function GetFunctionChangeMultisigOwner() {
            return contract.GetFunction("changeMultisigOwner");
        }
        public Function GetFunctionAddMultisigOwner() {
            return contract.GetFunction("addMultisigOwner");
        }
        public Function GetFunctionOwner() {
            return contract.GetFunction("owner");
        }
        public Function GetFunctionRevoke() {
            return contract.GetFunction("revoke");
        }
        public Function GetFunctionChangeRequirement() {
            return contract.GetFunction("changeRequirement");
        }
        public Function GetFunctionHasConfirmed() {
            return contract.GetFunction("hasConfirmed");
        }

        public Event GetEventConfirmation() {
            return contract.GetEvent("Confirmation");
        }
        public Event GetEventRevoke() {
            return contract.GetEvent("Revoke");
        }
        public Event GetEventOwnerChanged() {
            return contract.GetEvent("OwnerChanged");
        }
        public Event GetEventOwnerAdded() {
            return contract.GetEvent("OwnerAdded");
        }
        public Event GetEventOwnerRemoved() {
            return contract.GetEvent("OwnerRemoved");
        }
        public Event GetEventRequirementChanged() {
            return contract.GetEvent("RequirementChanged");
        }

        public Task<bool> IsMultisigOwnerAsyncCall(string _addr) {
            var function = GetFunctionIsMultisigOwner();
            return function.CallAsync<bool>(_addr);
        }
        public Task<BigInteger> M_numOwnersAsyncCall() {
            var function = GetFunctionM_numOwners();
            return function.CallAsync<BigInteger>();
        }
        public Task<string> GetMultisigOwnerAsyncCall(BigInteger ownerIndex) {
            var function = GetFunctionGetMultisigOwner();
            return function.CallAsync<string>(ownerIndex);
        }
        public Task<BigInteger> M_requiredAsyncCall() {
            var function = GetFunctionM_required();
            return function.CallAsync<BigInteger>();
        }
        public Task<string> OwnerAsyncCall() {
            var function = GetFunctionOwner();
            return function.CallAsync<string>();
        }
        public Task<bool> HasConfirmedAsyncCall(byte[] _operation, string _owner) {
            var function = GetFunctionHasConfirmed();
            return function.CallAsync<bool>(_operation, _owner);
        }

        public async Task<string> IsMultisigOwnerAsync(string _addr, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionIsMultisigOwner();
                string data = function.GetData(_addr);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> RemoveMultisigOwnerAsync(string _owner, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRemoveMultisigOwner();
                string data = function.GetData(_owner);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> ChangeMultisigOwnerAsync(string _from, string _to, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionChangeMultisigOwner();
                string data = function.GetData(_from, _to);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> AddMultisigOwnerAsync(string _owner, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddMultisigOwner();
                string data = function.GetData(_owner);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> RevokeAsync(byte[] _operation, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRevoke();
                string data = function.GetData(_operation);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> ChangeRequirementAsync(BigInteger _newRequired, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionChangeRequirement();
                string data = function.GetData(_newRequired);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }



    }


    public class ConfirmationEventDTO
    {
        [Parameter("address", "owner", 1, false)]
        public string Owner {get; set;}

        [Parameter("bytes32", "operation", 2, false)]
        public byte[] Operation {get; set;}

    }

    public class RevokeEventDTO
    {
        [Parameter("address", "owner", 1, false)]
        public string Owner {get; set;}

        [Parameter("bytes32", "operation", 2, false)]
        public byte[] Operation {get; set;}

    }

    public class OwnerChangedEventDTO
    {
        [Parameter("address", "oldOwner", 1, false)]
        public string OldOwner {get; set;}

        [Parameter("address", "newOwner", 2, false)]
        public string NewOwner {get; set;}

    }


}

