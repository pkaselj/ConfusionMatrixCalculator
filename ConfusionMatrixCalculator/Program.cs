using ConfusionMatrixCalculator.Lib;
using System;

namespace ConfusionMatrixCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\KASO\Desktop\confusionMatrix_9classes.csv";

            Console.WriteLine($"Reading file: {filePath}");
            var confusionMatrix = new Reader(filePath).ReadConfusionMatrix();

            Console.WriteLine("Confusion matrix:");
            Console.WriteLine(confusionMatrix);

            var statistics = new Calculator(confusionMatrix).CalculateStatistics();
            new ConsolePrinter().Print(statistics);

        }
    }
}
