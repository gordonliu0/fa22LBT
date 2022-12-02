using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using fa22LBT.DAL;
using fa22LBT.Models;
using fa22LBT.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace fa22LBT.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly PasswordValidator<AppUser> _passwordValidator;
        private readonly AppDbContext _context;

        public AccountController(AppDbContext appDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _context = appDbContext;
            _userManager = userManager;
            _signInManager = signIn;
            //user manager only has one password validator
            _passwordValidator = (PasswordValidator<AppUser>)userManager.PasswordValidators.FirstOrDefault();
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RegisterEmployee()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterEmployee(RegisterViewModel rvm)
        {
            //if registration data is valid, create a new user on the database
            if (ModelState.IsValid == false)
            {
                //this is the sad path - something went wrong, 
                //return the user to the register page to try again
                return View(rvm);
            }

            //this code maps the RegisterViewModel to the AppUser domain model
            AppUser newUser = new AppUser
            {
                UserName = rvm.Email,
                Email = rvm.Email,
                PhoneNumber = rvm.PhoneNumber,

                //TODO: Add the rest of the custom user fields here
                //FirstName is included as an example
                FirstName = rvm.FirstName,
                MiddleInitial = rvm.MiddleInitial,
                LastName = rvm.LastName,
                Address = rvm.Address,
                City = rvm.City,
                State = rvm.State,
                ZipCode = rvm.ZipCode,
                DOB = rvm.DOB,
                IsActive = true
            };

            //create AddUserModel
            AddUserModel aum = new AddUserModel()
            {
                User = newUser,
                Password = rvm.Password,

                //TODO: You will need to change this value if you want to 
                //add the user to a different role - just specify the role name.
                RoleName = "Employee"
            };

            //This code uses the AddUser utility to create a new user with the specified password
            IdentityResult result = await Utilities.AddUser.AddUserWithRoleAsync(aum, _userManager, _context);

            if (result.Succeeded) //everything is okay
            {
                //NOTE: This code logs the user into the account that they just created
                //You may or may not want to log a user in directly after they register - check
                //the business rules!
                //Microsoft.AspNetCore.Identity.SignInResult result2 = await _signInManager.PasswordSignInAsync(rvm.Email, rvm.Password, false, lockoutOnFailure: false);

                //Send the user to the home page
                return RedirectToAction("Index", "Account", new {id = rvm.Email} );
            }
            else  //the add user operation didn't work, and we need to show an error message
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                //send user back to page with errors
                return View(rvm);
            }
        }

        [Authorize(Roles = "Employee, Admin")]
        public async Task<IActionResult> AllCustomers()
        {
            //this is a list of all the users who ARE in this role (members)
            List<AppUser> RoleMembers = new List<AppUser>();

            //loop through ALL the users and decide if they are in the role(member) or not (non-member)
            //every user will be evaluated for every role, so this is a SLOW chunk of code because
            //it accesses the database so many times
            foreach (AppUser user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, "Customer") == true) //user is in the role
                {
                    //add user to list of members
                    RoleMembers.Add(user);
                }
            }

            return View(RoleMembers);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllEmployees()
        {
            //this is a list of all the users who ARE in this role (members)
            List<AppUser> RoleMembers = new List<AppUser>();

            //loop through ALL the users and decide if they are in the role(member) or not (non-member)
            //every user will be evaluated for every role, so this is a SLOW chunk of code because
            //it accesses the database so many times
            foreach (AppUser user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, "Employee") == true) //user is in the role
                {
                    //add user to list of members
                    RoleMembers.Add(user);
                }
            }

            return View(RoleMembers);
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel rvm)
        {
            //if registration data is valid, create a new user on the database
            if (ModelState.IsValid == false)
            {
                //this is the sad path - something went wrong, 
                //return the user to the register page to try again
                return View(rvm);
            }

            //this code maps the RegisterViewModel to the AppUser domain model
            AppUser newUser = new AppUser
            {
                UserName = rvm.Email,
                Email = rvm.Email,
                PhoneNumber = rvm.PhoneNumber,

                //TODO: Add the rest of the custom user fields here
                //FirstName is included as an example
                FirstName = rvm.FirstName,
                MiddleInitial = rvm.MiddleInitial,
                LastName = rvm.LastName,
                Address = rvm.Address,
                City = rvm.City,
                State = rvm.State,
                ZipCode = rvm.ZipCode,
                DOB = rvm.DOB,
                IsActive = true
            };

            //create AddUserModel
            AddUserModel aum = new AddUserModel()
            {
                User = newUser,
                Password = rvm.Password,

                //TODO: You will need to change this value if you want to 
                //add the user to a different role - just specify the role name.
                RoleName = "Customer"
            };

            //This code uses the AddUser utility to create a new user with the specified password
            IdentityResult result = await Utilities.AddUser.AddUserWithRoleAsync(aum, _userManager, _context);

            if (result.Succeeded) //everything is okay
            { 
                //NOTE: This code logs the user into the account that they just created
                //You may or may not want to log a user in directly after they register - check
                //the business rules!
                Microsoft.AspNetCore.Identity.SignInResult result2 = await _signInManager.PasswordSignInAsync(rvm.Email, rvm.Password, false, lockoutOnFailure: false);

                //Send the user to the home page
                return RedirectToAction("Index", "Home");
            }
            else  //the add user operation didn't work, and we need to show an error message
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                //send user back to page with errors
                return View(rvm);
            }
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) //user has been redirected here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            _signInManager.SignOutAsync(); //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl; //pass along the page the user should go back to
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel lvm, string returnUrl)
        {
            // Check if employee is allowed
            AppUser userLoggedIn = await _userManager.FindByNameAsync(lvm.Email);
            if (userLoggedIn.IsActive == false && await _userManager.IsInRoleAsync(userLoggedIn, "Employee"))
            {
                return View("Error", new string[] { "Please contact an admin. Your employee account has been deactivated." });
            }

            //attempt to sign the user in using the SignInManager
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, lvm.RememberMe, lockoutOnFailure: false);

            //if the login worked, take the user to either the url
            //they requested OR the homepage if there isn't a specific url
            if (result.Succeeded)
            {
                //return ?? "/" means if returnUrl is null, substitute "/" (home)
                return Redirect(returnUrl ?? "/");
            }
            else //log in was not successful
            {
                //add an error to the model to show invalid attempt
                ModelState.AddModelError("", "Invalid login attempt.");
                //send user back to login page to try again
                return View(lvm);
            }
        }

        public IActionResult AccessDenied()
        {
            return View("Error", new string[] { "You are not authorized for this resource" });
        }

        //GET: Account/Index
        public IActionResult Index(string? id)
        {
            IndexViewModel ivm = new IndexViewModel();
            if (id == null)
            {
                id = User.Identity.Name;
            } else
            {
                if (!(User.IsInRole("Admin") || User.IsInRole("Employee")) && id != User.Identity.Name)
                {
                    return View("Error", new string[] { "You are not authorized to view this account detail." });
                }
            }

            AppUser user = _context.Users.FirstOrDefault(u => u.UserName == id);

            if (user == null)
            {
                return View("Error", new string[] { "This account was not found." });
            }

            //populate the view model
            //(i.e. map the domain model to the view model)
            ivm.Email = user.Email;
            ivm.HasPassword = true;
            ivm.UserID = user.Id;
            ivm.UserName = user.UserName;
            ivm.PhoneNumber = user.PhoneNumber;
            ivm.FullName = user.FullName;
            ivm.FullAddress = user.FullAddress;
            ivm.Age = user.Age;
            ivm.IsActive = user.IsActive;

            //send data to the view
            return View(ivm);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var UserAccount = await _userManager.FindByEmailAsync(id);

            if (User.Identity.IsAuthenticated)
            {
                AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
                if (userLoggedIn.IsActive == false)
                {
                    return View("Locked");
                }
            }

            // If CurrentUser is a customer and attempts to change anybody else's account, not allowed
            if (!User.IsInRole("Admin") && !User.IsInRole("Employee") && User.Identity.Name != UserAccount.UserName)
            {
                return View("Error", new string[] { "You are not authorized to change this account!" });
            }

            // IF currentuser is an employee and attempts to change another employee, not allowed
            if (await _userManager.IsInRoleAsync(UserAccount, "Employee") && User.IsInRole("Employee") && User.Identity.Name != UserAccount.UserName)
            {
                return View("Error", new string[] { "You are not authorized to change this account!" });
            }

            // IF currentuser an employee and attempts to change an admin, not allowed
            if (await _userManager.IsInRoleAsync(UserAccount, "Admin") && User.IsInRole("Employee") && User.Identity.Name != UserAccount.UserName)
            {
                return View("Error", new string[] { "You are not authorized to change this account!" });
            }

            var euvm = new EditUserViewModel();
            euvm.Email = id;
            euvm.FirstName = UserAccount.FirstName;
            euvm.MiddleInitial = UserAccount.MiddleInitial;
            euvm.LastName = UserAccount.LastName;
            euvm.Address = UserAccount.Address;
            euvm.City = UserAccount.City;
            euvm.State = UserAccount.State;
            euvm.ZipCode = UserAccount.ZipCode;
            euvm.PhoneNumber = UserAccount.PhoneNumber;

            if (euvm == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Employee") && User.Identity.Name == UserAccount.UserName)
            {
                return View("EditEmployeeSelf", euvm);
            }

            return View(euvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel euvm, string email)
        {
            //if user forgot a field, send them back to 
            //change password page to try again
            if (ModelState.IsValid == false)
            {
                return View(euvm);
            }

            if (email != euvm.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AppUser userLoggedIn = await _userManager.FindByEmailAsync(email);
                    userLoggedIn.FirstName = euvm.FirstName;
                    userLoggedIn.MiddleInitial = euvm.MiddleInitial;
                    userLoggedIn.LastName = euvm.LastName;
                    userLoggedIn.Address = euvm.Address;
                    userLoggedIn.City = euvm.City;
                    userLoggedIn.State = euvm.State;
                    userLoggedIn.ZipCode = euvm.ZipCode;
                    userLoggedIn.PhoneNumber = euvm.PhoneNumber;
                    _context.Update(userLoggedIn);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return View("Error");
                }
                return RedirectToAction("Index", new { id = email });
            }
            return View("Index");
        }

        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult> Toggle(string id)
        {
            AppUser userLoggedIn = await _userManager.FindByNameAsync(id);
            userLoggedIn.IsActive = !userLoggedIn.IsActive;
            _context.Update(userLoggedIn);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Account", new { id = id } );
        }

        //Logic for change password
        // GET: /Account/ChangePassword
        public async Task<ActionResult> ChangePassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
                if (userLoggedIn.IsActive == false)
                {
                    return View("Locked");
                }
            }

            return View();
        }

        public ActionResult ChangePasswordEmployee(string id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel cpvm)
        {
            //if user forgot a field, send them back to 
            //change password page to try again
            if (ModelState.IsValid == false)
            {
                return View(cpvm);
            }

            //Find the logged in user using the UserManager
            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);

            //Attempt to change the password using the UserManager
            var result = await _userManager.ChangePasswordAsync(userLoggedIn, cpvm.OldPassword, cpvm.NewPassword);

            //if the attempt to change the password worked
            if (result.Succeeded)
            {
                //sign in the user with the new password
                await _signInManager.SignInAsync(userLoggedIn, isPersistent: false);

                // TODO: emails that the password have changed
                String emailsubject = "Password Changed";
                String emailbody = "Your password to Longhorn Bank has been changed.";
                Utilities.EmailMessaging.SendEmail(userLoggedIn.Email, emailsubject, emailbody);

                //send the user back to the home page
                return RedirectToAction("Index", "Home");
            }
            else //attempt to change the password didn't work
            {
                //Add all the errors from the result to the model state
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                //send the user back to the change password page to try again
                return View(cpvm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePasswordEmployee(ChangePasswordViewModel cpvm, string id)
        {
            //if user forgot a field, send them back to 
            //change password page to try again

            //Find the logged in user using the UserManager
            AppUser userLoggedIn = await _userManager.FindByNameAsync(id);

            //Attempt to change the password using the UserManager
            var token = await _userManager.GeneratePasswordResetTokenAsync(userLoggedIn);

            var result = await _userManager.ResetPasswordAsync(userLoggedIn, token, cpvm.NewPassword);


            //if the attempt to change the password worked
            if (result.Succeeded)
            {
                //send the user back to the home page
                return RedirectToAction("Index", "Home");
            }
            else //attempt to change the password didn't work
            {
                //Add all the errors from the result to the model state
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                //send the user back to the change password page to try again
                return View(cpvm);
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOff()
        {
            //sign the user out of the application
            _signInManager.SignOutAsync();

            //send the user back to the home page
            return RedirectToAction("Index", "Home");
        }           
    }
}