namespace LiverpoolFanSite.Web.Controllers
{
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Data;
    using LiverpoolFanSite.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(
            IVotesService votesService,
            UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Post(VoteInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.votesService.VoteAsync(inputModel.PostId, user.Id, inputModel.IsUpVote);
            var votes = this.votesService.GetVotes(inputModel.PostId);
            return votes;
        }
    }
}
