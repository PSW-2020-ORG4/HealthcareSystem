using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAdapters.Dtos
{
    public class PharmacyUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ApiKey { get; set; }
    }
}
