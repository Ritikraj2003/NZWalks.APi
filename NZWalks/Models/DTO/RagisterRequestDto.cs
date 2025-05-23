﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class RagisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string username { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
