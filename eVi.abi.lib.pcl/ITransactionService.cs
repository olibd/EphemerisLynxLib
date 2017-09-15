using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;

namespace eVi.abi.lib.pcl
{
    interface ITransactionService
    {
        Task<string> SignAndSendTransaction(string data, string to, HexBigInteger value = null, HexBigInteger gasPrice = null);
    }
}