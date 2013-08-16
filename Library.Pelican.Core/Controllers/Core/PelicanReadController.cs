using System.Collections.Generic;

namespace Pelican.Controllers.Core
{
    public abstract class PelicanReadController<T> : PelicanControllerBase,
                                                     IReadController<T>
    {
        public abstract IEnumerable<T> Get();
        public abstract T Get(string id);
    }
}