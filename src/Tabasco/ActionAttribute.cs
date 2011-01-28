using System;

namespace Tabasco
{
    public abstract class ActionAttribute : Attribute
    {
        protected ActionAttribute() : this(string.Empty) { }

        protected ActionAttribute(string actionRoute)
        {
            ActionRoute = actionRoute;
        }

        public string ActionRoute { get; set; }
    }

    public class GetAttribute : ActionAttribute
    {
        public GetAttribute()
        { }

        public GetAttribute(string actionRoute)
            : base(actionRoute)
        { }
    }

    public class PostAttribute : ActionAttribute
    {
        public PostAttribute()
        { }

        public PostAttribute(string actionRoute)
            : base(actionRoute)
        { }
    }

    public class PutAttribute : ActionAttribute
    {
        public PutAttribute()
        { }

        public PutAttribute(string actionRoute)
            : base(actionRoute)
        { }
    }

    public class DeleteAttribute : ActionAttribute
    {
        public DeleteAttribute()
        { }

        public DeleteAttribute(string actionRoute)
            : base(actionRoute)
        { }
    }

    public class HeadAttribute : ActionAttribute
    {
        public HeadAttribute()
        { }

        public HeadAttribute(string actionRoute)
            : base(actionRoute)
        { }
    }
}