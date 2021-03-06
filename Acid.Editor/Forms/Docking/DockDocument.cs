﻿using Acid.Forms.Config;
using Acid.Forms.Controls;
using Acid.Forms.Docking;
using Acid.Forms.Forms;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Acid.Editor.Forms.Docking
{
    public partial class DockDocument : DarkDocument
    {
        #region Constructor Region

        public DockDocument()
        {
            InitializeComponent();

            // Workaround to stop the textbox from highlight all text.
            txtDocument.SelectionStart = txtDocument.Text.Length;

	        // Build dummy dropdown data
	        cmbOptions.Items.Add(new DarkDropdownItem("25%"));
	        cmbOptions.Items.Add(new DarkDropdownItem("50%"));
			cmbOptions.Items.Add(new DarkDropdownItem("80%"));
			cmbOptions.Items.Add(new DarkDropdownItem("90%"));
			cmbOptions.Items.Add(new DarkDropdownItem("100%"));
	        cmbOptions.Items.Add(new DarkDropdownItem("110%"));
	        cmbOptions.Items.Add(new DarkDropdownItem("120%"));
			cmbOptions.Items.Add(new DarkDropdownItem("200%"));
	        cmbOptions.Items.Add(new DarkDropdownItem("300%"));
	        cmbOptions.Items.Add(new DarkDropdownItem("400%"));
			
			cmbOptions.SelectedItemChanged += delegate
			{
				var newSize = 10.0f * (float.Parse(cmbOptions.SelectedItem.Text.TrimEnd('%'),
					              CultureInfo.InvariantCulture.NumberFormat) / 100.0f);
				txtDocument.Font = new System.Drawing.Font("Segoe UI", newSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			};
	        cmbOptions.SelectedItem = cmbOptions.Items[4];
		}

        public DockDocument(string text, Image icon)
            : this()
        {
            DockText = text;
            Icon = icon;
        }

        #endregion

        #region Event Handler Region

        public override void Close()
        {
            var result = DarkMessageBox.Show(this, @"You will lose any unsaved changes. Continue?", @"Close document", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            base.Close();
        }

        #endregion
    }
}
