using System;
using Microsoft.AspNet.Mvc;

namespace ifesenko.com.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class NoCacheAttribute : ResponseCacheAttribute
    {
        public NoCacheAttribute()
        {
            this.NoStore = true;
            // Duration = 0 by default.
            // VaryByParam = "*" by default.
        }
    }
}
