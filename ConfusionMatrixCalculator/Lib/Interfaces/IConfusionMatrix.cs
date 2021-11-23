using System;
using System.Collections.Generic;
using System.Text;

namespace ConfusionMatrixCalculator.Lib.Interfaces
{
    public interface IConfusionMatrix
    {
        public IEnumerable<string> ClassNames { get; }
        public int GetOccurencesFor(string realClass, string evaluatedClass);

        public int GetOccurencesOf(string realClass);

        public int GetOccurencesOfAllExcept(string className);

        public int GetTP(string className);

        public int GetFP(string className);

        public int GetTN(string className);

        public int GetFN(string className);

        public string ToString();
    }
}
