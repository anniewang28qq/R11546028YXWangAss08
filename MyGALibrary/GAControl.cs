using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MyGALibrary
{
    /// <summary>
    /// User Controler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class GAControl<T> : UserControl
    {
        GenericGASolver<T> gasolver;
        /// <summary>
        /// CTOR
        /// </summary>
        public GAControl(GenericGASolver<T> ga)
        {
            InitializeComponent();
            gasolver = ga;
            prg_GA.SelectedObject = ga;
            btn_Reset.Enabled = btn_RunOneIteration.Enabled = btn_RunToEnd.Enabled = false;
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            gasolver.Reset();
            rtb_Iteration.Clear();
            rtb_SoFarTheBest.Clear();
            foreach (Series s in cht_Progress.Series)
            {
                s.Points.Clear();
            }

            for (int r = 0; r < gasolver.PopulationSize; r++)
            {
                rtb_Iteration.AppendText($"p{r}: ");

                for (int c = 0; c < gasolver.NumberOfGenes; c++)
                {
                    rtb_Iteration.AppendText($"{Convert.ToDouble(gasolver.Chromosomes[r][c])} ");
                }
                rtb_Iteration.AppendText($" obj = {gasolver.ObjectiveValues[r]}\n");
            }
            btn_RunOneIteration.Enabled = btn_RunToEnd.Enabled = true;
        }

        private void btn_RunOneIteration_Click(object sender, EventArgs e)
        {
            rtb_Iteration.Clear();

            for (int r = 0; r < gasolver.PopulationSize; r++)
            {
                rtb_Iteration.AppendText($"{r}: ");

                for (int c = 0; c < gasolver.NumberOfGenes; c++)
                {
                    rtb_Iteration.AppendText($"{Convert.ToDouble(gasolver.Chromosomes[r][c])} ");
                }
                rtb_Iteration.AppendText($" obj = {gasolver.ObjectiveValues[r]}\n");
            }

            gasolver.RunOneIteration();

            cht_Progress.Series[2].Points.AddXY(gasolver.IterationCount, gasolver.SoFarTheBestObjective);
            cht_Progress.Series[1].Points.AddXY(gasolver.IterationCount, gasolver.IterationBestObjective);
            cht_Progress.Series[0].Points.AddXY(gasolver.IterationCount, gasolver.IterationObjectiveAverage);

            if (cbx_ShowAnimation.Checked)
            {
                cht_Progress.Update();
            }
        }

        private void btn_RunToEnd_Click(object sender, EventArgs e)
        {
            rtb_Iteration.Clear();

            // in show animation mode
            if (cbx_ShowAnimation.Checked)
            {
                while (gasolver.IterationCount < gasolver.IterationLimit)
                {
                    btn_RunOneIteration_Click(null, null);
                }
            }
            else // hide animation
            {
                gasolver.RunToEnd();

                for (int r = 0; r < gasolver.PopulationSize; r++)
                {
                    rtb_Iteration.AppendText($"{r}: ");

                    for (int c = 0; c < gasolver.NumberOfGenes; c++)
                    {
                        //for (int j = 0; j < gasolver.NumberOfGenes; j++)
                        //{
                        //    if (gasolver.Chromosomes[r][c + j] == 1)
                        //    {
                        //        rtb_Iteration.AppendText($"{j} ");
                        //    }
                        //}

                        rtb_Iteration.AppendText($"{Convert.ToDouble(gasolver.Chromosomes[r][c])} ");
                    }
                    rtb_Iteration.AppendText($" obj = {gasolver.ObjectiveValues[r]}\n");
                }
            }

            // show final results on rich text box
            rtb_SoFarTheBest.Clear();
            rtb_SoFarTheBest.AppendText($"The Best Objective = {gasolver.SoFarTheBestObjective} \nThe Best Solution = ");
            for (int i = 0; i < gasolver.SoFarTheBestSolution.Length; i++)
            {
                //if (TypeDescriptor.GetClassName(gasolver) == "MyGALibrary.BinaryGA")
                //{
                //    for (int j = 0; j < gasolver.NumberOfGenes; j++)
                //    {
                //        if (Convert.ToInt32(gasolver.SoFarTheBestSolution[i + j]) == 1)
                //        {
                //            rtb_SoFarTheBest.AppendText($"{j} ");
                //        }
                //    }
                //}
                //else
                //{
                    
                //}
                rtb_SoFarTheBest.AppendText($"{Convert.ToDouble(gasolver.SoFarTheBestSolution[i])} ");
            }
        }
    }
}
