namespace Ch10___Bigram_Model
{
    partial class Form1
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tbFileAddress = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.tbResults = new System.Windows.Forms.TextBox();
            this.panelControls = new System.Windows.Forms.Panel();
            this.rbtnBigram = new System.Windows.Forms.RadioButton();
            this.rbtnTrigram = new System.Windows.Forms.RadioButton();
            this.tablePanel.SuspendLayout();
            this.panelControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(284, 3);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(97, 37);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate Sentence";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // tbFileAddress
            // 
            this.tbFileAddress.Location = new System.Drawing.Point(12, 10);
            this.tbFileAddress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbFileAddress.Name = "tbFileAddress";
            this.tbFileAddress.ReadOnly = true;
            this.tbFileAddress.Size = new System.Drawing.Size(102, 20);
            this.tbFileAddress.TabIndex = 1;
            this.tbFileAddress.TextChanged += new System.EventHandler(this.tbFileAddress_TextChanged);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.AutoSize = true;
            this.btnSelectFile.Location = new System.Drawing.Point(116, 9);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "Select File...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // tablePanel
            // 
            this.tablePanel.ColumnCount = 1;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanel.Controls.Add(this.tbResults, 0, 1);
            this.tablePanel.Controls.Add(this.panelControls, 0, 0);
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 2;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanel.Size = new System.Drawing.Size(387, 264);
            this.tablePanel.TabIndex = 3;
            // 
            // tbResults
            // 
            this.tbResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResults.Location = new System.Drawing.Point(2, 52);
            this.tbResults.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbResults.Multiline = true;
            this.tbResults.Name = "tbResults";
            this.tbResults.Size = new System.Drawing.Size(383, 210);
            this.tbResults.TabIndex = 0;
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.rbtnTrigram);
            this.panelControls.Controls.Add(this.rbtnBigram);
            this.panelControls.Controls.Add(this.tbFileAddress);
            this.panelControls.Controls.Add(this.btnGenerate);
            this.panelControls.Controls.Add(this.btnSelectFile);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControls.Location = new System.Drawing.Point(2, 2);
            this.panelControls.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(383, 46);
            this.panelControls.TabIndex = 1;
            // 
            // rbtnBigram
            // 
            this.rbtnBigram.AutoSize = true;
            this.rbtnBigram.Location = new System.Drawing.Point(201, 3);
            this.rbtnBigram.Name = "rbtnBigram";
            this.rbtnBigram.Size = new System.Drawing.Size(57, 17);
            this.rbtnBigram.TabIndex = 3;
            this.rbtnBigram.Text = "Bigram";
            this.rbtnBigram.UseVisualStyleBackColor = true;
            // 
            // rbtnTrigram
            // 
            this.rbtnTrigram.AutoSize = true;
            this.rbtnTrigram.Checked = true;
            this.rbtnTrigram.Location = new System.Drawing.Point(201, 23);
            this.rbtnTrigram.Name = "rbtnTrigram";
            this.rbtnTrigram.Size = new System.Drawing.Size(60, 17);
            this.rbtnTrigram.TabIndex = 4;
            this.rbtnTrigram.TabStop = true;
            this.rbtnTrigram.Text = "Trigram";
            this.rbtnTrigram.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(387, 264);
            this.Controls.Add(this.tablePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Ch10 - Bigram Model - Sentence Generator";
            this.tablePanel.ResumeLayout(false);
            this.tablePanel.PerformLayout();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox tbFileAddress;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.TextBox tbResults;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.RadioButton rbtnTrigram;
        private System.Windows.Forms.RadioButton rbtnBigram;
    }
}

