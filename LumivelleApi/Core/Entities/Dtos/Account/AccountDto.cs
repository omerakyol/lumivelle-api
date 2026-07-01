using System;
using System.Text.Json.Serialization;
using Core.Enums;
using MongoDB.Bson;

namespace Core.Entities.Dtos.Account;

public class AccountDto
{
    [JsonIgnore] public ObjectId Id { get; set; }
    public string AccountId => Id.ToString(); 
    public AccountStatus AccountStatus { get; set; }
    public AccountType AccountType { get; set; }
    public string Username { get; set; } 
    public string Email { get; set; }
    public string Phone { get; set; } 
    public string Language { get; set; }
    public bool EnablePushNotifications { get; set; }
    public string PhotoUrl { get; set; }
    public bool TwoFactorEnabled { get; set; } 
}