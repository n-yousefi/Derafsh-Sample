using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DerafshSample.ModelsLibrary.Interfaces
{
    public interface ISelectable
    {
        SelectListItem ConvertToSelectListItem();
    }
}