using System;
using Microsoft.AspNetCore.Identity;

public class ApplicationRole : IdentityRole<long>
{
    public ApplicationRole() : base()
    {
    }
    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}
