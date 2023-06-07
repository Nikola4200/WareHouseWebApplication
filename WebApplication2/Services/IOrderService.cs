using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Contracts;
using WarehouseWeb.Contracts.OrderDto;
using WarehouseWeb.Contracts.OrderDTO;
using WarehouseWeb.Contracts.OrderItemDTO;
using WarehouseWeb.Model;

namespace WarehouseWeb.Services
{
    public interface IOrderService
    {
        Task<Result> CreateOrderWithOrderItem(OrderItemDto orderItemDto);

        Task<Result> Create(OrderItemDto orderItemDto);

        Task<Result> DeleteOrder(long orderId);

        Task<Result> UpdateOrder(OrderDto orderDto);

        Task<Result> GetAllOrders(InputOrderDto inputOrderDto);

        Task<Result> GetOrderById(long orderId);

        Task<Result> GetOrderItems(long orderId, InputOrderItemDto inputOrderItemDto);

        Task<Result> AddOrderItems(OrderItemDto orderItemDto);

        Task<Result> DeleteOrderItemById(long orderItemId);

        Task<Result> UpdateOrderItem(OrderItemDto orderItemDto);

        //Da li treba update orderItem, u smislu da li je moguce da neko vidi svoj order
        //i kaze e hocu ovaj orderItem da promenim losu kolicinu sam stavio na primer
    }
}
