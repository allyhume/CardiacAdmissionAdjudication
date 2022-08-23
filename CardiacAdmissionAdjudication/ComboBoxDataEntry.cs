using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardiacAdmissionAdjudication
{
    public class ComboBoxDataEntry : IAdjudicationDataEntry
    {
        private Label label;
        private ComboBox comboBox;
        string name;
        private Color normalLabelForeColor;
        private Color normalComboBoxBackColor;
        private bool isVisible;

        public ComboBoxDataEntry(string name, Label label, ComboBox comboBox, string[] options)
        {
            this.label = label;
            this.comboBox = comboBox;
            this.name = name;

            this.comboBox.DisplayMember = "Text";
            this.comboBox.ValueMember = "ID";
            //cb.DropDownStyle = ComboBoxStyle.DropDownList; // Stops typing in the combo box
            //cb.FlatStyle = FlatStyle.Flat;                 // Stops it being grey background

            ComboItem[] values = new ComboItem[options.Length+1];

            for (int i =0; i < options.Length; i++) 
            {
                values[i] = new ComboItem { ID = options[i], Text = options[i] };
            }
            values[options.Length] = new ComboItem { ID = "", Text = "" };
            this.comboBox.DataSource = values;

            normalComboBoxBackColor = comboBox.BackColor;
            normalLabelForeColor = label.ForeColor;
            isVisible = true;   
        }

        string IAdjudicationDataEntry.GetValue()
        {
            return this.comboBox.Text;
        }

        void IAdjudicationDataEntry.Hide()
        {
            this.label.Hide();
            this.comboBox.Hide();
            this.isVisible = false;
        }

        bool IAdjudicationDataEntry.IsValid(out string report)
        {
            bool result;

            // If not visible then no need to validate it
            if (!this.isVisible)
            {
                result = true;
            }
            else
            {
                result = (comboBox.Text != "");
            }

            if (!result)
            {
                report = this.name + " : no valid selection made";
                label.ForeColor = Color.Red;
                comboBox.BackColor = Color.Red;
            }
            else
            {
                report = "";
                label.ForeColor = normalLabelForeColor;
                comboBox.BackColor = normalComboBoxBackColor;
            }

            return result;
        }

        void IAdjudicationDataEntry.SetEmpty()
        {
            this.comboBox.SelectedValue = "";
        }

        void IAdjudicationDataEntry.SetValue(string? value)
        {
            this.comboBox.SelectedValue = (value == null) ? "" : value;
        }

        void IAdjudicationDataEntry.Show()
        {
            this.label.Show();
            this.comboBox.Show();
            this.isVisible = true;
        }
    }
}
