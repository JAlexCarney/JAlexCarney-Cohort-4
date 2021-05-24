using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Core.Entities
{
    public class Agent : IValidatableObject
    {
        // Table Properties
        public int AgentId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Height { get; set; }

        // Navigation Properties
        public List<AgencyAgent> AgencyAgents { get; set; }
        public List<Mission> Missions { get; set; }
        public List<Alias> Aliases { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (DateTime.Now.Subtract(DateOfBirth).Ticks < 0) 
            {
                errors.Add(new ValidationResult("Date Of Birth Must Be A Past Date", new string[] { "DateOfBirth" }));
            }

            return errors;
        }
    }
}
