using CsvHelper;
using Didata.Core.Entities;
using Didata.Core.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Didata.Application.JsonMethods
{
  public class JsonMethods : IJsonMethods
  {
    // With this method it is possible to read Json from a file and show it in console
    public void ReadJsonFromFile()
    {
      try
      {
        string filename = "";
        Console.WriteLine("Which file do you want to open?");
        filename = Console.ReadLine();

        using (StreamReader file = File.OpenText(filename))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
          JObject objects = (JObject)JToken.ReadFrom(reader);
          Console.WriteLine(objects);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    // Here you need to use a foreach instead of what you see here
    public void ReadJsonFromMultipleFiles()
    {
      try
      {
        string filename = "";
        string otherfile = "";
        Console.WriteLine("Which file do you want to open?");
        filename = Console.ReadLine();

        Console.WriteLine("Do you want add another JSON file?");
        otherfile = Console.ReadLine();

        using (StreamReader file = File.OpenText(filename))
        using (StreamReader fileTwo = File.OpenText(otherfile))
        using (JsonTextReader reader = new JsonTextReader(file))
        using (JsonTextReader readerTwo = new JsonTextReader(fileTwo))
        {
          JObject objects = (JObject)JToken.ReadFrom(reader);
          JObject otherObjects = (JObject)JToken.ReadFrom(readerTwo);
          Console.WriteLine(objects);
          Console.WriteLine(otherObjects);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    // This method allows you to find all json files from any directory
    // To test this, the directory DirectoryTest is created in the folder structure of the project
    public void GetAllJsonFilesFromDirectory()
    {
      try
      {
        string path = "";
        Console.WriteLine("Which Directory do you want to search?");

        path = Console.ReadLine();
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        FileInfo[] files = directoryInfo.GetFiles("*.json");

        foreach (FileInfo file in files)
        {
          Console.WriteLine(file.Name);
        }

        Console.ReadLine();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    // In this method it is possible to serialize an object and create a new Json file
    // To test this, the directory DirectoryTest is created in the folder structure of the project
    public void CreateObjectToJSONFile()
    {
      try
      {
        Order orderData = new Order();

        OrderEntity order = orderData.OrderTestData();

        string output = JsonConvert.SerializeObject(order);

        string path = "";

        Console.WriteLine("Which directory do you want to go?");

        path = Console.ReadLine();

        File.WriteAllText(path, output);

        using (StreamWriter file = File.CreateText(path))
        {
          JsonSerializer jsonSerializer = new JsonSerializer();
          jsonSerializer.Serialize(file, order);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
