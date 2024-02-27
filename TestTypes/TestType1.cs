using CsvSerializer.Serialiser.Attributes;

namespace CsvSerializer.TestTypes;

public class TestType1
{
    public int IntField;

    public string StringField = null!;

    public float FloatField;

    public bool BoolField;

    [CsvIgnore]
    public bool BoolFieldIgnored;
}
