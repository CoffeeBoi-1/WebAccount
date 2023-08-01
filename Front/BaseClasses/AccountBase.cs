using Front.Validators;
using System.ComponentModel.DataAnnotations;
using System;
using Front.Models;

namespace Front.BaseClasses
{
    public class AccountBase
    {
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required, CompareGreater(nameof(StartDate), ErrorMessage = "Дата окончания должна быть после даты начала")]
        public DateTime EndDate { get; set; }

        [Required]
        public AddressModel Address { get; set; } = new AddressModel();

        [Required]
        public float RoomArea { get; set; }
    }
}