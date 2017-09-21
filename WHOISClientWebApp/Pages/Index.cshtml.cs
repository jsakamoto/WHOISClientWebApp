using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WHOISClientWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public string SiteBase { get; private set; }

        public void OnGet()
        {
            var scheme = this.Request.Headers.TryGetValue("X-Forwarded-Proto", out var value) ?
                value.ToString() :
                this.Request.Scheme;
            this.SiteBase = $"{scheme}://{this.Request.Host}";
        }
    }
}
