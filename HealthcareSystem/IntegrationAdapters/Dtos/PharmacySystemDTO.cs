﻿using System.ComponentModel.DataAnnotations;

namespace IntegrationAdapters.Dtos
{
    public class PharmacySystemDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the API key.")]
        public string ApiKey { get; set; }

        [Required(ErrorMessage = "Please enter the URL.")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Please enter the Email.")]
        public string Email { get; set; }

        public string ActionsBenefitsExchangeName { get; set; }

        [Required]
        public bool ActionsBenefitsSubscribed { get; set; } = false;

        public string GrpcHost { get; set; }

        public int GrpcPort { get; set; } = -1;
    }
}
