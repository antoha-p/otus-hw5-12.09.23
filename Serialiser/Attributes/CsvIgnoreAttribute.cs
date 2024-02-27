namespace CsvSerializer.Serialiser.Attributes;

/// <summary>
/// Аттрибут игнорирования сериализации в CSV. Если он указан, то поле не сериализуется.
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class CsvIgnoreAttribute : Attribute
{
}
