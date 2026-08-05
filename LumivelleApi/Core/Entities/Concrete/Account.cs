using System;
using System.ComponentModel.DataAnnotations;
using Core.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities.Concrete;

public class Account : DocumentDbEntity
{
    public AccountStatus AccountStatus { get; set; }
    public AccountType AccountType { get; set; }
    [BsonRequired] [EmailAddress] public string Email { get; set; }

    // Null for accounts created via Apple/Google sign-in that have never set a password.
    public string Password { get; set; }

    // Stable per-provider subject id ("sub" claim), null unless that provider has been linked.
    public string GoogleUserId { get; set; }
    public string AppleUserId { get; set; }

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

    public string PasswordResetCode { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? PasswordResetCodeExpiresAt { get; set; }

    public string DisplayName { get; set; }
    public string Bio { get; set; }
    public bool IsVerified { get; set; } = false;
    public bool IsCreator { get; set; } = false;

    public SubscriptionTier SubscriptionTier { get; set; } = SubscriptionTier.Free;
    public string SubscriptionPlatform { get; set; } // "apple" | "google", null if never subscribed
    public string SubscriptionProductId { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? SubscriptionExpiresAt { get; set; }

    public bool SubscriptionAutoRenewing { get; set; }
}