using System.ComponentModel;

namespace FoodCorp.DataAccess.Enums;

public enum PaymentMethod
{
    Cash = 1,

    [Description("Credit Card")]
    CreditCard = 2,

    [Description("Crypto Currency")]
    CryptoCurrency = 3,

    [Description("Electronic Wallet")]
    ElectronicWallet = 4,

    [Description("Debit Card")]
    DebitCard = 5,
}