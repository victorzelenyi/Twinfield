using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twinfield.Models;
using Twinfield.Services;

namespace Twinfield.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly MarketService _marketService;

        public MarketController(MarketService marketService)
        {
            _marketService = marketService;
        }

        /// <summary>
        /// Add default grocery item 
        /// </summary>
        /// <returns></returns>
        [HttpPost("default")]
        public IActionResult CreateDefaultGroceries()
        {
            _marketService.AddDefaultItems();
            return Ok();
        }

        /// <summary>
        /// Create grocery item 
        /// </summary>
        /// <param name="groceryItem"></param>
        /// <returns></returns>
        [HttpPost("custom")]
        public IActionResult CreateGrocery([FromForm]GroceryItem groceryItem)
        {
            return Ok(_marketService.PurchasedGroceryItems);
        }

        /// <summary>
        /// Purchase one item
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut("purchase/{name}")]
        public IActionResult PurchaseGrocery(string name)
        {
            _marketService.PurchaseGrocery(name);
            return Ok();
        }


        /// <summary>
        /// Purchase list of items
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        [HttpPut("purchase/list")]
        public IActionResult PurchaseGroceries(List<string> names)
        {
            _marketService.PurchaseGroceries(names);
            return Ok();
        }


        /// <summary>
        /// Return purchased items
        /// </summary>
        /// <returns></returns>
        [HttpGet("purchaseditems")]
        public IActionResult GetPurchasedItems()
        {
            return Ok(_marketService.PurchasedGroceryItems);
        }


        /// <summary>
        /// Return total sum 
        /// </summary>
        /// <returns></returns>
        [HttpGet("totalsum")]
        public IActionResult GetTotalSum()
        {
            return Ok(_marketService.GetTotalPrice());
        }


        /// <summary>
        /// Return avaliable items to purchase
        /// </summary>
        /// <returns></returns>
        [HttpGet("availablegroceries")]
        public IActionResult GetAvailableGroceries()
        {
            return Ok(_marketService.GroceryItemsList);
        }
    }
}