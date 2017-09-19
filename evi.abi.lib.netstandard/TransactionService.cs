using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace eVi.abi.lib.pcl
{
    class TransactionService : ITransactionService
    {
        private const int EstimateMultiplier = 3;

        private string _privateKey;
        private string _addressFrom;
        private Web3 _web3;

        public TransactionService(string key, Web3 web3)
        {
            _web3 = web3;
            _privateKey = key;
            _addressFrom = web3.GetAddressFromPrivateKey(_privateKey);
        }

        public async Task<string> SignAndSendTransaction(string data, string to, HexBigInteger value = null, HexBigInteger gasPrice = null)
        {
            value = value ?? new HexBigInteger(0);
            gasPrice = gasPrice ?? new HexBigInteger(0);

            HexBigInteger gasLimit = await EstimateGasLimit(data, to);

            HexBigInteger nonce = await _web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(_addressFrom);
            string transaction = _web3.OfflineTransactionSigning.SignTransaction(_privateKey, to, value, nonce, gasPrice, gasLimit, data);
            return await _web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + transaction);
        }

        private async Task<HexBigInteger> EstimateGasLimit(string data, string to)
        {

            EthTransactionsService txService = new EthTransactionsService(_web3.Client);
            HexBigInteger gasEstimate = await txService.EstimateGas.SendRequestAsync(String.IsNullOrEmpty(to)
                ? new CallInput {Data = data}
                : new CallInput {Data = data, To = to});

            //Use this to compensate for some transactions using slightly more gas than estimated
            BigInteger gasLimit = gasEstimate.Value * EstimateMultiplier;

            return new HexBigInteger(gasLimit);
        }
    }
}
