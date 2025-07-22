namespace DBApi.Models
{
    public class Results : Subject
    {
        public string FileName { get; set; }
        public float TimeDelta { get; set; }
        public DateTime MinDate { get; set; }
        public double AvgExecutionTime { get; set; }
        public double AvgValue { get; set; }
        public double MedianValue { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }

        public Results(string fileName, float timeDelta, DateTime minDate, double avgExecutionTime, double avgValue, double medianValue, double maxValue, double minValue)
        {
            FileName = fileName;
            TimeDelta = timeDelta;
            MinDate = minDate;
            AvgExecutionTime = avgExecutionTime;
            AvgValue = avgValue;
            MedianValue = medianValue;
            MaxValue = maxValue;
            MinValue = minValue;
        }
    }
}
