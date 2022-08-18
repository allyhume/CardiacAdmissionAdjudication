using System.Data;
using System.Text.RegularExpressions;

namespace CardiacAdmissionAdjudication
{
    public partial class Form1 : Form
    {
        private AdjudicationCases adjudicationCases;
        private int currentCase = 0;

        private OpenFileDialog openFileDialog;

        public Form1(AdjudicationCases cases)
        {
            this.adjudicationCases = cases;

             openFileDialog = new OpenFileDialog()
             {
                 FileName = "Select an annotation cases file",
                 Filter = "Annotation cases (cases_*_0.txt)|cases_*_0.txt",
                 Title = "Open annotation cases file"
             };

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ECG

            Utility.SetComboBoxYesNoNull(comboBox12LeadECG);
            Utility.SetComboBox(comboBoxECGNormal, new ComboItem[] {
                new ComboItem{ ID = "Normal",  Text = "Normal" },
                new ComboItem{ ID = "Abnormal",   Text = "Abnormal" },
                new ComboItem { ID = "NULL", Text = "" }
            });
            Utility.SetComboBoxYesNoNull(comboBoxMyocardialIschaemia);
            Utility.SetComboBoxYesNoNull(comboBoxSubsequentIschaemia);
            Utility.SetComboBoxYesNoNull(comboBoxSTElevation);
            Utility.SetComboBoxYesNoNull(comboBoxSTDepression);
            Utility.SetComboBoxYesNoNull(comboBoxTWaveInversion);
            Utility.SetComboBox(comboBoxQRSAbnormalities, new ComboItem[] {
                new ComboItem{ ID = "None",  Text = "None" },
                new ComboItem{ ID = "LBBB (new)",   Text = "LBBB (new)" },
                new ComboItem{ ID = "LBBB (old)",   Text = "LBBB (old)" },
                new ComboItem{ ID = "RBBB (new)",   Text = "RBBB (new)" },
                new ComboItem{ ID = "RBBB (old)",   Text = "RBBB (old)" },
                new ComboItem{ ID = "LVH",          Text = "LVH" },
                new ComboItem { ID = "NULL",        Text = "" }
            });
            Utility.SetComboBoxYesNoNull(comboBoxPathologicalQWave);
            Utility.SetComboBox(comboBoxRhythm, new ComboItem[] {
                new ComboItem{ ID = "SR",                 Text = "SR" },
                new ComboItem{ ID = "AF",                 Text = "AF" },
                new ComboItem{ ID = "Flutter",            Text = "Flutter" },
                new ComboItem{ ID = "SVT",                Text = "SVT" },
                new ComboItem{ ID = "VT",                 Text = "VT" },
                new ComboItem{ ID = "VF",                 Text = "VF" },
                new ComboItem{ ID = "Paced rhythm",       Text = "Paced rhythm" },
                new ComboItem{ ID = "Advanced AV block",  Text = "Advanced AV block" },
                new ComboItem{ ID = "Unknown",            Text = "Unknown" },
                new ComboItem{ ID = "NULL",               Text = "" }
                });

            // Angiogram Classification

            Utility.SetComboBox(comboBoxMechanism, new ComboItem[] {
                new ComboItem{ ID = "severe stenosis",                    Text = "severe stenosis" },
                new ComboItem{ ID = "plaque rupture with thrombosis",     Text = "plaque rupture with thrombosis" },
                new ComboItem{ ID = "moderate unobstructive disease",     Text = "moderate unobstructive disease" },
                new ComboItem{ ID = "plaque disease only",                Text = "plaque disease only" },
                new ComboItem{ ID = "coronary dissection",                Text = "coronary dissection" },
                new ComboItem{ ID = "in-stent restenosis",                Text = "in-stent restenosis" },
                new ComboItem{ ID = "stent thrombosis",                   Text = "stent thrombosis" },
                new ComboItem{ ID = "vasospasm",                          Text = "vasospasm" },
                new ComboItem{ ID = "embolism",                           Text = "embolism" },
                new ComboItem{ ID = "CTO related ischaemia",              Text = "CTO related ischaemia" },
                new ComboItem{ ID = "normal coronary arteries",           Text = "normal coronary arteries" },
                new ComboItem{ ID = "bypass graft disease",               Text = "bypass graft disease" },
                new ComboItem{ ID = "unknown (for angiographic review)",  Text = "unknown (for angiographic review)" },
                new ComboItem{ ID = "NULL",                               Text = "" }
                });

            // LMS | LAD | Dx | Cx | OM | RCA | PLV | PDA | intermediate | multiple | bypass graft | none
            Utility.SetComboBox(comboBoxCulpritVessel, new ComboItem[] {
                new ComboItem{ ID = "LMS",             Text = "LMS" },
                new ComboItem{ ID = "LAD",             Text = "LAD" },
                new ComboItem{ ID = "Dx",              Text = "Dx" },
                new ComboItem{ ID = "Cx",              Text = "Cx" },
                new ComboItem{ ID = "OM",              Text = "OM" },
                new ComboItem{ ID = "RCA",             Text = "RCA" },
                new ComboItem{ ID = "PLV",             Text = "PLV" },
                new ComboItem{ ID = "PDA",             Text = "PDA" },
                new ComboItem{ ID = "intermediate",    Text = "intermediate" },
                new ComboItem{ ID = "multiple",        Text = "multiple" },
                new ComboItem{ ID = "bypass graft",    Text = "bypass graft" },
                new ComboItem{ ID = "none",            Text = "none" },
                new ComboItem{ ID = "NULL",           Text = "" }
                });

            // Smoking 
            Utility.SetComboBox(comboBoxSmoking, new ComboItem[] {
                new ComboItem{ ID = "Current", Text = "Current" },
                new ComboItem{ ID = "Ex-",     Text = "Ex-" },
                new ComboItem{ ID = "Never",   Text = "Never" },
                new ComboItem{ ID = "Unknown", Text = "Unknown" },
                new ComboItem{ ID = "NULL",   Text = "" }
                });

            // Final adjudication
            Utility.SetComboBoxYesNoNull(comboBoxInsufficientInfo);
            Utility.SetComboBox(comboBoxSpontaneous, new ComboItem[] {
                new ComboItem{ ID = "No",      Text = "No" },
                new ComboItem{ ID = "NSTEMI",  Text = "NSTEMI" },
                new ComboItem{ ID = "STEMI",   Text = "STEMI" },
                new ComboItem{ ID = "NULL",   Text = "" }
                });
            Utility.SetComboBox(comboBoxProcedural, new ComboItem[] {
                new ComboItem{ ID = "No",       Text = "No" },
                new ComboItem{ ID = "Type 4a",  Text = "Type 4a" },
                new ComboItem{ ID = "Type 4b",  Text = "Type 4b" },
                new ComboItem{ ID = "Type 4c",  Text = "Type 4c" },
                new ComboItem{ ID = "Type 5",   Text = "Type 5" },
                new ComboItem{ ID = "NULL",   Text = "" }
                });
            Utility.SetComboBox(comboBoxSecondary, new ComboItem[] {
                new ComboItem{ ID = "No",                         Text = "No" },
                new ComboItem{ ID = "Type 2",                     Text = "Type 2" },
                new ComboItem{ ID = "Acute Myocardial Injury",    Text = "Acute Myocardial Injury" },
                new ComboItem{ ID = "Chronic Myocardial Injury",  Text = "Chronic Myocardial Injury" },
                new ComboItem{ ID = "NULL",                       Text = "" }
                });
            Utility.SetComboBoxYesNoNull(comboBoxSymptomsOfIschaemia);
            Utility.SetComboBoxYesNoNull(comboBoxSignsOfIschaemia);
            Utility.SetComboBoxYesNoNull(comboBoxSupplyDemandImbalance);
            Utility.SetComboBox(comboBoxPrimaryMechanism, new ComboItem[] {
                new ComboItem{ ID = "tachycardia",             Text = "tachycardia" },
                new ComboItem{ ID = "hypotension",             Text = "hypotension" },
                new ComboItem{ ID = "hypoxaemia",              Text = "hypoxaemia" },
                new ComboItem{ ID = "anaemia",                 Text = "anaemia" },
                new ComboItem{ ID = "malignant hypertension",  Text = "malignant hypertension" },
                new ComboItem{ ID = "LVH",                     Text = "LVH" },
                new ComboItem{ ID = "coronary embolism",       Text = "coronary embolism" },
                new ComboItem{ ID = "coronary vasospasm",      Text = "coronary vasospasm" },
                new ComboItem{ ID = "NULL",                   Text = "" }
                });
            Utility.SetComboBox(comboBoxSuspectedCAD, new ComboItem[] {
                new ComboItem{ ID = "known",             Text = "known" },
                new ComboItem{ ID = "high-probability",  Text = "high-probability" },
                new ComboItem{ ID = "low-probability",   Text = "low-probability" },
                new ComboItem{ ID = "NULL",                   Text = "" }
                });
            Utility.SetComboBox(comboBoxCardiac, new ComboItem[] {
                new ComboItem{ ID = "No",                         Text = "No" },
                new ComboItem{ ID = "myopericarditis",            Text = "myopericarditis" },
                new ComboItem{ ID = "acute heart failure",        Text = "acute heart failure" },
                new ComboItem{ ID = "chronic heart failure",      Text = "chronic heart failure" },
                new ComboItem{ ID = "hypertensive heart disease", Text = "hypertensive heart disease" },
                new ComboItem{ ID = "cardiomyopathy other",       Text = "cardiomyopathy other" },
                new ComboItem{ ID = "valvular heart disease",     Text = "valvular heart disease" },
                new ComboItem{ ID = "tachyarrhythmia",            Text = "tachyarrhythmia" },
                new ComboItem{ ID = "recent MI",                  Text = "recent MI" },
                new ComboItem{ ID = "acute aortic dissection",    Text = "acute aortic dissection" },
                new ComboItem{ ID = "takotsubo cardiomyopathy",   Text = "takotsubo cardiomyopathy" },
                new ComboItem{ ID = "other",                      Text = "other" },
                new ComboItem{ ID = "NULL",                      Text = "" }
                });
            Utility.SetComboBox(comboBoxSystemic, new ComboItem[] {
                new ComboItem{ ID = "No",                         Text = "No" },
                new ComboItem{ ID = "other",                      Text = "other" },
                new ComboItem{ ID = "acute kidney injury",        Text = "acute kidney injury" },
                new ComboItem{ ID = "chronic kidney disease",     Text = "chronic kidney disease" },
                new ComboItem{ ID = "pulmonary embolism",         Text = "pulmonary embolism" },
                new ComboItem{ ID = "sepsis",                     Text = "sepsis" },
                new ComboItem{ ID = "GI",                         Text = "GI" },
                new ComboItem{ ID = "bleed",                      Text = "bleed" },
                new ComboItem{ ID = "COPD",                       Text = "COPD" },
                new ComboItem{ ID = "other",                      Text = "other" },
                new ComboItem{ ID = "NULL",                      Text = "" }
                });

            // Physiological parameters
            Utility.SetComboBoxYesNoNull(comboBoxInitialObs);
            Utility.SetComboBox(comboBoxOxygenTherapy, new ComboItem[] {
                new ComboItem{ ID = "Yes",           Text = "Yes" },
                new ComboItem{ ID = "No",            Text = "No" },
                new ComboItem{ ID = "Uknown",        Text = "Unknown" },
                new ComboItem{ ID = "NULL",         Text = "" }
                });
            Utility.SetComboBox(comboBoxAlert, new ComboItem[] {
                new ComboItem{ ID = "Yes",           Text = "Yes" },
                new ComboItem{ ID = "No",            Text = "No" },
                new ComboItem{ ID = "Uknown",        Text = "Unknown" },
                new ComboItem{ ID = "NULL",         Text = "" }
                });
            Utility.SetComboBox(comboBoxKillipClass, new ComboItem[] {
                new ComboItem{ ID = "I",          Text = "I" },
                new ComboItem{ ID = "II",         Text = "II" },
                new ComboItem{ ID = "III",        Text = "III" },
                new ComboItem{ ID = "IV",         Text = "IV" },
                new ComboItem{ ID = "Uknown",     Text = "Unknown" },
                new ComboItem{ ID = "NULL",      Text = "" }
                });
            Utility.SetComboBoxYesNoNull(comboBoxCardiacArrest);
            Utility.SetComboBoxYesNoNull(comboBoxACSTreatmentInED);


            // Suspected ACS - not sure what to put here - think it is a yes or no
            Utility.SetComboBoxYesNoNull(comboBoxSuspectedACS);

            SetCurrentCase();
            DisplayCurrentCase();
        }

        private void ShowHideComponents()
        {
            if (comboBoxSecondary.Text == "Type 2")
            {
                labelSymptomsOfIschaemia.Show();
                comboBoxSymptomsOfIschaemia.Show();

                labelSignsOfIschaemia.Show();
                comboBoxSignsOfIschaemia.Show();

                labelSupplyDemandImbalance.Show();
                comboBoxSupplyDemandImbalance.Show();

                if (comboBoxSupplyDemandImbalance.SelectedValue.ToString() == "Yes")
                {
                    labelPrimaryMechanism.Show();
                    comboBoxPrimaryMechanism.Show();
                }
                else
                {
                    labelPrimaryMechanism.Hide();
                    comboBoxPrimaryMechanism.Hide();
                }
            }
            else
            {
                labelSymptomsOfIschaemia.Hide();
                comboBoxSymptomsOfIschaemia.Hide();

                labelSignsOfIschaemia.Hide();
                comboBoxSignsOfIschaemia.Hide();

                labelSupplyDemandImbalance.Hide();
                comboBoxSupplyDemandImbalance.Hide();

                labelPrimaryMechanism.Hide();
                comboBoxPrimaryMechanism.Hide();
            }

            if (comboBoxSecondary.Text == "Type 2"  ||
                comboBoxSecondary.Text == "Acute Myocardial Injury"  ||
                comboBoxSecondary.Text == "Chronic Myocardial Injury")
            {
                labelSuspectedCAD.Show();
                comboBoxSuspectedCAD.Show();

                labelCardiac.Show();
                comboBoxCardiac.Show();

                labelSystemic.Show();
                comboBoxSystemic.Show();
            }
            else
            {
                labelSuspectedCAD.Hide();
                comboBoxSuspectedCAD.Hide();

                labelCardiac.Hide();
                comboBoxCardiac.Hide();

                labelSystemic.Hide();
                comboBoxSystemic.Hide();
            }

            if (comboBoxInitialObs.Text == "Yes")
            {
                labelOxygenSat.Show();
                textBoxOxygenSat.Show();
                labelOxygenTherapy.Show();
                comboBoxOxygenTherapy.Show();
                labelRespiratoryRate.Show();
                textBoxRespiratoryRate.Show();
                labelSystolicBP.Show();
                textBoxSystolicBP.Show();
                labelDiastolicBP.Show();
                textBoxDiastolicBP.Show();
                labelHeartRate.Show();  
                textBoxHeartRate.Show();
                labelTemperature.Show();
                textBoxTemperature.Show();
                labelAlert.Show();
                comboBoxAlert.Show();
                labelKillipClass.Show();
                comboBoxKillipClass.Show();
                labelCardiacArrest.Show();
                comboBoxCardiacArrest.Show();
                labelACSTreatmentInED.Show();
                comboBoxACSTreatmentInED.Show();
            }
            else
            {
                labelOxygenSat.Hide();
                textBoxOxygenSat.Hide();
                labelOxygenTherapy.Hide();
                comboBoxOxygenTherapy.Hide();
                labelRespiratoryRate.Hide();
                textBoxRespiratoryRate.Hide();
                labelSystolicBP.Hide();
                textBoxSystolicBP.Hide();
                labelDiastolicBP.Hide();
                textBoxDiastolicBP.Hide();
                labelHeartRate.Hide();
                textBoxHeartRate.Hide();
                labelTemperature.Hide();
                textBoxTemperature.Hide();
                labelAlert.Hide();
                comboBoxAlert.Hide();
                labelKillipClass.Hide();
                comboBoxKillipClass.Hide();
                labelCardiacArrest.Hide();
                comboBoxCardiacArrest.Hide();
                labelACSTreatmentInED.Hide();
                comboBoxACSTreatmentInED.Hide();
            }

            if (comboBoxMyocardialIschaemia.Text == "No")
            {
                comboBoxSubsequentIschaemia.Show();
                labelSubsequentIschaemia.Show();
            }
            else
            {
                comboBoxSubsequentIschaemia.Hide();
                labelSubsequentIschaemia.Hide();
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

                if (this.adjudicationCases.IsFirstAdjudicator)
                {
                    if (c.Adjudication1Complete)
                    {
                        textBoxDiastolicBP.Text = c.DiastolicBP;
                        comboBoxSpontaneous.SelectedValue = c.Spontaneous;
                    }
                    else
                    {
                        textBoxDiastolicBP.Text = "";
                        comboBoxSpontaneous.SelectedValue = "NULL";
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
            comboBoxSuspectedACS.SelectedValue = "NULL";
            textBoxAge.Text = "";
            textBoxSex.Text = "";

            DataTable troponinTestsData = new DataTable("Troponin Tests");
            troponinTestsData.Columns.Add("Time from presentation");
            troponinTestsData.Columns.Add("Result");
            dataGridViewTroponinTests.DataSource = troponinTestsData;

            // Annotation 1 data
            textBoxDiastolicBP.Text = "";
            comboBoxSpontaneous.SelectedValue = "NULL";

            // Progress label
            labelProgress.Text = "";

            buttonNext.Enabled = false; 
            buttonPrevious.Enabled = false;
        }

        private Boolean StoreCurrentCase()
        {
            AdjudicationCase c = this.adjudicationCases.cases[currentCase];

            if (this.adjudicationCases.IsFirstAdjudicator)
            {
                c.Adjudicator1 = Environment.UserName;
                c.DiastolicBP = textBoxDiastolicBP.Text;
                c.Spontaneous = comboBoxSpontaneous.Text;
                c.Adjudication1Complete = true;
            }
            else
            {
                c.Adjudicator2 = Environment.UserName;
                c.Spontaneous2 = comboBoxSpontaneous.Text;
                c.Adjudication2Complete = true;
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