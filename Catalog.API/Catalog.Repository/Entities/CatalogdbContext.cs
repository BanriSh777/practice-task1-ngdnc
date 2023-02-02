using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repository.Entities;

public partial class CatalogdbContext : DbContext
{
    public CatalogdbContext()
    {
    }

    public CatalogdbContext(DbContextOptions<CatalogdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productstore> Productstores { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=cataloguser;password=catalog;database=catalogdb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("category");

            entity.HasIndex(e => e.CategoryName, "uc_Category_CategoryName").IsUnique();

            entity.Property(e => e.CategoryId).HasMaxLength(20);
            entity.Property(e => e.CategoryDescription).HasColumnType("text");
            entity.Property(e => e.CategoryName).HasMaxLength(30);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.CategoryId, "fk_Product_Category_CategoryId");

            entity.Property(e => e.ProductId).HasMaxLength(20);
            entity.Property(e => e.CategoryId).HasMaxLength(20);
            entity.Property(e => e.Depth)
                .HasPrecision(5)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Discount)
                .HasPrecision(9)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.Height)
                .HasPrecision(5)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.Image).HasColumnType("blob");
            entity.Property(e => e.Mrp)
                .HasPrecision(9)
                .HasColumnName("MRP");
            entity.Property(e => e.Price).HasPrecision(9);
            entity.Property(e => e.ProductName).HasMaxLength(35);
            entity.Property(e => e.Width)
                .HasPrecision(5)
                .HasDefaultValueSql("'0.00'");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Product_Category_CategoryId");
        });

        modelBuilder.Entity<Productstore>(entity =>
        {
            entity.HasKey(e => e.ProductStoreId).HasName("PRIMARY");

            entity.ToTable("productstore");

            entity.Property(e => e.ProductStoreId).HasMaxLength(25);
            entity.Property(e => e.Location).HasMaxLength(40);
            entity.Property(e => e.ProductStoreName).HasMaxLength(45);
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.ProductStoreId }).HasName("PRIMARY");

            entity.ToTable("stock");

            entity.HasIndex(e => e.ProductStoreId, "fk_Stock_ProductStore_ProductStoreId");

            entity.HasIndex(e => e.ProductId, "fk_Stock_Product_ProductId");

            entity.Property(e => e.ProductId).HasMaxLength(20);
            entity.Property(e => e.ProductStoreId).HasMaxLength(25);
            entity.Property(e => e.Unit).HasMaxLength(20);

            entity.HasOne(d => d.Product).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_Stock_Product_ProductId");

            entity.HasOne(d => d.ProductStore).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductStoreId)
                .HasConstraintName("fk_Stock_ProductStore_ProductStoreId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
