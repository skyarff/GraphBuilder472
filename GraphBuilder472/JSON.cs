using Newtonsoft.Json;
using System;
using System.ComponentModel;
using static GraphBuilder472.Graph;

namespace GraphBuilder472
{
    

    public class NodeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string nodeString = (string)value;

                // Ваш код преобразования строки в объект типа Node
                // Например, если Node имеет свойство Name, можно создать экземпляр Node с помощью строки в качестве имени:
                 

                return new Node { name = nodeString };
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
