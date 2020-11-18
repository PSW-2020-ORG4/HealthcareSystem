using Backend.Model;
using Model.Doctor;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MySqlActivePatientCardRepository : IActivePatientCardRepository
    {
        private readonly MyDbContext _context;

        public MySqlActivePatientCardRepository(MyDbContext context)
        {
            _context = context;
        }
        public void DeletePatientCard(string patientJmbg)
        {
            PatientCard patientCard = GetPatientCard(patientJmbg);
            _context.Remove(patientCard);
            _context.SaveChanges();
        }

        public PatientCard GetPatientCard(string jmbg)
        {
            return _context.PatientCards.Find(jmbg);
        }

        public void AddPatientCard(PatientCard patientCard)
        {
            _context.PatientCards.Add(patientCard);
            _context.SaveChanges();
        }

        public void SaveExaminationInPatientCard(Examination examination)
        {
            throw new NotImplementedException();
        }

        public void SetPatientCard(PatientCard patientCard)
        {
            _context.PatientCards.Update(patientCard);
            _context.SaveChanges();
        }
    }
}
