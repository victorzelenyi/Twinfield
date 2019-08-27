using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twinfield.Models;

namespace Twinfield.Services
{
    public class MarketService
    {
        public List<GroceryItem> GroceryItemsList { get; private set; }
        public List<PurchasedGrocery> PurchasedGroceryItems { get; private set; }

        public MarketService()
        {
            GroceryItemsList = new List<GroceryItem>();
            PurchasedGroceryItems = new List<PurchasedGrocery>();
        }

        public void AddDefaultItems()
        {
            GroceryItemsList.Add(new GroceryItem()
            {
                Name = "A",
                IndividualPrice = 1.25,
                VolumePrice = 3,
                VolumeForDiscount = 3
            });

            GroceryItemsList.Add(new GroceryItem()
            {
                Name = "B",
                IndividualPrice = 4.25,
            });

            GroceryItemsList.Add(new GroceryItem()
            {
                Name = "C",
                IndividualPrice = 1,
                VolumePrice = 5,
                VolumeForDiscount = 6
            });

            GroceryItemsList.Add(new GroceryItem()
            {
                Name = "D",
                IndividualPrice = 0.75,
            });
        }

        public void AddGrosery(GroceryItem groceryItem)
        {
            GroceryItemsList.Add(groceryItem);
        }

        public void PurchaseGrocery(string groceryName)
        {
            var grocery = GroceryItemsList.FirstOrDefault(x => x.Name.ToLower().Equals(groceryName.ToLower()));
            if (grocery != null)
            {
                var purchasedGroceryItem = PurchasedGroceryItems.FirstOrDefault(g => g.GroceryItem.Name.ToLower().Equals(groceryName.ToLower()));
                if (purchasedGroceryItem != null)
                {
                    purchasedGroceryItem.Volume++;
                }
                else
                {
                    PurchasedGroceryItems.Add(new PurchasedGrocery()
                    {
                        GroceryItem = grocery,
                        Volume = 1
                    });
                }

            }
        }

        public void PurchaseGroceries(IEnumerable<string> groceriesName)
        {
            foreach(var groceryName in groceriesName)
            {
                PurchaseGrocery(groceryName);
            }
        }

        public double GetTotalPrice()
        {
            var totalSum = default(double);

            foreach (var purchasedGroceryItem in PurchasedGroceryItems)
            {
                if (purchasedGroceryItem.GroceryItem.VolumePrice.HasValue &&
                    purchasedGroceryItem.GroceryItem.VolumeForDiscount <= purchasedGroceryItem.Volume)
                {
                    totalSum += purchasedGroceryItem.GroceryItem.VolumePrice.Value;
                    purchasedGroceryItem.Volume -= purchasedGroceryItem.GroceryItem.VolumeForDiscount.Value;
                }
                if (purchasedGroceryItem.Volume > 0)
                {
                    totalSum += purchasedGroceryItem.GroceryItem.IndividualPrice * purchasedGroceryItem.Volume;
                }
            }

            PurchasedGroceryItems = new List<PurchasedGrocery>();
            return totalSum;
        }
    }
}
