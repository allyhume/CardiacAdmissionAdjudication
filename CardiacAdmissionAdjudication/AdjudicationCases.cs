using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardiacAdmissionAdjudication
{
    public class AdjudicationCases
    {
        private static AdjudicationCases? instance = null;
        private static readonly object padlock = new object();

        private string? baseFilename;

        public static AdjudicationCases Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AdjudicationCases();
                    }
                    return instance;
                }
            }
        }


        public List<AdjudicationCase> cases;
        public Boolean IsFirstAdjudicator;

        private AdjudicationCases()
        {
            cases = new List<AdjudicationCase>();
        }

        public void ReadCasesFromFile(String fileName)
        {
            this.baseFilename = fileName.Replace("_0.txt", "");

            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines.Skip(1))  // Skip the header
            {
                cases.Add(new AdjudicationCase(line));
            }

            ReadTroponinData();
            ReadEmergencyDepartmentNotes();
            ReadDischargeNotes();

            // Read adjudication 1 if it exists
            string adj1FileName = this.baseFilename + "_adj1.txt";
            if (File.Exists(adj1FileName))
            {
                lines = File.ReadAllLines(adj1FileName);

                foreach (string line in lines.Skip(1))  // Skip the header
                {
                    string id = line.Split('\t')[0];

                    foreach (AdjudicationCase c in cases)
                    {
                        if (c.Id == id) c.AddFirstAdjudication(line);
                    }
                }

                // If any cases don't have a first adjudication then there is still 
                // work to be done by the first adjudicator.
                IsFirstAdjudicator = false;
                foreach (AdjudicationCase c in cases)
                {
                    if (!c.Adjudication1Complete)
                    {
                        IsFirstAdjudicator = true;
                    }
                }

                // If second adjudicator - try to load any second adjudicator data
                if (!IsFirstAdjudicator)
                {
                    string adj2FileName = this.baseFilename + "_adj2.txt";
                    if (File.Exists(adj2FileName))
                    {
                        lines = File.ReadAllLines(adj2FileName);

                        foreach (string line in lines.Skip(1))  // Skip the header
                        {
                            string id = line.Split('\t')[0];

                            foreach (AdjudicationCase c in cases)
                            {
                                if (c.Id == id) c.AddSecondAdjudication(line);
                            }
                        }
                    }
                }
            }
            else
            {
                IsFirstAdjudicator = true;
            }
        }

        public void Save()
        {
            List<string> lines = new List<string>();

            if (IsFirstAdjudicator)
            {
                lines.Add("Id\tAdjudicator\tDiastolicBP\tSpontaneous");
                foreach (AdjudicationCase c in cases)
                {
                    if (c.Adjudication1Complete)
                    {
                        lines.Add(c.GetAnnotation1TSV());
                    }
                }

                File.WriteAllLinesAsync(this.baseFilename + "_adj1.txt", lines);
            }
            else
            {
                lines.Add("Id\tAdjudicator\tSpontaneous");
                foreach (AdjudicationCase c in cases)
                {
                    if (c.Adjudication2Complete)
                    {
                        lines.Add(c.GetAnnotation2TSV());
                    }
                }

                File.WriteAllLinesAsync(this.baseFilename + "_adj2.txt", lines);
            }
        }


        public int FirstAdjudicatorCompletedCount()
        {
            int result = 0;

            foreach (AdjudicationCase c in cases)
            {
                if (c.Adjudication1Complete) result++;
            }

            return result;
        }

        public int SecondAdjudicatorCompletedCount()
        {
            int result = 0;

            foreach (AdjudicationCase c in cases)
            {
                if (c.Adjudication2Complete) result++;
            }

            return result;
        }

        public int FirstNonAdjudicatedCase()
        {
            for (int i = 0; i < cases.Count; i++)
            {
                if (IsFirstAdjudicator)
                {
                    if (!cases[i].Adjudication1Complete) return i;
                }
                else
                {
                    if (!cases[i].Adjudication2Complete) return i;
                }
            }

            return cases.Count - 1;
        }

        private void ReadTroponinData()
        {
            string troponinDataFileName = baseFilename + "_troponin.txt";

            string[] lines = File.ReadAllLines(troponinDataFileName);

            foreach (string line in lines.Skip(1))  // Skip the header
            {
                string id = line.Split('\t')[0];

                foreach (AdjudicationCase c in cases)
                {
                    if (c.Id == id) c.AddTroponinTest(new TroponinTest(line));
                }
            }
        }

        private void ReadEmergencyDepartmentNotes()
        {
            string filename = baseFilename + "_ed_notes.txt";

            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines.Skip(1))  // Skip the header
            {
                string id = line.Split('\t')[0];

                foreach (AdjudicationCase c in cases)
                {
                    if (c.Id == id) c.AddEmergencyDepartmentNote(line);
                }
            }
        }

        private void ReadDischargeNotes()
        {
            string filename = baseFilename + "_discharge_notes.txt";

            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines.Skip(1))  // Skip the header
            {
                string id = line.Split('\t')[0];

                foreach (AdjudicationCase c in cases)
                {
                    if (c.Id == id) c.AddDischargeNote(line);
                }
            }
        }

    }
}
