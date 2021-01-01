
namespace L8Task3
{
    partial class CSVtoXMLconverterForm
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblLoad = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblSave = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(80, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(123, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Загрузить из CSV";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblLoad
            // 
            this.lblLoad.Location = new System.Drawing.Point(25, 36);
            this.lblLoad.Name = "lblLoad";
            this.lblLoad.Size = new System.Drawing.Size(238, 46);
            this.lblLoad.TabIndex = 1;
            this.lblLoad.Text = "Загружено 5 записей из файла xxx.csv";
            this.lblLoad.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(80, 90);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(123, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить в XML";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblSave
            // 
            this.lblSave.Location = new System.Drawing.Point(12, 116);
            this.lblSave.Name = "lblSave";
            this.lblSave.Size = new System.Drawing.Size(272, 39);
            this.lblSave.TabIndex = 1;
            this.lblSave.Text = "Сохранено 5 записей в файл  111.xml";
            this.lblSave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(209, 12);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(26, 23);
            this.btnHelp.TabIndex = 1;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // CSVtoXMLconverterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 157);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblSave);
            this.Controls.Add(this.lblLoad);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CSVtoXMLconverterForm";
            this.Text = "Конвертер данных студентов из CSV в XML";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSave;
        private System.Windows.Forms.Button btnHelp;
    }
}

