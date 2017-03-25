using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace eVi.abi.lib.pcl
{
   public class WatchdogService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'_callDestination','type':'address'}],'name':'proposeDeletion','outputs':[{'name':'_r','type':'bytes32'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_addr','type':'address'}],'name':'isMultisigOwner','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'m_numOwners','outputs':[{'name':'','type':'uint256'}],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'ownerIndex','type':'uint256'}],'name':'getMultisigOwner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_owner','type':'address'}],'name':'removeMultisigOwner','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'m_required','outputs':[{'name':'','type':'uint256'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'proposal','outputs':[{'name':'callDestination','type':'address'},{'name':'newOwner','type':'address'},{'name':'initiator','type':'address'},{'name':'hash','type':'bytes32'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_h','type':'bytes32'}],'name':'confirm','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_from','type':'address'},{'name':'_to','type':'address'}],'name':'changeMultisigOwner','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_owner','type':'address'}],'name':'addMultisigOwner','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_callDestination','type':'address'},{'name':'newOwner','type':'address'}],'name':'proposeMigration','outputs':[{'name':'','type':'bytes32'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_operation','type':'bytes32'}],'name':'revoke','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'getProposal','outputs':[{'name':'','type':'address'},{'name':'','type':'address'},{'name':'','type':'address'},{'name':'','type':'bytes32'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_newRequired','type':'uint256'}],'name':'changeRequirement','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'_operation','type':'bytes32'},{'name':'_owner','type':'address'}],'name':'hasConfirmed','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_h','type':'bytes32'}],'name':'cancel','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'inputs':[{'name':'_owners','type':'address[]'},{'name':'_required','type':'uint256'}],'payable':false,'type':'constructor'},{'anonymous':false,'inputs':[{'indexed':false,'name':'operation','type':'bytes32'},{'indexed':false,'name':'initiator','type':'address'},{'indexed':false,'name':'to','type':'address'},{'indexed':false,'name':'newOwner','type':'address'}],'name':'NewProposal','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'operation','type':'bytes32'},{'indexed':false,'name':'initiator','type':'address'},{'indexed':false,'name':'lastSignatory','type':'address'},{'indexed':false,'name':'to','type':'address'},{'indexed':false,'name':'newOwner','type':'address'}],'name':'ProposalConfirmed','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'owner','type':'address'},{'indexed':false,'name':'operation','type':'bytes32'}],'name':'Confirmation','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'owner','type':'address'},{'indexed':false,'name':'operation','type':'bytes32'}],'name':'Revoke','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'oldOwner','type':'address'},{'indexed':false,'name':'newOwner','type':'address'}],'name':'OwnerChanged','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'newOwner','type':'address'}],'name':'OwnerAdded','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'oldOwner','type':'address'}],'name':'OwnerRemoved','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'newRequirement','type':'uint256'}],'name':'RequirementChanged','type':'event'}]";

        private Contract contract;

        public WatchdogService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionProposeDeletion() {
            return contract.GetFunction("proposeDeletion");
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
        public Function GetFunctionProposal() {
            return contract.GetFunction("proposal");
        }
        public Function GetFunctionConfirm() {
            return contract.GetFunction("confirm");
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
        public Function GetFunctionProposeMigration() {
            return contract.GetFunction("proposeMigration");
        }
        public Function GetFunctionRevoke() {
            return contract.GetFunction("revoke");
        }
        public Function GetFunctionGetProposal() {
            return contract.GetFunction("getProposal");
        }
        public Function GetFunctionChangeRequirement() {
            return contract.GetFunction("changeRequirement");
        }
        public Function GetFunctionHasConfirmed() {
            return contract.GetFunction("hasConfirmed");
        }
        public Function GetFunctionCancel() {
            return contract.GetFunction("cancel");
        }

        public Event GetEventNewProposal() {
            return contract.GetEvent("NewProposal");
        }
        public Event GetEventProposalConfirmed() {
            return contract.GetEvent("ProposalConfirmed");
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

        public Task<byte[]> ProposeDeletionAsyncCall(string _callDestination) {
            var function = GetFunctionProposeDeletion();
            return function.CallAsync<byte[]>(_callDestination);
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
        public Task<bool> ConfirmAsyncCall(byte[] _h) {
            var function = GetFunctionConfirm();
            return function.CallAsync<bool>(_h);
        }
        public Task<string> OwnerAsyncCall() {
            var function = GetFunctionOwner();
            return function.CallAsync<string>();
        }
        public Task<byte[]> ProposeMigrationAsyncCall(string _callDestination, string newOwner) {
            var function = GetFunctionProposeMigration();
            return function.CallAsync<byte[]>(_callDestination, newOwner);
        }
        public Task<bool> HasConfirmedAsyncCall(byte[] _operation, string _owner) {
            var function = GetFunctionHasConfirmed();
            return function.CallAsync<bool>(_operation, _owner);
        }
        public Task<bool> CancelAsyncCall(byte[] _h) {
            var function = GetFunctionCancel();
            return function.CallAsync<bool>(_h);
        }

        public Task<string> ProposeDeletionAsync(string addressFrom, string _callDestination, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionProposeDeletion();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _callDestination);
        }
        public Task<string> IsMultisigOwnerAsync(string addressFrom, string _addr, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionIsMultisigOwner();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _addr);
        }
        public Task<string> RemoveMultisigOwnerAsync(string addressFrom, string _owner, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRemoveMultisigOwner();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _owner);
        }
        public Task<string> ConfirmAsync(string addressFrom, byte[] _h, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionConfirm();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _h);
        }
        public Task<string> ChangeMultisigOwnerAsync(string addressFrom, string _from, string _to, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionChangeMultisigOwner();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _from, _to);
        }
        public Task<string> AddMultisigOwnerAsync(string addressFrom, string _owner, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddMultisigOwner();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _owner);
        }
        public Task<string> ProposeMigrationAsync(string addressFrom, string _callDestination, string newOwner, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionProposeMigration();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _callDestination, newOwner);
        }
        public Task<string> RevokeAsync(string addressFrom, byte[] _operation, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRevoke();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _operation);
        }
        public Task<string> ChangeRequirementAsync(string addressFrom, BigInteger _newRequired, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionChangeRequirement();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _newRequired);
        }
        public Task<string> CancelAsync(string addressFrom, byte[] _h, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionCancel();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _h);
        }

        public Task<ProposalDTO> ProposalAsyncCall() {
            var function = GetFunctionProposal();
            return function.CallDeserializingToObjectAsync<ProposalDTO>();
        }
        public Task<GetProposalDTO> GetProposalAsyncCall() {
            var function = GetFunctionGetProposal();
            return function.CallDeserializingToObjectAsync<GetProposalDTO>();
        }


    }

    [FunctionOutput]
    public class ProposalDTO
    {
        [Parameter("address", "callDestination", 1)]
        public string CallDestination {get; set;}

        [Parameter("address", "newOwner", 2)]
        public string NewOwner {get; set;}

        [Parameter("address", "initiator", 3)]
        public string Initiator {get; set;}

        [Parameter("bytes32", "hash", 4)]
        public byte[] Hash {get; set;}

    }

    [FunctionOutput]
    public class GetProposalDTO
    {
        [Parameter("address", "", 1)]
        public string A {get; set;}

        [Parameter("address", "", 2)]
        public string B {get; set;}

        [Parameter("address", "", 3)]
        public string C {get; set;}

        [Parameter("bytes32", "", 4)]
        public byte[] D {get; set;}

    }


    public class NewProposalEventDTO
    {
        [Parameter("bytes32", "operation", 1, false)]
        public byte[] Operation {get; set;}

        [Parameter("address", "initiator", 2, false)]
        public string Initiator {get; set;}

        [Parameter("address", "to", 3, false)]
        public string To {get; set;}

        [Parameter("address", "newOwner", 4, false)]
        public string NewOwner {get; set;}

    }

    public class ProposalConfirmedEventDTO
    {
        [Parameter("bytes32", "operation", 1, false)]
        public byte[] Operation {get; set;}

        [Parameter("address", "initiator", 2, false)]
        public string Initiator {get; set;}

        [Parameter("address", "lastSignatory", 3, false)]
        public string LastSignatory {get; set;}

        [Parameter("address", "to", 4, false)]
        public string To {get; set;}

        [Parameter("address", "newOwner", 5, false)]
        public string NewOwner {get; set;}

    }


}

