using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaterDetailsMvcCore.Models
{
    public class Experience
    {
        [Key]
        public int ExperienceId { get; set; }

        [ForeignKey("Applicant")]
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; private set; }

        public string CompanyName { get; set; }
        public string Designation { get; set; }

        [Range(1, 25, ErrorMessage = "Years Must Be Between 1 and 25!!!")]
        [Required]
        public int YearsWorked { get; set; }

    }
}
