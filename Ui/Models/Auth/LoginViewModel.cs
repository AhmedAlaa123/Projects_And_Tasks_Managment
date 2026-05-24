using System.ComponentModel.DataAnnotations;

namespace Ui.Models.Auth;

public class LoginViewModel
{
    [Required]
 
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
