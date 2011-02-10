using System;
using System.IO;
using System.Linq;
using Tabasco.Plumbing;
using Tabasco.TemplateEngines;

namespace Tabasco
{
    public class Razor : IView
    {
        private readonly string _callingMethod;
        private readonly dynamic _model;
        private string _viewPath;

        public Razor()
        {
            _callingMethod = StackTracer.GetPreviousMethodName(GetType().GetConstructor(Type.EmptyTypes));
        }

        public Razor(dynamic model)
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
            FileInfo template;

            if (_viewPath != null)
            {
                template = new FileInfo(_viewPath);
            }
            else
            {
                var viewPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "views");
                var directoryInfo = new DirectoryInfo(viewPath);
                template = GetDefaultTemplateFile(directoryInfo);
            }

            var viewType = typeof(RazorView);

            RazorEngine.Razor.SetTemplateBase(viewType);

            string templateString;
            using (var streamReader = new StreamReader(template.FullName))
            {
                templateString = streamReader.ReadToEnd();
            }

            RazorEngine.Razor.SetActivator(t =>
                                               {
                                                   dynamic view = Activator.CreateInstance(t);
                                                   if (_model != null)
                                                   {
                                                       view.Model = _model;
                                                   }

                                                   return view;
                                               });

            var viewString = RazorEngine.Razor.Parse(templateString, _model);

            action(viewString);
        }

        private FileInfo GetDefaultTemplateFile(DirectoryInfo directoryInfo)
        {
            var method = _callingMethod.Substring(_callingMethod.LastIndexOf('.') + 1);

            var template = directoryInfo.GetFiles().Where(
                file => file.Name.ToLowerInvariant() == method.ToLowerInvariant() + ".cshtml").First();

            return template;
        }
    }
}