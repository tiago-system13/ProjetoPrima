(function ($) {
    'use strict';
    var Id = '';

    $('#search_cliente').select2({
        theme: "bootstrap",
        language: "pt-BR",
        placeholder: "Pesquise aqui pela descrição.",
        minimumInputLength: 0,
        width: '100%',
        ajax: {
            url: baseUrl('Cliente/ObterClientePorNome'),
            dataType: 'json',
            type: "GET",
            delay: 250,
            data: function (params) {
                return {
                    nomeDocumento: params.term  // search term
                };
            },
            processResults: function (data) {
                if (data.length > 0) {
                    return {
                        results: $.map(data, function (item) {
                            return {
                                id: item.Id,
                                text: item.Nome
                            }
                        })
                    };
                } else {
                    $('#search_cliente').empty();
                }
            }
        }
    });

    $('#search_produto').select2({
        theme: "bootstrap",
        language: "pt-BR",
        placeholder: "Pesquise aqui pela descrição.",
        minimumInputLength: 0,
        width: '100%',
        ajax: {
            url: baseUrl('Produto/ObterProdutoPorNome'),
            dataType: 'json',
            type: "GET",
            delay: 250,
            data: function (params) {
                return {
                    nome: params.term  // search term
                };
            },
            processResults: function (data) {
                if (data.length > 0) {
                    return {
                        results: $.map(data, function (item) {
                            return {
                                id: item.Id,
                                text: item.NomeDoProduto
                            }
                        })
                    };
                } else {
                    $('#search_produto').empty();
                }
            }
        }
    });


    var PesquisaHistoricoVendaDatatable = function (cliente, produto, dataVenda) {

        $("#datatable-historico").DataTable({
            destroy: true,
            responsive: true,
            processing: true,
            ajax: {
                url: baseUrl('Venda/ObterHistoricoVenda'),
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
                        Cliente: cliente,
                        ProdutoId: produto,
                        DataDaVenda: dataVenda,
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

                    return '<a class="btn btn-info btn-sm fa  fa-list-ul"  title="Itens" id="btnItens"  data-id="' + full.Id + '" data-toggle="modal" data-target="#myModalItens"</a> Itens da Venda';
                }
            },
            {
                targets: 2,
                data: 'TotalDaVenda',
                render: function (data, type, full) {

                    return full.TotalDaVenda.toLocaleString('pt-br', {
                        style: 'currency', currency: 'BRL', minimumFractionDigits: 2
                    });

                }
            },
            {
                targets: 3,
                searchable: true,
                orderable: true,
                data: 'DataDaVenda',
                render: function (data) {
                    if (data !== null)
                        return moment(data).format('DD/MM/YYYY');
                    return "";
                }
            },

            ],
            columns: [
                    { data: 'Id' },
                    { data: 'NomeCliente' },
                    { data: 'TotalDaVenda' },
                    { data: 'DataDaVenda' },
                    { data: 'Id' }
            ],
        });
    }

    PesquisaHistoricoVendaDatatable($('#search_cliente').val(), $('#search_produto').val(), $('#dtVenda').val());

    $('#div_cliente').hide();
    $('#div_produto').hide();
    $('#div_data_venda').hide();

    $('#search_produto').on('change', function () {
        PesquisaHistoricoVendaDatatable($('#search_cliente').val(), $('#search_produto').val(), $('#dtVenda').val());
    });

    $('#search_cliente').on('change', function () {

        PesquisaHistoricoVendaDatatable($('#search_cliente').val(), $('#search_produto').val(), $('#dtVenda').val());
    });

    $('#datetimepicker3').datetimepicker({
        format: 'DD/MM/YYYY',
        locale: 'pt-br'
    }).on("dp.change", function () {

        PesquisaHistoricoVendaDatatable($('#search_cliente').val(), $('#search_produto').val(), $('#dtVenda').val());
    });

    function LimparValorPesquisa() {
        $('#search_produto').empty();
        $('#search_cliente').empty();
        $('#dtVenda').val('');
    }

    $("#tipodePesquisa").on('change', function () {
        var tipo = $("#tipodePesquisa").val();
        if (tipo !== '') {            
            if (tipo === "1") {
                $('#div_cliente').show();
                $('#div_produto').hide();
                $('#div_data_venda').hide();
                LimparValorPesquisa();

            } else if (tipo === "2") {
                $('#div_cliente').hide();
                $('#div_produto').show();
                $('#div_data_venda').hide();
                LimparValorPesquisa();
            } else if (tipo === "3") {
                $('#div_cliente').hide();
                $('#div_produto').hide();
                $('#div_data_venda').show();
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

    $("#datatable-historico").delegate('#btnItens', 'click', function () {
        PesquisaItemVendaDatatable();
        CarregarTabelaItemVenda($(this).attr('data-id'));
    });

    var tableItemVenda = null;
    function PesquisaItemVendaDatatable() {
        tableItemVenda = $("#datatable-item-venda").DataTable({
            destroy: true,
            responsive: true,
            "paging": true,
            "deferRender": true,
            "searching": true,
            "info": true,
            serverSide: false,
            data: [],
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
            columnDefs: [
           {
               targets: 2,
               data: 'TotalItem',
               render: function (data, type, full) {

                   return full.TotalItem.toLocaleString('pt-br', {
                       style: 'currency', currency: 'BRL', minimumFractionDigits: 2
                   });

               }
           }],
            columns: [
                     {
                         data: 'NomeProduto'
                     },
                     {
                         data: 'Quantidade'
                     },
                    {
                        data: 'TotalItem'
                    }
            ],
        });
    }

    PesquisaItemVendaDatatable();

    function CarregarTabelaItemVenda(id) {
        var formData = new FormData()
        formData.append("Id", id);
        $.ajax({
            url: baseUrl('Venda/ObterItemDaVenda'),
            type: "post",
            dataType: "json",
            data: formData,
            processData: false,
            contentType: false,
            dataSrc: 'Lista',
            dataFilter: function (data) {
                var json = JSON.parse(data);
                json.recordsTotal = json.TotalDeRegistros;
                json.recordsFiltered = json.TotalDeRegistros;
                json.data = json.Lista;
                var lista = json.data;
                $.each(lista, function (index, val) {
                    val.DataDaVenda = moment(val.DataValidade).format('DD/MM/YYYY');
                    tableItemVenda.row.add(val).draw(false);
                });
            }
        });
    }

})($ || jquery);