using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstituteAdministration.Data;
using InstituteAdministration.Models;
using InstituteAdministration.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InstituteAdministration.Controllers
{
    public class TeacherController : Controller
    {
        private StudentDbContext context;
        public TeacherController(StudentDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Teacher> teachers = context.Teachers.ToList();

            return View(teachers);
        }

        public IActionResult Add()
        {
            AddTeacherViewModel addTeacherViewModel = new AddTeacherViewModel();
            return View(addTeacherViewModel);
        }
        [HttpPost]
        public IActionResult Add(AddTeacherViewModel addTeacherViewModel)
        {
            Teacher newTeacher = new Teacher()
            {
                Name = addTeacherViewModel.Name,
                Location = addTeacherViewModel.Location
            };
            context.Teachers.Add(newTeacher);
            return Redirect("/Teacher");
        }

        public IActionResult ProcessAddTeacherForm(AddTeacherViewModel addTeacherViewModel)
        {
            if (ModelState.IsValid)
            {
                Teacher newTeacher = new Teacher
                {
                    Name = addTeacherViewModel.Name,
                    Location = addTeacherViewModel.Location
                };
                context.Teachers.Add(newTeacher);
                context.SaveChanges();
                return Redirect("/Teacher");
            }
            return View(addTeacherViewModel);
        }

        public IActionResult About(int id)
        {
            return View();
        }
    }
}
