using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentCarsApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RentCarsApp.Data;

namespace RentCarsApp.Controllers
{
    public class CarController : Controller
    {
        // GET: CarController
        private readonly ApplicationDbContext db;
        public CarController(ApplicationDbContext context)
        {
            db = context;
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

        // POST: CarController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            db.Cars.Add(car);
            await db.SaveChangesAsync();
            return RedirectToAction("List");
        }

        // GET: CarController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Car? car = await db.Cars.FirstOrDefaultAsync(p => p.Id == id);
                if (car != null) return View(car);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Car car)
        {
            db.Cars.Update(car);
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
