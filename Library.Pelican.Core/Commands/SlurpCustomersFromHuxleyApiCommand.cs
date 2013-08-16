using BrightSword.Pegasus.Commands.Core;

using Microsoft.WindowsAzure.Storage.Queue;

namespace Pelican.Commands
{
    public class SlurpCustomersFromHuxleyApiCommand : Command<SlurpCustomersFromHuxleyApiCommandArgument>
    {
        public SlurpCustomersFromHuxleyApiCommand(SlurpCustomersFromHuxleyApiCommandArgument argument)
            : base(argument) { }

        public SlurpCustomersFromHuxleyApiCommand(CloudQueueMessage message)
            : base(message) { }
    }
}