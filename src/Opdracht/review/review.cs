using SollicitantReview.Models;
using SollicitantReview.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SollicitantReview
{
    public class SollicitantReview
    {
        private SollicitantReviewSettings options;
        private IWriter writer;
        private Queue<List<ReviewExportLine>> exportLineQueue;

        public SollicitantReview(IOptions<SollicitantReviewSettings> options, IWriter writer)
        {
            this.options = options.Value;
            this.writer = writer;
            this.exportLineQueue = new Queue<List<ReviewExportLine>>();

            var processExportLinesThread = new Thread(new ThreadStart(ProcessReviewExportLines));
            processExportLinesThread.Start();
        }

        public void RegisterOrder(Order order)
        {
            DeleteOrder(order);
            exportLineQueue.Enqueue(ConvertOrderToExportLines(order));
        }

        private string FilterString(string value)
        {
            return value.Replace(";", ":");
        }

        private void DeleteOrder(Order order)
        {
            var exportLines = new List<ReviewExportLine>()
            {
                new ReviewExportLine()
                {
                    Type = "9",
                    OrderNumber = FilterString(order.OrderNumber),
                    Amount = "1",
                    UserId = FilterString(order.User),
                    JournalPostIndication = "Y"
                }
            };

            exportLineQueue.Enqueue(exportLines);
        }

        private List<ReviewExportLine> ConvertOrderToExportLines(Order order, string userId = null)
        {
            var exportLines = new List<ReviewExportLine>();

            foreach (var orderLine in order.OrderLines)
            {
                exportLines.Add(new ReviewExportLine()
                {
                    OrderNumber = FilterString(order.OrderNumber),
                    Name = FilterString(orderLine.Name),
                    Amount = FilterString(orderLine.Amount),
                    ProductNumber = FilterString(orderLine.Number),
                    ProductDescription = FilterString(orderLine.Description),
                    UserId = userId == null ? FilterString(order.User) : userId,
                    Location = FilterString(orderLine.Location),
                    JournalPostIndication = "Y"
                });
            }

            return exportLines;
        }

        private void ProcessReviewExportLines()
        {
            while(true)
            {
                if (exportLineQueue.Any() && IsFolderEmpty(options.ReviewFolderPath))
                {
                    var exportLines = exportLineQueue.Dequeue();
                    var randomStringLength = 7;

                    writer.OpenFile($"{options.ReviewFolderPath}\\{GenerateRandomString(randomStringLength)}.txt");

                    foreach (var exportLine in exportLines)
                    {
                        writer.WriteLine($"{exportLine.Type};" +
                                         $"{exportLine.OrderNumber};" +
                                         $"{exportLine.Name};" +
                                         $"{exportLine.Amount};" +
                                         $"{exportLine.ProductNumber};" +
                                         $"{exportLine.ProductDescription};" +
                                         $"{exportLine.UserId};" +
                                         $"{exportLine.Location};" +
                                         $"{exportLine.JournalPostIndication};" +
                                         $"{exportLine.Info2}");
                    }

                    writer.CloseFile();
                }

                Thread.Sleep(2000);
            }
        }

        private bool IsFolderEmpty(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            if (!dirInfo.Exists)
            {
                throw new CouldNotFindReviewDirectory();
            }

            return !dirInfo.GetFiles("*.txt").Any();
        }

        private string GenerateRandomString(int length)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            int ASCIIOffset = 65;
            int ASCIIRange = 25;

            char letter;

            for (int i = 0; i < length; i++)
            {
                double randomDouble = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(ASCIIRange * randomDouble));
                letter = Convert.ToChar(shift + ASCIIOffset);
                stringBuilder.Append(letter);
            }

            return stringBuilder.ToString();
        }
    }
}
