using Didata.Core.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Didata
{
  class Program
  {
    static void Main(string[] args)
    {
      var menu = new EasyConsole.Menu()
      .Add("-d mode", () => GetDirectory())
      .Add("-r mode", () => ReadJsonFromFile())
      .Add("-f mode", () => SerializeToJSON())
      .Add("Show Product in JSON", () => showProductsInConsoleJson());
      menu.Display();
    }


    static void ReadJsonFromFile()
    {
      string filename = "";
      Console.WriteLine("Which file do you want to open?");
      filename = Console.ReadLine();

      using (StreamReader file = File.OpenText(filename))
      using(JsonTextReader reader = new JsonTextReader(file))
      {
        JObject o2 = (JObject)JToken.ReadFrom(reader);
        Console.WriteLine(o2);
      }
    }

    static void GetDirectory()
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

    static void SerializeToJSON()
    {
      ProductEntity productOne = new ProductEntity("PID1", "pid1 description", 1.5, 19.5);
      ProductEntity productTwo = new ProductEntity("PID2", "pid2 description", 2.5, 12.1);

      List<ProductEntity> products = new List<ProductEntity>();

      products.Add(productOne);
      products.Add(productTwo);

      OrderEntity order = new OrderEntity(1, "Order 123", 123, products);

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

    static void showProductsInConsoleJson()
    {
      ProductEntity product1 = new ProductEntity("PID1", "pid1 description", 1.54, 19.54);

      string output = JsonConvert.SerializeObject(product1);

      Console.WriteLine(output);

      Console.ReadLine();
    }
  }
 }