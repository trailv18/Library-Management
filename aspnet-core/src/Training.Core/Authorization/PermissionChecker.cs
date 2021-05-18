using Abp.Authorization;
using Training.Authorization.Roles;
using Training.Authorization.Users;

namespace Training.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
