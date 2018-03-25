using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Derafsh.Models;
using Derafsh.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using DerafshSample.Core.Abstract;
using DerafshSample.ModelsLibrary.ViewModels.General;
using DerafshSample.Models;

namespace DerafshSample.Controllers
{
    public class BaseController<T> : Controller
    {
        private readonly IRepository<T> _repository;
        private readonly BaseModel _baseModel;
        public BaseController(IRepository<T> repository,BaseModel model)
        {
            _repository = repository;
            _baseModel = model;
        }

        [HttpGet]
        [ActionName("index")]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            var links = new List<LinkCol>()
            {
                new LinkCol()
                {
                    Url = Url.Action("Edit", _baseModel.Name) + "/{id}",
                    UrlTitle = "Edit",
                    ColTitle = "Actions"
                }
            };

            if (_baseModel.LinkRows != null)
            {
                links.AddRange(_baseModel.LinkRows);
            }

            int pageSize = 10;
            var filter = new FilterRequest(1,pageSize,"Id","Asc","");
            var result = await _repository.AbstractAsync($"[{_baseModel.Name}].IsDeleted = 0", filter);
            int totalNumberOfRows = await _repository.CountAsync(cancellationToken);
            var model = new DataTableComponentModel()
            {
                Name = _baseModel.Name,
                Title = _baseModel.PluralName + " list",
                TableId = _baseModel.Name.ToLower() + "-table",
                FetchUrl = Url.Action("Index", _baseModel.Name),
                CreateUrl = Url.Action("Create", _baseModel.Name),
                LinkRows = links,
                Entity = _baseModel.Name,
                DataTable = (System.Data.DataTable) result.Model,
                PageCount = totalNumberOfRows/ pageSize
            };
            return View("Base/_index", model);
        }

        [ActionName("indexfor")]
        public async Task<ActionResult> IndexFor(string entity, int entityId, 
            CancellationToken cancellationToken)
        {
            var links = new List<LinkCol>()
            {
                new LinkCol()
                {
                    Url = $"/Administration/{_baseModel.Name}/{{id}}/EditFor/{entity}",
                    UrlTitle = "Edit",
                    ColTitle = "Actions"
                }
            };

            if (_baseModel.LinkRows != null)
            {
                links.AddRange(_baseModel.LinkRows);
            }

            int pageSize = 10;
            var conditions = $"[{_baseModel.Name}].IsDeleted = 0";
            if (!string.IsNullOrEmpty(entity))
            {
                var attr = entity + "id";
                conditions += $" and {attr}={entityId}";
            }
            var filter = new FilterRequest(1, pageSize, "Id", "Asc", "");
            var result = await _repository.AbstractAsync(conditions, filter);
            int totalNumberOfRows = await _repository.CountAsync(cancellationToken);
            var model = new DataTableComponentModel()
            {
                Name = _baseModel.Name,
                Title = _baseModel.PluralName + " list",
                TableId = _baseModel.Name.ToLower() + "-table",
                FetchUrl = $"{Url.Action("IndexFor", _baseModel.Name)}/{{entity}}/{{entityId}}/",
                CreateUrl = $"{Url.Action("CreateFor", _baseModel.Name)}/{{entity}}/{{entityId}}/",
                Entity = entity,
                EntityId = entityId,
                LinkRows = links,
                DataTable = (System.Data.DataTable)result.Model,
                PageCount = totalNumberOfRows / pageSize
            };
            return View("Base/_index", model);
        }

        [ActionName("create")]
        public ActionResult Create()
        {
            var model = _repository.GetDefaultInstance();
            if (_baseModel.HasScript)
            {
                ViewData["HasScript"] = true;
            }
            ViewData["AshaPageInfoModel"] = _baseModel;
            var disables = new List<string>
            {                
                "IsActive",
                "IsDeleted"
            };
            if (_baseModel.DisableProperties != null)
                disables.AddRange(_baseModel.DisableProperties);
            ViewData["DisableProperties"] = disables;
            return View("Base/_Create", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(T model,
            CancellationToken cancellationToken)
        {
            try
            {
                await _repository.CreateAsync(model, cancellationToken);

                if (_baseModel.HasScript)
                {
                    ViewData["HasScript"] = true;
                }
                ViewData["AshaPageInfoModel"] = _baseModel;
                var disables = new List<string>
                {
                    "IsActive",
                    "IsDeleted"
                };
                if (_baseModel.DisableProperties != null)
                    disables.AddRange(_baseModel.DisableProperties);                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_baseModel.HasScript)
                {
                    ViewData["HasScript"] = true;
                }
                ViewBag.Response = new ActionMethodResponse
                {
                    Succeeded = false,
                    Description = "Error! Please try again."
                };
                ViewData["AshaPageInfoModel"] = _baseModel;
                var disables = new List<string>
                {
                    "IsActive",
                    "IsDeleted"
                };
                if (_baseModel.DisableProperties != null)
                    disables.AddRange(_baseModel.DisableProperties);
                return View("Base/_Create", model);
            }
        }

        
        [ActionName("createfor")]        
        public ActionResult CreateFor(string entity,int entityId)
        {
            var model = _repository.GetDefaultInstance(entity, entityId);
            if (_baseModel.HasScript)
            {
                ViewData["HasScript"] = true;
            }
            ViewData["AshaPageInfoModel"] = _baseModel;
            var disables = new List<string>
            {
                $"{entity.ToLower()}id",
                "IsActive",
                "IsDeleted"
            };
            if(_baseModel.DisableProperties!=null)
                disables.AddRange(_baseModel.DisableProperties);
            ViewData["DisableProperties"] = disables;
            return View("Base/_CreateFor", model);
        }


        [ActionName("edit")]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _repository.FindAsync(id);
            if (_baseModel.HasScript)
            {
                ViewData["HasScript"] = true;
            }
            ViewData["AshaPageInfoModel"] = _baseModel;
            if (_baseModel.DisableProperties != null)
                ViewData["DisableProperties"] = _baseModel.DisableProperties;
            return View("Base/_Edit",model);
        }

        [ActionName("editfor")]
        public async Task<ActionResult> EditFor(int id,string entity)
        {
            var model = await _repository.FindAsync(id);
            if (_baseModel.HasScript)
            {
                ViewData["HasScript"] = true;
            }
            ViewData["Entity"] = entity;
            ViewData["AshaPageInfoModel"] = _baseModel;
            var disables = new List<string>
            {
                $"{entity.ToLower()}id",
            };
            if (_baseModel.DisableProperties != null)
                disables.AddRange(_baseModel.DisableProperties);
            ViewData["DisableProperties"] = disables;
            return View("Base/_EditFor", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(T model, 
            CancellationToken cancellationToken)
        {
            try
            {
                if (_baseModel.HasScript)
                {
                    ViewData["HasScript"] = true;
                }
                await _repository.UpdateAsync(model, cancellationToken);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_baseModel.HasScript)
                {
                    ViewData["HasScript"] = true;
                }
                ViewBag.Response = new ActionMethodResponse
                {
                    Succeeded = false,
                    Description = "Error! Please try again."
                };
                ViewData["AshaPageInfoModel"] = _baseModel;
                return View("Base/_Edit", model);
            }
        }

        [ActionName("display")]
        public async Task<ActionResult> Display(int id)
        {
            var model = await _repository.FindAsync(id);                            
            return View("Base/_DetailsDisplay", model);
        }

        [ActionName("GetSelectListItems")]
        public async Task<JsonResult> GetSelectListItems()
        {
            var conditions = new QueryConditions();
            conditions.AddPublicCondition("IsActive","IsActive=1");
            conditions.AddPublicCondition("IsDeleted", "IsDeleted=0");
            var model = await _repository.GetSelectListItems(conditions);
            return Json(model);
        }
    }
}