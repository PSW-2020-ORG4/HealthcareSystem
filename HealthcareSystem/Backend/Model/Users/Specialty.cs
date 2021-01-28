using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model.Users
{
    public class Specialty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DoctorSpecialty> DoctorSpecialties { get; set; }

        public Specialty() { }

        public Specialty(int id, string name)
        {

            Id = id;
            Name = name;
        }

        public Specialty(Specialty specialty)
        {

            Id = specialty.Id;
            Name = specialty.Name;
        }

    }
}
