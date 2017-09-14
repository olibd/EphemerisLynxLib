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

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'_addr','type':'address'}],'name':'isMultisigOwner','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'m_numOwners','outputs':[{'name':'','type':'uint256'}],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'ownerIndex','type':'uint256'}],'name':'getMultisigOwner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_owner','type':'address'}],'name':'removeMultisigOwner','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'m_required','outputs':[{'name':'','type':'uint256'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_from','type':'address'},{'name':'_to','type':'address'}],'name':'changeMultisigOwner','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_owner','type':'address'}],'name':'addMultisigOwner','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_operation','type':'bytes32'}],'name':'revoke','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_newRequired','type':'uint256'}],'name':'changeRequirement','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'_operation','type':'bytes32'},{'name':'_owner','type':'address'}],'name':'hasConfirmed','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'inputs':[{'name':'_owners','type':'address[]'},{'name':'_required','type':'uint256'}],'payable':false,'type':'constructor'},{'anonymous':false,'inputs':[{'indexed':false,'name':'owner','type':'address'},{'indexed':false,'name':'operation','type':'bytes32'}],'name':'Confirmation','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'owner','type':'address'},{'indexed':false,'name':'operation','type':'bytes32'}],'name':'Revoke','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'oldOwner','type':'address'},{'indexed':false,'name':'newOwner','type':'address'}],'name':'OwnerChanged','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'newOwner','type':'address'}],'name':'OwnerAdded','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'oldOwner','type':'address'}],'name':'OwnerRemoved','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'newRequirement','type':'uint256'}],'name':'RequirementChanged','type':'event'}]";

        public static string BYTE_CODE = "0x6060604052341561000c57fe5b604051610f37380380610f37833981016040528080518201919060200180519060200190919050505b60005b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b60018351016002819055503373ffffffffffffffffffffffffffffffffffffffff1660036001610100811015156100ad57fe5b0160005b5081905550600161010360003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002081905550600090505b825181101561019357828181518110151561010257fe5b9060200190602002015173ffffffffffffffffffffffffffffffffffffffff166003826002016101008110151561013557fe5b0160005b5081905550806002016101036000858481518110151561015557fe5b9060200190602002015173ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055505b8060010190506100eb565b816001819055505b5050505b610d89806101ae6000396000f300606060405236156100ad576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680633612d48e146100af5780634123cb6b146100fd5780634dc2d492146101235780636fd4502714610183578063746c9171146101b95780637a2cc6d3146101df57806384105ee0146102345780638da5cb5b1461026a578063b75c7dc6146102bc578063ba51a6df146102e0578063c2cf732614610300575bfe5b34156100b757fe5b6100e3600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061035b565b604051808215151515815260200191505060405180910390f35b341561010557fe5b61010d610393565b6040518082815260200191505060405180910390f35b341561012b57fe5b6101416004808035906020019091905050610399565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561018b57fe5b6101b7600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506103bc565b005b34156101c157fe5b6101c9610528565b6040518082815260200191505060405180910390f35b34156101e757fe5b610232600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061052e565b005b341561023c57fe5b610268600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061070a565b005b341561027257fe5b61027a61087f565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156102c457fe5b6102de6004808035600019169060200190919050506108a5565b005b34156102e857fe5b6102fe60048080359060200190919050506109ba565b005b341561030857fe5b61034160048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610a6b565b604051808215151515815260200191505060405180910390f35b6000600061010360008473ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020541190505b919050565b60025481565b6000600360018301610100811015156103ae57fe5b0160005b505490505b919050565b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156105235761010360008373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020549050600081141561044f57610522565b600160025403600154111561046357610522565b60006003826101008110151561047557fe5b0160005b5081905550600061010360008473ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055506104b6610aed565b6104be610baf565b7f58619076adf5bb0943d100ef88d52d7c3fd691b19d3a9071b555b651fbf418da82604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5b5b5050565b60015481565b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156107045761058f8261035b565b1561059957610703565b61010360008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002054905060008114156105d457610703565b6105dc610aed565b8173ffffffffffffffffffffffffffffffffffffffff166003826101008110151561060357fe5b0160005b5081905550600061010360008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055508061010360008473ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055507fb532073b38c83145e3e5135377a08bf9aab55bc0fd7c1179cd4fb995d2a5159c8383604051808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019250505060405180910390a15b5b5b505050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141561087b576107698161035b565b156107735761087a565b61077b610aed565b60fa6002541015156107905761078f610baf565b5b60fa6002541015156107a15761087a565b6002600081548092919060010191905055508073ffffffffffffffffffffffffffffffffffffffff166003600254610100811015156107dc57fe5b0160005b508190555060025461010360008373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055507f994a936646fe87ffe4f1e469d3d6aa417d6b855598397f323de5b449f765f0c381604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5b5b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60006000600061010360003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002054925060008314156108e6576109b4565b8260020a915061010460008560001916600019168152602001908152602001600020905060008282600101541611156109b35780600001600081548092919060010191905055508181600101600082825403925050819055507fc7fb647e59b18047309aa15aad418e5d7ca96d173ad704f1031a2c3d7591734b3385604051808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200182600019166000191681526020019250505060405180910390a15b5b50505050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610a6757600254811115610a1f57610a66565b80600181905550610a2e610aed565b7facbdb084c721332ac59f9b8e392196c9eb0e4932862da8eb9beaf0dad4f550da816040518082815260200191505060405180910390a15b5b5b50565b600060006000600061010460008760001916600019168152602001908152602001600020925061010360008673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000205491506000821415610ad05760009350610ae4565b8160020a9050600081846001015416141593505b50505092915050565b60006000610105805490509150600090505b81811015610b9b57600060010261010582815481101515610b1c57fe5b906000526020600020900160005b505460001916141515610b8f57610104600061010583815481101515610b4c57fe5b906000526020600020900160005b505460001916600019168152602001908152602001600020600060008201600090556001820160009055600282016000905550505b5b806001019050610aff565b6101056000610baa9190610d16565b5b5050565b6000600190505b600254811015610d12575b60025481108015610be85750600060038261010081101515610bdf57fe5b0160005b505414155b15610bfa578080600101915050610bc1565b5b6001600254118015610c2457506000600360025461010081101515610c1c57fe5b0160005b5054145b15610c415760026000815480929190600190039190505550610bfb565b60025481108015610c6a57506000600360025461010081101515610c6157fe5b0160005b505414155b8015610c8b5750600060038261010081101515610c8357fe5b0160005b5054145b15610d0d57600360025461010081101515610ca257fe5b0160005b505460038261010081101515610cb857fe5b0160005b508190555080610103600060038461010081101515610cd757fe5b0160005b50548152602001908152602001600020819055506000600360025461010081101515610d0357fe5b0160005b50819055505b610bb6565b5b50565b5080546000825590600052602060002090810190610d349190610d38565b5b50565b610d5a91905b80821115610d56576000816000905550600101610d3e565b5090565b905600a165627a7a7230582029c7a5863909aeef7242ab58e7937067a3a2856f39f9a28c4620b301de7b1b450029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom, string[] _owners, BigInteger _required, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI , _owners, _required);
            TransactionService transactionService = new TransactionService(keyFrom, web3);
            return await transactionService.SignAndSendTransaction(data, "", new HexBigInteger(0), gasPrice);
        }

        private Contract contract;
        private TransactionService _transactionService;

        public string GetAddress()
        {
            return contract.Address;
        }

        public MultiOwnedService(Web3 web3, string key, string address)
        {
            this.web3 = web3;
            this._transactionService = new TransactionService(key, web3);
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

