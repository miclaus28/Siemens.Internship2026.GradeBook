using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;
using System;
using System.Threading.Tasks;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly IGradeReader _gradeReader;
    private readonly IGradeService _gradeService;
    private readonly IGradeStatisticsService _statisticsService;
    private readonly ILogger<GradeController> _logger;

    public GradeController(
        IGradeReader gradeReader,
        IGradeService gradeService,
        IGradeStatisticsService statisticsService,
        ILogger<GradeController> logger)
    {
        _gradeReader = gradeReader;
        _gradeService = gradeService;
        _statisticsService = statisticsService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int n)
    {
        _logger.LogInformation("GET api/grade called at {Timestamp} with N={N}", DateTime.UtcNow, n);

        var grades = (await _gradeService.GetFilteredGradesAsync(n)).ToList();
        var statistics = _statisticsService.Calculate(grades);

        _logger.LogInformation(
            "Returning {Count} grades, average score: {Average}",
            statistics.TotalCount,
            statistics.AverageScore);

        return Ok(new
        {
            Data = grades,
            Statistics = statistics
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("GET api/grade/{Id} called at {Timestamp}", id, DateTime.UtcNow);

        if (id <= 0)
        {
            _logger.LogWarning("Invalid id received: {Id}", id);
            return BadRequest("Id must be a positive integer.");
        }

        var grade = await _gradeReader.GetByIdAsync(id);
        if (grade == null || !grade.IsActive)
        {
            _logger.LogWarning("Grade {Id} not found or inactive", id);
            return NotFound($"Grade with Id {id} was not found.");
        }

        return Ok(grade);
    }
}