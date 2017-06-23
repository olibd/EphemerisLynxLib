using NUnit.Framework;
using System;
using Nethereum;
using Nethereum.Web3;
using System.Threading.Tasks;
using eVi.abi.lib.pcl;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using System.Numerics;

namespace UnitTests
{
    [TestFixture]
    public class WatchdogsTest : AutonomousTest
    {
        public IDControllerService IDController { get; set; }
        public IDService ID { get; set; }
        public WatchdogService Watchdogs { get; set; }

        [SetUp]
        public async Task SetupAsync()
        {
            await InitAutonomousTestAsync();
            await DeployIDAsync();
            await DeployIDControllerAsync();
            await DeployWatchdogsAsync();

        }

        [TearDown]
        public void TearDown()
        {
            StopAutonomousTest();
        }

        [Test]
        public async Task TestProposeAndGetMigration()
        {
            string transactionHash = await Watchdogs.ProposeMigrationAsync(AddressFrom, IDController.GetAddress(), AddressFrom2, new HexBigInteger(3905820));
            Assert.NotNull(transactionHash);
            GetProposalDTO proposal = await Watchdogs.GetProposalAsyncCall();
            //test for call destination
            Assert.AreEqual(IDController.GetAddress(), proposal.A);
            //test for the new proposed owner
            Assert.AreEqual(AddressFrom2, proposal.B);
            //test for the initiator of the proposal
            Assert.AreEqual(AddressFrom, proposal.C);
            //TODO: test for proposal hash by parsing the event
            //TODO: check that the user has automatically confirmed the proposal
        }

        [Test]
        public async Task TestProposeAndGetDeletion()
        {
            string transactionHash = await Watchdogs.ProposeDeletionAsync(AddressFrom, IDController.GetAddress(), new HexBigInteger(3905820));
            Assert.NotNull(transactionHash);
            GetProposalDTO proposal = await Watchdogs.GetProposalAsyncCall();
            //test for call destination
            Assert.AreEqual(IDController.GetAddress(), proposal.A);
            //test for the new proposed owner
            Assert.AreEqual("0x0000000000000000000000000000000000000000", proposal.B);
            //test for the initiator of the proposal
            Assert.AreEqual(AddressFrom, proposal.C);
            //TODO: test for proposal hash by parsing the event
            //TODO: check that the user has automatically confirmed the proposal
        }

        [Test]
        public async Task TestProposeDeletion()
        {
            string transactionHash = await Watchdogs.ProposeDeletionAsync(AddressFrom, IDController.GetAddress(), new HexBigInteger(3905820));
            GetProposalDTO proposal = await Watchdogs.GetProposalAsyncCall();
            //test for call destination
            Assert.AreEqual(IDController.GetAddress(), proposal.A);
            //test for the new proposed owner
            Assert.AreEqual("0x0000000000000000000000000000000000000000", proposal.B);
            //test for the initiator of the proposal
            Assert.AreEqual(AddressFrom, proposal.C);
            //TODO: test for proposal hash by parsing the event
        }

        [Test]
        [Ignore("Unimplemented due to inability to access event with testRPC, this is a TODO")]
        public async Task TestConfirm()
        {
            //TODO Implement TestConfirm
        }

        [Test]
        [Ignore("Unimplemented due to inability to access event with testRPC, this is a TODO")]
        public async Task TestCancel()
        {
            //TODO Implement TestCancel
        }

        protected async Task DeployIDAsync()
        {
            string transactionHash = await IDService.DeployContractAsync(
                Web3, AddressFrom, new HexBigInteger(3905820));

            Assert.NotNull(transactionHash);

            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            ID = new IDService(Web3, receipt.ContractAddress);
        }

        private async Task DeployWatchdogsAsync()
        {
            string transactionHash = await WatchdogService.DeployContractAsync(
                Web3, AddressFrom, new string[] { AddressFrom, AddressFrom2 },
                new BigInteger(2), new HexBigInteger(3905820));

            Assert.NotNull(transactionHash);

            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            Watchdogs = new WatchdogService(Web3, receipt.ContractAddress);

            transactionHash = await IDController.SetWatchDogsAsync(AddressFrom, Watchdogs.GetAddress(), new HexBigInteger(3905820));

            Assert.NotNull(transactionHash);
        }

        protected async Task DeployIDControllerAsync()
        {
            string transactionHash = await IDControllerService.DeployContractAsync(
                Web3, AddressFrom, ID.GetAddress(), new HexBigInteger(3905820));

            Assert.NotNull(transactionHash);

            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            IDController = new IDControllerService(Web3, receipt.ContractAddress);

            //Change the owner of the ID to make the ID controller the owner
            transactionHash = await ID.ChangeOwnerAsync(AddressFrom, IDController.GetAddress(), new HexBigInteger(3905820));

            Assert.NotNull(transactionHash);
        }
    }
}
