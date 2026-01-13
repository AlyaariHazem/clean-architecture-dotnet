using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entities
{
    public class Salary
    {
        public int SalaryID { get; set;}
        public DateTime SalaryMonth { get; set; }
        public DateTime SalaryHireDate { get; set; }
        public string? Note { get; set; }    
        public int SalaryAmount { get; set; }    
        public int TeacherID { get; set; }
        [JsonIgnore]
        public Teacher Teacher { get; set; }
    }
}