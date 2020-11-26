using Microsoft.AspNetCore.Identity;
using QueR.Domain;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.DAL.Seed
{
    public static class DbSeed
    {
        static PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

        public static IEnumerable<Company> Companies {
            get => new List<Company>
            {
                new Company
                {
                    Id = 1,
                    Name = "Company A",
                    MailingAddress = "Address of Company A",
                    AdministratorId = 2
                },
                new Company
                {
                    Id = 2,
                    Name = "Company B",
                    MailingAddress = "Address of Company B",
                    AdministratorId = 3
                },
            };
        }

        public static IEnumerable<QueueType> QueueTypes {
            get => new List<QueueType>
            {
                new QueueType
                {
                    Id = 1,
                    CompanyId = 1,
                    IsEnabled = true,
                    Name = "Warranty"
                },
                new QueueType
                {
                    Id = 2,
                    CompanyId = 1,
                    IsEnabled = true,
                    Name = "Returns"
                },
                new QueueType
                {
                    Id = 3,
                    CompanyId = 2,
                    IsEnabled = true,
                    Name = "General administration"
                }
            };
        }

        public static IEnumerable<Site> Sites {
            get => new List<Site>
            {
                new Site
                {
                    Id = 1,
                    CompanyId = 1,
                    Name = "A Site 1",
                    Address = "random address",
                    ManagerId = 4
                },
                new Site
                {
                    Id = 2,
                    CompanyId = 1,
                    Name = "A Site 2",
                    Address = "random address",
                    ManagerId = 5
                },
                new Site
                {
                    Id = 3,
                    CompanyId = 2,
                    Name = "B Site 1",
                    Address = "random address",
                    ManagerId = 6
                },
                new Site
                {
                    Id = 4,
                    CompanyId = 2,
                    Name = "B Site 2",
                    Address = "random address",
                    ManagerId = 7
                },
            };
        }

        public static IEnumerable<IdentityRole<int>> Roles {
            get => new List<IdentityRole<int>> {
                new IdentityRole<int>
                {
                    Id = 1,
                    Name = "operator",
                    NormalizedName = "OPERATOR",
                },
                new IdentityRole<int>
                {
                    Id = 2,
                    Name = "administrator",
                    NormalizedName = "ADMINISTRATOR",
                },
                new IdentityRole<int>
                {
                    Id = 3,
                    Name = "manager",
                    NormalizedName = "MANAGER",
                },
                new IdentityRole<int>
                {
                    Id = 4,
                    Name = "employee",
                    NormalizedName = "EMPLOYEE",
                },
                new IdentityRole<int>
                {
                    Id = 5,
                    Name = "user",
                    NormalizedName = "USER",
                },
            };
        }

        public static IEnumerable<ApplicationUser> Users {
            get => new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = 1,
                    Email = "operator@test.hu",
                    NormalizedEmail = "OPERATOR@TEST.HU",
                    UserName = "operator",
                    NormalizedUserName = "OPERATOR",
                    FirstName = "Test",
                    LastName = "Operator",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },

                new ApplicationUser
                {
                    Id = 2,
                    Email = "admina@test.hu",
                    NormalizedEmail = "ADMINA@TEST.HU",
                    UserName = "admina",
                    NormalizedUserName = "ADMINA",
                    FirstName = "Admin",
                    LastName = "A",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser
                {
                    Id = 3,
                    Email = "adminb@test.hu",
                    NormalizedEmail = "ADMINB@TEST.HU",
                    UserName = "adminb",
                    NormalizedUserName = "ADMINB",
                    FirstName = "Admin",
                    LastName = "B",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },

                new ApplicationUser
                {
                    Id = 4,
                    Email = "managera1@test.hu",
                    NormalizedEmail = "MANAGERA1@TEST.HU",
                    UserName = "managera1",
                    NormalizedUserName = "MANAGERA1",
                    FirstName = "Manager",
                    LastName = "A1",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser
                {
                    Id = 5,
                    Email = "managera2@test.hu",
                    NormalizedEmail = "MANAGERA2@TEST.HU",
                    UserName = "managera2",
                    NormalizedUserName = "MANAGERA2",
                    FirstName = "Manager",
                    LastName = "A2",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser
                {
                    Id = 6,
                    Email = "managerb1@test.hu",
                    NormalizedEmail = "MANAGERB1@TEST.HU",
                    UserName = "managerb1",
                    NormalizedUserName = "MANAGERB1",
                    FirstName = "Manager",
                    LastName = "B1",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser
                {
                    Id = 7,
                    Email = "managerb2@test.hu",
                    NormalizedEmail = "MANAGERB2@TEST.HU",
                    UserName = "managerb2",
                    NormalizedUserName = "MANAGERB2",
                    FirstName = "Manager",
                    LastName = "B2",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },

                new ApplicationUser
                {
                    Id = 8,
                    Email = "workera11@test.hu",
                    NormalizedEmail = "WORKERA11@TEST.HU",
                    UserName = "workera11",
                    NormalizedUserName = "WORKERA11",
                    FirstName = "Worker",
                    LastName = "A11",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser
                {
                    Id = 9,
                    Email = "workera12@test.hu",
                    NormalizedEmail = "WORKERA12@TEST.HU",
                    UserName = "workera12",
                    NormalizedUserName = "WORKERA12",
                    FirstName = "Worker",
                    LastName = "A12",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser
                {
                    Id = 10,
                    Email = "workera21@test.hu",
                    NormalizedEmail = "WORKERA21@TEST.HU",
                    UserName = "workera21",
                    NormalizedUserName = "WORKERA21",
                    FirstName = "Worker",
                    LastName = "A21",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser
                {
                    Id = 11,
                    Email = "workera22@test.hu",
                    NormalizedEmail = "WORKERA22@TEST.HU",
                    UserName = "workera22",
                    NormalizedUserName = "WORKERA22",
                    FirstName = "Worker",
                    LastName = "A22",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser
                {
                    Id = 12,
                    Email = "workerb11@test.hu",
                    NormalizedEmail = "WORKERB11@TEST.HU",
                    UserName = "workerb11",
                    NormalizedUserName = "WORKERB11",
                    FirstName = "Worker",
                    LastName = "B11",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser
                {
                    Id = 13,
                    Email = "workerb12@test.hu",
                    NormalizedEmail = "WORKERB12@TEST.HU",
                    UserName = "workerb12",
                    NormalizedUserName = "WORKERB12",
                    FirstName = "Worker",
                    LastName = "B12",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser
                {
                    Id = 14,
                    Email = "workerb21@test.hu",
                    NormalizedEmail = "WORKERB21@TEST.HU",
                    UserName = "workerb21",
                    NormalizedUserName = "WORKERB21",
                    FirstName = "Worker",
                    LastName = "B21",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },
                new ApplicationUser
                {
                    Id = 15,
                    Email = "workerb22@test.hu",
                    NormalizedEmail = "WORKERB22@TEST.HU",
                    UserName = "workerb22",
                    NormalizedUserName = "WORKERB22",
                    FirstName = "Worker",
                    LastName = "B22",
                    Gender = Gender.Other,
                    Address = "",
                    PasswordHash = hasher.HashPassword(null, "123456")
                },

            };
        }

        public static IEnumerable<IdentityUserRole<int>> UserRoles
        {
            get => new List<IdentityUserRole<int>>
            {
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 },

                new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
                new IdentityUserRole<int> { UserId = 3, RoleId = 2 },

                new IdentityUserRole<int> { UserId = 4, RoleId = 3 },
                new IdentityUserRole<int> { UserId = 5, RoleId = 3 },
                new IdentityUserRole<int> { UserId = 6, RoleId = 3 },
                new IdentityUserRole<int> { UserId = 7, RoleId = 3 },
                new IdentityUserRole<int> { UserId = 4, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 5, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 6, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 7, RoleId = 4 },

                new IdentityUserRole<int> { UserId = 8, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 9, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 10, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 11, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 12, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 13, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 14, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 15, RoleId = 4 },
            };
        }

        public static Dictionary<IEnumerable<string>, IEnumerable<ApplicationUser>> Userss {
            get => new Dictionary<IEnumerable<string>, IEnumerable<ApplicationUser>>
            {
                { new List<string>{ "operator" }, new List<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Id = 1,
                            Email = "operator@test.hu",
                            UserName = "operator",
                            FirstName = "Test",
                            LastName = "Operator",
                            Gender = Gender.Other
                        }
                    }
                },
                {
                     new List<string>{ "administrator" }, new List<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Id = 2,
                            Email = "admina@test.hu",
                            UserName = "admina",
                            FirstName = "Admin",
                            LastName = "A",
                            Gender = Gender.Male,
                            CompanyId = 1
                        },
                        new ApplicationUser
                        {
                            Id = 3,
                            Email = "adminb@test.hu",
                            UserName = "adminb",
                            FirstName = "Admin",
                            LastName = "B",
                            Gender = Gender.Female,
                            CompanyId = 2
                        },
                    }
                },
                {
                    new List<string>{ "manager", "employee" }, new List<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Id = 4,
                            Email = "managera1@test.hu",
                            UserName = "managera1",
                            FirstName = "Manager",
                            LastName = "A1",
                            Gender = Gender.Male,
                            CompanyId = 1
                        },
                        new ApplicationUser
                        {
                            Id = 5,
                            Email = "managera2@test.hu",
                            UserName = "managera2",
                            FirstName = "Manager",
                            LastName = "A2",
                            Gender = Gender.Female,
                            CompanyId = 1
                        },
                        new ApplicationUser
                        {
                            Id = 6,
                            Email = "managerb1@test.hu",
                            UserName = "managerb1",
                            FirstName = "Manager",
                            LastName = "B1",
                            Gender = Gender.Female,
                            CompanyId = 2 
                        },
                        new ApplicationUser
                        {
                            Id = 7,
                            Email = "managerb2@test.hu",
                            UserName = "managerb2",
                            FirstName = "Manager",
                            LastName = "B2",
                            Gender = Gender.Male,
                            CompanyId = 2
                        },
                    }
                },
                {
                    new List<string>{ "employee" }, new List<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Id = 8,
                            Email = "workera11@test.hu",
                            UserName = "workera11",
                            FirstName = "Worker",
                            LastName = "A1 1",
                            Gender = Gender.Female,
                            CompanyId = 1,
                        },
                        new ApplicationUser
                        {
                            Id = 9,
                            Email = "workera12@test.hu",
                            UserName = "workera12",
                            FirstName = "Worker",
                            LastName = "A1 2",
                            Gender = Gender.Female,
                            CompanyId = 1
                        },
                        new ApplicationUser
                        {
                            Id = 10,
                            Email = "workera21@test.hu",
                            UserName = "workera21",
                            FirstName = "Worker",
                            LastName = "A2 1",
                            Gender = Gender.Male,
                            CompanyId = 1
                        },
                        new ApplicationUser
                        {
                            Id = 11,
                            Email = "workera22@test.hu",
                            UserName = "workera22",
                            FirstName = "Worker",
                            LastName = "A2 2",
                            Gender = Gender.Male,
                            CompanyId = 1
                        },
                        new ApplicationUser
                        {
                            Id = 12,
                            Email = "workerb11@test.hu",
                            UserName = "workerb11",
                            FirstName = "Worker",
                            LastName = "B1 1",
                            Gender = Gender.Female,
                            CompanyId = 2
                        },
                        new ApplicationUser
                        {
                            Id = 13,
                            Email = "workerb12@test.hu",
                            UserName = "workerb12",
                            FirstName = "Worker",
                            LastName = "B1 2",
                            Gender = Gender.Female,
                            CompanyId = 2
                        },
                        new ApplicationUser
                        {
                            Id = 14,
                            Email = "workerb21@test.hu",
                            UserName = "workerb21",
                            FirstName = "Worker",
                            LastName = "B2 1",
                            Gender = Gender.Male,
                            CompanyId = 2
                        },
                        new ApplicationUser
                        {
                            Id = 15,
                            Email = "workerb22@test.hu",
                            UserName = "workerb22",
                            FirstName = "Worker",
                            LastName = "B2 2",
                            Gender = Gender.Male,
                            CompanyId = 2
                        },
                    }
                }/*,
                {
                    "user", new List<ApplicationUser>
                    {

                    }
                }*/
            };

        }
    }
}
