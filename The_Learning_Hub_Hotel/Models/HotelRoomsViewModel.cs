namespace The_Learning_Hub_Hotel.Models
{
    public class HotelRoomsViewModel
    {
        public int SelectedHotelId { get; set; }
        public IEnumerable<PHotel> Hotels { get; set; }
        public IEnumerable<PTestimonial> Testimonials { get; set; }
        public IEnumerable<JoinTabels> MultiTable { get; set; }
        public IEnumerable<PUserlogin> UserLogins { get; set; }
        public IEnumerable<PRoom> Rooms { get; set; }
        public IEnumerable<PService> Services { get; set; }
        public IEnumerable<PUser> Users { get; set; }
    }
}
