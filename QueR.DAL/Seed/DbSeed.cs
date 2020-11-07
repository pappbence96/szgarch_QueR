using QueR.Domain;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.DAL.Seed
{
    public static class DbSeed
    {
        public static IEnumerable<Company> Companies {
            get => new List<Company>
            {
                new Company
                {
                    Id = 1,
                    Name = "Company A",
                    MailingAddress = "Address of Company A"
                },
                new Company
                {
                    Id = 2,
                    Name = "Company B",
                    MailingAddress = "Address of Company B"
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
                    Address = "random address"
                },
                new Site
                {
                    Id = 2,
                    CompanyId = 1,
                    Name = "A Site 2",
                    Address = "random address"
                },
                new Site
                {
                    Id = 3,
                    CompanyId = 2,
                    Name = "B Site 1",
                    Address = "random address"
                },
                new Site
                {
                    Id = 4,
                    CompanyId = 2,
                    Name = "B Site 2",
                    Address = "random address"
                },
            };
        }

        public static IEnumerable<string> Roles {
            get => new List<string> { "user", "employee", "manager", "administrator", "operator" };
        }

        public static Dictionary<string, IEnumerable<ApplicationUser>> Users {
            get => new Dictionary<string, IEnumerable<ApplicationUser>>
            {
                { "operator", new List<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Email = "operator@test.hu",
                            UserName = "operator",
                            FirstName = "Test",
                            LastName = "Operator",
                            Gender = Gender.Other
                        }
                    }
                },
                {
                    "administrator", new List<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Email = "admina@test.hu",
                            UserName = "admina",
                            FirstName = "Admin",
                            LastName = "A",
                            Gender = Gender.Male
                        },
                        new ApplicationUser
                        {
                            Email = "adminb@test.hu",
                            UserName = "adminb",
                            FirstName = "Admin",
                            LastName = "B",
                            Gender = Gender.Female
                        },
                    }
                },
                {
                    "manager", new List<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Email = "managera1@test.hu",
                            UserName = "managera1",
                            FirstName = "Manager",
                            LastName = "A1",
                            Gender = Gender.Male
                        },
                        new ApplicationUser
                        {
                            Email = "managera2@test.hu",
                            UserName = "managera2",
                            FirstName = "Manager",
                            LastName = "A2",
                            Gender = Gender.Female
                        },
                        new ApplicationUser
                        {
                            Email = "managerb1@test.hu",
                            UserName = "managerb1",
                            FirstName = "Manager",
                            LastName = "B1",
                            Gender = Gender.Female
                        },
                        new ApplicationUser
                        {
                            Email = "managerb2@test.hu",
                            UserName = "managerb2",
                            FirstName = "Manager",
                            LastName = "B2",
                            Gender = Gender.Male
                        },
                    }
                },/*
                {
                    "employee", new List<ApplicationUser>
                    {
                        new ApplicationUser
                        {
                            Email = "workera11@test.hu",
                            UserName = "workera11",
                            FirstName = "Worker",
                            LastName = "A1 1",
                            Gender = Gender.Female,
                        },
                        new ApplicationUser
                        {
                            Email = "workera12@test.hu",
                            UserName = "workera12",
                            FirstName = "Worker",
                            LastName = "A1 2",
                            Gender = Gender.Female,
                        },
                        new ApplicationUser
                        {
                            Email = "workera21@test.hu",
                            UserName = "workera21",
                            FirstName = "Worker",
                            LastName = "A2 1",
                            Gender = Gender.Male,
                        },
                        new ApplicationUser
                        {
                            Email = "workera22@test.hu",
                            UserName = "workera22",
                            FirstName = "Worker",
                            LastName = "A2 2",
                            Gender = Gender.Male,
                        },
                        new ApplicationUser
                        {
                            Email = "workerb11@test.hu",
                            UserName = "workerb11",
                            FirstName = "Worker",
                            LastName = "B1 1",
                            Gender = Gender.Female,
                        },
                        new ApplicationUser
                        {
                            Email = "workerb12@test.hu",
                            UserName = "workerb12",
                            FirstName = "Worker",
                            LastName = "B1 2",
                            Gender = Gender.Female,
                        },
                        new ApplicationUser
                        {
                            Email = "workerb21@test.hu",
                            UserName = "workerb21",
                            FirstName = "Worker",
                            LastName = "B2 1",
                            Gender = Gender.Male,
                        },
                        new ApplicationUser
                        {
                            Email = "workerb22@test.hu",
                            UserName = "workerb22",
                            FirstName = "Worker",
                            LastName = "B2 2",
                            Gender = Gender.Male,
                        },
                    }
                },
                {
                    "user", new List<ApplicationUser>
                    {

                    }
                }*/
            };

        }
    }
}
