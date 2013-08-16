using System;

using BrightSword.Pegasus.API;

namespace Pelican.Commands
{
    public class SlurpAccountsFromHuxleyApiCommandArgument : ICommandArgument
    {
        public Guid CompanyFileId { get; set; }
    }
}