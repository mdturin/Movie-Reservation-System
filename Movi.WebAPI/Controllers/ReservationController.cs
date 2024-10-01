using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Movi.WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReservationController : ControllerBase
{
}
