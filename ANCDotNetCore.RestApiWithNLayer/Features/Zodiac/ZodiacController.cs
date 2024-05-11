using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static ANCDotNetCore.RestApiWithNLayer.Features.Zodiac.ZodiacController;

namespace ANCDotNetCore.RestApiWithNLayer.Features.Zodiac
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZodiacController : ControllerBase
    {
        private async Task<Zodiac> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
            var model = JsonConvert.DeserializeObject<Zodiac>(jsonStr);
            return model;
        }

        //Detail about All Zodiac

        [HttpGet("ZodiacSignsDetail")]
        public async Task<IActionResult> Zodiacs()
        {
            var model = await GetDataAsync();
            return Ok(model.ZodiacSignsDetail);
        }

        //Detail about Single Zodiac 

        [HttpGet("SingleZodiacMonth")]
        public async Task<IActionResult> Ids(int idNo)
        {
            var model = await GetDataAsync();
            return Ok(model.ZodiacSignsDetail.FirstOrDefault( x => x.Id == idNo));
        }

        public class Zodiac
        {
            public Zodiacsignsdetail[] ZodiacSignsDetail { get; set; }
        }

        public class Zodiacsignsdetail
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string MyanmarMonth { get; set; }
            public string ZodiacSignImageUrl { get; set; }
            public string ZodiacSign2ImageUrl { get; set; }
            public string Dates { get; set; }
            public string Element { get; set; }
            public string ElementImageUrl { get; set; }
            public string LifePurpose { get; set; }
            public string Loyal { get; set; }
            public string RepresentativeFlower { get; set; }
            public string Angry { get; set; }
            public string Character { get; set; }
            public string PrettyFeatures { get; set; }
            public Trait[] Traits { get; set; }
        }

        public class Trait
        {
            public string name { get; set; }
            public int percentage { get; set; }
        }


    }
}
