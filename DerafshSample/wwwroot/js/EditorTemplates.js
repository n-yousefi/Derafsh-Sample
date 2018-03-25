$(function () {
    edit_entity_init();
    
    uiRemoteSelect_init();
});

function edit_entity_init() {
    $('.edit-entity').click(function () {
        var $this = $(this);
        var dropDown = '#' + $this.attr('data-id');
        var selectedId = $(dropDown).val();
        var url = $this.attr('data-href') + '/' + selectedId;
        window.location = url;
    });
}

function uiRemoteSelect_init() {
    $('.bs-select').each(function (i, obj) {
        var id = obj.getAttribute('Id');
        var url = obj.getAttribute('data-fetch-url');
        var current = obj.getAttribute('data-val');

        $('#' + id).selectpicker({
            style: 'btn-info',
            liveSearch: true
        });

        $.ajax({
            url: url,
            cache: true,
            success: function (data) {
                $.each(data,
                    function () {
                        var sel = "";
                        if (current == this.value)
                            sel = "selected";
                        $(obj).append('<option value="' + this.value + '" ' + sel + '>' + this.text + '</option>');
                    });
                $(obj).selectpicker('refresh');
            }
        });
    });
}