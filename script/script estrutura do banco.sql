
Go
CREATE SCHEMA estoque    
Go
create table estoque.tb_cliente(
id integer not null IDENTITY(1,1) primary key,
nome varchar(80) not null,
cpf varchar(11) not null,
telefone  varchar(16),
email varchar(30)
);

create table estoque.tb_produto(
id integer not null IDENTITY(1,1) primary key,
nome varchar(50) not null,
quantidade integer not null,
preco_unitario decimal not null,
);

create table estoque.tb_venda(
id integer not null IDENTITY(1,1) primary key,
cliente_id integer not null,
dt_venda datetime not null,
total_venda decimal not null ,
CONSTRAINT FK_venda_cliente FOREIGN KEY (cliente_id)     
    REFERENCES estoque.tb_cliente (id)     
);

create table estoque.tb_item_venda(
id integer not null IDENTITY(1,1) primary key,
venda_id integer not null,
produto_id integer not null,
quantidade integer not null,
total_item decimal not null, 
CONSTRAINT FK_item_venda FOREIGN KEY (venda_id)     
    REFERENCES estoque.tb_venda (id),

CONSTRAINT FK_item_produto FOREIGN KEY (produto_id)     
    REFERENCES estoque.tb_produto (id)     
)

GO 

  



