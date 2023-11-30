using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.MapPost("/save-most-popular", async (SmpRequest smpRequest) =>
{
    //Get data
    HttpClient client = new();
    AvailabilityList? availabilityList = await GetData(client, smpRequest.DataUrl!);

    //Process data
    List<PartnerAvailabilityDates> partnerAvailabilityDates = GetDateAvailabilityPartners(availabilityList);
    string postJson = $"{{{string.Join(',', partnerAvailabilityDates.Select(pad => pad.GetJsonFormat()))}}}";

    //Post data
    HttpResponseMessage response = await PostData(postJson, client, smpRequest.PostUrl!);

    //Return response
    return Results.Json(new PostResponse(){ ConfirmationCode = await response.Content.ReadAsStringAsync()});
});

app.Run();


static async Task<AvailabilityList> GetData(HttpClient httpClient, string dataUrl)
{
    JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    AvailabilityList? availabilityList = await httpClient.GetFromJsonAsync<AvailabilityList>(dataUrl, options);
    return availabilityList!;
}

static async Task<HttpResponseMessage> PostData(string postJson, HttpClient httpClient, string postUrl)
{
    JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    HttpResponseMessage response = await httpClient.PostAsJsonAsync(postUrl, postJson, options);
    return response;
}

static List<PartnerAvailabilityDates> GetDateAvailabilityPartners(AvailabilityList availabilityList)
{
    List<PartnerAvailabilityDates>? partnerAvailabilityDates = availabilityList
      .Availability!
      //.DistinctBy(pa => pa.Date)
      .DistinctBy(pa => pa.Partner!.Country)
      .Select(pa => new PartnerAvailabilityDates()
        {
          Country = pa.Partner!.Country,
          Dates = availabilityList
            .Availability!
            .DistinctBy(pad => pad.Date)
            .Where(pad => pad.Partner!.Country!.Equals(pa.Partner!.Country))
            .Select(pad => pad.Date)
            .Order()
            .ToList()
        })
      .ToList();

    return partnerAvailabilityDates;
}