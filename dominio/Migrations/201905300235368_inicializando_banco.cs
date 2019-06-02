namespace dominio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicializando_banco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "estoque.tb_cliente",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cpf = c.String(nullable: false, maxLength: 11),
                        nome = c.String(nullable: false, maxLength: 80),
                        dt_nascimento = c.DateTime(),
                        telefone = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "estoque.tb_item_venda_produto",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        produto_id = c.Int(nullable: false),
                        venda_id = c.Int(nullable: false),
                        quantidade = c.Int(nullable: false),
                        total_item = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("estoque.tb_produto", t => t.produto_id, cascadeDelete: true)
                .ForeignKey("estoque.tb_venda", t => t.venda_id, cascadeDelete: true)
                .Index(t => t.produto_id)
                .Index(t => t.venda_id);
            
            CreateTable(
                "estoque.tb_produto",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 60),
                        quantidade = c.Int(nullable: false),
                        preco_unitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "estoque.tb_venda",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cliente_id = c.Int(nullable: false),
                        dt_venda = c.DateTime(nullable: false),
                        total_venda = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("estoque.tb_cliente", t => t.cliente_id, cascadeDelete: true)
                .Index(t => t.cliente_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("estoque.tb_item_venda_produto", "venda_id", "estoque.tb_venda");
            DropForeignKey("estoque.tb_venda", "cliente_id", "estoque.tb_cliente");
            DropForeignKey("estoque.tb_item_venda_produto", "produto_id", "estoque.tb_produto");
            DropIndex("estoque.tb_venda", new[] { "cliente_id" });
            DropIndex("estoque.tb_item_venda_produto", new[] { "venda_id" });
            DropIndex("estoque.tb_item_venda_produto", new[] { "produto_id" });
            DropTable("estoque.tb_venda");
            DropTable("estoque.tb_produto");
            DropTable("estoque.tb_item_venda_produto");
            DropTable("estoque.tb_cliente");
        }
    }
}
