using System;

namespace Tabasco
{
    public abstract class ActionAttribute : Attribute
    {
        public string ActionRoute { get; set; }
    }

    public class GetAttribute : ActionAttribute
    {
        public GetAttribute(string actionRoute)
        {
            ActionRoute = actionRoute;
        }
    }

    public class PostAttribute : ActionAttribute
    {
        public PostAttribute(string actionRoute)
        {
            ActionRoute = actionRoute;
        }
    }

    public class PutAttribute : ActionAttribute
    {
        public PutAttribute(string actionRoute)
        {
            ActionRoute = actionRoute;
        }
    }
    public class DeleteAttribute : ActionAttribute
    {
        public DeleteAttribute(string actionRoute)
        {
            ActionRoute = actionRoute;
        }
    }}