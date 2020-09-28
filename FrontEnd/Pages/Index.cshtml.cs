using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace SpaceparkWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _client;

        public string Name { get; set; }

        public IndexModel(HttpClient client)
        {
            _client = client;
        }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync()
        {
            try
            {
                Name = Request.Form[nameof(Name)];

                var url = "https://localhost:44366/api/v1.0/traveller/auth";
                _client.DefaultRequestHeaders.Add("name", "" + Name);
                var response = await _client.GetStringAsync(url);

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("Error");
            }

            //var jsonObject = result.FirstOrDefault().ToString();
        }
    }
}