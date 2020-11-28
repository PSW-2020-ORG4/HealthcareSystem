/***********************************************************************
 * Module:  WorkingTimeController.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Controller.WorkingTimeController.WorkingTimeController
 ***********************************************************************/

using System;
using Service.UsersAndWorkingTime;
using Model.Manager;
using Model.Users;
using System.Collections.Generic;

namespace Controller.UsersAndWorkingTime
{
   public class WorkingTimeController
{
    private Service.UsersAndWorkingTime.WorkingTimeService workingTimeService = new WorkingTimeService();

    public Model.Manager.WorkingTime DefineWorkingTime(Model.Manager.WorkingTime workingTime)
    {
            return workingTimeService.DefineWorkingTime(workingTime);
    }

    public List<WorkingTime> ViewWorkingTimes()
    {
            return workingTimeService.ViewWorkingTimes();
    }

    public Model.Manager.WorkingTime EditWorkingTime(Model.Manager.WorkingTime workingTime)
    {
            return workingTimeService.EditWorkingTime(workingTime);
    }

    public bool DeleteWorkingTime(string doctorJmbg)
    {
            return workingTimeService.DeleteWorkingTime(doctorJmbg);
    }

    public List<Doctor> ViewDoctorsWhoWork(DateTime date, Model.Manager.WorkShifts workShift)
    {
            return workingTimeService.ViewDoctorsWhoWork(date, workShift);
    }

    public WorkShifts getShiftForDoctor(string doctorJmbg)
        {
            return workingTimeService.getShiftForDoctor(doctorJmbg);
        }

        public bool DeleteDoctorWorkigTimes(string jmbg)
        {

            return workingTimeService.DeleteDoctorWorkigTimes(jmbg);
        }

        public WorkingTime viewWorkingTimeDoctor(string doctorJmbg)
        {
            return workingTimeService.viewWorkingTimeDoctor(doctorJmbg);
        }
    }
}