using Backend_Eternal.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Eternal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersNftController : Controller
    {
        public static EternalDbContext context = new EternalDbContext();

        [HttpGet]
        [Route("update/{userId}/{nftId}/{tokenCount}")]
        public ActionResult<IEnumerable<Wallet>> Update(int userId, int nftId, decimal tokenCount)
        {
            try
            {
                var data = context.UsersNfts.Where(x => x.NftId == nftId).FirstOrDefault();
                data.UserId = userId;
                context.UsersNfts.Update(data);
                context.SaveChanges();

                var wallet = context.Wallets.Where(x => x.UserId == userId).FirstOrDefault();
                wallet.Balance = wallet.Balance - tokenCount;
                context.Wallets.Update(wallet);
                context.SaveChanges();

                return Ok(wallet);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
