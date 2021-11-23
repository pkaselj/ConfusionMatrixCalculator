using System;
using System.Collections.Generic;
using System.Text;

namespace ConfusionMatrixCalculator.Lib.Models
{
    public class EvaluationResult
    {
        public IEnumerable<ClassStatistics> Statistics { get; set; }
        public ClassStatistics WeighedAverage { get; set; }
    }
    public class ClassStatistics
    {
        public string ClassName { get; set; }
        public int Count { get; set; }
        public double Recall { get; set; }
        public double Specificity { get; set; }
        public double Accuracy { get; set; }
        public double F1_Measure { get; set; }

        public override string ToString()
        {
            string[] stringRepresentation = 
            {
                $"CLASS: {ClassName}",
                $"COUNT: {Count}",
                $"Recall: {100*Recall:0.00}%",
                $"Specificity: {100*Specificity:0.00}%",
                $"Accuracy: {100*Accuracy:0.00}%",
                $"F1_Measure: {100*F1_Measure:0.00}%",
            };

            return String.Join('\n', stringRepresentation);
        }
    }
}
