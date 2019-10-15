﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InetMarket.Models;
using Microsoft.AspNetCore.Authorization;

namespace InetMarket.Controllers
{
    [Authorize(Roles = "admin")]
    public class FilterAddititonsController : Controller
    {
        private readonly MarketContext _context;

        public FilterAddititonsController(MarketContext context)
        {
            _context = context;
        }

        // список доп фильтров с сортировкой по фильтрам
        public IActionResult Index(int? filterId)
        {
            IQueryable<FilterAddititon> filterAddititonsFilter = _context.FilterAddititons.Include(p => p.Filter);
            if (filterId != null && filterId != 0)
            {
                filterAddititonsFilter = filterAddititonsFilter.Where(p => p.FilterId == filterId);
            }
            List<Filter> filters = _context.Filters.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            filters.Insert(0, new Filter { Title = "All", Id = 0 });
            FilterAddititonListView falv = new FilterAddititonListView
            {
                Filters = new SelectList(filters, "Id", "Title"),
                FilterAddititons = filterAddititonsFilter.ToList(),
            };
            return View(falv);
        }

        //отображение результатов сортировки по фильтрам
        [HttpGet]
        public PartialViewResult FilterSearch(int? filterId)
        {
            IQueryable<FilterAddititon> filterAddititonsFilter = _context.FilterAddititons.Include(p => p.Filter);
            if (filterId != null && filterId != 0)
            {
                filterAddititonsFilter = filterAddititonsFilter.Where(p => p.FilterId == filterId);
            }
            FilterAddititonListView falv = new FilterAddititonListView
            {
                FilterAddititons = filterAddititonsFilter.ToList(),
            };
            return PartialView(falv);
        }

        // GET: FilterAddititons/Details/5
        [HttpGet]
        public PartialViewResult Details(int? id)
        {
            List<Filter> filters = _context.Filters.ToList();
            FilterAddititon filterAddititon = _context.FilterAddititons.Find(id);
            return PartialView(filterAddititon);
        }

        // GET: FilterAddititons/Create
        [HttpGet]
        public PartialViewResult Create()
        {
            //отображение выпадающего списка фильтров при добавлении доп фильтра
            SelectList filters = new SelectList(_context.Filters, "Id", "Title");
            ViewBag.Filters = filters;

            return PartialView();
        }

        // POST: FilterAddititons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,FilterId")] FilterAddititon filterAddititon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filterAddititon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(FilterSearch), "Filters", new { filterId = filterAddititon.FilterId});
            }
            return View(filterAddititon);
        }

        // GET: FilterAddititons/Edit/5
        public PartialViewResult Edit(int? id)
        {
            //отображение выпадающего списка фильтров при изменении доп фильтра
            SelectList filters = new SelectList(_context.Filters, "Id", "Title");
            ViewBag.Filters = filters;

            var filterAddititon = _context.FilterAddititons.Find(id);
            return PartialView(filterAddititon);
        }

        // POST: FilterAddititons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,FilterId")] FilterAddititon filterAddititon)
        {
            if (id != filterAddititon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filterAddititon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilterAddititonExists(filterAddititon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(FilterSearch), "Filters", new { filterId = filterAddititon.FilterId });
            }
            return View(filterAddititon);
        }

        // GET: FilterAddititons/Delete/5
        [HttpGet]
        public PartialViewResult Delete(int? id)
        {
            FilterAddititon filterAddititon = _context.FilterAddititons.Find(id);
            return PartialView(filterAddititon);
        }

        // POST: FilterAddititons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filterAddititon = await _context.FilterAddititons.FindAsync(id);
            _context.FilterAddititons.Remove(filterAddititon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(FilterSearch), "Filters", new { filterId = filterAddititon.FilterId });
        }

        private bool FilterAddititonExists(int id)
        {
            return _context.FilterAddititons.Any(e => e.Id == id);
        }
    }
}
