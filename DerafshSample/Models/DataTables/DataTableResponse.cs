using System.Collections.Generic;

namespace DerafshSample.Models.DataTables
{
    public class DataTableResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public int Draw { get; set; }
    }
}