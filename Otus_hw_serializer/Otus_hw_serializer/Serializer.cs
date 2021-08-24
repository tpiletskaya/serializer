using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Otus_hw_serializer
{
    public static class CustomCsvSerializer
    {
        private const string Separator = ",";
        private const string RowEnd = "\n";

        private static BindingFlags _bindingFlags => BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        public static string Serialize(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Input object should be not null");

            var type = obj.GetType();

            if (type.Namespace.StartsWith("System")) return obj.ToString();

            var fields = type.GetFields(_bindingFlags);
            var properties = type.GetProperties(_bindingFlags);

            var header = new StringBuilder();
            var values = new StringBuilder();

            if (fields.Length > 0)
            {
                header.Append(string.Join(Separator, fields.Select(x => x.Name)));
                values.Append(string.Join(Separator, fields.Select(x => x.GetValue(obj)?.ToString())));
            }

            if (properties.Length > 0)
            {
                if (header.Length > 0)
                {
                    header.Append(Separator);
                    values.Append(Separator);
                }

                header.Append(string.Join(Separator, properties.Select(x => x.Name)));
                values.Append(string.Join(Separator, properties.Select(x => x.GetValue(obj)?.ToString())));
            }

            header.Append(RowEnd);

            return $"{header}{values}";
        }


        public static TType Deserialize<TType>(string csv) where TType : class
        {
            var type = typeof(TType);

            var entity = Activator.CreateInstance<TType>();

            var rows = csv.Split(RowEnd);

            var columns = rows[0].Split(Separator);
            var values = rows[1].Split(Separator);

            var fieldsCount = type.GetFields(_bindingFlags).Length;

            for (int i = 0; i < fieldsCount; i++)
            {
                var field = type.GetField(columns[i], _bindingFlags);
                var fieldType = field.FieldType;
                field.SetValue(entity, Convert.ChangeType(values[i], fieldType));
            }

            for (int i = fieldsCount; i < columns.Length; i++)
            {
                var property = type.GetProperty(columns[i], _bindingFlags);
                var propertyType = property.PropertyType;
                property.SetValue(entity, Convert.ChangeType(values[i], propertyType));
            }

            return entity;
        }
    }
}
