using CreatorFund.Application.Models;
using CreatorFund.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DemoController : ControllerBase
{
    private readonly DemoService _demoService;

    public DemoController(DemoService demoService)
    {
        _demoService = demoService;
    }

    // GET: api/demo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Demo>>> GetAll()
    {
        var demos = await _demoService.GetAllAsync();
        return Ok(demos);
    }

    // POST: api/demo
    [HttpPost]
    public async Task<ActionResult> Add([FromBody] Demo demo)
    {
        if (demo == null)
        {
            return BadRequest("Demo object cannot be null.");
        }

        await _demoService.AddAsync(demo);
        return CreatedAtAction(nameof(GetAll), new { id = demo.Id }, demo);
    }
}
