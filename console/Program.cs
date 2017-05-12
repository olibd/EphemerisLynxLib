using System;
using Nethereum.TestRPCRunner;
using Nethereum.Web3;

namespace console
{
    class MainClass
    {

        static string addressFrom;
        static Web3 web3;
        static TestRPCEmbeddedRunner launcher;
		public static void Main(string[] args)
		{

            getAccountAsync().Wait();


			launcher.StopTestRPC();
		}


        public static async System.Threading.Tasks.Task getAccountAsync()
        {
			launcher = new TestRPCEmbeddedRunner();
			launcher.RedirectOuputToDebugWindow = true;
			launcher.Arguments = "--port 8545";
			launcher.StartTestRPC();

			Console.ReadLine();

			web3 = new Web3();
            //get first account
            addressFrom = (await web3.Eth.Accounts.SendRequestAsync())[0];
        }
    }
}
