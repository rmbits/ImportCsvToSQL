using CsvHelper;
using ImportCsvToSQL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;

namespace ImportCsvToSQL.Utilities
{
    public class FileService
    {
        private IPathReader _pathReader;
        private IFileRepository _fileRepository;

        public FileService(IPathReader pathReader = null, IFileRepository fileRepository = null)
        {
            _pathReader = pathReader ?? new PathReader();
            _fileRepository = fileRepository ?? new FileRepository();
        }

        // Read file and return its path
        public string FilePath(HttpPostedFileBase file)
        {
            var path = "";

            try
            {
                // Get file name
                var fileName = _pathReader.GetFileNameWithoutExtension(file.FileName);
                if (String.IsNullOrEmpty(fileName)) return "Error parsing file name.";

                // Create new file name
                fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

                // Create path for new file name
                var mapPath = _pathReader.MapPath("~/Csvs");
                path = _pathReader.Combine(mapPath, fileName);
                if (String.IsNullOrEmpty(path)) return "Error parsing file path.";

            }
            catch(Exception e)
            {
                return e.Message;
            }

            return path;
        }

        // Save csv file content in DB and return a list
        public List<Employee> GetEmployeesFromCsv(string path)
        {
            var records = new List<Employee>();

            // Get all employees from database
            var employees = _fileRepository.GetEmployees(path);
            foreach (var r in employees)
                records.Add(r);

            return records;
        }

    }
}