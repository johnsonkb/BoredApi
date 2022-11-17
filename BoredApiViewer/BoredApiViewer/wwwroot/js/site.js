function ToggleSelection(activityKey) {
    $('.ToggleItems').hide(); 
    $('#body-' + activityKey).show();
}

$(() => {
    $('.Hidden').hide();
    $('.card').click(function () {
        var key = $(this).attr('data-id');
        ToggleSelection(key);
    });
});