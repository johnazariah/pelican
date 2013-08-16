using System;

using BrightSword.Pegasus.API;

namespace Pelican.Commands
{
    public class SlurpItemsFromHuxleyApiCommandArgument : ICommandArgument
    {
        public Guid CompanyFileId { get; set; }
    }
}