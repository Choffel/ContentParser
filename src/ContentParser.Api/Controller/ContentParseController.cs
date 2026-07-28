using ContentParser.Api.Extensions;
using ContentParser.Application.DTOs;
using ContentParser.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContentParser.Api.Controller;

[ApiController]
[Route("api/v1")]
public class ContentParseController : ControllerBase
{
    private readonly IContentParseService _parseService;

    public ContentParseController(IContentParseService parseService)
    {
        _parseService = parseService;
    }

    [HttpPost("parse-content")]
    public IActionResult ParseContent([FromBody] ParseContentRequestDto request)
    {
        return _parseService.ProcessPayload(request).ToActionResult();
    }
}