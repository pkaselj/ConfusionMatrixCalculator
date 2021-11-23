using ConfusionMatrixCalculator.Lib.Interfaces;
using ConfusionMatrixCalculator.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConfusionMatrixCalculator.Lib
{
    public class Reader : IReader
    {
        private readonly string _inputFilePath;
        private static readonly char _separator = ',';
        public Reader(string path)
        {
            _inputFilePath = path;
        }

        public IConfusionMatrix ReadConfusionMatrix()
        {
            string[] fileLines = System.IO.File.ReadAllLines(_inputFilePath);

            string[] classNames = fileLines[0].Split(_separator);

            string[] rawConfusionMatrix = fileLines.Skip(1).ToArray();

            int N = classNames.Count();
            int[][] confusionMatrix = new int[N][];

            for (int i = 0; i < classNames.Length; i++)
            {
                string className = classNames[i];
                int[] row = rawConfusionMatrix[i].Split(_separator).Select(x => Int32.Parse(x)).ToArray();
                confusionMatrix[i] = row;
            }

            return new ConfusionMatrix(classNames, confusionMatrix);

        }
    }
}
