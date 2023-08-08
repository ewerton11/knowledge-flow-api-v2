using knowledge_flow_api_v2.Data;
using knowledge_flow_api_v2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace knowledge_flow_api_v2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicController : ControllerBase
{
    private readonly DataContext _context;

    public TopicController(DataContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Topics topic)
    {
        if (topic.Title == string.Empty)
        {
            return BadRequest(new { ErrorMessage = "Invalid input: title is required." });
        }

        _context.Topics.Add(topic);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Topic successfully created.", TopicId = topic.Id });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Topics>>> Get()
    {
        var topics = await _context.Topics.ToListAsync();

        return Ok(topics);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Topics>> GetId(int id)
    {
        var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);

        if (topic == null)
        {
            return NotFound();
        }

        return Ok(topic);
    }

    // PUT api/<ValuesController1>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Topics>> Delete(int id)
    {
        var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);

        if (topic == null)
        {
            return NotFound();
        }

        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Topic deleted successfully.", TopicId = topic.Id });
    }
}