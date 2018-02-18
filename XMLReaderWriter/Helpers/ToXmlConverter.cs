using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System;

namespace XMLReaderWriter.Helpers
{
    public static class ToXmlConverter
    {
        /// <summary>
        /// Extention method for generic type that parse subclass of baseclass T to XElement
        /// </summary>
        /// <typeparam name="T">Base class of library</typeparam>
        /// <param name="element">Instance of base class</param>
        /// <param name="name">Name of xml element</param>
        /// <returns>XElement</returns>
        public static XElement ParseToXElement<T>(this T element, string name)
        {
            var tp = typeof(T);
            var types = Assembly.GetAssembly(tp).GetTypes().Where(t => tp.IsAssignableFrom(t)).ToList();
            var type = types.FirstOrDefault(_ => _ == element.GetType());
            return type != null ? ParseToXml(element, name, type) : null;
        }

        private static XElement ParseToXml<T>(T element, string name, Type type)
        {
            var xElement = new XElement(name, new XAttribute("type", type.Name));
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            if (properties.Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(RequiredAttribute)))
                .Any(p => p.GetValue(element) == null ||
                          p.PropertyType.IsValueType && p.GetValue(element).Equals(p.PropertyType.GetDefault())))
            {
                return null;
            }

            properties.ForEach(_ =>
            {
                var val = _.GetValue(element);
                var pname = _.Name;
                var defval = _.PropertyType.IsValueType ?_.PropertyType.GetDefault() : null;
                if (val != null && !val.Equals(defval))
                {
                    if (_.PropertyType == typeof(List<string>))
                        xElement.Add(new XElement(pname, ((List<string>) val).Select(v =>
                            new XElement(pname.Substring(0, pname.Length - 1), v))));
                    else
                        xElement.Add(new XElement(pname, val));
                }
            });

            return xElement;
        }

        public static object GetDefault(this Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
