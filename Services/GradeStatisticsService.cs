using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
using System;
using System.Collections.Generic;

namespace Siemens.Internship2026.GradeBook.Services;

public class GradeStatisticsService : IGradeStatisticsService
{
    public GradeStatistics Calculate(IList<Grade> grades)
    {
        return new GradeStatistics
        {
            TotalCount = grades.Count,
            AverageScore = grades.Any() ? grades.Average(g => g.Score) : 0,
            RetrievedAt = DateTime.UtcNow
        };
    }
}