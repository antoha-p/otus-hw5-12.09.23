using CsvSerializer.Serialiser.Attributes;
using System.Reflection;

namespace CsvSerializer.Serialiser;

public static class Serializer
{
    public static string Serialize(object obj)
    {
        var result = new List<string>();

        var type = obj.GetType();
        var fileds = type.GetFields(BindingFlags.Instance | BindingFlags.Public);

        foreach (var field in fileds)
        {
            if (field.GetCustomAttributes<CsvIgnoreAttribute>(true).Any())
                continue;

            var value = field.GetValue(obj)?.ToString() ?? string.Empty;
            result.Add($"{value}");
        }

        return string.Join(",", result);
    }

    public static T Deserialize<T>(string csv) where T : new()
    {
        var result = new T();

        var type = typeof(T);
        var fileds = type.GetFields(BindingFlags.Instance | BindingFlags.Public);

        var data = csv.Split(',');
        var dataIndex = 0;

        foreach (var field in fileds)
        {
            if (field.GetCustomAttributes<CsvIgnoreAttribute>(true).Any())
            {
                dataIndex++;
                continue;
            }

            var value = data[dataIndex];
            dataIndex++;
      
            var parseMethod = field.FieldType
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .FirstOrDefault(x => x.ToString().Contains("Parse(System.String)"));

            var obj = parseMethod?.Invoke(null, [value]) ?? value;
            field.SetValue(result, obj);
        }

        return result;
    }
}
