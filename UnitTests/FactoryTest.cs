﻿using NUnit.Framework;
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
        FactoryService factory;

        [SetUp]
        public async Task SetupAsync()
        {
            await InitAutonomousTestAsync();

            string transactionHash = await FactoryService.DeployContractAsync(
                web3, addressFrom, new HexBigInteger(3905820));
            TransactionReceipt receipt = await 
                web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            factory = new FactoryService(web3, receipt.ContractAddress);

        }

        [TearDown]
        public void TearDown()
        {
            StopAutonomousTest();
        }

        [Test()]
        public async Task TestCreateIDAsync()
        {

            Event idCreationEvent = factory.GetEventReturnIDController();

            HexBigInteger filterAddressFrom = await idCreationEvent.CreateFilterAsync(addressFrom);

            string transactionHash = await factory.CreateIDAsync(addressFrom, new HexBigInteger(3905820));
            string transactionHash2 = await factory.CreateIDAsync(addressFrom, new HexBigInteger(3905820));

            var log = await idCreationEvent.GetFilterChanges<ReturnIDControllerEventDTO>(filterAddressFrom);

            Assert.True(log.Count > 0);
            string controllerAddress = log[0].Event._controllerAddress;
            Assert.NotNull(controllerAddress);
        }
    }
}
