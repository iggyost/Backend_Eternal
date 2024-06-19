using Backend_Eternal.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Eternal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : Controller
    {
        public static EternalDbContext context = new EternalDbContext();

        [HttpGet]
        [Route("get/{userId}")]
        public ActionResult<IEnumerable<FavoritesView>> Get(int userId)
        {
            try
            {
                var data = context.FavoritesViews.Where(x => x.UserId == userId).ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("save/{userId}/{nftId}")]
        public ActionResult<IEnumerable<FavoritesView>> Save(int userId, int nftId)
        {
            try
            {

                Favorite newFavorite = new Favorite()
                {
                    UserId = userId,
                    NftId = nftId,
                };
                context.Favorites.Add(newFavorite);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("remove/{userId}/{nftId}")]
        public ActionResult<IEnumerable<FavoritesView>> Remove(int userId, int nftId)
        {
            try
            {
                var data = context.Favorites.Where(x => x.UserId == userId && x.NftId == nftId).FirstOrDefault();
                context.Favorites.Remove(data);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
