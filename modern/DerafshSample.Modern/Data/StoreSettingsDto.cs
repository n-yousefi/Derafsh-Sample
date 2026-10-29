using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Derafsh;

namespace DerafshSample.Modern.Data;

[Table("StoreSettings")]
public sealed class StoreSettingsDto
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; } = 1;

    [Required, StringLength(120)]
    [Display(Name = "Store name")]
    public string StoreName { get; set; } = "Derafsh Demo Store";

    [Required, StringLength(3, MinimumLength = 3)]
    [Display(Name = "Currency code")]
    public string Currency { get; set; } = "EUR";

    [Required, StringLength(80)]
    [Display(Name = "Time zone")]
    public string TimeZone { get; set; } = "Europe/Amsterdam";

    [ChildCollection(nameof(StoreContactDto.StoreSettingsId))]
    public List<StoreContactDto> Contacts { get; set; } = [];

    [ChildCollection(nameof(ShippingZoneDto.StoreSettingsId))]
    public List<ShippingZoneDto> ShippingZones { get; set; } = [];

    [ChildCollection(nameof(NotificationRecipientDto.StoreSettingsId))]
    public List<NotificationRecipientDto> NotificationRecipients { get; set; } = [];
}

[Table("StoreContact")]
public sealed class StoreContactDto
{
    [Key] public int Id { get; set; }
    public int StoreSettingsId { get; set; }

    [Required, StringLength(20)]
    [Display(Name = "Contact type")]
    public string Type { get; set; } = "Email";

    [Required, StringLength(160)]
    [Display(Name = "Contact value")]
    public string Value { get; set; } = string.Empty;
}

[Table("ShippingZone")]
public sealed class ShippingZoneDto
{
    [Key] public int Id { get; set; }
    public int StoreSettingsId { get; set; }

    [Required, StringLength(2, MinimumLength = 2)]
    [Display(Name = "Country code")]
    public string CountryCode { get; set; } = string.Empty;

    [Range(0, 100000)]
    [Display(Name = "Delivery fee")]
    public decimal Fee { get; set; }
}

[Table("NotificationRecipient")]
public sealed class NotificationRecipientDto
{
    [Key] public int Id { get; set; }
    public int StoreSettingsId { get; set; }

    [Required, EmailAddress, StringLength(200)]
    [Display(Name = "Email address")]
    public string Email { get; set; } = string.Empty;
}
