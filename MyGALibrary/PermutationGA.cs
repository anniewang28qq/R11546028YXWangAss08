using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace MyGALibrary
{
    /// <summary>
    /// Permutation GA
    /// </summary>
    public class PermutationGA : GenericGASolver<int>
    {
        int pos1, pos2, small_pos, big_pos, sub_len, countMarked, numberOfPosition;
        int temp; // for swap
        int[] temp1; // PMX的草稿紙1
        int[] temp2; // PMX的草稿紙2
        int[][] relation_map; // PMX的relationship map
        int[] subString1; // OX被選到的genes / OBX order / inversion要invert的部分
        int[] subString2; // OBX order
        int[] unmarkedGenes; // OX的沒被選到的genes
        bool[] PositionFlags; // PBX選到的position
        /// <summary>
        /// Crossover的方法。
        /// </summary>
        [Category("Crossover"), Description("Crossover的方法。")]
        public PermutationCrossoverOperators CrossoverOperators { set; get; } = PermutationCrossoverOperators.PMX;

        /// <summary>
        /// Mutation的方法。
        /// </summary>
        [Category("Mutation"), Description("Mutation的方法。")]
        public PermutationMutationOperators MutationOperators { set; get; } = PermutationMutationOperators.Inversion;

        /// <summary>
        /// 只能用PopulationSizeBased!
        /// </summary>
        public override MutationMode MutationType 
        {
            get => base.MutationType;
            set
            {
                if (value == MutationMode.GeneNumberBased)
                {
                    MessageBox.Show("Permutation GA 不支援 Gene number-based mutation!");
                    MutationType = MutationMode.PopulationSizeBased;
                }
            }
        }

        /// <summary>
        /// Constructor of permutation GA
        /// </summary>
        /// <param name="numberOfVariables"></param>
        /// <param name="optimizationType"></param>
        /// <param name="objectiveEvaluationFunction"></param>
        public PermutationGA(int numberOfVariables, OptimizationType optimizationType, ObjectiveFunction<int> objectiveEvaluationFunction, Panel host = null) : 
            base(numberOfVariables, optimizationType, objectiveEvaluationFunction, host)
        {

            temp1 = new int[numberOfVariables];
            temp2 = new int[numberOfVariables];
            PositionFlags = new bool[numberOfVariables];
            relation_map = new int[numberOfVariables][];
            for (int i = 0; i < numberOfVariables; i++)
            {
                relation_map[i] = new int[2];
                for (int j = 0; j < 2; j++)
                {
                    relation_map[i][j] = -1;
                }
            }

            // for OX
            subString1 = new int[numberOfVariables];
            unmarkedGenes = new int[numberOfVariables];

            // for OBX
            subString2 = new int[numberOfVariables];
        }

        // helping function
        void ShuffleToInitializeAChromosome(int r)
        {
            for (int c = 0; c < numberOfGenes; c++)
            {
                chromosomes[r][c] = c;
            }
            // chromosomes[r]要shuffle
            for (int j = numberOfGenes - 1; j > 0; j--)
            {
                int pos = randomizer.Next(j + 1);
                int temp = chromosomes[r][pos];
                chromosomes[r][pos] = chromosomes[r][j];
                chromosomes[r][j] = temp;
            }
        }

        /// <summary>
        /// 初始化chromosomes
        /// </summary>
        protected override void InitializePopulation()
        {
            for(int r = 0; r < populationSize; r++)
            {
                ShuffleToInitializeAChromosome(r);
            }
        }

        /// <summary>
        /// 製作一對crossover children
        /// </summary>
        /// <param name="fatherIdx"></param>
        /// <param name="motherIdx"></param>
        /// <param name="childIdx_1"></param>
        /// <param name="childIdx_2"></param>
        public override void GenerateAPairOfCrossoveredChildren(int fatherIdx, int motherIdx, int childIdx_1, int childIdx_2)
        {
            switch (CrossoverOperators)
            {
                case PermutationCrossoverOperators.PMX:
                    #region PMX FIELD
                    // select tow positions
                    pos1 = randomizer.Next(numberOfGenes);
                    pos2 = randomizer.Next(numberOfGenes);
                    small_pos = Math.Min(pos1, pos2);
                    big_pos = Math.Max(pos1, pos2);

                    // exchange the substring of parents
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if(c >= small_pos && c < big_pos)
                        {
                            temp1[c] = chromosomes[motherIdx][c];
                            temp2[c] = chromosomes[fatherIdx][c];
                        }
                        else
                        {
                            temp1[c] = chromosomes[fatherIdx][c];
                            temp2[c] = chromosomes[motherIdx][c];
                        }
                    }

                    // determine mapping relationship
                    sub_len = big_pos - small_pos;
                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            relation_map[i][j] = -1;
                        }
                    }

                    for (int c = small_pos; c < big_pos; c++)
                    {
                        relation_map[c - small_pos][0] = temp1[c];
                        relation_map[c - small_pos][1] = temp2[c];
                    }
                    for (int i = relation_map.Length - 1; i > 0; i--)
                    {
                        for (int j = i - 1; j > -1; j--)
                        {
                            if (relation_map[i][0] == relation_map[j][1])
                            {
                                relation_map[j][1] = relation_map[i][1];
                                relation_map[i][0] = relation_map[i][1] = -1;
                                break;
                            }
                            else if (relation_map[i][1] == relation_map[j][0])
                            {
                                relation_map[j][0] = relation_map[i][0];
                                relation_map[i][0] = relation_map[i][1] = -1;
                                break;
                            }
                        }
                    }

                    // legalize offspring with the mapping relationship
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        // 交換的那段
                        if(c >= small_pos && c < big_pos)
                        {
                            chromosomes[childIdx_1][c] = temp1[c];
                            chromosomes[childIdx_2][c] = temp2[c];
                        }
                        else
                        {
                            // child 1
                            if (IsContain(temp1[c], relation_map))
                            {
                                for (int i = 0; i < sub_len; i++)
                                {
                                    if (temp1[c] == relation_map[i][0])
                                    {
                                        chromosomes[childIdx_1][c] = relation_map[i][1];
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                chromosomes[childIdx_1][c] = temp1[c];
                            }

                            // child 2
                            if (IsContain(temp2[c], relation_map))
                            {
                                for (int i = 0; i < sub_len; i++)
                                {
                                    if (temp2[c] == relation_map[i][1])
                                    {
                                        chromosomes[childIdx_2][c] = relation_map[i][0];
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                chromosomes[childIdx_2][c] = temp2[c];
                            }
                        }
                    }
                    #endregion
                    break;
                
                case PermutationCrossoverOperators.OX:
                    #region OX FIELD
                    // select tow positions
                    pos1 = randomizer.Next(numberOfGenes);
                    pos2 = randomizer.Next(numberOfGenes);
                    small_pos = Math.Min(pos1, pos2);
                    big_pos = Math.Max(pos1, pos2);
                    for(int  c = 0; c < numberOfGenes; c++)
                    {
                        subString1[c] = -1;
                        unmarkedGenes[c] = -1;
                    }

                    // copy the substring of father into child 1
                    for (int c = small_pos; c < big_pos; c++)
                    {
                        chromosomes[childIdx_1][c] = chromosomes[fatherIdx][c];
                        subString1[c - small_pos] = chromosomes[fatherIdx][c];
                    }

                    // mark out the genes in mother
                    countMarked = 0;
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if (!subString1.Contains(chromosomes[motherIdx][c]))
                        {
                            unmarkedGenes[countMarked++] = chromosomes[motherIdx][c];
                        }
                    }

                    // add the unmarked genes left in mother
                    countMarked = 0;
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if(c < small_pos || c >= big_pos)
                        {
                            chromosomes[childIdx_1][c] = unmarkedGenes[countMarked++];
                        }
                    }

                    // copy the substring of father into child 2
                    for (int c = small_pos; c < big_pos; c++)
                    {
                        chromosomes[childIdx_2][c] = chromosomes[motherIdx][c];
                        subString1[c - small_pos] = chromosomes[motherIdx][c];
                    }

                    // mark out the genes in father
                    countMarked= 0;
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if (!subString1.Contains(chromosomes[fatherIdx][c]))
                        {
                            unmarkedGenes[countMarked++] = chromosomes[fatherIdx][c];
                        }
                    }

                    // add the unmarked genes left in mother
                    countMarked = 0;
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if (c < small_pos || c >= big_pos)
                        {
                            chromosomes[childIdx_2][c] = unmarkedGenes[countMarked++];
                        }
                    }
                    #endregion
                    break;

                case PermutationCrossoverOperators.PBX:
                    #region PBX FIELD
                    numberOfPosition = randomizer.Next(numberOfGenes);// 幾個position

                    // 初始化position flag and unmarkedGenes
                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        PositionFlags[i] = false;
                        unmarkedGenes[i] = -1;
                        subString1[i] = -1;
                    }
                    for(int i = 0; i < numberOfPosition; i++)
                    {
                        subString1[i] = -1;
                    }

                    // 選擇position
                    for (int i = 0; i < numberOfPosition; i++)
                    {
                        int pos = randomizer.Next(numberOfGenes);
                        PositionFlags[pos] = true;
                    }

                    // copy genes from father to child 1
                    countMarked = 0;
                    for(int c = 0; c < numberOfGenes; c++)
                    {
                        if (PositionFlags[c])
                        {
                            chromosomes[childIdx_1][c] = chromosomes[fatherIdx][c];
                            subString1[countMarked++] = chromosomes[fatherIdx][c];
                        }
                    }

                    // mark out the genes in mother
                    countMarked = 0;
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if (!subString1.Contains(chromosomes[motherIdx][c]))
                        {
                            unmarkedGenes[countMarked++] = chromosomes[motherIdx][c];
                        }
                    }

                    // add the unmarked genes left in mother
                    countMarked = 0;
                    for(int c = 0; c < numberOfGenes; c++)
                    {
                        if (!PositionFlags[c])
                        {
                            chromosomes[childIdx_1][c] = unmarkedGenes[countMarked++];
                        }
                    }

                    // copy genes from mother to child 2
                    countMarked = 0;
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if (PositionFlags[c])
                        {
                            chromosomes[childIdx_2][c] = chromosomes[motherIdx][c];
                            subString1[countMarked++] = chromosomes[motherIdx][c];
                        }
                    }

                    // mark out the genes in father
                    countMarked = 0;
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if (!subString1.Contains(chromosomes[fatherIdx][c]))
                        {
                            unmarkedGenes[countMarked++] = chromosomes[fatherIdx][c];
                        }
                    }

                    // add the unmarked genes left in father
                    countMarked = 0;
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if (!PositionFlags[c])
                        {
                            chromosomes[childIdx_2][c] = unmarkedGenes[countMarked++];
                        }
                    }
                    #endregion
                    break;
                case PermutationCrossoverOperators.OBX:
                    #region OBX FIELD
                    numberOfPosition = randomizer.Next(numberOfGenes);// 幾個position

                    // 初始化position flag and substring
                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        PositionFlags[i] = false;
                        subString1[i] = -1;
                        subString2[i] = -1;
                    }

                    // 選擇position
                    for (int i = 0; i < numberOfPosition; i++)
                    {
                        int pos = randomizer.Next(numberOfGenes);
                        PositionFlags[pos] = true;
                    }

                    // copy genes form father to subsring
                    countMarked = 0;
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if (PositionFlags[c])
                        {
                            subString1[countMarked++] = chromosomes[fatherIdx][c];
                        }
                    }

                    // copy genes from mother to child 1
                    countMarked = 0;
                    for(int c = 0; c < numberOfGenes; c++)
                    {
                        if (!subString1.Contains(chromosomes[motherIdx][c]))
                        {
                            chromosomes[childIdx_1][c] = chromosomes[motherIdx][c];
                        }
                        else
                        {
                            subString2[countMarked] = chromosomes[motherIdx][c];
                            chromosomes[childIdx_1][c] = subString1[countMarked++];
                        }
                    }

                    // copy genes from father to child 2
                    countMarked = 0;
                    for (int c = 0; c < numberOfGenes; c++)
                    {
                        if (!subString2.Contains(chromosomes[fatherIdx][c]))
                        {
                            chromosomes[childIdx_2][c] = chromosomes[fatherIdx][c];
                        }
                        else
                        {
                            chromosomes[childIdx_2][c] = subString2[countMarked++];
                        }
                    }

                    #endregion
                    break;

                case PermutationCrossoverOperators.CX:
                    #region CX FIELD
                    pos1 = -1;
                    // 初始化position flag
                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        PositionFlags[i] = false;
                    }

                    // 確保cycle不會只有一個gene
                    for(int i = 0; i < numberOfGenes; i++)
                    {
                        if(chromosomes[fatherIdx][i] != chromosomes[motherIdx][i])
                        {
                            pos1 = i;
                            break;
                        }
                    }
                    
                    if(pos1 != -1)
                    {
                        PositionFlags[pos1] = true;
                        temp = chromosomes[motherIdx][pos1];

                        for (int i = pos1 + 1; i < numberOfGenes; i++)
                        {
                            if (chromosomes[fatherIdx][i] == temp)
                            {
                                PositionFlags[i] = true;
                                temp = chromosomes[motherIdx][i];
                            }

                            if (temp == chromosomes[fatherIdx][pos1]) // cycle is completed
                            {
                                break;
                            }
                            else if (i == numberOfGenes - 1) // 找到最後面了但cycle還沒completed 就回去最前面繼續找
                            {
                                i = 0;
                            }
                        }
                    }

                    // construct child
                    for(int c = 0; c < numberOfGenes; c++)
                    {
                        if (PositionFlags[c])
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

                    #endregion
                    break;

                case PermutationCrossoverOperators.SEX:
                    #region SEX FIELD
                    // 亂數抽一個長度
                    sub_len = randomizer.Next(numberOfGenes / 2);
                    // 初始化subtour
                    for(int i = 0; i < numberOfGenes; i++)
                    {
                        subString1[i] = -1;
                        subString2[i] = -1;
                    }

                    for(int c = 0; c < numberOfGenes - sub_len + 1; c++) // 從father的頭開始找
                    {
                        for(int i = 0; i < sub_len; i++) // 存subtour from father
                        {
                            subString1[i] = chromosomes[fatherIdx][c+i];
                        }

                        for(int k = 0; k < numberOfGenes - sub_len + 1; k++) // 從mother的頭開始找
                        {
                            for(int j = 0; j < sub_len; j++) // 找相對的長度
                            {
                                countMarked = 0;
                                if (IsContain(chromosomes[motherIdx][k+j], subString1)) // 如果有在father的subtour裡
                                {
                                    subString2[j] = chromosomes[motherIdx][k+j];
                                    countMarked++;
                                }
                                else // 沒有的話直接跳出去找下一個
                                {
                                    k = k + j;
                                    break;
                                }
                            }
                            pos2 = k;
                        }
                        if (countMarked == sub_len) // 找到
                        {
                            pos1 = c;
                            break;
                        }
                        // 全部都沒有跟father's subtour一樣的->用下一個father的subtour找
                    }

                    if(countMarked != sub_len) // 完全沒找到
                    {
                        for(int c = 0; c < numberOfGenes; c++)
                        {
                            chromosomes[childIdx_1][c] = chromosomes[fatherIdx][c];
                            chromosomes[childIdx_2][c] = chromosomes[motherIdx][c];
                        }
                    }
                    else // 有找到
                    {
                        // child 1
                        for(int c = 0; c < numberOfGenes; c++)
                        {
                            if(c >= pos1 && c < pos1 + sub_len)
                            {
                                chromosomes[childIdx_1][c] = subString2[c - pos1];
                            }
                            else
                            {
                                chromosomes[childIdx_1][c] = chromosomes[fatherIdx][c];
                            }
                        }
                        // child2
                        for (int c = 0; c < numberOfGenes; c++)
                        {
                            if (c >= pos2 && c < pos2 + sub_len)
                            {
                                chromosomes[childIdx_2][c] = subString1[c - pos2];
                            }
                            else
                            {
                                chromosomes[childIdx_2][c] = chromosomes[motherIdx][c];
                            }
                        }
                    }
                    #endregion
                    break;
            }
        }

        private bool IsContain(int v, int[] subString)
        {
            bool exist = false;
            for(int i = 0; i < subString.Length; i++)
            {
                if(v == subString[i])
                {
                    exist = true;
                    return exist;
                }
            }
            return exist;
        }

        // gene是否有在map裡
        private bool IsContain(int v, int[][] relation_map)
        {
            bool exist = false;
            for(int i = 0; i < relation_map.Length; i++)
            {
                if(v == relation_map[i][0] || v == relation_map[i][1])
                {
                    if(relation_map[i][0] != relation_map[i][1])
                    {
                        exist = true;
                        return exist;
                    }
                    else
                    {
                        return exist;
                    }
                }
            }
            return exist;
        }

        /// <summary>
        /// 建構一個mutated child
        /// </summary>
        /// <param name="parentIdx"></param>
        /// <param name="childIdx"></param>
        public override void GenerateAMutatedChild(int parentIdx, int childIdx)
        {
            // copy parent to child
            for(int c = 0; c < numberOfGenes; c++)
            {
                chromosomes[childIdx][c] = chromosomes[parentIdx][c];
            }

            switch (MutationOperators)
            {
                case PermutationMutationOperators.Inversion:
                    #region INVERSE FIELD
                    // select tow positions
                    int temp = -1;
                    pos1 = randomizer.Next(numberOfGenes);
                    pos2 = randomizer.Next(numberOfGenes);
                    small_pos = Math.Min(pos1, pos2);
                    big_pos = Math.Max(pos1, pos2);
                    sub_len = big_pos - small_pos;

                    for(int i = 0; i < (sub_len / 2); i++)
                    {
                        temp = chromosomes[childIdx][i + small_pos];
                        chromosomes[childIdx][i + small_pos] = chromosomes[childIdx][big_pos - 1 - i];
                        chromosomes[childIdx][big_pos - 1 - i] = temp;
                    }
                    #endregion
                    break;

                case PermutationMutationOperators.Insertion:
                    #region INSERTION FIELD
                    // select tow positions and the len of subtour
                    pos1 = randomizer.Next(numberOfGenes);
                    pos2 = randomizer.Next(numberOfGenes);

                    if (pos1 == pos2) return;

                    int gene = chromosomes[childIdx][pos1];

                    if (pos1 < pos2)
                    {
                        for (int c = pos1; c < pos2; c++)
                        {
                            chromosomes[childIdx][c] = chromosomes[childIdx][c + 1];
                        }
                        chromosomes[childIdx][pos2] = gene;
                    }
                    else if(pos2 < pos1)
                    {
                        for(int c = pos1; c > pos2; c--)
                        {
                            chromosomes[childIdx][c] = chromosomes[childIdx][c - 1];
                        }
                        chromosomes[childIdx][pos2] = gene;
                    }

                    #endregion
                    break;

                case PermutationMutationOperators.Displacement:
                    #region DISPLACEMENT FIELD
                    // select tow positions and the len of subtour
                    sub_len = randomizer.Next(numberOfGenes);
                    pos1 = randomizer.Next(numberOfGenes - sub_len);
                    pos2 = randomizer.Next(numberOfGenes - sub_len);

                    if (pos1 == pos2) return;

                    for(int i = 0; i < numberOfGenes; i++)
                    {
                        subString1[i] = -1;
                    }

                    for (int c = pos1; c < pos1 + sub_len; c++)
                    {
                        subString1[c - pos1] = chromosomes[childIdx][c];
                    }

                    if (pos1 < pos2)
                    {
                        countMarked = 0;
                        for (int c = pos1; c < pos2; c++)
                        {
                            chromosomes[childIdx][c] = chromosomes[childIdx][c + sub_len];
                        }
                        for (int c = pos2; c < pos2 + sub_len; c++)
                        {
                            chromosomes[childIdx][c] = subString1[countMarked++];
                        }
                    }
                    else if (pos2 < pos1)
                    {
                        countMarked = 0;
                        for (int c = pos1 + sub_len - 1; c > pos2 + sub_len - 1; c--)
                        {
                            chromosomes[childIdx][c] = chromosomes[childIdx][c - sub_len];
                        }
                        for (int c = pos2; c < pos2 + sub_len; c++)
                        {
                            chromosomes[childIdx][c] = subString1[countMarked++];
                        }
                    }

                    #endregion
                    break;

                case PermutationMutationOperators.SWAP:
                    #region SWAP FIELD
                    // pos1 2 可以一樣沒關係
                    pos1 = randomizer.Next(numberOfGenes);
                    pos2 = randomizer.Next(numberOfGenes);

                    // swap
                    temp = chromosomes[childIdx][pos1];
                    chromosomes[childIdx][pos1] = chromosomes[childIdx][pos2];
                    chromosomes[childIdx][pos2] = temp;
                    #endregion
                    break;
            }
        }

        //public override void GenerateAGeneBasedMutatedChild(int parentIdx, int childIdx)
        //{
        //    MessageBox.Show("Permutation GA 不支援 Gene based mutated!");
        //}
    }
}