using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentCarsApp.Models;
using RentCarsApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RentCarsApp.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace RentCarsApp.Controllers
{
    public class CarController : Controller
    {
        // GET: CarController
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment hostingEnviroment;

        public CarController(ApplicationDbContext context, IWebHostEnvironment hostingEnviroment)
        {
            db = context;
            this.hostingEnviroment = hostingEnviroment;
        }
        public ActionResult List()
        {
            return View(db.Cars.ToList());
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: CarController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarController/Create
        public ActionResult Create()
        {
            return View();
        }
        private string UploadImage(CarCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
                uniqueFileName = model.Name + "_" + Guid.NewGuid().ToString() + model.Image.FileName;
                string FilePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.Image.CopyTo(new FileStream(FilePath, FileMode.Create));
            }
            return uniqueFileName;
        }
        // POST: CarController/Create
        [HttpPost]
        public async Task<IActionResult> Create(CarCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Car car = new Car
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ProductionYear = model.ProductionYear,
                    Transmission = model.Transmission,
                    Fuel = model.Fuel,
                    ImagePath = UploadImage(model)
                };
                db.Cars.Add(car);
                await db.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View();
        }

        // GET: CarController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Car? car = await db.Cars.FirstOrDefaultAsync(p => p.Id == id);
                var model = new CarCreateViewModel
                {
                    Id = car.Id,
                    Name = car.Name,
                    Description = car.Description,
                    Price= car.Price,
                    ProductionYear = car.ProductionYear,
                    Transmission=car.Transmission,
                    Fuel=car.Fuel,
                };
                if (car != null) return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CarCreateViewModel model)
        {

            var car = db.Cars.First(car => car.Id == model.Id);
            car.Name = model.Name;
            car.Description = model.Description;
            car.Price = model.Price;
            car.ProductionYear = model.ProductionYear;
            car.Transmission = model.Transmission;
            car.Fuel = model.Fuel;
            if (model.Image != null)
                car.ImagePath = UploadImage(model);

            await db.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Car? car = await db.Cars.FirstOrDefaultAsync(p => p.Id == id);
                if (car != null)
                {
                    db.Cars.Remove(car);
                    await db.SaveChangesAsync();
                    return RedirectToAction("List");
                }
            }
            return NotFound();
        }
    }
}
