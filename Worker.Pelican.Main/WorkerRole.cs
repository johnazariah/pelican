using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrightSword.Pegasus.API;
using BrightSword.Pegasus.Core.Role;

using Pelican.Configuration;

namespace Worker.Pelican.Main
{
    public class WorkerRole : CloudRunnerRole
    {
        private readonly PelicanContext _context;

        public WorkerRole()
        {
            _context = PelicanContext.CreateFromCloudConfiguration();
        }

        protected override ICloudRunnerContext Context
        {
            get { return _context; }
        }

        protected override int PeriodInMilliseconds
        {
            get { return 3 * 1000; }
        }
    }
}
