using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace CardiacAdmissionAdjudication
{
    public partial class Form1 : Form
    {
        private AdjudicationCases adjudicationCases;
        private int currentCase = 0;

        private OpenFileDialog openFileDialog;

        private IAdjudicationDataEntry de12LeadECG;
        private IAdjudicationDataEntry deECGNormal;
        private IAdjudicationDataEntry deMyocardialIschaemia;
        private IAdjudicationDataEntry deSubsequentIschaemia;
        private IAdjudicationDataEntry deSTElevation;
        private IAdjudicationDataEntry deSTDepression;
        private IAdjudicationDataEntry deTWaveInversion;
        private IAdjudicationDataEntry deQRSAbnormalities;
        private IAdjudicationDataEntry dePathologicalQWave;
        private IAdjudicationDataEntry deRhythm;
        private IAdjudicationDataEntry deMechanism;
        private IAdjudicationDataEntry deCulpritVessel;
        private IAdjudicationDataEntry deSmoking;
        private IAdjudicationDataEntry deInsufficientInfo;
        private IAdjudicationDataEntry deSpontaneous;
        private IAdjudicationDataEntry deProcedural;
        private IAdjudicationDataEntry deSecondary;
        private IAdjudicationDataEntry deSymptomsOfIschaemia;
        private IAdjudicationDataEntry deSignsOfIschaemia;
        private IAdjudicationDataEntry deSupplyDemandImbalance;
        private IAdjudicationDataEntry dePrimaryMechanism;
        private IAdjudicationDataEntry deSuspectedCAD;

        private IAdjudicationDataEntry deCardiac;
        private IAdjudicationDataEntry deSystemic;
        private IAdjudicationDataEntry deInitialObs;
        private IAdjudicationDataEntry deOxygenTherapy;
        private IAdjudicationDataEntry deAlert;
        private IAdjudicationDataEntry deKillipClass;
        private IAdjudicationDataEntry deCardiacArrest;
        private IAdjudicationDataEntry deACSTreatmentInED;
        private IAdjudicationDataEntry deSuspectedACS;

        private IAdjudicationDataEntry deOxygenSat;
        private IAdjudicationDataEntry deRespiratoryRate;
        private IAdjudicationDataEntry deSystolicBP;
        private IAdjudicationDataEntry deDiastolicBP;
        private IAdjudicationDataEntry deHeartRate;
        private IAdjudicationDataEntry deTemperature;

        private List<IAdjudicationDataEntry> adjudication1DataEntries = new List<IAdjudicationDataEntry>();
        private List<IAdjudicationDataEntry> adjudication2DataEntries = new List<IAdjudicationDataEntry>();

        public Form1(AdjudicationCases cases)
        {
            InitializeComponent();

            this.adjudicationCases = cases;

             openFileDialog = new OpenFileDialog()
             {
                 FileName = "Select an annotation cases file",
                 Filter = "Annotation cases (cases_*_0.txt)|cases_*_0.txt",
                 Title = "Open annotation cases file"
             };

            // ECG
            de12LeadECG = new ComboBoxDataEntry(
                "12 Lead ECG",
                label12LeadECG,
                comboBox12LeadECG,
                new string[] { "Yes", "No" });

            deECGNormal = new ComboBoxDataEntry(
                "ECG Normal",
                labelECGNormal,
                comboBoxECGNormal,
                new string[] { "Normal", "Abnormal" });

            deMyocardialIschaemia = new ComboBoxDataEntry(
                "Myocardial Ischaemia",
                label30,
                comboBoxMyocardialIschaemia,
                new string[] { "Yes", "No" });

            deSubsequentIschaemia = new ComboBoxDataEntry(
                "Subsequent Ischaemia",
                labelSubsequentIschaemia,
                comboBoxSubsequentIschaemia,
                new string[] { "Yes", "No" });

            deSTElevation = new ComboBoxDataEntry(
                "ST Elevation",
                labelSTElevation,
                comboBoxSTElevation,
                new string[] { "Yes", "No" });

            deSTDepression = new ComboBoxDataEntry(
                "ST Depression",
                labelSTDepression,
                comboBoxSTDepression,
                new string[] { "Yes", "No" });

            deTWaveInversion = new ComboBoxDataEntry(
                "T-Wave Inversion",
                labelTWaveInversion,
                comboBoxTWaveInversion,
                new string[] { "Yes", "No" });

            deQRSAbnormalities = new ComboBoxDataEntry(
                "QRS Abnormalities",
                labelQRSAbnormalities,
                comboBoxQRSAbnormalities,
                new string[] { "None", "LBBB (new)", "LBBB (old)", "RBBB (new)", "RBBB (old)", "LVH" });

            dePathologicalQWave = new ComboBoxDataEntry(
                "Pathological QWave",
                labelPathologicalQWave,
                comboBoxPathologicalQWave,
                new string[] { "Yes", "No" });

            deRhythm = new ComboBoxDataEntry(
                "Rhythm",
                labelRhythm,
                comboBoxRhythm,
                new string[] { "SR", "AF", "Flutter", "SVT", "VT", "VF", "Paced rhythm", "Advanced AV block", "Unknown" });

            // Angiogram Classification

            deMechanism = new ComboBoxDataEntry(
                "Mechanism",
                labelMechanism,
                comboBoxMechanism,
                new string[] {
                    "severe stenosis",
                    "plaque rupture with thrombosis",
                    "moderate unobstructive disease",
                    "plaque disease only",
                    "coronary dissection",
                    "in-stent restenosis",
                    "stent thrombosis",
                    "vasospasm",
                    "embolism",
                    "CTO related ischaemia",
                    "normal coronary arteries",
                    "bypass graft disease",
                    "unknown (for angiographic review)" });

            deCulpritVessel = new ComboBoxDataEntry(
                "Culprit Vessel",
                labelCulpritVessel,
                comboBoxCulpritVessel,
                new string[] {
                    "LMS",
                    "LAD",
                    "Dx",
                    "Cx",
                    "OM",
                    "RCA",
                    "PLV",
                    "PDA",
                    "intermediate",
                    "multiple",
                    "bypass graft",
                    "none" });

            // Smoking 
            deSmoking = new ComboBoxDataEntry(
                "Smoking",
                labelSmoking,
                comboBoxSmoking,
                new string[] {
                    "Current",
                    "Ex-",
                    "Never",
                    "Unknown" });

            // Final adjudication

            deInsufficientInfo = new ComboBoxDataEntry(
                "Insufficient Info",
                labelInsufficientInfo,
                comboBoxInsufficientInfo,
                new string[] { "Yes", "No" });

            deSpontaneous = new ComboBoxDataEntry(
                "Spontaneous",
                labelSpontaneous,
                comboBoxSpontaneous,
                new string[] {
                     "No",
                     "NSTEMI",
                     "STEMI" });

            deProcedural = new ComboBoxDataEntry(
                "Procedural",
                labelProcedural,
                comboBoxProcedural,
                new string[] {
                    "No",
                    "Type 4a",
                    "Type 4b",
                    "Type 4c",
                    "Type 5" });

            deSecondary = new ComboBoxDataEntry(
                "Secondary",
                labelSecondary,
                comboBoxSecondary,
                new string[] {
                    "No",
                    "Type 2",
                    "Acute Myocardial Injury",
                    "Chronic Myocardial Injury" });

            deSymptomsOfIschaemia = new ComboBoxDataEntry(
                "Symptoms Of Ischaemia",
                labelSymptomsOfIschaemia,
                comboBoxSymptomsOfIschaemia,
                new string[] { "Yes", "No" });

            deSignsOfIschaemia = new ComboBoxDataEntry(
                "Signs Of Ischaemia",
                labelSignsOfIschaemia,
                comboBoxSignsOfIschaemia,
                new string[] { "Yes", "No" });

            deSupplyDemandImbalance = new ComboBoxDataEntry(
                "Supply Demand Imbalance",
                labelSupplyDemandImbalance,
                comboBoxSupplyDemandImbalance,
                new string[] { "Yes", "No" });

            dePrimaryMechanism = new ComboBoxDataEntry(
                "Primary Mechanism",
                labelPrimaryMechanism,
                comboBoxPrimaryMechanism,
                new string[] {
                    "tachycardia",
                    "hypotension",
                    "hypoxaemia",
                    "anaemia",
                    "malignant hypertension",
                    "LVH",
                    "coronary embolism",
                    "coronary vasospasm" });

            deSuspectedCAD = new ComboBoxDataEntry(
                "Suspected CAD",
                labelSuspectedCAD,
                comboBoxSuspectedCAD,
                new string[] {
                    "known",
                    "high-probability",
                    "low-probability" });

            deCardiac = new ComboBoxDataEntry(
                "Cardiac",
                labelCardiac,
                comboBoxCardiac,
                new string[] {
                    "No",
                    "myopericarditis",
                    "acute heart failure",
                    "chronic heart failure",
                    "hypertensive heart disease",
                    "cardiomyopathy other",
                    "valvular heart disease",
                    "tachyarrhythmia",
                    "recent MI",
                    "acute aortic dissection",
                    "takotsubo cardiomyopathy",
                    "other" });

            deSystemic = new ComboBoxDataEntry(
                "Systemic",
                labelSystemic,
                comboBoxSystemic,
                new string[] {
                    "No",
                    "other",
                    "acute kidney injury",
                    "chronic kidney disease",
                    "pulmonary embolism",
                    "sepsis",
                    "GI",
                    "bleed",
                    "COPD",
                    "other" });

            // Physiological parameters

            deInitialObs = new ComboBoxDataEntry(
                "Initial Obs",
                labelInitialObs,
                comboBoxInitialObs,
                new string[] { "Yes", "No" });

            deOxygenTherapy = new ComboBoxDataEntry(
                "Oxygen Therapy",
                labelOxygenTherapy,
                comboBoxOxygenTherapy,
                new string[] {
                     "Yes",
                     "No",
                     "Uknown" });

            deAlert = new ComboBoxDataEntry(
                "Alert",
                labelAlert,
                comboBoxAlert,
                new string[] {
                     "Yes",
                     "No",
                     "Uknown" });

            deKillipClass = new ComboBoxDataEntry(
                "Killip Class",
                labelKillipClass,
                comboBoxKillipClass,
                new string[] {
                     "I",
                     "II",
                     "III",
                     "IV",
                     "Uknown" });

            deCardiacArrest = new ComboBoxDataEntry(
                "Cardiac Arrest",
                labelCardiacArrest,
                comboBoxCardiacArrest,
                new string[] { "Yes", "No" });

            deACSTreatmentInED = new ComboBoxDataEntry(
                "ACS Treatment In ED",
                labelACSTreatmentInED,
                comboBoxACSTreatmentInED,
                new string[] { "Yes", "No" });


            // Suspected ACS - not sure what to put here - think it is a yes or no
            deSuspectedACS = new ComboBoxDataEntry(
                "Suspected ACS",
                labelSuspectedACS,
                comboBoxSuspectedACS,
                new string[] { "Yes", "No" });

            // Text boxes

            deOxygenSat = new TextBoxDataEntry("Oxygen Sat", labelOxygenSat, textBoxOxygenSat, 0, 100);
            deRespiratoryRate = new TextBoxDataEntry("Respiratory Rate", labelRespiratoryRate, textBoxRespiratoryRate, 0, 100);
            deSystolicBP = new TextBoxDataEntry("Systolic BP", labelSystolicBP, textBoxSystolicBP,0,300);
            deDiastolicBP = new TextBoxDataEntry("Diastolic BP", labelDiastolicBP, textBoxDiastolicBP,0,300);
            deHeartRate = new TextBoxDataEntry("Heart Rate", labelHeartRate, textBoxHeartRate,0,300);
            deTemperature = new TextBoxDataEntry("Temperature", labelTemperature, textBoxTemperature,10,60);

            adjudication1DataEntries.Add(de12LeadECG);
            adjudication1DataEntries.Add(deECGNormal);
            adjudication1DataEntries.Add(deMyocardialIschaemia);
            adjudication1DataEntries.Add(deSubsequentIschaemia);
            adjudication1DataEntries.Add(deSTElevation);
            adjudication1DataEntries.Add(deSTDepression);
            adjudication1DataEntries.Add(deTWaveInversion);
            adjudication1DataEntries.Add(deQRSAbnormalities);
            adjudication1DataEntries.Add(dePathologicalQWave);
            adjudication1DataEntries.Add(deRhythm);
            adjudication1DataEntries.Add(deMechanism);
            adjudication1DataEntries.Add(deCulpritVessel);
            adjudication1DataEntries.Add(deSmoking);
            adjudication1DataEntries.Add(deInsufficientInfo);
            adjudication1DataEntries.Add(deSpontaneous);
            adjudication1DataEntries.Add(deProcedural);
            adjudication1DataEntries.Add(deSecondary);
            adjudication1DataEntries.Add(deSymptomsOfIschaemia);
            adjudication1DataEntries.Add(deSignsOfIschaemia);
            adjudication1DataEntries.Add(deSupplyDemandImbalance);
            adjudication1DataEntries.Add(dePrimaryMechanism);
            adjudication1DataEntries.Add(deSuspectedCAD);
            adjudication1DataEntries.Add(deCardiac);
            adjudication1DataEntries.Add(deSystemic);
            adjudication1DataEntries.Add(deInitialObs);
            adjudication1DataEntries.Add(deOxygenTherapy);
            adjudication1DataEntries.Add(deAlert);
            adjudication1DataEntries.Add(deKillipClass);
            adjudication1DataEntries.Add(deCardiacArrest);
            adjudication1DataEntries.Add(deACSTreatmentInED);
            adjudication1DataEntries.Add(deSuspectedACS);
            adjudication1DataEntries.Add(deOxygenSat);
            adjudication1DataEntries.Add(deRespiratoryRate);
            adjudication1DataEntries.Add(deSystolicBP);
            adjudication1DataEntries.Add(deDiastolicBP);
            adjudication1DataEntries.Add(deHeartRate);
            adjudication1DataEntries.Add(deTemperature);

            adjudication2DataEntries.Add(deInsufficientInfo);
            adjudication2DataEntries.Add(deSpontaneous); 
            adjudication2DataEntries.Add(deProcedural);
            adjudication2DataEntries.Add(deSecondary);
            adjudication2DataEntries.Add(deSymptomsOfIschaemia);
            adjudication2DataEntries.Add(deSignsOfIschaemia);
            adjudication2DataEntries.Add(deSupplyDemandImbalance);
            adjudication2DataEntries.Add(dePrimaryMechanism);
            adjudication2DataEntries.Add(deSuspectedCAD);
            adjudication2DataEntries.Add(deCardiac);
            adjudication2DataEntries.Add(deSystemic);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetCurrentCase();
            DisplayCurrentCase();
        }

        private void ShowHideComponents()
        {
            // This can be called by the contructor of these objects - ignore these invokations
            if (deSecondary == null) return;
            if (deSymptomsOfIschaemia == null) return;
            if (deInitialObs == null) return;
            if (deSubsequentIschaemia == null) return;

            if (deSecondary.GetValue() == "Type 2")
            {
                deSymptomsOfIschaemia.Show();
                deSignsOfIschaemia.Show();
                deSupplyDemandImbalance.Show();

                if (comboBoxSupplyDemandImbalance.SelectedValue.ToString() == "Yes")
                {
                    dePrimaryMechanism.Show();
                }
                else
                {
                    dePrimaryMechanism.Hide();
                }
            }
            else
            {
                deSymptomsOfIschaemia.Hide();
                deSignsOfIschaemia.Hide();
                deSupplyDemandImbalance.Hide();
                dePrimaryMechanism.Hide();
            }

            if (deSecondary.GetValue() == "Type 2"  ||
                deSecondary.GetValue() == "Acute Myocardial Injury"  ||
                deSecondary.GetValue() == "Chronic Myocardial Injury")
            {
                deSuspectedCAD.Show();
                deCardiac.Show();
                deSystemic.Show();
            }
            else
            {
                deSuspectedCAD.Hide();
                deCardiac.Hide();
                deSystemic.Hide();
            }

            if (deInitialObs.GetValue() == "Yes")
            {
                deOxygenSat.Show();
                deOxygenTherapy.Show();
                deRespiratoryRate.Show();
                deSystolicBP.Show();
                deDiastolicBP.Show();
                deHeartRate.Show();  
                deTemperature.Show();
                deAlert.Show();
                deKillipClass.Show();
                deCardiacArrest.Show();
                deACSTreatmentInED.Show();
            }
            else
            {
                deOxygenSat.Hide();
                deOxygenTherapy.Hide();
                deRespiratoryRate.Hide();
                deSystolicBP.Hide();
                deDiastolicBP.Hide();
                deHeartRate.Hide();
                deTemperature.Hide();
                deAlert.Hide();
                deKillipClass.Hide();
                deCardiacArrest.Hide();
                deACSTreatmentInED.Hide();

            }

            if (deMyocardialIschaemia.GetValue() == "No")
            {
                deSubsequentIschaemia.Show();
            }
            else
            {
                deSubsequentIschaemia.Hide();
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (StoreCurrentCase())
            {
                if (currentCase > 0) currentCase--;
                DisplayCurrentCase();
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (StoreCurrentCase())
            {
                if (currentCase < adjudicationCases.cases.Count() - 1) currentCase++;
                DisplayCurrentCase();
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            // TODO - check if saved first and warn if not saved etc

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                this.adjudicationCases.ReadCasesFromFile(filePath);

                SetCurrentCase();
                DisplayCurrentCase();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (StoreCurrentCase())
            {
                DisplayCurrentCase(); // This causes the status text to record this case has been adjudicated
            }
        }

        private void SetCurrentCase()
        {
            currentCase = adjudicationCases.FirstNonAdjudicatedCase();
        }

        private void DisplayCurrentCase()
        {
            if (this.adjudicationCases.cases.Count == 0)
            {
                DisplayEmptyCase();
            }
            else
            {
                AdjudicationCase c = this.adjudicationCases.cases[currentCase];

                textBoxId.Text = c.Id;
                textBoxArrivalDate.Text = c.ArrivalDate;
                textBoxPrimarySymptom.Text = c.PrimarySymptom;
                textBoxTimeSinceOnset.Text = c.TimeSinceOnset;
                comboBoxSuspectedACS.SelectedValue = c.SuspectedACS;
                textBoxAge.Text = c.Age;
                textBoxSex.Text = c.Sex;

                DataTable troponinTestsData = new DataTable("Troponin Tests");
                troponinTestsData.Columns.Add("Time from presentation");
                troponinTestsData.Columns.Add("Result");
                foreach (TroponinTest tt in c.TroponinTests)
                {
                    DataRow row = troponinTestsData.NewRow();
                    row["Time from presentation"] = tt.TimeFromPresentation;
                    row["Result"] = tt.Result;
                    if (tt.InvalidResultFlag == "Y")
                    {
                        row["Result"] = "INVALID";
                    }
                    troponinTestsData.Rows.Add(row);
                }
                dataGridViewTroponinTests.DataSource = troponinTestsData;

                // Hides the first column of each row that supports selection
                dataGridViewTroponinTests.RowHeadersVisible = false;
                dataGridViewTroponinTests.AllowUserToAddRows = false;
                dataGridViewTroponinTests.AllowUserToDeleteRows = false;
                dataGridViewTroponinTests.ReadOnly = true;
                dataGridViewTroponinTests.BackgroundColor = Color.White;

                // Display notes
                string notes = "";
                foreach (string note in c.EmergencyDepartmentNotes)
                {
                    notes += note + "\n";
                }
                richTextBoxEDNotes.Text = notes;

                notes = "";
                foreach (string note in c.DischargeNotes)
                {
                    notes += note + "\n";
                }
                richTextBoxDischargeNotes.Text = notes;


                HighlightText(richTextBoxDischargeNotes);
                HighlightText(richTextBoxEDNotes);

                textBoxECGTimeFromPresentation.Text = c.ECGTimeFromPresentation;
                richTextBoxECGMUSEText.Text = c.ECGMUSEText;

                // Tomcat data
                textBoxCath.Text = c.TomcatCath;
                textBoxDaysFromPresentation.Text = c.TomcatDaysFromPresentation; 
                textBoxLCx.Text = c.TomcatLCx;
                textBoxRCA.Text = c.TomcatRCA;
                textBoxLMS.Text = c.TomcatLMS;
                textBoxLAD.Text = c.TomcatLAD;
                richTextTomcatText.Text = c.TomcatText;

                if (this.adjudicationCases.IsFirstAdjudicator)
                {
                    if (c.Adjudication1Complete)
                    {
                        deSuspectedACS.SetValue(c.SuspectedACS1);
                        de12LeadECG.SetValue(c.ECG12Lead);
                        deECGNormal.SetValue(c.ECGNormalAbnormal);
                        deMyocardialIschaemia.SetValue(c.ECGMyocardialIschaemia);
                        deSubsequentIschaemia.SetValue(c.ECGSubsequentIschaemia);
                        deSTElevation.SetValue(c.ECGSTElevation);
                        deSTDepression.SetValue(c.ECGSTDepression);
                        deTWaveInversion.SetValue(c.ECGTWaveInversion);
                        deQRSAbnormalities.SetValue(c.ECGQRSAbnormalities);
                        dePathologicalQWave.SetValue(c.ECGPathlogicalQWave);
                        deRhythm.SetValue(c.Rhythum);
                        deMechanism.SetValue(c.Mechanism);
                        deCulpritVessel.SetValue(c.CulpritVessel);
                        deSmoking.SetValue(c.Smoking);
                        deInitialObs.SetValue(c.InitialObs);
                        deOxygenSat.SetValue(c.OxygenSat);
                        deOxygenTherapy.SetValue(c.OxygenTherapy);
                        deRespiratoryRate.SetValue(c.RespiratoryRate);
                        deSystolicBP.SetValue(c.SystolicBP);
                        deDiastolicBP.SetValue(c.DiastolicBP);
                        deHeartRate.SetValue(c.HeartRate);
                        deTemperature.SetValue(c.Temperature);
                        deAlert.SetValue(c.Alert);
                        deKillipClass.SetValue(c.KillipClass);
                        deCardiacArrest.SetValue(c.CardiacArrest);
                        deACSTreatmentInED.SetValue(c.ACSTreatmentInED);
                        deInsufficientInfo.SetValue(c.InsufficientInfo);
                        deSpontaneous.SetValue(c.Spontaneous);
                        deProcedural.SetValue(c.Procedural);
                        deSecondary.SetValue(c.Secondary);
                        deSymptomsOfIschaemia.SetValue(c.SymptomsOfIschaemia);
                        deSignsOfIschaemia.SetValue(c.SignsOfIschaemia);
                        deSupplyDemandImbalance.SetValue(c.SupplyDemandImbalance);
                        dePrimaryMechanism.SetValue(c.PrimaryMechanism);
                        deSuspectedCAD.SetValue(c.SuspectedCAD);
                        deCardiac.SetValue(c.Cardiac);
                        deSystemic.SetValue(c.Systemic);
                    }
                    else
                    {
                        foreach( IAdjudicationDataEntry dataEntry in adjudication1DataEntries)
                        {
                            dataEntry.SetEmpty();
                        }
                    }
                }
                else
                {
                    deSuspectedACS.SetValue(c.SuspectedACS1);
                    de12LeadECG.SetValue(c.ECG12Lead);
                    deECGNormal.SetValue(c.ECGNormalAbnormal);
                    deMyocardialIschaemia.SetValue(c.ECGMyocardialIschaemia);
                    deSubsequentIschaemia.SetValue(c.ECGSubsequentIschaemia);
                    deSTElevation.SetValue(c.ECGSTElevation);
                    deSTDepression.SetValue(c.ECGSTDepression);
                    deTWaveInversion.SetValue(c.ECGTWaveInversion);
                    deQRSAbnormalities.SetValue(c.ECGQRSAbnormalities);
                    dePathologicalQWave.SetValue(c.ECGPathlogicalQWave);
                    deRhythm.SetValue(c.Rhythum);
                    deMechanism.SetValue(c.Mechanism);
                    deCulpritVessel.SetValue(c.CulpritVessel);
                    deSmoking.SetValue(c.Smoking);
                    deInitialObs.SetValue(c.InitialObs);
                    deOxygenSat.SetValue(c.OxygenSat);
                    deOxygenTherapy.SetValue(c.OxygenTherapy);
                    deRespiratoryRate.SetValue(c.RespiratoryRate);
                    deSystolicBP.SetValue(c.SystolicBP);
                    deDiastolicBP.SetValue(c.DiastolicBP);
                    deHeartRate.SetValue(c.HeartRate);
                    deTemperature.SetValue(c.Temperature);
                    deAlert.SetValue(c.Alert);
                    deKillipClass.SetValue(c.KillipClass);
                    deCardiacArrest.SetValue(c.CardiacArrest);
                    deACSTreatmentInED.SetValue(c.ACSTreatmentInED);

                    // Adjudication 2
                    if (c.Adjudication2Complete)
                    {
                        deInsufficientInfo.SetValue(c.InsufficientInfo2);
                        deSpontaneous.SetValue(c.Spontaneous2);
                        deProcedural.SetValue(c.Procedural2);
                        deSecondary.SetValue(c.Secondary2);
                        deSymptomsOfIschaemia.SetValue(c.SymptomsOfIschaemia2);
                        deSignsOfIschaemia.SetValue(c.SignsOfIschaemia2);
                        deSupplyDemandImbalance.SetValue(c.SupplyDemandImbalance2);
                        dePrimaryMechanism.SetValue(c.PrimaryMechanism2);
                        deSuspectedCAD.SetValue(c.SuspectedCAD2);
                        deCardiac.SetValue(c.Cardiac2);
                        deSystemic.SetValue(c.Systemic2);
                    }
                    else
                    {
                        foreach (IAdjudicationDataEntry dataEntry in adjudication2DataEntries)
                        {
                            dataEntry.SetEmpty();
                        }
                    }
                }

                // Enable next and previous buttons
                buttonNext.Enabled = (currentCase < adjudicationCases.cases.Count - 1);
                buttonPrevious.Enabled = (currentCase > 0);

                // Progress label
                string progressText = "";
                if (this.adjudicationCases.IsFirstAdjudicator)
                {
                    progressText = "First adjudicator: case ";
                    progressText += (currentCase + 1);
                    progressText += " of ";
                    progressText += this.adjudicationCases.cases.Count;
                    progressText += " (";
                    progressText += this.adjudicationCases.FirstAdjudicatorCompletedCount();
                    progressText += " adjuicated)";
                }
                else
                {
                    progressText = "Second adjudicator: case ";
                    progressText += (currentCase + 1);
                    progressText += " of ";
                    progressText += this.adjudicationCases.cases.Count;
                    progressText += " (";
                    progressText += this.adjudicationCases.SecondAdjudicatorCompletedCount();
                    progressText += " adjuicated)";
                }
                labelProgress.Text = progressText;
            }

            ShowHideComponents();
        }

        private void DisplayEmptyCase()
        {
            textBoxId.Text = "";
            textBoxArrivalDate.Text = "";
            textBoxPrimarySymptom.Text = "";
            textBoxTimeSinceOnset.Text = "";
            comboBoxSuspectedACS.SelectedValue = "";
            textBoxAge.Text = "";
            textBoxSex.Text = "";

            DataTable troponinTestsData = new DataTable("Troponin Tests");
            troponinTestsData.Columns.Add("Time from presentation");
            troponinTestsData.Columns.Add("Result");
            dataGridViewTroponinTests.DataSource = troponinTestsData;

            // Annotation 1 data
            foreach (IAdjudicationDataEntry dataEntry in adjudication1DataEntries)
            {
                dataEntry.SetEmpty();
            }

            // Progress label
            labelProgress.Text = "";

            buttonNext.Enabled = false; 
            buttonPrevious.Enabled = false;
        }

        private bool ValidateInput()
        {
            bool valid = true;

            List<IAdjudicationDataEntry> inputsToValidate =
                adjudicationCases.IsFirstAdjudicator ? adjudication1DataEntries : adjudication2DataEntries;

            StringBuilder sb = new StringBuilder();

            foreach (IAdjudicationDataEntry de in inputsToValidate)
            {
                string errorReport;

                if (!de.IsValid(out errorReport))
                {
                    valid = false;
                    sb.AppendLine(errorReport);
                }
            }

            if (!valid)
            {
                MessageBox.Show(
                    sb.ToString(), "Data entry invalid",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valid;
        }

        private bool StoreCurrentCase()
        {
            if (!ValidateInput()) return false;

            AdjudicationCase c = this.adjudicationCases.cases[currentCase];

            if (this.adjudicationCases.IsFirstAdjudicator)
            {
                c.Adjudication1Complete = true;
                c.Adjudicator1 = Environment.UserName;

                c.SuspectedACS1 = deSuspectedACS.GetValue();
                c.ECG12Lead = de12LeadECG.GetValue();
                c.ECGNormalAbnormal = deECGNormal.GetValue();
                c.ECGMyocardialIschaemia = deMyocardialIschaemia.GetValue();
                c.ECGSubsequentIschaemia = deSubsequentIschaemia.GetValue();
                c.ECGSTElevation = deSTElevation.GetValue();
                c.ECGSTDepression = deSTDepression.GetValue();
                c.ECGTWaveInversion = deTWaveInversion.GetValue();
                c.ECGQRSAbnormalities = deQRSAbnormalities.GetValue();
                c.ECGPathlogicalQWave = dePathologicalQWave.GetValue();
                c.Rhythum = deRhythm.GetValue();
                c.Mechanism = deMechanism.GetValue();
                c.CulpritVessel = deCulpritVessel.GetValue();
                c.Smoking = deSmoking.GetValue();
                c.InitialObs = deInitialObs.GetValue();
                c.OxygenSat = deOxygenSat.GetValue();
                c.OxygenTherapy = deOxygenTherapy.GetValue();
                c.RespiratoryRate = deRespiratoryRate.GetValue();
                c.SystolicBP = deSystolicBP.GetValue();
                c.DiastolicBP = deDiastolicBP.GetValue();
                c.HeartRate = deHeartRate.GetValue();
                c.Temperature = deTemperature.GetValue();
                c.Alert = deAlert.GetValue();
                c.KillipClass = deKillipClass.GetValue();
                c.CardiacArrest = deCardiacArrest.GetValue();
                c.ACSTreatmentInED = deACSTreatmentInED.GetValue();
                c.InsufficientInfo = deInsufficientInfo.GetValue();
                c.Spontaneous = deSpontaneous.GetValue();
                c.Procedural = deProcedural.GetValue();
                c.Secondary = deSecondary.GetValue();
                c.SymptomsOfIschaemia = deSymptomsOfIschaemia.GetValue();
                c.SignsOfIschaemia = deSignsOfIschaemia.GetValue();
                c.SupplyDemandImbalance = deSupplyDemandImbalance.GetValue();
                c.PrimaryMechanism = dePrimaryMechanism.GetValue();
                c.SuspectedCAD = deSuspectedCAD.GetValue();
                c.Cardiac = deCardiac.GetValue();
                c.Systemic = deSystemic.GetValue();
            }
            else
            {
                c.Adjudication2Complete = true;
                c.Adjudicator2 = Environment.UserName;

                c.InsufficientInfo2 = deInsufficientInfo.GetValue();
                c.Spontaneous2 = deSpontaneous.GetValue();
                c.Procedural2 = deProcedural.GetValue();
                c.Secondary2 = deSecondary.GetValue();
                c.SymptomsOfIschaemia2 = deSymptomsOfIschaemia.GetValue();
                c.SignsOfIschaemia2 = deSignsOfIschaemia.GetValue();
                c.SupplyDemandImbalance2 = deSupplyDemandImbalance.GetValue();
                c.PrimaryMechanism2 = dePrimaryMechanism.GetValue();
                c.SuspectedCAD2 = deSuspectedCAD.GetValue();
                c.Cardiac2 = deCardiac.GetValue();
                c.Systemic2 = deSystemic.GetValue();
            }

            adjudicationCases.Save();

            return true;
        }

        private void HighlightText(RichTextBox aTextBox)
        {
            // match words containing "smok", or 2 numbers separated by a forward slash
            string pattern = @"(?i)\b\w*smok\w*\b|\d+/\d+";
            string input = aTextBox.Text;

            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match m in matches)
            {
                aTextBox.Select(m.Index, m.Length);
                aTextBox.SelectionBackColor = Color.Yellow;
            }

            // match cxr, chest x-ray, chest xray, chestx-ray, chestxray
            pattern = @"(?i)cxr|chest\s*x-?ray";
            input = aTextBox.Text;

            matches = Regex.Matches(input, pattern);

            foreach (Match m in matches)
            {
                aTextBox.Select(m.Index, m.Length);
                aTextBox.SelectionBackColor = Color.LightSkyBlue;
            }

            // match ecg, electro cardio graph, electro cardio gram
            pattern = @"(?i)ecg|electro\s*cardio\s*graph|electro\s*cardio\s*gram";
            input = aTextBox.Text;

            matches = Regex.Matches(input, pattern);

            foreach (Match m in matches)
            {
                aTextBox.Select(m.Index, m.Length);
                aTextBox.SelectionBackColor = Color.GreenYellow;
            }
        }

        private void comboBoxSecondary_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideComponents();
        }

        private void comboBoxInitialObs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideComponents();
        }

        private void comboBoxMyocardialIschaemia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideComponents();
        }
    }
}