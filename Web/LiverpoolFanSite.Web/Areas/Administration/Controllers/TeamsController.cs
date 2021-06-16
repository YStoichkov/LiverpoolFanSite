namespace LiverpoolFanSite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: Administration/Teams
        public async Task<IActionResult> Index()
        {
            return this.View(await this._context.Teams.ToListAsync());
        }

        // GET: Administration/Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var team = await this._context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return this.NotFound();
            }

            return this.View(team);
        }

        // GET: Administration/Teams/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Wins,Draws,Loses,GoalsScored,GoalsAgainst,Points,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Team team)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Add(team);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(team);
        }

        // GET: Administration/Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var team = await this._context.Teams.FindAsync(id);
            if (team == null)
            {
                return this.NotFound();
            }

            return this.View(team);
        }

        // POST: Administration/Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Wins,Draws,Loses,GoalsScored,GoalsAgainst,Points,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Team team)
        {
            if (id != team.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(team);
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.TeamExists(team.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(team);
        }

        // GET: Administration/Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var team = await this._context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return this.NotFound();
            }

            return this.View(team);
        }

        // POST: Administration/Teams/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await this._context.Teams.FindAsync(id);
            this._context.Teams.Remove(team);
            await this._context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool TeamExists(int id)
        {
            return this._context.Teams.Any(e => e.Id == id);
        }
    }
}
