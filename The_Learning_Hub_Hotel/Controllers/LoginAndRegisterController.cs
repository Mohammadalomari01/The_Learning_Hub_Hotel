using Microsoft.AspNetCore.Mvc;
using The_Learning_Hub_Hotel.Models;

namespace The_Learning_Hub_Hotel.Controllers
{
    public class LoginAndRegisterController : Controller

    {
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public LoginAndRegisterController (ModelContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public ActionResult Index()
        {
            
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Username,Passwordd")] PUserlogin userLogin , PUser user)
        {
            var auth = _context.PUserlogins.Where(x => x.Username == userLogin.Username && x.Passwordd == userLogin.Passwordd).FirstOrDefault();
            if (auth.Roleid == 1)
            {
                HttpContext.Session.SetString("username", auth.Username);
                HttpContext.Session.SetInt32("userid", (int)auth.Userloginid);
               
                 HttpContext.Session.SetInt32("Uid", (int)auth.UserId);
               

                //var name = HttpContext.Session.GetString("name");
                //var num= HttpContext.Session.GetInt32("custid").ToString();
                return RedirectToAction("Index", "Home");

            }
            else if (auth.Roleid == 21)
            {
                HttpContext.Session.SetInt32("adminid", (int)auth.Userloginid);
                HttpContext.Session.SetString("adminname", auth.Username);
                HttpContext.Session.SetInt32("Uid", (int)auth.UserId);



                return RedirectToAction("Index", "Admin");
            }
            return View(userLogin);
        }

    
    [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,Fname,Lname,PhoneNumber,Email,ImageFile")] PUser user, string username, string password)
        {

            if (ModelState.IsValid)
            {
                if (user.ImageFile != null)
                {
                    // 1- get rootpath

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    //2- filename
                    //dhchcvhcbdjcnbhcbhc_Aseel.png
                    //wiueyrueiryeuirueiori_Aseel.png
                    string fileName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;

                    //3- path == ~/Images/dhchcvhcbdjcnbhcbhc_Aseel.png

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //4- upload image to folder images  
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(fileStream);
                    }

                    user.Imagepath = fileName;
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    PUserlogin userLogin = new PUserlogin();
                    userLogin.UserId = user.UserId;
                    userLogin.Username = username;
                    userLogin.Passwordd = password;
                    userLogin.Roleid = 1;
                    _context.Add(userLogin);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "LoginAndRegister");

                }

            }


            return View(user);
        }
    }
}

