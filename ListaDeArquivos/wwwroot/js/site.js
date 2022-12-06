// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.close-alert').click(function () {
    $('.alert').hide('hide');
});


$('.btn-abrirModalArquivo').click(function () {
    var arquivoId = $(this).attr('arquivo-id')
    $('#modalAbrir').modal();
    $('.btn-modalApagar').click(function () {
        $.ajax({
            type: 'DELETE',
            url: '/Arquivo/Apagar/' + arquivoId,
        });
        location.reload()
    });
});

$('.btn-abrirModalGasto').click(function () {
    var gastoId = $(this).attr('gasto-id')
    $('#modalAbrir').modal();
    $('.btn-modalApagar').click(function () {
        $.ajax({
            type: 'DELETE',
            url: '/Gasto/Apagar/' + gastoId,
        });
        location.reload()
    });
});

$('.btn-recarregar').click(function () {
    location.reload()
});