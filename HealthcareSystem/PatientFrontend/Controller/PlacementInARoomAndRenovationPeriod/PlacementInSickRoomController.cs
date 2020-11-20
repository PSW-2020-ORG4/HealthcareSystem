/***********************************************************************
 * Module:  PlacementInRoomController.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Controller.Examination&Placement&DrugController.PlacementInRoomController
 ***********************************************************************/

using Model.Doctor;
using Service.PlacementInARoomAndRenovationPeriod;
using System;
using System.Collections.Generic;

namespace Controller.PlacementInARoomAndRenovationPeriod
{
    public class PlacementInSickRoomController
    {
        private PlacementInSickRoomService placementInSickRoomService = new PlacementInSickRoomService();

        public int getLastId()
        {
            return placementInSickRoomService.getLastId();
        }

        public PlacemetnInARoom PlaceInRoom(PlacemetnInARoom placementInRoom)
        {
            // TODO: implement
            return placementInSickRoomService.PlaceInRoom(placementInRoom);
        }

        public PlacemetnInARoom EditPlacement(PlacemetnInARoom placementInRoom)
        {
            // TODO: implement
            return placementInSickRoomService.EditPlacement(placementInRoom);
        }

        public bool DeletePlacement(int idExamination)
        {
            // TODO: implement
            return placementInSickRoomService.DeletePlacement(idExamination);
        }

        public List<PlacemetnInARoom> ViewAllPlacements()
        {
            // TODO: implement
            return placementInSickRoomService.ViewAllPlacements();
        }

        public List<PlacemetnInARoom> ViewRoomPlacements(int roomNumber, DateTime beginDate, DateTime endDate)
        {
            // TODO: implement
            return placementInSickRoomService.ViewRoomPlacements(roomNumber, beginDate, endDate);
        }

        public List<PlacemetnInARoom> ViewPatientPlacements(string jmbg)
        {

            return placementInSickRoomService.ViewPatientPlacements(jmbg);
        }


        public bool DeletePatientPlacements(string jmbg)
        {
            return placementInSickRoomService.DeletePatientPlacements(jmbg);
        }

        public bool DeleteRoomPlacements(int number)
        {
            return placementInSickRoomService.DeleteRoomPlacements(number);
        }

    }
}