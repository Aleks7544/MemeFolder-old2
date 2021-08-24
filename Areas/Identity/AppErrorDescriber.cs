namespace MemeFolder.Areas.Identity
{
    using Microsoft.AspNetCore.Identity;

    public class AppErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            var error = base.DuplicateUserName(userName);
            error.Description = "This username is already taken.";
            return error;
        }
    }
}
