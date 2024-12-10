using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace proje.Models
{
    public class SurveyType
    {
        [Key] // primary key
        public int Id { get; set; }

        [Required(ErrorMessage ="Bu alan boş bırakılamaz!")] // not null
        [MaxLength(30)]
        [DisplayName("Anket Adı")]
        public string Name { get; set; }
    }
}
