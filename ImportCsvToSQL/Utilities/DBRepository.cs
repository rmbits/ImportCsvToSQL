using ImportCsvToSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImportCsvToSQL.Utilities
{
    public interface IDBRepository
    {
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetEmployees();
        void Update(Employee emp);
        bool DeleteEmployee(int id);
        void AddEmployees(List<Employee> employees);
    }

    public class DBRepository : IDBRepository
    {
        private SQLDBContext _db;

        public DBRepository()
        {
            _db = new SQLDBContext();
        }

        // Get all employees
        public IEnumerable<Employee> GetEmployees()
        {
            return _db.Employees.ToList();
        }

        // Get employee by Id
        public Employee GetEmployee(int id)
        {
            return _db.Employees.Where(e => e.Id == id).FirstOrDefault();
        }

        public void Update(Employee emp)
        {
            // Check if employee exists in database
            if (emp.Id > 0)
            {
                // Get employee by Id from database
                var employee = _db.Employees.Where(e => e.Id == emp.Id).FirstOrDefault();

                if (employee != null)
                {
                    employee.Payroll_Number = emp.Payroll_Number;
                    employee.Forenames = emp.Forenames;
                    employee.Surname = emp.Surname;
                    employee.Date_of_Birth = emp.Date_of_Birth;
                    employee.Telephone = emp.Telephone;
                    employee.Mobile = emp.Mobile;
                    employee.Address = emp.Address;
                    employee.Address_2 = emp.Address_2;
                    employee.Postcode = emp.Postcode;
                    employee.EMail_Home = emp.EMail_Home;
                    employee.Start_Date = emp.Start_Date;
                }
            }
            else
            {
                // Add to database if employee is new
                _db.Employees.Add(emp);
            }

            _db.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            // Get employee by Id from database
            var employee = _db.Employees.Where(e => e.Id == id).FirstOrDefault();
            if (employee == null) return false;

            // Delete the employee
            _db.Employees.Remove(employee);
            _db.SaveChanges();

            return true;
        }

        public void AddEmployees(List<Employee> employees)
        {
            // Add employees data to database
            _db.Employees.AddRange(employees);
            _db.SaveChanges();
        }

    }
}