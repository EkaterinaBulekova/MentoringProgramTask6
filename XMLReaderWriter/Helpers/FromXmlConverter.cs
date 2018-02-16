using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace XMLReaderWriter.Helpers
{
    public static class FromXmlConverter
    {
        public static T ParseToBaseElement<T>(this XElement element, List<Type> types) where T: class 
        {
            var typeattr = element.Attribute("type")?.Value;
            if (typeattr != null)
            {
                var tp = types.FirstOrDefault(_ => _.Name == typeattr);
                if (tp != null)
                return ParseToBase<T>(element, tp);
            }

            return null;
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
