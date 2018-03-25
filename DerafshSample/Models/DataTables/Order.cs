using Microsoft.AspNetCore.Mvc;

namespace DerafshSample.Models.DataTables
{
    public class Order
    {
        [FromForm(Name = "dir")]
        public string Direction { get; set; }
        
        public int Column { get; set; }
    }
}