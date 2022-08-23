using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardiacAdmissionAdjudication
{
    public interface IAdjudicationDataEntry
    {
        bool IsValid(out string report);

        void Show();
        void Hide();

        String GetValue();

        void SetValue(String? value);

        void SetEmpty();

    }
}
