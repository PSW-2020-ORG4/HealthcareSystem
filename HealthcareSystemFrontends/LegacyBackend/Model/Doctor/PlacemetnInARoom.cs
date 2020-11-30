/***********************************************************************
 * Module:  PlacemetnInARoom.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Doctor.PlacemetnInARoom
 ***********************************************************************/

using Model.Manager;
using Model.Secretary;
using System;

namespace Model.Doctor
{
   public class PlacemetnInARoom
   {
      public Model.Secretary.PatientCard patientCard { get; set; }
      public Model.Manager.Room room { get; set; }
      public int Id { get; set; }
      public DateTime DateOfPlacement { get; set; }
      public DateTime DateOfDismison { get; set; }

        public PlacemetnInARoom() { }

        public PlacemetnInARoom(PatientCard patientCard, Room room, int id, DateTime dateOfPlacement, DateTime dateOfDismison)
        {
            if (patientCard == null)
            {
                this.patientCard = new PatientCard();
            }
            else
            {
                this.patientCard = new PatientCard(patientCard);
            }

            if (room == null)
            {
                this.room = new Room();
            }
            else
            {
                this.room = new Room(room);
            }
            this.Id = id;
            this.DateOfPlacement = dateOfPlacement;
            this.DateOfDismison = dateOfDismison;
        }

        public PlacemetnInARoom(PlacemetnInARoom placement) {
            if (placement.patientCard == null) {
                this.patientCard = new PatientCard();
            }
            else
            {
                this.patientCard = new PatientCard(placement.patientCard);
            }

            if (placement.room == null)
            {
                this.room = new Room();
            }
            else
            {
                this.room = new Room(placement.room);
            }
            this.Id = placement.Id;
            this.DateOfPlacement = placement.DateOfPlacement;
            this.DateOfDismison = placement.DateOfDismison;
        }

    }
}