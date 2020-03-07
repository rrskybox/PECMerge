using System;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PECMerge
{

    public partial class FormPECMerge : Form
    {
        public string[] pecfiles = null;
        public int dpntCount = 0;
        public int fileCount = 0;
        public double[,] dpntArray = null;
        public double[] avgVals;  //average per even sample
        public double[] noiseVals;  //average per odd sample
        public string clipOut;

        public FormPECMerge()
        {
            InitializeComponent();
            // Acquire the version information and put it in the form header
            try { this.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(); }
            catch { this.Text = " in Debug"; } //probably in debug, no version info available
            this.Text = "PEC Merge V" + this.Text;
        }

        private void LoadFilesButton_Click(object sender, EventArgs e)
        {
            PECResultsFileDialog.ShowDialog();
            pecfiles = PECResultsFileDialog.FileNames;

            GraphFiles();
            return;
        }

        public void GraphFiles()
        {
            Color logCurveColor = Color.Green;
            Color avgCurveColor = Color.Blue;
            Color logFreqColor = Color.Blue;
            Color noiseColor = Color.Red;
            Color medianFreqColor = Color.Red;

            int logCurveBorderWidth = 1;
            int avgCurveBorderWidth = 1;
            int logFreqBorderWidth = 1;
            int medianFreqBorderWidth = 1;
            int noiseBorderWidth = 1;


            const int maxPeriod = 20;
            fileCount = pecfiles.Length;
            dpntCount = 0;
            clipOut = null;

            Complex[,] cplxArray = null;
            Complex[,] fpntArray = null;


            char[] spc = { ' ', '\t' };

            for (int f = 0; f < fileCount; f++)
            {
                string[] fStringVals = File.ReadAllLines(pecfiles[f]);
                dpntCount = fStringVals.Length;

                //if this is the first file, then create idXVal array for contents of files
                if (f == 0)
                {
                    dpntArray = new double[fileCount, dpntCount];
                    cplxArray = new Complex[fileCount, dpntCount];
                    fpntArray = new Complex[fileCount, dpntCount];
                }

                //copy the contents into the idxVals
                for (int i = 0; i < fStringVals.Length; i++)
                {
                    dpntArray[f, i] = Convert.ToDouble(fStringVals[i].Split(spc, StringSplitOptions.RemoveEmptyEntries)[1]);
                    cplxArray[f, i] = new Complex(dpntArray[f, i], 0);
                }
            }

            //Let's graph this
            PECCurve.ChartAreas[0].AxisX.Minimum = 0;
            PECCurve.Series.Clear();
            PECFrequency.ChartAreas[0].AxisX.Minimum = 0;
            PECFrequency.Series.Clear();

            for (int f = 0; f < fileCount; f++)
            {
                //Series iSeries = new Series("PEC " + f.ToString());
                char[] splitter = { '.' };
                string[] shortNames = pecfiles[f].Split(splitter);
                string fileShortName = shortNames[shortNames.Length - 2];
                Series iSeries = new Series("Curve " + fileShortName);
                iSeries.XValueType = ChartValueType.Int32;
                iSeries.ChartType = SeriesChartType.FastLine;
                iSeries.MarkerStyle = MarkerStyle.Circle;
                iSeries.BorderWidth = logCurveBorderWidth;
                iSeries.Color = logCurveColor;
                for (int idx = 0; idx < dpntCount; idx++)
                {
                    iSeries.Points.AddXY(idx, Math.Round(dpntArray[f, idx]));
                }
                Complex[] fdx = new Complex[dpntCount];
                for (int c = 0; c < dpntCount; c++)
                {
                    fdx[c] = dpntArray[f, c];
                }
                // Compute DFT for each Series
                FourierTransform.DFT(fdx, FourierTransform.Direction.Forward);
                for (int c = 0; c < dpntCount; c++)
                {
                    fpntArray[f, c] = fdx[c];
                }

                Series fSeries = new Series("PECF " + fileShortName);
                fSeries.XValueType = ChartValueType.Double;
                fSeries.ChartType = SeriesChartType.Point;
                fSeries.MarkerStyle = MarkerStyle.Diamond;
                fSeries.BorderWidth = logFreqBorderWidth;
                fSeries.Color = logFreqColor;

                for (int idx = 0; idx <= maxPeriod; idx++)
                {
                    //int iFreq = idx + 1;
                    fSeries.Points.AddXY(idx, fdx[idx].Magnitude);
                }

                PECCurve.Series.Add(iSeries);
                PECFrequency.Series.Add(fSeries);
            }

            //graph the average and noise, if more than one PEC curve has been selected
            int fileMap = (int)Math.Pow(2, fileCount) - 1;
            SignalAverage(fileMap);

            Series aSeries = new Series("Average");
            aSeries.XValueType = ChartValueType.Int32;
            aSeries.ChartType = SeriesChartType.Line;
            aSeries.MarkerStyle = MarkerStyle.Circle;
            aSeries.BorderWidth = avgCurveBorderWidth;
            aSeries.Color = avgCurveColor;

            for (int idx = 0; idx < avgVals.Length; idx++)
            {
                aSeries.Points.AddXY(idx, (int)avgVals[idx]);
            }
            PECCurve.Series.Add(aSeries);

            Series nSeries = new Series("Est Noise");
            nSeries.XValueType = ChartValueType.Int32;
            nSeries.ChartType = SeriesChartType.FastLine;
            nSeries.MarkerStyle = MarkerStyle.Circle;
            aSeries.BorderWidth = noiseBorderWidth;
            nSeries.Color = noiseColor;

            PECCurve.ChartAreas[0].RecalculateAxesScale();
            PECFrequency.ChartAreas[0].RecalculateAxesScale();

            for (int idx = 0; idx < noiseVals.Length; idx++)
            {
                nSeries.Points.AddXY(idx, noiseVals[idx]);
            }
            PECCurve.Series.Add(nSeries);

            //Compute estimated noise via odd/even averages

            noiseTextBox.Text = NoiseEstimate().ToString("0.000");

            //Compute RMS of frequencies
            Series frmsSeries = new Series("Mean Frequency");
            frmsSeries.XValueType = ChartValueType.Int32;
             frmsSeries.IsValueShownAsLabel = true;
           frmsSeries.ChartType = SeriesChartType.Spline;
            frmsSeries.MarkerStyle = MarkerStyle.Circle;
            frmsSeries.LabelFormat = "G2";
            frmsSeries.Color = medianFreqColor;
            frmsSeries.BorderWidth = medianFreqBorderWidth;

            Complex[] meanFreqs = RMS(fpntArray, fileCount, dpntCount);
            for (int c = 0; c <= maxPeriod; c++)
            {
                frmsSeries.Points.AddXY(c, meanFreqs[c].Magnitude);
                //frmsSeries.Points[c].Label = meanFreqs[c].Magnitude.ToString("0.00");
            }
            PECFrequency.Series.Add(frmsSeries);

            int filePick = BestBehavior(fpntArray, meanFreqs);
            char[] fsplitter = { '.','\\','/' };
            string[] fshortNames = pecfiles[filePick].Split(fsplitter);
            string ffileShortName = fshortNames[fshortNames.Length - 2];

            BestLogBox.Text = ffileShortName;

            noiseTextBox.Text = NoiseEstimate().ToString("0.00");
            //Copy the result to the clipboard
            Clipboard.SetText(clipOut);
            return;
        }

        public static Complex[] RMS(Complex[,] dataArray, int dataSets, int dataPnts)
        {
            Complex[] meanVctr = new Complex[dataPnts];
            //Compute average 
            for (int pnt = 0; pnt < dataPnts; pnt++)
            {
                Complex mean = 0;
                for (int set = 0; set < dataSets; set++)
                {
                    mean = Complex.Add(Complex.Pow(dataArray[set, pnt], 2), mean);
                }
                meanVctr[pnt] = Complex.Sqrt(Complex.Divide(mean, dataSets));
            }
            return meanVctr;
        }

        private int BestBehavior(Complex[,] fpnts, Complex[] mpnts)
        {
            //figure out which combination of curve data produces the best match with the mean
            //For each set of fpnts, compute a sum of difference between freq and mean
            double[] fdif = new double[fileCount];
            for (int df = 0; df < fileCount; df++)
            {
                fdif[df] = 0;
                for (int dpt = 0; dpt < mpnts.Length / 2; dpt++)
                {
                    fdif[df] += Math.Abs(fpnts[df, dpt].Magnitude - mpnts[dpt].Magnitude);
                }
                fdif[df] /= mpnts.Length / 2;
            }
            //Find the smallest value
            double min = fdif[0];
            int minIndex = 0;
            for (int i = 1; i < fileCount; i++)
            {
                if (fdif[i] < min)
                {
                    minIndex = i;
                    min = fdif[i];
                }
            }
            return minIndex;
        }


        private void SignalAverage(int fileMap)
        {
            //Compute average (mean) for each position
            double oddAvg;
            double evenAvg;
            int evenCnt;
            int oddCnt;

            avgVals = new double[dpntCount];  //average per even sample
            noiseVals = new double[dpntCount];  //average per odd sample

            for (int idx = 0; idx < dpntCount; idx++)
            {
                evenAvg = 0;
                evenCnt = 0;
                oddAvg = 0;
                oddCnt = 0;

                for (int f = 0; f < fileCount; f += 2)
                {
                    if ((fileMap & (f + 1)) != 0)
                    {
                        evenAvg += dpntArray[f, idx];
                        evenCnt++;
                    }
                }
                for (int f = 1; f < fileCount; f += 2)
                {
                    if ((fileMap & (f + 1)) != 0)
                    {
                        oddAvg += dpntArray[f, idx];
                        oddCnt++;
                    }
                }
                evenAvg /= evenCnt;
                oddAvg /= oddCnt;

                if (fileCount > 1)
                {
                    avgVals[idx] = Math.Round((evenAvg + oddAvg) / 2.0);
                    noiseVals[idx] = Math.Abs(oddAvg - evenAvg) / 2.0;
                }
                else
                {
                    avgVals[idx] = dpntArray[0, idx];
                    noiseVals[idx] = 0;
                }

                string indStr = idx.ToString().PadLeft(4);
                string valStr = ((int)Math.Round(avgVals[idx])).ToString().PadLeft(5);
                clipOut += indStr + valStr + "\r\n";

            }
        }

        private double NoiseEstimate()
        {
            //Compute estimated noise via odd/even averages
            double noiseEstimate = 0;
            for (int idx = 0; idx < dpntCount; idx += 2) { noiseEstimate += noiseVals[idx]; }
            noiseEstimate /= (dpntCount / 2);
            return Math.Abs(noiseEstimate);
        }

        private double SignalEstimate()
        {
            //Compute estimated noise via odd/even averages
            double signalEstimate = 0;
            for (int idx = 1; idx < dpntCount; idx += 2) { signalEstimate += avgVals[idx]; }
            signalEstimate /= (dpntCount / 2);
            return Math.Abs(signalEstimate);
        }

    }

}



