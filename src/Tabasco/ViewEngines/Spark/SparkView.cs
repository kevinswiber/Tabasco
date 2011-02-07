using Spark;

namespace Tabasco.ViewEngines.Spark
{
    public abstract class SparkView : AbstractSparkView
    {
    }

    public abstract class SparkView<TModel> : AbstractSparkView
    {
        public TModel Model { get; set; }
    }
}