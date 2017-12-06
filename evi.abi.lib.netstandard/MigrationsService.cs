using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace eVi.abi.lib.pcl
{
   public class MigrationsService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'new_address','type':'address'}],'name':'upgrade','outputs':[],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'last_completed_migration','outputs':[{'name':'','type':'uint256'}],'payable':false,'type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'constant':false,'inputs':[{'name':'completed','type':'uint256'}],'name':'setCompleted','outputs':[],'payable':false,'type':'function'},{'inputs':[],'payable':false,'type':'constructor'}]";

        public static string BYTE_CODE = "0x6060604052341561000c57fe5b5b33600060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff1602179055505b5b6102c58061005f6000396000f30060606040526000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680630900f0101461005c578063445df0ac146100925780638da5cb5b146100b8578063fdacd5761461010a575bfe5b341561006457fe5b610090600480803573ffffffffffffffffffffffffffffffffffffffff1690602001909190505061012a565b005b341561009a57fe5b6100a261020a565b6040518082815260200191505060405180910390f35b34156100c057fe5b6100c8610210565b604051808273ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff16815260200191505060405180910390f35b341561011257fe5b6101286004808035906020019091905050610236565b005b6000600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff161415610205578190508073ffffffffffffffffffffffffffffffffffffffff1663fdacd5766001546040518263ffffffff167c010000000000000000000000000000000000000000000000000000000002815260040180828152602001915050600060405180830381600087803b15156101f257fe5b6102c65a03f1151561020057fe5b5050505b5b5b5050565b60015481565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1681565b600060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff16141561029557806001819055505b5b5b505600a165627a7a72305820a8c96bf453947d24d5e623cc2bdbdb052bb862e2d4ba0cb53ea8694c078e983e0029";

        public static async Task<string> DeployContractAsync(Web3 web3, string keyFrom,  HexBigInteger gasPrice = null, HexBigInteger valueAmount = null)
        {
            string data = web3.Eth.DeployContract.GetData(BYTE_CODE, ABI );
            ITransactionService transactionService = new ConstantGasTransactionService(keyFrom, web3);
            return await transactionService.SignAndSendTransaction(data, "", new HexBigInteger(0), gasPrice);
        }

        private Contract contract;
        private ITransactionService _transactionService;

        public string GetAddress()
        {
            return contract.Address;
        }

        public MigrationsService(Web3 web3, string key, string address)
        {
            this.web3 = web3;
            this._transactionService = new ConstantGasTransactionService(key, web3);
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionUpgrade() {
            return contract.GetFunction("upgrade");
        }
        public Function GetFunctionLast_completed_migration() {
            return contract.GetFunction("last_completed_migration");
        }
        public Function GetFunctionOwner() {
            return contract.GetFunction("owner");
        }
        public Function GetFunctionSetCompleted() {
            return contract.GetFunction("setCompleted");
        }


        public Task<BigInteger> Last_completed_migrationAsyncCall() {
            try{ 
                var function = GetFunctionLast_completed_migration();
                return function.CallAsync<BigInteger>();
            }catch(Exception e){
                throw new CallFailed(e);
            }
        }
        public Task<string> OwnerAsyncCall() {
            try{ 
                var function = GetFunctionOwner();
                return function.CallAsync<string>();
            }catch(Exception e){
                throw new CallFailed(e);
            }
        }

        public async Task<string> UpgradeAsync(string new_address, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionUpgrade();
                string data = function.GetData(new_address);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }
        public async Task<string> SetCompletedAsync(BigInteger completed, HexBigInteger gasPrice = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionSetCompleted();
                string data = function.GetData(completed);
                return await _transactionService.SignAndSendTransaction(data, contract.Address, valueAmount, gasPrice);
        }



    }



}

