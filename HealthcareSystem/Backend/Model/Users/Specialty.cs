using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Model.Users;

namespace Backend.Model.Users
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }

        public Specialty() { }

        public Specialty(int id, string name) {

            Id = id;
            Name = name;
        }

        public Specialty(Specialty specialty) {

            Id = specialty.Id;
            Name = specialty.Name;
        }

    }
}
