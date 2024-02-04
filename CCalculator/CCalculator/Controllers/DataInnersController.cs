﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CCalculator.Data;
using CCalculator.Models;
using CCalculator.BLL;

namespace CCalculator.Controllers
{
    public class DataInnersController : Controller
    {
        private readonly CCalculatorContext _context;

        public DataInnersController(CCalculatorContext context)
        {
            _context = context;
        }

        // GET: DataInners
        /// <summary>
        /// Список уже существующих в базе записей
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var dataInner = _context.DataInners;
            //return View(await dataInner.ToListAsync());
            var result = from di in _context.DataInners
                         join p in _context.Payments on di.Id equals p.DataInnerId
                         select new DIP
                         {
                             Id=di.Id,
                             LoanSum=di.LoanSum,
                             LoanTerm=di.LoanTerm,
                             LoanRate=di.LoanRate,
                             PaymentDate=p.PaymentDate,
                             PaymentByBody=p.PaymentByBody,
                             PamentByPercent=p.PamentByPercent,
                             BalanceOwed=p.BalanceOwed
                         };
            return View(await result.ToListAsync());
        }

        // GET: DataInners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataInner = await _context.DataInners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataInner == null)
            {
                return NotFound();
            }

            return View(dataInner);
        }

        // GET: DataInners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataInners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoanSum,LoanTerm,LoanRate")] DataInner dataInner)
        {
            PaymentCalculate p = new PaymentCalculate(_context);
            //p.DataInnerId= dataInner.Id;
            //p.PaymentDate=DateTimeOffset.Now;
            //p.PaymentByBody = 1;
            //p.PamentByPercent = 1;
            //dataInner.Pay = p;
            if (ModelState.IsValid)
            {
                _context.Add(dataInner);
                await _context.SaveChangesAsync();
                p.Calculate(dataInner);
                //p.CalculatePlateshi(dataInner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataInner);
        }

        // GET: DataInners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataInner = await _context.DataInners.FindAsync(id);
            if (dataInner == null)
            {
                return NotFound();
            }
            return View(dataInner);
        }

        // POST: DataInners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LoanSum,LoanTerm,LoanRate")] DataInner dataInner)
        {
            if (id != dataInner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataInner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataInnerExists(dataInner.Id))
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
            return View(dataInner);
        }

        // GET: DataInners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataInner = await _context.DataInners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataInner == null)
            {
                return NotFound();
            }

            return View(dataInner);
        }

        // POST: DataInners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataInner = await _context.DataInners.FindAsync(id);
            if (dataInner != null)
            {
                _context.DataInners.Remove(dataInner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataInnerExists(int id)
        {
            return _context.DataInners.Any(e => e.Id == id);
        }
    }
}
