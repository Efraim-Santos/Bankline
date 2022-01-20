var buttonSave = document.querySelector('#salvar').addEventListener("click", function (event) {

    var dataTable = document.querySelectorAll('.table tbody tr');

    if (dataTable.length <= 0)
        return;

    var bankStatement = [];

    for (var i = 0; i < dataTable.length; i++) {

        var childernLine = dataTable[i].children;

        bankStatement.push({
            Trntype: childernLine[1].innerHTML,
            Dtposted: childernLine[0].innerHTML,
            Trnamt: childernLine[2].innerHTML,
            Memo: childernLine[3].innerHTML
        });
    }
    $.ajax({
        type: "POST",
        url: "/BankStatement/CreatedDBNewRegisterOfExtractBank",
        data: JSON.stringify({ AllTransaction: bankStatement }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            resultado(true, result.message);
            removerTodosArquivos();
        },
        error: function (err) {
            resultado(false, `${err.statusText} ${err.status}`)
        }
    });

});

var removerTodosArquivos = function () {
    fetch("/ProcessFiles/RemoveAllFiles")
        .then(function () { });
}

var resultado = function (isSuccess, message) {
    $('.message').css("display", "block");

    if (isSuccess) {
        $('.message div').addClass("alert-success");
        $('#salvar').css("display", "none");
    } else {
        $('.message div').addClass("alert-danger");
    }
    $('.message span').html(message);
}