$(function () {
    $('#commentForm').submit(function (e) {
        e.preventDefault();
        var msg = $(this).serialize();
        $.ajax({
            type: 'POST',
            data: msg,
            success: function (newComment) {
                $('#comments').append('<li> Id : ' + newComment.Id + ' | comment : ' + newComment.Comment + '</li>')
            }
        })
    });
});