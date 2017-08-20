using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace eVi.abi.lib.pcl
{
   public class OwnedService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'inputs':[],'payable':false,'type':'constructor'}]";

        public static string BYTE_CODE = "0x6060604052341561000c57fe5b5b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b60db8061005e6000396000f30060606040526000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680638da5cb5b14603a575bfe5b3415604157fe5b60476089565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff16815600a165627a7a7230582018780768ce065c864cdd4e564d6f18b47c1253e9deac037459a8ff2be6dbcc920029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom,  HexBigInteger gas = null, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI );
            TransactionService transactionService = new TransactionService(keyFrom, web3);
            return await transactionService.SignAndSendTransaction(data, "", gasPrice, gas);
        }

        private Contract contract;
        private TransactionService _transactionService;

        public string GetAddress()
        {
            return contract.Address;
        }

        public OwnedService(Web3 web3, string key, string address)
        {
            this.web3 = web3;
            this._transactionService = new TransactionService(key, web3);
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionOwner() {
            return contract.GetFunction("owner");
        }


        public Task<string> OwnerAsyncCall() {
            var function = GetFunctionOwner();
            return function.CallAsync<string>();
        }




    }



}

