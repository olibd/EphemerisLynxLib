using NUnit.Framework;
using System;
using Nethereum;
using Nethereum.Web3;
using Nethereum.TestRPCRunner;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture()]
    public class FactoryTest
    {
        Web3 web3;
        TestRPCEmbeddedRunner launcher;

        [SetUp]
        public async Task SetupAsync()
        {
            //We configure embeded test rpc instance
            launcher = new TestRPCEmbeddedRunner();
            launcher.RedirectOuputToDebugWindow = true;
            launcher.Arguments = "--port 8545";
            //We configure embeded test rpc instance
            launcher.StartTestRPC();
            web3 = new Web3();

            //get first account
            var addressFrom = (await web3.Eth.Accounts.SendRequestAsync())[0];

        }

		[TearDown]
		public void TearDown()
		{
            launcher.StopTestRPC();
		}

        [Test()]
        public void TestCase()
        {
            Assert.Fail();
        }



    }
}
