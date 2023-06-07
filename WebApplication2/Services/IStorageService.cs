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

namespace WarehouseWeb.Services
{
    public interface IStorageService
    {

        Task<Result> CreateStorage(StorageDto storage);

        Task<Result> DeleteStorage(long idStorage);

        Task<Result> UpdateStorage(StorageDto storage);

        Task<Result> GetAllStorages(InputStorageDto inputStorageDto);

        Task<Result> GetStorageById(long storageId);

        Task<Result> AddStorageItem(StorageItemDto storageItemDto);

        //Task<Result> AddStorageItemsToStorage(StorageItemDto storageItemDto);

        Task<Result> GetAllStorageItemsFromStorage(long storageId, InputStorageItemDto inputStorageItemDto);

        Task<Result> UpdateStorageItem(StorageItemDto storageItemDto);

        Task<Result> DeleteStorageItem(long storageItemId);
    }
}
