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
    public class CertificateTest
    {
        Web3 web3;
        TestRPCEmbeddedRunner launcher;
        AttributeService attribute;
        CertificateService certificate;
        string addressFrom;
        string addressFromNotOwner;

        [SetUp]
        public async Task Setup()
        {
            //We configure embeded test rpc instance
            launcher = new TestRPCEmbeddedRunner();
            launcher.RedirectOuputToDebugWindow = true;
            launcher.Arguments = "--port 8545";
            //We configure embeded test rpc instance
            //launcher.StartTestRPC();

            web3 = new Web3();

            //Give time for the process start
            Thread.Sleep(2000);

            //get first account
            addressFrom = (await web3.Eth.Accounts.SendRequestAsync())[0];
            addressFromNotOwner = (await web3.Eth.Accounts.SendRequestAsync())[1];

            //Deploy attribute
            string transactionHash = await AttributeService.DeployContractAsync(web3, addressFrom, "neverland", "5678", addressFrom, new HexBigInteger(3000000));
            TransactionReceipt receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            string attributeAddress = receipt.ContractAddress;

            //Deploy certificate
            transactionHash = await CertificateService.DeployContractAsync(web3, addressFrom, "somewhere", "1234", attributeAddress, new HexBigInteger(3000000));
            receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            string certificateAddress = receipt.ContractAddress;

            //Create service objects
            attribute = new AttributeService(web3, attributeAddress);
            certificate = new CertificateService(web3, certificateAddress);

        }

        [TearDown]
        public void TearDown()
        {
            launcher.StopTestRPC();
        }

        [Test()]
        public async Task TestRevoke()
        {
            //TODO: Test the event (once it is implemented in the library)

            bool revoked = await certificate.GetFunctionRevoked().CallAsync<bool>();
            Assert.False(revoked);

            await certificate.RevokeAsync(addressFromNotOwner, new HexBigInteger(3000000));
             
            revoked = await certificate.GetFunctionRevoked().CallAsync<bool>();
            Assert.False(revoked);

            await certificate.RevokeAsync(addressFrom, new HexBigInteger(3000000));
             
            revoked = await certificate.GetFunctionRevoked().CallAsync<bool>();
            Assert.True(revoked);
        }
    }
}
