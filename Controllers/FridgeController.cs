using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FridgeAPI.Models;
using FridgeAPI.Services.Context;
using Microsoft.EntityFrameworkCore;

namespace FridgeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FridgeController : ControllerBase
    {
         private readonly DataContext _context;

        public FridgeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<FridgeItemModel>> GetFridgeItems()
        {
            var fridgeItems = await _context.FridgeItemInfo.AsNoTracking().ToListAsync();
            return fridgeItems;
        }

        [HttpPost]
        public async Task<IActionResult> Create(FridgeItemModel fridgeItem)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddAsync(fridgeItem);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditFridgeItem(int id, FridgeItemModel fridgeItem)
        {
            var itemStored = await _context.FridgeItemInfo.FindAsync(id);
            if(itemStored == null)
            {
                return BadRequest();
            }

            itemStored.FridgeItemName = fridgeItem.FridgeItemName;
            itemStored.Quantity = fridgeItem.Quantity;
            itemStored.ExpirationDate = fridgeItem.ExpirationDate;
            itemStored.Category = fridgeItem.Category;
            itemStored.IsDeleted = fridgeItem.IsDeleted;

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFridgeItem(int id)
        {
            var itemStored = await _context.FridgeItemInfo.FindAsync(id);
            if(itemStored == null)
            {
                return BadRequest();
            }

            _context.Remove(itemStored);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllFridgeItems()
        {
            var itemsStored = await _context.FridgeItemInfo.ToListAsync();
            if(itemsStored == null)
            {
                return BadRequest();
            }

            _context.RemoveRange(itemsStored);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }


    }
}