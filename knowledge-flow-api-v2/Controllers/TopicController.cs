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

        return Ok(new { Message = "Topic created successfully." });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Topics>>> Get(int? id)
    {
        if (id.HasValue)
        {
            var parentTopic = await _context.Topics.Where(t => t.ParentId == id).ToListAsync();

            return Ok(parentTopic);
        }

        var nonChildTopics = await _context.Topics.Where(t => t.ParentId == null).ToListAsync();

        return Ok(nonChildTopics);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Topics>> GetId(int id)
    {
        var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);

        if (topic == null)
        {
            return NotFound(new { ErrorMessage = "Topic not found." });
        }

        return Ok(topic);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Topics topic)
    {
        if (topic == null)
        {
            return NotFound(new { ErrorMessage = "Invalid input data." });
        }

        var existingTopic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);

        if (existingTopic == null)
        {
            return NotFound(new { ErrorMessage = "Topic not found." });
        }

        existingTopic.Title = topic.Title;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Topic updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);

        if (topic == null)
        {
            return NotFound(new { ErrorMessage = "Topic not found." });
        }

        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Topic deleted successfully." });
    }
}