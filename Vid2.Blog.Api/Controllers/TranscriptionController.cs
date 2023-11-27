using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vid2.Blog.Api.Services;

namespace Vid2.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranscriptionController(ITranscriptionService transcriptionService) : ControllerBase
    {
        // GET: api/<TranscriptionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TranscriptionController>/5
        [HttpGet("{id}")]
        public Task<string> Get(string id)
        {
            return transcriptionService.GenerateBlogPost(id);
        }

        // POST api/<TranscriptionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TranscriptionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TranscriptionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
