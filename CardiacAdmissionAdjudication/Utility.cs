using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardiacAdmissionAdjudication
{
    internal class Utility
    {
        public static void SetComboBox( ComboBox cb, ComboItem[] values)
        {
            cb.DisplayMember = "Text";
            cb.ValueMember = "ID";
            //cb.DropDownStyle = ComboBoxStyle.DropDownList; // Stops typing in the combo box
            //cb.FlatStyle = FlatStyle.Flat;                 // Stops it being grey background
            cb.DataSource = values;
        }

        public static void SetComboBoxYesNoNull(ComboBox cb)
        {
            Utility.SetComboBox(cb, new ComboItem[] {
                new ComboItem{ ID = "Yes",  Text = "Yes" },
                new ComboItem{ ID = "No",   Text = "No" },
                new ComboItem { ID = "NULL", Text = "" }
            });
        }
    }
}
