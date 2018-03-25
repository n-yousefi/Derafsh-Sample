using Microsoft.AspNetCore.Mvc;

namespace DerafshSample.Models.DataTables
{
    public class Search
    {
        public string Value { get; set; }

        [FromForm(Name = "regex")]
        public bool RegularExpression { get; set; }
    }
}