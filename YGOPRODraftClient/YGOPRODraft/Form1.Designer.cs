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
            this.ConfigurePackBtn = new System.Windows.Forms.Button();
            this.FindYGOPRODBBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.cardShowPanel = new System.Windows.Forms.Panel();
            this.labelCardName = new System.Windows.Forms.Label();
            this.labelCardDescription = new System.Windows.Forms.RichTextBox();
            this.labelCardAtk = new System.Windows.Forms.Label();
            this.labelCardDef = new System.Windows.Forms.Label();
            this.labelCardType = new System.Windows.Forms.Label();
            this.labelCardAttr = new System.Windows.Forms.Label();
            this.labelCardRace = new System.Windows.Forms.Label();
            this.labelCardLevel = new System.Windows.Forms.Label();
            this.panelDraftCards = new System.Windows.Forms.Panel();
            this.panelMainDeck = new System.Windows.Forms.Panel();
            this.panelExtraDeck = new System.Windows.Forms.Panel();
            this.panelSideDeck = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelNumMTS = new System.Windows.Forms.Label();
            this.labelPath = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DebugConsole
            // 
            this.DebugConsole.Location = new System.Drawing.Point(12, 394);
            this.DebugConsole.Name = "DebugConsole";
            this.DebugConsole.Size = new System.Drawing.Size(408, 184);
            this.DebugConsole.TabIndex = 1;
            this.DebugConsole.Text = "";
            // 
            // ConfigurePackBtn
            // 
            this.ConfigurePackBtn.Location = new System.Drawing.Point(12, 87);
            this.ConfigurePackBtn.Name = "ConfigurePackBtn";
            this.ConfigurePackBtn.Size = new System.Drawing.Size(112, 31);
            this.ConfigurePackBtn.TabIndex = 6;
            this.ConfigurePackBtn.Text = "Connect to Server";
            this.ConfigurePackBtn.UseVisualStyleBackColor = true;
            this.ConfigurePackBtn.Click += new System.EventHandler(this.ConfigurePackBtn_Click);
            // 
            // FindYGOPRODBBtn
            // 
            this.FindYGOPRODBBtn.Location = new System.Drawing.Point(12, 314);
            this.FindYGOPRODBBtn.Name = "FindYGOPRODBBtn";
            this.FindYGOPRODBBtn.Size = new System.Drawing.Size(81, 39);
            this.FindYGOPRODBBtn.TabIndex = 7;
            this.FindYGOPRODBBtn.Text = "FindYGOPRO cards.cdb";
            this.FindYGOPRODBBtn.UseVisualStyleBackColor = true;
            this.FindYGOPRODBBtn.Click += new System.EventHandler(this.FindYGOPRODBBtn_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 24);
            this.button1.TabIndex = 8;
            this.button1.Text = "Ready";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(12, 58);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxIP.TabIndex = 10;
            this.textBoxIP.TextChanged += new System.EventHandler(this.textBoxIP_TextChanged);
            // 
            // cardShowPanel
            // 
            this.cardShowPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cardShowPanel.Location = new System.Drawing.Point(702, 58);
            this.cardShowPanel.Name = "cardShowPanel";
            this.cardShowPanel.Size = new System.Drawing.Size(188, 269);
            this.cardShowPanel.TabIndex = 11;
            // 
            // labelCardName
            // 
            this.labelCardName.AutoSize = true;
            this.labelCardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardName.Location = new System.Drawing.Point(766, 18);
            this.labelCardName.Name = "labelCardName";
            this.labelCardName.Size = new System.Drawing.Size(65, 24);
            this.labelCardName.TabIndex = 12;
            this.labelCardName.Text = "Name";
            this.labelCardName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCardDescription
            // 
            this.labelCardDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCardDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardDescription.Location = new System.Drawing.Point(702, 333);
            this.labelCardDescription.Name = "labelCardDescription";
            this.labelCardDescription.ReadOnly = true;
            this.labelCardDescription.Size = new System.Drawing.Size(341, 245);
            this.labelCardDescription.TabIndex = 13;
            this.labelCardDescription.Text = "This is an example card text.";
            // 
            // labelCardAtk
            // 
            this.labelCardAtk.AutoSize = true;
            this.labelCardAtk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardAtk.Location = new System.Drawing.Point(896, 80);
            this.labelCardAtk.Name = "labelCardAtk";
            this.labelCardAtk.Size = new System.Drawing.Size(56, 24);
            this.labelCardAtk.TabIndex = 14;
            this.labelCardAtk.Text = "ATK:";
            // 
            // labelCardDef
            // 
            this.labelCardDef.AutoSize = true;
            this.labelCardDef.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardDef.Location = new System.Drawing.Point(896, 103);
            this.labelCardDef.Name = "labelCardDef";
            this.labelCardDef.Size = new System.Drawing.Size(57, 24);
            this.labelCardDef.TabIndex = 15;
            this.labelCardDef.Text = "DEF:";
            // 
            // labelCardType
            // 
            this.labelCardType.AutoSize = true;
            this.labelCardType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardType.Location = new System.Drawing.Point(896, 125);
            this.labelCardType.Name = "labelCardType";
            this.labelCardType.Size = new System.Drawing.Size(63, 24);
            this.labelCardType.TabIndex = 16;
            this.labelCardType.Text = "Type:";
            // 
            // labelCardAttr
            // 
            this.labelCardAttr.AutoSize = true;
            this.labelCardAttr.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardAttr.Location = new System.Drawing.Point(896, 148);
            this.labelCardAttr.Name = "labelCardAttr";
            this.labelCardAttr.Size = new System.Drawing.Size(93, 24);
            this.labelCardAttr.TabIndex = 17;
            this.labelCardAttr.Text = "Attribute:";
            // 
            // labelCardRace
            // 
            this.labelCardRace.AutoSize = true;
            this.labelCardRace.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardRace.Location = new System.Drawing.Point(896, 170);
            this.labelCardRace.Name = "labelCardRace";
            this.labelCardRace.Size = new System.Drawing.Size(64, 24);
            this.labelCardRace.TabIndex = 18;
            this.labelCardRace.Text = "Race:";
            // 
            // labelCardLevel
            // 
            this.labelCardLevel.AutoSize = true;
            this.labelCardLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardLevel.Location = new System.Drawing.Point(896, 58);
            this.labelCardLevel.Name = "labelCardLevel";
            this.labelCardLevel.Size = new System.Drawing.Size(66, 24);
            this.labelCardLevel.TabIndex = 19;
            this.labelCardLevel.Text = "Level:";
            // 
            // panelDraftCards
            // 
            this.panelDraftCards.BackColor = System.Drawing.Color.DarkGray;
            this.panelDraftCards.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDraftCards.Location = new System.Drawing.Point(132, 18);
            this.panelDraftCards.Name = "panelDraftCards";
            this.panelDraftCards.Size = new System.Drawing.Size(278, 335);
            this.panelDraftCards.TabIndex = 20;
            this.panelDraftCards.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDraftCards_Paint);
            this.panelDraftCards.MouseEnter += new System.EventHandler(this.panelDraftCards_MouseEnter);
            this.panelDraftCards.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelDraftCards_MouseMove);
            this.panelDraftCards.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDraftCards_MouseClick);
            // 
            // panelMainDeck
            // 
            this.panelMainDeck.BackColor = System.Drawing.Color.DarkGray;
            this.panelMainDeck.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMainDeck.Location = new System.Drawing.Point(424, 68);
            this.panelMainDeck.Name = "panelMainDeck";
            this.panelMainDeck.Size = new System.Drawing.Size(240, 192);
            this.panelMainDeck.TabIndex = 21;
            this.panelMainDeck.Enter += new System.EventHandler(this.panelMainDeck_Enter);
            this.panelMainDeck.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelMainDeck_MouseClick);
            this.panelMainDeck.MouseEnter += new System.EventHandler(this.panelMainDeck_MouseEnter);
            this.panelMainDeck.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelDraftCards_MouseMove);
            // 
            // panelExtraDeck
            // 
            this.panelExtraDeck.BackColor = System.Drawing.Color.DarkGray;
            this.panelExtraDeck.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelExtraDeck.Location = new System.Drawing.Point(424, 305);
            this.panelExtraDeck.Name = "panelExtraDeck";
            this.panelExtraDeck.Size = new System.Drawing.Size(240, 121);
            this.panelExtraDeck.TabIndex = 22;
            this.panelExtraDeck.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelMainDeck_MouseClick);
            this.panelExtraDeck.MouseEnter += new System.EventHandler(this.panelExtraDeck_MouseEnter);
            this.panelExtraDeck.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelDraftCards_MouseMove);
            // 
            // panelSideDeck
            // 
            this.panelSideDeck.BackColor = System.Drawing.Color.DarkGray;
            this.panelSideDeck.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSideDeck.Location = new System.Drawing.Point(424, 457);
            this.panelSideDeck.Name = "panelSideDeck";
            this.panelSideDeck.Size = new System.Drawing.Size(240, 121);
            this.panelSideDeck.TabIndex = 23;
            this.panelSideDeck.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelSideDeck_MouseClick);
            this.panelSideDeck.MouseEnter += new System.EventHandler(this.panelExtraDeck_MouseEnter);
            this.panelSideDeck.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelDraftCards_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(420, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 24);
            this.label1.TabIndex = 24;
            this.label1.Text = "Main Deck";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(420, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 24);
            this.label2.TabIndex = 25;
            this.label2.Text = "Extra Deck";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(426, 430);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 24);
            this.label3.TabIndex = 26;
            this.label3.Text = "Side Deck";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Server IP";
            // 
            // labelNumMTS
            // 
            this.labelNumMTS.AutoSize = true;
            this.labelNumMTS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumMTS.Location = new System.Drawing.Point(421, 18);
            this.labelNumMTS.Name = "labelNumMTS";
            this.labelNumMTS.Size = new System.Drawing.Size(0, 16);
            this.labelNumMTS.TabIndex = 28;
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.BackColor = System.Drawing.SystemColors.ControlLight;
            this.labelPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPath.Location = new System.Drawing.Point(13, 366);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(80, 15);
            this.labelPath.TabIndex = 29;
            this.labelPath.Text = "YGOPROPath:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(424, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 24);
            this.button2.TabIndex = 30;
            this.button2.Text = "Save Deck";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 594);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.labelNumMTS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelSideDeck);
            this.Controls.Add(this.panelExtraDeck);
            this.Controls.Add(this.panelMainDeck);
            this.Controls.Add(this.panelDraftCards);
            this.Controls.Add(this.labelCardLevel);
            this.Controls.Add(this.labelCardRace);
            this.Controls.Add(this.labelCardAttr);
            this.Controls.Add(this.labelCardType);
            this.Controls.Add(this.labelCardDef);
            this.Controls.Add(this.labelCardAtk);
            this.Controls.Add(this.labelCardDescription);
            this.Controls.Add(this.labelCardName);
            this.Controls.Add(this.cardShowPanel);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FindYGOPRODBBtn);
            this.Controls.Add(this.ConfigurePackBtn);
            this.Controls.Add(this.DebugConsole);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox DebugConsole;
        private System.Windows.Forms.Button ConfigurePackBtn;
        private System.Windows.Forms.Button FindYGOPRODBBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Panel cardShowPanel;
        private System.Windows.Forms.Label labelCardName;
        private System.Windows.Forms.RichTextBox labelCardDescription;
        private System.Windows.Forms.Label labelCardAtk;
        private System.Windows.Forms.Label labelCardDef;
        private System.Windows.Forms.Label labelCardType;
        private System.Windows.Forms.Label labelCardAttr;
        private System.Windows.Forms.Label labelCardRace;
        private System.Windows.Forms.Label labelCardLevel;
        private System.Windows.Forms.Panel panelDraftCards;
        private System.Windows.Forms.Panel panelMainDeck;
        private System.Windows.Forms.Panel panelExtraDeck;
        private System.Windows.Forms.Panel panelSideDeck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelNumMTS;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button button2;
    }
}

