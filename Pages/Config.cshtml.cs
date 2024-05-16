using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace ColorsWeb.Pages
{
    public class ConfigModel : PageModel
    {
        [BindProperty]
        public string APIUrl { get; set; }
        [BindProperty]
        public bool APIMode { get; set; }

        [BindProperty]
        public string NumberLights { get; set; }

        public void OnGet()
        {
            var vURL = Request.Cookies["APIUrl"];
            if (vURL != null) {
                APIUrl = vURL.ToString();
                APIMode = (Request.Cookies["APIMode"] == "Direct") ;
            }
            else
            {
                APIUrl = "https://colorsapi.azurewebsites.net/colors/random";
                APIMode = true;
            }

            var vNumberLights = Request.Cookies["NumberLights"];
            NumberLights = (vNumberLights != null) ? vNumberLights : "500"; 

        }

        public IActionResult OnPost()
        {
            if (APIUrl == null || APIUrl == "")
            {
                return RedirectToPage("/Config");
            }

            if (NumberLights == null || NumberLights == "")
            {
                return RedirectToPage("/Config");
            }

            try
            {
                int vNumberLights = Int32.Parse(NumberLights.Trim());
                if (vNumberLights < 1 || vNumberLights > 1000)
                {
                    return RedirectToPage("/Config");
                }
            }
            catch
            {
                return RedirectToPage("/Config");
            }


            Response.Cookies.Append("NumberLights", NumberLights.Trim(),
                new CookieOptions
                {
                    HttpOnly = false,
                    Secure = false,
                    Expires = DateTime.Now.AddMonths(12)
                }
            );

            Response.Cookies.Append("APIUrl", APIUrl,
                new CookieOptions
                {
                    HttpOnly = false,
                    Secure = false,
                    Expires = DateTime.Now.AddMonths(12)
                }
            );

            Response.Cookies.Append("APIMode", (APIMode) ? "Direct" : "",
                new CookieOptions
                {
                    HttpOnly = false,
                    Secure = false,
                    Expires = DateTime.Now.AddMonths(12)
                }
            );

            return RedirectToPage("/Index");

        }

    }
}