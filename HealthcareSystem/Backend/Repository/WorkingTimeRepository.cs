/***********************************************************************
 * Module:  WorkingTimeRepository.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Repository.WorkingTimeRepository
 ***********************************************************************/

using System;
using Model.Manager;
using Model.Users;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class WorkingTimeRepository
    {
        private string path;

        public WorkingTimeRepository()
        {

            string fileName = "WorkingTimeRepository.json";
            path = Path.GetFullPath(fileName);
        }

        public Model.Manager.WorkingTime GetWorkingTime(string doctorJmbg)
        {
            List<WorkingTime> workingTimes = ReadFromFile();
            foreach (WorkingTime w in workingTimes)
            {
                if (w.DoctorJmbg.Equals(doctorJmbg))
                {
                    return w;
                }
            }
            return null;
        }

        

        public List<WorkingTime> GetAllWorkingTimes()
        {
            List<WorkingTime> workingTimes = ReadFromFile();
            
            return workingTimes;
        }

        public Model.Manager.WorkingTime SetWorkingTime(Model.Manager.WorkingTime workingTime)
        {
            List<WorkingTime> workingTimes = ReadFromFile();

            foreach (WorkingTime w in workingTimes)
            {
                if ( w.DoctorJmbg.Equals(workingTime.DoctorJmbg))
                {
                    w.Doctor = new Doctor(workingTime.Doctor);
                    w.WorkShift = workingTime.WorkShift;
                    w.StartDate = workingTime.StartDate;
                    w.EndDate = workingTime.EndDate;
                  
                    break;
                }
            }
            WriteInFile(workingTimes);
            return workingTime;
        }

        public bool DeleteWorkingTime(string doctorJmbg)
        {
            List<WorkingTime> workingTimes = ReadFromFile();
            WorkingTime workingTimeForDelete = null;
            foreach (WorkingTime w in workingTimes)
            {
                if (w.DoctorJmbg.Equals(doctorJmbg))
                {
                    workingTimeForDelete = w;
                    break;
                }
            }
            if (workingTimeForDelete == null)
            {
                return false;
            }

            workingTimes.Remove(workingTimeForDelete);
            WriteInFile(workingTimes);
            return true;
        }

        public Model.Manager.WorkingTime NewWorkingTime(Model.Manager.WorkingTime workingTime)
        {
             List<WorkingTime> workingTimes = ReadFromFile();
             WorkingTime searchWorkingTime = GetWorkingTime(workingTime.DoctorJmbg);
             if (searchWorkingTime != null)
             {
                 return null;
             }

             workingTimes.Add(workingTime);
             WriteInFile(workingTimes);
             return workingTime;
        }

        public List<Doctor> GetDoctorsWhoWork(DateTime date, Model.Manager.WorkShifts workShift)
        {
            List<WorkingTime> workingTimes = ReadFromFile();
            List<Doctor> results = new List<Doctor>();
            foreach (WorkingTime w in workingTimes)
            {
                if (w.WorkShift.Equals(workShift))
                {
                    DateTime d1 = w.StartDate;
                    DateTime d2 = w.EndDate;
                    int res1 = DateTime.Compare(d1, date);
                    int res2 = DateTime.Compare(d2, date);
                    if (res1 <= 0 && res2 >= 0)
                    {
                        results.Add(w.Doctor);
                    }
                }
            }
            return results;
        }

        public WorkShifts getShiftForDoctor(string doctorJmbg)
        {
            List<WorkingTime> workingTimes = ReadFromFile();
            foreach(WorkingTime w in workingTimes)
            {
                if (w.DoctorJmbg.Equals(doctorJmbg))
                {
                    return w.WorkShift;
                }
            }
            return 0;
        }

        public bool DeleteDoctorWorkigTimes(string jmbg)
        {
            List<WorkingTime> workingTimes = ReadFromFile();
            List<WorkingTime> workingTimesForDelete = new List<WorkingTime>();
            foreach (WorkingTime w in workingTimes)
            {
                if (w.DoctorJmbg.Equals(jmbg))
                {
                    workingTimesForDelete.Add(w);
                }
            }
            if (workingTimesForDelete.Count == 0)
            {
                return false;
            }
            foreach (WorkingTime w in workingTimesForDelete)
            {
                workingTimes.Remove(w);
            }
            WriteInFile(workingTimes);
            return true;
        }

        private List<WorkingTime> ReadFromFile()
        {
            List<WorkingTime> workingTime;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                workingTime = JsonConvert.DeserializeObject<List<WorkingTime>>(json);
            }
            else
            {
                workingTime = new List<WorkingTime>();
                WriteInFile(workingTime);
            }
            return workingTime;
        }

        private void WriteInFile(List<WorkingTime> workingTime)
        {
            string json = JsonConvert.SerializeObject(workingTime);
            File.WriteAllText(path, json);
        }



    }
}