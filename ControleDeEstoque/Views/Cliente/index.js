(function ($) {
    'use strict';    
    var Id = '';    
    var PesquisaClientesDatatable = function () {
        $("#datatable-clientes").DataTable({
            destroy: true,
            responsive: true,
            processing: true,
            ajax: {
                url: baseUrl('Cliente/ObterClientes'),
                dataSrc: 'Lista',
                dataFilter: function (data) {
                    var json = JSON.parse(data);
                    json.recordsTotal = json.TotalDeRegistros;
                    json.recordsFiltered = json.TotalDeRegistros;
                    json.data = json.Lista;
                    return JSON.stringify(json);
                },
                bAutoWidth: true,
                data: function (data) {
                    var documento = $('#documento').val().replace('.', '').replace('.', '').replace('.', '').replace('-', '');                                        
                    return $.extend({}, data, {
                        Cpf: documento,
                        Nome: $('#nome').val(),                        
                        IndiceDePagina: data.start / data.length + 1,
                        RegistrosPorPagina: data.length,
                        Ordenacao: data.order[0].dir,
                        Coluna: data.columns[data.order[0].column].data
                    });
                }
            },
            initComplete: function () {
                this.api().columns().every(function () {
                    var column = this;
                    var select = $('<select><option value=""></option></select>')
                        .appendTo($(column.footer()).empty())
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex(
                                $(this).val()
                            );

                            column
                                .search(val ? '^' + val + '$' : '', true, false)
                                .draw();
                        });

                    column.data().unique().sort().each(function (d) {
                        select.append('<option value="' + d + '">' + d + '</option>')
                    });
                });
            },

            columnDefs: [{
                targets: -1,
                data: 'Id',
                render: function (data, type, full, row) {
                    var editar = baseUrl('Cliente/IndexEditar?id=' + data );
                    return '<a class="btn btn-info btn-sm fa fa-pencil-square-o" data-toggle="tooltip" title="Alterar" href="' + editar + '">Editar</a> ' +
                           '<a class="btn btn-danger btn-sm fa fa-trash-o btn-lg"  title="Excluir" id="btnExcluir" data-id="' + full.Id + '" data-toggle="modal" data-target="#myModal">Excluir</a> ';
                }
            },
            {
                targets: 3,
                searchable: true,
                orderable: true,
                data: 'DataDeNascimento',
                render: function (data) {
                    if (data !== null)
                        return moment(data).format('DD/MM/YYYY');
                    return "";
                }
            },

            ],
            columns: [
                    { data: 'Id' },
                    { data: 'Cpf' },
                    { data: 'Nome' },
                    { data: 'DataDeNascimento' },
                    { data: 'Telefone' },
                    { data: 'Email' },                    
                    { data: 'Id' }
            ],
        });
    }

    PesquisaClientesDatatable();


    $('#div_valorDocumento').hide();
    $('#div_valorNome').hide();    

    $('#tipodePessoa').on('change', function () {
        if ($(this).val() !== '') {                
            $('#documento').mask("999.999.999-99");          
        }
    });

    $('#documento').on('blur', function () {

        if ($('#tipodePesquisa').val() !== '' && $('#tipodePessoa').val() != '') {
            PesquisaClientesDatatable();
        }
    });

    $('#nome').on('blur', function () {

        if ($('#tipodePesquisa').val() !== '' && $('#tipodePessoa').val() != '') {
            PesquisaClientesDatatable();
        }
    });

    function LimparValorPesquisa() {
        $('#nome').val('');
        $('#documento').val('');
    }

    $("#tipodePesquisa").on('change', function () {
        var tipo = $("#tipodePesquisa").val();
        if (tipo !== '') {
            PesquisaClientesDatatable();
            if (tipo === "1") {
                $('#div_valorDocumento').show();
                $('#documento').mask("999.999.999-99");
                $('#div_valorNome').hide();
                LimparValorPesquisa();

            } else if (tipo === "2") {
                $('#div_valorDocumento').hide();
                $('#div_valorNome').show();
                LimparValorPesquisa();
            }
        }
    });

    function centerModal() {
        $(this).css('display', 'block');
        var $dialog = $(this).find(".modal-dialog");
        var offset = ($(window).height() - $dialog.height()) / 2;
        // Center modal vertically in window
        $dialog.css("margin-top", offset);
    }

    $('#confirme').click(function () {
        $.ajax({
            url: baseUrl('Cliente/Excluir'),
            type: "GET",
            dataType: "json",
            data: { 'id': Id },
            success: function (data) {

                if (data.sucesso) {

                    $(window.document.location).attr('href', baseUrl('Cliente/Index'));

                } else {

                    $('.alert-danger').remove();
                    $('#mensagemSucessoExclusao').after('<div class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Erro!</strong>' + data.mensagem + '</div>');
                }
            }
        });
    });

    $('#datatable-clientes').delegate('#btnExcluir', 'click', function () {
        Id = $(this).attr('data-id');
        console.log(Id);
        $('.modal').on('show.bs.modal', centerModal);
        $(window).on("resize", function () {
            $('.modal:visible').each(centerModal);
        });
    });

    $('#btnNovoCliente').click(function () {
        var url = $('#RedirectToNovoCliente').val();
        window.location.href = url;
    });
})($ || jquery);