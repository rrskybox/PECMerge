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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.LoadFilesButton = new System.Windows.Forms.Button();
            this.PECResultsFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PECchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.noiseTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PECchart)).BeginInit();
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
            // PECchart
            // 
            chartArea2.Name = "ChartArea1";
            this.PECchart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.PECchart.Legends.Add(legend2);
            this.PECchart.Location = new System.Drawing.Point(12, 56);
            this.PECchart.Name = "PECchart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.PECchart.Series.Add(series2);
            this.PECchart.Size = new System.Drawing.Size(776, 417);
            this.PECchart.TabIndex = 1;
            this.PECchart.Text = "chart1";
            // 
            // noiseTextBox
            // 
            this.noiseTextBox.Location = new System.Drawing.Point(545, 22);
            this.noiseTextBox.Name = "noiseTextBox";
            this.noiseTextBox.Size = new System.Drawing.Size(100, 20);
            this.noiseTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(413, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Average Estimated Noise";
            // 
            // FormPECMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noiseTextBox);
            this.Controls.Add(this.PECchart);
            this.Controls.Add(this.LoadFilesButton);
            this.Name = "FormPECMerge";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PECchart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadFilesButton;
        private System.Windows.Forms.OpenFileDialog PECResultsFileDialog;
        private System.Windows.Forms.DataVisualization.Charting.Chart PECchart;
        private System.Windows.Forms.TextBox noiseTextBox;
        private System.Windows.Forms.Label label1;
    }
}

