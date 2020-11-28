using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.PerformingExamination;

namespace Backend.Repository.ExaminationRepository
{
    public interface ICanceledExaminationRepository
    {
        Examination GetExaminationById(int id);
        void DeleteRoomCanceledExaminations(int numberOfRoom);
        void DeleteDoctorCanceledExaminations(string doctorJmbg);
        void DeletePatientCanceledExaminations(string jmbg);
        List<Examination> GetAllExaminations();
        void DeleteExamination(int id);
        void AddExamination(Examination examination);


    }
}
