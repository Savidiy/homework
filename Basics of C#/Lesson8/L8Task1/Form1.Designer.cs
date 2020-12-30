
namespace L8Task1
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
            this.tblQuestions = new System.Windows.Forms.TableLayoutPanel();
            this.panelForQuestions = new System.Windows.Forms.Panel();
            this.vsbQuestions = new System.Windows.Forms.VScrollBar();
            this.btnAddQuestion = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDeleteQuestion = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.questEditRow9 = new L8Task1.QuestEditRow();
            this.tblQuestions.SuspendLayout();
            this.panelForQuestions.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblQuestions
            // 
            this.tblQuestions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblQuestions.AutoSize = true;
            this.tblQuestions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblQuestions.ColumnCount = 1;
            this.tblQuestions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblQuestions.Controls.Add(this.questEditRow9, 0, 0);
            this.tblQuestions.Location = new System.Drawing.Point(0, 0);
            this.tblQuestions.Name = "tblQuestions";
            this.tblQuestions.RowCount = 1;
            this.tblQuestions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblQuestions.Size = new System.Drawing.Size(798, 37);
            this.tblQuestions.TabIndex = 1;
            this.tblQuestions.Resize += new System.EventHandler(this.tblQuestions_Resize);
            // 
            // panelForQuestions
            // 
            this.panelForQuestions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForQuestions.AutoScroll = true;
            this.panelForQuestions.AutoScrollMargin = new System.Drawing.Size(0, 15);
            this.panelForQuestions.Controls.Add(this.vsbQuestions);
            this.panelForQuestions.Controls.Add(this.tblQuestions);
            this.panelForQuestions.Location = new System.Drawing.Point(0, 27);
            this.panelForQuestions.Name = "panelForQuestions";
            this.panelForQuestions.Size = new System.Drawing.Size(856, 389);
            this.panelForQuestions.TabIndex = 2;
            // 
            // vsbQuestions
            // 
            this.vsbQuestions.Dock = System.Windows.Forms.DockStyle.Right;
            this.vsbQuestions.Enabled = false;
            this.vsbQuestions.Location = new System.Drawing.Point(839, 0);
            this.vsbQuestions.Name = "vsbQuestions";
            this.vsbQuestions.Size = new System.Drawing.Size(17, 389);
            this.vsbQuestions.TabIndex = 2;
            // 
            // btnAddQuestion
            // 
            this.btnAddQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddQuestion.Location = new System.Drawing.Point(12, 422);
            this.btnAddQuestion.Name = "btnAddQuestion";
            this.btnAddQuestion.Size = new System.Drawing.Size(90, 23);
            this.btnAddQuestion.TabIndex = 3;
            this.btnAddQuestion.Text = "Add question";
            this.btnAddQuestion.UseVisualStyleBackColor = true;
            this.btnAddQuestion.Click += new System.EventHandler(this.btnAddQuestion_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(856, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 448);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(856, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsStatusLabel
            // 
            this.tsStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsStatusLabel.Name = "tsStatusLabel";
            this.tsStatusLabel.Size = new System.Drawing.Size(463, 17);
            this.tsStatusLabel.Spring = true;
            this.tsStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // btnDeleteQuestion
            // 
            this.btnDeleteQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteQuestion.Location = new System.Drawing.Point(108, 422);
            this.btnDeleteQuestion.Name = "btnDeleteQuestion";
            this.btnDeleteQuestion.Size = new System.Drawing.Size(93, 23);
            this.btnDeleteQuestion.TabIndex = 4;
            this.btnDeleteQuestion.Text = "Delete #1";
            this.btnDeleteQuestion.UseVisualStyleBackColor = true;
            this.btnDeleteQuestion.Click += new System.EventHandler(this.btnDeleteQuestion_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(751, 422);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save as";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // questEditRow9
            // 
            this.questEditRow9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.questEditRow9.Location = new System.Drawing.Point(3, 3);
            this.questEditRow9.MinimumSize = new System.Drawing.Size(350, 26);
            this.questEditRow9.Name = "questEditRow9";
            this.questEditRow9.Number = 1;
            this.questEditRow9.Padding = new System.Windows.Forms.Padding(30, 1, 60, 1);
            this.questEditRow9.QuestionText = "";
            this.questEditRow9.Size = new System.Drawing.Size(792, 31);
            this.questEditRow9.TabIndex = 1;
            this.questEditRow9.TrueFalse = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 470);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDeleteQuestion);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnAddQuestion);
            this.Controls.Add(this.panelForQuestions);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(420, 287);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Questions Editor";
            this.tblQuestions.ResumeLayout(false);
            this.panelForQuestions.ResumeLayout(false);
            this.panelForQuestions.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tblQuestions;
        private System.Windows.Forms.Panel panelForQuestions;
        private System.Windows.Forms.Button btnAddQuestion;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
        private System.Windows.Forms.Button btnDeleteQuestion;
        private QuestEditRow questEditRow9;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.VScrollBar vsbQuestions;
    }
}

