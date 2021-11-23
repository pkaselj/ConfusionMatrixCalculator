using ConfusionMatrixCalculator.Lib.Interfaces;
using ConfusionMatrixCalculator.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConfusionMatrixCalculator.Lib
{
    public class Calculator : ICalculator
    {
        private readonly IConfusionMatrix _confusionMatrix;

        public Calculator(IConfusionMatrix confusionMatrix)
        {
            _confusionMatrix = confusionMatrix;
        }

        public EvaluationResult CalculateStatistics()
        {

            List<ClassStatistics> statistics = new List<ClassStatistics>();

            foreach (var className in _confusionMatrix.ClassNames)
            {
                var stats = CalculateStatisticsForClass(className);
                statistics.Add(stats);
            }

            return new EvaluationResult
            {
                WeighedAverage = CalculateAverage(statistics),
                Statistics = statistics
            };
        }

        private ClassStatistics CalculateAverage(IEnumerable<ClassStatistics> statistics)
        {
            ClassStatistics average = new ClassStatistics
            {
                ClassName = "Weighed Average",
                Count = 0,
                Recall = 0,
                Specificity = 0,
                Accuracy = 0,
                F1_Measure = 0
            };

            foreach (var stat in statistics)
            {
                int N = stat.Count;
                average.Count += N;
                average.Recall += N * stat.Recall;
                average.Specificity += N * stat.Specificity;
                average.Accuracy += N * stat.Accuracy;
                average.F1_Measure += N * stat.F1_Measure;
            }

            average.Recall /= average.Count;
            average.Specificity /= average.Count;
            average.Accuracy /= average.Count;
            average.F1_Measure /= average.Count;

            return average;
        }

        private ClassStatistics CalculateStatisticsForClass(string className)
        {
            double recall = CalculateRecallForClass(className);
            double accuracy = CalculateAccuracyForClass(className);

            return new ClassStatistics
            {
                ClassName = className,
                Count = GetClassOccurences(className),
                Recall = recall,
                Specificity = CalculateSpecificityForClass(className),
                Accuracy = accuracy,
                F1_Measure = CalculateF1_Measure(recall, accuracy)
            };
        }

        private int GetClassOccurences(string className)
        {
            return _confusionMatrix.GetOccurencesOf(className);
        }

        private double CalculateRecallForClass(string className)
        {
            double TP = _confusionMatrix.GetTP(className);
            double P = _confusionMatrix.GetOccurencesOf(realClass: className);

            return TP / P;
        }

        private double CalculateSpecificityForClass(string className)
        {
            double TN = _confusionMatrix.GetTN(className);
            double FP = _confusionMatrix.GetFP(className);

            return TN / (TN + FP);
        }

        private double CalculateAccuracyForClass(string className)
        {
            double TP = _confusionMatrix.GetTP(className);
            double FP = _confusionMatrix.GetFP(className);
            return TP / (TP + FP);
        }

        private double CalculateF1_Measure(double recall, double accuracy)
        {
            return 2 * (recall * accuracy) / (recall + accuracy);
        }
    }
}
