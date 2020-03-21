using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TVK.Web
{
    public partial class testSQLContext : DbContext
    {
        public testSQLContext()
        {
        }

        public testSQLContext(DbContextOptions<testSQLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BackgroundCommand> BackgroundCommand { get; set; }
        public virtual DbSet<BackgroundOs> BackgroundOs { get; set; }
        public virtual DbSet<BackgroundRoles> BackgroundRoles { get; set; }
        public virtual DbSet<Command> Command { get; set; }
        public virtual DbSet<ContactInformation> ContactInformation { get; set; }
        public virtual DbSet<Data> Data { get; set; }
        public virtual DbSet<HistorySysteminfo> HistorySysteminfo { get; set; }
        public virtual DbSet<MonitorSystem> MonitorSystem { get; set; }
        public virtual DbSet<Pc> Pc { get; set; }
        public virtual DbSet<PersonalInformation> PersonalInformation { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Systeminfo> Systeminfo { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=testSQL;Username=postgres;Password=14samagu");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<BackgroundCommand>(entity =>
            {
                entity.HasKey(e => e.IdBackgroundCommand)
                    .HasName("background_command_pkey");

                entity.ToTable("background_command");

                entity.Property(e => e.IdBackgroundCommand).HasColumnName("id_background_command");

                entity.Property(e => e.Command)
                    .IsRequired()
                    .HasColumnName("command")
                    .HasMaxLength(30);

                entity.Property(e => e.Help)
                    .IsRequired()
                    .HasColumnName("help")
                    .HasMaxLength(255);

                entity.Property(e => e.IdOs).HasColumnName("id_os");

                entity.HasOne(d => d.IdOsNavigation)
                    .WithMany(p => p.BackgroundCommand)
                    .HasForeignKey(d => d.IdOs)
                    .HasConstraintName("background_command_id_os_fkey");
            });

            modelBuilder.Entity<BackgroundOs>(entity =>
            {
                entity.HasKey(e => e.IdBackgroundOs)
                    .HasName("background_os_pkey");

                entity.ToTable("background_os");

                entity.Property(e => e.IdBackgroundOs).HasColumnName("id_background_os");

                entity.Property(e => e.Nameos)
                    .IsRequired()
                    .HasColumnName("nameos")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<BackgroundRoles>(entity =>
            {
                entity.HasKey(e => e.IdBackgroundRole)
                    .HasName("background_roles_pkey");

                entity.ToTable("background_roles");

                entity.Property(e => e.IdBackgroundRole).HasColumnName("id_background_role");

                entity.Property(e => e.DescriptionRole)
                    .IsRequired()
                    .HasColumnName("description_role")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Command>(entity =>
            {
                entity.HasKey(e => e.IdHistoryCommand)
                    .HasName("command_pkey");

                entity.ToTable("command");

                entity.Property(e => e.IdHistoryCommand).HasColumnName("id_history_command");

                entity.Property(e => e.IdCommandBackgroundCommand).HasColumnName("id_command_background_command");

                entity.Property(e => e.IdDataCommand).HasColumnName("id_data_command");

                entity.Property(e => e.IdPcCommand).HasColumnName("id_pc_command");

                entity.Property(e => e.IdRecipient).HasColumnName("id_recipient");

                entity.Property(e => e.IdSender).HasColumnName("id_sender");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.IdCommandBackgroundCommandNavigation)
                    .WithMany(p => p.CommandNavigation)
                    .HasForeignKey(d => d.IdCommandBackgroundCommand)
                    .HasConstraintName("command_id_command_background_command_fkey");

                entity.HasOne(d => d.IdDataCommandNavigation)
                    .WithMany(p => p.Command)
                    .HasForeignKey(d => d.IdDataCommand)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("command_id_data_command_fkey");

                entity.HasOne(d => d.IdPcCommandNavigation)
                    .WithMany(p => p.Command)
                    .HasForeignKey(d => d.IdPcCommand)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("command_id_pc_command_fkey");

                entity.HasOne(d => d.IdRecipientNavigation)
                    .WithMany(p => p.Command)
                    .HasForeignKey(d => d.IdRecipient)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("command_id_recipient_fkey");
            });

            modelBuilder.Entity<ContactInformation>(entity =>
            {
                entity.HasKey(e => e.IdContactInformation)
                    .HasName("contact_information_pkey");

                entity.ToTable("contact_information");

                entity.Property(e => e.IdContactInformation).HasColumnName("id_contact_information");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasMaxLength(40);

                entity.Property(e => e.IdUserContactInformation).HasColumnName("id_user_contact_information");

                entity.Property(e => e.PhoneOrEmail)
                    .IsRequired()
                    .HasColumnName("phone_or_email")
                    .HasMaxLength(30);

                entity.HasOne(d => d.IdUserContactInformationNavigation)
                    .WithMany(p => p.ContactInformation)
                    .HasForeignKey(d => d.IdUserContactInformation)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("contact_information_id_user_contact_information_fkey");
            });

            modelBuilder.Entity<Data>(entity =>
            {
                entity.HasKey(e => e.IdData)
                    .HasName("data_pkey");

                entity.ToTable("data");

                entity.Property(e => e.IdData).HasColumnName("id_data");

                entity.Property(e => e.Data1)
                    .IsRequired()
                    .HasColumnName("data");

                entity.Property(e => e.LeadTime).HasColumnName("lead_time");
            });

            modelBuilder.Entity<HistorySysteminfo>(entity =>
            {
                entity.HasKey(e => e.IdHistorySysteminfo)
                    .HasName("history_systeminfo_pkey");

                entity.ToTable("history_systeminfo");

                entity.Property(e => e.IdHistorySysteminfo).HasColumnName("id_history_systeminfo");

                entity.Property(e => e.DateHistory).HasColumnName("date_history");

                entity.Property(e => e.IdPcHistorySysteminfo).HasColumnName("id_pc_history_systeminfo");

                entity.Property(e => e.IdSysteminfo).HasColumnName("id_systeminfo");

                entity.HasOne(d => d.IdPcHistorySysteminfoNavigation)
                    .WithMany(p => p.HistorySysteminfo)
                    .HasForeignKey(d => d.IdPcHistorySysteminfo)
                    .HasConstraintName("history_systeminfo_id_pc_history_systeminfo_fkey");

                entity.HasOne(d => d.IdSysteminfoNavigation)
                    .WithMany(p => p.HistorySysteminfo)
                    .HasForeignKey(d => d.IdSysteminfo)
                    .HasConstraintName("history_systeminfo_id_systeminfo_fkey");
            });


            modelBuilder.Entity<MonitorSystem>(entity =>
            {
                entity.HasKey(e => e.IdMonitorSystem)
                    .HasName("monitor_system_pkey");

                entity.ToTable("monitor_system");

                entity.Property(e => e.IdMonitorSystem).HasColumnName("id_monitor_system");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data")
                    .HasMaxLength(500);

                entity.Property(e => e.Freephysicalmemory)
                    .IsRequired()
                    .HasColumnName("freephysicalmemory")
                    .HasMaxLength(30);

                entity.Property(e => e.IdSender).HasColumnName("id_sender");

                entity.Property(e => e.Loadpercentage)
                    .IsRequired()
                    .HasColumnName("loadpercentage")
                    .HasMaxLength(10);

                entity.Property(e => e.Numberofcores)
                    .IsRequired()
                    .HasColumnName("numberofcores")
                    .HasMaxLength(10);

                entity.Property(e => e.Numberoflogicalprocessors)
                    .IsRequired()
                    .HasColumnName("numberoflogicalprocessors")
                    .HasMaxLength(10);

                entity.Property(e => e.Totalvisiblememorysize)
                    .IsRequired()
                    .HasColumnName("totalvisiblememorysize")
                    .HasMaxLength(30);

                entity.HasOne(d => d.IdSenderNavigation)
                    .WithMany(p => p.MonitorSystem)
                    .HasForeignKey(d => d.IdSender)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("monitor_system_id_sender_fkey");

                entity.Property(e => e.DateMonitorSystem).HasColumnName("date_monitor_system");


            });

            modelBuilder.Entity<Pc>(entity =>
            {
                entity.HasKey(e => e.IdPc)
                    .HasName("pc_pkey");

                entity.ToTable("pc");

                entity.Property(e => e.IdPc).HasColumnName("id_pc");

                entity.Property(e => e.IdOsPc).HasColumnName("id_os_pc");

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasColumnName("ip_address")
                    .HasMaxLength(30);

                entity.Property(e => e.NamePc)
                    .IsRequired()
                    .HasColumnName("name_pc")
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdOsPcNavigation)
                    .WithMany(p => p.Pc)
                    .HasForeignKey(d => d.IdOsPc)
                    .HasConstraintName("pc_id_os_pc_fkey");
            });

            modelBuilder.Entity<PersonalInformation>(entity =>
            {
                entity.HasKey(e => e.IdPersonalInformation)
                    .HasName("personal_information_pkey");

                entity.ToTable("personal_information");

                entity.Property(e => e.IdPersonalInformation).HasColumnName("id_personal_information");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(40);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(10);

                entity.Property(e => e.IdUserPersonalInformation).HasColumnName("id_user_personal_information");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(40);

                entity.Property(e => e.Secondname)
                    .IsRequired()
                    .HasColumnName("secondname")
                    .HasMaxLength(40);

                entity.HasOne(d => d.IdUserPersonalInformationNavigation)
                    .WithMany(p => p.PersonalInformation)
                    .HasForeignKey(d => d.IdUserPersonalInformation)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("personal_information_id_user_personal_information_fkey");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("roles_pkey");

                entity.ToTable("roles");

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.IdBackgroundrole).HasColumnName("id_backgroundrole");

                entity.Property(e => e.IdUsersRole).HasColumnName("id_users_role");

                entity.HasOne(d => d.IdBackgroundroleNavigation)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.IdBackgroundrole)
                    .HasConstraintName("roles_id_backgroundrole_fkey");

                entity.HasOne(d => d.IdUsersRoleNavigation)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.IdUsersRole)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("roles_id_users_role_fkey");
            });

            modelBuilder.Entity<Systeminfo>(entity =>
            {
                entity.HasKey(e => e.IdSysteminfo)
                    .HasName("systeminfo_pkey");

                entity.ToTable("systeminfo");

                entity.Property(e => e.IdSysteminfo).HasColumnName("id_systeminfo");

                entity.Property(e => e.ModelSystem)
                    .IsRequired()
                    .HasColumnName("model_system")
                    .HasMaxLength(255);

                entity.Property(e => e.NameOfOs)
                    .IsRequired()
                    .HasColumnName("name_of_os")
                    .HasMaxLength(255);

                entity.Property(e => e.NetworkAdapters)
                    .IsRequired()
                    .HasColumnName("network_adapters")
                    .HasMaxLength(255);

                entity.Property(e => e.Nodename)
                    .IsRequired()
                    .HasColumnName("nodename")
                    .HasMaxLength(255);

                entity.Property(e => e.PhysicalMemory)
                    .IsRequired()
                    .HasColumnName("physical_memory")
                    .HasMaxLength(255);

                entity.Property(e => e.SystemManufacturer)
                    .IsRequired()
                    .HasColumnName("system_manufacturer")
                    .HasMaxLength(255);

                entity.Property(e => e.TypeOfSystem)
                    .IsRequired()
                    .HasColumnName("type_of_system")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(30);

                entity.Property(e => e.Nameusers)
                    .IsRequired()
                    .HasColumnName("nameusers")
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20);
            });
        }
    }
}
