/***********************************************************************
 * Module:  WorkingTime.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Manager.WorkingTime
 ***********************************************************************/

using Model.Users;
using System;

namespace Model.Manager
{
    public class WorkingTime
    {
        public Model.Users.Doctor doctor { get; set; }
        public WorkShifts WorkShift { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public WorkingTime() { }

        public WorkingTime(Users.Doctor doctor, WorkShifts workShift, DateTime start, DateTime end)
        {
            if (doctor == null)
            {
                this.doctor = new Users.Doctor();
            }
            else
            {
                this.doctor = new Users.Doctor(doctor);
            }
            this.WorkShift = workShift;
            this.StartDate = start;
            this.EndDate = end;
        }
        public WorkingTime(WorkingTime workingTime)
        {
            if (workingTime.doctor == null)
            {
                this.doctor = new Users.Doctor();
            }
            else
            {
                this.doctor = new Users.Doctor(workingTime.doctor);
            }
            this.WorkShift = workingTime.WorkShift;
            this.StartDate = workingTime.StartDate;
            this.EndDate = workingTime.EndDate;
        }

    }
}