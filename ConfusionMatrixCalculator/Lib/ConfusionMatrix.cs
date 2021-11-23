using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ConfusionMatrixCalculator.Lib.Interfaces;

namespace ConfusionMatrixCalculator.Lib.Models
{
    using ClassEvaluationArray = Dictionary<string, int>;
    using Int2D = IEnumerable<IEnumerable<int>>;
    public class ConfusionMatrix : IConfusionMatrix
    {
        private readonly Dictionary<string, ClassEvaluationArray> _confusionMatrix;
        private readonly IEnumerable<string> _classNames;

        public IEnumerable<string> ClassNames { get { return _classNames; } }

        // Real classes are defined in rows,
        // while evaluated classes are defined in columns
        public ConfusionMatrix(
            IEnumerable<string> classNames,
            Int2D rawConfusionMatrix
        )
        {
            _classNames = classNames;
            _confusionMatrix = generateConfusionMatrix(classNames, rawConfusionMatrix);
        }

        private Dictionary<string, ClassEvaluationArray> generateConfusionMatrix(
            IEnumerable<string> classNames,
            Int2D confusionMatrix
        )
        {
            int N = classNames.Count();
            Dictionary<string, ClassEvaluationArray> tempConfusionMatrix = new Dictionary<string, ClassEvaluationArray>(N);

            for (int i = 0; i < N; i++)
            {
                string name = classNames.ElementAt(i);
                IEnumerable<int> row = confusionMatrix.ElementAt(i);

                ClassEvaluationArray evaluation = new ClassEvaluationArray(N);
                for (int j = 0; j < N; j++)
                {
                    evaluation.Add(classNames.ElementAt(j), row.ElementAt(j));
                }

                tempConfusionMatrix.Add(name, evaluation);
            }

            return tempConfusionMatrix;
        }

        private ClassEvaluationArray GetStatisticsForClass(string realClass)
        {
            return _confusionMatrix[realClass];
        }

        public int GetOccurencesFor(string realClass, string evaluatedClass)
        {
            return _confusionMatrix[realClass][evaluatedClass];
        }

        public int GetOccurencesOf(string realClass)
        {
            var stats = _confusionMatrix[realClass];
            int count = stats.Values.Aggregate((a, b) => a + b);
            return count;
        }

        public int GetOccurencesOfAllExcept(string className)
        {
            int count = 0;
            var statistics = _classNames.Where(x => x != className).Select(x => GetStatisticsForClass(x));

            foreach (var stat in statistics)
            {
                count += stat.Values.Aggregate((a, b) => a + b);
            }

            return count;
        }

        public int GetTP(string className)
        {
            return GetOccurencesFor(realClass: className, evaluatedClass: className);
        }

        public int GetFP(string className)
        {
            var falseClassNames = _classNames.Where(x => x != className);
            int FP = 0;
            foreach (var name in falseClassNames)
            {
                FP += GetOccurencesFor(realClass: name, evaluatedClass: className);
            }
            return FP;
        }

        public int GetTN(string className)
        {
            IEnumerable<string> falseClassNames = _classNames.Where(x => x != className);
            var negatives = falseClassNames.Select(x => GetOccurencesFor(realClass: x, evaluatedClass: x));
            int TN = negatives.Aggregate((a, b) => a + b);
            return TN;
        }

        public int GetFN(string className)
        {
            var statisticsForPositiveClass = GetStatisticsForClass(className);
            int FN = 0;
            foreach (var item in statisticsForPositiveClass)
            {
                if(item.Key != className)
                {
                    FN += item.Value;
                }
            }
            return FN;
        }


        public override string ToString()
        {
            List<string> stringRepresentation = new List<string>();

            stringRepresentation.Add("=== Evaluated classes ===");

            string headerLine = $"\t{String.Join('\t', _classNames)}";
            stringRepresentation.Add(headerLine);

            foreach (var item in _confusionMatrix)
            {
                string line = $"{item.Key}\t"; // Class

                foreach (var number in item.Value)
                {
                    line += $"{number.Value}\t";
                }
                stringRepresentation.Add(line);
            }

            return String.Join('\n', stringRepresentation);

        }

    }
}
