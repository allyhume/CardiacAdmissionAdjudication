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
            cases = new List<AdjudicationCase>();

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
                StringBuilder sb = new StringBuilder();
                sb.Append("Id");
                sb.Append("\tAdjudicator1");
                sb.Append("\tSuspectedACS1");
                sb.Append("\tECG12Lead");
                sb.Append("\tECGNormalAbnormal");
                sb.Append("\tECGMyocardialIschaemia");
                sb.Append("\tECGSubsequentIschaemia");
                sb.Append("\tECGSTElevation");
                sb.Append("\tECGSTDepression");
                sb.Append("\tECGTWaveInversion");
                sb.Append("\tECGQRSAbnormalities");
                sb.Append("\tECGPathlogicalQWave");
                sb.Append("\tRhythum");
                sb.Append("\tMechanism");
                sb.Append("\tCulpritVessel");
                sb.Append("\tSmoking");
                sb.Append("\tInitialObs");
                sb.Append("\tOxygenSat");
                sb.Append("\tOxygenTherapy");
                sb.Append("\tRespiratoryRate");
                sb.Append("\tSystolicBP");
                sb.Append("\tDiastolicBP");
                sb.Append("\tHeartRate");
                sb.Append("\tTemperature");
                sb.Append("\tAlert");
                sb.Append("\tKillipClass");
                sb.Append("\tCardiacArrest");
                sb.Append("\tACSTreatmentInED");
                sb.Append("\tInsufficientInfo");
                sb.Append("\tSpontaneous");
                sb.Append("\tProcedural");
                sb.Append("\tSecondary");
                sb.Append("\tSymptomsOfIschaemia");
                sb.Append("\tSignsOfIschaemia");
                sb.Append("\tSupplyDemandImbalance");
                sb.Append("\tPrimaryMechanism");
                sb.Append("\tSuspectedCAD");
                sb.Append("\tCardiac");
                sb.Append("\tSystemic");

                lines.Add(sb.ToString());

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
                StringBuilder sb = new StringBuilder();
                sb.Append("Id");
                sb.Append("\tAdjudicator2");
                sb.Append("\tInsufficientInfo2");
                sb.Append("\tSpontaneous2");
                sb.Append("\tProcedural2");
                sb.Append("\tSecondary2");
                sb.Append("\tSymptomsOfIschaemia2");
                sb.Append("\tSignsOfIschaemia2");
                sb.Append("\tSupplyDemandImbalance2");
                sb.Append("\tPrimaryMechanism2");
                sb.Append("\tSuspectedCAD2");
                sb.Append("\tCardiac2");
                sb.Append("\tSystemic2");
                lines.Add(sb.ToString());

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
