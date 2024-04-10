using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTradeDTOs;

namespace SmartTradeAPI.Controllers
{
    [ApiController]
    [Route("SmartTradeAPI/Post")]
    public class PostController : ControllerBase
    {
        [HttpGet("GetAll", Name = "GetAll")]
        public IEnumerable<PostDTO> Get()
        {
            ISmartTradeService service = new SmartTradeService();
            string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

            return service.GetPosts(loggedId);
        }

        [HttpGet("GetContaining",Name = "GetContaining")]
        public IEnumerable<PostDTO> GetOnesThatContain(string content)
        {
            ISmartTradeService service = new SmartTradeService();

            return service.GetPostsFuzzyContain(content);
        }

        [HttpGet("GetAllNames", Name = "GetAllNames")]
        public IEnumerable<String> GetNames()
        {
            ISmartTradeService service = new SmartTradeService();

            return service.GetPostsNamesStartWith("", Int32.MaxValue);
        }

        [HttpGet("GetById")]
        public PostDTO Get(int id)
        {
            ISmartTradeService service = new SmartTradeService();
            return service.GetPost(id);
        }

        [HttpPost("PublishPost")]
        public void Post([FromBody] PostDTO info)
        {
            ISmartTradeService service = new SmartTradeService();
            string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;    

            service.AddPost(info, loggedId);
        }

        [HttpPut("EditPost")]
        public void Put(int id, [FromBody] PostDTO info)
        {
            SmartTradeService service = new SmartTradeService();
            string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

            service.EditPost(id, info, loggedId);
        }

        [HttpDelete("RemovePost")]
        public void Delete(int id)
        {
            SmartTradeService service = new();
            service.DeletePost(id);
        }
    }
}
