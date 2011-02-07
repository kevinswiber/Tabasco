using System;
using System.IO;
using System.Linq;
using System.Text;
using Spark;
using Spark.FileSystem;
using Tabasco.ViewEngines.Spark;

namespace Tabasco
{
    public class Spark : IView
    {
        private readonly string _callingMethod;
        private readonly dynamic _model;
        private string _viewPath;

        public Spark()
        {
            _callingMethod = StackTracer.GetPreviousMethodName(GetType().GetConstructor(Type.EmptyTypes));
        }

        public Spark(dynamic model)
        {
            _callingMethod =
                StackTracer.GetPreviousMethodName(GetType().GetConstructors().Where(ci => ci.GetParameters().Count() == 1).First());
            _model = model;
        }

        public IView Template(string viewPath)
        {
            _viewPath = viewPath;
            return this;
        }

        public void Each(Action<dynamic> action)
        {
            string viewPath;
            FileInfo template;
            DirectoryInfo directoryInfo;

            if (_viewPath != null)
            {
                template = new FileInfo(_viewPath);
                viewPath = template.DirectoryName;
                directoryInfo = new DirectoryInfo(viewPath);
            }
            else
            {
                viewPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "views");
                directoryInfo = new DirectoryInfo(viewPath);
                template = GetDefaultTemplateFile(directoryInfo);
            }

            var typeName = typeof(SparkView).FullName;

            if (_model != null)
            {
                Type modelType = _model.GetType();
                //var viewType = typeof(SparkView<>).MakeGenericType(modelType);
                typeName = "Tabasco.ViewEngines.Spark.SparkView<" + modelType.FullName + ">";
            }

            var engine = new SparkViewEngine
                         {
                             DefaultPageBaseType = typeName,
                             ViewFolder = new FileSystemViewFolder(directoryInfo.FullName)
                         };


            var descriptor = new SparkViewDescriptor().AddTemplate(template.Name);
            var viewInstance = engine.CreateInstance(descriptor);

            dynamic view = viewInstance;

            if (_model != null && typeName != null)
            {
                //view = Convert.ChangeType(viewInstance, Type.GetType(typeof(SparkView<>).MakeGenericType(_model.GetType)));
                view.Model = _model;
            }

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream, Encoding.UTF8);

            view.RenderView(writer);

            writer.Flush();

            var reader = new StreamReader(stream);

            stream.Position = 0;

            var viewString = reader.ReadToEnd();

            action(viewString);

            writer.Dispose();
            reader.Dispose();
            stream.Dispose();
        }

        private FileInfo GetDefaultTemplateFile(DirectoryInfo directoryInfo)
        {
            var method = _callingMethod.Substring(_callingMethod.LastIndexOf('.') + 1);

            var template = directoryInfo.GetFiles().Where(
                file => file.Name.ToLowerInvariant() == method.ToLowerInvariant() + ".spark").First();

            return template;
        }
    }
}