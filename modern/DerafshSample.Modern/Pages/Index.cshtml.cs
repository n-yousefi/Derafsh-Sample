using DerafshSample.Modern.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DerafshSample.Modern.Pages;

public sealed class IndexModel(StoreSettingsStore store) : PageModel
{
    [BindProperty]
    public StoreSettingsDto Input { get; set; } = new();

    public string? StatusMessage { get; private set; }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Input = await store.LoadAsync(cancellationToken);
        EnsureEditableRows();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        EnsureEditableRows();

        if (!ModelState.IsValid)
        {
            StatusMessage = "Please review the highlighted fields and try again.";
            return Page();
        }

        try
        {
            await store.SaveAsync(Input, cancellationToken);
            StatusMessage = "Changes saved. The complete graph was synchronized successfully.";
            Input = await store.LoadAsync(cancellationToken);
            EnsureEditableRows();
            return Page();
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "The graph could not be saved. Check the application logs and database configuration.");
            StatusMessage = "Save failed. Your submitted values are still shown below.";
            return Page();
        }
    }

    private void EnsureEditableRows()
    {
        Input.Contacts ??= [];
        Input.ShippingZones ??= [];
        Input.NotificationRecipients ??= [];

        if (Input.Contacts.Count == 0) Input.Contacts.Add(new StoreContactDto());
        if (Input.ShippingZones.Count == 0) Input.ShippingZones.Add(new ShippingZoneDto());
        if (Input.NotificationRecipients.Count == 0) Input.NotificationRecipients.Add(new NotificationRecipientDto());
    }
}
