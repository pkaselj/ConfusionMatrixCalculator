﻿using ConfusionMatrixCalculator.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfusionMatrixCalculator.Lib.Interfaces
{
    public interface IReader
    {
        public IConfusionMatrix ReadConfusionMatrix();
    }
}
