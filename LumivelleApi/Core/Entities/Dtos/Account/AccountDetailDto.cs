using System;
using System.Text.Json.Serialization;
using Core.Enums;
using MongoDB.Bson;

namespace Core.Entities.Dtos.Account;

public class AccountDetailDto
{
    [JsonIgnore] public ObjectId Id { get; set; }
    public string AccountId => Id.ToString();

    public AccountStatus AccountStatus { get; set; }
    public AccountType AccountType { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Language { get; set; }
    public bool EnablePushNotifications { get; set; }
    public string PhotoUrl { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? Last2FaVerifiedAt { get; set; }
}