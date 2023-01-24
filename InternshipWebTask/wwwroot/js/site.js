$(document).ready(function () {
    $('.delete-author').click(function (e) {
        e.preventDefault();
        var authorId = $(this).data('author-id');
        $.ajax({
            type: 'DELETE',
            url: '/Home/Delete/' + authorId,
            success: function (response) {
                $(e.target).closest('li').remove();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});

$(document).ready(function () {
    $('.delete-book').click(function (e) {
        e.preventDefault();
        var bookId = $(this).data('book-id');
        $.ajax({
            type: 'POST',
            url: '/Book/Delete/' + bookId,
            success: function (response) {
                $(e.target).closest('li').remove();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});

function setBookId(id) {
    $("#bookId").val(id);
}






