using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGALibrary
{
    /// <summary>
    /// 
    /// </summary>
    public enum OptimizationType
    {
        /// <summary>
        /// 
        /// </summary>
        Maximization,

        /// <summary>
        /// 
        /// </summary>
        Minimization
    }

    /// <summary>
    /// 如何選擇下一代
    /// </summary>
    public enum SelectionMode
    {
        /// <summary>
        /// 
        /// </summary>
        Stochastic,

        /// <summary>
        /// 
        /// </summary>
        Deterministic
    }

    /// <summary>
    /// 
    /// </summary>
    public enum MutationMode
    {
        /// <summary>
        /// binary, int, real時
        /// </summary>
        GeneNumberBased,

        /// <summary>
        /// 整條時
        /// </summary>
        PopulationSizeBased
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CutMode
    {
        /// <summary>
        /// 
        /// </summary>
        OnePintCut, 

        /// <summary>
        /// 
        /// </summary>
        TwoPointCut, 

        /// <summary>
        /// 
        /// </summary>
        NPointCut
    }

    /// <summary>
    /// 排序編碼的標配交配運算子
    /// </summary>
    public enum PermutationCrossoverOperators
    {
        /// <summary>
        /// partial-mapped crossover operator
        /// </summary>
        PMX, 

        /// <summary>
        /// order crossover operator
        /// </summary>
        OX, 

        /// <summary>
        /// position-based crossover operator
        /// </summary>
        PBX, 

        /// <summary>
        /// order-based crossover operator
        /// </summary>
        OBX, 

        /// <summary>
        /// cycle crossover operator
        /// </summary>
        CX, 

        /// <summary>
        /// subtour exchange crossover operator
        /// </summary>
        SEX
    }

    /// <summary>
    /// 
    /// </summary>
    public enum PermutationMutationOperators
    {
        /// <summary>
        /// inversion mutation operator
        /// </summary>
        Inversion,

        /// <summary>
        /// insertion mutation operator
        /// </summary>
        Insertion,

        /// <summary>
        /// displacement mutation operator
        /// </summary>
        Displacement, 

        /// <summary>
        /// reciprocal exchange mutation operator
        /// </summary>
        SWAP, 

         
    }
}