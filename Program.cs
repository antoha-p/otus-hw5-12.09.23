using CsvSerializer.Serialiser;
using CsvSerializer.TestTypes;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CsvSerializer;

public class Program
{
    static void Main(string[] args)
    {
        //Написать сериализацию свойств или полей класса в строку CSV
        //Проверить на классе: class F { int i1, i2, i3, i4, i5; Get() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; }
        //Замерить время до и после вызова функции(для большей точности можно сериализацию сделать в цикле 100-100000 раз)
        //Вывести в консоль полученную строку и разницу времен
        //Отправить в чат полученное время с указанием среды разработки и количества итераций
        //Замерить время еще раз и вывести в консоль сколько потребовалось времени на вывод текста в консоль

        //Провести сериализацию с помощью каких-нибудь стандартных механизмов(например в JSON)
        //И тоже посчитать время и прислать результат сравнения

        //Написать десериализацию/загрузку данных из строки(ini/csv-файла) в экземпляр любого класса
        //Замерить время на десериализацию

        //Общий результат прислать в чат с преподавателем в системе в таком виде:
        //Сериализуемый класс: class F { int i1, i2, i3, i4, i5; }
        // код сериализации-десериализации: ...
        // количество замеров: 1000 итераций
        // мой рефлекшен:
        //  Время на сериализацию = 100 мс
        //  Время на десериализацию = 100 мс
        // стандартный механизм(NewtonsoftJson) :
        //  Время на сериализацию = 100 мс
        //  Время на десериализацию = 100 мс

        Console.WriteLine("Started");

        var testObject1 = new TestType1()
        {
            IntField = 1,
            StringField = "hello",
            FloatField = 7f,
            BoolField = true
        };

        var csv = Serializer.Serialize(testObject1);
        testObject1 = Serializer.Deserialize<TestType1>(csv);
        Console.WriteLine(csv);

        var f = F.Get();

        const int iterationsCount = 100000;

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        for (int i = 0; i < iterationsCount; i++)
        {
            csv = Serializer.Serialize(f);
        }

        stopWatch.Stop();
        Console.WriteLine($"Csv serialization time (nsec): {(long)(stopWatch.Elapsed.TotalNanoseconds / iterationsCount)}");

        stopWatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            var objFromCsv = Serializer.Deserialize<F>(csv);
        }
        stopWatch.Stop();
        Console.WriteLine($"Csv deserialization time (nsec): {(long)(stopWatch.Elapsed.TotalNanoseconds / iterationsCount)}");

        stopWatch.Restart();
        Console.WriteLine(csv);
        stopWatch.Stop();
        Console.WriteLine($"Console output time (nsec): {(long)stopWatch.Elapsed.TotalNanoseconds}");

        string json = string.Empty;
        stopWatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            json = JsonConvert.SerializeObject(f);
        }
        stopWatch.Stop();
        Console.WriteLine($"Json serialization time (nsec): {(long)(stopWatch.Elapsed.TotalNanoseconds / iterationsCount)}");

        stopWatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            var objFromJson = JsonConvert.DeserializeObject<F>(json);
        }
        stopWatch.Stop();
        Console.WriteLine($"Json deserialization time (nsec): {(long)(stopWatch.Elapsed.TotalNanoseconds / iterationsCount)}");
    }
}
