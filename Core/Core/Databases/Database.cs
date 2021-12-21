using Core.Helpers;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using DayOfWeek = Core.Models.DayOfWeek;

namespace Core.Databases
{

    /// <summary>
    /// Add-Migration Initial -P Core -S CoreDatabaseConfiguration
    /// Remove-Migration -P Core -S CoreDatabaseConfiguration
    /// update-database -P Core -S CoreDatabaseConfiguration
    /// </summary>

    public class Database : DbContext
    {
        #region Fields
        string path;
        static Database instance;
        #endregion
        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database(Constants.DatabasePath);
                    instance.Initialize();
                }

                return instance;
            }

        }

        public DbSet<DayOfWeek> DayOfWeeks { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkBalance> WorkBalances { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Settings> Settings { get; set; }


        public Database(DbContextOptions<Database> options) : base(options)
        { }

        // contrutor used to build migration on project time
        public Database(string path) : base()
        {
            this.path = path;

        }

        public void Initialize()
        {
#if DEBUG
            // Database.EnsureDeleted();
#endif
            Database.Migrate();

            if (!Settings.Any())
            {
                Settings.Add(new Settings());
                SaveChanges();
            }

            if (!DayOfWeeks.Any())
            {
                DayOfWeeks.AddRange(new[]
                {
                    new DayOfWeek{Name = "Sunday", Number = 1},
                    new DayOfWeek{Name = "Monday", Number = 2},
                    new DayOfWeek{Name = "Tuesday", Number = 3},
                    new DayOfWeek{Name = "Wednesday", Number = 4},
                    new DayOfWeek{Name = "Thursday", Number = 5},
                    new DayOfWeek{Name = "Friday", Number = 6},
                    new DayOfWeek{Name = "Saturday", Number = 7},
                });

                SaveChanges();
            }

            if (!Payments.Any())
            {
                Payments.AddRange(new[]
                {
                    // sunday   
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 1) ,StartHour = new TimeSpan(0,1,0), FinishHour = new TimeSpan(9,0,0), AmounPaid=30},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 1) ,StartHour = new TimeSpan(9,1,0), FinishHour = new TimeSpan(18,0,0), AmounPaid=20},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 1) ,StartHour = new TimeSpan(18,1,0), FinishHour = new TimeSpan(0,0,0), AmounPaid=25},

                    // monday   
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 2) ,StartHour = new TimeSpan(0,1,0), FinishHour = new TimeSpan(9,0,0), AmounPaid=25},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 2) ,StartHour = new TimeSpan(9,1,0), FinishHour = new TimeSpan(18,0,0), AmounPaid=15},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 2) ,StartHour = new TimeSpan(18,1,0), FinishHour = new TimeSpan(0,0,0), AmounPaid=20},
                                        
                    // thuesday   
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 3) ,StartHour = new TimeSpan(0,1,0), FinishHour = new TimeSpan(9,0,0), AmounPaid=25},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 3) ,StartHour = new TimeSpan(9,1,0), FinishHour = new TimeSpan(18,0,0), AmounPaid=15},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 3) ,StartHour = new TimeSpan(18,1,0), FinishHour = new TimeSpan(0,0,0), AmounPaid=20},

                    // Wednesday   
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 4) ,StartHour = new TimeSpan(0,1,0), FinishHour = new TimeSpan(9,0,0), AmounPaid=25},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 4) ,StartHour = new TimeSpan(9,1,0), FinishHour = new TimeSpan(18,0,0), AmounPaid=15},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 4) ,StartHour = new TimeSpan(18,1,0), FinishHour = new TimeSpan(0,0,0), AmounPaid=20},

                    // Thursday   
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 5) ,StartHour = new TimeSpan(0,1,0), FinishHour = new TimeSpan(9,0,0), AmounPaid=25},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 5) ,StartHour = new TimeSpan(9,1,0), FinishHour = new TimeSpan(18,0,0), AmounPaid=15},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 5) ,StartHour = new TimeSpan(18,1,0), FinishHour = new TimeSpan(0,0,0), AmounPaid=20},
                         
                    // Friday   
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 6) ,StartHour = new TimeSpan(0,1,0), FinishHour = new TimeSpan(9,0,0), AmounPaid=25},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 6) ,StartHour = new TimeSpan(9,1,0), FinishHour = new TimeSpan(18,0,0), AmounPaid=15},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 6) ,StartHour = new TimeSpan(18,1,0), FinishHour = new TimeSpan(0,0,0), AmounPaid=20},

                    // saturday   
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 7) ,StartHour = new TimeSpan(0,1,0), FinishHour = new TimeSpan(9,0,0), AmounPaid=30},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 7) ,StartHour = new TimeSpan(9,1,0), FinishHour = new TimeSpan(18,0,0), AmounPaid=20},
                    new Payment{DayOfWeek = DayOfWeeks.First(s=> s.Number == 7) ,StartHour = new TimeSpan(18,1,0), FinishHour = new TimeSpan(0,0,0), AmounPaid=25},
                });

                SaveChanges();
            }

            if (!Employees.Any())
            {
                Employees.AddRange(new[]
                {
                    new Employee
                    {
                        Name = "Rene",
                        WorkBalance = new WorkBalance
                        {
                            TotalPaid = 215,
                            Works = new List<Work>
                            {
                                new Work
                                {
                                    DayOfWeek = DayOfWeeks.Where(day => day.Number == 2).FirstOrDefault(),
                                    StartHour = new TimeSpan(10,0,0),
                                    FinishHour = new TimeSpan(12,0,0),
                                    AmounPaid = 30
                                },
                                new Work
                                {
                                    DayOfWeek = DayOfWeeks.Where(day => day.Number == 3).FirstOrDefault(),
                                    StartHour = new TimeSpan(10,0,0),
                                    FinishHour = new TimeSpan(12,0,0),
                                    AmounPaid = 30
                                },
                                new Work
                                {
                                    DayOfWeek = DayOfWeeks.Where(day => day.Number == 5).FirstOrDefault(),
                                    StartHour = new TimeSpan(1,0,0),
                                    FinishHour = new TimeSpan(3,0,0),
                                    AmounPaid = 50
                                },
                                new Work
                                {
                                    DayOfWeek = DayOfWeeks.Where(day => day.Number == 7).FirstOrDefault(),
                                    StartHour = new TimeSpan(14,0,0),
                                    FinishHour = new TimeSpan(18,0,0),
                                    AmounPaid = 80
                                },
                                new Work
                                {
                                    DayOfWeek = DayOfWeeks.Where(day => day.Number == 1).FirstOrDefault(),
                                    StartHour = new TimeSpan(20,0,0),
                                    FinishHour = new TimeSpan(21,0,0),
                                    AmounPaid = 25
                                }
                            }
                        }

                    },
                    new Employee
                    {
                        Name = "Astrid",
                        WorkBalance = new WorkBalance
                        {
                            TotalPaid = 85,
                            Works = new List<Work>
                            {
                                new Work
                                {
                                    DayOfWeek = DayOfWeeks.Where(day => day.Number == 2).FirstOrDefault(),
                                    StartHour = new TimeSpan(10,0,0),
                                    FinishHour = new TimeSpan(12,0,0),
                                    AmounPaid = 30
                                },
                                new Work
                                {
                                    DayOfWeek = DayOfWeeks.Where(day => day.Number == 5).FirstOrDefault(),
                                    StartHour = new TimeSpan(12,0,0),
                                    FinishHour = new TimeSpan(14,0,0),
                                    AmounPaid = 30
                                },

                                new Work
                                {
                                    DayOfWeek = DayOfWeeks.Where(day => day.Number == 1).FirstOrDefault(),
                                    StartHour = new TimeSpan(20,0,0),
                                    FinishHour = new TimeSpan(21,0,0),
                                    AmounPaid = 25
                                }
                            }
                        }
                    }
                });

                SaveChanges();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(path))
            {
                optionsBuilder.UseSqlite($"Filename={path}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DayOfWeek>().HasMany(dayOfWeek => dayOfWeek.Payments).WithOne(payment => payment.DayOfWeek).HasForeignKey(payment => payment.DayOfWeekId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Employee>().HasOne(employee => employee.WorkBalance).WithOne(workBalance => workBalance.Employee).HasForeignKey<WorkBalance>(workBalance => workBalance.EmployeeId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<WorkBalance>().HasMany(workBalance => workBalance.Works).WithOne(work => work.WorkBalance).HasForeignKey(work => work.WorkBalanceId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
