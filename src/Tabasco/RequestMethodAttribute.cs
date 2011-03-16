using System;

namespace Tabasco
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class RequestMethodAttribute : Attribute
    {
        protected RequestMethodAttribute() : this(string.Empty) { }

        protected RequestMethodAttribute(string actionRoute)
        {
            ActionRoute = actionRoute;
        }

        public string ActionRoute { get; set; }
    }

    public class GetAttribute : RequestMethodAttribute
    {
        public GetAttribute()
        { }

        public GetAttribute(string actionRoute)
            : base(actionRoute)
        { }
    }

    public class PostAttribute : RequestMethodAttribute
    {
        public PostAttribute()
        { }

        public PostAttribute(string actionRoute)
            : base(actionRoute)
        { }
    }

    public class PutAttribute : RequestMethodAttribute
    {
        public PutAttribute()
        { }

        public PutAttribute(string actionRoute)
            : base(actionRoute)
        { }
    }

    public class DeleteAttribute : RequestMethodAttribute
    {
        public DeleteAttribute()
        { }

        public DeleteAttribute(string actionRoute)
            : base(actionRoute)
        { }
    }

    public class HeadAttribute : RequestMethodAttribute
    {
        public HeadAttribute()
        { }

        public HeadAttribute(string actionRoute)
            : base(actionRoute)
        { }
    }
}