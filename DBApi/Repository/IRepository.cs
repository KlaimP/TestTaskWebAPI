using DBApi.Models;
using System.Collections.ObjectModel;

namespace DBApi.Repository
{
    public interface IRepository
    {
        ObservableCollection<Values> GetValues();
        bool AddValues(Values _values);
    }
}
