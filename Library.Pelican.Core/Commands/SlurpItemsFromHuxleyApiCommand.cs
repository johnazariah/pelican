using BrightSword.Pegasus.Commands.Core;

using Microsoft.WindowsAzure.Storage.Queue;

namespace Pelican.Commands
{
    public class SlurpItemsFromHuxleyApiCommand : Command<SlurpItemsFromHuxleyApiCommandArgument>
    {
        public SlurpItemsFromHuxleyApiCommand(SlurpItemsFromHuxleyApiCommandArgument argument)
            : base(argument) { }

        public SlurpItemsFromHuxleyApiCommand(CloudQueueMessage message)
            : base(message) { }
    }
}