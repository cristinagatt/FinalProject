using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Collaborative_Proj
{
    public partial class Form1 : Form
    {
        //Global Vars : Page 2
        private int date_col, encounter_type_col, scale_col, event1_col, direct_col;

        //Global Vars: Page 3
        private string waveFilter, encounter_typeFilter, eventFilter, satisfactionFilter, importanceFilter;



        private int Index;
        //List of panels
        readonly List<Panel> ListPanel = new List<Panel>();

        //Form 
        public Form1()
        {
            InitializeComponent();
        }

        //Form loader
        private void Form1_Load(object sender, EventArgs e)
        {
            ListPanel.Add(panel1);
            ListPanel.Add(panel2);
            ListPanel.Add(panel3);
        }

        //Panels (1-3)
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        //New Project Button: Page 1
        private void NewProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV files | *.csv";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Import CSV ";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string strfilename = dialog.FileName;
                MessageBox.Show(strfilename);
                //   GetNoColumns(strfilename);
            }


        }

        //sajf: Get number of columns in CSV file
        private void GetNoColumns(string strfilename)
        {
            StreamReader reader;
            using (reader = new StreamReader(strfilename)) ;
            {
                var rowA = new List<string>();
                //while (!reader.ReadLine())
                {

                }
            }
        }

        //Exit Function
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Reading the Numeric up and down data, and saving the data in seprate variables. 
        private void numericUpDownScale_ValueChanged(object sender, EventArgs e)
        {
            scale_col = (int)numericUpDownScale.Value;
        }

        private void numericUpDownEvent_ValueChanged(object sender, EventArgs e)
        {
            event1_col = (int)numericUpDownEvent.Value;
        }

        private void numericUpDownDirect_ValueChanged(object sender, EventArgs e)
        {
            direct_col = (int)numericUpDownDirect.Value;
        }

        private void numericUpDownDate_ValueChanged(object sender, EventArgs e)
        {
            date_col = (int)numericUpDownDate.Value;
        }

        private void numericUpDownEncounter_ValueChanged(object sender, EventArgs e)
        {
            encounter_type_col = (int)numericUpDownEncounter.Value;
        }

        //Next Button Page 1
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Index < ListPanel.Count - 1) ListPanel[++Index].BringToFront();
        }

        //Next Button Page 2
        private void NextButton_Click(object sender, EventArgs e)
        {
            if (Index < ListPanel.Count - 1) ListPanel[++Index].BringToFront();
        }

        //Plot Button: Sends Data (5 variables and 5 filters to Python script)
        private void buttonPlot_Click(object sender, EventArgs e)
        {

            // full path of python interpreter 
            string python = @"C:\Users\matth\PycharmProjects\untitled\venv\Scripts\python.exe";

            // python app to call 
            string myPythonApp = "cristina.py";



            // Create new process start info 
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;

            // start python app with 3 arguments  
            // 1st arguments is pointer to itself,  
            // 2nd and 3rd are actual arguments we want to send 
            myProcessStartInfo.Arguments = myPythonApp + " " + (date_col) + " " + (encounter_type_col) + " " + (scale_col)
                                           + " " + (event1_col) + " " + (direct_col) + " " + (waveFilter) + " " + (encounter_typeFilter)
                                           + " " + (eventFilter) + " " + (satisfactionFilter) + " " + (importanceFilter);

            Process myProcess = new Process();
            // assign start information to the process 
            myProcess.StartInfo = myProcessStartInfo;

            //Console.WriteLine("Calling Python script with arguments {0} and {1}", x, y);
            // start the process 
            myProcess.Start();

            // Read the standard output of the app we called.  
            // in order to avoid deadlock we will read output first 
            // and then wait for process terminate: 
            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadLine();

            /*if you need to read multiple lines, you might use: 
                string myString = myStreamReader.ReadToEnd() */

            // wait exit signal from the app we called and then close it. 
            myProcess.WaitForExit();
            myProcess.Close();

            Console.WriteLine("Value received from script: " + myString);
            MessageBox.Show(myString);

        }


        //Combo boxes page 3
        private void comboBoxWave_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxWave.SelectedItem == null)
                waveFilter = null;

            waveFilter = comboBoxWave.SelectedItem.ToString();
        }

        private void comboBoxEncounter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEncounter.SelectedItem == null)
                encounter_typeFilter = null;

            encounter_typeFilter = comboBoxEncounter.SelectedItem.ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem == null)
                eventFilter = null;

            eventFilter = comboBox3.SelectedItem.ToString();
        }

        private void comboBoxSatifsaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSatifsaction.SelectedItem == null)
                satisfactionFilter = null;

            satisfactionFilter = comboBoxSatifsaction.SelectedItem.ToString();
        }

        private void comboBoxImportance_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxImportance.SelectedItem == null)
                importanceFilter = null;

            importanceFilter = comboBoxImportance.SelectedItem.ToString();
        }

        //Labels
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

    }
}

