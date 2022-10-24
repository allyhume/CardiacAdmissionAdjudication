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
        string[] options;
        bool editable;
        string? value;

        public ComboBoxDataEntry(string name, Label label, ComboBox comboBox, string[] options)
        {
            this.label = label;
            this.comboBox = comboBox;
            this.name = name;
            this.options = options;

            this.comboBox.DisplayMember = "Text";
            this.comboBox.ValueMember = "ID";
            this.editable = true;
            this.value = "";

            this.comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Stops typing in the combo box

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
            this.comboBox.SelectedValue = "";
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

        void IAdjudicationDataEntry.SetEditable(bool editable)
        {
            // Do nothing if editable and was already editable - we need to do work for non-editable stuff
            // even if we were previously non-editable as the value may have changed.
            if (editable && this.editable) return;

            if (editable)
            {
                ComboItem[] values = new ComboItem[options.Length + 1];
                for (int i = 0; i < options.Length; i++)
                {
                    values[i] = new ComboItem { ID = options[i], Text = options[i] };
                }
                values[options.Length] = new ComboItem { ID = "", Text = "" };
                this.comboBox.DataSource = values;
                this.comboBox.SelectedValue = this.value;
            }
            else
            {
                ComboItem[] values = new ComboItem[1];
                values[0] = new ComboItem { ID = this.value, Text = this.value };
                this.comboBox.DataSource = values;
            }
            this.editable = editable;
        }

        void IAdjudicationDataEntry.SetEmpty()
        {
            this.comboBox.SelectedValue = "";
            this.value = "";
        }

        void IAdjudicationDataEntry.SetValue(string? value)
        {
            this.value = (value == null) ? "" : value;
            this.comboBox.SelectedValue = this.value;
        }

        void IAdjudicationDataEntry.Show()
        {
            this.label.Show();
            this.comboBox.Show();
            this.isVisible = true;
        }

        void IAdjudicationDataEntry.SetValid()
        {
            label.ForeColor = normalLabelForeColor;
            comboBox.BackColor = normalComboBoxBackColor;
        }
    }
}
