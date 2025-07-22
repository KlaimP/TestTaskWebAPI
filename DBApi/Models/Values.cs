namespace DBApi.Models
{
    public class Values : Subject
    {
        public string FileName { get; set; }
        public DateTime Date { get; set; }
        public float ExecutionTime { get; set; }
        public double Value { get; set; }

        public Values(int id, DateTime date, float executionTime, double value, string fileName)
        {
            Id = id;
            Date = date;
            ExecutionTime = executionTime;
            Value = value;
            FileName = fileName;
        }
        public Values(DateTime date, float executionTime, double value, string fileName)
        {
            Date = date;
            ExecutionTime = executionTime;
            Value = value;
            FileName = fileName;
        }
    }
}
