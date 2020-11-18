/***********************************************************************
 * Module:  PlacementInRoomRepository.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Repository.PlacementInRoomRepository
 ***********************************************************************/

using Model.Doctor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Repository
{
   public class PlacementInSickRoomRepository
   {
        private string path;

        public PlacementInSickRoomRepository() {
            string fileName = "placement.json";
            path = Path.GetFullPath(fileName);

        }
		
		 public int getLastId()
        {
            List<PlacemetnInARoom> placements = ReadFromFile();
            if (placements.Count == 0)
            {
                return 0;
            }
            return placements[placements.Count - 1].Id;
        }
		
        public PlacemetnInARoom GetPlacementById(int idExamination)
      {
            // TODO: implement
            List<PlacemetnInARoom> placementList = ReadFromFile();
            foreach (PlacemetnInARoom p in placementList)
            {
                if (p.Id == idExamination)
                {
                    return p;
                }
            }

            return null;
      }
      
      public Model.Doctor.PlacemetnInARoom SetPlacement(Model.Doctor.PlacemetnInARoom placement)
      {
            // TODO: implement
            List<PlacemetnInARoom> placementList = ReadFromFile();
            foreach (PlacemetnInARoom p in placementList)
            {
                if (p.Id == placement.Id)
                {
                    p.patientCard = placement.patientCard;
                    p.room = placement.room;
                    p.Id = placement.Id;
                    p.DateOfPlacement = placement.DateOfPlacement;
                    p.DateOfDismison = placement.DateOfDismison;
                    break;
                }
            }
            WriteInFile(placementList);
            return null;

        }

        public Model.Doctor.PlacemetnInARoom NewPlacement(Model.Doctor.PlacemetnInARoom placement)
      {
            // TODO: implement
            List<PlacemetnInARoom> placementList = ReadFromFile();
            PlacemetnInARoom searchPlacement = GetPlacementById(placement.Id);
            if (searchPlacement != null)
            {
                return null;
            }
            placementList.Add(placement);
            WriteInFile(placementList);
            return placement;

        }

         public List<PlacemetnInARoom> GetAllPlacements()
         {
            // TODO: implement
            List<PlacemetnInARoom> placementList = ReadFromFile();
            return placementList;
         }

        public bool DeletePlacement(int idExamination)
      {
            // TODO: implement
            List<PlacemetnInARoom> placementList = ReadFromFile();
            PlacemetnInARoom placementForDelete = null;
            foreach (PlacemetnInARoom p in placementList)
            {
                if (p.Id == idExamination)
                {
                    placementForDelete = p;
                    break;
                }
            }
            if (placementForDelete == null)
            {
                return false;
            }
            placementList.Remove(placementForDelete);
            WriteInFile(placementList);
            return true;

        }

        public bool DeletePatientPlacements(string jmbg)
        {
            List<PlacemetnInARoom> placementList = ReadFromFile();
            List<PlacemetnInARoom> placementsForDelete = new List<PlacemetnInARoom>();
            foreach (PlacemetnInARoom p in placementList)
            {
                if (p.patientCard.Patient.Jmbg.Equals(jmbg))
                {
                    placementsForDelete.Add(p);
                }
            }
            if (placementsForDelete.Count == 0)
            {
                return false;
            }
            foreach (PlacemetnInARoom p in placementsForDelete)
            {
                placementList.Remove(p);
            }
            WriteInFile(placementList);
            return true;
        }

        public bool DeleteRoomPlacements(int number)
        {
            List<PlacemetnInARoom> placementList = ReadFromFile();
            List<PlacemetnInARoom> placementsForDelete = new List<PlacemetnInARoom>();
            foreach (PlacemetnInARoom p in placementList)
            {
                if (p.room.Number == number)
                {
                    placementsForDelete.Add(p);
                }
            }
            if (placementsForDelete.Count == 0)
            {
                return false;
            }
            foreach (PlacemetnInARoom p in placementsForDelete)
            {
                placementList.Remove(p);
            }
            WriteInFile(placementList);
            return true;
        }

        public List<PlacemetnInARoom> GetPlacementsByRoom(int roomNumber, DateTime beginDate, DateTime endDate)
         {
            // TODO: implement
            List<PlacemetnInARoom> placementList = ReadFromFile();
            List<PlacemetnInARoom> placementListRoom = new List<PlacemetnInARoom>();
            foreach (PlacemetnInARoom p in placementList)
            {
                int compareBeginDate = DateTime.Compare(beginDate, p.DateOfPlacement);
                int compareEndDate = DateTime.Compare(endDate, p.DateOfDismison);
                if (p.room.Number == roomNumber && compareBeginDate <=0 && compareEndDate >=0 )
                {
                    placementListRoom.Add(p);
                }
            }
            return placementListRoom;
        }

        public List<PlacemetnInARoom> GetPlacementsByPatient(string patientJmbg)
        {
            List<PlacemetnInARoom> placementList = ReadFromFile();
            List<PlacemetnInARoom> result = new List<PlacemetnInARoom>();
            foreach (PlacemetnInARoom p in placementList)
            {
                if (p.patientCard.Patient.Jmbg.Equals(patientJmbg))
                {
                    result.Add(p);
                }
            }
            return result;
        }


        private List<PlacemetnInARoom> ReadFromFile()
        {
            List<PlacemetnInARoom> placementList;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                placementList = JsonConvert.DeserializeObject<List<PlacemetnInARoom>>(json);
            }
            else
            {
                placementList = new List<PlacemetnInARoom>();
                WriteInFile(placementList);
            }
            return placementList;
        }

        private void WriteInFile(List<PlacemetnInARoom> placementList)
        {
            string json = JsonConvert.SerializeObject(placementList);
            File.WriteAllText(path, json);
        }

   
      
   
   }
}
 