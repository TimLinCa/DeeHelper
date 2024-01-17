namespace DeeHelper
{
    partial class AddBusinessTierPathForm
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
            this.ProjectNameTB = new System.Windows.Forms.TextBox();
            this.lable1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FolderPathTB = new System.Windows.Forms.TextBox();
            this.SelectFolder_button = new System.Windows.Forms.Button();
            this.Save_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProjectNameTB
            // 
            this.ProjectNameTB.Location = new System.Drawing.Point(156, 16);
            this.ProjectNameTB.Name = "ProjectNameTB";
            this.ProjectNameTB.Size = new System.Drawing.Size(293, 20);
            this.ProjectNameTB.TabIndex = 0;
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Location = new System.Drawing.Point(26, 19);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(71, 13);
            this.lable1.TabIndex = 1;
            this.lable1.Text = "Project Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "BusinessTier Folder Path";
            // 
            // FolderPathTB
            // 
            this.FolderPathTB.Location = new System.Drawing.Point(156, 50);
            this.FolderPathTB.Name = "FolderPathTB";
            this.FolderPathTB.Size = new System.Drawing.Size(293, 20);
            this.FolderPathTB.TabIndex = 3;
            // 
            // SelectFolder_button
            // 
            this.SelectFolder_button.Location = new System.Drawing.Point(455, 50);
            this.SelectFolder_button.Name = "SelectFolder_button";
            this.SelectFolder_button.Size = new System.Drawing.Size(38, 20);
            this.SelectFolder_button.TabIndex = 4;
            this.SelectFolder_button.Text = "...";
            this.SelectFolder_button.UseVisualStyleBackColor = true;
            this.SelectFolder_button.Click += new System.EventHandler(this.SelectFolder_button_Click);
            // 
            // Save_button
            // 
            this.Save_button.Location = new System.Drawing.Point(115, 100);
            this.Save_button.Name = "Save_button";
            this.Save_button.Size = new System.Drawing.Size(75, 25);
            this.Save_button.TabIndex = 5;
            this.Save_button.Text = "Save";
            this.Save_button.UseVisualStyleBackColor = true;
            this.Save_button.Click += new System.EventHandler(this.Save_button_Click);
            // 
            // Cancel_button
            // 
            this.Cancel_button.Location = new System.Drawing.Point(335, 100);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 25);
            this.Cancel_button.TabIndex = 6;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            this.Cancel_button.Click += new System.EventHandler(this.Cancel_button_Click);
            // 
            // AddBusinessTierPathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 144);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.Save_button);
            this.Controls.Add(this.SelectFolder_button);
            this.Controls.Add(this.FolderPathTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lable1);
            this.Controls.Add(this.ProjectNameTB);
            this.Name = "AddBusinessTierPathForm";
            this.Text = "AddBusinessTierPathForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ProjectNameTB;
        private System.Windows.Forms.Label lable1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FolderPathTB;
        private System.Windows.Forms.Button SelectFolder_button;
        private System.Windows.Forms.Button Save_button;
        private System.Windows.Forms.Button Cancel_button;
    }
}