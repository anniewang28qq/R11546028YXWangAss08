using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGALibrary
{
    /// <summary>
    /// Generic GA Solver
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericGASolver<T>
    {
        #region DATA FIELDS

        // Max Min
        OptimizationType optimizationMode;

        // Stochastic Deterministic
        ObjectiveFunction<T> theObjectiveFunction;

        /// <summary>
        /// gene的數量
        /// </summary>
        protected int numberOfGenes;

        /// <summary>
        /// population的大小, 由使用者變更
        /// </summary>
        protected int populationSize = 10;

        /// <summary>
        /// 染色體。一維陣列，裡面的元素是一維陣列
        /// </summary>
        protected T[][] chromosomes;

        /// <summary>
        /// 目標函式值
        /// </summary>
        protected double[] objectiveValues;

        /// <summary>
        /// 適合度, 越大越好
        /// </summary>
        protected double[] fitnessValue;

        /// <summary>
        /// 目前最好的chromosomes
        /// </summary>
        protected T[] soFarTheBestSolution;

        /// <summary>
        /// 目前最好的目標函式值
        /// </summary>
        protected double soFarTheBestObjective;

        /// <summary>
        /// 某代中目標函式值的平均
        /// </summary>
        protected double iterationObjectiveAverage;

        /// <summary>
        /// 某代裡最好的目標函式值
        /// </summary>
        protected double iterationBestObjective;

        /// <summary>
        /// 交配子代數量；固定
        /// </summary>
        protected int numberOfCrossoveredChildren;

        /// <summary>
        /// 突變子代數量；變動
        /// </summary>
        protected int numberOfMutatedChildren;

        /// <summary>
        /// crossover rate, 先寫死
        /// </summary>
        protected double crossoverRate = 0.8;

        /// <summary>
        /// mutation rate, population size based
        /// </summary>
        protected double mutationRate = 0.05;

        // mutation的type
        MutationMode mutationType = MutationMode.PopulationSizeBased;

        /// <summary>
        /// index的草稿紙
        /// </summary>
        protected int[] indices;

        /// <summary>
        /// cut point的草稿紙
        /// </summary>
        protected bool[] cutPositionFlags;

        /// <summary>
        /// 選到的Chromosomes。二維陣列!
        /// </summary>
        protected T[,] selectedChromosomes;

        /// <summary>
        /// 選到的Objectives
        /// </summary>
        protected double[] selectedObjectives;

        /// <summary>
        /// 最小的Fitness值: alpha, 使用者可以改
        /// </summary>
        protected double leastFitnessFraction = 0.5;

        /// <summary>
        /// 幾個iteration後停止
        /// </summary>
        protected int iterationLimit = 100;

        /// <summary>
        /// 現在到第幾個iteration
        /// </summary>
        protected int iterationCount = 0;
 
        /// <summary>
        /// 哪些突變 to set mutated gene in gene-number based mutation
        /// </summary>
        protected bool[][] mutatedFlags;

        /// <summary>
        /// So far the best solution更新次數
        /// </summary>
        protected int soFarTheBestSolutionUpdatedTime = 0;

        /// <summary>
        /// 亂數產生器
        /// </summary>
        protected Random randomizer = new Random();

        GAControl<T> theControl = null;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Mutation的Type，由使用者變更。
        /// </summary>
        [Category("Mutation"), Description("基於gene或基於整條染色體做mutation。")]
        public virtual MutationMode MutationType
        {
            set
            {
                mutationType = value;

                if(mutationType == MutationMode.GeneNumberBased)
                {
                    mutationRate = 0.05;
                }
                else
                {
                    mutationRate = 0.3;
                }
            }

            get => mutationType;
        }

        /// <summary>
        /// 選擇留下的解的方法。
        /// </summary>
        [Category("GA"), Description("選擇留下的解的方法。")]
        public SelectionMode SelectionType { set; get; } = SelectionMode.Deterministic;



        /// <summary>
        /// population的大小 (Job Problem不能由使用者變更!只能從檔案)
        /// </summary>
        /// [Browsable(false)]
        [Category("GA"), Description("每一代次留下的數量。")]
        public int PopulationSize
        { 
            get => populationSize;
            set
            {
                if (value >= 10)
                {
                    populationSize = value;
                }
            }
        }

        /// <summary>
        /// 目前的最佳解的目標函數值
        /// </summary>
        [Browsable(false)]
        public double SoFarTheBestObjective { get => soFarTheBestObjective; }

        /// <summary>
        /// 目前的最佳解
        /// </summary>
        [Browsable(false)]
        public T[] SoFarTheBestSolution { get => soFarTheBestSolution; }


        /// <summary>
        /// 此代中object值的平均
        /// </summary>
        [Browsable(false)]
        public double IterationObjectiveAverage { get => iterationObjectiveAverage; }

        /// <summary>
        /// 此代中的最佳解
        /// </summary>
        [Browsable(false)]
        public double IterationBestObjective { get => iterationBestObjective; }

        /// <summary>
        /// 目前到第幾代
        /// </summary>
        
        [Browsable(false)]
        public int IterationCount { get => iterationCount; }

        /// <summary>
        /// 全部的染色體
        /// </summary>
        [Browsable(false)]
        public T[][] Chromosomes { get => chromosomes; }

        /// <summary>
        /// 全部的目標值
        /// </summary>
        [Browsable(false)]
        public double[] ObjectiveValues { get => objectiveValues; }

        /// <summary>
        /// Crossover Rate
        /// </summary>
        [Category("Crossover"), Description("Crossover的比例。")]
        public double CrossoverRate
        {
            get => crossoverRate;

            set
            {
                if(value < 1 && value > 0)
                {
                    crossoverRate = value;
                }
            }
        }

        /// <summary>
        /// Mutation Rate
        /// </summary>
        [Category("Mutation"), Description("Mutation的比例。")]
        public double MutationRate 
        { 
            get => mutationRate;

            set
            {
                if (value < 1 && value > 0)
                {
                    mutationRate = value;
                }
            }
        }

        /// <summary>
        /// 最多可跑幾個iteration
        /// </summary>
        [Category("GA"), Description("迭代的次數。")]
        public int IterationLimit
        {
            get => iterationLimit;
            set
            {
                if (value > 0)
                {
                    iterationLimit = value;
                }
            }
        }

        /// <summary>
        /// genes 的數量
        /// </summary>
        [Browsable(false)]
        public int NumberOfGenes { get => numberOfGenes; set => numberOfGenes = value; }

        /// <summary>
        /// panel
        /// </summary>
        [Browsable(false)]
        public GAControl<T> TheControl { get => theControl; }

        /// <summary>
        /// so far the best solution updated time
        /// </summary>
        [Browsable(false)]
        public int SoFarTheBestSolutionUpdatedTime { get => soFarTheBestSolutionUpdatedTime; set { soFarTheBestSolutionUpdatedTime =  value; } }

        #endregion

        #region PUBLIC EVENTS

        /// <summary>
        /// reset
        /// </summary>
        public event EventHandler AfterReset;

        /// <summary>
        /// 計算最佳解更新次數
        /// </summary>
        public event EventHandler SoFarTheBestSolutionUpdated;

        /// <summary>
        /// 計算迭代次數
        /// </summary>
        public event EventHandler OneIterationCompleted;

        /// <summary>
        /// reset
        /// </summary>
        protected void FireAfterResetEvent()
        {
            if (AfterReset != null)
            {
                AfterReset(this, null);
            }
        }

        /// <summary>
        /// 計算迭代次數
        /// </summary>
        protected void FireOneIterationCompletedEvent()
        {
            if(OneIterationCompleted != null)
            {
                OneIterationCompleted(this, null);
            }
        }

        /// <summary>
        /// 計算最佳解更新次數
        /// </summary>
        protected void FireSoFarTheBestSolutionUpdatedEvent()
        {
            if (SoFarTheBestSolutionUpdated != null)
            {
                SoFarTheBestSolutionUpdated(this, null);
            }
        }

        #endregion

        #region CTOR

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="numberOfVariables"></param>
        /// <param name="optimizationType"></param>
        /// <param name="objectiveEvaluationFunction"></param>
        public GenericGASolver(int numberOfVariables, OptimizationType optimizationType, ObjectiveFunction<T> objectiveEvaluationFunction, Panel hostPanel = null)
        {
            // new 出來就不會再改
            numberOfGenes = numberOfVariables;
            soFarTheBestSolution = new T[numberOfGenes];
            optimizationMode = optimizationType;
            theObjectiveFunction = objectiveEvaluationFunction;

            cutPositionFlags = new bool[numberOfGenes];

            if(hostPanel != null)
            {
                theControl = new GAControl<T>(this);
                hostPanel.Controls.Clear();
                hostPanel.Controls.Add(theControl);
                theControl.Dock = DockStyle.Fill;
            }
        }

        #endregion

        #region FUNCTION

        /// <summary>
        /// 重設
        /// </summary>
        public void Reset()
        {
            // Reallocate memory if population size is changed
            SoFarTheBestSolutionUpdatedTime = 0;
            int ThreeSizes = 3 * populationSize;
            chromosomes = new T[ThreeSizes][];
            for(int i = 0; i < ThreeSizes; i++)
            {
                chromosomes[i] = new T[numberOfGenes];
            }
            objectiveValues = new double[ThreeSizes];
            fitnessValue = new double[ThreeSizes];
            selectedObjectives = new double[populationSize];
            mutatedFlags = new bool[populationSize][];
            for(int i = 0; i < populationSize; i++)
            {
                mutatedFlags[i] = new bool[numberOfGenes];
            }

            selectedChromosomes = new T[populationSize, numberOfGenes]; // 二維陣列

            indices = new int[ThreeSizes];
            iterationCount = 0; // 歸0
            soFarTheBestObjective = optimizationMode == OptimizationType.Maximization ? double.MinValue : double.MaxValue;

            // initialize initial population
            InitializePopulation();

            // evaluate initial population
            for(int i = 0; i < populationSize; i++)
            {
                objectiveValues[i] = theObjectiveFunction(chromosomes[i]);
            }

            FireAfterResetEvent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        protected virtual void InitializePopulation()
        {
            // 子類別的責任！
            throw new NotImplementedException();
        }

        /// <summary>
        /// 一次跑一個iteration
        /// </summary>
        public void RunOneIteration()
        {
            if (iterationCount < iterationLimit)
            {
                PerformCrossoverOperation();
                PerformMutationOperation();
                PerformSelectionOperation();
                iterationCount++;

                FireOneIterationCompletedEvent();
            }
        }

        /// <summary>
        /// 一次跑完全部
        /// </summary>
        public void RunToEnd()
        {
            do
            {
                RunOneIteration();
            } while (iterationCount < iterationLimit);
        }

        /// <summary>
        /// 打亂indice
        /// </summary>
        /// <param name="upIdx">要做到哪</param>
        protected void ShuffleIndicies(int upIdx)
        {
            // 讓indices的值先都是正確的
            for(int i = 0; i < upIdx; i++)
            {
                indices[i] = i;
            }

            // shuffle
            for(int j = upIdx - 1; j > 0; j--)
            {
                int pos = randomizer.Next(j + 1);
                int temp = indices[pos];
                indices[pos] = indices[j];
                indices[j] = temp;
            }
        }
        #endregion

        #region PERFORM OPERATION FIELD
        void PerformSelectionOperation()
        {
            int numberAll = populationSize + numberOfCrossoveredChildren + numberOfMutatedChildren;

            UpdateIterationBestAverageAndSoFarTheBest();

            // caculate fitness
            SetFitnessFromObjectives();

            switch (SelectionType)
            {
                case SelectionMode.Deterministic:
                    //ShuffleIndicies(numberAll);
                    for (int i = 0; i < numberAll; i++)
                    {
                        indices[i] = i;
                    }
                    Array.Sort(fitnessValue, indices, 0, numberAll); // small to large
                    Array.Reverse(indices, 0, numberAll); // large to small -> 前面的就是選中的
                    break;
                case SelectionMode.Stochastic: // 輪盤
                    double wholeRange = 0;
                    for (int i = 0; i < numberAll; i++)
                    {
                        wholeRange += fitnessValue[i];
                    }
                    // cumulated fitness assignment

                    // perform populationSize stochastic selection
                    double fitnessSum = fitnessValue.Sum();
                    for (int i = 0; i < populationSize; i++)
                    {
                        int HitIdx = -1;
                        double iterationSum = 0;
                        double q = randomizer.NextDouble();
                        for (int j = 0; j < fitnessValue.Length; j++)
                        {
                            iterationSum += fitnessValue[j];
                            if ((fitnessSum * q) <= iterationSum)
                            {
                                HitIdx = j;
                                break;
                            }
                        }
                        indices[i] = HitIdx;
                    }
                    break;
            }
            // gene-wisely copy the selected chromosomes to selectedChromosome and their objective value
            // selected indexes are store in the front of the indices array
            for (int r = 0; r < populationSize; r++)
            {
                for (int c = 0; c < numberOfGenes; c++)
                {
                    selectedChromosomes[r, c] = chromosomes[indices[r]][c]; // 選中的
                }
                // copy the objective values to selected objective value
                selectedObjectives[r] = objectiveValues[indices[r]];
            }

            // copy back to chromosomes
            for (int r = 0; r < populationSize; r++)
            {
                for (int c = 0; c < numberOfGenes; c++)
                {
                    chromosomes[r][c] = selectedChromosomes[r, c];
                }
                objectiveValues[r] = selectedObjectives[r];
            }
        }

        private void PerformCrossoverOperation()
        {
            // shuffle indices array to arrange parent IDs
            ShuffleIndicies(PopulationSize);

            numberOfCrossoveredChildren = (int)(crossoverRate * populationSize);

            // 確保numberOfCrossoveredChildren不要比populationSize大
            if (numberOfCrossoveredChildren > populationSize)
            {
                numberOfCrossoveredChildren = populationSize;
            }

            // 確保numberOfCrossoveredChildren是偶數
            if (numberOfCrossoveredChildren % 2 == 1)
            {
                numberOfCrossoveredChildren++;
            }

            for (int i = 0; i < numberOfCrossoveredChildren; i += 2)
            {
                int childIdx_1, childIdx_2;
                childIdx_1 = populationSize + i;
                childIdx_2 = populationSize + i + 1;
                GenerateAPairOfCrossoveredChildren(indices[i], indices[i + 1], childIdx_1, childIdx_2);

                // compute objective values of child 1 and 2
                objectiveValues[childIdx_1] = theObjectiveFunction(chromosomes[childIdx_1]);
                objectiveValues[childIdx_2] = theObjectiveFunction(chromosomes[childIdx_2]);
            }
        }

        private void PerformMutationOperation()
        {
            int childIdx = populationSize + numberOfCrossoveredChildren;
            switch (mutationType)
            {
                case MutationMode.PopulationSizeBased:

                    numberOfMutatedChildren = (int)(mutationRate * populationSize); // 條數 * rate
                    ShuffleIndicies(PopulationSize);
                    for(int i = 0; i < numberOfMutatedChildren; i++)
                    {
                        GenerateAMutatedChild(indices[i], childIdx);
                        objectiveValues[childIdx] = theObjectiveFunction(chromosomes[childIdx]);
                        childIdx++;
                    }

                    break;
                case MutationMode.GeneNumberBased:

                    numberOfMutatedChildren = 0;
                    int limit = numberOfGenes * populationSize;
                    int numberOfMutatedGenes = (int)(mutationRate * limit); // 全部的gene * rate

                    // 重設mutatedFlags都=false
                    for (int p = 0; p < populationSize; p++)
                    {
                        for(int q = 0; q < numberOfGenes; q++)
                        {
                            mutatedFlags[p][q] = false;
                        }
                    }

                    // 隨機選=true的位置
                    for(int i = 0; i < numberOfMutatedGenes; i++)
                    {
                        int sequIdx = randomizer.Next(limit);
                        int rowIdx = sequIdx / numberOfGenes;
                        int columnIdx = sequIdx % numberOfGenes;
                        mutatedFlags[rowIdx][columnIdx] = true;
                    }

                    // traverse陣列
                    for(int r = 0; r < populationSize; r++)
                    {
                        bool hit = false;
                        for(int c = 0; c < numberOfGenes; c++)
                        {
                            if (mutatedFlags[r][c])
                            {
                                hit = true;
                                break;
                            }
                        }
                        if (hit)
                        {
                            // ask this parent to mutate
                            GenerateAGeneBasedMutatedChild(r, childIdx);
                            objectiveValues[childIdx] = theObjectiveFunction(chromosomes[childIdx]);
                            childIdx++;
                            numberOfMutatedChildren++;
                        }
                    }

                    break;
            }
        }

        private void UpdateIterationBestAverageAndSoFarTheBest()
        {
            int numberAll = populationSize + numberOfCrossoveredChildren + numberOfMutatedChildren;

            // caculate the iteration best, iteration average, check whether the iteration best can replace the so-far-the-best
            double sum = theObjectiveFunction(chromosomes[0]);
            double obj = theObjectiveFunction(chromosomes[0]);
            double thisObj;
            int iterationBestIdx = 0;
            for(int i = 1; i < numberAll; i++)
            {
                thisObj = theObjectiveFunction(chromosomes[i]);

                if (optimizationMode == OptimizationType.Maximization)
                {
                    if (thisObj > obj)
                    {
                        obj = thisObj;
                        iterationBestIdx = i;
                    }
                }
                else if(optimizationMode == OptimizationType.Minimization)
                {
                    if (thisObj < obj)
                    {
                        obj = thisObj;
                        iterationBestIdx = i;
                    }
                }

                sum += thisObj;
            }

            iterationBestObjective = obj;
            iterationObjectiveAverage = sum / numberAll;

            // if iteration best is better than the so-far-the-best, update so-far-the-best objective and solution
            // solution update must be done gene-wisely. (一個一個copy進去)
            if(optimizationMode == OptimizationType.Maximization)
            {
                if(iterationBestObjective > soFarTheBestObjective)
                {
                    soFarTheBestSolutionUpdatedTime++;
                    FireSoFarTheBestSolutionUpdatedEvent();
                    soFarTheBestObjective = iterationBestObjective;
                    for(int c = 0; c < numberOfGenes; c++)
                    {
                        soFarTheBestSolution[c] = chromosomes[iterationBestIdx][c];
                    }
                }
            }
            else if(optimizationMode == OptimizationType.Minimization)
            {
                if (iterationBestObjective < soFarTheBestObjective)
                {
                    soFarTheBestSolutionUpdatedTime++;
                    FireSoFarTheBestSolutionUpdatedEvent();
                    soFarTheBestObjective = iterationBestObjective;
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        soFarTheBestSolution[c] = chromosomes[iterationBestIdx][c];
                    }
                }
            }
        }

        private void SetFitnessFromObjectives()
        {
            int numberAll = populationSize + numberOfCrossoveredChildren + numberOfMutatedChildren;
            double maxObj = double.MinValue;
            double minObj = double.MaxValue;
            double thisObj;
            double b;

            // 找最大跟最小的obj
            for(int i = 0; i < numberAll; i++)
            {
                thisObj = objectiveValues[i];
                if (thisObj > maxObj)
                {
                    maxObj = thisObj;
                }
                if (thisObj < minObj)
                {
                    minObj = thisObj;
                }
            }

            // minimum fitness
            b = Math.Max(leastFitnessFraction * (maxObj - minObj), Math.Pow(10, -5));

            // caculate the fitness value of each chromosome
            for(int i = 0; i < numberAll; i++)
            {
                if (optimizationMode == OptimizationType.Maximization)
                {
                    fitnessValue[i] = b + objectiveValues[i] - minObj;
                }
                else if (optimizationMode == OptimizationType.Minimization)
                {
                    fitnessValue[i] = b + maxObj - objectiveValues[i];
                }
            }
        }

        #endregion

        #region GENERATE CHILD FIELD
        /// <summary>
        /// 產生子代
        /// </summary>
        /// <param name="fatherIdx"></param>
        /// <param name="motherIdx"></param>
        /// <param name="childIdx_1"></param>
        /// <param name="childIdx_2"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void GenerateAPairOfCrossoveredChildren(int fatherIdx, int motherIdx, int childIdx_1, int childIdx_2)
        {
            // 子類別的責任
            throw new NotImplementedException();
        }

        /// <summary>
        /// 產生 mutated child (gene based)
        /// </summary>
        /// <param name="parentIdx"></param>
        /// <param name="childIdx"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void GenerateAGeneBasedMutatedChild(int parentIdx, int childIdx)
        {
            // refer to mutatedFlags for gene value
            // 子類別的責任
            throw new NotImplementedException();
        }

        /// <summary>
        /// 產生mutated child
        /// </summary>
        /// <param name="parentIdx"></param>
        /// <param name="childIdx"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void GenerateAMutatedChild(int parentIdx, int childIdx)
        {
            // 子類別的責任
            throw new NotImplementedException();
        }
        #endregion
        
    }
}
