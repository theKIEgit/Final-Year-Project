﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        static int month = currentTime.Month;
        static int year = currentTime.Year;
        public Form1()
        {
            InitializeComponent();
            dateDisplay();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dateDisplay();
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
            }
        }

        /*
         * Buttons to switch selected month
         */

        //Used to go to next month
        private void nextBtn_Click(object sender, EventArgs e)
        {
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
    }
}

/* Refrences
 * https://stackoverflow.com/questions/3184121/get-month-name-from-month-number
 * 
 */