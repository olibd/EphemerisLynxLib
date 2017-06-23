using NUnit.Framework;
using Nethereum.Web3;
using System.Threading.Tasks;
using eVi.abi.lib.pcl;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using System.Numerics;

namespace UnitTests
{
    [TestFixture]
    public class IDControllerTest : IDTest
    {
        public IDControllerService IDController { get; set; }
        public WatchdogService Watchdogs { get; set; }

        [SetUp]
        public async override Task SetupAsync()
        {
            await base.SetupAsync();
            await DeployIDControllerAsync();
            await DeployWatchdogsAsync();
        }

        [Test]
        public async override Task TestAddGetOwnedAttributeAsync()
        {
            byte[] key = Byte32Encoder.Encode("key");
            string transactionHash = await IDController.AddAttributeAsync(AddressFrom, key, OwnedAttribute.GetAddress(), new HexBigInteger(3905820));
            Assert.NotNull(transactionHash);
            string attributeAddress = await IDController.GetAttributeAsyncCall(key);
            Assert.NotNull(attributeAddress);
            Assert.AreEqual(OwnedAttribute.GetAddress(), attributeAddress);
        }

        [Test]
        public async Task TestSetGetWatchdogsAsync()
        {
            string transactionHash = await IDController.SetWatchDogsAsync(AddressFrom, Watchdogs.GetAddress(), new HexBigInteger(3905820));
            Assert.NotNull(transactionHash);
            string watchdogsAddress = await IDController.GetWatchDogsAsyncCall();
            Assert.AreEqual(Watchdogs.GetAddress(), watchdogsAddress);
        }

        [Test]
        public async override Task TestRemoveAttributeAsync()
        {
            //Add the attribute to the contract, and get it
            await TestAddGetOwnedAttributeAsync();

            //Remove the attribute
            byte[] key = Byte32Encoder.Encode("key");
            string tansactionHash = await IDController.RemoveAttributeAsync(AddressFrom, key, new HexBigInteger(3905820));
            string attributeAddress = await IDController.GetAttributeAsyncCall(key);
            Assert.AreEqual("0x0000000000000000000000000000000000000000", attributeAddress);
        }

        [Test]
        public async Task TestGetIDAsync()
        {
            string IDAddress = await IDController.GetIDAsyncCall();
            Assert.AreEqual(ID.GetAddress(), IDAddress);
        }

        [Test]
        public async override Task TestChangeOwner()
        {
            string newOwner = "0x0000000000000000000000000000000000000000";
            string ownerAddress = await IDController.OwnerAsyncCall();
            Assert.AreEqual(AddressFrom, ownerAddress);
            string transactionHash = await IDController.ChangeOwnerAsync(AddressFrom, newOwner, new HexBigInteger(3905820));
            ownerAddress = await IDController.OwnerAsyncCall();
            Assert.AreEqual(newOwner, ownerAddress);
        }

        //Unimplemented tests

        [Test]
        [Ignore("To implement, implementation unknown yet")]
        public async Task TestDeleteID()
        {
            //TODO
        }

        [Test]
        [Ignore("Unimplemented, waiting for the proper event to be added to the contract")]
        public async override Task TestCreateCertificate()
        {
            //TODO
        }

        [Test]
        [Ignore("Unimplemented, waiting for the proper event to be added to the contract")]
        public async override Task TestRevokeCertificate()
        {
        }

        [Test]
        [Ignore("Inherited test not part of IDController interface")]
        public async override Task TestAddGetCertificateWithNotOwnedAttributeAsync()
        {
        }

        [Test]
        [Ignore("Inherited test not part of IDController interface")]
        public async override Task TestAddGetCertificateAsync()
        {
        }

        [Test]
        [Ignore("Unimplemented, functionality tested by IDTest")]
        public async override Task TestAddNotOwnedAttributeAsync()
        {
        }

        protected async Task DeployIDControllerAsync()
        {
            string transactionHash = await IDControllerService.DeployContractAsync(
                Web3, AddressFrom, ID.GetAddress(), new HexBigInteger(3905820));

            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            IDController = new IDControllerService(Web3, receipt.ContractAddress);

            //Change the owner of the ID to make the ID controller the owner
            transactionHash = await ID.ChangeOwnerAsync(AddressFrom, IDController.GetAddress(), new HexBigInteger(3905820));
            Assert.NotNull(transactionHash);
        }

        protected async Task DeployWatchdogsAsync()
        {
            string transactionHash = await WatchdogService.DeployContractAsync(
                Web3, AddressFrom, new string[] { AddressFrom }, new BigInteger(1), new HexBigInteger(3905820));

            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            Watchdogs = new WatchdogService(Web3, receipt.ContractAddress);
        }
    }
}
