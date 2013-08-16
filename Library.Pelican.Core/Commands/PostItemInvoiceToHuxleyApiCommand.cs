using BrightSword.Pegasus.API;

namespace Pelican.Commands
{
    public class PostItemInvoiceToHuxleyApiCommand : ICommand
    {
        public string CommandId { get; private set; }
        public string CommandName { get; private set; }
        public string CommandArgumentTypeName { get; private set; }
        public string SerializedCommandArgument { get; private set; }
    }
}