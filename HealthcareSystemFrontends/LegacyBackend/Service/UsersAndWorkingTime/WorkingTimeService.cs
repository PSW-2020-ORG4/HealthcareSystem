/***********************************************************************
 * Module:  WorkingTimeService.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Service.WorkingTimeService
 ***********************************************************************/

using System;
using Model.Manager;
using Model.Users;
using Repository;
using System.Collections.Generic;

namespace Service.UsersAndWorkingTime
{
   public class WorkingTimeService
{
    private Repository.WorkingTimeRepository workingTimeRepository = new WorkingTimeRepository();

    public List<WorkingTime> ViewWorkingTimes()
    {
            return workingTimeRepository.GetAllWorkingTimes();
    }

    public Model.Manager.WorkingTime DefineWorkingTime(Model.Manager.WorkingTime workingTime)
    {
            return workingTimeRepository.NewWorkingTime(workingTime);
    }

    public Model.Manager.WorkingTime EditWorkingTime(Model.Manager.WorkingTime workingTime)
    {
            return workingTimeRepository.SetWorkingTime(workingTime);
    }

    public bool DeleteWorkingTime(string doctorJmbg)
    {
            return workingTimeRepository.DeleteWorkingTime(doctorJmbg);
    }

    public List<Doctor> ViewDoctorsWhoWork(DateTime date, Model.Manager.WorkShifts workShift)
    {
            return workingTimeRepository.GetDoctorsWhoWork(date, workShift);
    }

        public WorkShifts getShiftForDoctor(string doctorJmbg)
        {
            return workingTimeRepository.getShiftForDoctor(doctorJmbg);
        }

        public bool DeleteDoctorWorkigTimes(string jmbg)
        {

            return workingTimeRepository.DeleteDoctorWorkigTimes(jmbg);
        }

        public WorkingTime viewWorkingTimeDoctor(string doctorJmbg)
        {
            return workingTimeRepository.GetWorkingTime(doctorJmbg);
        }

    }
}