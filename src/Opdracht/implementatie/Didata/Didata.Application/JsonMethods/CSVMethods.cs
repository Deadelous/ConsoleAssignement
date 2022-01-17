using CsvHelper;
using Didata.Core.Entities;
using Didata.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Didata.Application.JsonMethods
{
  public class CSVMethods : ICSVMethods
  {
    // Create with the help of CSVHelper a csv file with the help of the Order() method.
    // In the path you need to search the directory where you want to go and provide a name for the csv file
    // To test this, the directory DirectoryTest is created in the folder structure of the project
    public void CreateCSVFileFromJSON()
    {
      try
      {
        Order orderData = new Order();

        OrderEntity record = orderData.OrderTestData();

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
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}


