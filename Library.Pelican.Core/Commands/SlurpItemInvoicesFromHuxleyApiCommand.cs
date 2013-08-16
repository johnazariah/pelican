using BrightSword.Pegasus.Commands.Core;

using Microsoft.WindowsAzure.Storage.Queue;

namespace Pelican.Commands
{
    public class SlurpItemInvoicesFromHuxleyApiCommand : Command<SlurpItemInvoicesFromHuxleyApiCommandArgument>
    {
        public SlurpItemInvoicesFromHuxleyApiCommand(SlurpItemInvoicesFromHuxleyApiCommandArgument argument)
            : base(argument) { }

        public SlurpItemInvoicesFromHuxleyApiCommand(CloudQueueMessage message)
            : base(message) { }
    }
}