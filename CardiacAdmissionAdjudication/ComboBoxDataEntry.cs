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
        }

        string IAdjudicationDataEntry.GetValue()
        {
            return this.comboBox.Text;
        }

        void IAdjudicationDataEntry.Hide()
        {
            this.label.Hide();
            this.comboBox.Hide();
        }

        bool IAdjudicationDataEntry.IsValid()
        {
            throw new NotImplementedException();
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
        }
    }
}
