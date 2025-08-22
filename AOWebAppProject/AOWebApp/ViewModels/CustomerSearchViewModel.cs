using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using AOWebApp.Models;

namespace AOWebApp.Models.ViewModels
{
    public class CustomerSearchViewModel
    {
        public string? SearchText { get; set; }  

        public string? Suburb { get; set; }

        public SelectList? SuburbList { get; set; }

        public List<Customer> CustomerList { get; set; } = new ();

        public bool HasFilter => !string.IsNullOrWhiteSpace(SearchText) || !string.IsNullOrWhiteSpace(Suburb);

        public bool HasResults => CustomerList != null && CustomerList.Count > 0;

        public string StatusMessage => !HasFilter ? "Please enter a search term or select a suburb." :
            HasResults ? $"{CustomerList.Count} customer(s) found." : "No customers found matching your criteria.";
    }
}
