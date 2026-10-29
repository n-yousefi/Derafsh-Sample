(() => {
    const configs = {
        contact: { html: index => `
            <input type="hidden" name="Input.Contacts[${index}].Id" value="0">
            <input type="hidden" name="Input.Contacts[${index}].StoreSettingsId" value="1">
            <select class="text-input select-input" name="Input.Contacts[${index}].Type"><option value="Email">Email</option><option value="Phone">Phone</option><option value="Website">Website</option></select>
            <input class="text-input" name="Input.Contacts[${index}].Value" placeholder="hello@example.com" maxlength="160" required>
            <button class="icon-button remove-row" type="button" aria-label="Remove contact">×</button>` },
        shipping: { html: index => `
            <input type="hidden" name="Input.ShippingZones[${index}].Id" value="0">
            <input type="hidden" name="Input.ShippingZones[${index}].StoreSettingsId" value="1">
            <input class="text-input uppercase" name="Input.ShippingZones[${index}].CountryCode" placeholder="NL" maxlength="2" required>
            <div class="money-input"><span>€</span><input class="text-input" type="number" min="0" max="100000" step="0.01" name="Input.ShippingZones[${index}].Fee" value="0" required></div>
            <button class="icon-button remove-row" type="button" aria-label="Remove shipping zone">×</button>` },
        notification: { html: index => `
            <input type="hidden" name="Input.NotificationRecipients[${index}].Id" value="0">
            <input type="hidden" name="Input.NotificationRecipients[${index}].StoreSettingsId" value="1">
            <input class="text-input" type="email" name="Input.NotificationRecipients[${index}].Email" placeholder="ops@example.com" maxlength="200" required>
            <button class="icon-button remove-row" type="button" aria-label="Remove recipient">×</button>` }
    };
    const selectors = {
        "contacts-rows": { count: "contacts-count", graph: "graph-contacts" },
        "shipping-rows": { count: "shipping-count", graph: "graph-shipping" },
        "notification-rows": { count: "notifications-count", graph: "graph-notifications" }
    };
    function refreshRows(container) {
        const rows = [...container.querySelectorAll(".collection-row")];
        rows.forEach((row, index) => row.querySelectorAll("[name]").forEach(input => {
            input.name = input.name.replace(/\[\d+\]/, `[${index}]`);
        }));
        const config = selectors[container.id];
        if (config) {
            document.getElementById(config.count).textContent = rows.length;
            document.getElementById(config.graph).textContent = rows.length;
        }
    }
    document.querySelectorAll(".add-row").forEach(button => button.addEventListener("click", () => {
        const container = document.getElementById(button.dataset.target);
        const row = document.createElement("div");
        row.className = "collection-row";
        row.innerHTML = configs[button.dataset.kind].html(container.querySelectorAll(".collection-row").length);
        container.appendChild(row);
        row.querySelector("input:not([type=hidden]), select")?.focus();
        refreshRows(container);
    }));
    document.addEventListener("click", event => {
        const button = event.target.closest(".remove-row");
        if (!button) return;
        const row = button.closest(".collection-row");
        const container = row?.parentElement;
        if (row && container) { row.remove(); refreshRows(container); }
    });
    document.getElementById("graph-form")?.addEventListener("submit", () => Object.keys(selectors).forEach(id => refreshRows(document.getElementById(id))));
    document.querySelectorAll(".uppercase").forEach(input => input.addEventListener("input", () => {
        const start = input.selectionStart;
        input.value = input.value.toUpperCase();
        if (start !== null) input.setSelectionRange(start, start);
    }));
})();