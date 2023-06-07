using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Contracts.StorageDto;
using WarehouseWeb.Contracts.StorageDTO;
using WarehouseWeb.Contracts.StorageItemDto;
using WarehouseWeb.Contracts.StorageItemDTO;
using WarehouseWeb.Data.Enum;
using WarehouseWeb.Model;
using WarehouseWeb.Model.Repository;

namespace WarehouseWeb.Services
{
    public class StorageService : IStorageService
    {
        private readonly IGenericRepository<StorageItem> _StorageItemRepository;
        private readonly IGenericRepository<Storage> _StorageRepository;
        private readonly IUnitOfWork _UnitOfWork;


        public StorageService(IGenericRepository<Storage> storageRepository, IUnitOfWork unitOfWork, IGenericRepository<StorageItem> storageItemRepository)
        {
            _StorageItemRepository = storageItemRepository;
            _StorageRepository = storageRepository;
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result> CreateStorage(StorageDto storageDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (storageDto == null)
            {
                result.StatusCode = StatusCodes.Status204NoContent;
                result.ErrorMessage = "Not able to insert null object";
                return result;
            }

            Storage storage = new Storage();
            storage.Description = storageDto.Description;
            storage.City = storageDto.City;

            bool addedStorage = await _StorageRepository.Add(storage);

            if (addedStorage == false)
            {
                result.Value = addedStorage;
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Object hasn't been added";
                return result;
            }


            _UnitOfWork.commit();
            result.Value = addedStorage;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> DeleteStorage(long storageId)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);
            if (storageId == 0)
            {
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Storage ID must be greater than 0";
                return result;
            }

            Storage storage = _StorageRepository.GetQueryable<Storage>()
                .Where(x => x.Id == storageId)
                .AsNoTracking()
                .FirstOrDefault();

            if (storage == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Storage can't be found, please enter correct ID";
                return result;
            }

            bool isDeleted = await _StorageRepository.Delete(storage);

            if (isDeleted == false)
            {
                result.Value = isDeleted;
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Object hasn't been deleted";
                return result;
            }

            _UnitOfWork.commit();
            result.Value = isDeleted;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> GetAllStorages(InputStorageDto inputStorageDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            List<GetAllStoragesResponse> listOfStoragesResponse = _StorageRepository.GetQueryable<Storage>()
                .Select(x => new GetAllStoragesResponse(x.Id, x.Description, x.City))
                .Skip((inputStorageDto.Page - 1) * inputStorageDto.PageSize)
                .Take(inputStorageDto.PageSize)
                .AsNoTracking()
                .ToList();
            if (listOfStoragesResponse == null)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.ErrorMessage = "Couldn't insert Storages into the list";
                return result;
            }
            result.Value = listOfStoragesResponse;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> GetStorageById(long storageId)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (storageId == 0)
            {
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Storage ID must be greater than 0";
                return result;
            }

            GetStorageByIdResponse storageResponse = _StorageRepository.GetQueryable<Storage>()
                .Where(x => x.Id == storageId)
                .Select(x => new GetStorageByIdResponse(x.Id, x.Description, x.City))
                .AsNoTracking()
                .FirstOrDefault();

            if (storageResponse == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Storage can't be found, please enter correct ID";
                return result;
            }

            result.Value = storageResponse;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> UpdateStorage(StorageDto storageDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (storageDto == null)
            {
                result.StatusCode = StatusCodes.Status204NoContent;
                result.ErrorMessage = "Not able to update null object";
                return result;
            }

            Storage storage = _StorageRepository.GetQueryable<Storage>()
                .Where(x => x.Id == storageDto.Id)
                .FirstOrDefault();

            storage.Description = storageDto.Description;
            storage.City = storageDto.City;
            //storage.Id = storageDto.Id;
            //Mozda jos koji atribut za posle
            bool isUpdated = await _StorageRepository.Update(storage);

            if (isUpdated == false)
            {
                result.Value = isUpdated;
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Object hasn't been updated";
                return result;
            }
            _UnitOfWork.commit();
            result.Value = isUpdated;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }


        //public async Task<Result> GetAllStorageItems()
        //{
        //    Result result = Result.Create(null, 500, null);

        //    List<StorageItem> listOfStorageItems = _StorageItemRepository.GetQueryable<StorageItem>()
        //        .Where()
        //    if (listOfStorageItems == null)
        //    {
        //        result.StatusCode = StatusCodes.Status404NotFound;
        //        result.ErrorMessage = "List not filled with storage items.";
        //        return result;
        //    }

        //    result.Value = listOfStorageItems;
        //    result.StatusCode = StatusCodes.Status200OK;
        //    return result;
        //}

        public async Task<Result> UpdateStorageItem(StorageItemDto storageItemDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (storageItemDto == null)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.ErrorMessage = "Object can't be null";
            }

            StorageItem storageItem = _StorageItemRepository.GetQueryable<StorageItem>()
                .Include(x => x.ListOfStorageItemInputOutputs)
                .Where(x => x.Id == storageItemDto.Id)
                .FirstOrDefault();

            //Storage storage = _StorageRepository.GetQueryable<Storage>()
            //    .Where(x => x.Id == storageItemDto.StorageId)
            //    .FirstOrDefault();

            //bool isThereStorageItem = _StorageItemRepository.GetQueryable<StorageItem>()
            //    .Any(x => x.Id == storageItemDto.Id);

            storageItem.ListOfStorageItemInputOutputs.RemoveAll(x => x.ItemActivity == ItemActivity.Input);

            StorageItemInputOutput storageItemInputOutput = MakeInputOutput(storageItemDto);

            storageItem.ListOfStorageItemInputOutputs.Add(storageItemInputOutput);
            storageItem.ProductId = storageItemDto.ProductId;
            storageItem.StorageId = storageItemDto.StorageId;

            storageItem.QuantityAmount = new Quantity()
            {
                QuantityAmount = storageItemInputOutput.QuantityAmount.QuantityAmount,
                UnitOfMeasurementId = storageItemInputOutput.QuantityAmount.UnitOfMeasurementId
            };

            bool isUpdated = await _StorageItemRepository.Update(storageItem);
            if (isUpdated == false)
            {
                result.ErrorMessage = "Couldn't update storage item";
                result.StatusCode = StatusCodes.Status400BadRequest;
            }
            _UnitOfWork.commit();
            result.Value = isUpdated;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> AddStorageItem(StorageItemDto storageItemDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (storageItemDto == null)
            {
                result.StatusCode = StatusCodes.Status204NoContent;
                result.ErrorMessage = "Not able to insert items into Storage(Null)";
                return result;
            }

            bool IsStorage = _StorageRepository.GetQueryable<Storage>()
                .AsNoTracking()
                .Any(x => x.Id == storageItemDto.StorageId);

            if (!IsStorage)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Storage can't be found, please enter correct ID";
                return result;
            }

            bool findStorageItem = _StorageItemRepository.GetQueryable<StorageItem>()
                .AsNoTracking()
                .Include(x => x.ListOfStorageItemInputOutputs)
                .Any(x => x.ProductId == storageItemDto.ProductId && x.StorageId == storageItemDto.StorageId);
                
            if (findStorageItem)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.ErrorMessage = "Storage item you trying to add already exists in database with given product, try to update his quantity";
                return result;
            }

            List<StorageItemInputOutput> listOfInputsOutputs = InsertAllOutputsAndInputs(storageItemDto);

            StorageItem storageItem = new StorageItem();
            storageItem.Id = storageItemDto.Id;

            storageItem.QuantityAmount = new Quantity()
            {
                QuantityAmount = storageItemDto.QuantityAmount.QuantityAmount, //OVO
                UnitOfMeasurementId = storageItemDto.QuantityAmount.UnitOfMeasurementId
            };

            storageItem.ProductId = storageItemDto.ProductId;
            storageItem.StorageId = storageItemDto.StorageId;
            storageItem.ListOfStorageItemInputOutputs = listOfInputsOutputs;

            //storage.StorageItemList.Add(storageItem); //Pa tu listu dopunjavam ovde

            bool isAdded = await _StorageItemRepository.Add(storageItem);

            if (isAdded == false)
            {
                result.Value = isAdded;
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Object hasn't been added";
                return result;
            }

            _UnitOfWork.commit();
            result.Value = isAdded;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> DeleteStorageItem(long storageItemId)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (storageItemId == 0)
            {
                result.ErrorMessage = "Id must be greater than 0";
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            StorageItem storageItem = _StorageItemRepository.GetQueryable<StorageItem>()
                .Where(x => x.Id == storageItemId)
                .AsNoTracking()
                .FirstOrDefault();

            if (storageItem == null)
            {
                result.ErrorMessage = "Couldn't find storageItem by given ID";
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
            }

            bool isDeleted = await _StorageItemRepository.Delete(storageItem);

            if (!isDeleted)
            {
                result.Value = isDeleted;
                result.ErrorMessage = "Couldn't delete storageItem";
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            _UnitOfWork.commit();
            result.Value = isDeleted;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }


        private StorageItemInputOutput MakeInputOutput(StorageItemDto storageItemDto)
        {
            StorageItemInputOutput storageItemInputOutput = new StorageItemInputOutput();
            storageItemInputOutput.ItemActivity = ItemActivity.Input;
            storageItemInputOutput.QuantityAmount = new Quantity()
            {
                QuantityAmount = storageItemDto.QuantityAmount.QuantityAmount,
                UnitOfMeasurementId = storageItemDto.QuantityAmount.UnitOfMeasurementId
            };
            return storageItemInputOutput;
        }

        private List<StorageItemInputOutput> InsertAllOutputsAndInputs(StorageItemDto storageItemDto)
        {
            StorageItemInputOutput storageInputOutput = new StorageItemInputOutput();
            storageInputOutput.QuantityAmount = storageItemDto.QuantityAmount;
            storageInputOutput.ItemActivity = ItemActivity.Input;

            List<StorageItemInputOutput> listOfInputsOutputs = new List<StorageItemInputOutput>();

            listOfInputsOutputs.Add(storageInputOutput);

            return listOfInputsOutputs;
        }



        public async Task<Result> GetAllStorageItemsFromStorage(long storageId, InputStorageItemDto inputStorageItemDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (storageId == 0)
            {
                result.ErrorMessage = "You must enter correct Id";
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
            }

            List<GetAllStorageItemsResponse> storageItemsListResponse = _StorageItemRepository.GetQueryable<StorageItem>()
                .Where(x => x.StorageId == storageId)
                .Select(x => new GetAllStorageItemsResponse(x.Id, x.QuantityAmount, x.ProductId, x.StorageId))
                .Skip((inputStorageItemDto.Page - 1) * inputStorageItemDto.PageSize)
                .Take(inputStorageItemDto.PageSize)
                .AsNoTracking()
                .ToList();

            if (storageItemsListResponse == null)
            {
                result.ErrorMessage = "Couldn't fill the list of storage items";
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            result.StatusCode = StatusCodes.Status200OK;
            result.Value = storageItemsListResponse;
            return result;
        }







        //private List<StorageItem> AddStorageItems(StorageDto storage)
        //{
        //    List<StorageItem> listOfStorageItems = new List<StorageItem>();

        //    foreach (var item in storage.StorageItemList)
        //    {
        //        var storageItem = new StorageItem();
        //        storageItem.Id = storage.Id;
        //        storageItem.ProductId = item.ProductId;
        //        storageItem.QuantityAmount = item.QuantityAmount;
        //        storageItem.ListOfStorageItemInputOutputs = AddInputOutputs(item);
        //        double sum = storageItem.ListOfStorageItemInputOutputs.Sum(x => x.QuantityAmount.QuantityAmount);
        //        storageItem.QuantityAmount.QuantityAmount = sum;

        //        listOfStorageItems.Add(storageItem);
        //    }
        //    if (listOfStorageItems == null)
        //    {
        //        return null;
        //    }
        //    return listOfStorageItems;
        //}

        //private List<StorageItemInputOutput> AddInputOutputs(StorageItemDto storageItemDto)
        //{
        //    List<StorageItemInputOutput> listOfInputOutputs = new List<StorageItemInputOutput>();

        //    foreach (var item in storageItemDto.ListOfStorageItemInputOutput)
        //    {
        //        var inputOutput = new StorageItemInputOutput();
        //        inputOutput.QuantityAmount = item.QuantityAmount;
        //        inputOutput.ItemActivity = item.ItemActivity;
        //        inputOutput.StorageItemId = storageItemDto.Id;

        //        if (inputOutput.QuantityAmount.QuantityAmount == 0)
        //        {
        //            return null;
        //        }

        //        listOfInputOutputs.Add(inputOutput);

        //    }
        //    if (listOfInputOutputs == null)
        //    {
        //        return null;
        //    }
        //    return listOfInputOutputs;

        //}

    }
}
