using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ImportCsvToSQL.Models
{
    public class Employee
    {
        [Ignore]
        public int Id { get; set; }
        
        public string Payroll_Number { get; set; }
        
        [StringLength(100)]
        public string Forenames { get; set; }
        
        [StringLength(50)]
        public string Surname { get; set; }

        public DateTime Date_of_Birth { get; set; }
        
        [StringLength(50)]
        public string Telephone { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Address_2 { get; set; }

        [StringLength(25)]
        public string Postcode { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string EMail_Home { get; set; }

        public DateTime Start_Date { get; set; }
    }
}