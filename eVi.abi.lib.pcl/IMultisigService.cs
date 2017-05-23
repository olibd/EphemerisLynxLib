using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace eVi.abi.lib.pcl
{
   public class IMultisigService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'_h','type':'bytes32'}],'name':'confirm','outputs':[{'name':'','type':'bool'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_from','type':'address'},{'name':'_to','type':'address'}],'name':'changeMultisigOwner','outputs':[],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'_to','type':'address'},{'name':'_value','type':'uint256'},{'name':'_data','type':'bytes'}],'name':'propose','outputs':[{'name':'','type':'bytes32'}],'payable':false,'type':'function'},{'anonymous':false,'inputs':[{'indexed':false,'name':'owner','type':'address'},{'indexed':false,'name':'value','type':'uint256'},{'indexed':false,'name':'to','type':'address'},{'indexed':false,'name':'data','type':'bytes'}],'name':'SingleTransact','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'owner','type':'address'},{'indexed':false,'name':'operation','type':'bytes32'},{'indexed':false,'name':'value','type':'uint256'},{'indexed':false,'name':'to','type':'address'},{'indexed':false,'name':'data','type':'bytes'}],'name':'MultiTransact','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'operation','type':'bytes32'},{'indexed':false,'name':'initiator','type':'address'},{'indexed':false,'name':'value','type':'uint256'},{'indexed':false,'name':'to','type':'address'},{'indexed':false,'name':'data','type':'bytes'}],'name':'ConfirmationNeeded','type':'event'}]";

        public static string BYTE_CODE = "0x";

        public static Task<string> DeployContractAsync(Web3 web3, string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            return web3.Eth.DeployContract.SendRequestAsync(ABI, BYTE_CODE, addressFrom, gas, valueAmount );
        }

        private Contract contract;

        public string GetAddress()
        {
            return contract.Address;
        }

        public IMultisigService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionConfirm() {
            return contract.GetFunction("confirm");
        }
        public Function GetFunctionChangeMultisigOwner() {
            return contract.GetFunction("changeMultisigOwner");
        }
        public Function GetFunctionPropose() {
            return contract.GetFunction("propose");
        }

        public Event GetEventSingleTransact() {
            return contract.GetEvent("SingleTransact");
        }
        public Event GetEventMultiTransact() {
            return contract.GetEvent("MultiTransact");
        }
        public Event GetEventConfirmationNeeded() {
            return contract.GetEvent("ConfirmationNeeded");
        }

        public Task<bool> ConfirmAsyncCall(byte[] _h) {
            var function = GetFunctionConfirm();
            return function.CallAsync<bool>(_h);
        }
        public Task<byte[]> ProposeAsyncCall(string _to, BigInteger _value, byte[] _data) {
            var function = GetFunctionPropose();
            return function.CallAsync<byte[]>(_to, _value, _data);
        }

        public Task<string> ConfirmAsync(string addressFrom, byte[] _h, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionConfirm();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _h);
        }
        public Task<string> ChangeMultisigOwnerAsync(string addressFrom, string _from, string _to, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionChangeMultisigOwner();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _from, _to);
        }
        public Task<string> ProposeAsync(string addressFrom, string _to, BigInteger _value, byte[] _data, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionPropose();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, _to, _value, _data);
        }



    }


    public class SingleTransactEventDTO
    {
        [Parameter("address", "owner", 1, false)]
        public string Owner {get; set;}

        [Parameter("uint256", "value", 2, false)]
        public BigInteger Value {get; set;}

        [Parameter("address", "to", 3, false)]
        public string To {get; set;}

        [Parameter("bytes", "data", 4, false)]
        public byte[] Data {get; set;}

    }

    public class MultiTransactEventDTO
    {
        [Parameter("address", "owner", 1, false)]
        public string Owner {get; set;}

        [Parameter("bytes32", "operation", 2, false)]
        public byte[] Operation {get; set;}

        [Parameter("uint256", "value", 3, false)]
        public BigInteger Value {get; set;}

        [Parameter("address", "to", 4, false)]
        public string To {get; set;}

        [Parameter("bytes", "data", 5, false)]
        public byte[] Data {get; set;}

    }

    public class ConfirmationNeededEventDTO
    {
        [Parameter("bytes32", "operation", 1, false)]
        public byte[] Operation {get; set;}

        [Parameter("address", "initiator", 2, false)]
        public string Initiator {get; set;}

        [Parameter("uint256", "value", 3, false)]
        public BigInteger Value {get; set;}

        [Parameter("address", "to", 4, false)]
        public string To {get; set;}

        [Parameter("bytes", "data", 5, false)]
        public byte[] Data {get; set;}

    }


}
