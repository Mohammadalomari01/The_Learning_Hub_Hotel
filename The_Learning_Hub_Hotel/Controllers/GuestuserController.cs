using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using The_Learning_Hub_Hotel.Models;

namespace The_Learning_Hub_Hotel.Controllers
{
    public class GuestuserController : Controller
    {
       
        private readonly ModelContext _context;
        
        public GuestuserController(ModelContext context)
        {
            _context = context;
           
        }
        public IActionResult Index()
        {
            ViewBag.Desc = _context.PHomepagecontents.Select(x => x.WelcomeText).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            ViewBag.RoomsCount = _context.PRooms.Count();
            ViewBag.HotelCount = _context.PHotels.Count();
            ViewBag.UserCount = _context.PUserlogins.Where(x => x.Roleid == 1).Count();


            var hotels = _context.PHotels.ToList();
            var testimonials = _context.PTestimonials
                .Where(x => x.Status == "Approved")
                .ToList();
            var userlogins = _context.PUserlogins.ToList();
            var rooms = _context.PRooms.ToList();
            var services = _context.PServices.ToList();
            var users = _context.PUsers.ToList();
            var teams = _context.PTeams.ToList();


            var multiTable = from h in hotels
                             join t in testimonials on h.Hotelid equals t.Hotelid
                             join u in userlogins on t.UserId equals u.UserId
                             join s in services on h.Hotelid equals s.Hotelid
                             join r in rooms on h.Hotelid equals r.Hotelid
                             join us in users on t.UserId equals us.UserId
                             select new JoinTabels
                             {
                                 hotel = h,
                                 testimonial = t,
                                 userlogin = u,
                                 room = r,
                                 service = s,
                                 user = us
                             };

            var data = Tuple.Create(
                (IEnumerable<PHotel>)hotels,
                (IEnumerable<PTestimonial>)testimonials,
                (IEnumerable<JoinTabels>)multiTable,
                (IEnumerable<PUserlogin>)userlogins,
                (IEnumerable<PRoom>)rooms,
                (IEnumerable<PService>)services,
                (IEnumerable<PUser>)users
            );

            return View(data);
        }

        public IActionResult About()
        {
            ViewBag.PageName = _context.PAboutpagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();
            ViewBag.Desc = _context.PAboutpagecontents.Select(x => x.WelcomeText).FirstOrDefault();

            ViewBag.RoomsCount = _context.PRooms.Count();
            ViewBag.HotelCount = _context.PHotels.Count();
            ViewBag.UserCount = _context.PUserlogins.Where(x => x.Roleid == 1).Count();
            var hotels = _context.PHotels.ToList();
            var testimonial = _context.PTestimonials.ToList();
            var userlogin = _context.PUserlogins.ToList();
            var room = _context.PRooms.ToList();
            var services = _context.PServices.ToList();
            var users = _context.PUsers.ToList();
            var teams = _context.PTeams.ToList();
            var multiTable = from h in hotels
                             join r in room on h.Hotelid equals r.Hotelid
                             join t in testimonial on h.Hotelid equals t.Hotelid
                             join u in userlogin on t.UserId equals u.UserId
                             join s in services on h.Hotelid equals s.Hotelid
                             join us in users on t.UserId equals us.UserId


                             select new JoinTabels { hotel = h, testimonial = t, userlogin = u, room = r, service = s, user = us };


            var data = Tuple.Create<IEnumerable<PHotel>, IEnumerable<PTestimonial>, IEnumerable<JoinTabels>, IEnumerable<PUserlogin>, IEnumerable<PRoom>, IEnumerable<PService>, IEnumerable<PUser>>(hotels, testimonial, multiTable, userlogin, room, services, users);
            return View(data);
        }
        public IActionResult Services()
        {
            ViewBag.pagename = _context.PServicespagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            ViewBag.RoomsCount = _context.PRooms.Count();
            ViewBag.HotelCount = _context.PHotels.Count();
            ViewBag.UserCount = _context.PUserlogins.Where(x => x.Roleid == 1).Count();


            var hotels = _context.PHotels.ToList();
            var testimonials = _context.PTestimonials
                .Where(x => x.Status == "Approved")
                .ToList();
            var userlogins = _context.PUserlogins.ToList();
            var rooms = _context.PRooms.ToList();
            var services = _context.PServices.ToList();
            var users = _context.PUsers.ToList();
            var teams = _context.PTeams.ToList();


            var multiTable = from h in hotels
                             join t in testimonials on h.Hotelid equals t.Hotelid
                             join u in userlogins on t.UserId equals u.UserId
                             join s in services on h.Hotelid equals s.Hotelid
                             join r in rooms on h.Hotelid equals r.Hotelid
                             join us in users on t.UserId equals us.UserId
                             select new JoinTabels
                             {
                                 hotel = h,
                                 testimonial = t,
                                 userlogin = u,
                                 room = r,
                                 service = s,
                                 user = us
                             };

            var data = Tuple.Create(
                (IEnumerable<PHotel>)hotels,
                (IEnumerable<PTestimonial>)testimonials,
                (IEnumerable<JoinTabels>)multiTable,
                (IEnumerable<PUserlogin>)userlogins,
                (IEnumerable<PRoom>)rooms,
                (IEnumerable<PService>)services,
                (IEnumerable<PUser>)users
            );

            return View(data);
        }
        public IActionResult Hotels()
        {
            ViewBag.pagename = _context.PHotelspagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            ViewBag.RoomsCount = _context.PRooms.Count();
            ViewBag.HotelCount = _context.PHotels.Count();
            ViewBag.UserCount = _context.PUserlogins.Where(x => x.Roleid == 1).Count();


            var hotels = _context.PHotels.ToList();
            var testimonials = _context.PTestimonials
                .Where(x => x.Status == "Approved")
                .ToList();
            var userlogins = _context.PUserlogins.ToList();
            var rooms = _context.PRooms.ToList();
            var services = _context.PServices.ToList();
            var users = _context.PUsers.ToList();
            var teams = _context.PTeams.ToList();


            var multiTable = from h in hotels
                             join t in testimonials on h.Hotelid equals t.Hotelid
                             join u in userlogins on t.UserId equals u.UserId
                             join s in services on h.Hotelid equals s.Hotelid
                             join r in rooms on h.Hotelid equals r.Hotelid
                             join us in users on t.UserId equals us.UserId
                             select new JoinTabels
                             {
                                 hotel = h,
                                 testimonial = t,
                                 userlogin = u,
                                 room = r,
                                 service = s,
                                 user = us
                             };

            var data = Tuple.Create(
                (IEnumerable<PHotel>)hotels,
                (IEnumerable<PTestimonial>)testimonials,
                (IEnumerable<JoinTabels>)multiTable,
                (IEnumerable<PUserlogin>)userlogins,
                (IEnumerable<PRoom>)rooms,
                (IEnumerable<PService>)services,
                (IEnumerable<PUser>)users
            );

            return View(data);
        }
        public IActionResult Rooms(int hotelId)
        {
            ViewBag.pagename = _context.PRoomspagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            ViewBag.RoomsCount = _context.PRooms.Count();
            ViewBag.HotelCount = _context.PHotels.Count();
            ViewBag.UserCount = _context.PUserlogins.Where(x => x.Roleid == 1).Count();

            var selectedHotel = _context.PHotels
                                       .Include(h => h.PRooms)
                                       .FirstOrDefault(h => h.Hotelid == hotelId);

            if (selectedHotel == null)
            {
                return NotFound();
            }

            var testimonial = _context.PTestimonials.Where(t => t.Hotelid == hotelId).ToList();
            var userlogin = _context.PUserlogins.ToList();
            var services = _context.PServices.Where(s => s.Hotelid == hotelId).ToList();
            var users = _context.PUsers.ToList();
            var teams = _context.PTeams.ToList();

            var multiTable = from r in selectedHotel.PRooms
                             join t in testimonial on selectedHotel.Hotelid equals t.Hotelid
                             join u in userlogin on t.UserId equals u.UserId
                             join s in services on selectedHotel.Hotelid equals s.Hotelid
                             join us in users on t.UserId equals us.UserId
                             select new JoinTabels
                             {
                                 hotel = selectedHotel,
                                 testimonial = t,
                                 userlogin = u,
                                 room = r,
                                 service = s,
                                 user = us
                             };

            var viewModel = new HotelRoomsViewModel
            {
                SelectedHotelId = hotelId,
                Hotels = new List<PHotel> { selectedHotel },
                Testimonials = testimonial,
                MultiTable = multiTable,
                UserLogins = userlogin,
                Rooms = selectedHotel.PRooms,
                Services = services,
                Users = users
            };

            return View(viewModel);
        }


        public IActionResult Booking()
        {
            ViewBag.pagename = _context.PBookingpagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            return View();
        }
        public IActionResult Team()
        {
            ViewBag.pagename = _context.PTeampagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();


            var teams1 = _context.PTeams.ToList();
            return View(teams1);
        }
        public IActionResult Testimonial()
        {
            ViewBag.pagename = _context.PTestimonialpagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            ViewBag.RoomsCount = _context.PRooms.Count();
            ViewBag.HotelCount = _context.PHotels.Count();
            ViewBag.UserCount = _context.PUserlogins.Where(x => x.Roleid == 1).Count();


            var hotels = _context.PHotels.ToList();
            var testimonials = _context.PTestimonials
                .Where(x => x.Status == "Approved")
                .ToList();
            var userlogins = _context.PUserlogins.ToList();
            var rooms = _context.PRooms.ToList();
            var services = _context.PServices.ToList();
            var users = _context.PUsers.ToList();
            var teams = _context.PTeams.ToList();


            var multiTable = from h in hotels
                             join t in testimonials on h.Hotelid equals t.Hotelid
                             join u in userlogins on t.UserId equals u.UserId
                             join s in services on h.Hotelid equals s.Hotelid
                             join r in rooms on h.Hotelid equals r.Hotelid
                             join us in users on t.UserId equals us.UserId
                             select new JoinTabels
                             {
                                 hotel = h,
                                 testimonial = t,
                                 userlogin = u,
                                 room = r,
                                 service = s,
                                 user = us
                             };

            var data = Tuple.Create(
                (IEnumerable<PHotel>)hotels,
                (IEnumerable<PTestimonial>)testimonials,
                (IEnumerable<JoinTabels>)multiTable,
                (IEnumerable<PUserlogin>)userlogins,
                (IEnumerable<PRoom>)rooms,
                (IEnumerable<PService>)services,
                (IEnumerable<PUser>)users
            );

            return View(data);
        }
        public IActionResult Contact()
        {
            ViewBag.pagename = _context.PContactpagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.Ebooking = _context.PContactpagecontents.Select(x => x.Bookingemail).FirstOrDefault();
            ViewBag.Egeneral = _context.PContactpagecontents.Select(x => x.Generalemail).FirstOrDefault();
            ViewBag.Etechnical = _context.PContactpagecontents.Select(x => x.Technicalemail).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();
            return View();
        }
    }
}
