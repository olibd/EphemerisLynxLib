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

        public static string BYTE_CODE = "0x6060604052341561000c57fe5b60405162001e2c38038062001e2c833981016040528080518201919060200180519060200190919050505b81815b60005b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b60018351016002819055503373ffffffffffffffffffffffffffffffffffffffff1660036001610100811015156100b257fe5b0160005b5081905550600161010360003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002081905550600090505b825181101561019857828181518110151561010757fe5b9060200190602002015173ffffffffffffffffffffffffffffffffffffffff166003826002016101008110151561013a57fe5b0160005b5081905550806002016101036000858481518110151561015a57fe5b9060200190602002015173ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055505b8060010190506100f0565b816001819055505b5050505b50505b611c7580620001b76000396000f300606060405236156100ef576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680632e6f6285146100f15780633612d48e146101435780634123cb6b146101915780634dc2d492146101b75780636fd4502714610217578063746c91711461024d578063753ec10314610273578063797af6271461033a5780637a2cc6d31461037657806384105ee0146103cb5780638da5cb5b146104015780638fc1fa9814610453578063b75c7dc6146104c4578063b9e2bea0146104e8578063ba51a6df146105af578063c2cf7326146105cf578063c4d252f51461062a575bfe5b34156100f957fe5b610125600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610666565b60405180826000191660001916815260200191505060405180910390f35b341561014b57fe5b610177600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061068b565b604051808215151515815260200191505060405180910390f35b341561019957fe5b6101a16106c3565b6040518082815260200191505060405180910390f35b34156101bf57fe5b6101d560048080359060200190919050506106c9565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561021f57fe5b61024b600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506106ec565b005b341561025557fe5b61025d610858565b6040518082815260200191505060405180910390f35b341561027b57fe5b61028361085e565b604051808573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001826000191660001916815260200194505050505060405180910390f35b341561034257fe5b61035c6004808035600019169060200190919050506108dd565b604051808215151515815260200191505060405180910390f35b341561037e57fe5b6103c9600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610d4d565b005b34156103d357fe5b6103ff600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610f29565b005b341561040957fe5b61041161109e565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561045b57fe5b6104a6600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506110c4565b60405180826000191660001916815260200191505060405180910390f35b34156104cc57fe5b6104e6600480803560001916906020019091905050611376565b005b34156104f057fe5b6104f861148b565b604051808573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001826000191660001916815260200194505050505060405180910390f35b34156105b757fe5b6105cd600480803590602001909190505061152a565b005b34156105d757fe5b61061060048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506115db565b604051808215151515815260200191505060405180910390f35b341561063257fe5b61064c60048080356000191690602001909190505061165d565b604051808215151515815260200191505060405180910390f35b60006106713361068b565b15610685576106818260006110c4565b90505b5b5b919050565b6000600061010360008473ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020541190505b919050565b60025481565b6000600360018301610100811015156106de57fe5b0160005b505490505b919050565b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156108535761010360008373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020549050600081141561077f57610852565b600160025403600154111561079357610852565b6000600382610100811015156107a557fe5b0160005b5081905550600061010360008473ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055506107e6611706565b6107ee611795565b7f58619076adf5bb0943d100ef88d52d7c3fd691b19d3a9071b555b651fbf418da82604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5b5b5050565b60015481565b6101068060000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff16908060010160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff16908060020160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff16908060030154905084565b6000816108e9816118fc565b15610d46578260001916610106600301546000191614151561090b5760006000fd5b600061010660000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16141515610d3e57600061010660010160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff161415610a355761010660000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16637ca307b46040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050600060405180830381600087803b1515610a1f57fe5b6102c65a03f11515610a2d57fe5b505050610b2a565b61010660000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663a6f9dae161010660010160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff166040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050600060405180830381600087803b1515610b1857fe5b6102c65a03f11515610b2657fe5b5050505b7f019eddefe6ca3e65432d705ec0720044c000990026958d30082839685ac70b716101066003015461010660020160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff163361010660000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1661010660010160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff166040518086600019166000191681526020018573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019550505050505060405180910390a161010660006000820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff02191690556001820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff02191690556002820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff02191690556003820160009055505060019150610d45565b60006000fd5b5b5b5b50919050565b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610f2357610dae8261068b565b15610db857610f22565b61010360008473ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000205490506000811415610df357610f22565b610dfb611706565b8173ffffffffffffffffffffffffffffffffffffffff1660038261010081101515610e2257fe5b0160005b5081905550600061010360008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055508061010360008473ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055507fb532073b38c83145e3e5135377a08bf9aab55bc0fd7c1179cd4fb995d2a5159c8383604051808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019250505060405180910390a15b5b5b505050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141561109a57610f888161068b565b15610f9257611099565b610f9a611706565b60fa600254101515610faf57610fae611795565b5b60fa600254101515610fc057611099565b6002600081548092919060010191905055508073ffffffffffffffffffffffffffffffffffffffff16600360025461010081101515610ffb57fe5b0160005b508190555060025461010360008373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055507f994a936646fe87ffe4f1e469d3d6aa417d6b855598397f323de5b449f765f0c381604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5b5b50565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b600060006110d13361068b565b1561136e5760006001026101066003015460001916141561136057600036436040518084848082843782019150508281526020019350505050604051809103902090506080604051908101604052808573ffffffffffffffffffffffffffffffffffffffff1681526020018473ffffffffffffffffffffffffffffffffffffffff1681526020013373ffffffffffffffffffffffffffffffffffffffff168152602001826000191681525061010660008201518160000160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555060208201518160010160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555060408201518160020160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff160217905550606082015181600301906000191690559050507f3e9852196e48b690cdc5af7d6715b023a00382e7116220005bbbb07917247c17813386866040518085600019166000191681526020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200194505050505060405180910390a1611351610106600301546108dd565b5061010660030154915061136d565b6000600102915061136d565b5b5b5b5092915050565b60006000600061010360003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002054925060008314156113b757611485565b8260020a915061010460008560001916600019168152602001908152602001600020905060008282600101541611156114845780600001600081548092919060010191905055508181600101600082825403925050819055507fc7fb647e59b18047309aa15aad418e5d7ca96d173ad704f1031a2c3d7591734b3385604051808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200182600019166000191681526020019250505060405180910390a15b5b50505050565b6000600060006000600060006000600061010660000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1661010660010160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1661010660020160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff166101066003015497509750975097505b5050505090919293565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156115d75760025481111561158f576115d6565b8060018190555061159e611706565b7facbdb084c721332ac59f9b8e392196c9eb0e4932862da8eb9beaf0dad4f550da816040518082815260200191505060405180910390a15b5b5b50565b600060006000600061010460008760001916600019168152602001908152602001600020925061010360008673ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002054915060008214156116405760009350611654565b8160020a9050600081846001015416141593505b50505092915050565b60006116683361068b565b15611700578160001916610106600301546000191614151561168a5760006000fd5b3373ffffffffffffffffffffffffffffffffffffffff1661010660020160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1614156116f5576116ec611706565b600190506116ff565b600090506116ff565b5b5b5b919050565b61010660006000820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff02191690556001820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff02191690556002820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff021916905560038201600090555050611792611b14565b5b565b6000600190505b6002548110156118f8575b600254811080156117ce57506000600382610100811015156117c557fe5b0160005b505414155b156117e05780806001019150506117a7565b5b600160025411801561180a5750600060036002546101008110151561180257fe5b0160005b5054145b1561182757600260008154809291906001900391905055506117e1565b600254811080156118505750600060036002546101008110151561184757fe5b0160005b505414155b8015611871575060006003826101008110151561186957fe5b0160005b5054145b156118f35760036002546101008110151561188857fe5b0160005b50546003826101008110151561189e57fe5b0160005b5081905550806101036000600384610100811015156118bd57fe5b0160005b505481526020019081526020016000208190555060006003600254610100811015156118e957fe5b0160005b50819055505b61179c565b5b50565b600060006000600061010360003373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020549250600083141561193f57611b0c565b6101046000866000191660001916815260200190815260200160002091506000826000015414156119c95760015482600001819055506000826001018190555061010580548091906001016119949190611bd6565b82600201819055508461010583600201548154811015156119b157fe5b906000526020600020900160005b5081600019169055505b8260020a90506000818360010154161415611b0b577fe1c52dc63b719ade82e8bea94cc41a0d5d28e4aaf536adb5e9cccc9ff8c1aeda3386604051808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200182600019166000191681526020019250505060405180910390a160018260000154111515611ae2576101056101046000876000191660001916815260200190815260200160002060020154815481101515611a8e57fe5b906000526020600020900160005b5060009055610104600086600019166000191681526020019081526020016000206000600082016000905560018201600090556002820160009055505060019350611b0c565b8160000160008154809291906001900391905055508082600101600082825417925050819055505b5b5b505050919050565b60006000610105805490509150600090505b81811015611bc257600060010261010582815481101515611b4357fe5b906000526020600020900160005b505460001916141515611bb657610104600061010583815481101515611b7357fe5b906000526020600020900160005b505460001916600019168152602001908152602001600020600060008201600090556001820160009055600282016000905550505b5b806001019050611b26565b6101056000611bd19190611c02565b5b5050565b815481835581811511611bfd57818360005260206000209182019101611bfc9190611c24565b5b505050565b5080546000825590600052602060002090810190611c209190611c24565b5b50565b611c4691905b80821115611c42576000816000905550600101611c2a565b5090565b905600a165627a7a723058200c26f66817925211ce66f3bee1025f58bdae55a86ba83f741c02004203c887860029";

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

        public WatchdogService(Web3 web3, string key, string address)
        {
            this.web3 = web3;
            this._transactionService = new ConstantGasTransactionService(key, web3);
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

        public async Task<string> ProposeDeletionAsync(string _callDestination, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionProposeDeletion();
                string data = function.GetData(_callDestination);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
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
        public async Task<string> ConfirmAsync(byte[] _h, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionConfirm();
                string data = function.GetData(_h);
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
        public async Task<string> ProposeMigrationAsync(string _callDestination, string newOwner, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionProposeMigration();
                string data = function.GetData(_callDestination, newOwner);
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
        public async Task<string> CancelAsync(byte[] _h, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionCancel();
                string data = function.GetData(_h);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
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

