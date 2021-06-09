namespace LiverpoolFanSite.Web.ViewModels.Teams
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LiverpoolFanSite.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class CreateTeamInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<IFormFile> ClubBadge { get; set; }
    }
}
