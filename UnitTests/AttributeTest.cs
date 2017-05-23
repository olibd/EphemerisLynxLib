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
                transactionHash = await _attribute.AddCertificateAsync(addressFrom, _certificateNotOwnedByAttribute.GetAddress(), new HexBigInteger(3905820));
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
                transactionHash = await _attribute.AddCertificateAsync(addressFrom, _attributeOwnedCertificate.GetAddress(), new HexBigInteger(3905820));
            }
            catch (RpcResponseException e)
            {
                Assert.Fail();
            }

            Assert.NotNull(transactionHash);
            string callResult = await _attribute.GetCertificateAsyncCall(addressFrom);
            Assert.AreEqual(callResult, _attributeOwnedCertificate.GetAddress());
        }

        private async Task DeployAttributeAsync(string location, string hash)
        {
            string owner = addressFrom;
            string transactionHash = await
                AttributeService.DeployContractAsync(
                    web3, addressFrom, location, hash, owner,
                    new HexBigInteger(3905820));

            TransactionReceipt receipt = await
                web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            _attribute = new AttributeService(web3, receipt.ContractAddress);
        }

        private async Task DeployCertificatesAsync(string location, string hash)
        {
            await DeployAttributeOwnedCertificateAsync(location, hash);
            await DeployNotOwnedCertificateAsync(location, hash);
        }

        private async Task DeployAttributeOwnedCertificateAsync(string location, string hash)
        {
            string owner = addressFrom;
            string transactionHash = await
                CertificateService.DeployContractAsync(
                    web3, addressFrom, location, hash, _attribute.GetAddress(),
                    new HexBigInteger(3905820));

            TransactionReceipt receipt = await
                web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            _attributeOwnedCertificate = new CertificateService(web3, receipt.ContractAddress);
        }

        private async Task DeployNotOwnedCertificateAsync(string location, string hash)
        {
            string owner = addressFrom;
            string transactionHash = await
                CertificateService.DeployContractAsync(
                    web3, addressFrom, location, hash, owner,
                    new HexBigInteger(3905820));

            TransactionReceipt receipt = await
                web3.Eth.Transactions.GetTransactionReceipt.
                    SendRequestAsync(transactionHash);

            _certificateNotOwnedByAttribute = new CertificateService(web3, receipt.ContractAddress);
        }

    }
}
