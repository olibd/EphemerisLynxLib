using NUnit.Framework;
using Nethereum.Web3;
using Nethereum.ABI.Encoders;
using System.Threading.Tasks;
using eVi.abi.lib.pcl;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;

namespace UnitTests
{
    [TestFixture]
    public class IDTest : AutonomousTest
    {
        public IDService ID { get; set; }
        public AttributeService OwnedAttribute;
        private AttributeService NotOwnedAttribute;
        private CertificateService _attributeOwnedCertificate;
        public Bytes32TypeEncoder Byte32Encoder { get; set; }

        [SetUp]
        public async virtual Task SetupAsync()
        {
            Byte32Encoder = new Bytes32TypeEncoder();
            await InitAutonomousTestAsync();
            await DeployIDAsync();
            string location = "somewhere";
            string hash = "somehash";
            OwnedAttribute = await DeployAttributeAsync(location, hash, ID.GetAddress());
            NotOwnedAttribute = await DeployAttributeAsync(location, hash, "0x7e2be0405d58e50de14522b47de83383597245e8");
            await DeployAttributeOwnedCertificateAsync(location, hash);
        }

        [Test]
        public async virtual Task TestAddNotOwnedAttributeAsync()
        {
            string transactionHash = null;
            try
            {
                byte[] key = Byte32Encoder.Encode("key");
                transactionHash = await ID.AddAttributeAsync(AddressFrom, key, NotOwnedAttribute.GetAddress(), new HexBigInteger(3905820));
            }
            catch (RpcResponseException e)
            {

            }

            Assert.Null(transactionHash);
        }

        [Test]
        public async virtual Task TestAddGetOwnedAttributeAsync()
        {
            byte[] key = Byte32Encoder.Encode("key");
            string transactionHash = await ID.AddAttributeAsync(AddressFrom, key, OwnedAttribute.GetAddress(), new HexBigInteger(3905820));
            Assert.NotNull(transactionHash);
            string attributeAddress = await ID.GetAttributeAsyncCall(key);
            Assert.NotNull(attributeAddress);
            Assert.AreEqual(OwnedAttribute.GetAddress(), attributeAddress);
        }

        [Test]
        [Ignore("This test is currently failing and under investigation. It is potentially a Nethereum problem.")]
        public async virtual Task TestAddGetCertificateWithNotOwnedAttributeAsync()
        {
            //Add the attribute to the contract
            await TestAddGetOwnedAttributeAsync();

            //Add the certificate
            byte[] key = Byte32Encoder.Encode("key");
            string transactionHash = null;
            try
            {
                transactionHash = await ID.AddCertificateAsync(AddressFrom, NotOwnedAttribute.GetAddress(), _attributeOwnedCertificate.GetAddress(), new HexBigInteger(3905820));
            }
            catch (RpcResponseException e)
            {

            }

            Assert.Null(transactionHash);
        }

        [Test]
        public async virtual Task TestAddGetCertificateAsync()
        {
            //Add the attribute to the contract
            await TestAddGetOwnedAttributeAsync();

            //Add the certificate
            byte[] key = Byte32Encoder.Encode("key");
            string transactionHash = await ID.AddCertificateAsync(AddressFrom, key, _attributeOwnedCertificate.GetAddress(), new HexBigInteger(3905820));
            Assert.NotNull(transactionHash);
            string certificateAddress = await ID.GetCertificateAsyncCall(key, AddressFrom);
            Assert.AreEqual(_attributeOwnedCertificate.GetAddress(), certificateAddress);
        }

        [Test]
        public async virtual Task TestRemoveAttributeAsync()
        {
            //Add the attribute to the contract, and get it
            await TestAddGetOwnedAttributeAsync();

            //Remove the attribute
            byte[] key = Byte32Encoder.Encode("key");
            string tansactionHash = await ID.RemoveAttributeAsync(AddressFrom, key, new HexBigInteger(3905820));
            string attributeAddress = await ID.GetAttributeAsyncCall(key);
            Assert.AreEqual("0x0000000000000000000000000000000000000000", attributeAddress);
        }

        [Test]
        public async virtual Task TestChangeOwner()
        {
            string newOwner = "0x0000000000000000000000000000000000000000";
            string ownerAddress = await ID.OwnerAsyncCall();
            Assert.AreEqual(AddressFrom, ownerAddress);
            string transactionHash = await ID.ChangeOwnerAsync(AddressFrom, newOwner, new HexBigInteger(3905820));
            ownerAddress = await ID.OwnerAsyncCall();
            Assert.AreEqual(newOwner, ownerAddress);
        }

        [Test]
        [Ignore("Unimplemented, waiting for the proper event to be added to the contract")]
        public async virtual Task TestCreateCertificate()
        {
        }

        [Test]
        [Ignore("Unimplemented, waiting for the proper event to be added to the contract")]
        public async virtual Task TestRevokeCertificate()
        {
        }

        [TearDown]
        public void TearDown()
        {
            StopAutonomousTest();
        }

        protected async Task DeployIDAsync()
        {
            string transactionHash = await IDService.DeployContractAsync(
                Web3, AddressFrom, new HexBigInteger(3905820));

            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            ID = new IDService(Web3, receipt.ContractAddress);
        }

        private async Task<AttributeService> DeployAttributeAsync(string location, string hash, string owner)
        {
            string transactionHash = await
                AttributeService.DeployContractAsync(
                    Web3, AddressFrom, location, hash, owner,
                    new HexBigInteger(3905820));

            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            return new AttributeService(Web3, receipt.ContractAddress);
        }

        private async Task DeployAttributeOwnedCertificateAsync(string location, string hash)
        {
            string owner = AddressFrom;
            string transactionHash = await
                CertificateService.DeployContractAsync(
                    Web3, AddressFrom, location, hash, OwnedAttribute.GetAddress(),
                    new HexBigInteger(3905820));

            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            _attributeOwnedCertificate = new CertificateService(Web3, receipt.ContractAddress);
        }
    }
}
