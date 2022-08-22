using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardiacAdmissionAdjudication
{
    public class TextBoxDataEntry : IAdjudicationDataEntry
    {
        private Label label;
        private TextBox textBox;
        string name;

        public TextBoxDataEntry(string name, Label label, TextBox textBox)
        {
            this.name = name;
            this.label = label;
            this.textBox = textBox;
        }

        string IAdjudicationDataEntry.GetValue()
        {
            return textBox.Text;
        }

        void IAdjudicationDataEntry.Hide()
        {
            textBox.Hide();
            label.Hide();
        }

        bool IAdjudicationDataEntry.IsValid()
        {
            throw new NotImplementedException();
        }

        void IAdjudicationDataEntry.SetEmpty()
        {
            textBox.Text = "";
        }

        void IAdjudicationDataEntry.SetValue(string? value)
        {
            textBox.Text = (value == null) ? "" : value ;
        }

        void IAdjudicationDataEntry.Show()
        {
            textBox.Show();
            label.Show();
        }
    }
}
