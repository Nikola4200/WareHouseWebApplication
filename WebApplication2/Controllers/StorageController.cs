using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Contracts;
using WarehouseWeb.Contracts.StorageDto;
using WarehouseWeb.Contracts.StorageDTO;
using WarehouseWeb.Contracts.StorageItemDto;
using WarehouseWeb.Contracts.StorageItemDTO;
using WarehouseWeb.Model;
using WarehouseWeb.Services;

namespace WarehouseWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : BaseController
    {
        private IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost]
        [Route("api/controller/CreateStorage")]
        public async Task<ActionResult<Result<Storage>>> CreateStorage(StorageDto storageDto)
        {
            Result result = await _storageService.CreateStorage(storageDto);
            return GetResultByStatusCode(result);

        }

        [HttpGet] //("{id}")
        [Route("api/controller/GetStorageById")]
        public async Task<ActionResult<Result<Storage>>> GetStorageById(long id)
        {
            Result result = await _storageService.GetStorageById(id);
            return GetResultByStatusCode(result);
        }

        [HttpDelete]
        [Route("api/controller/DeleteStorage")]
        public async Task<ActionResult<Result<Storage>>> DeleteStorage(long storageId)
        {
            Result result = await _storageService.DeleteStorage(storageId);
            return GetResultByStatusCode(result);
        }

        //[HttpPut]
        //public async Task<ActionResult<Result<StorageItem>>> AddStorageItemToStorage(StorageItemDto storageItemDto)
        //{
        //    Result result = await _storageService.AddStorageItemsToStorage(storageItemDto);
        //    return GetResultByStatusCode(result);
        //}

        [HttpPut]
        [Route("api/controller/UpdateStorage")]
        public async Task<ActionResult<Result<Storage>>> UpdateStorage(StorageDto storageDto)
        {
            Result result = await _storageService.UpdateStorage(storageDto);
            return GetResultByStatusCode(result);
        }

        [HttpGet]
        [Route("api/controller/GetAllStorages")]
        public async Task<ActionResult<Result<Storage>>> GetAllStorages(int page, int pageSize)
        {
            InputStorageDto inputStorageDto = new InputStorageDto()
            {
                Page = page,
                PageSize = pageSize
            };

            Result result = await _storageService.GetAllStorages(inputStorageDto);
            return GetResultByStatusCode(result);
        }

        //[HttpGet]
        //public async Task<ActionResult<Result<StorageItem>>> GetAllStorageItemsFromStorage(long storageId)
        //{
        //    Result result = await _storageService.GetAllStorageItemsFromStorage(storageId);
        //    return GetResultByStatusCode(result);
        //}

        [HttpGet]
        [Route("api/controller/GetAllStorageItemsFromStorage")]
        public async Task<ActionResult<Result<StorageItem>>> GetAllStorageItemsFromStorage(long storageId, int page, int pageSize)
        {
            InputStorageItemDto inputStorageItemDto = new InputStorageItemDto()
            {
                Page = page,
                PageSize = pageSize
            };

            Result result = await _storageService.GetAllStorageItemsFromStorage(storageId, inputStorageItemDto);
            return GetResultByStatusCode(result);
        }

        [HttpPost]
        [Route("api/controller/AddStorageItem")]
        public async Task<ActionResult<Result<StorageItem>>> AddStorageItem([FromBody]StorageItemDto storageItemDto) //Proveri da l mora frombody
        {
            Result result = await _storageService.AddStorageItem(storageItemDto);
            return GetResultByStatusCode(result);
        }

        [HttpPut]
        [Route("api/controller/UpdateStorageItem")]
        public async Task<ActionResult<Result<StorageItem>>> UpdateStorageItem([FromBody]StorageItemDto storageItemDto)
        {
            Result result = await _storageService.UpdateStorageItem(storageItemDto);
            return GetResultByStatusCode(result);
        }

        [HttpDelete]
        [Route("api/controller/DeleteStorageItem")]
        public async Task<ActionResult<Result<StorageItem>>> DeleteStorageItem(long storageItemId)
        {
            Result result = await _storageService.DeleteStorageItem(storageItemId);
            return GetResultByStatusCode(result);
        }
    }
}
