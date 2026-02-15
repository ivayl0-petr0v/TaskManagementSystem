using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static TaskManagementSystem.GCommon.ValidationConstants;

namespace TaskManagementSystem.Data.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(StatusNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Project> Projects { get; set; }
            = new List<Project>();
    }
}
