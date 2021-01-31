using Backend.Model;
using Backend.Model.PerformingExamination;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository.EquipmentInExaminationRepository.MySqlEquipmentInExaminationRepository
{
    public class MySqlEquipmentInExaminationRepository : IEquipmentInExaminationRepository
    {
        private readonly MyDbContext _context;
        public MySqlEquipmentInExaminationRepository(MyDbContext context)
        {
            _context = context;
        }
        public EquipmentInExamination AddEquipmentInExamination(EquipmentInExamination equipmentInExamination)
        {
            _context.EquipmentInExamination.Add(equipmentInExamination);
            _context.SaveChanges();
            return equipmentInExamination;
        }

        public List<EquipmentInExamination> GetEquipmentInExaminationByExaminationId(int examinationId)
        {
            var equipmentInExamination = _context.EquipmentInExamination.Where(x => x.ExaminationId == examinationId).ToList();
            return (List<EquipmentInExamination>)equipmentInExamination;
        }
    }
}
