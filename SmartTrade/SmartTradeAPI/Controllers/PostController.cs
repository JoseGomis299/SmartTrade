using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTradeDTOs;

namespace SmartTradeAPI.Controllers
{
    [ApiController]
    [Route("SmartTradeAPI/Post")]
    public class PostController : ControllerBase
    {
        private readonly ISmartTradeService _service = new SmartTradeService();

        [HttpGet("GetAll", Name = "GetAll")]
        public IEnumerable<SimplePostDTO> Get()
        {
            string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

            return _service.GetPosts(loggedId);
        }

        [HttpGet("GetContaining",Name = "GetContaining")]
        public IEnumerable<SimplePostDTO> GetOnesThatContain(string content)
        {
            return _service.GetPostsFuzzyContain(content);
        }

        [HttpGet("GetAllNames", Name = "GetAllNames")]
        public IEnumerable<String> GetNames()
        {
            return _service.GetPostNames();
        }

        [HttpGet("GetById")]
        public PostDTO Get(int id)
        {
            return _service.GetPost(id);
        }

        [HttpPost("PublishPost")]
        public void Post([FromBody] PostDTO info)
        {
            string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;    
            _service.AddPost(info, loggedId);
        }

        [HttpPut("EditPost")]
        public void Put(int id, [FromBody] PostDTO info)
        {
            string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
            _service.EditPost(id, info, loggedId);
        }

        [HttpDelete("RemovePost")]
        public void Delete(int id)
        {
            _service.DeletePost(id);
        }
    }
}
