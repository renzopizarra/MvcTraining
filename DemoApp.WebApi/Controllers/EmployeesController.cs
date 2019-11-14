using AutoMapper;
using DemoApp.Data.Models;
using DemoApp.IBusiness;
using DemoApp.Log4Net;
using DemoApp.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DemoApp.WebApi.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        //private readonly IEmployeeRepository _repository;
        private static readonly ILog log = LogManager.GetLogger(Environment.MachineName);

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //public EmployeesController(IEmployeeRepository repository, IMapper mapper)
        //{
        //    _repository = repository;
        //    _mapper = mapper;
        //}

        //Get all
        [Route()]
        public IHttpActionResult Get()
        {
            try
            {
                var result = _unitOfWork.Employees.GetAll();
                var mappedResult = _mapper.Map<IEnumerable<ModelEmployee>>(result);
                AppLogger.Info("Get all successfull. From: Employees using Web API");
                return Ok(mappedResult);

            }
            catch (Exception ex)
            {
                AppLogger.Error("Get all error. From: Employees using Web API");
                return InternalServerError(ex);
            }
        }

        //By Individual
        [Route("{id}", Name = "GetEmployee")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _unitOfWork.Employees.GetById(id);
                if (result == null) return NotFound();
                AppLogger.Info("Get employee successfull. From: Employees using Web API");
                return Ok(_mapper.Map<ModelEmployee>(result));

            }
            catch (Exception ex)
            {
                AppLogger.Error("Get employee error. From: Employees using Web API");
                return InternalServerError(ex);
            }
        }

        //Create/Post
        [Route()]
        [HttpPost]
        public IHttpActionResult Post(ModelEmployee model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_unitOfWork.Employees.GetById(model.Id) != null)
                    {
                        AppLogger.Debug("New employee unsuccessfull. From: Employees using Web API");
                        ModelState.AddModelError("ID", "Id is in use");
                    }
                    else
                    {
                        //<Destination>(Source)
                        var emp = _mapper.Map<Employee>(model);
                        _unitOfWork.Employees.Add(emp);

                        if (_unitOfWork.Complete() > 0)
                        {

                            var newModel = _mapper.Map<ModelEmployee>(emp);
                            //CreatedAtRoute("GetEmployee", new { moniker = newModel.Id }, newModel);
                            AppLogger.Info("New employee successfull. From: Employees using Web API");
                            return Ok(newModel);
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("New employee error. From: Employees using Web API");
                return InternalServerError(ex);
            }
            AppLogger.Debug("New employee unsuccessfull. From: Employees using Web API");
            return BadRequest(ModelState);
        }


        //Update/PUT
        [Route("{id}")]
        public IHttpActionResult Put(int id, ModelEmployee model)
        {
            try
            {
                var emp = _unitOfWork.Employees.GetById(id);
                if (emp == null) return NotFound();

                //(Source,Destination)
                _mapper.Map(model, emp);

                if (_unitOfWork.Complete() > 0)
                {
                    AppLogger.Info("Update employee successful. From: Employees using Web API");
                    return Ok(_mapper.Map<ModelEmployee>(emp));
                }
                else
                {
                    AppLogger.Error("Update employee error. From: Employees using Web API");
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("Update employee error. From: Employees using Web API");
                return InternalServerError(ex);
            }

        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var emp = _unitOfWork.Employees.GetById(id);
                if (emp == null) return NotFound();

                _unitOfWork.Employees.Remove(emp);


                if (_unitOfWork.Complete() > 0)
                {
                    var result = _unitOfWork.Employees.GetAll();

                    //Mapping
                    var mappedResult = _mapper.Map<IEnumerable<ModelEmployee>>(result);

                    AppLogger.Info("Delete employee successful. From: Employees using Web API");
                    return Ok(mappedResult);
                }
                else
                {
                    AppLogger.Error("Delete employee error. From: Employees using Web API");
                    return InternalServerError();
                }

            }
            catch (Exception ex)
            {
                AppLogger.Error("Delete employee error. From: Employees using Web API");
                return InternalServerError(ex);
            }
        }

    }

}
