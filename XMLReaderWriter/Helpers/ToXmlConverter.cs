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
        public static XElement ParseToXElement<T>(this T element, string name, List<Type> types)
        {
            var tp = types.FirstOrDefault(_ => _ == element.GetType());
            if (tp != null)
                return ParseToXml(element, name, tp);
            return null;
        }

        private static XElement ParseToXml<T>(T element, string name, Type type)
        {
            var xElement = new XElement(name, new XAttribute("type", type.Name));
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            if (properties.Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(RequiredAttribute)))
                .Any(p => p.GetValue(element) == null))
            {
                return null;
            }

            properties.ForEach(_=>
            {
                var val = _.GetValue(element);
                var pname = _.Name;
                if (val == null) return;

                if (_.PropertyType == typeof(List<string>))
                    xElement.Add(new XElement(pname, ((List<string>) val).Select(v =>
                        new XElement(pname.Substring(0, pname.Length - 1), v))));
                else
                    xElement.Add(new XElement(pname, val));
            });

            return xElement;
        }
    }
}
