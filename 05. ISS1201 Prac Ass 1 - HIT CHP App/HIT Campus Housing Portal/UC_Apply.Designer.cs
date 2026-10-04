namespace HIT_Campus_Housing_Portal
{
    partial class UC_Apply
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.panelApplied = new System.Windows.Forms.Panel();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.btnStatus = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelApplied.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelTop.Controls.Add(this.lblPath);
            this.panelTop.Controls.Add(this.lblDisplay);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(960, 40);
            this.panelTop.TabIndex = 32;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(815, 12);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(81, 17);
            this.lblPath.TabIndex = 8;
            this.lblPath.Text = "Home/Apply";
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplay.Location = new System.Drawing.Point(6, 12);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(91, 17);
            this.lblDisplay.TabIndex = 7;
            this.lblDisplay.Text = "Apply for Res";
            // 
            // panelApplied
            // 
            this.panelApplied.Controls.Add(this.lbl1);
            this.panelApplied.Controls.Add(this.lbl2);
            this.panelApplied.Location = new System.Drawing.Point(105, 270);
            this.panelApplied.Name = "panelApplied";
            this.panelApplied.Size = new System.Drawing.Size(750, 100);
            this.panelApplied.TabIndex = 33;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Century Gothic", 38.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(4, 1);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(743, 60);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "YOU HAVE ALREADY APPLIED.";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("Century Gothic", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(27, 61);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(697, 37);
            this.lbl2.TabIndex = 28;
            this.lbl2.Text = "Click STATUS to follow up on your application.\r\n";
            // 
            // btnStatus
            // 
            this.btnStatus.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnStatus.FlatAppearance.BorderSize = 0;
            this.btnStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatus.ForeColor = System.Drawing.Color.White;
            this.btnStatus.Location = new System.Drawing.Point(491, 90);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(200, 50);
            this.btnStatus.TabIndex = 31;
            this.btnStatus.Text = "STATUS";
            this.btnStatus.UseVisualStyleBackColor = false;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Location = new System.Drawing.Point(269, 90);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(200, 50);
            this.btnApply.TabIndex = 30;
            this.btnApply.Text = "APPLY";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // UC_Apply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelApplied);
            this.Controls.Add(this.btnStatus);
            this.Controls.Add(this.btnApply);
            this.Name = "UC_Apply";
            this.Size = new System.Drawing.Size(960, 670);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelApplied.ResumeLayout(false);
            this.panelApplied.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.Panel panelApplied;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.Button btnApply;
    }
}
