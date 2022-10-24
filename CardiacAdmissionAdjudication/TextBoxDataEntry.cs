namespace CardiacAdmissionAdjudication
{
    public class TextBoxDataEntry : IAdjudicationDataEntry
    {
        private Label label;
        private TextBox textBox;
        string name;
        private Color normalLabelForeColor;
        private Color normalTextBoxBackColor;
        private bool isVisible;
        private float min;
        private float max;

        public TextBoxDataEntry(string name, Label label, TextBox textBox, float min, float max)
        {
            this.name = name;
            this.label = label;
            this.textBox = textBox;
            this.isVisible = true;
            this.min = min;
            this.max = max;

            normalTextBoxBackColor = textBox.BackColor;
            normalLabelForeColor = label.ForeColor;
        }

        string IAdjudicationDataEntry.GetValue()
        {
            return textBox.Text.ToUpper().Trim();
        }

        void IAdjudicationDataEntry.Hide()
        {
            textBox.Hide();
            label.Hide();
            isVisible = false;
            textBox.Text = "";
        }

        bool IAdjudicationDataEntry.IsValid(out string report)
        {
            bool result;
            report = "";

            string value = textBox.Text.ToUpper().Trim();

            // If not visible then no need to validate it
            if (!isVisible)
            {
                result = true;
            }
            else
            {

                // M is valid entry for a missing value 
                result = (value == "M");

                // If not M then try to see if a numeric value
                if (!result)
                {
                    float f;
                    result = float.TryParse(value, out f);

                    if (!result)
                    {
                        report = name + " : not a valid number or 'M' : " + value;
                    }
                    else
                    {
                        result = (f >= min && f <= max);
                        if (!result) report = name + " : number out of range";
                    }
                }
            }

            if (!result)
            {
                label.ForeColor = Color.Red;
                textBox.BackColor = Color.Red;
            }
            else
            {
                label.ForeColor = normalLabelForeColor;
                textBox.BackColor = normalTextBoxBackColor;
            }

            return result;
        }

        void IAdjudicationDataEntry.SetEditable(bool editable)
        {
            textBox.ReadOnly = !editable;
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
            isVisible = true;
        }

        void IAdjudicationDataEntry.SetValid()
        {
            label.ForeColor = normalLabelForeColor;
            textBox.BackColor = normalTextBoxBackColor;
        }
    }
}
