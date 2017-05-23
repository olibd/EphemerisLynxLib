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

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'attr','type':'address'}],'name':'addAttribute','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'','type':'uint256'}],'name':'attributesKeys','outputs':[{'name':'','type':'bytes32'}],'payable':false,'type':'function'},{'constant':false,'inputs':[],'name':'kill','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_location','type':'string'},{'name':'_hash','type':'string'},{'name':'_owningAttribute','type':'address'}],'name':'createCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'cert','type':'address'}],'name':'revokeCertificate','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'attr','type':'address'},{'name':'cert','type':'address'}],'name':'addCertificate','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'newOwner','type':'address'}],'name':'changeOwner','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'','type':'bytes32'}],'name':'attributes','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'key','type':'bytes32'},{'name':'issuer','type':'address'}],'name':'getCertificate','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'key','type':'bytes32'}],'name':'removeAttribute','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'key','type':'bytes32'}],'name':'getAttribute','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'anonymous':false,'inputs':[{'indexed':false,'name':'_from','type':'address'},{'indexed':false,'name':'_certAddress','type':'address'}],'name':'ReturnCertificate','type':'event'}]";

        public static string BYTE_CODE = "0x60606040525b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b611710806100576000396000f300606060405236156100c3576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806302859d60146100c55780631ab807f814610120578063277c94671461016357806341c0e1b51461019f5780634f23eb86146101b15780635f893bfa146102ad578063781dd722146102e35780638da5cb5b14610338578063a6f9dae11461038a578063b115ce0d146103c0578063d943f41914610424578063e7996f07146104a7578063eb43e033146104cb575bfe5b34156100cd57fe5b61010660048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061052f565b604051808215151515815260200191505060405180910390f35b341561012857fe5b61016160048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610744565b005b341561016b57fe5b61018160048080359060200190919050506107b3565b60405180826000191660001916815260200191505060405180910390f35b34156101a757fe5b6101af6107d8565b005b34156101b957fe5b61026b600480803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803590602001908201803590602001908080601f0160208091040260200160405190810160405280939291908181526020018383808284378201915050505050509190803573ffffffffffffffffffffffffffffffffffffffff169060200190919050506108c4565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156102b557fe5b6102e1600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610b0d565b005b34156102eb57fe5b610336600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610b85565b005b341561034057fe5b610348610cf0565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561039257fe5b6103be600480803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610d16565b005b34156103c857fe5b6103e2600480803560001916906020019091905050610db3565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561042c57fe5b61046560048080356000191690602001909190803573ffffffffffffffffffffffffffffffffffffffff16906020019091905050610de6565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b34156104af57fe5b6104c9600480803560001916906020019091905050610eb1565b005b34156104d357fe5b6104ed600480803560001916906020019091905050610f4b565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141561073d573073ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b151561060a57fe5b6102c65a03f1151561061857fe5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff161415156106455760006000fd5b8160016000856000191660001916815260200190815260200160002060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff160217905550600280548060010182816106b39190610f91565b916000526020600020900160005b85909190915090600019169055508173ffffffffffffffffffffffffffffffffffffffff1660016000856000191660001916815260200190815260200160002060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff161490505b5b5b92915050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156107ae576107ac6107a683610f4b565b82610b85565b5b5b5b5050565b6002818154811015156107c257fe5b906000526020600020900160005b915090505481565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156108c157600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156108bf57600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16ff5b5b5b5b565b60006000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610b0457848484610929610fbd565b8080602001806020018473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018381038352868181518152602001915080519060200190808383600083146109ab575b8051825260208311156109ab57602082019150602081019050602083039250610987565b505050905090810190601f1680156109d75780820380516001836020036101000a031916815260200191505b50838103825285818151815260200191508051906020019080838360008314610a1f575b805182526020831115610a1f576020820191506020810190506020830392506109fb565b505050905090810190601f168015610a4b5780820380516001836020036101000a031916815260200191505b5095505050505050604051809103906000f0801515610a6657fe5b90507f4c1991050161bf763bdff362dd4481e654e2f4658bd3cda0cf2f1b4f22a2aebb3382604051808373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020018273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019250505060405180910390a18091505b5b5b509392505050565b8073ffffffffffffffffffffffffffffffffffffffff1663b6549f756040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050600060405180830381600087803b1515610b7057fe5b6102c65a03f11515610b7e57fe5b5050505b50565b3073ffffffffffffffffffffffffffffffffffffffff168273ffffffffffffffffffffffffffffffffffffffff16638da5cb5b6000604051602001526040518163ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401809050602060405180830381600087803b1515610c0857fe5b6102c65a03f11515610c1657fe5b5050506040518051905073ffffffffffffffffffffffffffffffffffffffff16141515610c435760006000fd5b8173ffffffffffffffffffffffffffffffffffffffff16637c6ebde9826040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050600060405180830381600087803b1515610cda57fe5b6102c65a03f11515610ce857fe5b5050505b5050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610daf5780600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b5b50565b60016020528060005260406000206000915054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b6000610df183610f4b565b73ffffffffffffffffffffffffffffffffffffffff1663fd531e93836000604051602001526040518263ffffffff167c0100000000000000000000000000000000000000000000000000000000028152600401808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001915050602060405180830381600087803b1515610e9057fe5b6102c65a03f11515610e9e57fe5b5050506040518051905090505b92915050565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610f475760016000826000191660001916815260200190815260200160002060006101000a81549073ffffffffffffffffffffffffffffffffffffffff02191690555b5b5b50565b600060016000836000191660001916815260200190815260200160002060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1690505b919050565b815481835581811511610fb857818360005260206000209182019101610fb79190610fcd565b5b505050565b6040516106f280610ff383390190565b610fef91905b80821115610feb576000816000905550600101610fd3565b5090565b90560060606040526000600360006101000a81548160ff021916908315150217905550341561002757fe5b6040516106f23803806106f2833981016040528080518201919060200180518201919060200180519060200190919050505b5b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b82600190805190602001906100b2929190610114565b5081600290805190602001906100c9929190610114565b5080600360016101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5050506101b9565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061015557805160ff1916838001178555610183565b82800160010185558215610183579182015b82811115610182578251825591602001919060010190610167565b5b5090506101909190610194565b5090565b6101b691905b808211156101b257600081600090555060010161019a565b5090565b90565b61052a806101c86000396000f30060606040523615610076576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806309bd5a6014610078578063232533b214610111578063516f279e1461016357806363d256ce146101fc5780638da5cb5b14610226578063b6549f7514610278575bfe5b341561008057fe5b61008861028a565b60405180806020018281038252838181518152602001915080519060200190808383600083146100d7575b8051825260208311156100d7576020820191506020810190506020830392506100b3565b505050905090810190601f1680156101035780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b341561011957fe5b610121610328565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561016b57fe5b61017361034e565b60405180806020018281038252838181518152602001915080519060200190808383600083146101c2575b8051825260208311156101c25760208201915060208101905060208303925061019e565b505050905090810190601f1680156101ee5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b341561020457fe5b61020c6103ec565b604051808215151515815260200191505060405180910390f35b341561022e57fe5b6102366103ff565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561028057fe5b610288610425565b005b60028054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103205780601f106102f557610100808354040283529160200191610320565b820191906000526020600020905b81548152906001019060200180831161030357829003601f168201915b505050505081565b600360019054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b60018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103e45780601f106103b9576101008083540402835291602001916103e4565b820191906000526020600020905b8154815290600101906020018083116103c757829003601f168201915b505050505081565b600360009054906101000a900460ff1681565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614156104fb576001600360006101000a81548160ff0219169083151502179055507fb6fa8b8bd5eab60f292eca876e3ef90722275b785309d84b1de113ce0b8c4e7433604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390a15b5b5b5600a165627a7a72305820a5bd7ca92252e144ef6f0bfba7808efe06a5c93b4b5890846b2fc47fff3121cc0029a165627a7a7230582011c2452c2236a624fa559db4e0e76d5461a8762502aa92e3a00684e22c77f74e0029";

        public static Task<string> DeployContractAsync(Web3 web3, string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            return web3.Eth.DeployContract.SendRequestAsync(ABI, BYTE_CODE, addressFrom, gas, valueAmount );
        }

        private Contract contract;

        public string GetAddress()
        {
            return contract.Address;
        }

        public IDService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionAddAttribute() {
            return contract.GetFunction("addAttribute");
        }
        public Function GetFunctionAddCertificate() {
            return contract.GetFunction("addCertificate");
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
        public Function GetFunctionChangeOwner() {
            return contract.GetFunction("changeOwner");
        }
        public Function GetFunctionAttributes() {
            return contract.GetFunction("attributes");
        }
        public Function GetFunctionGetCertificate() {
            return contract.GetFunction("getCertificate");
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

        public Task<bool> AddAttributeAsyncCall(byte[] key, string attr) {
            var function = GetFunctionAddAttribute();
            return function.CallAsync<bool>(key, attr);
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
        public Task<string> AttributesAsyncCall(byte[] a) {
            var function = GetFunctionAttributes();
            return function.CallAsync<string>(a);
        }
        public Task<string> GetCertificateAsyncCall(byte[] key, string issuer) {
            var function = GetFunctionGetCertificate();
            return function.CallAsync<string>(key, issuer);
        }
        public Task<string> GetAttributeAsyncCall(byte[] key) {
            var function = GetFunctionGetAttribute();
            return function.CallAsync<string>(key);
        }

        public Task<string> AddAttributeAsync(string addressFrom, byte[] key, string attr, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddAttribute();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, key, attr);
        }
        public Task<string> AddCertificateAsync(string addressFrom, byte[] key, string cert, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddCertificate();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, key, cert);
        }
        public Task<string> KillAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionKill();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount);
        }
        public Task<string> CreateCertificateAsync(string addressFrom, string _location, string _hash, string _owningAttribute, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionCreateCertificate();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _location, _hash, _owningAttribute);
        }
        public Task<string> RevokeCertificateAsync(string addressFrom, string cert, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRevokeCertificate();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, cert);
        }
        public Task<string> AddCertificateAsync(string addressFrom, string attr, string cert, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionAddCertificate();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, attr, cert);
        }
        public Task<string> ChangeOwnerAsync(string addressFrom, string newOwner, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionChangeOwner();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, newOwner);
        }
        public Task<string> RemoveAttributeAsync(string addressFrom, byte[] key, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionRemoveAttribute();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, key);
        }



    }


    public class ReturnCertificateEventDTO
    {
        [Parameter("address", "_from", 1, false)]
        public string _from {get; set;}

        [Parameter("address", "_certAddress", 2, false)]
        public string _certAddress {get; set;}

    }


}