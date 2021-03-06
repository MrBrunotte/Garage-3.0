﻿using System.ComponentModel.DataAnnotations;

namespace Garage3.Models
{
    public class VehicleTypes
    {
        public int ID { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "Vehicle Type")]
        public string VehicleType { get; set; }

        [Required, Display(Name = "Number of required parking spaces")]
        public int FillsNumberOfSpaces { get; set; }
    }
}
