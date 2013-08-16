using System;
using System.Collections.Generic;

using Pelican.Configuration;

namespace Pelican.Commands
{
    public interface IHuxleyApiSlurper<out T>
    {
        void Slurp(Action<T> processItem,
                   PelicanContext context = null,
                   Guid? companyFileId = null);
    }
}