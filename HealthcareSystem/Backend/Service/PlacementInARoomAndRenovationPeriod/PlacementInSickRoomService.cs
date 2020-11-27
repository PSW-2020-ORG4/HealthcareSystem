/***********************************************************************
 * Module:  PlacementInRoomService.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Service.Examination&DrugController.PlacementInRoomService
 ***********************************************************************/

using Model.PerformingExamination;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.PlacementInARoomAndRenovationPeriod
{
   public class PlacementInSickRoomService
   {
        private PlacementInSickRoomRepository placementInSickRoomRepository = new PlacementInSickRoomRepository();
		
		 public int getLastId()
        {
            return placementInSickRoomRepository.getLastId();
        }
		
      public PlacemetnInARoom PlaceInRoom(PlacemetnInARoom placementInRoom)
      {
            // TODO: implement
            return placementInSickRoomRepository.NewPlacement(placementInRoom);
      }
      
      public PlacemetnInARoom EditPlacement(PlacemetnInARoom placementInRoom)
      {
            // TODO: implement
            return placementInSickRoomRepository.SetPlacement(placementInRoom);
      }
      
      public bool DeletePlacement(int idExamination)
      {
            // TODO: implement
            return placementInSickRoomRepository.DeletePlacement(idExamination);
      }
      
      public List<PlacemetnInARoom> ViewAllPlacements()
      {
            // TODO: implement
            return placementInSickRoomRepository.GetAllPlacements();
      }
      
      public List<PlacemetnInARoom> ViewRoomPlacements(int roomNumber, DateTime beginDate, DateTime endDate)
      {
            // TODO: implement
            return placementInSickRoomRepository.GetPlacementsByRoom(roomNumber, beginDate, endDate);
      }

        public List<PlacemetnInARoom> ViewPatientPlacements(string jmbg)
        {
            
            return placementInSickRoomRepository.GetPlacementsByPatient(jmbg);
        }


        public bool DeletePatientPlacements(string jmbg)
        {
            return placementInSickRoomRepository.DeletePatientPlacements(jmbg);
        }

        public bool DeleteRoomPlacements(int number)
        {
            return placementInSickRoomRepository.DeleteRoomPlacements(number);
        }

    }
}