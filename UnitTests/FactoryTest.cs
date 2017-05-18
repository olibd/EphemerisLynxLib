using NUnit.Framework;
using System;
using Nethereum;
using Nethereum.Web3;
using Nethereum.TestRPCRunner;
using System.Threading.Tasks;
using eVi.abi.lib.pcl;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using System.Threading;

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
            launcher = new TestRPCEmbeddedRunner();
            launcher.RedirectOuputToDebugWindow = true;
            launcher.Arguments = "--port 8545";
            //We configure embeded test rpc instance
            launcher.StartTestRPC();

            web3 = new Web3();

            //Give time for the process start
            Thread.Sleep(2000);

            //get first account
            addressFrom = (await web3.Eth.Accounts.SendRequestAsync())[0];

            string transactionHash = await FactoryService.DeployContractAsync(web3, addressFrom, new HexBigInteger(3905820));
            TransactionReceipt receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            factory = new FactoryService(web3, receipt.ContractAddress);

        }

        [TearDown]
        public void TearDown()
        {
            launcher.StopTestRPC();
        }

        [Test()]
        public async Task TestCreateIDAsync()
        {

            Event idCreationEvent = factory.GetEventReturnIDController();

            HexBigInteger filterAddressFrom = await idCreationEvent.CreateFilterAsync();

            string transactionHash = await factory.CreateIDAsync(addressFrom, new HexBigInteger(3905820));
            string transactionHash2 = await factory.CreateIDAsync(addressFrom, new HexBigInteger(3905820));

            var log = await idCreationEvent.GetFilterChanges<ReturnIDControllerEventDTO>(filterAddressFrom);

            Assert.True(log.Count > 0);
            string controllerAddress = log[0].Event._controllerAddress;
            Assert.NotNull(controllerAddress);
        }
    }
}
