namespace The_Learning_Hub_Hotel.Models
{
    public class JoinTabels
    {
        public PHotel hotel { get; set; }
        public PTestimonial testimonial { get; set; }
        public PUserlogin userlogin { get; set; }
        public PRoom room { get; set; }
        public PService service { get; set; }
        public PUser user { get; set; }
        public PTeam team { get; set; }
        public PHomepagecontent phomepagecontent { get; set; }
        public PReservation reservation { get; set; }
        public PRole role { get; set; }
    }
}
