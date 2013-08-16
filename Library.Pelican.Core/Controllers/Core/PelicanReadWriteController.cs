using System.Collections.Generic;
using System.Web.Http;

namespace Pelican.Controllers.Core
{
    public abstract class PelicanReadWriteController<T> : PelicanControllerBase,
                                                          IReadController<T>,
                                                          IWriteController<T>
    {
        public abstract IEnumerable<T> Get();
        public abstract T Get(string id);
        public abstract void Put([FromBody] T value);
    }
}