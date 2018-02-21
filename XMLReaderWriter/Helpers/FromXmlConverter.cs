using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using XMLReaderWriter.LibraryLogger;

namespace XMLReaderWriter.Helpers
{
    public static class FromXmlConverter
    {
        /// <summary>
        /// Extention method for XElement type that parse XElement to subclass of baseclass T 
        /// </summary>
        /// <typeparam name="T">Base class of library</typeparam>
        /// <param name="element">XElement</param>
        /// <returns>Instance of base class</returns>
        public static T ParseTo<T>(this XElement element) where T: class
        {
            var tp = typeof(T);
            var types = Assembly.GetAssembly(tp).GetTypes().Where(t => tp.IsAssignableFrom(t)).ToList();
            var typeattr = element.Attribute("type")?.Value;
            if (typeattr == null) return null;
            var type = types.FirstOrDefault(_ => _.Name == typeattr);
            return type != null ? ParseToBase<T>(element, type) : null;
        }

        private static T ParseToBase<T>(XElement element, Type type) where T: class 
        {
            var entity = (T)Activator.CreateInstance(type);
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();

            foreach (var property in properties)
            {
                var val = element.Element(property.Name)?.Value;
                if (property.CustomAttributes.Any(a => a.AttributeType == typeof(RequiredAttribute)) 
                    && string.IsNullOrWhiteSpace(val))
                {
                    return null;
                }

                if(string.IsNullOrWhiteSpace(val)) continue;
                var ptype = property.PropertyType;
                if ( ptype == typeof(List<string>))
                {
                    property.SetValue(entity, element.Element(property.Name)?.Elements().Select(_ => _.Value).ToList());
                }
                else
                {
                    if (ptype == typeof(int))
                    {
                        property.SetValue(entity, XmlConvert.ToInt32(val));
                    }
                    else
                    {
                        if (ptype == typeof(DateTime))
                        {
                            property.SetValue(entity, XmlConvert.ToDateTime(val, XmlDateTimeSerializationMode.Local));
                        }
                        else
                        {
                            property.SetValue(entity, val);
                        }
                    }
                }
            }

            return entity;
        }
    }
}
