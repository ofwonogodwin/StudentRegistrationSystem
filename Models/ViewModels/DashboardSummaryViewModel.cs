namespace StudentRegistrationSystem.Models.ViewModels
{
    public class DashboardSummaryViewModel
    {
        public int TotalStudents { get; set; }
        public int TotalUsers { get; set; }
        public int TotalCourses { get; set; }

        public IReadOnlyList<ChartDataPoint> StudentsByCourse { get; set; } = Array.Empty<ChartDataPoint>();
        public IReadOnlyList<ChartDataPoint> StudentsByYear { get; set; } = Array.Empty<ChartDataPoint>();
    }

    public class ChartDataPoint
    {
        public string Label { get; set; } = string.Empty;
        public int Value { get; set; }
    }
}