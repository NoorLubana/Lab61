using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

public class Event
{
    public int EventNumber { get; set; }
    public string Location { get; set; }
}

class Program
{
    static void Main()
    {
        // Task 2
        Event eventObj = new Event { EventNumber = 1, Location = "Calgary" };

        // Task 3
        SerializeObjectToJsonFile(eventObj, "event.txt");

        // Task 4
        Event deserializedEvent = DeserializeObjectFromJsonFile<Event>("event.txt");
        Console.WriteLine(deserializedEvent.EventNumber);
        Console.WriteLine(deserializedEvent.Location);

        // Task 5
        List<Event> eventsList = new List<Event>
        {
            new Event { EventNumber = 1, Location = "Calgary" },
            new Event { EventNumber = 2, Location = "Vancouver" },
            new Event { EventNumber = 3, Location = "Toronto" },
            new Event { EventNumber = 4, Location = "Edmonton" },
        };

        SerializeObjectToJsonFile(eventsList, "eventsList.json");

        List<Event> deserializedEventsList = DeserializeObjectFromJsonFile<List<Event>>("eventsList.json");
        foreach (var evt in deserializedEventsList)
        {
            Console.WriteLine($"{evt.EventNumber} {evt.Location}");
        }

        // Task 6
        ReadFromFile();
    }

    static void SerializeObjectToJsonFile<T>(T obj, string fileName)
    {
        using (FileStream fs = File.Create(fileName))
        {
            JsonSerializer.SerializeAsync(fs, obj).Wait();
        }
    }

    static T DeserializeObjectFromJsonFile<T>(string fileName)
    {
        byte[] jsonBytes = File.ReadAllBytes(fileName);
        ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(jsonBytes);
        return JsonSerializer.Deserialize<T>(readOnlySpan);
    }

    static void ReadFromFile()
    {
        string fileContent = "Hackathon";

        using (FileStream fileStream = new FileStream("hackathon.txt", FileMode.Create))
        {
            byte[] textBytes = Encoding.ASCII.GetBytes(fileContent);
            fileStream.Write(textBytes, 0, textBytes.Length);

            char firstChar = ' ';
            char middleChar = ' ';
            char lastChar = ' ';

            // Read First character
            fileStream.Seek(0, SeekOrigin.Begin);
            firstChar = (char)fileStream.ReadByte();

            // Read Middle character
            fileStream.Seek(fileStream.Length / 2, SeekOrigin.Begin);
            middleChar = (char)fileStream.ReadByte();

            // Read Last character
            fileStream.Seek(-1, SeekOrigin.End);
            lastChar = (char)fileStream.ReadByte();

            Console.WriteLine($"In Word: {fileContent}");
            Console.WriteLine($"The First Character is: \"{firstChar}\"");
            Console.WriteLine($"The Middle Character is: \"{middleChar}\"");
            Console.WriteLine($"The Last Character is: \"{lastChar}\"");
        }
    }
}