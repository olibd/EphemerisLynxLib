using System;
using Nethereum.TestRPCRunner;
using Nethereum.Web3;
using System.Threading;

namespace UnitTests
{
    public abstract class AutonomousTest
    {
        public string AddressFrom { get; set; }
        public Web3 Web3 { get; set; }
        public TestRPCEmbeddedRunner Launcher { get; set; }

        public void LaunchTestRPC()
        {
            //We configure embeded test rpc instance
            Launcher = new TestRPCEmbeddedRunner();
            Launcher.RedirectOuputToDebugWindow = true;
            Launcher.Arguments = "--port 8545";
            //We configure embeded test rpc instance
            Launcher.StartTestRPC();
        }

        public void LaunchWeb3()
        {
            Web3 = new Web3();
        }

        public async System.Threading.Tasks.Task InitAutonomousTestAsync()
        {
            LaunchTestRPC();
            LaunchWeb3();
            //Give time for the process start
            Thread.Sleep(2000);

            //get first account
            AddressFrom = (await Web3.Eth.Accounts.SendRequestAsync())[0];
        }

        public void StopAutonomousTest()
        {
            Launcher.StopTestRPC();
            //Give time for the process to stop
            Thread.Sleep(2000);
        }
    }
}
