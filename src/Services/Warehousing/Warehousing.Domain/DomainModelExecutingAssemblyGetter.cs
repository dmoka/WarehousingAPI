using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Warehousing.Domain
{
    public class DomainModelExecutingAssemblyGetter
    {
        public Assembly Get()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
