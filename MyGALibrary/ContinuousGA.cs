using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGALibrary
{
    public class ContinuousGA : GenericGASolver<double>
    {
        double[] lowerBounds;
        double[] upperBounds;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfVariables"></param>
        /// <param name="lowerBounds"></param>
        /// <param name="upperBounds"></param>
        /// <param name="optimizationType"></param>
        /// <param name="objectiveEvaluationFunction"></param>
        public ContinuousGA(int numberOfVariables, double[] lowerBounds, double[] upperBounds, OptimizationType optimizationType, ObjectiveFunction<double> objectiveEvaluationFunction) : base(numberOfVariables, optimizationType, objectiveEvaluationFunction)
        {
            this.upperBounds = upperBounds;
            this.lowerBounds = lowerBounds;
        }
    }
}