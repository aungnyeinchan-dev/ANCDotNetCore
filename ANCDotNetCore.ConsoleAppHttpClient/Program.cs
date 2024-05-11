using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

string jsonStr = await File.ReadAllTextAsync("data.json");
var model = JsonConvert.DeserializeObject<MainDto>(jsonStr);

//Console.WriteLine(jsonStr);

foreach (var id in model.ZodiacSignsDetail)
{
    Console.WriteLine(id.Id);
}

Console.ReadLine();


public class MainDto
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

