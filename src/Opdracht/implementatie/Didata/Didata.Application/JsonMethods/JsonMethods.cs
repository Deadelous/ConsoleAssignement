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


    // Here you need to use a foreach instead of what you see here
    public void ReadJsonFromMultipleFiles()
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

    // This method allows you to find all json files from any directory
    // To test this, the directory DirectoryTest is created in the folder structure of the project
    public void GetAllJsonFilesFromDirectory()
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

    // In this method it is possible to serialize an object and create a new Json file
    // To test this, the directory DirectoryTest is created in the folder structure of the project
    public void CreateObjectToJSONFile()
    {
      OrderEntity order = Order();

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
    // A test method for testing the serializeObject from NewtonSoft
    public void showProductsInConsoleJson()
    {
      ProductEntity product1 = new ProductEntity("PID1", "pid1 description", 1.54, 19.54);

      string output = JsonConvert.SerializeObject(product1);

      Console.WriteLine(output);

      Console.ReadLine();
    }
    // Create with the help of CSVHelper a csv file with the help of the Order() method.
    // In the path you need to search the directory where you want to go and provide a name for the csv file
    // To test this, the directory DirectoryTest is created in the folder structure of the project
    public void CreateCSVFileFromJSON()
    {
      var record = Order();

      string path = "";

      Console.WriteLine("Where do you want to save the csv file?");
      path = Console.ReadLine();
      
      using (StreamWriter writer = new StreamWriter(path)) 
      using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
      {
        csv.WriteHeader<OrderEntity>();
        csv.NextRecord();

        csv.WriteRecord(record);
        csv.NextRecord();
      }
    }
    // Create a private OrderEntity that can be used for multiple methods if needed.
    private OrderEntity Order()
    {
      ProductEntity productOne = new ProductEntity("PID1", "pid1 description", 1.5, 19.5);
      ProductEntity productTwo = new ProductEntity("PID2", "pid2 description", 2.5, 12.1);

      List<ProductEntity> products = new List<ProductEntity>();

      products.Add(productOne);
      products.Add(productTwo);

      OrderEntity order = new OrderEntity(1, "Order 123", 123, products);

      return order;
    }
  }
}
