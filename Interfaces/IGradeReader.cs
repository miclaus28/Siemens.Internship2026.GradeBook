using Siemens.Internship2026.GradeBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IGradeReader
{
    Task<Grade?> GetByIdAsync(int id);
    Task<IEnumerable<Grade>> GetAllAsync();
}