using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Derafsh.Models;
using Derafsh.Models.RequestModels;
using DerafshSample.ModelsLibrary.ViewModels.General;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DerafshSample.Core.Abstract
{
    public interface IRepository<TViewModel>
    {
        /// <summary>
        /// یافتن یک موجودیت با شناسه 
        /// </summary>
        Task<TViewModel> FindAsync(int id,SqlTransaction transaction = null);
      
        /// <summary>
        /// ایجاد موجودیت
        /// </summary>
        Task<IdentityResult> CreateAsync(TViewModel viewModel,
            CancellationToken cancellationToken,
            SqlTransaction transaction = null);

        /// <summary>
        /// بروز رسانی یک موجودیت
        /// </summary>
        Task<IdentityResult> UpdateAsync(TViewModel editModel, 
            CancellationToken cancellationToken,
            SqlTransaction transaction = null);

        /// <summary>
        /// حذف یک موجودیت
        /// </summary>
        Task<IdentityResult> DeleteAsync(int id,CancellationToken cancellationToken,
            SqlTransaction transaction = null);

        /// <summary>
        ///  فیلتر لیست موجودیت ها
        /// </summary>
        Task<IEnumerable<TViewModel>> FilterAsync(QueryConditions conditions=null, 
            FilterRequest filterRequest = null);

        Task<IdentityResult> AbstractAsync(string whereTerm = null,
            FilterRequest filterRequest = null);

        /// <summary>
        /// گرفتن تعداد موجودیت ها
        /// </summary>
        Task<int> CountAsync(CancellationToken cancellationToken,SqlTransaction transaction = null);

        /// <summary>
        /// گرفتن لیست موجودیت ها برای نمایش به صورت سلکت
        /// </summary>
        Task<IEnumerable<SelectListItem>> GetSelectListItems(QueryConditions conditions);

        /// <summary>
        ///  گرفتن یک نمونه خالی از این موجودیت
        /// </summary>
        TViewModel GetDefaultInstance(string relatedTableName = "",int foreignKeyValue=0);
    }
}
