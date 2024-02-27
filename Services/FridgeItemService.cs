using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgeAPI.Models;
using FridgeAPI.Services.Context;

namespace FridgeAPI.Services
{
    public class FridgeItemService
    {
        private readonly DataContext _context;

        public FridgeItemService(DataContext context)
        {
            _context = context;
        }

        //Add a fridge item to the database
        public bool AddFridgeItem(FridgeItemModel newFridgeItem)
        {
            bool result = false;
            _context.Add(newFridgeItem);

            result = _context.SaveChanges() != 0;
            return result;
        }

        //Get All Fridge Items
        public IEnumerable<FridgeItemModel> GetAllFridgeItems()
        {
            return _context.FridgeItemInfo;
        }

        //Get fridge items by category
        public IEnumerable<FridgeItemModel> GetItemsByCategory(string Category)
        {
            return _context.FridgeItemInfo.Where(item => item.Category == Category);
        }

        //Update fridge item
        public bool UpdateFridgeItems(FridgeItemModel ItemUpdate)
        {
            _context.Update<FridgeItemModel>(ItemUpdate);
            return _context.SaveChanges() != 0;
        }

        //Delete fridge item
        public bool DeleteFridgeItem(FridgeItemModel ItemDelete)
        {
            _context.Update<FridgeItemModel>(ItemDelete);
            return _context.SaveChanges() != 0;
        }

        //Delete All fridge items
        public bool DeleteAllFridgeItems()
        {
            _context.RemoveRange(_context.FridgeItemInfo);
            return _context.SaveChanges() != 0;
        }

    }
}