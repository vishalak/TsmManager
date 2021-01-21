﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TsmManager
{
    public partial class Form : System.Windows.Forms.Form
    {
        private Manager manager;
        private bool isPaused;
        public Form()
        {
            InitializeComponent();
            manager = new Manager();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (!manager.Open())
            {
                MessageBox.Show("An error occurred while opening TransModeler or the specified project. Please see the log file for more details.");
                manager.Close();
                return;
            }
            runSimulationButton.Enabled = true;
            openButton.Enabled = false;
        }

        private void runSimulationButton_Click(object sender, EventArgs e)
        {
            manager.Start();
            pauseButton.Enabled = true;
            stopButton.Enabled = true;
            stepModeButton.Enabled = true;
            stepForwardButton.Enabled = true;
            speedUpButton.Enabled = true;
            slowDownButton.Enabled = true;
            runSimulationButton.Enabled = false;
            label1.Enabled = true;
            stepSizeTextBox.Enabled = true;
            MessageBox.Show("Simulation started!");
        }

        private void changeSettingsButton_Click(object sender, EventArgs e)
        {
            manager.ChangeSettings();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            manager.Stop();
            MessageBox.Show("Simulation stopped!");
            pauseButton.Enabled = false;
            stopButton.Enabled = false;
            stepModeButton.Enabled = false;
            stepForwardButton.Enabled = false;
            speedUpButton.Enabled = false;
            slowDownButton.Enabled = false;
            runSimulationButton.Enabled = true;
            label1.Enabled = false;
            stepSizeTextBox.Enabled = false;
            pauseButton.Text = "Pause";
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            manager.Stop();
            manager.Close();
            Close();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            var status = string.Empty;
            if (!isPaused)
            {
                isPaused = true;
                pauseButton.Text = "Resume";
                status = manager.Pause("True");
            }
            else
            {
                isPaused = false;
                pauseButton.Text = "Pause";
                status = manager.Pause("False");
            }
            MessageBox.Show($"The status of pause simulation is {status}");
        }

        private void stepModeButton_Click(object sender, EventArgs e)
        {
            var status = manager.EnterStepMode();
            MessageBox.Show($"The staus of enter step mode is {status}");
        }

        private void stepForwardButton_Click(object sender, EventArgs e)
        {
            var stepSize = int.Parse(stepSizeTextBox.Text);
            //stepSize is the size in seconds of step for Step Mode simulation, default value is 1 second
            manager.StepForward(stepSize);
        }

        private void speedUpButton_Click(object sender, EventArgs e)
        {
            manager.SpeedUp();
        }

        private void slowDownButton_Click(object sender, EventArgs e)
        {
            manager.SlowDown();
        }
    }
}
