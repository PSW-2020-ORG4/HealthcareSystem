/***********************************************************************
 * Module:  ExaminationService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Examination&Drug&PatientCard&TherapyService.ExaminationService
 ***********************************************************************/

using Model.PerformingExamination;
using Model.Manager;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using Backend.Repository.ExaminationRepository;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Model.Exceptions;

namespace Service.ExaminationAndPatientCard
{
   public class ExaminationService: IExaminationService
   {
        private IScheduledExaminationRepository _scheduledExaminationRepository;

        public ExaminationService(IScheduledExaminationRepository scheduledExaminationRepository) {
            _scheduledExaminationRepository = scheduledExaminationRepository;
        }
        public void AddExamination(Examination examination)
        {
            _scheduledExaminationRepository.AddExamination(examination);
        }

        public List<Examination> AdvancedSearchExaminationReports(List<Examination> examinations, string startDate, string endDate, string doctorSurname, string anamesis, string firstOperator, string secondOperator, string thirdOperator)
        {
            List<Examination> filteredExamintion = new List<Examination>();

            if (startDate.Equals("null") && endDate.Equals("null") && doctorSurname.Equals("null") && anamesis.Equals("null"))
            {
                return examinations;
            }

            foreach (Examination e in examinations)
            {
                if (endDate.Equals("null") && doctorSurname.Equals("null") && anamesis.Equals("null"))
                {
                    if (DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0)
                    {
                        filteredExamintion.Add(e);
                    }
                }
                else if (startDate.Equals("null") && doctorSurname.Equals("null") && anamesis.Equals("null"))
                {
                    if (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0)
                    {
                        filteredExamintion.Add(e);
                    }
                }
                else if (startDate.Equals("null") && endDate.Equals("null") && anamesis.Equals("null"))
                {
                    if (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower()))
                    {
                        filteredExamintion.Add(e);
                    }
                }
                else if (startDate.Equals("null") && endDate.Equals("null") && doctorSurname.Equals("null"))
                {
                    if (e.Anamnesis.ToLower().Contains(anamesis.ToLower()))
                    {
                        filteredExamintion.Add(e);
                    }
                }
                else if (anamesis.Equals("null") && doctorSurname.Equals("null"))
                {
                    if (firstOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                }
                else if (anamesis.Equals("null") && endDate.Equals("null"))
                {
                    if (secondOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                }
                else if (doctorSurname.Equals("null") && endDate.Equals("null"))
                {
                    if (thirdOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                }
                else if (anamesis.Equals("null") && startDate.Equals("null"))
                {
                    if (secondOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                }
                else if (doctorSurname.Equals("null") && startDate.Equals("null"))
                {
                    if (thirdOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                }
                else if (startDate.Equals("null") && endDate.Equals("null"))
                {
                    if (thirdOperator.Equals("AND"))
                    {
                        if ((e.Anamnesis.ToLower().Contains(anamesis.ToLower())) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else
                    {
                        if ((e.Anamnesis.ToLower().Contains(anamesis.ToLower())) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                }
                else if (startDate.Equals("null"))
                {
                    if (thirdOperator.Equals("AND") && secondOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (thirdOperator.Equals("OR") && secondOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (thirdOperator.Equals("OR") && secondOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (thirdOperator.Equals("AND") && secondOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                }
                else if (endDate.Equals("null"))
                {
                    if (thirdOperator.Equals("AND") && secondOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (thirdOperator.Equals("OR") && secondOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (thirdOperator.Equals("OR") && secondOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (thirdOperator.Equals("AND") && secondOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                }
                else if (doctorSurname.Equals("null"))
                {
                    if (firstOperator.Equals("AND") && thirdOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("OR") && thirdOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("OR") && thirdOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("AND") && thirdOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                }
                else if (anamesis.Equals("null"))
                {
                    if (firstOperator.Equals("AND") && secondOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("OR") && secondOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("OR") && secondOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("AND") && secondOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                }
                else
                {
                    if (firstOperator.Equals("AND") && secondOperator.Equals("AND") && thirdOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("OR") && secondOperator.Equals("OR") && thirdOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("OR") && secondOperator.Equals("AND") && thirdOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("AND") && secondOperator.Equals("OR") && thirdOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("OR") && secondOperator.Equals("OR") && thirdOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("AND") && secondOperator.Equals("AND") && thirdOperator.Equals("OR"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) || (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("AND") && secondOperator.Equals("OR") && thirdOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) && (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) || (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                    else if (firstOperator.Equals("OR") && secondOperator.Equals("AND") && thirdOperator.Equals("AND"))
                    {
                        if ((DateTime.Compare(e.DateAndTime, DateTime.Parse(startDate)) >= 0) || (DateTime.Compare(e.DateAndTime, DateTime.Parse(endDate)) <= 0) && (e.Doctor.Surname.ToLower().Contains(doctorSurname.ToLower())) && (e.Anamnesis.ToLower().Contains(anamesis.ToLower())))
                        {
                            filteredExamintion.Add(e);
                        }
                    }
                }

            }

            return filteredExamintion;
        }

        public Examination AppointmentRecommendationByDate(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return _scheduledExaminationRepository.AppointmentRecommendationByDate(doctor, beginDate, endDate);
        }

        public Examination AppointmentRecommendationByDoctor(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return _scheduledExaminationRepository.AppointmentRecommendationByDoctor(doctor, beginDate, endDate);
        }

        public bool CheckDoctorAvailability(Doctor doctor, DateTime dateAndTime)
        {
            return _scheduledExaminationRepository.CheckDoctorAvailability(doctor, dateAndTime);
        }

        public bool CheckRoomAvailability(Room room, DateTime dateAndTime)
        {
            return _scheduledExaminationRepository.CheckRoomAvailability(room, dateAndTime);
        }

        public void DeleteDoctorScheduledExaminations(string doctorJmbg)
        {
             _scheduledExaminationRepository.DeleteDoctorScheduledExaminations(doctorJmbg);
        }

        public void DeleteExamination(int id)
        {
            _scheduledExaminationRepository.DeleteExamination(id);
        }

        public void DeletePatientScheduledExaminations(string patientJmbg)
        {
            _scheduledExaminationRepository.DeletePatientScheduledExaminations(patientJmbg);
        }

        public void DeleteRoomScheduledExaminations(int numberOfRoom)
        {
            _scheduledExaminationRepository.DeleteRoomScheduledExaminations(numberOfRoom);
        }

        public List<Examination> fillAppointments(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return _scheduledExaminationRepository.fillAppointments(doctor, beginDate, endDate);
        }

        public List<Examination> GetAllExaminations()
        {
            return _scheduledExaminationRepository.GetAllExaminations();
        }

        public Examination GetExaminationByDoctorDateAndTime(Doctor doctor, DateTime dateAndTime)
        {
            return _scheduledExaminationRepository.GetExaminationByDoctorDateAndTime(doctor, dateAndTime);
        }

        public Examination GetExaminationById(int id)
        {
            return _scheduledExaminationRepository.GetExaminationById(id);
        }

        public List<Examination> GetExaminationsByDate(DateTime date)
        {
            return _scheduledExaminationRepository.GetExaminationsByDate(date);
        }

        public List<Examination> GetExaminationsByDoctor(string doctorJmbg)
        {
            return _scheduledExaminationRepository.GetExaminationsByDoctor(doctorJmbg);
        }

        public List<Examination> GetExaminationsByDoctorAndDate(Doctor doctor, DateTime dateAndTime)
        {
            return _scheduledExaminationRepository.GetExaminationsByDoctorAndDate(doctor, dateAndTime);
        }

        public List<Examination> GetExaminationsByPatient(string patientJmbg)
        {
            try
            {
                return _scheduledExaminationRepository.GetExaminationsByPatient(patientJmbg);
            }
            catch (Exception)
            {
                throw new NotFoundException("There is no patients examinations in database.");
            }
        }

        public List<Examination> GetExaminationsByRoom(int numberOfRoom)
        {
            return _scheduledExaminationRepository.GetExaminationsByRoom(numberOfRoom);
        }

        public List<Examination> GetExaminationsByRoomAndDates(int numberOfRoom, DateTime beginDate, DateTime endDate)
        {
            return _scheduledExaminationRepository.GetExaminationsByRoomAndDates(numberOfRoom, beginDate, endDate);
        }

        public List<Examination> getFreeAppointments(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            return _scheduledExaminationRepository.getFreeAppointments(doctor, beginDate, endDate);
        }

        public void UpdateExamination(Examination examination)
        {
            _scheduledExaminationRepository.UpdateExamination(examination);
        }
    }
}