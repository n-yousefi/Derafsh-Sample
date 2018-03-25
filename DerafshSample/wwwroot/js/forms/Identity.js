$(function () {
    $('input[name="IdentityEnumId"]:radio').change(function() {
        var val = $("input[name='IdentityEnumId']:checked").val();
        if (val == 15) {
            $('#Person').show();
            $('#Organization').hide();
        } else {
            $('#Person').hide();
            $('#Organization').show();
        }
    }).change();
});