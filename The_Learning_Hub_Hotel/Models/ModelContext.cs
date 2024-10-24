using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace The_Learning_Hub_Hotel.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PAboutpagecontent> PAboutpagecontents { get; set; }

    public virtual DbSet<PBank> PBanks { get; set; }

    public virtual DbSet<PBookingpagecontent> PBookingpagecontents { get; set; }

    public virtual DbSet<PContactpagecontent> PContactpagecontents { get; set; }

    public virtual DbSet<PHomepagecontent> PHomepagecontents { get; set; }

    public virtual DbSet<PHotel> PHotels { get; set; }

    public virtual DbSet<PHotelspagecontent> PHotelspagecontents { get; set; }

    public virtual DbSet<PReservation> PReservations { get; set; }

    public virtual DbSet<PRole> PRoles { get; set; }

    public virtual DbSet<PRoom> PRooms { get; set; }

    public virtual DbSet<PRoomspagecontent> PRoomspagecontents { get; set; }

    public virtual DbSet<PService> PServices { get; set; }

    public virtual DbSet<PServicespagecontent> PServicespagecontents { get; set; }

    public virtual DbSet<PTeam> PTeams { get; set; }

    public virtual DbSet<PTeampagecontent> PTeampagecontents { get; set; }

    public virtual DbSet<PTestimonial> PTestimonials { get; set; }

    public virtual DbSet<PTestimonialpagecontent> PTestimonialpagecontents { get; set; }

    public virtual DbSet<PUser> PUsers { get; set; }

    public virtual DbSet<PUserlogin> PUserlogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.106)(PORT=1521) (CONNECT_DATA=(SERVICE_NAME=xe))));User Id=c##mohammad; Password=Test321;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##MOHAMMAD")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<PAboutpagecontent>(entity =>
        {
            entity.HasKey(e => e.Aboutpagecontentid).HasName("SYS_C008392");

            entity.ToTable("P_ABOUTPAGECONTENT");

            entity.Property(e => e.Aboutpagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ABOUTPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");
            entity.Property(e => e.WelcomeText)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("WELCOME_TEXT");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.PAboutpagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN2");
        });

        modelBuilder.Entity<PBank>(entity =>
        {
            entity.HasKey(e => e.Bankid).HasName("SYS_C008387");

            entity.ToTable("P_BANK");

            entity.Property(e => e.Bankid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BANKID");
            entity.Property(e => e.Creditcardexp)
                .HasColumnType("DATE")
                .HasColumnName("CREDITCARDEXP");
            entity.Property(e => e.Creditcardnumber)
                .HasColumnType("NUMBER")
                .HasColumnName("CREDITCARDNUMBER");
            entity.Property(e => e.Money)
                .HasColumnType("NUMBER")
                .HasColumnName("MONEY");
        });

        modelBuilder.Entity<PBookingpagecontent>(entity =>
        {
            entity.HasKey(e => e.Bookingpagecontentid).HasName("SYS_C008404");

            entity.ToTable("P_BOOKINGPAGECONTENT");

            entity.Property(e => e.Bookingpagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BOOKINGPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.PBookingpagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN6");
        });

        modelBuilder.Entity<PContactpagecontent>(entity =>
        {
            entity.HasKey(e => e.Contactpagecontentid).HasName("SYS_C008413");

            entity.ToTable("P_CONTACTPAGECONTENT");

            entity.Property(e => e.Contactpagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONTACTPAGECONTENTID");
            entity.Property(e => e.Bookingemail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BOOKINGEMAIL");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.Generalemail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GENERALEMAIL");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Technicalemail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TECHNICALEMAIL");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.PContactpagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN9");
        });

        modelBuilder.Entity<PHomepagecontent>(entity =>
        {
            entity.HasKey(e => e.Homepagecontent).HasName("SYS_C008389");

            entity.ToTable("P_HOMEPAGECONTENT");

            entity.Property(e => e.Homepagecontent)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOMEPAGECONTENT");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP1");
            entity.Property(e => e.ImagepathTop2)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP2");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");
            entity.Property(e => e.WelcomeText)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("WELCOME_TEXT");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.PHomepagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN1");
        });

        modelBuilder.Entity<PHotel>(entity =>
        {
            entity.HasKey(e => e.Hotelid).HasName("SYS_C008366");

            entity.ToTable("P_HOTELS");

            entity.Property(e => e.Hotelid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Hotelname)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("HOTELNAME");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Location)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("LOCATION");
        });

        modelBuilder.Entity<PHotelspagecontent>(entity =>
        {
            entity.HasKey(e => e.Hotelspagecontentid).HasName("SYS_C008398");

            entity.ToTable("P_HOTELSPAGECONTENT");

            entity.Property(e => e.Hotelspagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELSPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.PHotelspagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN4");
        });

        modelBuilder.Entity<PReservation>(entity =>
        {
            entity.HasKey(e => e.Reservationsid).HasName("SYS_C008374");

            entity.ToTable("P_RESERVATIONS");

            entity.Property(e => e.Reservationsid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RESERVATIONSID");
            entity.Property(e => e.CheckInDate)
                .HasColumnType("DATE")
                .HasColumnName("CHECK_IN_DATE");
            entity.Property(e => e.CheckOutDate)
                .HasColumnType("DATE")
                .HasColumnName("CHECK_OUT_DATE");
            entity.Property(e => e.Invoicepdf)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("INVOICEPDF");
            entity.Property(e => e.Roomid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMID");
            entity.Property(e => e.Toltalprice)
                .HasColumnType("NUMBER")
                .HasColumnName("TOLTALPRICE");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Room).WithMany(p => p.PReservations)
                .HasForeignKey(d => d.Roomid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ROOM5");

            entity.HasOne(d => d.User).WithMany(p => p.PReservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USER321");
        });

        modelBuilder.Entity<PRole>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("SYS_C008356");

            entity.ToTable("P_ROLES");

            entity.Property(e => e.Roleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<PRoom>(entity =>
        {
            entity.HasKey(e => e.Roomid).HasName("SYS_C008371");

            entity.ToTable("P_ROOMS");

            entity.Property(e => e.Roomid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMID");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Isavailable)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ISAVAILABLE");
            entity.Property(e => e.PricePerNight)
                .HasColumnType("NUMBER")
                .HasColumnName("PRICE_PER_NIGHT");
            entity.Property(e => e.Roomcapacity)
                .HasColumnType("NUMBER")
                .HasColumnName("ROOMCAPACITY");
            entity.Property(e => e.Roomnumber)
                .HasColumnType("NUMBER")
                .HasColumnName("ROOMNUMBER");

            entity.HasOne(d => d.Hotel).WithMany(p => p.PRooms)
                .HasForeignKey(d => d.Hotelid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_HOTEL4");
        });

        modelBuilder.Entity<PRoomspagecontent>(entity =>
        {
            entity.HasKey(e => e.Roomspagecontentid).HasName("SYS_C008401");

            entity.ToTable("P_ROOMSPAGECONTENT");

            entity.Property(e => e.Roomspagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMSPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.PRoomspagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN5");
        });

        modelBuilder.Entity<PService>(entity =>
        {
            entity.HasKey(e => e.Serviceid).HasName("SYS_C008368");

            entity.ToTable("P_SERVICES");

            entity.Property(e => e.Serviceid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SERVICEID");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Servicename)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("SERVICENAME");
            entity.Property(e => e.Servicetext)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("SERVICETEXT");

            entity.HasOne(d => d.Hotel).WithMany(p => p.PServices)
                .HasForeignKey(d => d.Hotelid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_HOTEL3");
        });

        modelBuilder.Entity<PServicespagecontent>(entity =>
        {
            entity.HasKey(e => e.Servicespagecontentid).HasName("SYS_C008395");

            entity.ToTable("P_SERVICESPAGECONTENT");

            entity.Property(e => e.Servicespagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SERVICESPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.PServicespagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN3");
        });

        modelBuilder.Entity<PTeam>(entity =>
        {
            entity.HasKey(e => e.Teamid).HasName("SYS_C008385");

            entity.ToTable("P_TEAM");

            entity.Property(e => e.Teamid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TEAMID");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Position)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("POSITION");
            entity.Property(e => e.TeamMembername)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("TEAM_MEMBERNAME");
        });

        modelBuilder.Entity<PTeampagecontent>(entity =>
        {
            entity.HasKey(e => e.Teampagecontentid).HasName("SYS_C008407");

            entity.ToTable("P_TEAMPAGECONTENT");

            entity.Property(e => e.Teampagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TEAMPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.PTeampagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN7");
        });

        modelBuilder.Entity<PTestimonial>(entity =>
        {
            entity.HasKey(e => e.Testimonialid).HasName("SYS_C008381");

            entity.ToTable("P_TESTIMONIAL");

            entity.Property(e => e.Testimonialid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIALID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Rating)
                .HasColumnType("NUMBER")
                .HasColumnName("RATING");
            entity.Property(e => e.TestimonialText)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("TESTIMONIAL_TEXT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("STATUS");

            entity.HasOne(d => d.Hotel).WithMany(p => p.PTestimonials)
                .HasForeignKey(d => d.Hotelid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_HOTEL5");

            entity.HasOne(d => d.User).WithMany(p => p.PTestimonials)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USER421");
        });

        modelBuilder.Entity<PTestimonialpagecontent>(entity =>
        {
            entity.HasKey(e => e.Testimonialpagecontentid).HasName("SYS_C008410");

            entity.ToTable("P_TESTIMONIALPAGECONTENT");

            entity.Property(e => e.Testimonialpagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIALPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathMiddle)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_MIDDLE");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.PTestimonialpagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN8");
        });

        modelBuilder.Entity<PUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("SYS_C008359");

            entity.ToTable("P_USERS");

            entity.HasIndex(e => e.Email, "SYS_C008360").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Lname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LNAME");
            entity.Property(e => e.PhoneNumber)
                .HasPrecision(10)
                .HasColumnName("PHONE_NUMBER");
        });

        modelBuilder.Entity<PUserlogin>(entity =>
        {
            entity.HasKey(e => e.Userloginid).HasName("SYS_C008362");

            entity.ToTable("P_USERLOGIN");

            entity.Property(e => e.Userloginid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");
            entity.Property(e => e.Passwordd)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PASSWORDD");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLEID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.PUserlogins)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ROLE2");

            entity.HasOne(d => d.User).WithMany(p => p.PUserlogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USER32");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
