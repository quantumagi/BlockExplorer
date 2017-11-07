﻿using NBitcoin;
using NBitcoin.Protocol;
using Stratis.Bitcoin.Builder;
using Stratis.Bitcoin.Configuration;
using Stratis.Bitcoin.Features.IndexStore;
using Stratis.Bitcoin.Features.MemoryPool;
using Stratis.Bitcoin.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace Stratis.IndexStoreD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        public static async Task MainAsync(string[] args)
        {
            Network network = args.Contains("-testnet") ? Network.StratisTest : Network.StratisMain;
            NodeSettings nodeSettings = NodeSettings.FromArguments(args, "stratis", network, ProtocolVersion.ALT_PROTOCOL_VERSION);

            // NOTES: running BTC and STRAT side by side is not possible yet as the flags for serialization are static

            var node = new FullNodeBuilder()
                .UseNodeSettings(nodeSettings)
                .UseIndexStore()
                .UseMempool()
                .Build();

            await node.RunAsync();
        }
    }
}