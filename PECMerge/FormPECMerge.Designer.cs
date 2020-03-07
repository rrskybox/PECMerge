namespace PECMerge
{
    partial class FormPECMerge
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.LoadFilesButton = new System.Windows.Forms.Button();
            this.PECResultsFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PECCurve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.noiseTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PECFrequency = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.BestLogBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PECCurve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PECFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadFilesButton
            // 
            this.LoadFilesButton.Location = new System.Drawing.Point(12, 12);
            this.LoadFilesButton.Name = "LoadFilesButton";
            this.LoadFilesButton.Size = new System.Drawing.Size(106, 38);
            this.LoadFilesButton.TabIndex = 0;
            this.LoadFilesButton.Text = "Load PEC Curves";
            this.LoadFilesButton.UseVisualStyleBackColor = true;
            this.LoadFilesButton.Click += new System.EventHandler(this.LoadFilesButton_Click);
            // 
            // PECResultsFileDialog
            // 
            this.PECResultsFileDialog.Multiselect = true;
            // 
            // PECCurve
            // 
            chartArea1.Name = "ChartArea1";
            this.PECCurve.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.PECCurve.Legends.Add(legend1);
            this.PECCurve.Location = new System.Drawing.Point(12, 56);
            this.PECCurve.Name = "PECCurve";
            this.PECCurve.Size = new System.Drawing.Size(776, 230);
            this.PECCurve.TabIndex = 1;
            this.PECCurve.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "PE Correction at Index";
            this.PECCurve.Titles.Add(title1);
            // 
            // noiseTextBox
            // 
            this.noiseTextBox.Location = new System.Drawing.Point(444, 18);
            this.noiseTextBox.Name = "noiseTextBox";
            this.noiseTextBox.Size = new System.Drawing.Size(53, 20);
            this.noiseTextBox.TabIndex = 2;
            this.noiseTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(312, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Average Estimated Noise";
            // 
            // PECFrequency
            // 
            chartArea2.AxisX.IsStartedFromZero = false;
            chartArea2.Name = "ChartArea1";
            this.PECFrequency.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.PECFrequency.Legends.Add(legend2);
            this.PECFrequency.Location = new System.Drawing.Point(12, 292);
            this.PECFrequency.Name = "PECFrequency";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.PECFrequency.Series.Add(series1);
            this.PECFrequency.Size = new System.Drawing.Size(776, 230);
            this.PECFrequency.TabIndex = 4;
            this.PECFrequency.Text = "chart1";
            title2.Name = "Title1";
            title2.Text = "Correction Frequency (per worm turn)";
            this.PECFrequency.Titles.Add(title2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(513, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Best Fit";
            // 
            // BestLogBox
            // 
            this.BestLogBox.Location = new System.Drawing.Point(561, 18);
            this.BestLogBox.Name = "BestLogBox";
            this.BestLogBox.Size = new System.Drawing.Size(227, 20);
            this.BestLogBox.TabIndex = 5;
            this.BestLogBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormPECMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(800, 530);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BestLogBox);
            this.Controls.Add(this.PECFrequency);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noiseTextBox);
            this.Controls.Add(this.PECCurve);
            this.Controls.Add(this.LoadFilesButton);
            this.Name = "FormPECMerge";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PECCurve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PECFrequency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadFilesButton;
        private System.Windows.Forms.OpenFileDialog PECResultsFileDialog;
        private System.Windows.Forms.DataVisualization.Charting.Chart PECCurve;
        private System.Windows.Forms.TextBox noiseTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart PECFrequency;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox BestLogBox;
    }
}

