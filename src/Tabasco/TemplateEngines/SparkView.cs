using Spark;

namespace Tabasco.TemplateEngines
{
    public abstract class SparkView : AbstractSparkView
    {
    }

    public abstract class SparkView<TModel> : AbstractSparkView
    {
        public TModel Model { get; set; }
    }
}