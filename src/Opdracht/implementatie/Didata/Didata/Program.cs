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
      JsonMethods jsonmethods = new JsonMethods();
      CSVMethods csvmethods = new CSVMethods();

      var menu = new EasyConsole.Menu()
      .Add("-d mode", () => jsonmethods.GetAllJsonFilesFromDirectory())
      .Add("-r mode", () => jsonmethods.ReadJsonFromFile())
      .Add("-r multiplemode", () => jsonmethods.ReadJsonFromMultipleFiles())
      .Add("-f mode", () => jsonmethods.CreateObjectToJSONFile())
      .Add("-csv mode", () => csvmethods.CreateCSVFileFromJSON());
      menu.Display();
    }
  }
 }
