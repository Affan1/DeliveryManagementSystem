using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Users_API.Model;
using Users_API.Email;
using Users_API.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailSender _emailSender;

    public AccountController(UserManager<User> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] userInformationDTO userdto)
    {
        

        var user = new User
        {
            UserName = userdto.Username,
            Email = userdto.Email,
            PhoneNumber = userdto.Phone,
            PasswordHash = userdto.Password
        };

        var result = await _userManager.CreateAsync(user, user.PasswordHash);

        if (result.Succeeded)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var verificationUrl = Url.Action("VerifyEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Verify your email",
                $"Please verify your email by clicking this link: <a href='{verificationUrl}'>Verify Email</a>");

            return Ok("User registered successfully. Please check your email to confirm your account.");
        }

        return BadRequest(result.Errors.Select(e => e.Description));
    }

    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string userId, [FromQuery] string token)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            return BadRequest("User ID and token are required.");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
        {
            return Ok("Email verified successfully.");
        }

        return BadRequest(result.Errors.Select(e => e.Description));
    }
}
