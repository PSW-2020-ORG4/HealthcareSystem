/***********************************************************************
 * Module:  WorkingTime.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Manager.WorkingTime
 ***********************************************************************/

using Model.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Manager
{
    public class WorkingTime
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorJmbg { get; set; }
        public virtual Doctor Doctor { get; set; }
        public WorkShifts WorkShift { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public WorkingTime() { }

        public WorkingTime(Users.Doctor doctor, WorkShifts workShift, DateTime start, DateTime end)
        {
            if (doctor == null)
            {
                this.Doctor = new Users.Doctor();
            }
            else
            {
                this.Doctor = new Users.Doctor(doctor);
            }
            this.WorkShift = workShift;
            this.StartDate = start;
            this.EndDate = end;
        }
        public WorkingTime(WorkingTime workingTime)
        {
            if (workingTime.Doctor == null)
            {
                this.Doctor = new Users.Doctor();
            }
            else
            {
                this.Doctor = new Users.Doctor(workingTime.Doctor);
            }
            this.WorkShift = workingTime.WorkShift;
            this.StartDate = workingTime.StartDate;
            this.EndDate = workingTime.EndDate;
        }

    }
}