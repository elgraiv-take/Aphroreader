using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Elgraiv.Aphroreader.Util
{
    static class DataTemplateUtil
    {
        public static void RegisterDataTemplate(ResourceDictionary resources, Assembly assembly)
        {
            foreach(var type in assembly.DefinedTypes)
            {
                var attribute = type.GetCustomAttribute<DataTemplateTargetAttribute>();
                if (attribute != null)
                {
                    RegisterDataTemplate(resources, type, attribute.ViewType);
                }
            }
        }
        public static void RegisterDataTemplate(ResourceDictionary resources, Type viewModelType, Type viewType)
        {
            resources.Add(
                new DataTemplateKey(viewModelType),
                new DataTemplate(viewModelType)
                {
                    VisualTree = new FrameworkElementFactory(viewType)
                }
                );
        }
    }

    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    class DataTemplateTargetAttribute : Attribute
    {
        public Type ViewType { get; }
        public DataTemplateTargetAttribute(Type viewType)
        {
            ViewType = viewType;
        }
    }
}
