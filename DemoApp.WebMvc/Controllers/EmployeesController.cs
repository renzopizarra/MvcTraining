using Dapper;
using DemoApp.Global;
using DemoApp.Log4Net;
using DemoApp.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace DemoApp.WebMvc.Controllers
{
    public class EmployeesController : Controller
    {
        IDbConnection db = new SqlConnection(GlobalConnectionString._connString);

        public ActionResult Index()
        {
            try
            {
                using (db)
                {
                    db.Open();
                    IList<ModelEmployeeMvc> EmpList = SqlMapper.Query<ModelEmployeeMvc>(db, "GetEmployees").ToList();
                    db.Close();
                    AppLogger.Info("Get all successfull. From: Employees using Dapper");
                    return View(EmpList.ToList());
                }
            }
            catch (Exception ex)
            {

                AppLogger.Error("Get all error. From: Employees using Dapper");
                return View(ex);
            }
        }

        public ActionResult Details(int id)
        {
            ModelEmployeeMvc Employees = new ModelEmployeeMvc();
            using (db)
            {
                db.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@empId", id);
                Employees = db.Query<ModelEmployeeMvc>("GetEmployeeDetails", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
                db.Close();
                AppLogger.Info("Employee details successful. From: Employees using Dapper");
                return View(Employees);
            }
        }

        public ActionResult Create()
        {
            AppLogger.Error("Employee details error. From: Employees using Dapper");
            return View();
        }

        [HttpPost]
        public ActionResult Create(ModelEmployeeMvc emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (db)
                    {
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@firstName", emp.FirstName);
                        param.Add("@lastName", emp.LastName);
                        param.Add("@contactNumber", emp.ContactNumber);
                        param.Add("@email", emp.Email);
                        param.Add("@address", emp.Address);

                        db.Open();
                        db.Execute("AddNewEmployee", param, commandType: CommandType.StoredProcedure);

                        AppLogger.Info("Insert new employee. From: Employees using Dapper");
                        db.Close();
                        return RedirectToAction("Index");
                    }

                }
                return View();
            }
            catch (Exception ex)
            {
                AppLogger.Error("Insert new employee error. From: Employees using Dapper");
                return View(ex);
            }
        }

        public ActionResult Edit(int id)
        {
            ModelEmployeeMvc Employees = new ModelEmployeeMvc();

            if (id > 0)
            {
                using (db)
                {
                    db.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@empId", id);
                    Employees = db.Query<ModelEmployeeMvc>("GetEmployeeDetails", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    db.Close();
                    AppLogger.Info("Get employee details successful. From: Employees using Dapper");
                    return View(Employees);
                }
            }
            AppLogger.Error("Get employee details error. No employee details found. From: Employees using Dapper");
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(int id, ModelEmployeeMvc emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (db)
                    {
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@empId", id);
                        param.Add("@firstName", emp.FirstName);
                        param.Add("@lastName", emp.LastName);
                        param.Add("@contactNumber", emp.ContactNumber);
                        param.Add("@email", emp.Email);
                        param.Add("@address", emp.Address);

                        db.Open();
                        db.Execute("UpdateEmployee", param, commandType: CommandType.StoredProcedure);
                        db.Close();
                        AppLogger.Info("Edit employee successful. From: Employees using Dapper");
                        return RedirectToAction("Index");
                    }
                }
                using (db)
                {
                    db.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@empId", id);
                    emp = db.Query<ModelEmployeeMvc>("GetEmployeeDetails", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    db.Close();
                    AppLogger.Info("Edit employee unsuccessful. From: Employees using Dapper");
                    return View(emp);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("Edit employee error. From: Employees using Dapper");
                return View(ex);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ModelEmployeeMvc Employees = new ModelEmployeeMvc();

            if (id > 0)
            {
                using (db)
                {
                    db.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@empId", id);
                    Employees = db.Query<ModelEmployeeMvc>("GetEmployeeDetails", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    db.Close();
                    AppLogger.Info("Get employee successful. From: Employees using Dapper");
                    return View(Employees);
                }
            }
            AppLogger.Info("Get employee unsuccessful. From: Employees using Dapper");
            return HttpNotFound();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                if (id > 0)
                {
                    using (db)
                    {
                        db.Open();
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@empId", id);
                        db.Query<ModelEmployeeMvc>("DeleteEmployee", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
                        db.Close();
                        AppLogger.Info("Get employee successful. From: Employees using Dapper");
                        return RedirectToAction("Index");
                    }
                }
                AppLogger.Info("Get employee unsuccessful. From: Employees using Dapper");
                return HttpNotFound();
            }
            catch
            {
                AppLogger.Info("Get employee error. From: Employees using Dapper");
                return View();
            }
        }
    }
}
