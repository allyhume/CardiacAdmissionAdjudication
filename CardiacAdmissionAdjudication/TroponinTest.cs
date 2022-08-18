using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardiacAdmissionAdjudication
{
    public class TroponinTest
    {
        public int TimeFromPresentation;
        public String Result;
        public String InvalidResultFlag;

        public TroponinTest(String row)
        {
            string[] columns = row.Split('\t');

            TimeFromPresentation = Convert.ToInt32(columns[1]);
            Result = columns[2];
            InvalidResultFlag = columns[3];
        }
    }
}
