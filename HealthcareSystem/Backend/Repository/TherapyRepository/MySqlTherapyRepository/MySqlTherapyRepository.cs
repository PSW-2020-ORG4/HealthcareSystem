using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Model.PerformingExamination;

namespace Backend.Repository.TherapyRepository.MySqlTherapyRepository
{
    class MySqlTherapyRepository : ITherapyRepository
    {
        private readonly MyDbContext _context;
        public MySqlTherapyRepository(MyDbContext context)
        {
            _context = context;
        }

        public void AddTherapy(Therapy therapy)
        {
            _context.Therapies.Add(therapy);
            _context.SaveChanges();
        }

        public void DeleteDrugTherapies(int idDrug)
        {
            List<Therapy> therapies = GetTherapyByDrug(idDrug);
            _context.Remove(therapies);
            _context.SaveChanges();
        }

        public void DeletePatientTherapies(string patientJmbg)
        {
            List<Therapy> therapies = GetTherapyByPatient(patientJmbg);
            _context.Remove(therapies);
            _context.SaveChanges();
        }

        public void DeleteTherapy(int idTherapy)
        {
            Therapy therapy = GetTherapyById(idTherapy);
            _context.Remove(therapy);
            _context.SaveChanges();
        }

        public List<Therapy> GetActiveTherapyByPatient(string patientJmbg)
        {
            throw new NotImplementedException();
        }

        public List<Therapy> GetTherapyByDrug(int idDrug)
        {
            return _context.Therapies.Where(t => t.IdDrug == idDrug).ToList();
        }

        public Therapy GetTherapyById(int id)
        {
            return _context.Therapies.Find(id);
        }

        public List<Therapy> GetTherapyByPatient(string patientJmbg)
        {
            return _context.Therapies.Where(t => t.Examination.PatientCard.PatientJmbg == patientJmbg).ToList();
        }

        public List<Therapy> GetTherapyForNextSevenDaysByPatient(string patientJmbg)
        {
            throw new NotImplementedException();
        }

        public void UpdateTherapy(Therapy therapy)
        {
            _context.Update(therapy);
            _context.SaveChanges();
        }
    }
}
