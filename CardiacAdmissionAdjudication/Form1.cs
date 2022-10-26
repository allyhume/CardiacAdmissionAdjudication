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

        private bool dynamicallyHandleSelectionChanges = true;

        private System.Diagnostics.Process? pdfViewerProcess;

        public Form1(AdjudicationCases cases)
        {
            InitializeComponent();

            dynamicallyHandleSelectionChanges = false;

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
                    "GI bleed",
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
                new string[] { "Yes", "No", "Unknown" });

            deACSTreatmentInED = new ComboBoxDataEntry(
                "ACS Treatment In ED",
                labelACSTreatmentInED,
                comboBoxACSTreatmentInED,
                new string[] { "Yes", "No", "Unknown" });


            // Suspected ACS
            deSuspectedACS = new ComboBoxDataEntry(
                "Suspected ACS",
                labelSuspectedACS,
                comboBoxSuspectedACS,
                new string[] { "Yes", "No", "Unknown" });

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

            dynamicallyHandleSelectionChanges = true;

            pdfViewerProcess = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetCurrentCase();
            DisplayCurrentCase();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsCurrentCaseSaved())
            {
                if (MessageBox.Show("Do you want to exit without saving the current case?", "Exit", MessageBoxButtons.YesNo) 
                    == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }


        /// <summary>
        /// Shows or hides the various components based on the application logic of
        /// which combinations are valid together.
        /// </summary>
        private void ShowHideComponents()
        {
            // Ignore if not currently dynamically handling selection changes.
            // We need this or things become a recursive mess
            if (!dynamicallyHandleSelectionChanges) return;

            // This can be called by the contructor of these objects - ignore these invokations
            if (deSecondary == null) return;
            if (deSymptomsOfIschaemia == null) return;
            if (deInitialObs == null) return;
            if (deSubsequentIschaemia == null) return;
            if (de12LeadECG == null) return;
            if (deECGNormal == null) return;

            dynamicallyHandleSelectionChanges = false; ;

            // 12 Lead ECG set to No hides all other ECG options as there is no ECG.
            if (de12LeadECG.GetValue() == "No")
            {
                deECGNormal.Hide();
                deMyocardialIschaemia.Hide();
                deSTElevation.Hide();
                deSTDepression.Hide();
                deTWaveInversion.Hide();
                deQRSAbnormalities.Hide();
                deRhythm.Hide();
                dePathologicalQWave.Hide();
                deSubsequentIschaemia.Hide();
            }
            else
            {
                deECGNormal.Show();
                deMyocardialIschaemia.Show();
                deSTElevation.Show();
                deSTDepression.Show();
                deTWaveInversion.Show();
                deQRSAbnormalities.Show();
                deRhythm.Show();
                dePathologicalQWave.Show();
                deSubsequentIschaemia.Show();
            }


            // If ECG is normal then all the other options ECG options are set to
            // No or None and Rhythm is set to SR
            if (deECGNormal.GetValue() == "Normal")
            {
                deMyocardialIschaemia.SetValue("No");
                deSTElevation.SetValue("No");
                deSTDepression.SetValue("No");
                deTWaveInversion.SetValue("No");
                deQRSAbnormalities.SetValue("None");
                dePathologicalQWave.SetValue("No");
                deRhythm.SetValue("SR");

                deMyocardialIschaemia.SetEditable(false);
                deSTElevation.SetEditable(false);
                deSTDepression.SetEditable(false);
                deTWaveInversion.SetEditable(false);
                deQRSAbnormalities.SetEditable(false);
                dePathologicalQWave.SetEditable(false);
                deRhythm.SetEditable(false);
            }
            else
            {
                deMyocardialIschaemia.SetEditable(true);
                deSTElevation.SetEditable(true);
                deSTDepression.SetEditable(true);
                deTWaveInversion.SetEditable(true);
                deQRSAbnormalities.SetEditable(true);
                dePathologicalQWave.SetEditable(true);
                deRhythm.SetEditable(true);
            }

            // When Myocardial Ischaemia is No then ask if there was a subsequent
            // ischaemia.

            if (deMyocardialIschaemia.GetValue() == "No")
            {
                deSubsequentIschaemia.Show();
            }
            else
            {
                deSubsequentIschaemia.Hide();
            }

            // Initial Obs = No means Initial Obs hide oxygen *, respiratory rate,
            // * BP, heart rate, and temperature.
            if (deInitialObs.GetValue() == "Yes")
            {
                deOxygenSat.Show();
                deOxygenTherapy.Show();
                deRespiratoryRate.Show();
                deSystolicBP.Show();
                deDiastolicBP.Show();
                deHeartRate.Show();
                deTemperature.Show();
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
            }


            // Select exactlty one thing from insufficient info, spontaneous, procedural and secondary.
            // If insufficient info = 'Yes' the other 3 are hidden.
            // For the other three, if one is not No there others are all set to No (with corresponding
            // further hiding)
            if (deInsufficientInfo.GetValue() == "Yes")
            {
                deSpontaneous.Hide();
                deProcedural.Hide();
                deSecondary.Hide();
            }
            else
            {
                deSpontaneous.Show();
                deProcedural.Show();
                deSecondary.Show();

                bool isSomethingSpecified = false;

                if (deSpontaneous.GetValue() != "No" && deSpontaneous.GetValue() != "")
                {
                    deInsufficientInfo.SetValue("No");
                    deProcedural.SetValue("No");
                    deSecondary.SetValue("No");

                    deInsufficientInfo.SetEditable(false);
                    deProcedural.SetEditable(false);
                    deSecondary.SetEditable(false);

                    isSomethingSpecified = true;
                }

                if (deProcedural.GetValue() != "No" && deProcedural.GetValue() != "")
                {
                    deInsufficientInfo.SetValue("No");
                    deSpontaneous.SetValue("No");
                    deSecondary.SetValue("No");
                    isSomethingSpecified = true;

                    deInsufficientInfo.SetEditable(false);
                    deSpontaneous.SetEditable(false);
                    deSecondary.SetEditable(false);

                }

                if (deSecondary.GetValue() != "No" && deSecondary.GetValue() != "")
                {
                    deInsufficientInfo.SetValue("No");
                    deSpontaneous.SetValue("No");
                    deProcedural.SetValue("No");
                    isSomethingSpecified = true;

                    deInsufficientInfo.SetEditable(false);
                    deSpontaneous.SetEditable(false);
                    deProcedural.SetEditable(false);
                }

                // If nothing specified then all are editable
                if (!isSomethingSpecified)
                {
                    // These calls ensure the current value is stored before making editable
                    deInsufficientInfo.SetValue(deInsufficientInfo.GetValue());
                    deSpontaneous.SetValue(deSpontaneous.GetValue());
                    deProcedural.SetValue(deProcedural.GetValue());
                    deSecondary.SetValue(deSecondary.GetValue());

                    deInsufficientInfo.SetEditable(true);
                    deSpontaneous.SetEditable(true);
                    deProcedural.SetEditable(true);
                    deSecondary.SetEditable(true);
                }
            }

            // Symptoms of Ischaemia, signs of ischaemia and supply demand imbalance are
            // only asked for when Secondary = 'Type 2'. If supply demand imbalance is
            // 'Yes' then also ask for primary mechanism.

            if (deSecondary.GetValue() == "Type 2")
            {
                deSymptomsOfIschaemia.Show();
                deSignsOfIschaemia.Show();
                deSupplyDemandImbalance.Show();

                if (deSupplyDemandImbalance.GetValue() == "Yes")
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

            // Suspected CAD, Cardiac and Systemic are only asked for when Secondary
            // is one of Type 2; Acute Myocardial Injury; or Chronic Myocardial Injury.

            if (deSecondary.GetValue() == "Type 2"  ||
                deSecondary.GetValue() == "Acute Myocardial Injury"  ||
                deSecondary.GetValue() == "Chronic Myocardial Injury")
            {
                deSuspectedCAD.Show();
                deCardiac.Show();
                deSystemic.Show();

                bool isCardiacOrSystemicSpecified = false;

                if (deCardiac.GetValue() != "" && deCardiac.GetValue() != "No")
                {
                    isCardiacOrSystemicSpecified = true;
                    deSystemic.SetValue("No");
                    deSystemic.SetEditable(false);
                }

                if (deSystemic.GetValue() != "" && deSystemic.GetValue() != "No")
                {
                    isCardiacOrSystemicSpecified = true;
                    deCardiac.SetValue("No");
                    deCardiac.SetEditable(false);
                }

                if (!isCardiacOrSystemicSpecified)
                {
                    // These calls ensure the current value is stored before making editable
                    deCardiac.SetValue(deCardiac.GetValue());
                    deSystemic.SetValue(deSystemic.GetValue());

                    deCardiac.SetEditable(true);
                    deSystemic.SetEditable(true);
                }
            }
            else
            {
                deSuspectedCAD.Hide();
                deCardiac.Hide();
                deSystemic.Hide();
            }

            dynamicallyHandleSelectionChanges = true;
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (IsCurrentCaseSaved())
            {
                if (currentCase > 0) currentCase--;
                DisplayCurrentCase();
                DisplayECGPDF();
            }
            else if (StoreCurrentCase())
            {
                if (currentCase > 0) currentCase--;
                DisplayCurrentCase();
                DisplayECGPDF();
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (IsCurrentCaseSaved())
            {
                if (currentCase < adjudicationCases.cases.Count() - 1) currentCase++;
                DisplayCurrentCase();
                DisplayECGPDF();
            }
            else if (StoreCurrentCase())
            {
                if (currentCase < adjudicationCases.cases.Count() - 1) currentCase++;
                DisplayCurrentCase();
                DisplayECGPDF();
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (!IsCurrentCaseSaved())
            {
                if (MessageBox.Show("Do you want to load without saving the current case?", "Load", MessageBoxButtons.YesNo)
                        == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                this.adjudicationCases.ReadCasesFromFile(filePath);

                SetCurrentCase();
                DisplayCurrentCase();
                DisplayECGPDF();
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

        private void DisplayECGPDF()
        {
            if (this.adjudicationCases.cases.Count != 0)
            {
                AdjudicationCase c = this.adjudicationCases.cases[currentCase];

                this.pdfViewerProcess = new System.Diagnostics.Process();
                this.pdfViewerProcess.StartInfo.UseShellExecute = true;
                this.pdfViewerProcess.StartInfo.FileName = c.ECGPDF;
                this.pdfViewerProcess.Start();
            }
        }

        private void DisplayCurrentCase()
        {
            dynamicallyHandleSelectionChanges = false;

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
                textBoxAge.Text = c.Age;
                textBoxSex.Text = c.Sex;
                textBoxHaemoglobin.Text = c.Haemoglobin;
                textBoxCreatinine.Text = c.Creatinine;
                textBoxEGFR.Text = c.eGRF;

                DataTable troponinTestsData = new DataTable("Troponin Tests");
                troponinTestsData.Columns.Add("Hour");
                troponinTestsData.Columns.Add("Troponin");
                troponinTestsData.Columns.Add("Status");

                foreach (TroponinTest tt in c.TroponinTests)
                {
                    DataRow row = troponinTestsData.NewRow();
                    row["Hour"] = tt.TimeFromPresentation;
                    row["Troponin"] = tt.Result;
                    row["Status"] = tt.Status;
                    troponinTestsData.Rows.Add(row);
                }
                dataGridViewTroponinTests.DataSource = troponinTestsData;

                // Hides the first column of each row that supports selection
                dataGridViewTroponinTests.RowHeadersVisible = false;
                dataGridViewTroponinTests.AllowUserToAddRows = false;
                dataGridViewTroponinTests.AllowUserToDeleteRows = false;
                dataGridViewTroponinTests.ReadOnly = true;
                dataGridViewTroponinTests.BackgroundColor = Color.White;
                dataGridViewTroponinTests.ClearSelection();

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
                textBoxDaysFromPresentation.Text = c.TomcatDaysFromPresentation; 
                textBoxLCx.Text = c.TomcatLCx;
                textBoxRCA.Text = c.TomcatRCA;
                textBoxLMS.Text = c.TomcatLMS;
                textBoxLAD.Text = c.TomcatLAD;
                richTextTomcatText.Text = c.TomcatText;

                // If no Tomcat data hide the tomcat inputs
                if (c.TomcatDaysFromPresentation == "" &&
                    c.TomcatLCx == "" &&
                    c.TomcatRCA == "" &&
                    c.TomcatLMS == "" &&
                    c.TomcatLAD == "" &&
                    c.TomcatText == "" )
                {
                    deCulpritVessel.Hide();
                    deMechanism.Hide();    
                }
                else
                {
                    deCulpritVessel.Show();
                    deMechanism.Show();
                }


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
                        foreach (IAdjudicationDataEntry dataEntry in adjudication1DataEntries)
                        {
                            dataEntry.SetEmpty();
                        }
                        foreach (IAdjudicationDataEntry dataEntry in adjudication2DataEntries)
                        {
                            dataEntry.SetEmpty();
                        }
                    }

                    // All entry points must be editable
                    foreach (IAdjudicationDataEntry dataEntry in adjudication1DataEntries)
                    {
                        dataEntry.SetEditable(true);
                    }
                    foreach (IAdjudicationDataEntry dataEntry in adjudication2DataEntries)
                    {
                        dataEntry.SetEditable(true);
                    }
                }
                else // Second adjudicator
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
                    // Only adjudicate 2 data entry points are editable
                    foreach (IAdjudicationDataEntry dataEntry in adjudication1DataEntries)
                    {
                        dataEntry.SetEditable(false);
                    }
                    foreach (IAdjudicationDataEntry dataEntry in adjudication2DataEntries)
                    {
                        dataEntry.SetEditable(true);
                    }
                }

                // Ensure all data entry points are set flagging as invalid (in red)
                foreach (IAdjudicationDataEntry dataEntry in adjudication1DataEntries)
                {
                    dataEntry.SetValid();
                }
                foreach (IAdjudicationDataEntry dataEntry in adjudication2DataEntries)
                {
                    dataEntry.SetValid();
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

            // Always go to ECG tab when displaying new case
            DataEntryTab.SelectedTab = tabPageECG;

            dynamicallyHandleSelectionChanges = true;

            ShowHideComponents();
        }

        private void DisplayEmptyCase()
        {
            dynamicallyHandleSelectionChanges = false;

            textBoxId.Text = "";
            textBoxArrivalDate.Text = "";
            textBoxPrimarySymptom.Text = "";
            textBoxTimeSinceOnset.Text = "";
            textBoxAge.Text = "";
            textBoxSex.Text = "";
            textBoxHaemoglobin.Text = "";
            textBoxCreatinine.Text = "";
            textBoxEGFR.Text = "";

            DataTable troponinTestsData = new DataTable("Troponin Tests");

            dataGridViewTroponinTests.DataSource = troponinTestsData;

            // Everything is set to empty
            foreach (IAdjudicationDataEntry dataEntry in adjudication1DataEntries)
            {
                dataEntry.SetEmpty();
            }
            foreach (IAdjudicationDataEntry dataEntry in adjudication2DataEntries)
            {
                dataEntry.SetEmpty();
            }

            // Progress label
            labelProgress.Text = "";

            buttonNext.Enabled = false; 
            buttonPrevious.Enabled = false;

            dynamicallyHandleSelectionChanges = true;
        }

        private bool ValidateInput()
        {
            bool valid = true;

            StringBuilder sb = new StringBuilder();

            if (adjudicationCases.IsFirstAdjudicator)
            {
                foreach (IAdjudicationDataEntry de in adjudication1DataEntries)
                {
                    string errorReport;

                    if (!de.IsValid(out errorReport))
                    {
                        valid = false;
                        sb.AppendLine(errorReport);
                    }
                }
            }

            foreach (IAdjudicationDataEntry de in adjudication2DataEntries)
            {
                string errorReport;

                if (!de.IsValid(out errorReport))
                {
                    valid = false;
                    sb.AppendLine(errorReport);
                }
            }

            // Only one of Cardiac or Systemic can be 'No'
            if (deCardiac.GetValue() == "No" && deSystemic.GetValue() == "No")
            {
                sb.AppendLine("One of Cardiac or Systemic must not be No.");
                valid = false;
            }

            if (!valid)
            {
                MessageBox.Show(
                    sb.ToString(), "Data entry invalid",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valid;
        }

        private bool IsCurrentCaseSaved()
        {
            // If no cases then we consider it saved
            if (this.adjudicationCases.cases.Count == 0) return true;

            AdjudicationCase c = this.adjudicationCases.cases[currentCase];

            if (this.adjudicationCases.IsFirstAdjudicator)
            {
                // If nothing has been saved then we consider it saved if we are
                // still on all the default values so nothing has been entered
                if (!c.Adjudication1Complete)
                {
                    if (deSuspectedACS.GetValue() != c.SuspectedACS) return false;
                    if ("" != de12LeadECG.GetValue()) return false;
                    if ("" != deECGNormal.GetValue()) return false;
                    if ("" != deMyocardialIschaemia.GetValue()) return false;
                    if ("" != deSubsequentIschaemia.GetValue()) return false;
                    if ("" != deSTElevation.GetValue()) return false;
                    if ("" != deSTDepression.GetValue()) return false;
                    if ("" != deTWaveInversion.GetValue()) return false;
                    if ("" != deQRSAbnormalities.GetValue()) return false;
                    if ("" != dePathologicalQWave.GetValue()) return false;
                    if ("" != deRhythm.GetValue()) return false;
                    if ("" != deMechanism.GetValue()) return false;
                    if ("" != deCulpritVessel.GetValue()) return false;
                    if ("" != deSmoking.GetValue()) return false;
                    if ("" != deInitialObs.GetValue()) return false;
                    if ("" != deOxygenSat.GetValue()) return false;
                    if ("" != deOxygenTherapy.GetValue()) return false;
                    if ("" != deRespiratoryRate.GetValue()) return false;
                    if ("" != deSystolicBP.GetValue()) return false;
                    if ("" != deDiastolicBP.GetValue()) return false;
                    if ("" != deHeartRate.GetValue()) return false;
                    if ("" != deTemperature.GetValue()) return false;
                    if ("" != deAlert.GetValue()) return false;
                    if ("" != deKillipClass.GetValue()) return false;
                    if ("" != deCardiacArrest.GetValue()) return false;
                    if ("" != deACSTreatmentInED.GetValue()) return false;
                    if ("" != deInsufficientInfo.GetValue()) return false;
                    if ("" != deSpontaneous.GetValue()) return false;
                    if ("" != deProcedural.GetValue()) return false;
                    if ("" != deSecondary.GetValue()) return false;
                    if ("" != deSymptomsOfIschaemia.GetValue()) return false;
                    if ("" != deSignsOfIschaemia.GetValue()) return false;
                    if ("" != deSupplyDemandImbalance.GetValue()) return false;
                    if ("" != dePrimaryMechanism.GetValue()) return false;
                    if ("" != deSuspectedCAD.GetValue()) return false;
                    if ("" != deCardiac.GetValue()) return false;
                    if ("" != deSystemic.GetValue()) return false;
                }
                else
                {
                    if (c.SuspectedACS1 != deSuspectedACS.GetValue()) return false;
                    if (c.ECG12Lead != de12LeadECG.GetValue()) return false;
                    if (c.ECGNormalAbnormal != deECGNormal.GetValue()) return false;
                    if (c.ECGMyocardialIschaemia != deMyocardialIschaemia.GetValue()) return false;
                    if (c.ECGSubsequentIschaemia != deSubsequentIschaemia.GetValue()) return false;
                    if (c.ECGSTElevation != deSTElevation.GetValue()) return false;
                    if (c.ECGSTDepression != deSTDepression.GetValue()) return false;
                    if (c.ECGTWaveInversion != deTWaveInversion.GetValue()) return false;
                    if (c.ECGQRSAbnormalities != deQRSAbnormalities.GetValue()) return false;
                    if (c.ECGPathlogicalQWave != dePathologicalQWave.GetValue()) return false;
                    if (c.Rhythum != deRhythm.GetValue()) return false;
                    if (c.Mechanism != deMechanism.GetValue()) return false;
                    if (c.CulpritVessel != deCulpritVessel.GetValue()) return false;
                    if (c.Smoking != deSmoking.GetValue()) return false;
                    if (c.InitialObs != deInitialObs.GetValue()) return false;
                    if (c.OxygenSat != deOxygenSat.GetValue()) return false;
                    if (c.OxygenTherapy != deOxygenTherapy.GetValue()) return false;
                    if (c.RespiratoryRate != deRespiratoryRate.GetValue()) return false;
                    if (c.SystolicBP != deSystolicBP.GetValue()) return false;
                    if (c.DiastolicBP != deDiastolicBP.GetValue()) return false;
                    if (c.HeartRate != deHeartRate.GetValue()) return false;
                    if (c.Temperature != deTemperature.GetValue()) return false;
                    if (c.Alert != deAlert.GetValue()) return false;
                    if (c.KillipClass != deKillipClass.GetValue()) return false;
                    if (c.CardiacArrest != deCardiacArrest.GetValue()) return false;
                    if (c.ACSTreatmentInED != deACSTreatmentInED.GetValue()) return false;
                    if (c.InsufficientInfo != deInsufficientInfo.GetValue()) return false;
                    if (c.Spontaneous != deSpontaneous.GetValue()) return false;
                    if (c.Procedural != deProcedural.GetValue()) return false;
                    if (c.Secondary != deSecondary.GetValue()) return false;
                    if (c.SymptomsOfIschaemia != deSymptomsOfIschaemia.GetValue()) return false;
                    if (c.SignsOfIschaemia != deSignsOfIschaemia.GetValue()) return false;
                    if (c.SupplyDemandImbalance != deSupplyDemandImbalance.GetValue()) return false;
                    if (c.PrimaryMechanism != dePrimaryMechanism.GetValue()) return false;
                    if (c.SuspectedCAD != deSuspectedCAD.GetValue()) return false;
                    if (c.Cardiac != deCardiac.GetValue()) return false;
                    if (c.Systemic != deSystemic.GetValue()) return false;
                }
            }
            else
            {
                if (!c.Adjudication2Complete)
                {
                    if ("" != deInsufficientInfo.GetValue()) return false;
                    if ("" != deSpontaneous.GetValue()) return false;
                    if ("" != deProcedural.GetValue()) return false;
                    if ("" != deSecondary.GetValue()) return false;
                    if ("" != deSymptomsOfIschaemia.GetValue()) return false;
                    if ("" != deSignsOfIschaemia.GetValue()) return false;
                    if ("" != deSupplyDemandImbalance.GetValue()) return false;
                    if ("" != dePrimaryMechanism.GetValue()) return false;
                    if ("" != deSuspectedCAD.GetValue()) return false;
                    if ("" != deCardiac.GetValue()) return false;
                    if ("" != deSystemic.GetValue()) return false;
                }
                else
                { 
                    if (c.InsufficientInfo2 != deInsufficientInfo.GetValue()) return false;
                    if (c.Spontaneous2 != deSpontaneous.GetValue()) return false;
                    if (c.Procedural2 != deProcedural.GetValue()) return false;
                    if (c.Secondary2 != deSecondary.GetValue()) return false;
                    if (c.SymptomsOfIschaemia2 != deSymptomsOfIschaemia.GetValue()) return false;
                    if (c.SignsOfIschaemia2 != deSignsOfIschaemia.GetValue()) return false;
                    if (c.SupplyDemandImbalance2 != deSupplyDemandImbalance.GetValue()) return false;
                    if (c.PrimaryMechanism2 != dePrimaryMechanism.GetValue()) return false;
                    if (c.SuspectedCAD2 != deSuspectedCAD.GetValue()) return false;
                    if (c.Cardiac2 != deCardiac.GetValue()) return false;
                    if (c.Systemic2 != deSystemic.GetValue()) return false;
                }
            }

            return true;
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

        private void comboBox12LeadECG_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideComponents();

        }

        private void comboBoxECGNormal_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideComponents();
        }

        private void comboBoxSupplyDemandImbalance_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideComponents();
        }

        private void comboBoxInsufficientInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideComponents();
        }

        private void comboBoxSpontaneous_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideComponents();
        }

        private void comboBoxProcedural_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideComponents();
        }

        private void comboBoxCardiac_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideComponents();
        }

        private void comboBoxSystemic_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideComponents();
        }
    }
}