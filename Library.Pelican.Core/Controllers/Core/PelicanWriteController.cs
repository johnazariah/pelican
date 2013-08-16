using System.Web.Http;

namespace Pelican.Controllers.Core
{
    public abstract class PelicanWriteController<T> : PelicanControllerBase,
                                                      IWriteController<T>
    {
        public abstract void Put([FromBody] T value);
    }
}