namespace DBApi.Models
{
    public class Values : Subject
    {
        public DateTime Date { get; set; }
        public float ExecutionTime { get; set; }
        public double Value { get; set; }

        public Values(int id, DateTime date, float executionTime, double value)
        {
            Id = id;
            Date = date;
            ExecutionTime = executionTime;
            Value = value;
        }
        public Values(DateTime date, float executionTime, double value)
        {
            
            Date = date;
            ExecutionTime = executionTime;
            Value = value;
        }
    }
}
