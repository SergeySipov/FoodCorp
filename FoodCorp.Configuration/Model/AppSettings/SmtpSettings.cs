﻿namespace FoodCorp.Configuration.Model.AppSettings;

public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string SenderMail { get; set; }
    public string SenderMailPassword { get; set; }
}
