namespace LiverpoolFanSite.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Common.Repositories;
    using LiverpoolFanSite.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    // [Area("Administration")]
    // [Authorize]
    public class PlayersController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Player> playerRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public PlayersController(
            IDeletableEntityRepository<Player> playerRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.playerRepository = playerRepository;
            this.userManager = userManager;
        }

        // GET: Administration/Players
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.playerRepository.All().Include(p => p.AddedByUser);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var player = await this.playerRepository.All()
                .Include(p => p.AddedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return this.NotFound();
            }

            return this.View(player);
        }

        // GET: Administration/Players/Create
        public IActionResult Create()
        {
            this.ViewData["AddedByUserId"] = new SelectList(this.userManager.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Position,ShirtNumber,DateOfBirth,PlaceOfBirth,Description,Signed,Appearances,Goals,AddedByUserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Player player)
        {
            if (this.ModelState.IsValid)
            {
                await this.playerRepository.AddAsync(player);
                await this.playerRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["AddedByUserId"] = new SelectList(this.userManager.Users, "Id", "Id", player.AddedByUserId);
            return this.View(player);
        }

        // GET: Administration/Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var player = this.playerRepository.All().FirstOrDefault(x => x.Id == id);
            if (player == null)
            {
                return this.NotFound();
            }

            this.ViewData["AddedByUserId"] = new SelectList(this.userManager.Users, "Id", "Id", player.AddedByUserId);
            return this.View(player);
        }

        // POST: Administration/Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Position,ShirtNumber,DateOfBirth,PlaceOfBirth,Description,Signed,Appearances,Goals,AddedByUserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Player player)
        {
            if (id != player.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.playerRepository.Update(player);
                    await this.playerRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.PlayerExists(player.Id))
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

            this.ViewData["AddedByUserId"] = new SelectList(this.userManager.Users, "Id", "Id", player.AddedByUserId);
            return this.View(player);
        }

        // GET: Administration/Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var player = await this.playerRepository.All()
                .Include(p => p.AddedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return this.NotFound();
            }

            return this.View(player);
        }

        // POST: Administration/Players/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = this.playerRepository.All().FirstOrDefault(x => x.Id == id);
            this.playerRepository.Delete(player);
            await this.playerRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool PlayerExists(int id)
        {
            return this.playerRepository.All().Any(e => e.Id == id);
        }
    }
}
