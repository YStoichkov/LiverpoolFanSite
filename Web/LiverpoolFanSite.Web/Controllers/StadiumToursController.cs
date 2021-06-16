namespace LiverpoolFanSite.Web.Controllers
{
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data.Common.Repositories;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Web.ViewModels.StadiumTours;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class StadiumToursController : Controller
    {
        private readonly IDeletableEntityRepository<StadiumTour> stadiumTourRepository;

        public StadiumToursController(IDeletableEntityRepository<StadiumTour> stadiumTourRepository)
        {
            this.stadiumTourRepository = stadiumTourRepository;
        }

        public IActionResult BookTour()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BookTour(StadiumTourInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var stadiumTour = new StadiumTour
            {
                Tickets = inputModel.Tickets,
                TourType = inputModel.TourType,
                TourDate = inputModel.TourDate,
                TotalPriceForTour = inputModel.TotalPriceForTour,
                Email = inputModel.Email,
            };
            await this.stadiumTourRepository.AddAsync(stadiumTour);
            await this.stadiumTourRepository.SaveChangesAsync();

            return this.Redirect("/");
        }
    }
}
