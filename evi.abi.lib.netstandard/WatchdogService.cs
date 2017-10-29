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

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'_callDestination','type':'address'}],'name':'proposeDeletion','outputs':[{'name':'_r','type':'bytes32'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'_addr','type':'address'}],'name':'isMultisigOwner','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'m_numOwners','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'name':'ownerIndex','type':'uint256'}],'name':'getMultisigOwner','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_owner','type':'address'}],'name':'removeMultisigOwner','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'m_required','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'proposal','outputs':[{'name':'callDestination','type':'address'},{'name':'newOwner','type':'address'},{'name':'initiator','type':'address'},{'name':'hash','type':'bytes32'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_h','type':'bytes32'}],'name':'confirm','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'_from','type':'address'},{'name':'_to','type':'address'}],'name':'changeMultisigOwner','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'_owner','type':'address'}],'name':'addMultisigOwner','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_callDestination','type':'address'},{'name':'newOwner','type':'address'}],'name':'proposeMigration','outputs':[{'name':'','type':'bytes32'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'_operation','type':'bytes32'}],'name':'revoke','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'getProposal','outputs':[{'name':'','type':'address'},{'name':'','type':'address'},{'name':'','type':'address'},{'name':'','type':'bytes32'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_newRequired','type':'uint256'}],'name':'changeRequirement','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'name':'_operation','type':'bytes32'},{'name':'_owner','type':'address'}],'name':'hasConfirmed','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_h','type':'bytes32'}],'name':'cancel','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'},{'inputs':[{'name':'_owners','type':'address[]'},{'name':'_required','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':false,'name':'operation','type':'bytes32'},{'indexed':false,'name':'initiator','type':'address'},{'indexed':false,'name':'to','type':'address'},{'indexed':false,'name':'newOwner','type':'address'}],'name':'NewProposal','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'operation','type':'bytes32'},{'indexed':false,'name':'initiator','type':'address'},{'indexed':false,'name':'lastSignatory','type':'address'},{'indexed':false,'name':'to','type':'address'},{'indexed':false,'name':'newOwner','type':'address'}],'name':'ProposalConfirmed','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'owner','type':'address'},{'indexed':false,'name':'operation','type':'bytes32'}],'name':'Confirmation','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'owner','type':'address'},{'indexed':false,'name':'operation','type':'bytes32'}],'name':'Revoke','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'oldOwner','type':'address'},{'indexed':false,'name':'newOwner','type':'address'}],'name':'OwnerChanged','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'newOwner','type':'address'}],'name':'OwnerAdded','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'oldOwner','type':'address'}],'name':'OwnerRemoved','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'newRequirement','type':'uint256'}],'name':'RequirementChanged','type':'event'}]";

        public static string BYTE_CODE = "0x6060604052341561000f57600080fd5b604051611de7380380611de78339810160405280805182019190602001805190602001909190505081816000336000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555060018351016002819055503373ffffffffffffffffffffffffffffffffffffffff1660036001610100811015156100ae57fe5b0181905550600161010360003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002081905550600090505b825181101561018b5782818151811015156100ff57fe5b9060200190602002015173ffffffffffffffffffffffffffffffffffffffff166003826002016101008110151561013257fe5b0181905550806002016101036000858481518110151561014e57fe5b9060200190602002015173ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055508060010190506100e8565b816001819055505050505050611c41806101a66000396000f3006060604052600436106100f1576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680632e6f6285146100f65780633612d48e1461014b5780634123cb6b1461019c5780634dc2d492146101c55780636fd4502714610228578063746c917114610261578063753ec1031461028a578063797af627146103545780637a2cc6d31461039357806384105ee0146103eb5780638da5cb5b146104245780638fc1fa9814610479578063b75c7dc6146104ed578063b9e2bea014610514578063ba51a6df146105de578063c2cf732614610601578063c4d252f51461065f575b600080fd5b341561010157600080fd5b61012d600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061069e565b60405180826000191660001916815260200191505060405180910390f35b341561015657600080fd5b610182600480803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506106c1565b604051808215151515815260200191505060405180910390f35b34156101a757600080fd5b6101af6106f7565b6040518082815260200191505060405180910390f35b34156101d057600080fd5b6101e660048080359060200190919050506106fd565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561023357600080fd5b61025f600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061071b565b005b341561026c57600080fd5b610274610881565b6040518082815260200191505060405180910390f35b341561029557600080fd5b61029d610887565b604051808573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001826000191660001916815260200194505050505060405180910390f35b341561035f57600080fd5b610379600480803560001916906020019091905050610906565b604051808215151515815260200191505060405180910390f35b341561039e57600080fd5b6103e9600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610d7a565b005b34156103f657600080fd5b610422600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610f50565b005b341561042f57600080fd5b6104376110bf565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561048457600080fd5b6104cf600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506110e4565b60405180826000191660001916815260200191505060405180910390f35b34156104f857600080fd5b61051260048080356000191690602001909190505061138f565b005b341561051f57600080fd5b6105276114a3565b604051808573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001826000191660001916815260200194505050505060405180910390f35b34156105e957600080fd5b6105ff600480803590602001909190505061153d565b005b341561060c57600080fd5b61064560048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506115ec565b604051808215151515815260200191505060405180910390f35b341561066a57600080fd5b61068460048080356000191690602001909190505061166c565b604051808215151515815260200191505060405180910390f35b60006106a9336106c1565b156106bc576106b98260006110e4565b90505b919050565b60008061010360008473ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002054119050919050565b60025481565b60006003600183016101008110151561071257fe5b01549050919050565b60008060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141561087d5761010360008373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002054905060008114156107ad5761087c565b60016002540360015411156107c15761087c565b6000600382610100811015156107d357fe5b0181905550600061010360008473ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000208190555061081061170e565b61081861179b565b7f58619076adf5bb0943d100ef88d52d7c3fd691b19d3a9071b555b651fbf418da82604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5b5050565b60015481565b6101068060000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff16908060010160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff16908060020160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff16908060030154905084565b600081610912816118e1565b15610d74578260001916610106600301546000191614151561093357600080fd5b600061010660000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16141515610d6e57600061010660010160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff161415610a605761010660000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16637ca307b46040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401600060405180830381600087803b1515610a4757600080fd5b6102c65a03f11515610a5857600080fd5b505050610b5b565b61010660000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1663a6f9dae161010660010160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff166040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050600060405180830381600087803b1515610b4657600080fd5b6102c65a03f11515610b5757600080fd5b5050505b7f019eddefe6ca3e65432d705ec0720044c000990026958d30082839685ac70b716101066003015461010660020160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff163361010660000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1661010660010160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff166040518086600019166000191681526020018573ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019550505050505060405180910390a1610106600080820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff02191690556001820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff02191690556002820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff02191690556003820160009055505060019150610d73565b600080fd5b5b50919050565b60008060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610f4b57610dda826106c1565b15610de457610f4a565b61010360008473ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000205490506000811415610e1f57610f4a565b610e2761170e565b8173ffffffffffffffffffffffffffffffffffffffff1660038261010081101515610e4e57fe5b0181905550600061010360008573ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055508061010360008473ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055507fb532073b38c83145e3e5135377a08bf9aab55bc0fd7c1179cd4fb995d2a5159c8383604051808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019250505060405180910390a15b5b505050565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156110bc57610fae816106c1565b15610fb8576110bb565b610fc061170e565b60fa600254101515610fd557610fd461179b565b5b60fa600254101515610fe6576110bb565b6002600081548092919060010191905055508073ffffffffffffffffffffffffffffffffffffffff1660036002546101008110151561102157fe5b018190555060025461010360008373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020819055507f994a936646fe87ffe4f1e469d3d6aa417d6b855598397f323de5b449f765f0c381604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5b50565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b6000806110f0336106c1565b156113885760006001026101066003015460001916141561137f57600036436040518084848082843782019150508281526020019350505050604051809103902090506080604051908101604052808573ffffffffffffffffffffffffffffffffffffffff1681526020018473ffffffffffffffffffffffffffffffffffffffff1681526020013373ffffffffffffffffffffffffffffffffffffffff168152602001826000191681525061010660008201518160000160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555060208201518160010160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff16021790555060408201518160020160006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff160217905550606082015181600301906000191690559050507f3e9852196e48b690cdc5af7d6715b023a00382e7116220005bbbb07917247c17813386866040518085600019166000191681526020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200194505050505060405180910390a161137061010660030154610906565b50610106600301549150611387565b600060010291505b5b5092915050565b600080600061010360003373ffffffffffffffffffffffffffffffffffffffff16815260200190815260200160002054925060008314156113cf5761149d565b8260020a9150610104600085600019166000191681526020019081526020016000209050600082826001015416111561149c5780600001600081548092919060010191905055508181600101600082825403925050819055507fc7fb647e59b18047309aa15aad418e5d7ca96d173ad704f1031a2c3d7591734b3385604051808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200182600019166000191681526020019250505060405180910390a15b5b50505050565b60008060008060008060008061010660000160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1661010660010160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1661010660020160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff166101066003015497509750975097505050505090919293565b6000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156115e9576002548111156115a1576115e8565b806001819055506115b061170e565b7facbdb084c721332ac59f9b8e392196c9eb0e4932862da8eb9beaf0dad4f550da816040518082815260200191505060405180910390a15b5b50565b60008060008061010460008760001916600019168152602001908152602001600020925061010360008673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020549150600082141561164f5760009350611663565b8160020a9050600081846001015416141593505b50505092915050565b6000611677336106c1565b15611709578160001916610106600301546000191614151561169857600080fd5b3373ffffffffffffffffffffffffffffffffffffffff1661010660020160009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff161415611703576116fa61170e565b60019050611708565b600090505b5b919050565b610106600080820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff02191690556001820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff02191690556002820160006101000a81549073ffffffffffffffffffffffffffffffffffffffff021916905560038201600090555050611799611aed565b565b6000600190505b6002548110156118de575b600254811080156117d057506000600382610100811015156117cb57fe5b015414155b156117e25780806001019150506117ad565b5b60016002541180156118085750600060036002546101008110151561180457fe5b0154145b1561182557600260008154809291906001900391905055506117e3565b6002548110801561184a5750600060036002546101008110151561184557fe5b015414155b8015611867575060006003826101008110151561186357fe5b0154145b156118d95760036002546101008110151561187e57fe5b01546003826101008110151561189057fe5b0181905550806101036000600384610100811015156118ab57fe5b015481526020019081526020016000208190555060006003600254610100811015156118d357fe5b01819055505b6117a2565b50565b60008060008061010360003373ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020549250600083141561192257611ae5565b6101046000866000191660001916815260200190815260200160002091506000826000015414156119a85760015482600001819055506000826001018190555061010580548091906001016119779190611ba3565b826002018190555084610105836002015481548110151561199457fe5b906000526020600020900181600019169055505b8260020a90506000818360010154161415611ae4577fe1c52dc63b719ade82e8bea94cc41a0d5d28e4aaf536adb5e9cccc9ff8c1aeda3386604051808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200182600019166000191681526020019250505060405180910390a160018260000154111515611abc576101056101046000876000191660001916815260200190815260200160002060020154815481101515611a6d57fe5b9060005260206000209001600090556101046000866000191660001916815260200190815260200160002060008082016000905560018201600090556002820160009055505060019350611ae5565b8160000160008154809291906001900391905055508082600101600082825417925050819055505b5b505050919050565b600080610105805490509150600090505b81811015611b9057600060010261010582815481101515611b1b57fe5b90600052602060002090015460001916141515611b8557610104600061010583815481101515611b4757fe5b906000526020600020900154600019166000191681526020019081526020016000206000808201600090556001820160009055600282016000905550505b806001019050611afe565b6101056000611b9f9190611bcf565b5050565b815481835581811511611bca57818360005260206000209182019101611bc99190611bf0565b5b505050565b5080546000825590600052602060002090810190611bed9190611bf0565b50565b611c1291905b80821115611c0e576000816000905550600101611bf6565b5090565b905600a165627a7a7230582061e4413ca2139224fa1207d257b2aad3aad1adad78efaf6497aaf3fc04d625080029";

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
}

