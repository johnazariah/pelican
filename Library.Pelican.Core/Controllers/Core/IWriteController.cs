using System.Web.Http;

namespace Pelican.Controllers.Core
{
    public interface IWriteController<in T>
    {
        void Put([FromBody] T value);
    }
}