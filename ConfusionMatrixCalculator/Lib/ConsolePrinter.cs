using ConfusionMatrixCalculator.Lib.Interfaces;
using ConfusionMatrixCalculator.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConfusionMatrixCalculator.Lib
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(EvaluationResult data)
        {
            Console.WriteLine(GetStringRepresentation(data));
        }

        private string GetStringRepresentation(EvaluationResult data)
        {
            List<string> stringRepresentation = new List<string>
            {
                "====================STATISTICS===================",
                "Class\tCount\tRecall\tSpec\tAccur\tF1"
            };

            foreach (var stat in data.Statistics)
            {
                stringRepresentation.Add(
                    $"{stat.ClassName}\t{stat.Count}\t{100 * stat.Recall:0.00}\t{100 * stat.Specificity:0.00}\t{100 * stat.Accuracy:0.00}\t{100 * stat.F1_Measure:0.00}"
                );
            }

            stringRepresentation.Add("-------------------------------------------------");
            var avg = data.WeighedAverage;
            stringRepresentation.Add(
                $"AVG\t{avg.Count}\t{100*avg.Recall:0.00}\t{100 * avg.Specificity:0.00}\t{100 * avg.Accuracy:0.00}\t{100 * avg.F1_Measure:0.00}"
            );

            stringRepresentation.Add("=================================================");

            return String.Join('\n', stringRepresentation);
        }
    }
}
