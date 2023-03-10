using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieCrudApp.Data;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace MovieCrudApp.Models
{
    public class Show
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int Interest { get; set; }
        public double? Rating { get; set; }
        public string ImgUrl { get; set; }

        public async Task GetTVMazeData()
        {
            using (var client = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("https://api.tvmaze.com/singlesearch/shows");
                builder.Query = $"q={this.Name}";
                HttpResponseMessage resp = await client.GetAsync(builder.Uri);

                if (resp.IsSuccessStatusCode)
                {
                    var result = await resp.Content.ReadAsStringAsync();

                    dynamic respData = JObject.Parse(result);
                    this.Rating = respData?.rating?.average ?? 0;

                    if (respData?.image?.medium != null)
                    {
                        this.ImgUrl = respData?.image?.medium ?? "";
                    }
                    else
                    {
                        this.ImgUrl = "https://via.placeholder.com/210";
                    }

                    if (respData.genres.Count > 0)
                    {
                        this.Genre = respData.genres.First;
                    }
                    else
                    {
                        this.Genre = respData.type;
                    }

                }
                else
                {
                    this.ImgUrl = "https://via.placeholder.com/210";
                }
            }
        }
    }


}
