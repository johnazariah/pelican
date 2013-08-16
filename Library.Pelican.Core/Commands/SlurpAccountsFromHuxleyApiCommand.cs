using BrightSword.Pegasus.Commands.Core;

using Microsoft.WindowsAzure.Storage.Queue;

namespace Pelican.Commands
{
    public class SlurpAccountsFromHuxleyApiCommand : Command<SlurpAccountsFromHuxleyApiCommandArgument>
    {
        public SlurpAccountsFromHuxleyApiCommand(SlurpAccountsFromHuxleyApiCommandArgument argument)
            : base(argument) {}

        public SlurpAccountsFromHuxleyApiCommand(CloudQueueMessage message)
            : base(message) {}
    }
}