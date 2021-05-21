namespace LiverpoolFanSite.Web.ViewModels.Players
{
    using System.Collections.Generic;

    public class PlayersListViewModel : PagingViewModel
    {
        public IEnumerable<PlayersInListViewModel> Players { get; set; }
    }
}
