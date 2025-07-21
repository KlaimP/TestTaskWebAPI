using DBApi.Models;
using System.Collections.ObjectModel;

namespace DBApi.Repository
{
    public interface IRepository
    {
        ObservableCollection<Values> GetValues();
        bool AddValue(Values _value); 
        bool AddValues(List<CsvValues> _values);
    }
}
