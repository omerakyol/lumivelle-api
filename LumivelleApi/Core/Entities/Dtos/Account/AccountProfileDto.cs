using System.Text.Json.Serialization;
using Core.Helpers;
using MongoDB.Bson;

namespace Core.Entities.Dtos.Account;

public class AccountProfileDto
{
    [JsonIgnore] public ObjectId Id { get; set; }
    public string AccountId => Id.ToString();

    public string Username { get; set; }
    public string ProfileQrCodeLink => GenerateQrCodeHelper.Generate(Username);

    public string Email { get; set; }
    public string Phone { get; set; }
    public string Language { get; set; }
    public bool EnablePushNotifications { get; set; }
    public string PhotoUrl { get; set; }
    public bool TwoFactorEnabled { get; set; }
}