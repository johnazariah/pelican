using System;

using BrightSword.Pegasus.API;

namespace Pelican.Commands
{
    public class SlurpCustomersFromHuxleyApiCommandArgument : ICommandArgument
    {
        public Guid CompanyFileId { get; set; }
    }
}