using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyGALibrary
{
    /// <summary>
    /// Binary GA
    /// </summary>
    public class BinaryGA : GenericGASolver<byte>
    {
        /// <summary>
        /// Crossover的cut point數量。
        /// </summary>
        [Category("Crossover"), Description("Crossover的cut point數量。")]
        public CutMode CrossOverCut { set; get; } = CutMode.OnePintCut;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="numberOfVariables"></param>
        /// <param name="optimizationType"></param>
        /// <param name="objectiveEvaluationFunction"></param>
        public BinaryGA(int numberOfVariables, OptimizationType optimizationType, ObjectiveFunction<byte> objectiveEvaluationFunction, Panel host = null) : 
            base(numberOfVariables, optimizationType, objectiveEvaluationFunction, host)
        {

        }

        /// <summary>
        /// 初始化Binary GA 的chromosomes
        /// </summary>
        protected override void InitializePopulation()
        {
            for(int r = 0; r < populationSize; r++)
            {
                for(int c = 0; c < numberOfGenes; c++)
                {
                    chromosomes[r][c] = (byte)randomizer.Next(2);
                }
            }
        }

        /// <summary>
        /// Crossover Operations
        /// </summary>
        /// <param name="fatherIdx"></param>
        /// <param name="motherIdx"></param>
        /// <param name="childIdx_1"></param>
        /// <param name="childIdx_2"></param>
        public override void GenerateAPairOfCrossoveredChildren(int fatherIdx, int motherIdx, int childIdx_1, int childIdx_2)
        {
            int cutPos = randomizer.Next(numberOfGenes);
            switch (CrossOverCut)
            {
                case CutMode.OnePintCut:
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if (c < cutPos)
                        {
                            chromosomes[childIdx_1][c] = chromosomes[fatherIdx][c];
                            chromosomes[childIdx_2][c] = chromosomes[motherIdx][c];
                        }
                        else
                        {
                            chromosomes[childIdx_1][c] = chromosomes[motherIdx][c];
                            chromosomes[childIdx_2][c] = chromosomes[fatherIdx][c];
                        }
                    }
                    break;

                case CutMode.TwoPointCut:
                    int cutPos2 = -1;
                    int cut1, cut2;

                    // 確認兩個cut point不一樣
                    if (cutPos2 == -1 || cutPos == cutPos2)
                    {
                        cutPos2 = randomizer.Next(numberOfGenes);
                    }

                    // 先確認哪個cut position大哪個小
                    if(cutPos > cutPos2)
                    {
                        cut1 = cutPos2;
                        cut2 = cutPos;
                    }
                    else
                    {
                        cut1 = cutPos;
                        cut2 = cutPos2;
                    }

                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if(c >= cut1 && c < cut2)
                        {
                            chromosomes[childIdx_1][c] = chromosomes[motherIdx][c];
                            chromosomes[childIdx_2][c] = chromosomes[fatherIdx][c];
                        }
                        else 
                        {
                            chromosomes[childIdx_1][c] = chromosomes[fatherIdx][c];
                            chromosomes[childIdx_2][c] = chromosomes[motherIdx][c];
                        }
                    }
                    break;

                case CutMode.NPointCut:
                    int numberOfCutPoints = 2 + randomizer.Next(numberOfGenes / 4); // 自己決定最多要幾個cut point，一定大於2
                    int currentNumOfCutPoint = 0;

                    // 初始化cutPositionFlags(都是false)
                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        cutPositionFlags[i] = false;
                    }

                    // 選擇cut point位置
                    for(int i = 0; i < numberOfCutPoints; i++)
                    {
                        int pos = randomizer.Next(numberOfGenes);
                        cutPositionFlags[pos] = true;
                    }

                    for(int c = 0; c < numberOfGenes; c++)
                    {
                        if (cutPositionFlags[c]) currentNumOfCutPoint++;

                        if(currentNumOfCutPoint % 2 == 0)
                        {
                            chromosomes[childIdx_1][c] = chromosomes[fatherIdx][c];
                            chromosomes[childIdx_2][c] = chromosomes[motherIdx][c];
                        }
                        else
                        {
                            chromosomes[childIdx_1][c] = chromosomes[motherIdx][c];
                            chromosomes[childIdx_2][c] = chromosomes[fatherIdx][c];
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Gene-based mutation
        /// </summary>
        /// <param name="parentIdx"></param>
        /// <param name="childIdx"></param>
        public override void GenerateAGeneBasedMutatedChild(int parentIdx, int childIdx)
        {
            // refer to mutatedFlags to mutate gene values
            for (int c = 0; c < numberOfGenes; c++)
            {
                if (mutatedFlags[parentIdx][c]) // 如果是突變的gene
                {
                    if (chromosomes[parentIdx][c] == 0) // 原本是0
                    {
                        chromosomes[childIdx][c] = 1; // 老師寫parentIdx?!
                    }
                    else // 原本是1
                    {
                        chromosomes[childIdx][c] = 0; // 老師寫parentIdx?!
                    }
                }
                else // 沒有要突變的gene
                {
                    chromosomes[childIdx][c] = chromosomes[parentIdx][c];
                }
                
            }
        }

        /// <summary>
        /// PopulationSize-based mutation
        /// </summary>
        /// <param name="parentIdx"></param>
        /// <param name="childIdx"></param>
        public override void GenerateAMutatedChild(int parentIdx, int childIdx)
        {
            // only mutate one gene
            int mutatePos = randomizer.Next(0, numberOfGenes);
            for (int c = 0; c < numberOfGenes; c++)
            {
                chromosomes[childIdx][c] = chromosomes[parentIdx][c];
            }

            if (chromosomes[parentIdx][mutatePos] == 0) // 原本是0
            {
                chromosomes[childIdx][mutatePos] = 1; // 老師寫parentIdx?!
            }
            else // 原本是1
            {
                chromosomes[childIdx][mutatePos] = 0; // 老師寫parentIdx?!
            }
        }
    }
}