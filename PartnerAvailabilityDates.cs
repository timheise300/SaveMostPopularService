using System.Text.Json;

public class PartnerAvailabilityDates
{
    public string? Country { get; set; }
    public List<string?>? Dates { get; set; }

    public string GetJsonFormat() => 
      @$"""{Country}"": {JsonSerializer.Serialize(Dates!.Take(2))}";
}