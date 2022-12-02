using Microsoft.AspNetCore.Identity;

using fa22LBT.Models;
using fa22LBT.Utilities;
using fa22LBT.DAL;
using Azure.Core;
using static Azure.Core.HttpHeader;
using System.Diagnostics.Metrics;

namespace fa22LBT.Seeding
{
    public static class SeedUsers
    {
        public async static Task<IdentityResult> SeedAllUsers(UserManager<AppUser> userManager, AppDbContext context)
        {
            //Create a list of AddUserModels
            List<AddUserModel> AllUsers = new List<AddUserModel>();

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    PhoneNumber = "(512)555-1234",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Admin",
                    LastName = "Admin",
                    Address = "123 Admin St.",
                    City = "Houston",
                    State = "Texas",
                    ZipCode = "77001",
                    DOB = new DateTime(2000, 1, 1),
                    IsActive = true
                },
                Password = "Abc123!",
                RoleName = "Admin"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "bevo@example.com",
                    Email = "bevo@example.com",
                    PhoneNumber = "(512)555-5555",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Bevo",
                    MiddleInitial = "T",
                    LastName = "Customer",
                    Address = "123 Customer St.",
                    City = "Houston",
                    State = "Texas",
                    ZipCode = "77001",
                    DOB = new DateTime(2000, 1, 1),
                    IsActive = true
                },
                Password = "Password123!",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "cbaker@freezing.co.uk",
                    Email = "cbaker@freezing.co.uk",
                    PhoneNumber = "5125571146",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Christopher",
                    MiddleInitial = "L",
                    LastName = "Baker",
                    Address = "1245 Lake Austin Blvd.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78733",
                    DOB = new DateTime(1991, 2, 7),
                    IsActive = true
                },
                Password = "gazing",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "mb@aool.com",
                    Email = "mb@aool.com",
                    PhoneNumber = "2102678873",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Michelle",
                    LastName = "Banks",
                    Address = "1300 Tall Pine Lane",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78261",
                    DOB = new DateTime(1990, 6, 23),
                    IsActive = true
                },
                Password = "banquet",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "fd@aool.com",
                    Email = "fd@aool.com",
                    PhoneNumber = "8175659699",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Franco",
                    MiddleInitial = "V",
                    LastName = "Broccolo",
                    Address = "62 Browning Rd",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77019",
                    DOB = new DateTime(1986, 5, 6),
                    IsActive = true
                },
                Password = "666666",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "wendy@ggmail.com",
                    Email = "wendy@ggmail.com",
                    PhoneNumber = "5125943222",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Wendy",
                    MiddleInitial = "L",
                    LastName = "Chang",
                    Address = "202 Bellmont Hall",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78713",
                    DOB = new DateTime(1964, 12, 21),
                    IsActive = true
                },
                Password = "clover",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "limchou@yaho.com",
                    Email = "limchou@yaho.com",
                    PhoneNumber = "2107724599",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Lim",
                    LastName = "Chou",
                    Address = "1600 Teresa Lane",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78266",
                    DOB = new DateTime(1950, 6, 14),
                    IsActive = true
                },
                Password = "austin",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "Dixon@aool.com",
                    Email = "Dixon@aool.com",
                    PhoneNumber = "2142643255",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Shan",
                    MiddleInitial = "D",
                    LastName = "Dixon",
                    Address = "234 Holston Circle",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75208",
                    DOB = new DateTime(1930, 5, 9),
                    IsActive = true
                },
                Password = "mailbox",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "louann@ggmail.com",
                    Email = "louann@ggmail.com",
                    PhoneNumber = "8172556749",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Lou Ann",
                    MiddleInitial = "K",
                    LastName = "Feeley",
                    Address = "600 S 8th Street W",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77010",
                    DOB = new DateTime(1930, 2, 24),
                    IsActive = true
                },
                Password = "aggies",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "tfreeley@minntonka.ci.state.mn.us",
                    Email = "tfreeley@minntonka.ci.state.mn.us",
                    PhoneNumber = "8173255687",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Tesa",
                    MiddleInitial = "P",
                    LastName = "Freeley",
                    Address = "4448 Fairview Ave.",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77009",
                    DOB = new DateTime(1935, 9, 1),
                    IsActive = true
                },
                Password = "raiders",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "mgar@aool.com",
                    Email = "mgar@aool.com",
                    PhoneNumber = "8176593544",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Margaret",
                    MiddleInitial = "L",
                    LastName = "Garcia",
                    Address = "594 Longview",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77003",
                    DOB = new DateTime(1990, 7, 3),
                    IsActive = true
                },
                Password = "mustangs",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "chaley@thug.com",
                    Email = "chaley@thug.com",
                    PhoneNumber = "2148475583",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Charles",
                    MiddleInitial = "E",
                    LastName = "Haley",
                    Address = "One Cowboy Pkwy",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75261",
                    DOB = new DateTime(1985, 9, 17),
                    IsActive = true
                },
                Password = "region",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "jeff@ggmail.com",
                    Email = "jeff@ggmail.com",
                    PhoneNumber = "5126978613",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jeffrey",
                    MiddleInitial = "T",
                    LastName = "Hampton",
                    Address = "337 38th St.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78705",
                    DOB = new DateTime(1995, 1, 23),
                    IsActive = true
                },
                Password = "hungry",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "wjhearniii@umch.edu",
                    Email = "wjhearniii@umch.edu",
                    PhoneNumber = "2148965621",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "John",
                    MiddleInitial = "B",
                    LastName = "Hearn",
                    Address = "4225 North First",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75237",
                    DOB = new DateTime(1994, 1, 8),
                    IsActive = true
                },
                Password = "logicon",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "hicks43@ggmail.com",
                    Email = "hicks43@ggmail.com",
                    PhoneNumber = "2105788965",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Anthony",
                    MiddleInitial = "J",
                    LastName = "Hicks",
                    Address = "32 NE Garden Ln., Ste 910",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78239",
                    DOB = new DateTime(1990, 10, 6),
                    IsActive = true
                },
                Password = "doofus",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "bradsingram@mall.utexas.edu",
                    Email = "bradsingram@mall.utexas.edu",
                    PhoneNumber = "5124678821",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Brad",
                    MiddleInitial = "S",
                    LastName = "Ingram",
                    Address = "6548 La Posada Ct.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78736",
                    DOB = new DateTime(1984, 4, 12),
                    IsActive = true
                },
                Password = "mother",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "mother.Ingram@aool.com",
                    Email = "mother.Ingram@aool.com",
                    PhoneNumber = "5124653365",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Todd",
                    MiddleInitial = "L",
                    LastName = "Jacobs",
                    Address = "4564 Elm St.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78731",
                    DOB = new DateTime(1983, 4, 4),
                    IsActive = true
                },
                Password = "whimsical",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "victoria@aool.com",
                    Email = "victoria@aool.com",
                    PhoneNumber = "5129457399",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Victoria",
                    MiddleInitial = "M",
                    LastName = "Lawrence",
                    Address = "6639 Butterfly Ln.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78761",
                    DOB = new DateTime(1961, 2, 3),
                    IsActive = true
                },
                Password = "nothing",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "lineback@flush.net",
                    Email = "lineback@flush.net",
                    PhoneNumber = "2102449976",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Erik",
                    MiddleInitial = "W",
                    LastName = "Lineback",
                    Address = "1300 Netherland St",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78293",
                    DOB = new DateTime(1946, 9, 3),
                    IsActive = true
                },
                Password = "GoodFellow",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "elowe@netscrape.net",
                    Email = "elowe@netscrape.net",
                    PhoneNumber = "2105344627",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Ernest",
                    MiddleInitial = "S",
                    LastName = "Lowe",
                    Address = "3201 Pine Drive",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78279",
                    DOB = new DateTime(1992, 2, 7),
                    IsActive = true
                },
                Password = "impede",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "luce_chuck@ggmail.com",
                    Email = "luce_chuck@ggmail.com",
                    PhoneNumber = "2106983548",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Chuck",
                    MiddleInitial = "B",
                    LastName = "Luce",
                    Address = "2345 Rolling Clouds",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78268",
                    DOB = new DateTime(1942, 10, 25),
                    IsActive = true
                },
                Password = "LuceyDucey",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "mackcloud@pimpdaddy.com",
                    Email = "mackcloud@pimpdaddy.com",
                    PhoneNumber = "5124748138",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jennifer",
                    MiddleInitial = "D",
                    LastName = "MacLeod",
                    Address = "2504 Far West Blvd.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78731",
                    DOB = new DateTime(1965, 8, 6),
                    IsActive = true
                },
                Password = "cloudyday",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "liz@ggmail.com",
                    Email = "liz@ggmail.com",
                    PhoneNumber = "5124579845",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Elizabeth",
                    MiddleInitial = "P",
                    LastName = "Markham",
                    Address = "7861 Chevy Chase",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78732",
                    DOB = new DateTime(1959, 4, 13),
                    IsActive = true
                },
                Password = "emarkbark",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "mclarence@aool.com",
                    Email = "mclarence@aool.com",
                    PhoneNumber = "8174955201",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Clarence",
                    MiddleInitial = "A",
                    LastName = "Martin",
                    Address = "87 Alcedo St.",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77045",
                    DOB = new DateTime(1990, 1, 6),
                    IsActive = true
                },
                Password = "smartinmartin",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "smartinmartin.Martin@aool.com",
                    Email = "smartinmartin.Martin@aool.com",
                    PhoneNumber = "8178746718",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Gregory",
                    MiddleInitial = "R",
                    LastName = "Martinez",
                    Address = "8295 Sunset Blvd.",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77030",
                    DOB = new DateTime(1987, 10, 9),
                    IsActive = true
                },
                Password = "looter",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "cmiller@mapster.com",
                    Email = "cmiller@mapster.com",
                    PhoneNumber = "8177458615",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Charles",
                    MiddleInitial = "R",
                    LastName = "Miller",
                    Address = "8962 Main St.",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77031",
                    DOB = new DateTime(1984, 7, 21),
                    IsActive = true
                },
                Password = "chucky33",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "nelson.Kelly@aool.com",
                    Email = "nelson.Kelly@aool.com",
                    PhoneNumber = "5122926966",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Kelly",
                    MiddleInitial = "T",
                    LastName = "Nelson",
                    Address = "2601 Red River",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78703",
                    DOB = new DateTime(1956, 7, 4),
                    IsActive = true
                },
                Password = "orange",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "jojoe@ggmail.com",
                    Email = "jojoe@ggmail.com",
                    PhoneNumber = "2143125897",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Joe",
                    MiddleInitial = "C",
                    LastName = "Nguyen",
                    Address = "1249 4th SW St.",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75238",
                    DOB = new DateTime(1963, 1, 29),
                    IsActive = true
                },
                Password = "victorious",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "orielly@foxnets.com",
                    Email = "orielly@foxnets.com",
                    PhoneNumber = "2103450925",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Bill",
                    MiddleInitial = "T",
                    LastName = "O'Reilly",
                    Address = "8800 Gringo Drive",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78260",
                    DOB = new DateTime(1983, 1, 7),
                    IsActive = true
                },
                Password = "billyboy",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "or@aool.com",
                    Email = "or@aool.com",
                    PhoneNumber = "2142345566",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Anka",
                    MiddleInitial = "L",
                    LastName = "Radkovich",
                    Address = "1300 Elliott Pl",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75260",
                    DOB = new DateTime(1980, 3, 31),
                    IsActive = true
                },
                Password = "radicalone",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "megrhodes@freezing.co.uk",
                    Email = "megrhodes@freezing.co.uk",
                    PhoneNumber = "5123744746",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Megan",
                    MiddleInitial = "C",
                    LastName = "Rhodes",
                    Address = "4587 Enfield Rd.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78707",
                    DOB = new DateTime(1944, 8, 12),
                    IsActive = true
                },
                Password = "gohorns",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "erynrice@aool.com",
                    Email = "erynrice@aool.com",
                    PhoneNumber = "5123876657",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Eryn",
                    MiddleInitial = "M",
                    LastName = "Rice",
                    Address = "3405 Rio Grande",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78705",
                    DOB = new DateTime(1934, 8, 2),
                    IsActive = true
                },
                Password = "iloveme",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "jorge@hootmail.com",
                    Email = "jorge@hootmail.com",
                    PhoneNumber = "8178904374",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jorge",
                    LastName = "Rodriguez",
                    Address = "6788 Cotter Street",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77057",
                    DOB = new DateTime(1989, 8, 11),
                    IsActive = true
                },
                Password = "greedy",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "ra@aoo.com",
                    Email = "ra@aoo.com",
                    PhoneNumber = "5128752943",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Allen",
                    MiddleInitial = "B",
                    LastName = "Rogers",
                    Address = "4965 Oak Hill",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78732",
                    DOB = new DateTime(1967, 8, 27),
                    IsActive = true
                },
                Password = "familiar",
                RoleName = "Customer"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "st-jean@home.com",
                    Email = "st-jean@home.com",
                    PhoneNumber = "2104145678",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Olivier",
                    MiddleInitial = "M",
                    LastName = "Saint-Jean",
                    Address = "255 Toncray Dr.",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78292",
                    DOB = new DateTime(1950, 7, 8),
                    IsActive = true
                },
                Password = "historical",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "ss34@ggmail.com",
                    Email = "ss34@ggmail.com",
                    PhoneNumber = "5123497810",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Sarah",
                    MiddleInitial = "J",
                    LastName = "Saunders",
                    Address = "332 Avenue C",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78705",
                    DOB = new DateTime(1977, 10, 29),
                    IsActive = true
                },
                Password = "guiltless",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "willsheff@email.com",
                    Email = "willsheff@email.com",
                    PhoneNumber = "5124510084",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "William",
                    MiddleInitial = "T",
                    LastName = "Sewell",
                    Address = "2365 51st St.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78709",
                    DOB = new DateTime(1941, 4, 21),
                    IsActive = true
                },
                Password = "frequent",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "sheff44@ggmail.com",
                    Email = "sheff44@ggmail.com",
                    PhoneNumber = "5125479167",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Martin",
                    MiddleInitial = "J",
                    LastName = "Sheffield",
                    Address = "3886 Avenue A",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78705",
                    DOB = new DateTime(1937, 11, 10),
                    IsActive = true
                },
                Password = "history",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "johnsmith187@aool.com",
                    Email = "johnsmith187@aool.com",
                    PhoneNumber = "2108321888",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "John",
                    MiddleInitial = "A",
                    LastName = "Smith",
                    Address = "23 Hidden Forge Dr.",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78280",
                    DOB = new DateTime(1954, 10, 26),
                    IsActive = true
                },
                Password = "squirrel",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "dustroud@mail.com",
                    Email = "dustroud@mail.com",
                    PhoneNumber = "2142346667",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Dustin",
                    MiddleInitial = "P",
                    LastName = "Stroud",
                    Address = "1212 Rita Rd",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75221",
                    DOB = new DateTime(1932, 9, 1),
                    IsActive = true
                },
                Password = "snakes",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "ericstuart@aool.com",
                    Email = "ericstuart@aool.com",
                    PhoneNumber = "5128178335",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Eric",
                    MiddleInitial = "D",
                    LastName = "Stuart",
                    Address = "5576 Toro Ring",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78746",
                    DOB = new DateTime(1930, 12, 28),
                    IsActive = true
                },
                Password = "landus",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "peterstump@hootmail.com",
                    Email = "peterstump@hootmail.com",
                    PhoneNumber = "8174560903",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Peter",
                    MiddleInitial = "L",
                    LastName = "Stump",
                    Address = "1300 Kellen Circle",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77018",
                    DOB = new DateTime(1989, 8, 13),
                    IsActive = true
                },
                Password = "rhythm",
                RoleName = "Customer"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "tanner@ggmail.com",
                    Email = "tanner@ggmail.com",
                    PhoneNumber = "8174590929",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jeremy",
                    MiddleInitial = "S",
                    LastName = "Tanner",
                    Address = "4347 Almstead",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77044",
                    DOB = new DateTime(1982, 5, 21),
                    IsActive = true
                },
                Password = "kindly",
                RoleName = "Customer"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "taylordjay@aool.com",
                    Email = "taylordjay@aool.com",
                    PhoneNumber = "5124748452",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Allison",
                    MiddleInitial = "R",
                    LastName = "Taylor",
                    Address = "467 Nueces St.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78705",
                    DOB = new DateTime(1960, 1, 8),
                    IsActive = true
                },
                Password = "instrument",
                RoleName = "Customer"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "TayTaylor@aool.com",
                    Email = "TayTaylor@aool.com",
                    PhoneNumber = "5124512631",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Rachel",
                    MiddleInitial = "K",
                    LastName = "Taylor",
                    Address = "345 Longview Dr.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78705",
                    DOB = new DateTime(1975, 7, 27),
                    IsActive = true
                },
                Password = "arched",
                RoleName = "Customer"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "teefrank@hootmail.com",
                    Email = "teefrank@hootmail.com",
                    PhoneNumber = "8178765543",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Frank",
                    MiddleInitial = "J",
                    LastName = "Tee",
                    Address = "5590 Lavell Dr",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77004",
                    DOB = new DateTime(1968, 4, 6),
                    IsActive = true
                },
                Password = "median",
                RoleName = "Customer"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "tuck33@ggmail.com",
                    Email = "tuck33@ggmail.com",
                    PhoneNumber = "2148471154",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Clent",
                    MiddleInitial = "J",
                    LastName = "Tucker",
                    Address = "312 Main St.",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75315",
                    DOB = new DateTime(1978, 5, 19),
                    IsActive = true
                },
                Password = "approval",
                RoleName = "Customer"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "avelasco@yaho.com",
                    Email = "avelasco@yaho.com",
                    PhoneNumber = "2143985638",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Allen",
                    MiddleInitial = "G",
                    LastName = "Velasco",
                    Address = "679 W. 4th",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75207",
                    DOB = new DateTime(1963, 10, 6),
                    IsActive = true
                },
                Password = "decorate",
                RoleName = "Customer"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "westj@pioneer.net",
                    Email = "westj@pioneer.net",
                    PhoneNumber = "2148475244",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jake",
                    MiddleInitial = "T",
                    LastName = "West",
                    Address = "RR 3287",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75323",
                    DOB = new DateTime(1993, 10, 14),
                    IsActive = true
                },
                Password = "grover",
                RoleName = "Customer"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "louielouie@aool.com",
                    Email = "louielouie@aool.com",
                    PhoneNumber = "2145650098",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Louis",
                    MiddleInitial = "L",
                    LastName = "Winthorpe",
                    Address = "2500 Padre Blvd",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75220",
                    DOB = new DateTime(1952, 5, 31),
                    IsActive = true
                },
                Password = "sturdy",
                RoleName = "Customer"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "rwood@voyager.net",
                    Email = "rwood@voyager.net",
                    PhoneNumber = "5124545242",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Reagan",
                    MiddleInitial = "B",
                    LastName = "Wood",
                    Address = "447 Westlake Dr.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78746",
                    DOB = new DateTime(1992, 4, 24),
                    IsActive = true
                },
                Password = "decorous",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "dman@wdwebsolutions.com",
                    Email = "dman@wdwebsolutions.com",
                    PhoneNumber = "5556409287",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Derek",
                    LastName = "Dreibrodt",
                    Address = "423 Brentwood Dr",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78705",
                    DOB = new DateTime(2001, 1, 1),
                    IsActive = true
                },
                Password = "nasus123",
                RoleName = "Customer"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "jman@outlook.com",
                    Email = "jman@outlook.com",
                    PhoneNumber = "5558471234",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jacob",
                    LastName = "Foster",
                    Address = "700 Fancy St",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78705",
                    DOB = new DateTime(2000, 9, 1),
                    IsActive = true
                },
                Password = "pres4baseball",
                RoleName = "Customer"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "t.jacobs@longhornbank.neet",
                    Email = "t.jacobs@longhornbank.neet",
                    PhoneNumber = "8176593544",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Todd",
                    MiddleInitial = "L",
                    LastName = "Jacobs",
                    Address = "4564 Elm St",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77003",
                    IsActive = true
                },
                Password = "society",
                RoleName = "Employee"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "e.rice@longhornbank.neet",
                    Email = "e.rice@longhornbank.neet",
                    PhoneNumber = "2148475583",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Eryn",
                    MiddleInitial = "M",
                    LastName = "Rice",
                    Address = "3405 Rio Grande",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75261",
                    IsActive = true
                },
                Password = "ricearoni",
                RoleName = "Employee"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "b.ingram@longhornbank.neet",
                    Email = "b.ingram@longhornbank.neet",
                    PhoneNumber = "5126978613",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Brad",
                    MiddleInitial = "S",
                    LastName = "Ingram",
                    Address = "6548 La Posada Ct.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78705",
                    IsActive = true
                },
                Password = "ingram45",
                RoleName = "Employee"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "a.taylor@longhornbank.neet",
                    Email = "a.taylor@longhornbank.neet",
                    PhoneNumber = "2148965621",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Allison",
                    MiddleInitial = "R",
                    LastName = "Taylor",
                    Address = "467 Nueces St.",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75237",
                    IsActive = true
                },
                Password = "nostalgic",
                RoleName = "Admin"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "g.martinez@longhornbank.neet",
                    Email = "g.martinez@longhornbank.neet",
                    PhoneNumber = "2105788965",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Gregory",
                    MiddleInitial = "R",
                    LastName = "Martinez",
                    Address = "8295 Sunset Blvd.",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78239",
                    IsActive = true
                },
                Password = "fungus",
                RoleName = "Employee"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "m.sheffield@longhornbank.neet",
                    Email = "m.sheffield@longhornbank.neet",
                    PhoneNumber = "5124678821",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Martin",
                    MiddleInitial = "J",
                    LastName = "Sheffield",
                    Address = "3886 Avenue A",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78736",
                    IsActive = true
                },
                Password = "longhorns",
                RoleName = "Admin"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "j.macleod@longhornbank.neet",
                    Email = "j.macleod@longhornbank.neet",
                    PhoneNumber = "5124653365",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jennifer",
                    MiddleInitial = "D",
                    LastName = "Macleod",
                    Address = "2504 Far West Blvd.",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78731",
                    IsActive = true
                },
                Password = "smitty",
                RoleName = "Admin"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "j.tanner@longhornbank.neet",
                    Email = "j.tanner@longhornbank.neet",
                    PhoneNumber = "5129457399",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jeremy",
                    MiddleInitial = "S",
                    LastName = "Tanner",
                    Address = "4347 Almstead",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78761",
                    IsActive = true
                },
                Password = "tanman",
                RoleName = "Employee"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "m.rhodes@longhornbank.neet",
                    Email = "m.rhodes@longhornbank.neet",
                    PhoneNumber = "2102449976",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Megan",
                    MiddleInitial = "C",
                    LastName = "Rhodes",
                    Address = "4587 Enfield Rd.",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78293",
                    IsActive = true
                },
                Password = "countryrhodes",
                RoleName = "Admin"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "e.stuart@longhornbank.neet",
                    Email = "e.stuart@longhornbank.neet",
                    PhoneNumber = "2105344627",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Eric",
                    MiddleInitial = "F",
                    LastName = "Stuart",
                    Address = "5576 Toro Ring",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78279",
                    IsActive = true
                },
                Password = "stewboy",
                RoleName = "Admin"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "l.chung@longhornbank.neet",
                    Email = "l.chung@longhornbank.neet",
                    PhoneNumber = "2106983548",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Lisa",
                    MiddleInitial = "N",
                    LastName = "Chung",
                    Address = "234 RR 12",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78268",
                    IsActive = true
                },
                Password = "lisssa",
                RoleName = "Employee"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "l.swanson@longhornbank.neet",
                    Email = "l.swanson@longhornbank.neet",
                    PhoneNumber = "5124748138",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Leon",
                    LastName = "Swanson",
                    Address = "245 River Rd",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78731",
                    IsActive = true
                },
                Password = "swansong",
                RoleName = "Admin"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "w.loter@longhornbank.neet",
                    Email = "w.loter@longhornbank.neet",
                    PhoneNumber = "5124579845",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Wanda",
                    MiddleInitial = "K",
                    LastName = "Loter",
                    Address = "3453 RR 3235",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78732",
                    IsActive = true
                },
                Password = "lottery",
                RoleName = "Employee"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "j.white@longhornbank.neet",
                    Email = "j.white@longhornbank.neet",
                    PhoneNumber = "8174955201",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jason",
                    MiddleInitial = "M",
                    LastName = "White",
                    Address = "12 Valley View",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77045",
                    IsActive = true
                },
                Password = "evanescent",
                RoleName = "Admin"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "w.montgomery@longhornbank.neet",
                    Email = "w.montgomery@longhornbank.neet",
                    PhoneNumber = "8178746718",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Wilda",
                    MiddleInitial = "K",
                    LastName = "Montgomery",
                    Address = "210 Blanco Dr",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77030",
                    IsActive = true
                },
                Password = "monty3",
                RoleName = "Admin"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "h.morales@longhornbank.neet",
                    Email = "h.morales@longhornbank.neet",
                    PhoneNumber = "8177458615",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Hector",
                    MiddleInitial = "N",
                    LastName = "Morales",
                    Address = "4501 RR 140",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77031",
                    IsActive = true
                },
                Password = "hecktour",
                RoleName = "Employee"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "m.rankin@longhornbank.neet",
                    Email = "m.rankin@longhornbank.neet",
                    PhoneNumber = "5122926966",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Mary",
                    MiddleInitial = "T",
                    LastName = "Rankin",
                    Address = "340 Second St",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78703",
                    IsActive = true
                },
                Password = "rankmary",
                RoleName = "Employee"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "l.walker@longhornbank.neet",
                    Email = "l.walker@longhornbank.neet",
                    PhoneNumber = "2143125897",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Larry",
                    MiddleInitial = "G",
                    LastName = "Walker",
                    Address = "9 Bison Circle",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75238",
                    IsActive = true
                },
                Password = "walkamile",
                RoleName = "Admin"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "g.chang@longhornbank.neet",
                    Email = "g.chang@longhornbank.neet",
                    PhoneNumber = "2103450925",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "George",
                    MiddleInitial = "M",
                    LastName = "Chang",
                    Address = "9003 Joshua St",
                    City = "San Antonio",
                    State = "TX",
                    ZipCode = "78260",
                    IsActive = true
                },
                Password = "changalang",
                RoleName = "Admin"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "g.gonzalez@longhornbank.neet",
                    Email = "g.gonzalez@longhornbank.neet",
                    PhoneNumber = "2142345566",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Gwen",
                    MiddleInitial = "J",
                    LastName = "Gonzalez",
                    Address = "103 Manor Rd",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = "75260",
                    IsActive = true
                },
                Password = "offbeat",
                RoleName = "Employee"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "dman@longhornbank.neet",
                    Email = "dman@longhornbank.neet",
                    PhoneNumber = "5556409287",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Derek",
                    LastName = "Dreibrodt",
                    Address = "423 Brentwood Dr",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78705",
                    IsActive = true
                },
                Password = "nasus123",
                RoleName = "Admin"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "jman@longhornbank.neet",
                    Email = "jman@longhornbank.neet",
                    PhoneNumber = "5558471234",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jacob",
                    LastName = "Foster",
                    Address = "700 Fancy St",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78705",
                    IsActive = true
                },
                Password = "pres4baseball",
                RoleName = "Admin"
            });


            //create flag to help with errors
            String errorFlag = "Start";

            //create an identity result
            IdentityResult result = new IdentityResult();
            //call the method to seed the user
            try
            {
                foreach (AddUserModel aum in AllUsers)
                {
                    errorFlag = aum.User.Email;
                    result = await Utilities.AddUser.AddUserWithRoleAsync(aum, userManager, context);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem adding the user with email: "
                    + errorFlag, ex);
            }

            return result;
        }
    }
}
