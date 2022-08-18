using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardiacAdmissionAdjudication
{
    public class AdjudicationCase
    {
        // Variables to  display but cannot be altered
        public string Id;
        public string ArrivalDate;
        public string PrimarySymptom;
        public string SuspectedACS;
        public string TimeSinceOnset;
        public string Age;
        public string Sex;

        public List<TroponinTest> TroponinTests;
        public List<string> EmergencyDepartmentNotes;
        public List<string> DischargeNotes;

        public string ECGTimeFromPresentation;
        public string ECGMUSEText;

        public string TomcatCath;
        public string TomcatDaysFromPresentation;
        public string TomcatLCx;
        public string TomcatRCA;
        public string TomcatLMS;
        public string TomcatPCI;
        public string TomcatLAD;
        public string TomcatText;


        // Adjudication 1 data
        public string? Adjudicator1;
        public Boolean Adjudication1Complete;

        public string? ECG12Lead;
        public string? ECGNormalAbnormal;
        public string? ECGMyocardialIschaemia;
        public string? ECGSubsequentIschaemia;
        public string? ECGSTElevation;
        public string? ECGSTDepression;
        public string? ECGTWaveInversion;
        public string? ECGQRSAbnormalities;
        public string? ECGPathlogicalQWave;
        public string? Rhythum;

        public string? Mechanism;
        public string? CulpritVessel;

        public string? Smoking;

        public string? InitialObs;
        public string? OxygenSat;
        public string? OxygenTherapy;
        public string? RespiratoryRate;
        public string? SystolicBP;
        public string? DiastolicBP;
        public string? HeartRate;
        public string? Temperature;
        public string? Alert;
        public string? KillipClass;
        public string? CardiacArrest;
        public string? ACSTreatmentInED;

        public string? InsufficientInfo;
        public string? Spontaneous;
        public string? Procedural;
        public string? Secondary;
        public string? SymptomsOfIschaemia;
        public string? SignsOfIschaemia;
        public string? SupplyDemandImbalance;
        public string? PrimaryMechanism;
        public string? SuspectedCAD;
        public string? Cardiac;
        public string? Systemic;

        // Adjudication 2 data
        public string? Adjudicator2;
        public Boolean Adjudication2Complete;

        public string? InsufficientInfo2;
        public string? Spontaneous2;
        public string? Procedural2;
        public string? Secondary2;
        public string? SymptomsOfIschaemia2;
        public string? SignsOfIschaemia2;
        public string? SupplyDemandImbalance2;
        public string? PrimaryMechanism2;
        public string? SuspectedCAD2;
        public string? Cardiac2;
        public string? Systemic2;

        public AdjudicationCase(string input)
        {
            string[] columns = input.Split('\t');

            Id = columns[0];
            Age = columns[1];
            Sex = columns[2];
            ArrivalDate = columns[3];
            PrimarySymptom = columns[4];
            SuspectedACS = columns[5];
            TimeSinceOnset = columns[6];
            ECGTimeFromPresentation = columns[7];
            ECGMUSEText = columns[8];

            TroponinTests = new List<TroponinTest>();
            EmergencyDepartmentNotes = new List<string>();
            DischargeNotes = new List<string>();

            Adjudication1Complete = false;
            Adjudication2Complete = false;
        }

        public void AddTroponinTest(TroponinTest test)
        {
            TroponinTests.Add(test);
        }

        public void AddEmergencyDepartmentNote(string line)
        {
            string[] columns = line.Split('\t');
            EmergencyDepartmentNotes.Add(columns[1]);
        }

        public void AddDischargeNote(string line)
        {
            string[] columns = line.Split('\t');
            DischargeNotes.Add(columns[1]);
        }

        public void AddFirstAdjudication(string input)
        {
            string[] columns = input.Split('\t');

            Adjudication1Complete = true;

            // Id will be column 0
            Adjudicator1 = columns[1];
            DiastolicBP  = columns[2];
            Spontaneous  = columns[3];
        }

        public void AddSecondAdjudication(string input)
        {
            string[] columns = input.Split('\t');

            Adjudication2Complete = true;

            // Id will be column 0
            Adjudicator2 = columns[1];
            Spontaneous2 = columns[2];
        }

        public string GetAnnotation1TSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Id);
            sb.Append('\t');
            sb.Append(Adjudicator1);
            sb.Append('\t');



            sb.Append(DiastolicBP);
            sb.Append('\t');
            sb.Append(Spontaneous);

            return sb.ToString();
        }

        public string GetAnnotation2TSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Id);
            sb.Append('\t');
            sb.Append(Adjudicator2);
            sb.Append('\t');
            sb.Append(Spontaneous2);

            return sb.ToString();
        }
        
    }
}
