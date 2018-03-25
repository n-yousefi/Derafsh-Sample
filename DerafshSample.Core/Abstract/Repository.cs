using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Derafsh.Business;
using Derafsh.Models;
using Derafsh.Models.RequestModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using DerafshSample.ModelsLibrary.Interfaces;
using DerafshSample.ModelsLibrary.ViewModels.General;
using DerafshSample.Services.Abstract;

namespace DerafshSample.Core.Abstract
{
    public abstract class Repository<T>
    {
        private readonly IDatabaseActions _databaseActions;
        private readonly IDatabaseConnectionService _connectionService;
        protected Repository(IDatabaseActions databaseActions,
            IDatabaseConnectionService databaseConnectionService)
        {
            _databaseActions = databaseActions;
            _connectionService = databaseConnectionService;
        }

        public async Task<IdentityResult> CreateAsync(T model,
            CancellationToken cancellationToken, SqlTransaction transaction = null)
        {
            using (var connection = _connectionService.Create())
            {
                var result = await _databaseActions.Insert<T>(connection, model, cancellationToken,
                transaction);
                return await Task.FromResult(result > 0
                    ? IdentityResult.Success
                    : IdentityResult.SubmitFailed($"خطایی در خواندن اطلاعات بوجود آمد است.")); ;
            }
        }


        public async Task<IdentityResult> DeleteAsync(int id, CancellationToken cancellationToken, SqlTransaction transaction = null)
        {
            using (var connection = _connectionService.Create())
            {
                var result = await _databaseActions
                                .SoftDelete<T>(connection, cancellationToken, id, transaction);
                return await Task.FromResult(result > 0
                    ? IdentityResult.Success
                    : IdentityResult.SubmitFailed($"خطایی در ثبت اطلاعات بوجود آمد است.")); ;
            }

        }

        public async Task<T> FindAsync(int id, SqlTransaction transaction = null)
        {
            using (var connection = _connectionService.Create())
            {
                var conditions = new QueryConditions();
                conditions.AddPublicCondition("IsDeleted", "IsDeleted = 0");
                var model = await _databaseActions.Find<T>(connection, id, conditions, transaction);
                return model;
            }
        }

        public async Task<IdentityResult> UpdateAsync(T model,
            CancellationToken cancellationToken,
            SqlTransaction transaction = null)
        {
            using (var connection = _connectionService.Create())
            {
                var result = await _databaseActions
                    .Update<T>(connection, model, cancellationToken);
                return await Task.FromResult(result > 0
                    ? IdentityResult.Success
                    : IdentityResult.SubmitFailed($"خطایی در ثبت اطلاعات بوجود آمد است.")); ;
            }
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken,
            SqlTransaction transaction = null)
        {
            using (var connection = _connectionService.Create())
            {
                var response = await _databaseActions.Count<T>(connection, cancellationToken,
                                "IsDeleted = 0", transaction);
                return response;
            }

        }

        public async Task<IEnumerable<T>> FilterAsync(
            QueryConditions conditions = null, FilterRequest filterRequest = null)
        {
            using (var connection = _connectionService.Create())
            {
                var items = await _databaseActions.Select<T>(connection, conditions, filterRequest);
                return items;
            }

        }

        public async Task<IdentityResult> AbstractAsync(
            string whereTerm = null, FilterRequest filterRequest = null)
        {
            using (var connection = _connectionService.Create())
            {
                var result = new IdentityResult();
                var items = await _databaseActions.Abstract<T>(connection, whereTerm, filterRequest);
                if (items == null)
                    result = IdentityResult.FetchFailed();
                else
                {
                    result.Succeeded = true;
                    result.Model = items;
                }
                return result;
            }

        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListItems(
            QueryConditions conditions)
        {
            using (var connection = _connectionService.Create())
            {
                var result = new List<SelectListItem>();
                var items = await _databaseActions.Select<T>(connection, conditions, null);
                foreach (var item in items)
                {
                    if (item is ISelectable selectable)
                    {
                        var selectItem = selectable.ConvertToSelectListItem();
                        if (!string.IsNullOrEmpty(selectItem.Text))
                            result.Add(selectItem);
                    }
                }
                return await Task.FromResult(result);
            }

        }
    }
}
