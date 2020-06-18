using ImportCsvToSQL.Models;
using ImportCsvToSQL.Utilities;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImportCsvToSQL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDBRepository _dbRepository;

        public IDBRepository DBRepository { get; set; }
        
        public HomeController()
        {
            _dbRepository = new DBRepository();
        }

        // Constructor for testing
        public HomeController(IDBRepository dBRepository)
        {
            _dbRepository = dBRepository;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    _context.Dispose();
        //}

        public ActionResult Index()
        {
            // Get all employees from database
            var employees = _dbRepository.GetEmployees().ToList();
            
            return View(employees);
        }

        public ActionResult GetEmployees()
        {   
            // Get all employees from database
            var employees = _dbRepository.GetEmployees().ToList();

            return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            if (id == 0)
                return View();

            // Get employee by Id from database
            var employee = _dbRepository.GetEmployee(id);
            
            return View(employee);
        }

        [HttpPost]
        public ActionResult Update(Employee emp)
        {
            bool status = false;
            if(ModelState.IsValid)
            {
                _dbRepository.Update(emp);
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var employee = _dbRepository.GetEmployee(id);

            if(employee != null)
            {
                return View(employee);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = _dbRepository.DeleteEmployee(id);

            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public ActionResult ImportCsv(HttpPostedFileBase file)
        {
            var errorMessage = "";

            if (file != null && file.ContentLength > 0 && file.FileName.EndsWith(".csv"))
            {
                try
                {
                    FileService fileService = new FileService();

                    // Save csv file in local folder
                    var path = fileService.FilePath(file);
                    file.SaveAs(path);

                    // Get employees data from saved csv file
                    var records = fileService.GetEmployeesFromCsv(path).ToList();

                    _dbRepository.AddEmployees(records);

                    TempData["SuccessMessage"] = records.Count + " rows were successfully imported.";

                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                }
            }
            else
            {
                errorMessage = "Please check if the file has valid format and is not empty.";
            }
            
            if (errorMessage != "") TempData["ErrorMessage"] = "Error message: " + errorMessage;

            return RedirectToAction("Index");
        }
    }
}