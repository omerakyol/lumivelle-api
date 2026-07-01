using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete;

public class DeviceInformation
{
    [StringLength(50)] public string DeviceName { get; set; }
    [StringLength(50)] public string DeviceModelName { get; set; }
    [StringLength(50)] public string DeviceBrand { get; set; }
    [StringLength(50)] public string DeviceManufacturer { get; set; }
    [StringLength(20)] public string DeviceType { get; set; } 
    [StringLength(20)] public string OsName { get; set; }
    [StringLength(10)] public string OsVersion { get; set; } 
    [StringLength(10)] public string AppVersion { get; set; }
    [StringLength(10)] public string AppBuildNumber { get; set; }
}