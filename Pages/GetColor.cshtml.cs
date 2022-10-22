using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Net.Http;
using ColorsWeb.Models;

namespace ColorsWeb.Pages
{
    //public class ColorsItem
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Data { get; set; }

    //}


    public class GetColorsModel : PageModel
    {
        public string strResponse;
        public int iStatusCode = 200;

        public static async Task<string> OnGetPink()
        {
            string[] strColors = { "pink", "hotpink", "deeppink", "fuchsia","mediumvioletred" };
            Random r = new Random();
            int rInt = r.Next(strColors.Length);

            ColorsItem _Item = new ColorsItem()
            {
                Id = 1,
                Name = strColors[rInt],
                Data = ""
            };

            await Task.Run(() => { });

            return JsonSerializer.Serialize(_Item, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true });

        }

        public static async Task<string> OnGetRelay(string vURL)
        {

            await Task.Run(() => { });

            try
            {
                HttpClient client = new HttpClient();

                var msg = client.GetStringAsync(vURL).Result;

                return msg;
            }
            catch( Exception ex)
            {
                ProblemDetails res = new ProblemDetails { Status = 500, Title = ex.Message };

                return res.ToString();
            }

        }

        public async Task OnGetAsync()
        {
            var vURL = Request.Cookies["APIUrl"];

            if (vURL == "pink")
            {
                strResponse = await OnGetPink();
            }
            else
            {
                try
                {
                    HttpClient client = new HttpClient();
                    strResponse = client.GetStringAsync(vURL).Result;

                }
                catch (Exception ex)
                {
                    iStatusCode = 500;
                    ProblemDetails res = new ProblemDetails { Status = 500, Title = ex.Message };
                    strResponse =  JsonSerializer.Serialize(res, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true });
                }
            }
        }
    }

}
