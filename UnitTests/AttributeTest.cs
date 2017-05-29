using NUnit.Framework;
using System;
using eVi.abi.lib.pcl;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;

namespace UnitTests
{
    [TestFixture()]
    public class AttributeTest : AutonomousTest
    {
        private AttributeService _attribute;
        private CertificateService _attributeOwnedCertificate;
        private CertificateService _certificateNotOwnedByAttribute;

        [SetUp]
        public async Task SetupAsync()
        {
            await InitAutonomousTestAsync();
            string location = "somewhere";
            string hash = "somehash";
            await DeployAttributeAsync(location, hash);
            await DeployCertificatesAsync(location, hash);
        }

        [TearDown]
        public void TearDown()
        {
            StopAutonomousTest();
        }

        [Test]
        public async Task TestAddNotOwnedCertificateAsync()
        {
            string transactionHash = null;
            try
            {
                transactionHash = await _attribute.AddCertificateAsync(AddressFrom, _certificateNotOwnedByAttribute.GetAddress(), new HexBigInteger(3905820));
            }
            catch (RpcResponseException e)
            { }

            Assert.Null(transactionHash);
        }

        [Test]
        public async Task TestAddGetOwnedCertificateAsync()
        {
            string transactionHash = null;

            try
            {
                transactionHash = await _attribute.AddCertificateAsync(AddressFrom, _attributeOwnedCertificate.GetAddress(), new HexBigInteger(3905820));
            }
            catch (RpcResponseException e)
            {
                Assert.Fail();
            }

            Assert.NotNull(transactionHash);
            string callResult = await _attribute.GetCertificateAsyncCall(AddressFrom);
            Assert.AreEqual(callResult, _attributeOwnedCertificate.GetAddress());
        }

        private async Task DeployAttributeAsync(string location, string hash)
        {
            string owner = AddressFrom;
            string transactionHash = await
                AttributeService.DeployContractAsync(
                    Web3, AddressFrom, location, hash, owner,
                    new HexBigInteger(3905820));

            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            _attribute = new AttributeService(Web3, receipt.ContractAddress);
        }

        private async Task DeployCertificatesAsync(string location, string hash)
        {
            await DeployAttributeOwnedCertificateAsync(location, hash);
            await DeployNotOwnedCertificateAsync(location, hash);
        }

        private async Task DeployAttributeOwnedCertificateAsync(string location, string hash)
        {
            string owner = AddressFrom;
            string transactionHash = await
                CertificateService.DeployContractAsync(
                    Web3, AddressFrom, location, hash, _attribute.GetAddress(),
                    new HexBigInteger(3905820));

            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            _attributeOwnedCertificate = new CertificateService(Web3, receipt.ContractAddress);
        }

        private async Task DeployNotOwnedCertificateAsync(string location, string hash)
        {
            string owner = AddressFrom;
            string transactionHash = await
                CertificateService.DeployContractAsync(
                    Web3, AddressFrom, location, hash, owner,
                    new HexBigInteger(3905820));

            TransactionReceipt receipt = await
                Web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            _certificateNotOwnedByAttribute = new CertificateService(Web3, receipt.ContractAddress);
        }

    }
}
