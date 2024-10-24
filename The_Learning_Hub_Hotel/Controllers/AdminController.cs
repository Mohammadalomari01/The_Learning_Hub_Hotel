using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using The_Learning_Hub_Hotel.Models;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;

namespace The_Learning_Hub_Hotel.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var name = HttpContext.Session.GetString("adminname");
            
            var id = HttpContext.Session.GetInt32("Uid");

            var user = _context.PUsers.Where(x => x.UserId == id).SingleOrDefault();
            //var user = _context.PUsers.Where(x=>x.UserId == id).FirstOrDefault();
            if (user != null)
            {   
                ViewBag.FName = user.Fname;
                ViewBag.LName = user.Lname;
                ViewBag.Email = user.Email;
                ViewBag.phone = user.PhoneNumber;
                ViewBag.Image = user.Imagepath;
            }
            //--------------------
            ViewBag.NumOfRoomAv = _context.PRooms.Where(x => x.Isavailable == "true").Count();
            ViewBag.NumOfRoomBK = _context.PRooms.Where(x => x.Isavailable == "false").Count();
            ViewBag.TestCount = _context.PTestimonials.Where(x => x.Status == "Pending").Count();
            ViewBag.NumOfHotel = _context.PHotels.Count();
            ViewBag.NumOfResr = _context.PReservations.Count();

                ViewBag.NumOfUser = _context.PUsers.Count();
            var room = _context.PRooms.Where(x => x.Isavailable == "true").ToList();
            var hotels = _context.PHotels.ToList();
            var testimonial = _context.PTestimonials.ToList();
            var userlogin = _context.PUserlogins.ToList();
            //var room = _context.PRooms.ToList();
            var services = _context.PServices.ToList();
            var users = _context.PUsers.ToList();
            var teams = _context.PTeams.ToList();
            var roles = _context.PRoles.ToList();

            var multiTable = from h in hotels
                             join r in room on h.Hotelid equals r.Hotelid
                             join t in testimonial on h.Hotelid equals t.Hotelid
                             join u in userlogin on t.UserId equals u.UserId
                             join s in services on h.Hotelid equals s.Hotelid
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

            var data = Tuple.Create<IEnumerable<PHotel>, IEnumerable<PTestimonial>, IEnumerable<JoinTabels>, IEnumerable<PUserlogin>, IEnumerable<PRoom>, IEnumerable<PService>, IEnumerable<PUser>>(
                hotels, testimonial, multiTable, userlogin, room, services, users
            );

            return View(data);


        }
        public async Task<IActionResult> Profile()
        {
            ViewBag.name = HttpContext.Session.GetString("adminname");

            var adminid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == adminid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            if (adminid == null)
            {
                return NotFound();
            }

            decimal decimalAdminId = Convert.ToDecimal(adminid);
            var user = await _context.PUsers.FirstOrDefaultAsync(x=>x.UserId == adminid);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Edit
        public async Task<IActionResult> Edit()
        {
            ViewBag.name = HttpContext.Session.GetString("adminname");

            var adminid = HttpContext.Session.GetInt32("Uid");
            decimal decimalAdminId = Convert.ToDecimal(adminid);
            var user1 = _context.PUsers.Where(x => x.UserId == adminid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }

            if (adminid == null)
            {
                return NotFound();
            }
            var user = await _context.PUsers.FindAsync(decimalAdminId);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.id = adminid;
            return View(user);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("UserId,Fname,Lname,PhoneNumber,Email,ImageFile")] PUser pUser)
        {
            ViewBag.name = HttpContext.Session.GetString("adminname");

            var adminid = HttpContext.Session.GetInt32("Uid");
            //if (adminid == null || adminid != pUser.UserId)
            //{
            //    return NotFound();
            //}
            decimal decimalAdminId = Convert.ToDecimal(adminid);

            var user = await _context.PUsers.FindAsync(decimalAdminId);
            if (user == null)
            {
                return NotFound();
            }

            user.Fname = pUser.Fname;
            user.Lname = pUser.Lname;
            user.Email = pUser.Email;
            user.PhoneNumber = pUser.PhoneNumber;

            if (pUser.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + "_" + pUser.ImageFile.FileName;
                string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pUser.ImageFile.CopyToAsync(fileStream);
                }

                user.Imagepath = fileName;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PUserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        private bool PUserExists(decimal id)
        {
            return (_context.PUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        public IActionResult Search()
        {
            var adminid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == adminid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            var data = _context.PReservations
                   .Include(p => p.User) 
                   .Include(p => p.Room) 
                   .AsQueryable();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> SearchResults(DateTime? startDate, DateTime? endDate)
        {
            var adminid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == adminid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            var data = _context.PReservations
                               .Include(p => p.User) // Include related User entity
                               .Include(p => p.Room) // Include related Room entity
                               .AsQueryable();
            if (startDate == null && endDate == null)
            {
                return View("Search", data);
            }
            else if (startDate != null && endDate == null)
            {
                var result = await data.Where(x => x.CheckInDate.Value.Date == startDate).ToListAsync();
                ViewBag.TotalPriceSum = result.Sum(x => x.Toltalprice);
                return View("Search", result);
            }
            else if (startDate == null && endDate != null)
            {
                var result = await data.Where(x => x.CheckOutDate.Value.Date == endDate).ToListAsync();
                ViewBag.TotalPriceSum = result.Sum(x => x.Toltalprice);
                return View("Search", result);
            }
            else
            {
                var result = await data.Where(x => x.CheckOutDate.Value.Date <= endDate && x.CheckInDate.Value.Date >= startDate).ToListAsync();
                ViewBag.TotalPriceSum = result.Sum(x => x.Toltalprice);
                return View("Search", result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(DateTime? startDate, DateTime? endDate, string reportType)
        {
            var adminid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == adminid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            var data = _context.PReservations
                               .Include(p => p.User)
                               .Include(p => p.Room)
                               .AsQueryable();

            if (startDate != null)
            {
                data = data.Where(x => x.CheckInDate.Value.Date >= startDate);
            }
            if (endDate != null)
            {
                data = data.Where(x => x.CheckOutDate.Value.Date <= endDate);
            }

            var result = await data.ToListAsync();

            if (reportType == "PDF")
            {
                var pdf = GeneratePdf(result);
                return File(pdf, "application/pdf", "ReservationReport.pdf");
            }
            else if (reportType == "Excel")
            {
                var excel = GenerateExcel(result);
                return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReservationReport.xlsx");
            }

            // If no report type is specified, return the standard view
            return View("Search", result);
        }

        [HttpPost]
        public async Task<IActionResult> SearchByMonthYear(int? month, int? year)
        {
            var adminid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == adminid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            var data = _context.PReservations
                               .Include(p => p.User)
                               .Include(p => p.Room)
                               .AsQueryable();

            if (month.HasValue)
            {
                data = data.Where(x => x.CheckInDate.Value.Month == month.Value);
            }
            if (year.HasValue)
            {
                data = data.Where(x => x.CheckInDate.Value.Year == year.Value);
            }

            var result = await data.ToListAsync();
            return View("Search", result); // Ensure "Search" view is ready to handle the result
        }

        private byte[] GeneratePdf(List<PReservation> reservations)
        {
            using (var memoryStream = new MemoryStream())
            {
                var document = new Document();
                PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Add your PDF content here, e.g., tables, headers, etc.
                PdfPTable table = new PdfPTable(8);
                table.AddCell("First Name");
                table.AddCell("Last Name");
                table.AddCell("Room Number");
                table.AddCell("Room Price");
                table.AddCell("Room Capacity");
                table.AddCell("Total Price");
                table.AddCell("Start Date");
                table.AddCell("End Date");

                foreach (var reservation in reservations)
                {
                    table.AddCell(reservation.User.Fname);
                    table.AddCell(reservation.User.Lname);
                    table.AddCell(reservation.Room.Roomnumber.ToString());
                    table.AddCell(reservation.Room.PricePerNight.ToString());
                    table.AddCell(reservation.Room.Roomcapacity.ToString());
                    table.AddCell(reservation.Toltalprice.ToString());
                    table.AddCell(reservation.CheckInDate?.ToString("dd/MM/yyyy"));
                    table.AddCell(reservation.CheckOutDate?.ToString("dd/MM/yyyy"));
                }

                document.Add(table);
                document.Close();

                return memoryStream.ToArray();
            }
        }

        private byte[] GenerateExcel(List<PReservation> reservations)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reservations");

                // Add headers
                worksheet.Cells[1, 1].Value = "First Name";
                worksheet.Cells[1, 2].Value = "Last Name";
                worksheet.Cells[1, 3].Value = "Room Number";
                worksheet.Cells[1, 4].Value = "Room Price";
                worksheet.Cells[1, 5].Value = "Room Capacity";
                worksheet.Cells[1, 6].Value = "Total Price";
                worksheet.Cells[1, 7].Value = "Start Date";
                worksheet.Cells[1, 8].Value = "End Date";

                // Add values
                for (int i = 0; i < reservations.Count; i++)
                {
                    var reservation = reservations[i];
                    worksheet.Cells[i + 2, 1].Value = reservation.User.Fname;
                    worksheet.Cells[i + 2, 2].Value = reservation.User.Lname;
                    worksheet.Cells[i + 2, 3].Value = reservation.Room.Roomnumber;
                    worksheet.Cells[i + 2, 4].Value = reservation.Room.PricePerNight;
                    worksheet.Cells[i + 2, 5].Value = reservation.Room.Roomcapacity;
                    worksheet.Cells[i + 2, 6].Value = reservation.Toltalprice;
                    worksheet.Cells[i + 2, 7].Value = reservation.CheckInDate?.ToString("dd/MM/yyyy");
                    worksheet.Cells[i + 2, 8].Value = reservation.CheckOutDate?.ToString("dd/MM/yyyy");
                }

                return package.GetAsByteArray();
            }
        }
        //public ActionResult GenerateReport(int month, int year)
        //{
        //    var reservations = _context.PReservations
        //                      .Where(r => r.CheckInDate.HasValue &&
        //                                  r.CheckInDate.Value.Month == month &&
        //                                  r.CheckInDate.Value.Year == year)
        //                      .ToList();



        //    // Export to PDF or Excel logic here
        //    return View(reservations);
        //}



    }
}
