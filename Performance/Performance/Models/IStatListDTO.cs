namespace Performance.Models
{
    public interface IStatListDTO
    {
        double? AttendancePoints { get; }
        double? AverageHandleTime { get; }
        double? AverageHoldTime { get; }
        double? AverageWorkTime { get; }
    }
}