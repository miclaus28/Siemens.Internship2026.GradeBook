using Siemens.Internship2026.GradeBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IGradeStatisticsService
{
    GradeStatistics Calculate(IList<Grade> grades);
}

