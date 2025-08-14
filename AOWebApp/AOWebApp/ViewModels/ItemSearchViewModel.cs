using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;   
using AOWebApp.Models;                      

namespace AOWebApp.Models.ViewModels
{
    public class ItemSearchViewModel
    {
      
        public string? SearchText { get; set; }

        
        public int? CategoryId { get; set; }

    
        public SelectList? CategoryList { get; set; }


        public List<ItemWithRatingViewModel> ItemList { get; set; } = new();
    }
}
