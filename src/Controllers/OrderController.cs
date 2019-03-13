using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using order.APIModels;
using order.DAL;
using order.Services;

namespace order.Controllers
{
  [Route("order")]
  public class OrderController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public OrderController(
      IUnitOfWork unitOfWork
    )
    {
      _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [Route("order")]
    public ActionResult<OrderAPIModel> FindOrder(
      [FromServices] SessionService sessionService,
      [FromServices] OrderService orderService
    )
    {
      var order = orderService.Find(
        sessionService.Session.Id
      );

      return Ok(Mapper.Map<OrderAPIModel>(order));
    }

    [HttpPost]
    [Route("order/create")]
    public ActionResult<OrderAPIModel> CreateOrder(
      [FromServices] SessionService sessionService,
      [FromServices] OrderService orderService
    )
    {
      var order = orderService.Create(
        sessionService.Session.Id
      );

      _unitOfWork.Commit();

      return Ok(Mapper.Map<OrderAPIModel>(order));
    }
  }
}
