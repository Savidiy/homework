
namespace L8Task1
{
    partial class QuestEditRow
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
            this.tbQuestion = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.cbTrueFalse = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbQuestion
            // 
            this.tbQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbQuestion.Location = new System.Drawing.Point(30, 1);
            this.tbQuestion.Margin = new System.Windows.Forms.Padding(0);
            this.tbQuestion.Multiline = true;
            this.tbQuestion.Name = "tbQuestion";
            this.tbQuestion.Size = new System.Drawing.Size(541, 81);
            this.tbQuestion.TabIndex = 0;
            this.tbQuestion.Text = "Тут будет записан вопрос. Если несколько строк, то поле должно растягиваться вниз" +
    ". \r\n";
            this.tbQuestion.TextChanged += new System.EventHandler(this.tbQuestiontText_TextChanged);
            this.tbQuestion.Enter += new System.EventHandler(this.tbQuestion_Enter);
            // 
            // lblNumber
            // 
            this.lblNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumber.Location = new System.Drawing.Point(0, 1);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(30, 81);
            this.lblNumber.TabIndex = 1;
            this.lblNumber.Text = "999";
            this.lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbTrueFalse
            // 
            this.cbTrueFalse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTrueFalse.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbTrueFalse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbTrueFalse.Location = new System.Drawing.Point(571, 0);
            this.cbTrueFalse.Name = "cbTrueFalse";
            this.cbTrueFalse.Size = new System.Drawing.Size(60, 83);
            this.cbTrueFalse.TabIndex = 1;
            this.cbTrueFalse.Text = "FALSE";
            this.cbTrueFalse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbTrueFalse.UseVisualStyleBackColor = true;
            this.cbTrueFalse.CheckedChanged += new System.EventHandler(this.cbTrueFalse_CheckedChanged);
            this.cbTrueFalse.Enter += new System.EventHandler(this.cbTrueFalse_Enter);
            // 
            // QuestEditRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbTrueFalse);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.tbQuestion);
            this.MinimumSize = new System.Drawing.Size(350, 26);
            this.Name = "QuestEditRow";
            this.Padding = new System.Windows.Forms.Padding(30, 1, 60, 1);
            this.Size = new System.Drawing.Size(631, 83);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.CheckBox cbTrueFalse;
        public System.Windows.Forms.TextBox tbQuestion;
    }
}
