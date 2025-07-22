using DBApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApi.Services
{
    class ResultsCalculate
    {
        public Results CalculateResults(List<CsvValues> _values, string filename)
        {
            double sumExecutionTime = 0, sumValue = 0;
            double minValue = double.MaxValue, maxValue = double.MinValue;
            DateTime minDate = _values[0].Date, maxDate = _values[0].Date;

            double[] valueList = new double[_values.Count];


            for (int i = 0; i < _values.Count; i++)
            {
                sumExecutionTime += _values[i].ExecutionTime;
                sumValue += _values[i].Value;

                if (_values[i].Value < minValue)
                    minValue = _values[i].Value;

                if (_values[i].Value > maxValue)
                    maxValue = _values[i].Value;

                if (_values[i].Date < minDate)
                    minDate = _values[i].Date;

                if (_values[i].Date > maxDate)
                    maxDate = _values[i].Date;
                valueList[i] = _values[i].Value;
            }

            Array.Sort(valueList);
            double median = _values.Count % 2 == 0 ? (valueList[_values.Count / 2 - 1] + valueList[_values.Count / 2]) / 2.0 : valueList[_values.Count / 2];
            float timeDelta = (float)(maxDate - minDate).TotalSeconds;
            double avgExecutionTime = sumExecutionTime / _values.Count;
            double avgValue = sumValue / _values.Count;

           return new Results(filename, timeDelta, minDate.ToUniversalTime(), avgExecutionTime, avgValue, median, maxValue, minValue);

        }

    }
}
