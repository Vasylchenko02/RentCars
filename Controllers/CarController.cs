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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<ActionResult> Details(int id)
        {
            if (id != null)
            {
                Car? car = await db.Cars.Include(c => c.Gallery).FirstOrDefaultAsync(p => p.Id == id);
                if (car != null) return View(car);
            }
            return NotFound();
        }

        // GET: CarController/Create
        [Authorize(Roles = "Moderator,Admin")]
        public ActionResult Create()
        {
            return View();
        }
        private string UploadImage(CarCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                uniqueFileName = UploadFile(model.Image, model);
            }
            return uniqueFileName;
        }
        private List<GalleryImage> UploadGallery(CarCreateViewModel model, Car car)
        {
            List<GalleryImage> images = new List<GalleryImage>();
            if (model.Gallery != null)
            {
                foreach (var item in model.Gallery)
                {
                    var uniqueFileName = UploadFile(item, model);
                    images.Add(new GalleryImage() { ImagePath = uniqueFileName, Car = car });
                }
            }
            return images;
        }
        private string UploadFile(IFormFile file, CarCreateViewModel model)
        {
            string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
            var uniqueFileName = model.NameProducer + "_" + model.NameModel + "_" + Guid.NewGuid().ToString() + file.FileName;
            string FilePath = Path.Combine(uploadsFolder, uniqueFileName);
            file.CopyTo(new FileStream(FilePath, FileMode.Create));
            return uniqueFileName;
        }
        // POST: CarController/Create
        [HttpPost]
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> Create(CarCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Car car = new Car
                {
                    NameProducer = model.NameProducer,
                    NameModel = model.NameModel,
                    Description = model.Description,
                    Price = model.Price,
                    ProductionYear = model.ProductionYear,
                    Transmission = model.Transmission,
                    Fuel = model.Fuel,
                    ImagePath = UploadImage(model),

                };
                car.Gallery = UploadGallery(model, car);
                db.Cars.Add(car);
                await db.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View();
        }

        // GET: CarController/Edit/5
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Car? car = await db.Cars.FirstOrDefaultAsync(p => p.Id == id);
                var model = new CarCreateViewModel
                {
                    Id = car.Id,
                    NameProducer = car.NameProducer,
                    NameModel = car.NameModel,
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
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> Edit(CarCreateViewModel model)
        {

            var car = db.Cars.Include(c => c.Gallery).First(car => car.Id == model.Id);
            car.NameProducer = model.NameProducer;
            car.NameModel = model.NameModel;
            car.Description = model.Description;
            car.Price = model.Price;
            car.ProductionYear = model.ProductionYear;
            car.Transmission = model.Transmission;
            car.Fuel = model.Fuel;
            if (model.Image != null)
                car.ImagePath = UploadImage(model);
            if (model.Gallery != null)
            {
                car.Gallery.Clear();
                car.Gallery = UploadGallery(model, car);
            }              
            await db.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpPost]
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Car? car = await db.Cars.Include(c => c.Gallery).FirstOrDefaultAsync(p => p.Id == id);
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
