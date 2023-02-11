using System.ComponentModel;

namespace FoodCorp.DataAccess.Enums;

public enum Role
{
    [Description("Legal Entity")]
    LegalEntity = 1,

    [Description("Natural Person")]
    NaturalPerson = 2,

    [Description("Individual Enterpreneur")]
    IndividualEntrepreneur = 3,

    Moderator = 4,
    Admin = 5
}