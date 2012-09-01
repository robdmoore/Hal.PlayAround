using System;

namespace WebApi.Hal.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class LinkedResourceAttribute : Attribute
    {
        readonly string _resourceId;

        public LinkedResourceAttribute(string resourceId)
        {
            _resourceId = resourceId;
        }

        public string Resource
        {
            get { return _resourceId; }
        }
    }
}
