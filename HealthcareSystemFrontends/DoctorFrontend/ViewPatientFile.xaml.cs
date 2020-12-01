using Controller.ExaminationAndPatientCard;
using Model.Secretary;
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ViewPatientFile.xaml
    /// </summary>
    public partial class ViewPatientFile : Window
    {
        private static PatientCardController ap = new PatientCardController();
        public ViewPatientFile(string txt)
        {
            InitializeComponent();
         
            txtPatient.Text = txt;
       
            PatientCard pc = new PatientCard();

            string[] ss = txt.Split(' ');
            pc = ap.ViewPatientCard(ss[ss.Length - 1]);
            string mh = pc.MedicalHistory;
            if (mh.Equals("")) { }
           
            else
            {             
                string[] pp = mh.Split(';');    

                    List<MedicalHistoryDTO> mds = new List<MedicalHistoryDTO>();
                    for (int i = 0; i < pp.Length-1; i++)
                    {
                        MedicalHistoryDTO md = new MedicalHistoryDTO();
                        string[] p1 = pp[i].Split(':');
                        md.date = p1[0];
                        md.disease = p1[1];
                        md.therapy = p1[2];

                        mds.Add(md);
                    }

                    DataGrid1.ItemsSource = mds;
    
            }
        }
    }
}
