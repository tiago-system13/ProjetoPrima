(function ($) {
    'use strict';
    var Id = '';
    var tipoPessoa = '';
    var PesquisaProdutoDatatable = function () {
        $("#datatable-produtos").DataTable({
            destroy: true,
            responsive: true,
            processing: true,
            ajax: {
                url: baseUrl('Produto/ObterProdutos'),
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

                    return $.extend({}, data, {
                        NomeProduto: $('#nome').val(),
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
                    var editar = baseUrl('Produto/IndexEditar?id=' + data);
                    return '<a class="btn btn-info btn-sm fa fa-pencil-square-o" data-toggle="tooltip" title="Alterar" href="' + editar + '">Editar</a> ' +
                           '<a class="btn btn-danger btn-sm fa fa-trash-o btn-lg"  title="Excluir" id="btnExcluir" data-id="' + full.Id + '" data-toggle="modal" data-target="#myModal">Excluir</a> ';
                }
            }, {
                targets: 3,
                data: 'ValorUnitario',
                render: function (data, type, full) {

                    return full.ValorUnitario.toLocaleString('pt-br', {
                        style: 'currency', currency: 'BRL', minimumFractionDigits: 2
                    });

                }
            },
            ],
            columns: [
                    { data: 'Id' },
                    { data: 'NomeDoProduto' },
                    { data: 'QuantidadeDoProduto' },
                    { data: 'ValorUnitario' },
                    { data: 'Id' }
            ],
        });
    }

    PesquisaProdutoDatatable();






    $('#nome').on('blur', function () {

        if ($('#tipodePesquisa').val() !== '') {
            PesquisaProdutoDatatable();
        }
    });

    function LimparValorPesquisa() {
        $('#nome').val('');

    }


    function centerModal() {
        $(this).css('display', 'block');
        var $dialog = $(this).find(".modal-dialog");
        var offset = ($(window).height() - $dialog.height()) / 2;
        // Center modal vertically in window
        $dialog.css("margin-top", offset);
    }

    $('#confirme').click(function () {
        $.ajax({
            url: baseUrl('Produto/Excluir'),
            type: "GET",
            dataType: "json",
            data: { 'id': Id },
            success: function (data) {

                if (data.sucesso) {

                    $(window.document.location).attr('href', baseUrl('Produto/Index'));

                } else {

                    $('.alert-danger').remove();
                    $('#mensagemSucessoExclusao').after('<div class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Erro!</strong>' + data.mensagem + '</div>');
                }

            }
        });
    });

    $('#datatable-produtos').delegate('#btnExcluir', 'click', function () {
        Id = $(this).attr('data-id');
        $('.modal').on('show.bs.modal', centerModal);
        $(window).on("resize", function () {
            $('.modal:visible').each(centerModal);
        });
    });

    $('#btnNovoProduto').click(function () {
        var url = $('#RedirectToNovoProduto').val();
        window.location.href = url;
    });
})($ || jquery);