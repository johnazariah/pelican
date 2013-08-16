using System.Web.Http;

using Pelican.Configuration;
using Pelican.Service;

namespace Pelican.Controllers.Core
{
    public abstract class PelicanControllerBase : ApiController
    {
        protected PelicanControllerBase()
        {
            PelicanService = new PelicanService(PelicanContext.CreateFromApplicationSettings());
        }

        public PelicanService PelicanService { get; private set; }
    }
}