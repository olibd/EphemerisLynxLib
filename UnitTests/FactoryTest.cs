using NUnit.Framework;
using System;
using Nethereum;
using Nethereum.Web3;
using Nethereum.TestRPCRunner;
using System.Threading.Tasks;
using eVi.abi.lib.pcl;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;

namespace UnitTests
{
    [TestFixture()]
    public class FactoryTest
    {
        Web3 web3;
        TestRPCEmbeddedRunner launcher;
        FactoryService factory;
        string addressFrom;

        [SetUp]
        public async Task Setup()
        {
            //We configure embeded test rpc instance
            /*launcher = new TestRPCEmbeddedRunner();
            launcher.RedirectOuputToDebugWindow = true;
            launcher.Arguments = "--port 8545";
            //We configure embeded test rpc instance
            launcher.StartTestRPC();*/
            web3 = new Web3();

            //get first account
            addressFrom = (await web3.Eth.Accounts.SendRequestAsync())[0];

            //deploy the factory contract
            //string transactionHash = await FactoryService.DeployContractAsync(web3, addressFrom, new HexBigInteger(900000), new HexBigInteger(900000));

            //ensures a transaction was properly created
            /*Assert.NotNull(transactionHash);

            //get the transaction receipt from which we can recover
            TransactionReceipt receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            //Instantiate the factory
            factory = new FactoryService(web3, receipt.ContractAddress);*/

            var contractByteCode = OwnedService.BYTE_CODE;
			//OwnedService ABI
			//var abi = OwnedService.ABI;

			//Factory ABI
			//var abi = FactoryService.ABI;

			//no @ ABI
			//var abi = "[{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'type':'function'},{'inputs':[],'payable':false,'type':'constructor'}]";

			//abi form testrpc sample
			var abi = @"[{""constant"":true,""inputs"":[],""name"":""getMultiplier"",""outputs"":[{""name"":""d"",""type"":""uint256""}],""type"":""function""},{""constant"":true,""inputs"":[],""name"":""contractName"",""outputs"":[{""name"":"""",""type"":""string""}],""type"":""function""},{""constant"":false,""inputs"":[{""name"":""a"",""type"":""uint256""}],""name"":""multiply"",""outputs"":[{""name"":""d"",""type"":""uint256""}],""type"":""function""},{""inputs"":[{""name"":""multiplier"",""type"":""uint256""}],""type"":""constructor""}]";
			//abi form testrpc sample
			//var abi = "[{'constant':true,'inputs':[],'name':'getMultiplier','outputs':[{'name':'d','type':'uint256'}],'type':'function'},{'constant':true,'inputs':[],'name':'contractName','outputs':[{'name':'','type':'string'}],'type':'function'},{'constant':false,'inputs':[{'name':'a','type':'uint256'}],'name':'multiply','outputs':[{'name':'d','type':'uint256'}],'type':'function'},{'inputs':[{'name':'multiplier','type':'uint256'}],'type':'constructor'}]";
			//abi form testrpc sample
			//var abi = "[{\"constant\":true,\"inputs\":[],\"name\":\"getMultiplier\",\"outputs\":[{\"name\":\"d\",\"type\":\"uint256\"}],\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"contractName\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"a\",\"type\":\"uint256\"}],\"name\":\"multiply\",\"outputs\":[{\"name\":\"d\",\"type\":\"uint256\"}],\"type\":\"function\"},{\"inputs\":[{\"name\":\"multiplier\",\"type\":\"uint256\"}],\"type\":\"constructor\"}]";

			//ABI for multiplication contract of abi-code-gen
			//var abi = @"[{'constant':false,'inputs':[{'name':'a','type':'int256'}],'name':'multiply','outputs':[{'name':'r','type':'int256'}],'payable':false,'type':'function'},{'inputs':[{'name':'multiplier','type':'int256'}],'type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'name':'a','type':'int256'},{'indexed':true,'name':'sender','type':'address'},{'indexed':false,'name':'result','type':'int256'}],'name':'Multiplied','type':'event'}]";

			//ABI for FuntionDTO contract of abi-code-gen
			//var abi = @"[{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'name','type':'string'},{'name':'description','type':'string'}],'name':'StoreDocument','outputs':[{'name':'success','type':'bool'}],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'','type':'bytes32'},{'name':'','type':'uint256'}],'name':'documents','outputs':[{'name':'name','type':'string'},{'name':'description','type':'string'},{'name':'sender','type':'address'}],'payable':false,'type':'function'}]";
			//abi generated from solcjs raw:
			//var abi = "[{\"constant\":true,\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"type\":\"function\"},{\"inputs\":[],\"payable\":false,\"type\":\"constructor\"}]";

			//Factory abi generated from solcjs raw:
			//var abi = "[{\"constant\":false,\"inputs\":[],\"name\":\"createID\",\"outputs\":[{\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"_from\",\"type\":\"address\"},{\"indexed\":false,\"name\":\"_controllerAddress\",\"type\":\"address\"}],\"name\":\"ReturnIDController\",\"type\":\"event\"}]";


			//UroSharp Demo ABI
			//var abi = "[{'constant':false,'inputs':[{'name':'score','type':'int256'}],'name':'setTopScore','outputs':[],'type':'function'},{'constant':true,'inputs':[{'name':'','type':'uint256'}],'name':'topScores','outputs':[{'name':'addr','type':'address'},{'name':'score','type':'int256'}],'type':'function'},{'constant':false,'inputs':[],'name':'getCountTopScores','outputs':[{'name':'','type':'uint256'}],'type':'function'},{'constant':true,'inputs':[{'name':'','type':'address'}],'name':'userTopScores','outputs':[{'name':'','type':'int256'}],'type':'function'}]";
			

            var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, contractByteCode, addressFrom, new HexBigInteger(900000), 7);

			Assert.NotNull(transactionHash);
        }

		[TearDown]
		public void TearDown()
		{
            //launcher.StopTestRPC();
		}

        [Test()]
        public async Task TestCreateIDAsync()
        {
            string transactionHash = await factory.CreateIDAsync(addressFrom);
            Assert.NotNull(transactionHash);
        }



    }
}
