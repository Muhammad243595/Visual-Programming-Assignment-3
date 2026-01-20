using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechNovaLeave.Data;


public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public AccountController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        ApplicationDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        // 🔒 Check employee active status
        var employee = _context.Employees
            .FirstOrDefault(e => e.Email == model.Email);

        if (employee != null && employee.IsActive == false)
        {
            ModelState.AddModelError("", "Your account is deactivated. Contact admin.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName,
            model.Password,
            model.RememberMe,
            false);

        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        ModelState.AddModelError("", "Invalid login attempt");
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
