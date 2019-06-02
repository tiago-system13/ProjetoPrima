(function ($) {
    'use strict';
    var itensInseridos = [];
    var TotaDaVenda = 0;

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
                    $('#qtdEstoque').val('');
                    $('#vlrUnitario').val('');
                    $('#qtdItem').val('');
                }
            }
        }
    });

    var contadorDeItemNota = 1;
    var tableItensVenda = null;
    function PesquisaItensNotaDatatable() {
        tableItensVenda = $("#datatable-itemVenda").DataTable({
            destroy: true,
            responsive: true,
            processing: true,
            serverSide: false,
            data: [],
            columnDefs: [
                {
                    targets: -1,
                    data: 'NomeDoProduto',
                    render: function (data, type, full) {
                        var buttons = '<a class="btn btn-danger btn-sm fa fa-trash-o btn-lg"  title="Excluir" id="btnExcluir" data-Produto="' + full[0].NomeDoProduto + '" data-ValorTotalItem="' + full[0].ValorTotalItem + '" data-toggle="modal" data-target="#myModalItemExclusao" >Excluir</a> ';
                        return buttons;
                    }
                },
            {
                targets: 0,
                data: 'NomeDoProduto',
                render: function (data, type, full) {
                    if (full[0].NomeDoProduto === null) {
                        return '';
                    } else {
                        return full[0].NomeDoProduto
                    }
                }
            },
            {
                targets: 1,
                data: 'QuantidadeItem',
                render: function (data, type, full) {
                    if (full[0].QuantidadeItem === null) {
                        return '';
                    } else {
                        return full[0].QuantidadeItem
                    }
                },
            },
            {
                targets: 2,
                data: 'ValorUnitario',
                render: function (data, type, full) {
                    if (full[0].ValorUnitario === null) {
                        return '';
                    } else {
                        if (full[0].ValorUnitario !== null)

                            var valorUnitario = full[0].ValorUnitario;
                        return valorUnitario.toLocaleString('pt-br', {
                            style: 'currency', currency: 'BRL', minimumFractionDigits: 2
                        });
                        return "";
                    }
                },
            },
            {
                targets: 3,
                data: 'ValorTotalItem',
                render: function (data, type, full) {
                    if (full[0].ValorTotalItem === null) {
                        return '';
                    }

                    var valorTotalItem = full[0].ValorTotalItem;
                    return valorTotalItem.toLocaleString('pt-br', {
                        style: 'currency', currency: 'BRL', minimumFractionDigits: 2
                    });

                },
            },
            ],
            initComplete: function () {
            },

            columns: [

            {
                data: 'NomeDoProduto'
            },
            {
                data: 'QuantidadeItem', class: "id"
            },
            {
                data: 'ValorUnitario'
            },
            {
                data: 'ValorTotalItem'
            },
            {
                data: 'NomeDoProduto'
            }
            ]
        });
    }
    PesquisaItensNotaDatatable();

    $('#btnAddNovoItemVenda').on('click', function () {

        InserirLinhaNaTabelaItensDaNota($('#search_produto').val(), $('#qtdItem').val(), $('#qtdEstoque').val(), $('#vlrUnitario').val());

    });

    function centerModal() {
        $(this).css('display', 'block');
        var $dialog = $(this).find(".modal-dialog");
        var offset = ($(window).height() - $dialog.height()) / 2;
        // Center modal vertically in window
        $dialog.css("margin-top", offset);
    }

    function InserirLinhaNaTabelaItensDaNota(produto, quantidade, quantidadeEstoque, valorUnitario) {

        if (produto != null && quantidade != '' && valorUnitario != null) {

            var nomeProduto = $('#search_produto option:selected').text();

            var existeProduto = itensInseridos.filter((item) => {
                return item.NomeDoProduto === nomeProduto;
            });

            if (existeProduto.length > 0 && itensInseridos.length > 0) {

                $('.alert-danger').remove();
                $('#mensagemSucessoExclusao').after('<div class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Erro!</strong> o item já foi adicionado na venda.</div>');
                return false;
            }

            var valorUnitario = parseFloat(valorUnitario.replace(',', '.'));
            var saldo = parseInt(quantidadeEstoque)
            var valorTotalItem = (parseInt(quantidade) * valorUnitario);
            if (quantidade > saldo) {
                $('.alert-danger').remove();
                $('#mensagemSucessoExclusao').after('<div class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Erro!</strong> A quantidade do Item deve ser menor ou igual ao saldo do produto</div>');
                return false;
            }

            TotaDaVenda += valorTotalItem;
            $('#totalVenda').val(TotaDaVenda.toFixed(2));

            itensInseridos.push({
                ProdutoId: produto,
                NomeDoProduto: nomeProduto,
                QuantidadeItem: quantidade,
                ValorUnitario: valorUnitario,
                ValorTotalItem: valorTotalItem
            });

            $(".alert-success").remove();
            $('#mensagemSucessoExclusao').after('<div class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Sucesso!</strong> Item da venda incluído com sucesso!</div>');
            $(".alert-success")
            .fadeTo(2000, 500)
            .slideUp(500, function () {
            });

            var itemAdicionado = itensInseridos.filter((item) => {
                return item.NomeDoProduto === nomeProduto;
            });
            tableItensVenda.row.add(itemAdicionado).draw(false);

            $('#qtdEstoque').val('');
            $('#vlrUnitario').val('');
            $('#qtdItem').val('');
            $('#search_produto').empty();

        }
        else {

            $('.alert-danger').remove();
            $('#mensagemSucessoItem').after('<div class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Erro!</strong> Para inserir o item na nota é necessário preencher todos os campos obrigatórios</div>');
        }
    }

    $('#search_produto').on('change', function () {

        if ($(this).val() != null) {

            ObterProduto($(this).val());
        }
    });


    function ObterProduto(produto) {

        $.ajax({
            url: baseUrl('Produto/ObterProdutoPorId'),
            type: "GET",
            dataType: "json",
            data: { produtoId: produto },
            success: function (data) {
                console.log(data);
                $('#qtdEstoque').val(data.QuantidadeDoProduto);
                $('#vlrUnitario').val(data.ValorUnitario);
            }
        });
    }


    function removerLinhaTabela(linha) {
        tableItensVenda.row(linha.parents('tr')).remove().draw(false);
    }

    function atualizarEmMemoriaItemNota(itens, linha) {
        for (var i = 0; i < itens.length; i++) {
            if (linha.NomeDoProduto === itens[i].NomeDoProduto) {
                var index = itens.indexOf(itens[i]);
                itens.splice(index, 1);
            }
        }
    }

    var linha = null;
    $('#datatable-itemVenda').delegate('#btnExcluir', 'click', function () {
        linha = $(this);
        $('.modal').on('show.bs.modal', centerModal);
        $(window).on("resize", function () {
            $('.modal:visible').each(centerModal);
        });
        linha = $(this);
    });


    $('#confirme').click(function () {

        var item = {
            NomeDoProduto: linha.attr('data-Produto')
        }
        atualizarEmMemoriaItemNota(itensInseridos, item);
        removerLinhaTabela(linha);
        var valorTotalItem = parseFloat(linha.attr('data-ValorTotalItem'));
        TotaDaVenda -= valorTotalItem;
        $('#totalVenda').val(TotaDaVenda);

        $('#myModalItemExclusao').modal('toggle');
        $('#mensagemSucessoExclusao').after('<div class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Sucesso!</strong> Item excluído com sucesso!</div>');
        $(".alert-success")
        .fadeTo(2000, 500)
        .slideUp(500, function () {
            $(this).remove();
        });
    });

    var itensNota = [];
    $('#btnSalvar').on('click', function () {
        var totalItens = 0;
        var casasDecimais = [];
        if (itensInseridos.length > 0) {

            for (var i = 0; i < itensInseridos.length; i++) {
                totalItens = (parseFloat(totalItens) + parseFloat(itensInseridos[i].ValorTotal))
            }
        }

        var valorTotal = '0';
        casasDecimais = [];

        valorTotal = $('#totalVenda').val().replace(',', '.');
        casasDecimais = $('#totalVenda').val().replace(',', '.').split('.');
        if (casasDecimais.length > 0) {
            if (casasDecimais.length < 3) {
                valorTotal = parseFloat(valorTotal).toFixed(2);
            } else {
                valorTotal = parseFloat(valorTotal.replace('.', '')).toFixed(2);
            }
        }

        var formData = new FormData()
        formData.append("ClienteId", $('#search_cliente').val());
        formData.append("TotalDaVenda", casasDecimais.length > 2 ? parseFloat(valorTotal).toString().replace('.', ',') : valorTotal.toString().replace('.', ','));

        for (var j = 0; j < itensInseridos.length; j++) {
            formData.append("ItensVenda[" + j + "].ProdutoId", itensInseridos[j].ProdutoId);
            formData.append("ItensVenda[" + j + "].Quantidade", itensInseridos[j].QuantidadeItem);
            formData.append("ItensVenda[" + j + "].TotalItem", itensInseridos[j].ValorTotalItem.toString().replace('.', ','));
        }

        formData.append("__RequestVerificationToken", $('#formVenda input[name=__RequestVerificationToken]').val());

        if (itensInseridos.length == 0) {
            $('#mensagemSucessoExclusao').after('<div class="alert alert-info"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Erro!</strong> ' + "É necessário adicionar um item da venda ." + '</div>');
            return false;
        }

        $('#pleaseWaitDialog').modal('show');

        $.ajax({
            url: baseUrl('Venda/Cadastrar'),
            type: "Post",
            dataType: "json",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                if (data.Sucesso) {
                    window.location.href = baseUrl('Venda/Index');
                } else {
                    $('#mensagemSucessoExclusao').after('<div class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Erro!</strong> ' + data.mensagem + '</div>');
                }
                $('#pleaseWaitDialog').modal('hide');
            }
        });


        itensInseridos = [];
        return false
    });


})($ || jquery);