namespace YGOPRODraft
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
            this.ConfigurePackBtn = new System.Windows.Forms.Button();
            this.FindYGOPRODBBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConfigurePackBtn
            // 
            this.ConfigurePackBtn.Location = new System.Drawing.Point(12, 57);
            this.ConfigurePackBtn.Name = "ConfigurePackBtn";
            this.ConfigurePackBtn.Size = new System.Drawing.Size(125, 31);
            this.ConfigurePackBtn.TabIndex = 6;
            this.ConfigurePackBtn.Text = "Convert .ydk to JSON";
            this.ConfigurePackBtn.UseVisualStyleBackColor = true;
            this.ConfigurePackBtn.Click += new System.EventHandler(this.ConfigurePackBtn_Click);
            // 
            // FindYGOPRODBBtn
            // 
            this.FindYGOPRODBBtn.Location = new System.Drawing.Point(12, 12);
            this.FindYGOPRODBBtn.Name = "FindYGOPRODBBtn";
            this.FindYGOPRODBBtn.Size = new System.Drawing.Size(81, 39);
            this.FindYGOPRODBBtn.TabIndex = 7;
            this.FindYGOPRODBBtn.Text = "FindYGOPRO Path";
            this.FindYGOPRODBBtn.UseVisualStyleBackColor = true;
            this.FindYGOPRODBBtn.Click += new System.EventHandler(this.FindYGOPRODBBtn_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(149, 101);
            this.Controls.Add(this.FindYGOPRODBBtn);
            this.Controls.Add(this.ConfigurePackBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ConfigurePackBtn;
        private System.Windows.Forms.Button FindYGOPRODBBtn;
    }
}

