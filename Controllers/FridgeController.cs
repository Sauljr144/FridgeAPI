using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FridgeAPI.Models;
using FridgeAPI.Services;

namespace FridgeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FridgeController : ControllerBase
    {
        private readonly FridgeItemService _data;

        public FridgeController(FridgeItemService dataFromService)
        {
            _data = dataFromService;
        }

        //Add a fridge item to the database
        [HttpPost("AddFridgeItems")]
        public bool AddFridgeItems(FridgeItemModel newFridgeItem)
        {
            return _data.AddFridgeItem(newFridgeItem);
        }

        //Get All Fridge Items
        [HttpGet("GetFridgeItems")]
        public IEnumerable<FridgeItemModel> GetAllFridgeItems()
        {
            return _data.GetAllFridgeItems();
        }

        //Get fridge items by category
        [HttpGet("GetItemsByCategory/{Category}")]
        public IEnumerable<FridgeItemModel> GetItemsByCategory(string Category)
        {
            return _data.GetItemsByCategory(Category);
        }

        //Update fridge item
        [HttpPost("UpdateFridgeItem")]
        public bool UpdateFridgeItems(FridgeItemModel ItemUpdate)
        {
            return _data.UpdateFridgeItems(ItemUpdate);
        }

        //Delete fridge item
        [HttpPost("DeleteFridgeItem/{FridgeItemToDelete}")]
        public bool DeleteFridgeItem(FridgeItemModel ItemDelete)
        {
            return _data.DeleteFridgeItem(ItemDelete);
        }

        //Delete All fridge items
        [HttpPost("DeleteAllFridgeItems")]
        public bool DeleteAllFridgeItems()
        {
            return _data.DeleteAllFridgeItems();
        }

    }
}