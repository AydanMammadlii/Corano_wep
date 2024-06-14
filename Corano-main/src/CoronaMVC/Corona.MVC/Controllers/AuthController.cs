using Corona.MVC.ExamProgramUIExceptions;
using Corona.MVC.Service.Interfaces;
using Corona.MVC.ViewModel.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Corona.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IApiService _apiService;

        public AuthController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AuthViewModel model)
        {
            try
            {
                var response = await _apiService.Register(model.RegisterViewModel);
                var newLoginViewaModel = new LoginViewModel()
                {
                    UsernameOrEmail = response.UserEmail,
                    password = response.Password
                };
                var newAuthViewModel = new AuthViewModel();
                newAuthViewModel.LoginViewModel = newLoginViewaModel;
                await Login(newAuthViewModel);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    foreach (var key in ex.ModelErrors.Keys)
                    {
                        ModelState.AddModelError(key, ex.ModelErrors[key]);
                    }
                    return View(model);
                }
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Login(AuthViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var authToken = await _apiService.Login(model.LoginViewModel);

                    Response.Cookies.Append("token", authToken.token, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddYears(1),
                        HttpOnly = true
                    });

                    Response.Cookies.Append("appUserId", authToken.appuserid, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddYears(1),
                        HttpOnly = true
                    });

                    Response.Cookies.Append("basketId", authToken.basketId.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddYears(1),
                        HttpOnly = true
                    });

                    Response.Cookies.Append("username", authToken.username, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddYears(1),
                        HttpOnly = true
                    });

                    Response.Cookies.Append("email", authToken.email, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddYears(1),
                        HttpOnly = true
                    });

                    Response.Cookies.Append("UserRole", authToken.role, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddYears(1),
                        HttpOnly = true
                    });

                    return RedirectToAction("Index", "Home");
                }
                catch (ApiException ex)
                {
                    if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        foreach (var key in ex.ModelErrors.Keys)
                        {
                            ModelState.AddModelError(key, ex.ModelErrors[key]);
                        }
                        return View(model);
                    }
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                Response.Cookies.Delete("token");
                Response.Cookies.Delete("appUserId");
                Response.Cookies.Delete("basketId");
                Response.Cookies.Delete("username");
                Response.Cookies.Delete("email");
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Auth");
            }

            return RedirectToAction("Login", "Auth");
        }
    }
}
