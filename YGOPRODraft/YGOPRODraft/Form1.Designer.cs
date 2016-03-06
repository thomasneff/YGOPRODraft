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
            this.DebugConsole = new System.Windows.Forms.RichTextBox();
            this.ListChoosePacks = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ListChosenPacks = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ConfigurePackBtn = new System.Windows.Forms.Button();
            this.FindYGOPRODBBtn = new System.Windows.Forms.Button();
            this.numCardsPerBooster = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numRaresPerBooster = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numCardsPerDraftRound = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numCardsPerDeck = new System.Windows.Forms.TextBox();
            this.startServerButton = new System.Windows.Forms.Button();
            this.chkRemovePool = new System.Windows.Forms.CheckBox();
            this.chkPullOnlyOne = new System.Windows.Forms.CheckBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DebugConsole
            // 
            this.DebugConsole.Location = new System.Drawing.Point(12, 394);
            this.DebugConsole.Name = "DebugConsole";
            this.DebugConsole.Size = new System.Drawing.Size(863, 184);
            this.DebugConsole.TabIndex = 1;
            this.DebugConsole.Text = "";
            // 
            // ListChoosePacks
            // 
            this.ListChoosePacks.FormattingEnabled = true;
            this.ListChoosePacks.HorizontalScrollbar = true;
            this.ListChoosePacks.Location = new System.Drawing.Point(12, 94);
            this.ListChoosePacks.Name = "ListChoosePacks";
            this.ListChoosePacks.Size = new System.Drawing.Size(163, 277);
            this.ListChoosePacks.TabIndex = 2;
            this.ListChoosePacks.SelectedIndexChanged += new System.EventHandler(this.ListChoosePacks_SelectedIndexChanged);
            this.ListChoosePacks.DoubleClick += new System.EventHandler(this.ListChoosePacks_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Choose Packs";
            // 
            // ListChosenPacks
            // 
            this.ListChosenPacks.FormattingEnabled = true;
            this.ListChosenPacks.Location = new System.Drawing.Point(223, 94);
            this.ListChosenPacks.Name = "ListChosenPacks";
            this.ListChosenPacks.Size = new System.Drawing.Size(163, 277);
            this.ListChosenPacks.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Chosen Packs";
            // 
            // ConfigurePackBtn
            // 
            this.ConfigurePackBtn.Location = new System.Drawing.Point(12, 12);
            this.ConfigurePackBtn.Name = "ConfigurePackBtn";
            this.ConfigurePackBtn.Size = new System.Drawing.Size(136, 59);
            this.ConfigurePackBtn.TabIndex = 6;
            this.ConfigurePackBtn.Text = "Configure Pack Folder";
            this.ConfigurePackBtn.UseVisualStyleBackColor = true;
            this.ConfigurePackBtn.Click += new System.EventHandler(this.ConfigurePackBtn_Click);
            // 
            // FindYGOPRODBBtn
            // 
            this.FindYGOPRODBBtn.Location = new System.Drawing.Point(163, 12);
            this.FindYGOPRODBBtn.Name = "FindYGOPRODBBtn";
            this.FindYGOPRODBBtn.Size = new System.Drawing.Size(136, 59);
            this.FindYGOPRODBBtn.TabIndex = 7;
            this.FindYGOPRODBBtn.Text = "Find YGOPRO cards.cdb";
            this.FindYGOPRODBBtn.UseVisualStyleBackColor = true;
            this.FindYGOPRODBBtn.Click += new System.EventHandler(this.FindYGOPRODBBtn_Click);
            // 
            // numCardsPerBooster
            // 
            this.numCardsPerBooster.Location = new System.Drawing.Point(346, 51);
            this.numCardsPerBooster.Name = "numCardsPerBooster";
            this.numCardsPerBooster.Size = new System.Drawing.Size(100, 20);
            this.numCardsPerBooster.TabIndex = 8;
            this.numCardsPerBooster.Text = "7";
            this.numCardsPerBooster.TextChanged += new System.EventHandler(this.numCardsPerBooster_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(343, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Number of Cards Per Booster";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(505, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Number of Rares Per Booster";
            // 
            // numRaresPerBooster
            // 
            this.numRaresPerBooster.Location = new System.Drawing.Point(508, 51);
            this.numRaresPerBooster.Name = "numRaresPerBooster";
            this.numRaresPerBooster.Size = new System.Drawing.Size(100, 20);
            this.numRaresPerBooster.TabIndex = 10;
            this.numRaresPerBooster.Text = "1";
            this.numRaresPerBooster.TextChanged += new System.EventHandler(this.numRaresPerBooster_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(672, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Number of Cards Per Draft Round";
            // 
            // numCardsPerDraftRound
            // 
            this.numCardsPerDraftRound.Location = new System.Drawing.Point(675, 51);
            this.numCardsPerDraftRound.Name = "numCardsPerDraftRound";
            this.numCardsPerDraftRound.Size = new System.Drawing.Size(100, 20);
            this.numCardsPerDraftRound.TabIndex = 12;
            this.numCardsPerDraftRound.Text = "5";
            this.numCardsPerDraftRound.TextChanged += new System.EventHandler(this.numCardsPerDraftRound_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(438, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Number of Cards Per Deck";
            // 
            // numCardsPerDeck
            // 
            this.numCardsPerDeck.Location = new System.Drawing.Point(441, 158);
            this.numCardsPerDeck.Name = "numCardsPerDeck";
            this.numCardsPerDeck.Size = new System.Drawing.Size(100, 20);
            this.numCardsPerDeck.TabIndex = 14;
            this.numCardsPerDeck.Text = "45";
            this.numCardsPerDeck.TextChanged += new System.EventHandler(this.numCardsPerDeck_TextChanged);
            // 
            // startServerButton
            // 
            this.startServerButton.Location = new System.Drawing.Point(574, 210);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(115, 56);
            this.startServerButton.TabIndex = 16;
            this.startServerButton.Text = "Start Server";
            this.startServerButton.UseVisualStyleBackColor = true;
            this.startServerButton.Click += new System.EventHandler(this.startServerButton_Click);
            // 
            // chkRemovePool
            // 
            this.chkRemovePool.AutoSize = true;
            this.chkRemovePool.Location = new System.Drawing.Point(444, 105);
            this.chkRemovePool.Name = "chkRemovePool";
            this.chkRemovePool.Size = new System.Drawing.Size(178, 17);
            this.chkRemovePool.TabIndex = 17;
            this.chkRemovePool.Text = "Remove Pulled Cards From Pool";
            this.chkRemovePool.UseVisualStyleBackColor = true;
            this.chkRemovePool.CheckedChanged += new System.EventHandler(this.chkRemovePool_CheckedChanged);
            // 
            // chkPullOnlyOne
            // 
            this.chkPullOnlyOne.AutoSize = true;
            this.chkPullOnlyOne.Location = new System.Drawing.Point(628, 105);
            this.chkPullOnlyOne.Name = "chkPullOnlyOne";
            this.chkPullOnlyOne.Size = new System.Drawing.Size(194, 17);
            this.chkPullOnlyOne.TabIndex = 18;
            this.chkPullOnlyOne.Text = "Pull only from one Pack each round";
            this.chkPullOnlyOne.UseVisualStyleBackColor = true;
            this.chkPullOnlyOne.CheckedChanged += new System.EventHandler(this.chkPullOnlyOne_CheckedChanged);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.BackColor = System.Drawing.SystemColors.ControlLight;
            this.labelPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPath.Location = new System.Drawing.Point(343, 12);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(2, 15);
            this.labelPath.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 594);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.chkPullOnlyOne);
            this.Controls.Add(this.chkRemovePool);
            this.Controls.Add(this.startServerButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numCardsPerDeck);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numCardsPerDraftRound);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numRaresPerBooster);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numCardsPerBooster);
            this.Controls.Add(this.FindYGOPRODBBtn);
            this.Controls.Add(this.ConfigurePackBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListChosenPacks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListChoosePacks);
            this.Controls.Add(this.DebugConsole);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox DebugConsole;
        private System.Windows.Forms.ListBox ListChoosePacks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox ListChosenPacks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ConfigurePackBtn;
        private System.Windows.Forms.Button FindYGOPRODBBtn;
        private System.Windows.Forms.TextBox numCardsPerBooster;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox numRaresPerBooster;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox numCardsPerDraftRound;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox numCardsPerDeck;
        private System.Windows.Forms.Button startServerButton;
        private System.Windows.Forms.CheckBox chkRemovePool;
        private System.Windows.Forms.CheckBox chkPullOnlyOne;
        private System.Windows.Forms.Label labelPath;
    }
}

