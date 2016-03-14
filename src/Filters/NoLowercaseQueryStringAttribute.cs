using System;
using Microsoft.AspNet.Mvc.Filters;

namespace ifesenko.com.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class NoLowercaseQueryStringAttribute : Attribute, IFilterMetadata
    {
    }
}
