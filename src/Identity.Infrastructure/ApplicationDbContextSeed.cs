//using Identity.Domain.Models;
//using LinFx.Utils;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.IO.Compression;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace Identity.Web.Models
//{
//    public class ApplicationDbContextSeed
//    {
//        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

//        public async Task SeedAsync(ApplicationDbContext context,IHostingEnvironment env,
//            ILogger<ApplicationDbContextSeed> logger, IOptions<AppSettings> settings,int? retry = 0)
//        {
//            int retryForAvaiability = retry.Value;

//            try
//            {
//                var useCustomizationData = settings.Value.UseCustomizationData;
//                var contentRootPath = env.ContentRootPath;
//                var webroot = env.WebRootPath;

//                if (!context.Users.Any())
//                {
//                    context.Users.AddRange(useCustomizationData
//                        ? GetUsersFromFile(contentRootPath, logger)
//                        : GetDefaultUser());

//                    await context.SaveChangesAsync();
//                }

//                if (!context.Roles.Any())
//                {
//                    context.Roles.AddRange(GetDefaultRole());

//                    await context.SaveChangesAsync();
//                }

//                if (useCustomizationData)
//                {
//                    GetPreconfiguredImages(contentRootPath, webroot, logger);
//                }
//            }
//            catch (Exception ex)
//            {
//                if (retryForAvaiability < 10)
//                {
//                    retryForAvaiability++;
                    
//                    logger.LogError(ex.Message,$"There is an error migrating data for ApplicationDbContext");

//                    await SeedAsync(context,env,logger,settings, retryForAvaiability);
//                }
//            }
//        }

//        private IEnumerable<ApplicationUser> GetUsersFromFile(string contentRootPath, ILogger logger)
//        {
//            string csvFileUsers = Path.Combine(contentRootPath, "Setup", "Users.csv");

//            if (!File.Exists(csvFileUsers))
//            {
//                return GetDefaultUser();
//            }

//            string[] csvheaders;
//            try
//            {
//                string[] requiredHeaders = {
//                    "cardholdername", "cardnumber", "cardtype", "city", "country",
//                    "email", "expiration", "lastname", "name", "phonenumber",
//                    "username", "zipcode", "state", "street", "securitynumber",
//                    "normalizedemail", "normalizedusername", "password"
//                };
//                csvheaders = GetHeaders(requiredHeaders, csvFileUsers);
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex.Message);

//                return GetDefaultUser();
//            }

//            List<ApplicationUser> users = File.ReadAllLines(csvFileUsers)
//                        .Skip(1) // skip header column
//                        .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)") )
//                        .SelectTry(column => CreateApplicationUser(column, csvheaders))
//                        .OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
//                        .Where(x => x != null)
//                        .ToList();

//            return users;
//        }

//        private ApplicationUser CreateApplicationUser(string[] column, string[] headers)
//        {
//            if (column.Count() != headers.Count())
//            {
//                throw new Exception($"column count '{column.Count()}' not the same as headers count'{headers.Count()}'");
//            }

//            string cardtypeString = column[Array.IndexOf(headers, "cardtype")].Trim('"').Trim();
//            if (!int.TryParse(cardtypeString, out int cardtype))
//            {
//                throw new Exception($"cardtype='{cardtypeString}' is not a number");
//            }

//            var user = new ApplicationUser
//            {
//                //    CardHolderName = column[Array.IndexOf(headers, "cardholdername")].Trim('"').Trim(),
//                //    CardNumber = column[Array.IndexOf(headers, "cardnumber")].Trim('"').Trim(),
//                //    CardType = cardtype,
//                //    City = column[Array.IndexOf(headers, "city")].Trim('"').Trim(),
//                //    Country = column[Array.IndexOf(headers, "country")].Trim('"').Trim(),
//                Email = column[Array.IndexOf(headers, "email")].Trim('"').Trim(),
//                //Expiration = column[Array.IndexOf(headers, "expiration")].Trim('"').Trim(),
//                Id = Guid.NewGuid().ToString(),
//                //LastName = column[Array.IndexOf(headers, "lastname")].Trim('"').Trim(),
//                //Name = column[Array.IndexOf(headers, "name")].Trim('"').Trim(),
//                PhoneNumber = column[Array.IndexOf(headers, "phonenumber")].Trim('"').Trim(),
//                UserName = column[Array.IndexOf(headers, "username")].Trim('"').Trim(),
//                //ZipCode = column[Array.IndexOf(headers, "zipcode")].Trim('"').Trim(),
//                //State = column[Array.IndexOf(headers, "state")].Trim('"').Trim(),
//                //Street = column[Array.IndexOf(headers, "street")].Trim('"').Trim(),
//                //SecurityNumber = column[Array.IndexOf(headers, "securitynumber")].Trim('"').Trim(),
//                NormalizedEmail = column[Array.IndexOf(headers, "normalizedemail")].Trim('"').Trim(),
//                NormalizedUserName = column[Array.IndexOf(headers, "normalizedusername")].Trim('"').Trim(),
//                SecurityStamp = Guid.NewGuid().ToString("D"),
//                PasswordHash = column[Array.IndexOf(headers, "password")].Trim('"').Trim(), // Note: This is the password
//            };

//            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

//            return user;
//        }

//        private IEnumerable<IdentityRole> GetDefaultRole()
//        {
//            return new List<IdentityRole>
//            {
//                new IdentityRole("admin"),
//                new IdentityRole("developer_park")
//            };
//        }

//        private IEnumerable<ApplicationUser> GetDefaultUser()
//        {
//            var user1 = new ApplicationUser
//            {
//                Id = IDUtils.CreateNewId().ToString(),
//                UserName = "admin",
//                Email = "admin@microsoft.com",
//                PhoneNumber = "1234567890",
//                NormalizedEmail = "DEMOUSER@MICROSOFT.COM",
//                NormalizedUserName = "DEMOUSER@MICROSOFT.COM",
//                SecurityStamp = Guid.NewGuid().ToString("D"),
//            };
//            user1.PasswordHash = _passwordHasher.HashPassword(user1, "123456");


//            var user2 = new ApplicationUser
//            {
//                Id = IDUtils.CreateNewId().ToString(),
//                UserName = "f101@qq.com",
//                Email = "f101@qq.com",
//                PhoneNumber = "1234567890",
//                NormalizedEmail = "f101@qq.com",
//                NormalizedUserName = "f101@qq.com",
//                SecurityStamp = Guid.NewGuid().ToString("D"),
//            };
//            user2.PasswordHash = _passwordHasher.HashPassword(user1, "123456");

//            return new List<ApplicationUser>()
//            {
//                user1,
//                user2
//            };
//        }

//        static string[] GetHeaders(string[] requiredHeaders, string csvfile)
//        {
//            string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

//            if (csvheaders.Count() != requiredHeaders.Count())
//            {
//                throw new Exception($"requiredHeader count '{ requiredHeaders.Count()}' is different then read header '{csvheaders.Count()}'");
//            }

//            foreach (var requiredHeader in requiredHeaders)
//            {
//                if (!csvheaders.Contains(requiredHeader))
//                {
//                    throw new Exception($"does not contain required header '{requiredHeader}'");
//                }
//            }

//            return csvheaders;
//        }

//        static void GetPreconfiguredImages(string contentRootPath, string webroot, ILogger logger)
//        {
//            try
//            {
//                string imagesZipFile = Path.Combine(contentRootPath, "Setup", "images.zip");
//                if (!File.Exists(imagesZipFile))
//                {
//                    logger.LogError($" zip file '{imagesZipFile}' does not exists.");
//                    return;
//                }

//                string imagePath = Path.Combine(webroot, "images");
//                string[] imageFiles = Directory.GetFiles(imagePath).Select(file => Path.GetFileName(file)).ToArray();

//                using (ZipArchive zip = ZipFile.Open(imagesZipFile, ZipArchiveMode.Read))
//                {
//                    foreach (ZipArchiveEntry entry in zip.Entries)
//                    {
//                        if (imageFiles.Contains(entry.Name))
//                        {
//                            string destinationFilename = Path.Combine(imagePath, entry.Name);
//                            if (File.Exists(destinationFilename))
//                            {
//                                File.Delete(destinationFilename);
//                            }
//                            entry.ExtractToFile(destinationFilename);
//                        }
//                        else
//                        {
//                            logger.LogWarning($"Skip file '{entry.Name}' in zipfile '{imagesZipFile}'");
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError($"Exception in method GetPreconfiguredImages WebMVC. Exception Message={ex.Message}");
//            }
//        }
//    }
//}
