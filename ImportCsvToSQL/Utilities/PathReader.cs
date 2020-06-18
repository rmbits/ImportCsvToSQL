using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ImportCsvToSQL.Utilities
{
    public interface IPathReader
    {
        string GetExtension(string fileName);
        string GetFileNameWithoutExtension(string fileName);
        string Combine(string mapPath, string fileName);
        string MapPath(string mapPath);
    }

    public class PathReader : IPathReader
    {
        public string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public string GetFileNameWithoutExtension(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }

        public string Combine(string mapPath, string fileName)
        {
            return Path.Combine(mapPath, fileName);
        }

        public string MapPath(string mapPath)
        {
            return HttpContext.Current.Server.MapPath(mapPath);
        }
    }
}