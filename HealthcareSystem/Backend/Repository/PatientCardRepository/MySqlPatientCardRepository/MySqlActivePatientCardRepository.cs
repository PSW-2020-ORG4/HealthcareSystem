using Backend.Model;
using Backend.Model.Users;
using System.Linq;

namespace Backend.Repository
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
            PatientCard patientCard = GetPatientCardByJmbg(patientJmbg);
            _context.Remove(patientCard);
            _context.SaveChanges();
        }

        public PatientCard GetPatientCardByJmbg(string jmbg)
        {
            return _context.PatientCards.Where(patientCard => patientCard.PatientJmbg == jmbg).FirstOrDefault();
        }

        public void AddPatientCard(PatientCard patientCard)
        {
            _context.PatientCards.Add(patientCard);
            _context.SaveChanges();
        }
        public void UpdatePatientCard(PatientCard patientCard)
        {
            _context.PatientCards.Update(patientCard);
            _context.SaveChanges();
        }
        public bool CheckIfPatientCardExists(int patientCardId)
        {
            if (_context.PatientCards.Find(patientCardId) == null)
                return false;

            return true;
        }
    }
}
