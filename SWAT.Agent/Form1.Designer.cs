using System;

namespace SWAT.Agent
{
    partial class Agent
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
            this.buttonAssign = new System.Windows.Forms.Button();
            this.comboBoxTFSProject = new System.Windows.Forms.ComboBox();
            this.comboBoxTFSTestSuites = new System.Windows.Forms.ComboBox();
            this.TFSProjects = new System.Windows.Forms.Label();
            this.TFSTestPlan = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAssign
            // 
            this.buttonAssign.Location = new System.Drawing.Point(163, 116);
            this.buttonAssign.Name = "buttonAssign";
            this.buttonAssign.Size = new System.Drawing.Size(144, 23);
            this.buttonAssign.TabIndex = 0;
            this.buttonAssign.Text = "Assign";
            this.buttonAssign.UseVisualStyleBackColor = true;
            this.buttonAssign.Click += new System.EventHandler(this.AssignButton_Click);
            // 
            // comboBoxTFSProject
            // 
            this.comboBoxTFSProject.FormattingEnabled = true;
            this.comboBoxTFSProject.Items.AddRange(new object[] {
            "Test"});
            this.comboBoxTFSProject.Location = new System.Drawing.Point(163, 26);
            this.comboBoxTFSProject.Name = "comboBoxTFSProject";
            this.comboBoxTFSProject.Size = new System.Drawing.Size(320, 21);
            this.comboBoxTFSProject.TabIndex = 1;
            this.comboBoxTFSProject.SelectedIndexChanged += new System.EventHandler(this.comboBoxTFSProject_SelectedIndexChanged);
            // 
            // comboBoxTFSTestSuites
            // 
            this.comboBoxTFSTestSuites.FormattingEnabled = true;
            this.comboBoxTFSTestSuites.Location = new System.Drawing.Point(163, 78);
            this.comboBoxTFSTestSuites.Name = "comboBoxTFSTestSuites";
            this.comboBoxTFSTestSuites.Size = new System.Drawing.Size(320, 21);
            this.comboBoxTFSTestSuites.TabIndex = 2;
            this.comboBoxTFSTestSuites.SelectedIndexChanged += new System.EventHandler(this.comboBoxTFSTestSuites_SelectedIndexChanged);
            // 
            // TFSProjects
            // 
            this.TFSProjects.AutoSize = true;
            this.TFSProjects.Location = new System.Drawing.Point(39, 26);
            this.TFSProjects.Name = "TFSProjects";
            this.TFSProjects.Size = new System.Drawing.Size(65, 13);
            this.TFSProjects.TabIndex = 3;
            this.TFSProjects.Text = "TFSProjects";
            // 
            // TFSTestPlan
            // 
            this.TFSTestPlan.AutoSize = true;
            this.TFSTestPlan.Location = new System.Drawing.Point(39, 78);
            this.TFSTestPlan.Name = "TFSTestPlan";
            this.TFSTestPlan.Size = new System.Drawing.Size(69, 13);
            this.TFSTestPlan.TabIndex = 4;
            this.TFSTestPlan.Text = "TFSTestPlan";
            // 
            // Agent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 159);
            this.Controls.Add(this.TFSTestPlan);
            this.Controls.Add(this.TFSProjects);
            this.Controls.Add(this.comboBoxTFSTestSuites);
            this.Controls.Add(this.comboBoxTFSProject);
            this.Controls.Add(this.buttonAssign);
            this.Name = "Agent";
            this.Text = "Agent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAssign;
        private System.Windows.Forms.ComboBox comboBoxTFSProject;
        private System.Windows.Forms.ComboBox comboBoxTFSTestSuites;
        private System.Windows.Forms.Label TFSProjects;
        private System.Windows.Forms.Label TFSTestPlan;
    }
}

