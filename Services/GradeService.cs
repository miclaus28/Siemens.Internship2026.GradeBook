using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Siemens.Internship2026.GradeBook.Services;

public class GradeService : IGradeService
{
    private readonly IGradeReader _gradeReader;

    public GradeService(IGradeReader gradeReader)
    {
        _gradeReader = gradeReader;
    }

    public async Task<IEnumerable<Grade>> GetFilteredGradesAsync(int n)
    {
        var grades = await _gradeReader.GetAllAsync();

        return grades
            .Where(g => g.IsActive && g.Score >= 5)
            .Take(n);
    }
}