using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingSystemCore
{
    public class parkingslot
    {
        [Key]

        public int sl { get; set; }

        [Required(ErrorMessage = "Enter valid integer")]
        [Display(Name = "Floor number")]
        public int floor { get; set; }

        [Required(ErrorMessage = "Enter valid integer")]
        [Display(Name = "Slot number")]
        public int slot { get; set; }

        [Required]
        [Display(Name = "Section")]
        public string section { get; set; }

        [Required]
        [Display(Name = "Availability")]
        public Availability availability { get; set; }



    }

    public enum Availability
    {
        Available,
        Occupied
    }

    public class details
    {
        [Key]
        [Display(Name = "Vehicle Number")]
        public string vehicleNo { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$",
            ErrorMessage = "only digits are allowed and length must be 10.")]                           //done
        [Display(Name = "Contact Number")]
        public string contactNumber { get; set; }

        [Required]
        [Display(Name = "Date")]
        //     [RegularExpression(@"[0-9]{4}[-]{1}[")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Display(Name = "In Time")]
        [Range(0, 23.58)]
        public double inTime { get; set; }

        [Display(Name = "Out Time")]
        [Range(0, 23.59)]
        public double? outTime { get; set; }

        [Display(Name = "Cost")]
        public double? cost { get; set; }


        [ForeignKey("serialno")]
        [Display(Name = "Slot")]
        public int? Slot { get; set; }
        public virtual parkingslot serialno { get; set; }


    }


    public class backup
    {
        [Key]
        public int sl { get; set; }


        [Display(Name = "Vehicle Number")]
        public string vehicleNo { get; set; }


        [Display(Name = "Name")]
        public string name { get; set; }


        [Display(Name = "Contact Number")]
        public string contactNumber { get; set; }

        [Display(Name = "In Time")]
        public double inTime { get; set; }

        [Display(Name = "Out Time")]
        public double outTime { get; set; }

        [Display(Name = "Cost")]
        public double cost { get; set; }

        [ForeignKey("serialno")]
        [Display(Name = "Slot")]
        public int slot { get; set; }
        public virtual parkingslot serialno { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }


    }
}
