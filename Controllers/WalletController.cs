using Backend_Eternal.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Eternal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : Controller
    {
        public static EternalDbContext context = new EternalDbContext();

        [HttpGet]
        [Route("get/{userId}")]
        public ActionResult<IEnumerable<Wallet>> GetUserWallet(int userId)
        {
            try
            {
                var data = context.Wallets.Where(x => x.UserId == userId).ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("deposit/{userId}/{tokenCount}")]
        public ActionResult<IEnumerable<Wallet>> Deposit(int userId, decimal tokenCount)
        {
            try
            {
                var userWallet = context.Wallets.Where(x => x.UserId == userId).FirstOrDefault();
                userWallet.Balance += tokenCount;
                context.Wallets.Update(userWallet);
                context.SaveChanges();
                return context.Wallets.Where(x => x.UserId == userId).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
