﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daily_Digital_Task_Tracker
{
    public partial class Form1 : Form
    {



        //gets date
        static DateTime currentTime = DateTime.Now;
        public static int month = currentTime.Month;
        public static int year = currentTime.Year;
        public Form1()
        {
            InitializeComponent();
            CreateCSV();
            getIni();
            dateDisplay();
        }

        //Creates file in bin/debug
        private void CreateCSV()
        {
            try
            {
                StreamWriter sw = new StreamWriter(File.Open("config.ini", System.IO.FileMode.CreateNew));
                Console.WriteLine("File created");
                sw.Close();
                File.AppendAllText("config.ini", "[SECTION]" + "\n");
                File.AppendAllText("config.ini", "key = light" + "\n");
            }
            catch (IOException)
            {
                Console.WriteLine("File already exists");
            }
            //Makes sure that the files exist if not they are made.
            try
            {
                StreamWriter sw = new StreamWriter(File.Open("Events.csv", System.IO.FileMode.CreateNew));
                Console.WriteLine("File created");
                sw.Close();
            }
            catch (IOException)
            {
                Console.WriteLine("File already exists");
            }

            try
            {
                StreamWriter sw = new StreamWriter(File.Open("Temp.csv", System.IO.FileMode.CreateNew));
                Console.WriteLine("File created");
                sw.Close();
            }
            catch (IOException)
            {
                Console.WriteLine("File already exists");
            }
        }

        private void dateDisplay()
        {
            /*
             * Sets variables to current time
             */

            //changes month label to selected month
            String monthText = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            month_year_lbl.Text = monthText +" - "+ year;

            //GetScaledBounds the first day of the moth
            DateTime monthStart = new DateTime(year, month, 1);

            //Gets the total days in the month
            int days = DateTime.DaysInMonth(year, month);

            //Converts monthstart to an ineger
            int daysInWeek = Convert.ToInt32(monthStart.DayOfWeek.ToString("d"));
            
            /*
             * Loops for user control
             */

            //Blank user control for last months
            for(int i = 1;i < daysInWeek; i++)
            {
                EmptyUserControl euc = new EmptyUserControl();
                month_container.Controls.Add(euc);
            }

            //This months user control
            for(int i = 1;i <= days; i++)
            {
                DayUserControl duc = new DayUserControl();
                duc.day(i);
                month_container.Controls.Add(duc);
                duc.eventsDisplay(i);
            }
        }

        /*
         * Buttons to switch selected month
         */

        //Used to go to next month
        private void nextBtn_Click(object sender, EventArgs e)
        {
            //Clears the container
            month_container.Controls.Clear();

            //Checks to see if next changes year
            if (month == 12)
            {
                year += 1;
                month = 1;
            }
            else
            {
                month += 1;
            }
            dateDisplay();
        }

        //Used to go tothe previous month
        private void prevBtn_Click(object sender, EventArgs e)
        {
            month_container.Controls.Clear();
            //Checks to see if previous changes year
            if (month == 1)
            {
                year -= 1;
                month = 12;
            }
            else
            {
                month -= 1;
            }
            dateDisplay();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            month_container.Controls.Clear();
            Console.WriteLine("aact");
            CreateCSV();
            dateDisplay();
        }

        private void Themebtn_Click(object sender, EventArgs e)
        {
            Settings set = new Settings();
            Console.WriteLine(set.theme);
            set.readIni();
            if (set.theme == "light")
            {
                set.writeini("SECTION", "key", "dark");
                Console.WriteLine("dark already exists");
            }
            else if (set.theme == "dark")
            {
                set.writeini("SECTION", "key", "light");
                Console.WriteLine("light already exists");
            }
            getIni();
            month_container.Controls.Clear();
            Console.WriteLine("aact");
            CreateCSV();
            dateDisplay();
        }

        //https://learn.microsoft.com/en-us/visualstudio/extensibility/ux-guidelines/color-value-reference-for-visual-studio?view=vs-2022
        private void getIni()
        {
            Settings settings = new Settings();
            settings.readIni();
            //https://coolors.co/palettes/trending
            //https://coolors.co/palette/131316-1c1c21-26262c-2f3037-393a41-4b4c52-5b5c62-6a6b70
            if (settings.theme == "light")
            {
                backColour = "#F8F9FA";
                textColour = "#000000";
                buttonBackColour = "#CED4DA";
                buttonBorderColour = "#ADB5BD";
            }

            if (settings.theme == "dark")
            {
                backColour = "#1C1C21";
                textColour = "#FFF1F1F1";
                buttonBackColour = "#393A41";
                buttonBorderColour = "#4B4C52";
            }


            this.BackColor = ColorTranslator.FromHtml(backColour);

            //Button colours
            this.nextBtn.ForeColor = ColorTranslator.FromHtml(textColour);
            this.nextBtn.BackColor = ColorTranslator.FromHtml(buttonBackColour);
            this.nextBtn.FlatAppearance.BorderColor = ColorTranslator.FromHtml(buttonBorderColour);

            this.prevBtn.ForeColor = ColorTranslator.FromHtml(textColour);
            this.prevBtn.BackColor = ColorTranslator.FromHtml(buttonBackColour);
            this.prevBtn.FlatAppearance.BorderColor = ColorTranslator.FromHtml(buttonBorderColour);

            this.Themebtn.ForeColor = ColorTranslator.FromHtml(textColour);
            this.Themebtn.BackColor = ColorTranslator.FromHtml(buttonBackColour);
            this.Themebtn.FlatAppearance.BorderColor = ColorTranslator.FromHtml(buttonBorderColour);
            //Label colours
            this.month_year_lbl.ForeColor = ColorTranslator.FromHtml(textColour);
            this.monday_lbl.ForeColor = ColorTranslator.FromHtml(textColour);
            this.tuesday_lbl.ForeColor = ColorTranslator.FromHtml(textColour);
            this.wednesday_lbl.ForeColor = ColorTranslator.FromHtml(textColour);
            this.thursday_lbl.ForeColor = ColorTranslator.FromHtml(textColour);
            this.friday_lbl.ForeColor = ColorTranslator.FromHtml(textColour);
            this.saturday_lbl.ForeColor = ColorTranslator.FromHtml(textColour);
            this.sunday_lbl.ForeColor = ColorTranslator.FromHtml(textColour);
        }
        public static String backColour;
        public static String textColour;
        public static String buttonBackColour;
        public static String buttonBorderColour;
    }
}

/* Refrences
 * https://stackoverflow.com/questions/3184121/get-month-name-from-month-number
 * https://github.com/cccu-uk/autorentals-Kieran-Rutter/tree/master/FormAutoRentals
 * 
 * 
 */