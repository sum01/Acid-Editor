﻿using Acid.UI.Config;
using Acid.UI.Controls;
using Acid.UI.Docking;

namespace Acid.Editor.Forms.Docking
{
    partial class DockAssets
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
			this.treeProject = new Acid.UI.Controls.DarkTreeView();
			this.SuspendLayout();
			// 
			// treeProject
			// 
			this.treeProject.AllowDrop = true;
			this.treeProject.AllowMoveNodes = true;
			this.treeProject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeProject.Location = new System.Drawing.Point(0, 25);
			this.treeProject.MaxDragChange = 20;
			this.treeProject.MultiSelect = true;
			this.treeProject.Name = "treeProject";
			this.treeProject.ShowIcons = true;
			this.treeProject.Size = new System.Drawing.Size(200, 175);
			this.treeProject.TabIndex = 0;
			this.treeProject.Text = "darkTreeView1";
			// 
			// DockAssets
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.treeProject);
			this.DefaultDockArea = Acid.UI.Docking.DarkDockArea.Bottom;
			this.DockText = "Assets Explorer";
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = global::Acid.Editor.Icons.application_16x;
			this.Name = "DockAssets";
			this.SerializationKey = "DockAssets";
			this.Size = new System.Drawing.Size(200, 200);
			this.ResumeLayout(false);

        }

        #endregion

        private DarkTreeView treeProject;
    }
}
