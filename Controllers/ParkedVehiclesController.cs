﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models;
using Garage3.Models.ViewModels;

namespace Garage3.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private const double costPerMinute = 0.2;

        private readonly Garage3Context _context;

        public ParkedVehiclesController(Garage3Context context)
        {
            _context = context;
        }

        // GET: ParkedVehicles
        // Added by Stefan search functionality
        public async Task<IActionResult> Index()
        {
            var vehicles = await _context.ParkedVehicle.ToListAsync();

            var model = new VehicleTypeViewModel
            {
                VehicleList = vehicles,
                VehicleTypes = await TypeAsync()
            };
            return View(model);
        }

        private async Task<IEnumerable<SelectListItem>> TypeAsync()
        {
            return await _context.ParkedVehicle
                         .Select(m => m.VehicleType)
                         // Only distinct type, no multiples
                         .Distinct()
                         .Select(m => new SelectListItem
                         {
                             Text = m.ToString(),
                             Value = m.ToString()
                         })
                         .ToListAsync();
        }

        public async Task<IActionResult> Filter(VehicleTypeViewModel viewModel)
        {
            var vehicles = string.IsNullOrWhiteSpace(viewModel.SearchString) ?
                _context.ParkedVehicle :
                _context.ParkedVehicle.Where(m => m.RegNum.Contains(viewModel.SearchString));

            vehicles = viewModel.VehicleTypes == null ?
                vehicles :
                vehicles.Where(m => m.VehicleType == viewModel.VehicleTypes);

            var model = new VehicleTypeViewModel
            {
                VehicleList = await vehicles.ToListAsync(),
                VehicleTypes = await TypeAsync()
            };

            return View(nameof(Index), model);
        }

        // Torbjörn

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .Include(p => p.VehicleType)
                .Include(p => p.Parking)
                  .ThenInclude(p => p.ParkingSpace)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (parkedVehicle == null)
            {
                return NotFound();
            }

            var detailsView = new ParkedVehicleDetailsViewModel
            {
                VehicleType = parkedVehicle.VehicleType,
                Member = parkedVehicle.Member,
                RegNum = parkedVehicle.RegNum,
                Color = parkedVehicle.Color,
                Make = parkedVehicle.Make,
                Model = parkedVehicle.Model,
                ArrivalTime = parkedVehicle.ArrivalTime,
                Period = DateTime.Now - parkedVehicle.ArrivalTime,
                ParkingSpaces = parkedVehicle.Parking.Select(s => s.ParkingSpace).ToList()

            };

            return View(detailsView);
        }

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "ConfirmPassword");
            ViewData["VehicleTypesID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleTYpe");
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,VehicleTypesID,MemberID,RegNum,Color,Make,Model,ArrivalTime")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "ConfirmPassword", parkedVehicle.MemberID);
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleTYpe", parkedVehicle.VehicleTypeID);
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "ConfirmPassword", parkedVehicle.MemberID);
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleTYpe", parkedVehicle.VehicleTypeID);
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,VehicleTypesID,MemberID,RegNum,Color,Make,Model,ArrivalTime")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "Id", "ConfirmPassword", parkedVehicle.MemberID);
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleTypes>(), "ID", "VehicleTYpe", parkedVehicle.VehicleTypeID);
            return View(parkedVehicle);
        }

        // Torbjörn

        // GET: ParkedVehicles/Checkout/5
        public async Task<IActionResult> Checkout(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .Include(p => p.VehicleType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            var arrival = parkedVehicle.ArrivalTime;
            var checkout = DateTime.Now;

            var checkoutView = new ParkedVehicleCheckoutViewModel
            {              
                Member = parkedVehicle.Member,
                RegNum = parkedVehicle.RegNum,
                ArrivalTime = arrival,
                CheckOutTime = checkout,
                Period = checkout - arrival,
                CostPerMinute = costPerMinute,
                Cost = Math.Round((checkout - arrival).TotalMinutes * costPerMinute, 2)
            };

            return View(checkoutView);
        }

        // Torbjörn
        // ParkedVehicle is deleted and Available is set to True in the ParkingSpace(s) that was used

        // POST: ParkedVehicles/Checkout/5
        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .Include(p => p.Parking)
                .ThenInclude(p => p.ParkingSpace)
                .FirstOrDefaultAsync(m => m.ID == id);

            // Save for use in receipt
            var regnum = parkedVehicle.RegNum;
            var arrival = parkedVehicle.ArrivalTime;
            var checkout = DateTime.Now;

            TempData["regnum"] = parkedVehicle.RegNum;
            TempData["arrival"] = parkedVehicle.ArrivalTime;
            TempData["checkout"] = DateTime.Now;
            TempData["membername"] = parkedVehicle.Member.FullName;

            // Update ParkingSpace (set Available = True)
            parkedVehicle.Parking.Select(s => s.ParkingSpace)
                .ToList()
                .ForEach(p => p.Available = true);         

           _context.ParkedVehicle.Remove(parkedVehicle);
           await _context.SaveChangesAsync();
           return RedirectToAction(nameof(Receipt));
        }

        public IActionResult Receipt()
        {
            var arrival = (DateTime)TempData["arrival"];
            var checkout = (DateTime)TempData["checkout"];

            var receipt = new ParkedVehicleReceiptViewModel
            {
                RegNum = (string)TempData["regnum"],
                MemberName = (string)TempData["membername"],
                ArrivalTime = arrival,
                CheckOutTime = checkout,
                Period = checkout - arrival,
                Cost = Math.Round((checkout - arrival).TotalMinutes * costPerMinute, 2)
            };

            return View(receipt);
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.ID == id);
        }
    }
}
