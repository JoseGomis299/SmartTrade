using Microsoft.AspNetCore.Mvc;
using SmartTradeAPI.Library.Persistence.NewFolder;
using SmartTradeLib.BusinessLogic;

namespace SmartTradeAPI.Controllers
{
    [ApiController]
    [Route("SmartTradeAPI/Post")]
    public class PostController : ControllerBase
    {
        [HttpGet(Name = "Get")]
        public IEnumerable<PostDTO> Get()
        {
            SmartTradeService service = new();
            return service.GetPosts();
        }
    }
}
