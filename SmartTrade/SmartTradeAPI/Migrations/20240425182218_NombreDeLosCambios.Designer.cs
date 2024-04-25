﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartTrade.Persistence;

#nullable disable

namespace SmartTradeAPI.Migrations
{
    [DbContext(typeof(SmartTradeContext))]
    [Migration("20240425182218_NombreDeLosCambios")]
    partial class NombreDeLosCambios
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartTrade.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConsumerEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Door")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerEmail");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SmartTrade.Entities.Alert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserEmail");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("SmartTrade.Entities.BizumInfo", b =>
                {
                    b.Property<string>("TelephonNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConsumerEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TelephonNumber");

                    b.HasIndex("ConsumerEmail");

                    b.ToTable("Bizums");
                });

            modelBuilder.Entity("SmartTrade.Entities.CreditCardInfo", b =>
                {
                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CVV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardHolder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConsumerEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CardNumber");

                    b.HasIndex("ConsumerEmail");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("SmartTrade.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("ImageSource")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("SmartTrade.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdminEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SellerEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TargetPostId")
                        .HasColumnType("int");

                    b.Property<string>("TargetUserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Visited")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AdminEmail");

                    b.HasIndex("SellerEmail");

                    b.HasIndex("TargetPostId");

                    b.HasIndex("TargetUserEmail");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("SmartTrade.Entities.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("ShippingCost")
                        .HasColumnType("real");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("ProductId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("SmartTrade.Entities.PayPalInfo", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConsumerEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.HasIndex("ConsumerEmail");

                    b.ToTable("PayPals");
                });

            modelBuilder.Entity("SmartTrade.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdminEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("SellerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Validated")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AdminEmail");

                    b.HasIndex("ProductId");

                    b.HasIndex("SellerEmail");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("SmartTrade.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdminEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Certification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("EcologicPrint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HowToReducePrint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HowToUse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MinimumAge")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminEmail");

                    b.ToTable("Products");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Product");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SmartTrade.Entities.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConsumerEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("PurchasePostId")
                        .HasColumnType("int");

                    b.Property<int?>("PurchaseProductId")
                        .HasColumnType("int");

                    b.Property<string>("PurchaseSellerEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ShippingPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerEmail");

                    b.HasIndex("PurchasePostId");

                    b.HasIndex("PurchaseProductId");

                    b.HasIndex("PurchaseSellerEmail");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("SmartTrade.Entities.User", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("LastNames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SmartTrade.Entities.Wish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserEmail");

                    b.ToTable("Wish");
                });

            modelBuilder.Entity("SmartTrade.Entities.Book", b =>
                {
                    b.HasBaseType("SmartTrade.Entities.Product");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pages")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Book");
                });

            modelBuilder.Entity("SmartTrade.Entities.Clothing", b =>
                {
                    b.HasBaseType("SmartTrade.Entities.Product");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Clothing");
                });

            modelBuilder.Entity("SmartTrade.Entities.Nutrition", b =>
                {
                    b.HasBaseType("SmartTrade.Entities.Product");

                    b.Property<string>("Allergens")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Calories")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Carbohydrates")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fats")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Proteins")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Weight")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Nutrition");
                });

            modelBuilder.Entity("SmartTrade.Entities.Toy", b =>
                {
                    b.HasBaseType("SmartTrade.Entities.Product");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Products", t =>
                        {
                            t.Property("Brand")
                                .HasColumnName("Toy_Brand");

                            t.Property("Material")
                                .HasColumnName("Toy_Material");
                        });

                    b.HasDiscriminator().HasValue("Toy");
                });

            modelBuilder.Entity("SmartTrade.Entities.Admin", b =>
                {
                    b.HasBaseType("SmartTrade.Entities.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("SmartTrade.Entities.Consumer", b =>
                {
                    b.HasBaseType("SmartTrade.Entities.User");

                    b.Property<int>("BillingAddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("BillingAddressId");

                    b.ToTable("User", t =>
                        {
                            t.Property("DNI")
                                .HasColumnName("Consumer_DNI");
                        });

                    b.HasDiscriminator().HasValue("Consumer");
                });

            modelBuilder.Entity("SmartTrade.Entities.Seller", b =>
                {
                    b.HasBaseType("SmartTrade.Entities.User");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IBAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Seller");
                });

            modelBuilder.Entity("SmartTrade.Entities.Address", b =>
                {
                    b.HasOne("SmartTrade.Entities.Consumer", null)
                        .WithMany("Addresses")
                        .HasForeignKey("ConsumerEmail");
                });

            modelBuilder.Entity("SmartTrade.Entities.Alert", b =>
                {
                    b.HasOne("SmartTrade.Entities.Product", "Product")
                        .WithMany("Alerts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartTrade.Entities.User", "User")
                        .WithMany("Alerts")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartTrade.Entities.BizumInfo", b =>
                {
                    b.HasOne("SmartTrade.Entities.Consumer", null)
                        .WithMany("BizumAccounts")
                        .HasForeignKey("ConsumerEmail");
                });

            modelBuilder.Entity("SmartTrade.Entities.CreditCardInfo", b =>
                {
                    b.HasOne("SmartTrade.Entities.Consumer", null)
                        .WithMany("CreditCards")
                        .HasForeignKey("ConsumerEmail");
                });

            modelBuilder.Entity("SmartTrade.Entities.Image", b =>
                {
                    b.HasOne("SmartTrade.Entities.Product", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("SmartTrade.Entities.Notification", b =>
                {
                    b.HasOne("SmartTrade.Entities.Admin", null)
                        .WithMany("Notifications")
                        .HasForeignKey("AdminEmail");

                    b.HasOne("SmartTrade.Entities.Seller", null)
                        .WithMany("Notifications")
                        .HasForeignKey("SellerEmail");

                    b.HasOne("SmartTrade.Entities.Post", "TargetPost")
                        .WithMany()
                        .HasForeignKey("TargetPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartTrade.Entities.Consumer", "TargetUser")
                        .WithMany("Notifications")
                        .HasForeignKey("TargetUserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TargetPost");

                    b.Navigation("TargetUser");
                });

            modelBuilder.Entity("SmartTrade.Entities.Offer", b =>
                {
                    b.HasOne("SmartTrade.Entities.Post", "Post")
                        .WithMany("Offers")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartTrade.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SmartTrade.Entities.PayPalInfo", b =>
                {
                    b.HasOne("SmartTrade.Entities.Consumer", null)
                        .WithMany("PayPalAccounts")
                        .HasForeignKey("ConsumerEmail");
                });

            modelBuilder.Entity("SmartTrade.Entities.Post", b =>
                {
                    b.HasOne("SmartTrade.Entities.Admin", null)
                        .WithMany("ValidatedPosts")
                        .HasForeignKey("AdminEmail");

                    b.HasOne("SmartTrade.Entities.Product", null)
                        .WithMany("Posts")
                        .HasForeignKey("ProductId");

                    b.HasOne("SmartTrade.Entities.Seller", "Seller")
                        .WithMany("Posts")
                        .HasForeignKey("SellerEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("SmartTrade.Entities.Product", b =>
                {
                    b.HasOne("SmartTrade.Entities.Admin", null)
                        .WithMany("ValidatedProducts")
                        .HasForeignKey("AdminEmail");
                });

            modelBuilder.Entity("SmartTrade.Entities.Purchase", b =>
                {
                    b.HasOne("SmartTrade.Entities.Consumer", null)
                        .WithMany("Purchases")
                        .HasForeignKey("ConsumerEmail");

                    b.HasOne("SmartTrade.Entities.Post", "PurchasePost")
                        .WithMany()
                        .HasForeignKey("PurchasePostId");

                    b.HasOne("SmartTrade.Entities.Product", "PurchaseProduct")
                        .WithMany()
                        .HasForeignKey("PurchaseProductId");

                    b.HasOne("SmartTrade.Entities.Seller", "PurchaseSeller")
                        .WithMany()
                        .HasForeignKey("PurchaseSellerEmail");

                    b.Navigation("PurchasePost");

                    b.Navigation("PurchaseProduct");

                    b.Navigation("PurchaseSeller");
                });

            modelBuilder.Entity("SmartTrade.Entities.Wish", b =>
                {
                    b.HasOne("SmartTrade.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartTrade.Entities.User", "User")
                        .WithMany("WishList")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartTrade.Entities.Consumer", b =>
                {
                    b.HasOne("SmartTrade.Entities.Address", "BillingAddress")
                        .WithMany()
                        .HasForeignKey("BillingAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BillingAddress");
                });

            modelBuilder.Entity("SmartTrade.Entities.Post", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("SmartTrade.Entities.Product", b =>
                {
                    b.Navigation("Alerts");

                    b.Navigation("Images");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("SmartTrade.Entities.User", b =>
                {
                    b.Navigation("Alerts");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("SmartTrade.Entities.Admin", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("ValidatedPosts");

                    b.Navigation("ValidatedProducts");
                });

            modelBuilder.Entity("SmartTrade.Entities.Consumer", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("BizumAccounts");

                    b.Navigation("CreditCards");

                    b.Navigation("Notifications");

                    b.Navigation("PayPalAccounts");

                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("SmartTrade.Entities.Seller", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
