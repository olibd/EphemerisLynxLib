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
    public class FactoryTest : AutonomousTest
    {
        private FactoryService _factory;

        [SetUp]
        public async Task SetupAsync()
        {
            await InitAutonomousTestAsync();

            string transactionHash = await FactoryService.DeployContractAsync(
                Web3, AddressFrom, new HexBigInteger(3905820));
            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            _factory = new FactoryService(Web3, receipt.ContractAddress);

        }

        [TearDown]
        public void TearDown()
        {
            StopAutonomousTest();
        }

        [Test]
        [Ignore("Currently skipping due to a bug in TestRPC causing test failre. The test itself is valid and passes with Parity")]
        public async Task TestCreateIDAsync()
        {

            Event idCreationEvent = _factory.GetEventReturnIDController();

            HexBigInteger filterAddressFrom = await idCreationEvent.CreateFilterAsync(AddressFrom);

            string transactionHash = await _factory.CreateIDAsync(AddressFrom, new HexBigInteger(3905820));
            string transactionHash2 = await _factory.CreateIDAsync(AddressFrom, new HexBigInteger(3905820));

            var log = await idCreationEvent.GetFilterChanges<ReturnIDControllerEventDTO>(filterAddressFrom);

            Assert.True(log.Count > 0);
            string controllerAddress = log[0].Event._controllerAddress;
            Assert.NotNull(controllerAddress);
        }
    }
}
