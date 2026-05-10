using Siemens.Internship2026.GradeBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IGradeService
{
    Task<IEnumerable<Grade>> GetFilteredGradesAsync(int n);
}