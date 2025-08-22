using AOWebApp.Models;

namespace AOWebApp.Models.ViewModels
{
    public class ItemWithRatingViewModel
    {
        
        public int ItemId { get; set; }
        public string ItemName { get; set; } = "";
        public string? ItemDescription { get; set; }
        public decimal ItemCost { get; set; }
        public string? ItemImage { get; set; }
        public string CategoryName { get; set; } = "";

        public int ReviewCount { get; set; }          
        public double AverageRating { get; set; }   

        public string RatingText => ReviewCount == 0
            ? "no reviews" : AverageRating.ToString("0.0");
           
    }
}