using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace proje.Models
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public string Question { get; set; }

        [ValidateNever]
        public int SurveyTypeId { get; set; }
        [ForeignKey("SurveyTypeId")]

        [ValidateNever]
        public SurveyType SurveyType { get; set; }

        [ValidateNever]
        public string ResimUrl { get; set; }
    }
}
