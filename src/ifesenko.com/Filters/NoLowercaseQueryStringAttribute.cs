using System;
using Microsoft.AspNet.Mvc.Filters;

namespace IfesenkoDotCom.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class NoLowercaseQueryStringAttribute : Attribute, IFilterMetadata
    {
    }
}
