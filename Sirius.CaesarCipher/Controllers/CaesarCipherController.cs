using Microsoft.AspNetCore.Mvc;
using Sirius.CaesarCipher.Interfaces;
using Sirius.CaesarCipher.Model;

namespace Sirius.CaesarCipher.Controllers;

[ApiController]
[Route("")]
public sealed class CaesarCipherController : ControllerBase
{
    private readonly ILogger<CaesarCipherController> _logger;
    private readonly ICaesarEncoder _encoder;
    private readonly IShiftRepository _shiftRepository;

    public CaesarCipherController(ILogger<CaesarCipherController> logger, ICaesarEncoder encoder, IShiftRepository repo)
    {
        _logger = logger;
        _encoder = encoder;
        _shiftRepository = repo;
    }

    [HttpPost("encode")]
    public async Task<IActionResult> PostEncode([FromBody] EncodeRequest requestData)
    {
        if (!ModelState.IsValid)
        {
            return new JsonResult(BadRequest());
        }

        EncodeResponse resp;
        try
        {
            resp = new EncodeResponse
            {
                Message = _encoder.Encode(requestData.Message, requestData.Rot)
            };
            await _shiftRepository.AddShiftAsync(requestData.Rot);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occured while processing encoding:");
            return new StatusCodeResult(500);
        }
        return new JsonResult(resp);
    }
    
    [HttpGet("decode")]
    public IActionResult GetDecode([FromQuery] DecodeRequest requestData)
    {
        if (!ModelState.IsValid)
        {
            return new JsonResult(BadRequest());
        }

        EncodeResponse resp;
        try
        {
            resp = new EncodeResponse
            {
                Message = _encoder.Decode(requestData.Message, requestData.Rot)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex.StackTrace);
            return new StatusCodeResult(500);
        }
        return new JsonResult(resp);
    }
}