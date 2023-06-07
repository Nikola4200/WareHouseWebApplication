using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Contracts;
using WarehouseWeb.Contracts.OrderDto;
using WarehouseWeb.Contracts.OrderDTO;
using WarehouseWeb.Contracts.OrderItemDTO;
using WarehouseWeb.Model;
using WarehouseWeb.Services;

namespace WarehouseWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("api/controller/GetAllOrders")]
        public async Task<ActionResult<IEnumerable<Result<Order>>>> GetAllOrders(int page, int pageSize)
        {
            InputOrderDto input = new InputOrderDto()
            {
                Page = page,
                PageSize = pageSize
            };

            Result result = await _orderService.GetAllOrders(input);
            return GetResultByStatusCode(result);
        }

        [HttpGet]
        [Route("api/controller/GetOrderById")]
        public async Task<ActionResult<Result<Order>>> GetOrderById(long orderId)
        {
            Result result = await _orderService.GetOrderById(orderId);
            return GetResultByStatusCode(result);
        }

        [HttpPost]
        [Route("api/controller/Create")]
        public async Task<ActionResult<Result<Order>>> Create(OrderItemDto orderItemDto)
        {
            Result result = await _orderService.Create(orderItemDto);
            return GetResultByStatusCode(result);
        }

        [HttpDelete]
        [Route("api/controller/DeleteOrder")]
        public async Task<ActionResult<Result<Order>>> DeleteOrder(long id)
        {
            Result result = await _orderService.DeleteOrder(id);
            return GetResultByStatusCode(result);
        }

        [HttpPut]
        [Route("api/controller/UpdateOrder")]
        public async Task<ActionResult<Result<Order>>> UpdateOrder(OrderDto orderDto)
        {
            Result result = await _orderService.UpdateOrder(orderDto);
            return GetResultByStatusCode(result);
        }

        [HttpGet]
        [Route("api/controller/GetOrderItems")]
        public async Task<ActionResult<Result<OrderItem>>> GetOrderItems(long orderId, int page, int pageSize)
        {
            InputOrderItemDto inputOrderItemDto = new InputOrderItemDto()
            {
                Page = page,
                PageSize = pageSize
            };

            Result result = await _orderService.GetOrderItems(orderId, inputOrderItemDto);
            return GetResultByStatusCode(result);
        }

        [HttpDelete]
        [Route("api/controller/DeleteOrderItemById")]
        public async Task<ActionResult<Result<OrderItem>>> DeleteOrderItemById(long orderItemId)
        {
            Result result = await _orderService.DeleteOrderItemById(orderItemId);
            return GetResultByStatusCode(result);
        }

        [HttpPut]
        [Route("api/controller/UpdateOrderItem")]
        public async Task<ActionResult<Result<OrderItem>>> UpdateOrderItem(OrderItemDto orderItemDto)
        {
            Result result = await _orderService.UpdateOrderItem(orderItemDto);
            return GetResultByStatusCode(result);
        }
        //[HttpPost]
        //[Route("api/controller/AddOrderItems")]
        //public async Task<ActionResult<Result<Order>>> AddOrderItems(OrderItemDto orderItemDto)
        //{
        //    Result result = await _orderService.AddOrderItems(orderItemDto);
        //    return GetResultByStatusCode(result);
        //}
    }
}
