using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FlightSearchEngine.Models.ViewModels
{
    public class SearchViewModel
    {
        [Required(ErrorMessage = "Please select source location")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Please select destination")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Enter number of persons")]
        [Range(1, 10, ErrorMessage = "Persons must be between 1 and 10")]
        public int NumberOfPersons { get; set; }

        // Dropdown Lists
        public SelectList SourceList { get; set; }
        public SelectList DestinationList { get; set; }
    }
}