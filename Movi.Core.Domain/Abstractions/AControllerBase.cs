using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Movi.Core.Domain.Abstractions;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class AControllerBase : ControllerBase
{
}
