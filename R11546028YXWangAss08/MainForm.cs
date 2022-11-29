using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MyGALibrary;

namespace R11546028YXWangAss07
{
    public partial class MainForm : Form
    {
        BinaryGA myBinaryGASolver;
        int num_Genes = 10;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // btn_Reset.Enabled = btn_RunOneIteration.Enabled = btn_RunToEnd.Enabled = false;
        }

        private void tsb_CreateBinaryGA_Click(object sender, EventArgs e)
        {
            pga = null;
            myBinaryGASolver = new BinaryGA((num_Genes * num_Genes), OptimizationType.Minimization, BinaryJobProblemFunction, splitContainer_Form.Panel1);
            myBinaryGASolver.PopulationSize = num_Genes;
            
            myBinaryGASolver.OneIterationCompleted += MyBinaryGASolver_OneIterationCompleted;
            myBinaryGASolver.SoFarTheBestSolutionUpdated += MyBinaryGASolver_SoFarTheBestSolutionUpdated;
            if(dgv_SetUpTime.Rows.Count != 0)
            {
                myBinaryGASolver.TheControl.btn_Reset.Enabled = true;
            }
            txb_Penalty.Enabled = true;
            tssl_type.Text = "Binary GA";
        }

        private void MyBinaryGASolver_OneIterationCompleted(object sender, EventArgs e)
        {
            l_numOfIteration.Text = $"One iteration completed at {myBinaryGASolver.IterationCount}.\n";
        }

        private void MyBinaryGASolver_SoFarTheBestSolutionUpdated(object sender, EventArgs e)
        {
            l_numOfSoFarUpdated.Text = $"So far the best solution is updated at {myBinaryGASolver.SoFarTheBestSolutionUpdatedTime}.\n";
        }

        // Testing optimization problem global best solution is 0101010101 obj=10...
        private double MySimpleObjFunction(byte[] chromosome)
        {
            double obj = 0;
            for (int i = 0; i < chromosome.Length; i++)
            {
                if (i % 2 == 0)
                {
                    if (chromosome[i] == 0) obj++;
                }
                else
                {
                    if (chromosome[i] == 1) obj++;
                }
            }
            return obj;
        }

        private double BinaryJobProblemFunction(byte[] chromosome)
        {
            double obj = 0;
            double binVariable_1 = 0;
            double binVariable_2 = 0;
            double penalty;

            for(int i = 0; i < num_Genes; i++)
            {
                for (int j = 0; j < num_Genes; j++)
                {
                    obj += Convert.ToInt32(dgv_SetUpTime.Rows[i].Cells[j].Value) * chromosome[i * num_Genes + j];
                }
            }

            for(int j = 0; j < num_Genes; j++)
            {
                double bin = 0;

                for (int i = 0; i < num_Genes; i++)
                {
                    bin += chromosome[i * num_Genes + j];
                }
                binVariable_1 += Math.Abs(bin - 1);
            }
            
            for(int i = 0; i < num_Genes; i++)
            {
                double bin = 0;

                for (int j = 0; j < num_Genes; j++)
                {
                    bin += chromosome[i * num_Genes + j];
                }
                binVariable_2 += Math.Abs(bin - 1);
            }

            penalty = Convert.ToDouble(txb_Penalty.Text) * (binVariable_1 + binVariable_2);

            return obj + penalty;
        }

        #region NO USE

        //private void btn_Reset_Click(object sender, EventArgs e)
        //{
        //    myBinaryGASolver.Reset();
        //    rtb_Iteration.Clear();
        //    rtb_SoFarTheBest.Clear();
        //    foreach (Series s in cht_Progress.Series)
        //    {
        //        s.Points.Clear();
        //    }

        //    for(int r = 0; r < myBinaryGASolver.PopulationSize; r++)
        //    {
        //        rtb_Iteration.AppendText($"p{r}: ");

        //        for (int c = 0; c < myBinaryGASolver.NumberOfGenes; c++)
        //        {
        //            rtb_Iteration.AppendText(myBinaryGASolver.Chromosomes[r][c].ToString() + " ");
        //        }
        //        rtb_Iteration.AppendText($" obj = {myBinaryGASolver.ObjectiveValues[r]}\n");
        //    }
        //    btn_RunOneIteration.Enabled = btn_RunToEnd.Enabled = true;
        //}

        //private void btn_RunOneIteration_Click(object sender, EventArgs e)
        //{
        //    rtb_Iteration.Clear();

        //    for (int r = 0; r < myBinaryGASolver.PopulationSize; r++)
        //    {
        //        rtb_Iteration.AppendText($"{r}: ");

        //        for (int c = 0; c < myBinaryGASolver.NumberOfGenes; c++)
        //        {
        //            rtb_Iteration.AppendText(myBinaryGASolver.Chromosomes[r][c].ToString() + " ");
        //        }
        //        rtb_Iteration.AppendText($" obj = {myBinaryGASolver.ObjectiveValues[r]}\n");
        //    }

        //    myBinaryGASolver.RunOneIteration();

        //    cht_Progress.Series[2].Points.AddXY(myBinaryGASolver.IterationCount, myBinaryGASolver.SoFarTheBestObjective);
        //    cht_Progress.Series[1].Points.AddXY(myBinaryGASolver.IterationCount, myBinaryGASolver.IterationBestObjective);
        //    cht_Progress.Series[0].Points.AddXY(myBinaryGASolver.IterationCount, myBinaryGASolver.IterationObjectiveAverage);

        //    cht_Progress.Update();
        //}

        //private void btn_RunToEnd_Click(object sender, EventArgs e)
        //{
        //    rtb_Iteration.Clear();

        //    // in show animation mode
        //    if (cbx_ShowAnimation.Checked)
        //    {
        //        while (myBinaryGASolver.IterationCount < myBinaryGASolver.IterationLimit)
        //        {
        //            btn_RunOneIteration_Click(null, null);
        //        }
        //    }
        //    else // hide animation
        //    {
        //        myBinaryGASolver.RunToEnd();

        //        for (int r = 0; r < myBinaryGASolver.PopulationSize; r++)
        //        {
        //            rtb_Iteration.AppendText($"{r}: ");

        //            for (int c = 0; c < myBinaryGASolver.NumberOfGenes; c += 7)
        //            {
        //                for (int j = 0; j < num_Genes; j++)
        //                {
        //                    if (myBinaryGASolver.Chromosomes[r][c + j] == 1)
        //                    {
        //                        rtb_Iteration.AppendText($"{j} ");
        //                    }
        //                }

        //                // rtb_Iteration.AppendText(myBinaryGASolver.Chromosomes[r][c].ToString() + " ");
        //            }
        //            rtb_Iteration.AppendText($" obj = {myBinaryGASolver.ObjectiveValues[r]}\n");
        //        }
        //    }

        //    // show final results on rich text box
        //    rtb_SoFarTheBest.Clear();
        //    rtb_SoFarTheBest.AppendText($"The Best Objective = {myBinaryGASolver.SoFarTheBestObjective} \nThe Best Solution = ");
        //    for(int i = 0; i < myBinaryGASolver.SoFarTheBestSolution.Length; i += 7)
        //    {
        //        for(int j = 0; j < num_Genes; j++)
        //        {
        //            if(myBinaryGASolver.SoFarTheBestSolution[i + j] == 1)
        //            {
        //                rtb_SoFarTheBest.AppendText($"{j} ");
        //            }
        //        }
        //        //rtb_SoFarTheBest.AppendText($"{myBinaryGASolver.SoFarTheBestSolution[i]} ");
        //    }
        //}

        #endregion

        PermutationGA pga;

        private void tsb_CreatePermutation_Click(object sender, EventArgs e)
        {
            myBinaryGASolver = null;
            pga = new PermutationGA(num_Genes, OptimizationType.Minimization, PermutationJobProblemFunction, splitContainer_Form.Panel1);
            
            pga.OneIterationCompleted += Pga_OneIterationCompleted;
            pga.SoFarTheBestSolutionUpdated += Pga_SoFarTheBestSolutionUpdated;

            if (dgv_SetUpTime.Rows.Count != 0)
            {
                pga.TheControl.btn_Reset.Enabled = true;
            }
            txb_Penalty.Enabled = false;
            tssl_type.Text = "Permutation GA";
        }

        private void Pga_OneIterationCompleted(object sender, EventArgs e)
        {
            l_numOfIteration.Text = $"One iteration completed at {pga.IterationCount}.\n";
        }

        private void Pga_SoFarTheBestSolutionUpdated(object sender, EventArgs e)
        {
            l_numOfSoFarUpdated.Text = $"So far the best solution is updated {pga.SoFarTheBestSolutionUpdatedTime}.";
        }

        // 反向排序最佳化 test max
        private double SequenceObj(int[] chromosome)
        {
            double obj = 0;
            for(int i = 0; i < chromosome.Length; i++)
            {
                if (chromosome[i] == chromosome.Length - i - 1)
                {
                    obj++;
                }
            }
            return obj;
        }

        private double PermutationJobProblemFunction(int[] chromosome)
        {
            double obj = 0;
            for (int i = 0; i < num_Genes; i++)
            {
                obj += Convert.ToInt32(dgv_SetUpTime.Rows[chromosome[i]].Cells[i].Value);
            }
            return obj;
        }

        #region OPEN FIELD
        private void tsb_Open_Click(object sender, EventArgs e)
        {
            dgv_SetUpTime.Rows.Clear();
            dgv_SetUpTime.Columns.Clear();

            if (myBinaryGASolver == null && pga == null)
            {
                MessageBox.Show("Please select a new GA first.");
                return;
            }
            // 讀檔
            if (dlg_Open.ShowDialog() != DialogResult.OK) return;
            StreamReader sr = new StreamReader(dlg_Open.FileName);
            num_Genes = Convert.ToInt32(sr.ReadLine());

            if(myBinaryGASolver != null)
            {
                tsb_CreateBinaryGA_Click(null, null);
                myBinaryGASolver.TheControl.btn_Reset.Enabled = true;
            }
            else if(pga != null)
            {
                tsb_CreatePermutation_Click(null, null);
                pga.TheControl.btn_Reset.Enabled = true;
            }

            for (int i = 0; i < num_Genes; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = col.HeaderText = $"M{i}";
                dgv_SetUpTime.Columns.Add(col);
            }
            for (int i = 0; i < num_Genes; i++)
            {
                dgv_SetUpTime.Rows.Add();
                dgv_SetUpTime.Rows[i].HeaderCell.Value = $"J{i}";

                DataGridViewCell cell;

                string str;
                string[] items;
                str = sr.ReadLine();
                items = str.Split(' ');

                for (int j = 0; j < num_Genes; j++)
                {
                    cell = dgv_SetUpTime.Rows[i].Cells[j];
                    cell.Value = Convert.ToDouble(items[j]);
                }
            }
           
        }
        #endregion
    }
}
