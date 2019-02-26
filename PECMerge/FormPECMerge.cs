using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Deployment.Application;

namespace PECMerge
{
    public partial class FormPECMerge : Form
    {
        public FormPECMerge()
        {
            InitializeComponent();
            // Acquire the version information and put it in the form header
            try { this.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(); }
            catch { this.Text = " in Debug"; } //probably in debug, no version info available
            this.Text = "PECMerge V" + this.Text;
        }

        private void LoadFilesButton_Click(object sender, EventArgs e)
        {
            double[,] idxVal = null;
            int idxCount = 0;
            char[] spc = { ' ', '\t' };

            PECResultsFileDialog.ShowDialog();
            string[] pecfiles = PECResultsFileDialog.FileNames;
            int fileCount = pecfiles.Length;

            for (int f = 0; f < fileCount; f++)
            {
                string[] fStringVals = File.ReadAllLines(pecfiles[f]);
                idxCount = fStringVals.Length;

                //if this is the first file, then create idXVal array for contents of files
                if (f == 0) idxVal = new double[fileCount, idxCount];
                //copy the contents into the idxVals
                for (int i = 0; i < fStringVals.Length; i++)
                {
                    idxVal[f, i] = Convert.ToDouble(fStringVals[i].Split(spc, StringSplitOptions.RemoveEmptyEntries)[1]);
                }
            }

            //Compute average (mean) for each position
            double oddAvg;
            double evenAvg;
            int evenCnt;
            int oddCnt;
            string clipOut = null;

            double[] avgVals = new double[idxCount];  //average per even sample
            double[] noiseVals = new double[idxCount];  //average per odd sample

            for (int idx = 0; idx < idxCount; idx++)
            {
                evenAvg = 0;
                evenCnt = 0;
                oddAvg = 0;
                oddCnt = 0;
                for (int f = 0; f < fileCount; f += 2)
                {
                    evenAvg += idxVal[f, idx];
                    evenCnt++;
                }
                for (int f = 1; f < fileCount; f += 2)
                {
                    oddAvg += idxVal[f, idx];
                    oddCnt++;
                }
                evenAvg /= evenCnt;
                oddAvg /= oddCnt;

                avgVals[idx] = (evenAvg + oddAvg) / 2;
                noiseVals[idx] = Math.Abs(oddAvg - evenAvg) / 2;
                string indStr = idx.ToString().PadLeft(4);
                string valStr = ((int)Math.Round(avgVals[idx])).ToString().PadLeft(5);
                clipOut += indStr + valStr + "\r\n";
            }

            //Let's graph this
            PECchart.ChartAreas[0].AxisX.Minimum = 0;
            PECchart.Series.Clear();
            for (int f = 0; f < fileCount; f++)
            {
                Series iSeries = new Series("PEC " + f.ToString());
                iSeries.XValueType = ChartValueType.Int32;
                iSeries.ChartType = SeriesChartType.FastLine;
                iSeries.MarkerStyle = MarkerStyle.Circle;
                iSeries.Color = Color.LightBlue;
                for (int idx = 0; idx < idxCount; idx++)
                {
                    iSeries.Points.AddXY(idx, Math.Round(idxVal[f, idx]));
                }
                PECchart.Series.Add(iSeries);
            }

            //graph the average and noise, if more than one PEC curve has been selected
            if (fileCount > 1)
            {
                Series aSeries = new Series("Average");
                aSeries.XValueType = ChartValueType.Int32;
                aSeries.ChartType = SeriesChartType.FastLine;
                aSeries.MarkerStyle = MarkerStyle.Circle;
                aSeries.Color = Color.Green;
                for (int idx = 0; idx < avgVals.Length; idx++)
                {
                    aSeries.Points.AddXY(idx, (int)avgVals[idx]);
                }
                PECchart.Series.Add(aSeries);

                Series nSeries = new Series("Est Noise");
                nSeries.XValueType = ChartValueType.Int32;
                nSeries.ChartType = SeriesChartType.FastLine;
                nSeries.MarkerStyle = MarkerStyle.Circle;
                nSeries.Color = Color.Red;
                for (int idx = 0; idx < noiseVals.Length; idx++)
                {
                    nSeries.Points.AddXY(idx, noiseVals[idx]);
                }
                PECchart.Series.Add(nSeries);

                //Compute estimated noise via odd/even averages
                double noiseEstimate = 0;
                for (int idx = 0; idx < idxCount; idx += 2) { noiseEstimate += noiseVals[idx]; }
                noiseEstimate /= (idxCount / 2);
                noiseTextBox.Text = noiseEstimate.ToString("0.000");

            }
            //Copy the result to the clipboard
            Clipboard.SetText(clipOut);
            return;
        }


    }
}

