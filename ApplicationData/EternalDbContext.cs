using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend_Eternal.ApplicationData;

public partial class EternalDbContext : DbContext
{
    public EternalDbContext()
    {
    }

    public EternalDbContext(DbContextOptions<EternalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<FavoritesView> FavoritesViews { get; set; }

    public virtual DbSet<Nft> Nfts { get; set; }

    public virtual DbSet<NftView> NftViews { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagsNft> TagsNfts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersNft> UsersNfts { get; set; }

    public virtual DbSet<UsersView> UsersViews { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=IgorPc\\SQLEXPRESS; Database=EternalDb; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.NameShort)
                .HasMaxLength(3)
                .HasColumnName("name_short");
            entity.Property(e => e.PriceRub)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price_rub");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.Property(e => e.FavoriteId).HasColumnName("favorite_id");
            entity.Property(e => e.NftId).HasColumnName("nft_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Nft).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.NftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorites_NFT");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorites_Users");
        });

        modelBuilder.Entity<FavoritesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FavoritesView");

            entity.Property(e => e.CoverImage).HasColumnName("cover_image");
            entity.Property(e => e.FavoriteId).HasColumnName("favorite_id");
            entity.Property(e => e.NftId).HasColumnName("nft_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Nft>(entity =>
        {
            entity.ToTable("NFT");

            entity.Property(e => e.NftId).HasColumnName("nft_id");
            entity.Property(e => e.CostCurrency)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cost_currency");
            entity.Property(e => e.CoverImage).HasColumnName("cover_image");
            entity.Property(e => e.CreationDate)
                .HasColumnType("date")
                .HasColumnName("creation_date");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<NftView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NftView");

            entity.Property(e => e.CostCurrency)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cost_currency");
            entity.Property(e => e.CoverImage).HasColumnName("cover_image");
            entity.Property(e => e.CreationDate)
                .HasColumnType("date")
                .HasColumnName("creation_date");
            entity.Property(e => e.Currency)
                .HasMaxLength(50)
                .HasColumnName("currency");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.CurrencyShort)
                .HasMaxLength(3)
                .HasColumnName("currency_short");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.NftId).HasColumnName("nft_id");
            entity.Property(e => e.Owner)
                .HasMaxLength(30)
                .HasColumnName("owner");
            entity.Property(e => e.PriceRub)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price_rub");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TagsNft>(entity =>
        {
            entity.HasKey(e => e.TagNftId);

            entity.ToTable("TagsNFT");

            entity.Property(e => e.TagNftId).HasColumnName("tag_nft_id");
            entity.Property(e => e.NftId).HasColumnName("nft_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");

            entity.HasOne(d => d.Nft).WithMany(p => p.TagsNfts)
                .HasForeignKey(d => d.NftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TagsNFT_NFT");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagsNfts)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TagsNFT_Tags");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CoverImage).HasColumnName("cover_image");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.TagName)
                .HasMaxLength(35)
                .HasColumnName("tag_name");
        });

        modelBuilder.Entity<UsersNft>(entity =>
        {
            entity.HasKey(e => e.UserNftId);

            entity.ToTable("UsersNFT");

            entity.Property(e => e.UserNftId).HasColumnName("user_nft_id");
            entity.Property(e => e.NftId).HasColumnName("nft_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Nft).WithMany(p => p.UsersNfts)
                .HasForeignKey(d => d.NftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersNFT_NFT");

            entity.HasOne(d => d.User).WithMany(p => p.UsersNfts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersNFT_Users");
        });

        modelBuilder.Entity<UsersView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UsersView");

            entity.Property(e => e.Balance)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("balance");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.Property(e => e.WalletId).HasColumnName("wallet_id");
            entity.Property(e => e.Balance)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("balance");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Currency).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wallets_Currencies");

            entity.HasOne(d => d.User).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wallets_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
