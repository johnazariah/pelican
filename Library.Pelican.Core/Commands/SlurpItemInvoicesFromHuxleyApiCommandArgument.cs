using System;

using BrightSword.Pegasus.API;

namespace Pelican.Commands
{
    public class SlurpItemInvoicesFromHuxleyApiCommandArgument : ICommandArgument
    {
        public Guid CompanyFileId { get; set; }
    }
}