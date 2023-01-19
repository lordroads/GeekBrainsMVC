﻿using EmployeesWebApplication.Models;
using EmployeesWebApplication.Servises;
using EmployeesWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesWebApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeeController(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public IActionResult Index()
        {
            var _employees = _employeesRepository.GetAll();

            return View(_employees);
        }

        public IActionResult Details(int id)
        {
            var employee = _employeesRepository.GetById(id);
            if (employee is null)
            {
                return NotFound();
            }

            return View(new EmployeesViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Birthday = employee.Birthday
            });
        }

        public IActionResult Create()
        {
            return View("Edit", new EmployeesViewModel());
        }

        public IActionResult Edit(EmployeesViewModel model)
        {
            var employee = new Employee
            {
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Patronymic = model.Patronymic,
                Birthday = model.Birthday
            };

            if (employee.Id == 0)
            {
                var id = _employeesRepository.Add(employee);
                return RedirectToAction("Details", new { id });
            }

            var success = _employeesRepository.Edit(employee);

            if (!success)
            {
                return NotFound();
            }
            else
            {
                return RedirectToAction("Index");
            }            
        }

        //public IActionResult Edit(int id)
        //{
        //    return View();
        //}

        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
