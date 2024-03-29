﻿using Microsoft.AspNetCore.Identity;

namespace FoodCorp.DataAccess.Entities;

public class User : IdentityUser<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime RegistrationDateTimeUtc { get; set; }
    public string ProfileImagePath { get; set; }
    public Enums.Role Role { get; set; }

    public ICollection<UserShowcaseImage> ShowcaseImages { get; set; }
    public Customer Customer { get; set; }
    public Performer Performer { get; set; }
    public UserPermissions Permissions { get; set; }
}