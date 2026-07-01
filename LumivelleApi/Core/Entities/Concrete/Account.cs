using System;
using System.ComponentModel.DataAnnotations;
using Core.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities.Concrete;

public class Account : DocumentDbEntity
{
    public AccountStatus AccountStatus { get; set; }
    public AccountType AccountType { get; set; }
    [BsonRequired] public string Username { get; set; }
    [BsonRequired] public string Password { get; set; }
    [EmailAddress] public string Email { get; set; }
    [Phone] public string Phone { get; set; }
    public string FirebaseToken { get; set; }
    [StringLength(2)] public string Language { get; set; } = "en";
    public bool EnablePushNotifications { get; set; } = true;
    public string PhotoUrl { get; set; }
    public DeviceInformation DeviceInformation { get; set; }
    public bool TwoFactorEnabled { get; set; } = false;
    public string TwoFactorSecretKey { get; set; } // Authenticator base32 secret

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? Last2FaVerifiedAt { get; set; } // Optional, for information
}