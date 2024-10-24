using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using The_Learning_Hub_Hotel.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using System.Net;
using System.Composition;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Search;
using System.Linq;



namespace The_Learning_Hub_Hotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string searchQuery)
        {
            var uid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            ViewBag.RoomsCount = _context.PRooms.Count();
            ViewBag.HotelCount = _context.PHotels.Count();
            ViewBag.UserCount = _context.PUserlogins.Where(x => x.Roleid == 1).Count();


            var hotels = _context.PHotels.AsQueryable();
            var testimonials = _context.PTestimonials
                .Where(x => x.Status == "Approved")
                .ToList();
            var userlogins = _context.PUserlogins.ToList();
            var rooms = _context.PRooms.ToList();
            var services = _context.PServices.ToList();
            var users = _context.PUsers.ToList();
            var teams = _context.PTeams.ToList();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();
            ViewBag.Desc = _context.PHomepagecontents.Select(x => x.WelcomeText).FirstOrDefault();
            ViewBag.Image1 = _context.PHomepagecontents.Select(x => x.ImageFileTop1).FirstOrDefault();

            // Apply search filter if searchQuery is provided
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {

                var lowerSearchQuery = searchQuery.ToLower();

                // Filter by hotel name with case-insensitive search
                hotels = hotels.Where(h => h.Hotelname.ToLower().Contains(lowerSearchQuery));

                // Get hotel IDs from filtered hotels
                var filteredHotelIds = hotels.Select(h => h.Hotelid).ToList();


            }

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
            ViewBag.SearchQuery = searchQuery; // Pass the search query to the view

            return View(data);
        }


        public IActionResult About()
        {
            var uid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
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
            var uid = HttpContext.Session.GetInt32("Uid");
            var userlogin = HttpContext.Session.GetInt32("userid");
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            if (userlogin != null) {
                ViewBag.pagename = _context.PServicespagecontents.Select(x => x.Pagename).FirstOrDefault();
                ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
                ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
                ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
                ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();
            }
               
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
        public IActionResult Hotels(string searchQuery)
        {
            var uid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            ViewBag.pagename = _context.PHotelspagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            ViewBag.RoomsCount = _context.PRooms.Count();
            ViewBag.HotelCount = _context.PHotels.Count();
            ViewBag.UserCount = _context.PUserlogins.Where(x => x.Roleid == 1).Count();


            var hotels = _context.PHotels.AsQueryable();
            var testimonials = _context.PTestimonials
                .Where(x => x.Status == "Approved")
                .ToList();
            var userlogins = _context.PUserlogins.ToList();
            var rooms = _context.PRooms.ToList();
            var services = _context.PServices.ToList();
            var users = _context.PUsers.ToList();
            var teams = _context.PTeams.ToList();

            // Apply search filter if searchQuery is provided
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {

                var lowerSearchQuery = searchQuery.ToLower();

                // Filter by hotel name with case-insensitive search
                hotels = hotels.Where(h => h.Hotelname.ToLower().Contains(lowerSearchQuery));

                // Get hotel IDs from filtered hotels
                var filteredHotelIds = hotels.Select(h => h.Hotelid).ToList();


            }

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
            ViewBag.SearchQuery = searchQuery; // Pass the search query to the view

            return View(data);
        }
        public IActionResult Rooms(int hotelId)
        {
            var uid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
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
            var uid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            ViewBag.pagename = _context.PBookingpagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            var rooms = _context.PRooms
   .Where(r => r.Isavailable == "true")
   .Select(r => new
   {
       r.Roomid,
       r.Roomnumber,
       r.PricePerNight
   })
   .ToList();

            if (rooms.Any())
            {
                ViewData["Roomid"] = rooms;
            }
            else
            {

                ViewData["Roomid"] = null;
            }

            ViewData["Hotelid"] = new SelectList(_context.PHotels, "Hotelid", "Hotelname");
            //ViewData["Roomid"] = rooms;

            ViewBag.UserId = HttpContext.Session.GetInt32("Uid");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Booking([Bind("Reservationsid,CheckInDate,CheckOutDate,TotalPrice,UserId,Roomid,HotelId")] PReservation pReservation, string creditCardNumber)
        {
            var uid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            // تحقق من صحة التواريخ
            if (pReservation.CheckOutDate <= pReservation.CheckInDate)
            {
                ModelState.AddModelError(string.Empty, "تاريخ الخروج يجب أن يكون بعد تاريخ الوصول.");
                return View(pReservation);
            }

            var room = _context.PRooms.Find(pReservation.Roomid);
            if (room != null)
            {
                var numberOfDays = (pReservation.CheckOutDate - pReservation.CheckInDate)?.TotalDays ?? 0;
                var pricePerNight = room.PricePerNight ?? 0;
                pReservation.Toltalprice = (decimal)numberOfDays * (decimal)pricePerNight;

                room.Isavailable = "false";
                _context.Update(room);
            }

            var bankRecord = _context.PBanks.SingleOrDefault(b => b.Creditcardnumber.ToString() == creditCardNumber.ToString());
            if (bankRecord != null)
            {

                bankRecord.Money -= pReservation.Toltalprice;
                _context.Update(bankRecord);
            }
            else
            {
                // معالجة فشل الدفع
                ModelState.AddModelError(string.Empty, "رقم بطاقة الائتمان غير صحيح.");
                return View(pReservation);
            }

            _context.Add(pReservation);
            await _context.SaveChangesAsync();

            // إنشاء الفاتورة كملف PDF
            string pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/invoices", $"Invoice_{pReservation.Reservationsid}.pdf");
            CreatePdfInvoice(pdfPath, pReservation, user1, room);

            // تخزين مسار الفاتورة في قاعدة البيانات
            pReservation.Invoicepdf = $"invoices/Invoice_{pReservation.Reservationsid}.pdf";
            _context.Update(pReservation);
            await _context.SaveChangesAsync();
            await SendInvoiceEmail(user1.Email, "Invoice", "Thank You For Booking.", pdfPath);

            return RedirectToAction(nameof(Index));
        }
        private void CreatePdfInvoice(string pdfPath, PReservation pReservation, PUser user, PRoom room)
        {
            
            string directoryPath = Path.GetDirectoryName(pdfPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
                doc.Open();

                // إنشاء عنوان الفاتورة
                Paragraph title = new Paragraph("invoice", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                doc.Add(new Paragraph($"Your Name: {user.Fname} {user.Lname}"));
                doc.Add(new Paragraph($"Room Number: {room.Roomnumber}"));
                doc.Add(new Paragraph($"Check In Data: {(pReservation.CheckInDate.HasValue ? pReservation.CheckInDate.Value.ToShortDateString() : "N/A")}"));
                doc.Add(new Paragraph($"Check Out Date: {(pReservation.CheckOutDate.HasValue ? pReservation.CheckOutDate.Value.ToShortDateString() : "N/A")}"));
                doc.Add(new Paragraph($"Total Price: {pReservation.Toltalprice:C}"));

                doc.Close();
                writer.Close();
            }
        
    
        }
        private async Task SendInvoiceEmail(string recipientEmail, string subject, string bodyText, string pdfPath)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Mohammad Alomari", "mo.omari.2001@outlook.com"));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = subject;

            var body = new TextPart(TextFormat.Plain) { Text = bodyText };

            // Prepare the attachment
            MimePart attachment;
            var fileStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read, FileShare.Read);

            try
            {
                attachment = new MimePart("application", "pdf")
                {
                    Content = new MimeContent(fileStream),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(pdfPath)
                };

                var multipart = new Multipart("mixed");
                multipart.Add(body);
                multipart.Add(attachment);

                message.Body = multipart;

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    try
                    {
                        await client.ConnectAsync("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                        await client.AuthenticateAsync("mo.omari.2001@outlook.com", "mohammad@@01");
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending email: {ex.Message}");
                    }
                }
            }
            finally
            {
                // Ensure the file stream is closed after the email is sent
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }


        //private async Task SendInvoiceEmail(string recipientEmail, string subject, string bodyText, string pdfPath)
        //{
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("Mohammad Alomari", "mohammad5112001@gmail.com"));
        //    message.To.Add(new MailboxAddress("", recipientEmail));
        //    message.Subject = subject;

        //    var body = new TextPart(TextFormat.Plain) { Text = bodyText };

        //    // إعداد المرفق مع التأكد من إغلاق الملف بعد استخدامه
        //    MimePart attachment;
        //    using (var fileStream = System.IO.File.OpenRead(pdfPath))
        //    {
        //        attachment = new MimePart("application", "pdf")
        //        {
        //            Content = new MimeContent(fileStream),
        //            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
        //            ContentTransferEncoding = ContentEncoding.Base64,
        //            FileName = Path.GetFileName(pdfPath)
        //        };
        //    }

        //    var multipart = new Multipart("mixed");
        //    multipart.Add(body);
        //    multipart.Add(attachment);

        //    message.Body = multipart;

        //    using (var client = new SmtpClient())
        //    {
        //        try
        //        {
        //            // الاتصال بخادم SMTP لـ Gmail
        //            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

        //            // إعداد بيانات الاعتماد
        //            var networkCredential = new NetworkCredential("mohammad5112001@gmail.com", "mohammad01@@");
        //            client.Authenticate(networkCredential);

        //            // إرسال البريد الإلكتروني
        //            await client.SendAsync(message);

        //            // قطع الاتصال
        //            await client.DisconnectAsync(true);

        //            Console.WriteLine("Email sent successfully!");
        //        }
        //        catch (Exception ex)
        //        {
        //            // معالجة الأخطاء
        //            Console.WriteLine($"Failed to send email: {ex.Message}");
        //        }
        //    }
        //}

        public IActionResult GetRooms(int hotelId)
        {
            var rooms = _context.PRooms
                                .Where(r => r.Hotelid == hotelId)
                                .Select(r => new { r.Roomid, r.Roomnumber })
                                .ToList();
            return Json(rooms);
        }


        private async Task<IActionResult> ReloadBookingView(int? userId)
        {
            var hotels = await _context.PHotels.ToListAsync();
            ViewBag.Hotels = new SelectList(hotels, "Hotelid", "Hotelname");
            ViewBag.UserId = userId;
            ViewBag.UserImage = Url.Content("~/Images/" + (await _context.PUsers.FirstOrDefaultAsync(u => u.UserId == userId))?.Imagepath);

            return View("Booking");
        }
        public IActionResult Team()
        {
            var uid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            ViewBag.pagename = _context.PTeampagecontents.Select(x=>x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            var teams1 = _context.PTeams.ToList();
            return View(teams1);
        }
        public IActionResult Testimonial()
        {
            var uid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
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
            var uid = HttpContext.Session.GetInt32("Uid");
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            ViewBag.pagename = _context.PContactpagecontents.Select(x=>x.Pagename).FirstOrDefault();
            ViewBag.Ebooking =_context.PContactpagecontents.Select(x=>x.Bookingemail).FirstOrDefault();
            ViewBag.Egeneral =_context.PContactpagecontents.Select(x=>x.Generalemail).FirstOrDefault();
            ViewBag.Etechnical = _context.PContactpagecontents.Select(x => x.Technicalemail).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            // Populate dropdown lists for hotels
            ViewData["Hotelid"] = new SelectList(_context.PHotels, "Hotelid", "Hotelname");

            ViewBag.UserId = HttpContext.Session.GetInt32("Uid");
            
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Contact([Bind("Testimonialid,TestimonialText,Rating,CreatedAt,UserId,Hotelid,Status")] PTestimonial pTestimonial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pTestimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Hotelid"] = new SelectList(_context.PHotels, "Hotelid", "Hotelid", pTestimonial.Hotelid);
            ViewData["UserId"] = new SelectList(_context.PUsers, "UserId", "UserId", pTestimonial.UserId);
          

            return View(pTestimonial);
        }


        private async Task<IActionResult> ReloadContactView(int? userId, string username)
        {
            var hotels = await _context.PHotels.ToListAsync();
            ViewBag.Hotels = new SelectList(hotels, "Hotelid", "Hotelname");
            ViewBag.UserId = userId;
            ViewBag.UserName = username;
            ViewBag.UserImage = Url.Content("~/Images/" + (await _context.PUsers.FirstOrDefaultAsync(u => u.UserId == userId))?.Imagepath);

            return View("Contact");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });




           
        }
        public async Task<IActionResult> Profile()
        {
            ViewBag.pagename = _context.PRoomspagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            ViewBag.name = HttpContext.Session.GetString("username");

            var uid = HttpContext.Session.GetInt32("Uid");
            if (uid == null)
            {
                return NotFound();
            }
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }

            decimal decimaluserId = Convert.ToDecimal(uid);
            var user = await _context.PUsers.FirstOrDefaultAsync(x => x.UserId == uid);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Edit
        public async Task<IActionResult> Edit()
        {
            ViewBag.pagename = _context.PRoomspagecontents.Select(x => x.Pagename).FirstOrDefault();
            ViewBag.projectname = _context.PHomepagecontents.Select(x => x.Projectname).FirstOrDefault();
            ViewBag.Email = _context.PHomepagecontents.Select(x => x.Footeremail).FirstOrDefault();
            ViewBag.Phone = _context.PHomepagecontents.Select(x => x.Footerphonenumber).FirstOrDefault();
            ViewBag.Location = _context.PHomepagecontents.Select(x => x.Footerlocation).FirstOrDefault();

            ViewBag.name = HttpContext.Session.GetString("username");

            var uid = HttpContext.Session.GetInt32("Uid");
            decimal decimaluserId = Convert.ToDecimal(uid);
            
            

            if (uid == null)
            {
                return NotFound();
            }
            var user = await _context.PUsers.FindAsync(decimaluserId);
            var user1 = _context.PUsers.Where(x => x.UserId == uid).SingleOrDefault();
            if (user1 != null)
            {
                ViewBag.FName = user1.Fname;
                ViewBag.LName = user1.Lname;
                ViewBag.Email = user1.Email;
                ViewBag.phone = user1.PhoneNumber;
                ViewBag.Image = user1.Imagepath;
            }
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.id = uid;
            return View(user);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("UserId,Fname,Lname,PhoneNumber,Email,ImageFile")] PUser pUser)
        {
            ViewBag.name = HttpContext.Session.GetString("username");

            var uid = HttpContext.Session.GetInt32("Uid");
            //if (adminid == null || adminid != pUser.UserId)
            //{
            //    return NotFound();
            //}
            decimal decimaluserId = Convert.ToDecimal(uid);

            var user = await _context.PUsers.FindAsync(decimaluserId);
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



    }
}
