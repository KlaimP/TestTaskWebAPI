﻿namespace DBApi.Models
{
    public class Results
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public float TimeDelta { get; set; }
        public DateTime MinDate { get; set; }
        public double AvgExecutionTime { get; set; }
        public double AvgValue { get; set; }
        public double MedianValue { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }

    }
}
