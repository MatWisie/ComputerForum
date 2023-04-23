using ComputerForum.Interfaces;
using System.Security.Claims;

namespace ComputerForum.Validations
{
    public class RoleValidation : IRoleValidation
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoleValidation(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool CheckIfAdmin()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != "Admin")
            {
                return false;
            }
            return true;
        }
    }
}
