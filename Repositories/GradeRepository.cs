using System.Text.Json;
using System.Text.Json.Serialization;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class GradeRepository : IGradeReader
{
    private readonly HttpClient _httpClient;
    private const string EndpointUrl = "https://gist.githubusercontent.com/ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw/145b121103dd1cee3737a681c487f7295ac82e6b/gistfile1.txt";

    public GradeRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Grade?> GetByIdAsync(int id)
    {
        var grades = await GetAllAsync();
        return grades.FirstOrDefault(g => g.Id == id);
    }

    public async Task<IEnumerable<Grade>> GetAllAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(EndpointUrl);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var wrapper = JsonSerializer.Deserialize<ExternalDataWrapper>(jsonString, options);
            return wrapper?.Items ?? Enumerable.Empty<Grade>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la procesarea datelor externe: {ex.Message}");
            return Enumerable.Empty<Grade>();
        }
    }
    private class ExternalDataWrapper
    {
        [JsonPropertyName("items")]
        public List<Grade> Items { get; set; } = new();
    }
}