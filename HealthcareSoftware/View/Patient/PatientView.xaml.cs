﻿using HealthcareData.Models;
using HealthcareSoftware.ViewModel.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthcareSoftware.View.Patient
{
    /// <summary>
    /// Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView : Window
    {
        public PatientView(tblPatient patient)
        {
            InitializeComponent();
            this.DataContext = new PatientViewModel(this, patient);
        }
    }
}
