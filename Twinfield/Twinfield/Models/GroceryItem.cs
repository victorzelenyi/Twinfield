using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twinfield.Models
{
    public class GroceryItem
    {
        public string Name { get; set; }

        public double IndividualPrice { get; set; }

        public double? VolumePrice { get; set; }

        public int? VolumeForDiscount { get; set; }
    }
}
