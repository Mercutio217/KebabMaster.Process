using AutoMapper;
using KebabMaster.Process.Api.Interfaces;
using KebabMaster.Process.Api.Models;
using KebabMaster.Process.Api.Models.Orders;
using KebabMaster.Process.Domain.Entities;
using KebabMaster.Process.Domain.Exceptions;
using KebabMaster.Process.Domain.Filters;
using KebabMaster.Process.Domain.Interfaces;
using KebabMaster.Process.Domain.Models;

namespace KebabMaster.Process.Api.Services;

public class OrderApiService : IOrderApiService
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly IApplicationLogger _logger;
    private readonly IMenuRepository _menuRepository;
    
    public OrderApiService(
        IOrderService orderService,
        IMapper mapper,
        IApplicationLogger logger, 
        IMenuRepository menuRepository)
    {
        _orderService = orderService;
        _mapper = mapper;
        _logger = logger;
        _menuRepository = menuRepository;
    }

    public async Task CreateOrder(OrderRequest order)
    {
        await Execute(async () =>
        {
            _logger.LogPostStart(order);
            await _orderService.CreateOrder(_mapper.Map<Order>(order));

            _logger.LogPostEnd(order);
        });
    }

    public async Task<ApplicationResponse<OrderResponse>> GetOrdersAsync(OrderFilter filter) =>
        await Execute(async () =>
        {
            _logger.LogGetStart(filter);
            IEnumerable<Order> result = await _orderService.GetOrdersAsync(filter);
            _logger.LogGetEnd(filter);
            return new ApplicationResponse<OrderResponse>(
                _mapper.Map<IEnumerable<OrderResponse>>(result));
        });

    public async Task<OrderResponse> GetOrderById(int id) =>
        await Execute(async () =>
        {
            _logger.LogGetStart(id);
            Order result = await Execute<Order>(() => _orderService.GetOrderByIdAsync(id));
            _logger.LogGetEnd(id);

            return _mapper.Map<OrderResponse>(result);
        });

    public async Task DeleteOrder(int id) =>
        await Execute(async () =>
        {
            _logger.LogDeleteStart(id);
            await _orderService.DeleteOrder(id);
            _logger.LogDeleteEnd(id);
        });

    public async Task UpdateOrder(OrderUpdateRequest order) =>
        await Execute(async () =>
        {
            _logger.LogDeleteStart(order);
            var dto = _mapper.Map<OrderUpdateModel>(order);

            await _orderService.UpdateOrder(dto);

            _logger.LogDeleteEnd(order);
        });

    public async Task<IEnumerable<MenuItem>> GetMenuItems()
    {
        return await Execute(() => _menuRepository.GetMenuItems());
    }

    private async Task Execute(Func<Task> function)
    {
        try
        {
            await function();
        }
        catch (ApplicationValidationException validationException)
        {
            _logger.LogValidationException(validationException);
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
            throw;
        }
    }

    private async Task<T> Execute<T>(Func<Task<T>> function)
    {
        try
        {
            return await function();
        }
        catch (ApplicationValidationException validationException)
        {
            _logger.LogValidationException(validationException);
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
            throw;
        }
    }
}
