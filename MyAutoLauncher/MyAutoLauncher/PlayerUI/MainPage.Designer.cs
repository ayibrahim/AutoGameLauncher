namespace PlayerUI
{
    partial class MainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.btnNewProcess = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNewProcess
            // 
            this.btnNewProcess.AllowDrop = true;
            this.btnNewProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.btnNewProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.btnNewProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewProcess.ForeColor = System.Drawing.Color.Silver;
            this.btnNewProcess.Image = ((System.Drawing.Image)(resources.GetObject("btnNewProcess.Image")));
            this.btnNewProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewProcess.Location = new System.Drawing.Point(12, 12);
            this.btnNewProcess.Name = "btnNewProcess";
            this.btnNewProcess.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnNewProcess.Size = new System.Drawing.Size(247, 45);
            this.btnNewProcess.TabIndex = 7;
            this.btnNewProcess.Text = "  Add New Process";
            this.btnNewProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewProcess.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNewProcess.UseVisualStyleBackColor = true;
            this.btnNewProcess.Click += new System.EventHandler(this.btnNewProcess_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.ClientSize = new System.Drawing.Size(538, 450);
            this.Controls.Add(this.btnNewProcess);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Auto Launcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainPage_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainPage_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNewProcess;
    }
}