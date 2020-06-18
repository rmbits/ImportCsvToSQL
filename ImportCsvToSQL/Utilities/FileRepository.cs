using CsvHelper;
using ImportCsvToSQL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace ImportCsvToSQL.Utilities
{
    public interface IFileRepository
    {
        IEnumerable<Employee> GetEmployees(string path);
    }

    public class FileRepository : IFileRepository
    {
        public IEnumerable<Employee> GetEmployees(string path)
        {
            var records = new List<Employee>();

            // Read employee data from csv and return as a list
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("en-GB")))
            {
                // Read by row
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new Employee
                    {
                        Payroll_Number = csv.GetField<string>("Personnel_Records.Payroll_Number").Trim(),
                        Forenames = csv.GetField<string>("Personnel_Records.Forenames").Trim(),
                        Surname = csv.GetField<string>("Personnel_Records.Surname").Trim(),
                        Date_of_Birth = csv.GetField<DateTime>("Personnel_Records.Date_of_Birth"),
                        Telephone = csv.GetField<string>("Personnel_Records.Telephone").Trim(),
                        Mobile = csv.GetField<string>("Personnel_Records.Mobile").Trim(),
                        Address = csv.GetField<string>("Personnel_Records.Address").Trim(),
                        Address_2 = csv.GetField<string>("Personnel_Records.Address_2").Trim(),
                        Postcode = csv.GetField<string>("Personnel_Records.Postcode").Trim(),
                        EMail_Home = csv.GetField<string>("Personnel_Records.EMail_Home").Trim(),
                        Start_Date = csv.GetField<DateTime>("Personnel_Records.Start_Date"),
                    };

                    records.Add(record);
                }
            }

            return records;
        }

    }
}