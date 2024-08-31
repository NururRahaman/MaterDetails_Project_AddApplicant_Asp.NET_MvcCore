using MaterDetailsMvcCore.Data;
using MaterDetailsMvcCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MaterDetailsMvcCore.Controllers
{
    public class MasterDetailsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public MasterDetailsController(MyDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public IActionResult Index()   
        {
            List<Applicant> applicants;
            applicants= _context.applicants.ToList();
            return View(applicants);
        }
        [HttpGet]
        public IActionResult Create()
        { Applicant applicant= new Applicant();
            applicant.Experiences.Add(new Experience() { ExperienceId = 1 });
            return View(applicant);

        }
        [HttpPost]
        public IActionResult Create(Applicant applicant)
        {
            applicant.Experiences.RemoveAll(n => n.YearsWorked == 0);
            string uniqueFileName= GetUploadedFileName(applicant);
            applicant.PhotoUrl = uniqueFileName;
            _context.Add(applicant);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        private string GetUploadedFileName(Applicant applicant)
        {
            string uniqueFileName = null;
            if (applicant.ProfilePhoto != null)
            {
                string uploadsFolde = Path.Combine(_webHost.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolde, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public IActionResult Details(int id)
        {
            Applicant applicant = _context.applicants
                 .Include(c => c.Experiences)
                 .Where(a => a.Id == id).FirstOrDefault();
            return View(applicant);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Applicant applicant = _context.applicants
                 .Include(c => c.Experiences)
                 .Where(a => a.Id == id).FirstOrDefault();
            return View(applicant);
        }
        [HttpPost]
        public IActionResult Delete(Applicant applicant)
        {
            _context.Attach(applicant);
              _context.Entry(applicant).State = EntityState.Deleted;
              _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
