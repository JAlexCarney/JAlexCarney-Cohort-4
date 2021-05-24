using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Entities
{
    public class Mission : IValidatableObject
    {
        // Table Properties
        public int MissionId { get; set; }
        public int AgencyId { get; set; }
        [Required]
        [MaxLength(50)]
        public string CodeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? OperationalCost { get; set; }
        [MaxLength(50)]
        public string Notes { get; set; }

        // Navigation Properties
        public List<Agent> Agents { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (ProjectedEndDate.Subtract(StartDate).Ticks < 0) 
            {
                errors.Add(new ValidationResult("Projected End Date Must Be After Start Date", new string[] { "ProjectedEndDate", "StartDate" }));
            }

            if (ActualEndDate.HasValue && ActualEndDate.Value.Subtract(StartDate).Ticks < 0)
            {
                errors.Add(new ValidationResult("Actual End Date Must Be After Start Date", new string[] { "ActualEndDate", "StartDate" }));
            }

            return errors;
        }
    }
}
