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
        public string Haemoglobin;
        public string Creatinine;
        public string eGRF;

        public List<TroponinTest> TroponinTests;
        public List<string> EmergencyDepartmentNotes;
        public List<string> DischargeNotes;

        public string ECGTimeFromPresentation;
        public string ECGMUSEText;
        public string ECGPDF;

        public string TomcatDaysFromPresentation;
        public string TomcatLCx;
        public string TomcatRCA;
        public string TomcatLMS;
        public string TomcatLAD;
        public string TomcatText;


        // Adjudication 1 data
        public string? Adjudicator1;
        public Boolean Adjudication1Complete;

        public string? SuspectedACS1;
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
        public string? ClinicalDiagnosis;

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
        public string? ClinicalDiagnosis2;

        public AdjudicationCase(string input)
        {
            string[] columns = input.Split('\t');

            int index = 0;
            Id = columns[index++];
            Age = columns[index++];
            Sex = columns[index++];
            ArrivalDate = columns[index++];
            PrimarySymptom = columns[index++];
            SuspectedACS = columns[index++];
            TimeSinceOnset = columns[index++];
            Haemoglobin = columns[index++];
            Creatinine = columns[index++];
            eGRF = columns[index++];

            ECGTimeFromPresentation = columns[index++];
            ECGMUSEText = columns[index++].Replace("<<NL>>", "\n").Replace("<<TAB>>", "\t");
            ECGPDF = columns[index++];

            TomcatDaysFromPresentation = columns[index++];
            TomcatLCx = columns[index++];
            TomcatRCA = columns[index++];
            TomcatLMS = columns[index++];
            TomcatLAD = columns[index++];
            TomcatText = columns[index++].Replace("<<NL>>", "\n").Replace("<<TAB>>", "\t");

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

            string spacer = "";
            if (EmergencyDepartmentNotes.Count > 0)
            {
                spacer = "\n\n";
            }

            EmergencyDepartmentNotes.Add(
                spacer +
                columns[1] + " " +    // Date 
                columns[2] + " " +    // Time
                columns[3] + "\n\n" + // Note Type
                columns[4].Replace("<<NL>>", "\n").Replace("<<TAB>>","\t"));
        }

        public void AddDischargeNote(string line)
        {
            string[] columns = line.Split('\t');

            string spacer = "";
            if (DischargeNotes.Count > 0)
            {
                spacer = "\n\n";
            }

            DischargeNotes.Add(
                spacer + 
                columns[1] + " " +    // Date 
                columns[2] + " " +    // Time
                columns[3] + "\n\n" + // Note Type
                columns[4].Replace("<<NL>>", "\n").Replace("<<TAB>>", "\t"));
        }

        public void AddFirstAdjudication(string input)
        {
            string[] columns = input.Split('\t');

            Adjudication1Complete = true;

            // Id will be column 0
            int index = 1;
            Adjudicator1 = columns[index++];
            SuspectedACS1 = columns[index++];
            ECG12Lead = columns[index++];
            ECGNormalAbnormal = columns[index++];
            ECGMyocardialIschaemia = columns[index++];
            ECGSubsequentIschaemia = columns[index++];
            ECGSTElevation = columns[index++];
            ECGSTDepression = columns[index++];
            ECGTWaveInversion = columns[index++];
            ECGQRSAbnormalities = columns[index++];
            ECGPathlogicalQWave = columns[index++];
            Rhythum = columns[index++];
            Mechanism = columns[index++];
            CulpritVessel = columns[index++];
            Smoking = columns[index++];
            InitialObs = columns[index++];
            OxygenSat = columns[index++];
            OxygenTherapy = columns[index++];
            RespiratoryRate = columns[index++];
            SystolicBP = columns[index++];
            DiastolicBP = columns[index++];
            HeartRate = columns[index++];
            Temperature = columns[index++];
            Alert = columns[index++];
            KillipClass = columns[index++];
            CardiacArrest = columns[index++];
            ACSTreatmentInED = columns[index++];
            InsufficientInfo = columns[index++];
            Spontaneous = columns[index++];
            Procedural = columns[index++];
            Secondary = columns[index++];
            SymptomsOfIschaemia = columns[index++];
            SignsOfIschaemia = columns[index++];
            SupplyDemandImbalance = columns[index++];
            PrimaryMechanism = columns[index++];
            SuspectedCAD = columns[index++];
            Cardiac = columns[index++];
            Systemic = columns[index++];
            ClinicalDiagnosis = columns[index++];
        }

        public void AddSecondAdjudication(string input)
        {
            string[] columns = input.Split('\t');

            Adjudication2Complete = true;

            // Id will be column 0
            int index = 1;
            Adjudicator2 = columns[index++];
            InsufficientInfo2 = columns[index++];
            Spontaneous2 = columns[index++];
            Procedural2 = columns[index++];
            Secondary2 = columns[index++];
            SymptomsOfIschaemia2 = columns[index++];
            SignsOfIschaemia2 = columns[index++];
            SupplyDemandImbalance2 = columns[index++];
            PrimaryMechanism2 = columns[index++];
            SuspectedCAD2 = columns[index++];
            Cardiac2 = columns[index++];
            Systemic2 = columns[index++];
            ClinicalDiagnosis2 = columns[index++];
        }

        public string GetAnnotation1TSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Id);
            sb.Append("\t" + Adjudicator1);
            sb.Append("\t" + SuspectedACS1);   
            sb.Append("\t" + ECG12Lead);   
            sb.Append("\t" + ECGNormalAbnormal);   
            sb.Append("\t" + ECGMyocardialIschaemia);   
            sb.Append("\t" + ECGSubsequentIschaemia);   
            sb.Append("\t" + ECGSTElevation);   
            sb.Append("\t" + ECGSTDepression);   
            sb.Append("\t" + ECGTWaveInversion);   
            sb.Append("\t" + ECGQRSAbnormalities);
            sb.Append("\t" + ECGPathlogicalQWave);
            sb.Append("\t" + Rhythum);
            sb.Append("\t" + Mechanism);
            sb.Append("\t" + CulpritVessel);
            sb.Append("\t" + Smoking);
            sb.Append("\t" + InitialObs);
            sb.Append("\t" + OxygenSat);
            sb.Append("\t" + OxygenTherapy);
            sb.Append("\t" + RespiratoryRate);
            sb.Append("\t" + SystolicBP);
            sb.Append("\t" + DiastolicBP);
            sb.Append("\t" + HeartRate);
            sb.Append("\t" + Temperature);
            sb.Append("\t" + Alert);
            sb.Append("\t" + KillipClass);
            sb.Append("\t" + CardiacArrest);
            sb.Append("\t" + ACSTreatmentInED);
            sb.Append("\t" + InsufficientInfo);
            sb.Append("\t" + Spontaneous);
            sb.Append("\t" + Procedural);
            sb.Append("\t" + Secondary);
            sb.Append("\t" + SymptomsOfIschaemia);
            sb.Append("\t" + SignsOfIschaemia);
            sb.Append("\t" + SupplyDemandImbalance);
            sb.Append("\t" + PrimaryMechanism);
            sb.Append("\t" + SuspectedCAD);
            sb.Append("\t" + Cardiac);
            sb.Append("\t" + Systemic);
            sb.Append("\t" + ClinicalDiagnosis);

            return sb.ToString();
        }

        public string GetAnnotation2TSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Id);
            sb.Append("\t" + Adjudicator2);
            sb.Append("\t" + InsufficientInfo2);
            sb.Append("\t" + Spontaneous2);
            sb.Append("\t" + Procedural2);
            sb.Append("\t" + Secondary2);
            sb.Append("\t" + SymptomsOfIschaemia2);
            sb.Append("\t" + SignsOfIschaemia2);
            sb.Append("\t" + SupplyDemandImbalance2);
            sb.Append("\t" + PrimaryMechanism2);
            sb.Append("\t" + SuspectedCAD2);
            sb.Append("\t" + Cardiac2);
            sb.Append("\t" + Systemic2);
            sb.Append("\t" + ClinicalDiagnosis2);

            return sb.ToString();
        }

    }
}
