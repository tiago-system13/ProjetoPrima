(function ($) {
    'use strict';

    $("#cpf").mask("999.999.999-99");
    $("#telefone").mask("(99) 9999-9999");

    function validarInteiros() {

        if ($("#quantidade").val() !== "") {
            var valor = $("#quantidade").val().match(/\d|,/g);
            if (!valor) {
                $("#quantidade").val('');
            }
        }
    }
    var ValidarDouble = function () {

        if ($("#preco").val() !== "" && $("#preco").val() !== null) {
            var valor = /^\d+.?\d*$/.test($("#preco").val())
            if (!valor) {
                $("#preco").val('');
            }
        }
    };

    $("#preco").on('keyup', function () {
        ValidarDouble();
    });

    $("#quantidade").on('keyup', function () {
        validarInteiros();
    })


    $("#btnSalvar").on("click", function () {

        $('#preco').val($('#preco').val().replace('.', ','));
        console.log($('#preco').val());
    })

})($ || jquery);