using System.Collections.Generic;

namespace Pelican.Controllers.Core
{
    public interface IReadController<out T>
    {
        IEnumerable<T> Get();
        T Get(string id);
    }
}