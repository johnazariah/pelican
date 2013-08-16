using System;

using BrightSword.Pegasus.API;

namespace Pelican.Commands
{
    public class PostItemInvoiceToHuxleyApiCommandArgument : ICommandArgument
    {
        public Guid CompanyFileId { get; set; }
    }
}