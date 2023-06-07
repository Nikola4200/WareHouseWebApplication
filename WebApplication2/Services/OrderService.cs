using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Contracts;
using WarehouseWeb.Contracts.OrderDto;
using WarehouseWeb.Contracts.OrderDTO;
using WarehouseWeb.Contracts.OrderItemDTO;
using WarehouseWeb.Controllers;
using WarehouseWeb.Data.Enum;
using WarehouseWeb.Model;
using WarehouseWeb.Model.Repository;

namespace WarehouseWeb.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Order> _OrderRepository;
        private readonly IGenericRepository<OrderItem> _OrderItemRepository;
        //private readonly IGenericRepository<Product> _ProductRepository;
        private readonly IGenericRepository<StorageItem> _StorageItemRepository;

        public OrderService(IUnitOfWork unitOfWork, IGenericRepository<Order> orderRepository
            , IGenericRepository<OrderItem> orderItemRepository, IGenericRepository<StorageItem> storageItemRepository)
        {
            _unitOfWork = unitOfWork;
            _OrderRepository = orderRepository;
            //_ProductRepository = productRepository;
            _OrderItemRepository = orderItemRepository;
            _StorageItemRepository = storageItemRepository;
        }

        public async Task<Result> Create(OrderItemDto orderItemDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            bool activeOrder = _OrderRepository.GetQueryable<Order>()
                .AsNoTracking()
                .Any(x => x.UserId == 2 && x.Status == Status.Active);

            if (activeOrder)
            {
                result = await AddOrderItems(orderItemDto);
                return result;
            }

            result = await CreateOrderWithOrderItem(orderItemDto);
            return result;
        }

        public async Task<Result> CreateOrderWithOrderItem(OrderItemDto orderItemDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (orderItemDto == null)
            {
                result.StatusCode = StatusCodes.Status204NoContent;
                result.ErrorMessage = "Not able to insert null object";
                return result;
            }

            StorageItem storageItem = _StorageItemRepository.GetQueryable<StorageItem>()
                  .Include(x => x.ListOfStorageItemInputOutputs)
                  .Where(x => x.ProductId == orderItemDto.ProductId)
                  .FirstOrDefault();

            if (orderItemDto.QuantityAmount.QuantityAmount > storageItem.QuantityAmount.QuantityAmount)
            {
                result.ErrorMessage = "There is not enough items!";
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            StorageItemInputOutput storageItemInputOutput = makeInputOutput(orderItemDto, storageItem);

            if (storageItemInputOutput == null)
            {
                result.ErrorMessage = "Error in makin StorageItemInputOutput";
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            storageItem.ListOfStorageItemInputOutputs.Add(storageItemInputOutput);
            storageItem.SetQuantityAmount();

            OrderItem orderItem = new OrderItem();
            orderItem.ProductId = orderItemDto.ProductId;
            orderItem.Price = orderItemDto.Price;
            orderItem.QuantityAmount = orderItemDto.QuantityAmount;

            Order order = new Order();
            order.OrderItemList = new List<OrderItem>();
            order.OrderItemList.Add(orderItem);

            order.SetTotalPriceForOrder();

            order.Status = Status.Active;
            order.UserId = 2;

            bool isAdded = await _OrderRepository.Add(order);

            if (isAdded == false)
            {
                result.Value = isAdded;
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Object hasn't been added";
                return result;
            }

            _unitOfWork.commit();
            result.Value = isAdded;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> AddOrderItems(OrderItemDto orderItemDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            Order order = _OrderRepository.GetQueryable<Order>()
                .Include(x => x.OrderItemList)
                .ThenInclude(x => x.Product)
                .Where(x => x.UserId == 2 && x.Status == Status.Active)
                .FirstOrDefault();

            order.OrderItemList.All(x => { x.Price = x.Product.Price; return true; });

            StorageItem storageItem = _StorageItemRepository.GetQueryable<StorageItem>()
                  .Include(x => x.ListOfStorageItemInputOutputs)
                  .Where(x => x.ProductId == orderItemDto.ProductId)
                  .FirstOrDefault();

            if (orderItemDto.QuantityAmount.QuantityAmount > storageItem.QuantityAmount.QuantityAmount)
            {
                result.ErrorMessage = "There is not enough items!";
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            StorageItemInputOutput storageItemInputOutput = makeInputOutput(orderItemDto, storageItem);

            if (storageItemInputOutput == null)
            {
                result.ErrorMessage = "Error in makin StorageItemInputOutput";
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            storageItem.ListOfStorageItemInputOutputs.Add(storageItemInputOutput);

            storageItem.SetQuantityAmount();

            bool findOrderItem = order.OrderItemList.Any(x => x.OrderId == order.Id && x.ProductId == orderItemDto.ProductId);

            if (findOrderItem)
            {
                result.ErrorMessage = "There is already a orderItem with given product.Try changing quantity.";
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
            }

            OrderItem orderItem = new OrderItem();
            orderItem.OrderId = order.Id;
            orderItem.ProductId = orderItemDto.ProductId;
            orderItem.Price = orderItemDto.Price;

            orderItem.QuantityAmount = new Quantity()
            {
                QuantityAmount = orderItemDto.QuantityAmount.QuantityAmount,
                UnitOfMeasurementId = orderItemDto.QuantityAmount.UnitOfMeasurementId
            };

            order.OrderItemList.Add(orderItem);
            order.SetTotalPriceForOrder();

            bool isAdded = await _OrderItemRepository.Add(orderItem);

            if (!isAdded)
            {
                result.ErrorMessage = "Couldn't Add orderItem!";
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            _unitOfWork.commit();
            result.Value = isAdded;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }


        //public async Task<Result> CreateOrderWithOrderItems(OrderDto orderDto) 
        //{
        //    Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null);

        //    if (orderDto == null)
        //    {
        //        result.StatusCode = StatusCodes.Status204NoContent;
        //        result.ErrorMessage = "Not able to insert null object";
        //        return result;
        //    }

        //    //if (orderDto.Id == 0)
        //    //{
        //    //    result.StatusCode = StatusCodes.Status400BadRequest;
        //    //    result.ErrorMessage = "Id must be greater than 0";
        //    //    return result;
        //    //}
        //    //bool isOrderExist = _OrderRepository.GetQueryable<Order>()
        //    //    .Where(x => x.UserId == orderDto.UserId)
        //    //    .Any();

        //    //if (isOrderExist)
        //    //{
        //    //    AddOrderItems(OrderItem)
        //    //}

        //    List<OrderItem> listOfOrderItems = AddOrderItemsToGivenOrder(orderDto);

        //    if (listOfOrderItems == null)
        //    {
        //        result.StatusCode = StatusCodes.Status400BadRequest;
        //        result.ErrorMessage = "Not able to insert order items in order";
        //        return result;
        //    }

        //    double totalPrice = listOfOrderItems.Sum(x => x.QuantityAmount.QuantityAmount * x.Price);

        //    Order order = new Order();
        //    order.Adress = orderDto.Adress;
        //    order.City = orderDto.City;
        //    order.UserId = orderDto.UserId;
        //    order.OrderItemList = (List<OrderItem>)listOfOrderItems;
        //    order.Price = totalPrice;

        //    bool isAdded = await _OrderRepository.Add(order);

        //    if (isAdded == false)
        //    {
        //        result.Value = isAdded;
        //        result.StatusCode = StatusCodes.Status406NotAcceptable;
        //        result.ErrorMessage = "Object hasn't been added";
        //        return result;
        //    }

        //    _unitOfWork.commit();
        //    result.ErrorMessage = null;
        //    result.Value = isAdded;
        //    result.StatusCode = StatusCodes.Status200OK;
        //    return result;
        //}

        public async Task<Result> DeleteOrder(long orderId)
        {
            //DELETE ORDER NE TREBA, Nema smisla.
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (orderId == 0)
            {
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Order ID must be greater than 0";
                return result;
            }

            Order order = _OrderRepository.GetQueryable<Order>()
                .Where(x => x.Id == orderId)
                .AsNoTracking()
                .FirstOrDefault();
            //Ovde je menjano nesto ako zabaguje DeleteOrder

            if (order == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Order can't be found, please enter correct ID";
                return result;
            }

            bool isDeleted = await _OrderRepository.Delete(order);

            if (isDeleted == false)
            {
                result.Value = isDeleted;
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Object hasn't been deleted";
                return result;
            }

            _unitOfWork.commit();
            result.Value = isDeleted;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> GetAllOrders(InputOrderDto inputOrderDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            List<GetAllOrdersResponse> listOfOrders = _OrderRepository.GetQueryable<Order>()
                .AsNoTracking()
                .Select(x => new GetAllOrdersResponse(x.Id, x.DeliveryDate, x.City, x.Adress, x.Price))
                .Skip((inputOrderDto.Page - 1) * inputOrderDto.PageSize)
                .Take(inputOrderDto.PageSize)
                .ToList();

            if (listOfOrders == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Orders can't be found";
                return result;
            }

            result.Value = listOfOrders;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> GetOrderById(long orderId)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);
            if (orderId == 0)
            {
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Order ID must be greater than 0";
                return result;
            }

            GetOrderByIdResponse orderResponse = _OrderRepository.GetQueryable<Order>()
                .AsNoTracking()
                .Where(x => x.Id == orderId)
                .Select(x => new GetOrderByIdResponse(x.Id, x.DeliveryDate, x.City, x.Adress, x.Price))
                .FirstOrDefault();

            if (orderResponse == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Order can't be found, please enter correct ID";
                return result;
            }
            result.Value = orderResponse;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> UpdateOrder(OrderDto orderDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (orderDto == null)
            {
                result.StatusCode = StatusCodes.Status204NoContent;
                result.ErrorMessage = "Not able to update null object";
                return result;
            }

            Order order = _OrderRepository.GetQueryable<Order>()
                .Include(x => x.OrderItemList)
                .ThenInclude(x => x.Product)
                .Where(x => x.Id == orderDto.Id)
                .FirstOrDefault();

            // Order order = await _OrderRepository.GetById(orderDto.Id);

            if (order == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "Not able to find Order with given Id";
                return result;
            }

            // double totalPrice = listOfOrderItems.Sum(x => x.QuantityAmount.QuantityAmount * x.Price);

            order.Adress = orderDto.Adress;
            order.City = orderDto.City;
            order.DeliveryDate = orderDto.DeliveryDate;
            order.UserId = orderDto.UserId;
            //order.Price = orderDto.Price; //Ne bi trebalo da moze da menja price jer je po pravilu o odnosu na proizvode/orderIteme izracunata
            order.OrderItemList.All(x => { x.Price = x.Product.Price; return true; });
            order.SetTotalPriceForOrder();

            bool isUpdated = await _OrderRepository.Update(order);

            if (isUpdated == false)
            {
                result.Value = isUpdated;
                result.StatusCode = StatusCodes.Status406NotAcceptable;
                result.ErrorMessage = "Object hasn't been updated";
                return result;
            }

            _unitOfWork.commit();
            result.Value = isUpdated;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> GetOrderItems(long orderId, InputOrderItemDto inputOrderItemDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);

            if (orderId == 0)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.ErrorMessage = "Id must be greater than 0";
                return result;
            }

            List<GetOrderItemsResponse> listOfOrderItems = _OrderItemRepository.GetQueryable<OrderItem>()
                .AsNoTracking()
                .Where(x => x.OrderId == orderId)
                .Select(x => new GetOrderItemsResponse(x.Id, x.QuantityAmount, x.OrderId, x.ProductId))
                .Skip((inputOrderItemDto.Page - 1) * inputOrderItemDto.PageSize)
                .Take(inputOrderItemDto.PageSize)
                .ToList();

            if (listOfOrderItems == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.ErrorMessage = "ListOfOrderItems not found";
                return result;
            }

            result.Value = listOfOrderItems;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> DeleteOrderItemById(long orderItemId)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);
            if (orderItemId == 0)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.ErrorMessage = "Id must be greater than 0!";
                return result;
            }

            Order order = _OrderRepository.GetQueryable<Order>()
                .Include(x => x.OrderItemList)
                .ThenInclude(x => x.Product)
                .Where(x => x.Status == Status.Active && x.UserId == 2)
                .FirstOrDefault();

            if (order == null)
            {
                result.ErrorMessage = "Order couldn't be found!";
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
            }

            OrderItem orderItem = order.OrderItemList.Find(x => x.Id == orderItemId);

            if (orderItem == null)
            {
                result.ErrorMessage = "OrderItem couldn't be find in orderItem list from order";
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
            }

            StorageItem storageItem = _StorageItemRepository.GetQueryable<StorageItem>()
               .Include(x => x.ListOfStorageItemInputOutputs)
               .Where(x => x.ProductId == orderItem.ProductId)
               .FirstOrDefault();

            if (storageItem == null)
            {
                result.ErrorMessage = "Storage item isn't found!";
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
            }

            StorageItemInputOutput storageItemInputOutput = storageItem.ListOfStorageItemInputOutputs.FindLast(x => x.StorageItemId == storageItem.Id && x.ItemActivity == ItemActivity.Output
                                                    && x.CreatedBy == 2);
            storageItem.ListOfStorageItemInputOutputs.Remove(storageItemInputOutput);
            storageItem.SetQuantityAmount();

            //OrderItem orderItem = _OrderItemRepository.GetQueryable<OrderItem>()
            //    .AsNoTracking()
            //    .Include(x=>x.Product)
            //    .Where(x => x.Id == orderItemId)
            //    .FirstOrDefault();

            bool isDeleted = order.OrderItemList.Remove(orderItem);

            if (!isDeleted)
            {
                result.Value = isDeleted;
                result.ErrorMessage = "OrderItem is not deleted!";
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            order.OrderItemList.All(x => { x.Price = x.Product.Price; return true; });
            order.SetTotalPriceForOrder();

            if (order.OrderItemList.Count == 0)
            {
                await _OrderRepository.Delete(order);
            }

            _unitOfWork.commit();
            result.Value = isDeleted;
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        public async Task<Result> UpdateOrderItem(OrderItemDto orderItemDto)
        {
            Result result = Result.Create(null, StatusCodes.Status500InternalServerError, null, 0);
            if (orderItemDto == null)
            {
                result.ErrorMessage = "OrderItemDto is null";
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            //Order order = _OrderRepository.GetQueryable<Order>()
            //    .Include(x=>x.OrderItemList)
            //    .Where(x=> x.)

            Order order = _OrderRepository.GetQueryable<Order>()
                .Include(x => x.OrderItemList)
                .ThenInclude(x => x.Product)
                .Where(x => x.Status == Status.Active && x.UserId == 2)
                .FirstOrDefault();

            OrderItem orderItem = order.OrderItemList.Find(x => x.Id == orderItemDto.Id);

            StorageItem storageItem = _StorageItemRepository.GetQueryable<StorageItem>()
                .Include(x => x.ListOfStorageItemInputOutputs)
                .Where(x => x.ProductId == orderItem.ProductId)
                .FirstOrDefault();

            StorageItemInputOutput storageItemInputOutput = storageItem.ListOfStorageItemInputOutputs.FindLast
                            (x => x.StorageItemId == storageItem.Id && x.ItemActivity == ItemActivity.Output && x.CreatedBy == 2);

            if ((orderItemDto.QuantityAmount.QuantityAmount-storageItemInputOutput.QuantityAmount.QuantityAmount) > storageItem.QuantityAmount.QuantityAmount) 
            {
                result.ErrorMessage = "There is not enough products in storage!";
                result.StatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            storageItem.ListOfStorageItemInputOutputs.Remove(storageItemInputOutput);

            storageItemInputOutput = makeInputOutput(orderItemDto, storageItem);
            storageItem.ListOfStorageItemInputOutputs.Add(storageItemInputOutput);
            storageItem.SetQuantityAmount();

            if (orderItem == null)
            {
                result.ErrorMessage = "OrderItem is not found!";
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
            }

            //order.OrderItemList.Remove(orderItem);

            int index = order.OrderItemList.FindIndex(x => x.Id == orderItemDto.Id);

            if (index == -1)
            {
                result.ErrorMessage = "OrderItem not found in order";
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
            }

            //order.OrderItemList[index].CreatedBy = 2;
            order.OrderItemList[index].QuantityAmount = new Quantity
            {
                QuantityAmount = orderItemDto.QuantityAmount.QuantityAmount,
                UnitOfMeasurementId = orderItemDto.QuantityAmount.UnitOfMeasurementId
            };

            order.OrderItemList.All(x => { x.Price = x.Product.Price; return true; });
            order.SetTotalPriceForOrder();

            _unitOfWork.commit();
            result.StatusCode = StatusCodes.Status200OK;
            result.Value = true;
            return result;
        }

        private StorageItemInputOutput makeInputOutput(OrderItemDto orderItemDto, StorageItem storageItem)
        {
            StorageItemInputOutput storageItemInputOutput = new StorageItemInputOutput();

            storageItemInputOutput.StorageItemId = storageItem.Id;
            storageItemInputOutput.ItemActivity = ItemActivity.Output;
            storageItemInputOutput.QuantityAmount = orderItemDto.QuantityAmount;
            storageItemInputOutput.CreatedBy = 2;

            return storageItemInputOutput;
        }

        //private List<OrderItem> AddOrderItemsToGivenOrder(OrderDto orderDto)
        //{
        //    List<OrderItem> listOfOrderItems = new List<OrderItem>();

        //    foreach (var item in orderDto.OrderItemList)
        //    {
        //        var orderItem = new OrderItem();
        //        orderItem.OrderId = orderDto.Id;
        //        orderItem.QuantityAmount = item.QuantityAmount;
        //        orderItem.ProductId = item.ProductId;
        //        orderItem.Price = item.Price;

        //        listOfOrderItems.Add(orderItem);
        //    }

        //    if (listOfOrderItems == null)
        //    {
        //        return null;
        //    }

        //    return listOfOrderItems;
        //}

    }
}

