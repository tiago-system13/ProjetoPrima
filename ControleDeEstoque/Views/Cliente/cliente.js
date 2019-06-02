(function ($) {
    'use strict';

    $("#cpf").mask("999.999.999-99");
    $("#telefone").mask("(99) 99999-9999");
    
    function validarInteiros() {

        if ($("#cnpj").val() !== "") {
            var valor = $("#cnpj").val().match(/\d|,/g);
            if (!valor) {
                $("#cnpj").val('');
            }
        }

        if ($("#cpf").val() !== "") {
            var valor = $("#cpf").val().match(/\d|,/g);
            if (!valor) {
                $("#cpf").val('');
            }
        }
        if ($("#telefone").val() !== "") {
            var valor = $("#telefone").val().match(/\d|,/g);
            if (!valor) {
                $("#telefone").val('');
            }
        }
    }
    
    $("#cpf").on('keyup', function () {
        validarInteiros();
    })

    $("#telefone").on('keyup', function () {
        validarInteiros();
    })

    $('#cpf').on('blur', function () {
        var cpf = $('#cpf').val().replace('.', '').replace('.', '').replace('.', '').replace('-', '');
        if (!validarCPF(cpf)) {
            $('#div_cpf').find('span').html('');
            $('#div_cpf').append(
                   '<span class="form-group has-error field-validation-error" data-valmsg-for="data"  data-val-replace="true">' +
                       '<span style="color:red" class="" id="data-error">CPF inválido!.</span>' +
                       '</span>');

            $('#cpf').focus();
        } else {
            $('#div_cpf').find('span').html('');
        }
    })    

   
    $('#datetimepicker2').datetimepicker({
        format: 'DD/MM/YYYY',
        locale: 'pt-br'
    }).on("dp.change", function () {
        if ($('#dataNascimento').val() != '') {
            $('#div_data').find('span').html('');
        }
    });


    function validarCPF(cpfInformado) {
        if (cpfInformado != '' && cpfInformado != undefined) {
            var cpf = cpfInformado;
            var numeros, digitos, soma, i, resultado, digitos_iguais;
            digitos_iguais = 1;
            if (cpf.length < 11)
                return false;
            for (i = 0; i < cpf.length - 1; i++)
                if (cpf.charAt(i) != cpf.charAt(i + 1)) {
                    digitos_iguais = 0;
                    break;
                }
            if (!digitos_iguais) {
                numeros = cpf.substring(0, 9);
                digitos = cpf.substring(9);
                soma = 0;
                for (i = 10; i > 1; i--)
                    soma += numeros.charAt(10 - i) * i;
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(0))
                    return false;
                numeros = cpf.substring(0, 10);
                soma = 0;
                for (i = 11; i > 1; i--)
                    soma += numeros.charAt(11 - i) * i;
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(1))
                    return false;
                return true;
            }
            else
                return false;
        } else {
            return true
        }
    };
  
    $("#btnSalvar").on("click", function () {

        $('#cpf').val($('#cpf').val().replace('.', '').replace('.', '').replace('.', '').replace('-', ''));

    });

})($ || jquery);