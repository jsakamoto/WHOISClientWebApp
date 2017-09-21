using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace WHOISClientWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public string SiteBase { get; private set; }

        private bool EnforceHTTPS { get; }

        public IndexModel(IConfiguration configuration)
        {
            this.EnforceHTTPS = configuration.GetValue<bool>("EnforceHTTPS", defaultValue: false);
        }

        public IActionResult OnGet()
        {
            var scheme = this.Request.Headers.TryGetValue("X-Forwarded-Proto", out var value) ?
                value.ToString() :
                this.Request.Scheme;

            if (scheme != "https" && this.EnforceHTTPS)
                return RedirectPermanent($"https://{this.Request.Host}/");

            this.SiteBase = $"{scheme}://{this.Request.Host}";
            return this.Page();
        }
    }
}
