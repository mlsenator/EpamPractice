
$(document).ready(function () {

    $('#alert-btn').click(function () {
        alert('Done');
    });

    $('#dialog-btn').click(function () {
        $('#bla').css('visibility', 'visible');
        $('#bla').attr("src", "Home/Alert");
    })

});


