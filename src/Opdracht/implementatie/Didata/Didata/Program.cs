using Didata.Application.JsonMethods;
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
      ConsoleMenus();
    }

    static void ConsoleMenus()
    {
      JsonMethods methods = new JsonMethods();

      var menu = new EasyConsole.Menu()
      .Add("-d mode", () => methods.GetAllJsonFilesFromDirectory())
      .Add("-r mode", () => methods.ReadJsonFromFile())
      .Add("-r multiplemode", () => methods.ReadJsonFromMultipleFiles())
      .Add("-f mode", () => methods.CreateObjectToJSONFile())
      .Add("-csv mode", () => methods.CreateCSVFileFromJSON());
      menu.Display();
    }
  }
 }
