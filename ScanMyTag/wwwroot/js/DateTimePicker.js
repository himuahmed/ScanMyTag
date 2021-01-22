$(function () {
    $.datetimepicker.setDateFormatter('moment');
    $(".datetimefield").datetimepicker({
        format: 'DD-MM-YYYY',
        timepicker: false
    });
});