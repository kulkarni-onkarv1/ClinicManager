using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CS_DoctorVolumeMiniProj.Models2
{
    public partial class DoctorVolumeContext : DbContext
    {
        public DoctorVolumeContext()
        {
        }

        public DoctorVolumeContext(DbContextOptions<DoctorVolumeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<LabTest> LabTests { get; set; } = null!;
        public virtual DbSet<OutDoorPatient> OutDoorPatients { get; set; } = null!;
        public virtual DbSet<PatientDiease> PatientDieases { get; set; } = null!;
        public virtual DbSet<PatientLabReport> PatientLabReports { get; set; } = null!;
        public virtual DbSet<PatientPrescribedMedicine> PatientPrescribedMedicines { get; set; } = null!;
        public virtual DbSet<PatientRefLabTest> PatientRefLabTests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DoctorVolume;Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorRegNo)
                    .HasName("PK__Doctors__EC836D32DBD1ADA4");

                entity.Property(e => e.DoctorRegNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorDegree)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<LabTest>(entity =>
            {
                entity.HasKey(e => e.TestRn)
                    .HasName("PK__LabTests__8CEC4217352B6998");

                entity.Property(e => e.TestRn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TestRN");

                entity.Property(e => e.TestCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TestDescription)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OutDoorPatient>(entity =>
            {
                entity.HasKey(e => e.PatientId)
                    .HasName("PK__OutDoorP__970EC346ED0E71B7");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.DoctorRegNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PatientAddress)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.PatientIdcardNumber)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PatientIDCardNumber");

                entity.Property(e => e.PatientName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.DoctorRegNoNavigation)
                    .WithMany(p => p.OutDoorPatients)
                    .HasForeignKey(d => d.DoctorRegNo)
                    .HasConstraintName("FK__OutDoorPa__Docto__3F466844");
            });

            modelBuilder.Entity<PatientDiease>(entity =>
            {
                entity.HasKey(e => e.Unr)
                    .HasName("PK__PatientD__C5B17EBDE3AC4C2A");

                entity.ToTable("PatientDiease");

                entity.Property(e => e.Unr).HasColumnName("UNR");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Diease)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fees).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Hdl)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HDL");

                entity.Property(e => e.Ldl)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LDL");

                entity.Property(e => e.NextAppointmentDate).HasColumnType("date");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.RegDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientDieases)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__PatientDi__Patie__5EBF139D");
            });

            modelBuilder.Entity<PatientLabReport>(entity =>
            {
                entity.HasKey(e => e.ReportNumber)
                    .HasName("PK__PatientL__5A964EF939224ADE");

                entity.ToTable("PatientLabReport");

                entity.Property(e => e.DoctorRegNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Impression)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.RegDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TestRn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TestRN");

                entity.HasOne(d => d.DoctorRegNoNavigation)
                    .WithMany(p => p.PatientLabReports)
                    .HasForeignKey(d => d.DoctorRegNo)
                    .HasConstraintName("FK__PatientLa__Docto__4F7CD00D");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientLabReports)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__PatientLa__Patie__4D94879B");

                entity.HasOne(d => d.TestRnNavigation)
                    .WithMany(p => p.PatientLabReports)
                    .HasForeignKey(d => d.TestRn)
                    .HasConstraintName("FK__PatientLa__TestR__4E88ABD4");
            });

            modelBuilder.Entity<PatientPrescribedMedicine>(entity =>
            {
                entity.HasKey(e => e.Urn)
                    .HasName("PK__PatientP__C5B1000EB63EED2F");

                entity.Property(e => e.Urn).HasColumnName("URN");

                entity.Property(e => e.MedicineName)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientPrescribedMedicines)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__PatientPr__Patie__5BE2A6F2");
            });

            modelBuilder.Entity<PatientRefLabTest>(entity =>
            {
                entity.HasKey(e => e.UniqueId)
                    .HasName("PK__PatientR__A2A2BAAA59C5B642");

                entity.Property(e => e.UniqueId).HasColumnName("UniqueID");

                entity.Property(e => e.DoctorRegNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.RegDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TestRn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TestRN");

                entity.HasOne(d => d.DoctorRegNoNavigation)
                    .WithMany(p => p.PatientRefLabTests)
                    .HasForeignKey(d => d.DoctorRegNo)
                    .HasConstraintName("FK__PatientRe__Docto__49C3F6B7");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientRefLabTests)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__PatientRe__Patie__47DBAE45");

                entity.HasOne(d => d.TestRnNavigation)
                    .WithMany(p => p.PatientRefLabTests)
                    .HasForeignKey(d => d.TestRn)
                    .HasConstraintName("FK__PatientRe__TestR__48CFD27E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
