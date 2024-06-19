using Backend_Eternal.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Eternal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        public static EternalDbContext context = new EternalDbContext();

        [HttpGet]
        [Route("enter/{phone}/{password}")]
        public ActionResult<IEnumerable<User>> Enter(string phone, string password)
        {
            try
            {
                var user = context.Users.Where(x => x.Phone == phone && x.Password == password).FirstOrDefault();
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Неверный пароль!");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("phone/{phone}")]
        public ActionResult<IEnumerable<User>> ValidatePhone(string phone)
        {
            try
            {
                var user = context.Users.Where(x => x.Phone == phone).FirstOrDefault();
                if (user != null)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("reg/{phone}/{name}/{tag}/{password}")]
        public ActionResult<IEnumerable<User>> RegUser(string phone, string name, string tag, string password)
        {
            try
            {
                var checkAvail = context.Users.Where(x => x.Phone == phone).FirstOrDefault();
                if (checkAvail == null)
                {
                    User user = new User()
                    {
                        Password = password,
                        Phone = phone,
                        Name = name,
                        TagName = "@"+tag,
                        CoverImage = null,
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                    Wallet wallet = new Wallet()
                    {
                        UserId = user.UserId,
                        Balance = 0,
                        CurrencyId = 1
                    };
                    context.Wallets.Add(wallet);
                    context.SaveChanges();
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Пользователь с таким номером уже есть");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}