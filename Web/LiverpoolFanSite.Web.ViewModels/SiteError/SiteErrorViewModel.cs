namespace LiverpoolFanSite.Web.ViewModels.SiteError
{
    public class SiteErrorViewModel
    {
        public string Content { get; set; }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}
