using Backend_Eternal.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Eternal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NftViewController : Controller
    {
        public static EternalDbContext context = new EternalDbContext();

        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<NftView>> GetAllNft()
        {
            try
            {
                var data = context.NftViews.ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("get/user/{userId}")]
        public ActionResult<IEnumerable<NftView>> GetUserNft(int UserId)
        {
            try
            {
                var data = context.NftViews.Where(x => x.UserId == UserId).ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
