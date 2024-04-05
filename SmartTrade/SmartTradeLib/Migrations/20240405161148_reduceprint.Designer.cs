﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartTradeLib.Persistence;

#nullable disable

namespace SmartTradeLib.Migrations
{
    [DbContext(typeof(SmartTradeContext))]
    [Migration("20240405161148_reduceprint")]
    partial class reduceprint
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

            modelBuilder.Entity("SmartTradeLib.Entities.Address", b =>
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

            modelBuilder.Entity("SmartTradeLib.Entities.Alert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConsumerEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerEmail");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserEmail");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.BizumInfo", b =>
                {
                    b.Property<string>("TelephonNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConsumerEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TelephonNumber");

                    b.HasIndex("ConsumerEmail");

                    b.ToTable("Bizums");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.CreditCardInfo", b =>
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

                    b.Property<string>("ExpirationDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CardNumber");

                    b.HasIndex("ConsumerEmail");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Image", b =>
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

            modelBuilder.Entity("SmartTradeLib.Entities.Offer", b =>
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

            modelBuilder.Entity("SmartTradeLib.Entities.PayPalInfo", b =>
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

            modelBuilder.Entity("SmartTradeLib.Entities.Post", b =>
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

            modelBuilder.Entity("SmartTradeLib.Entities.Product", b =>
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

            modelBuilder.Entity("SmartTradeLib.Entities.User", b =>
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

            modelBuilder.Entity("SmartTradeLib.Entities.Book", b =>
                {
                    b.HasBaseType("SmartTradeLib.Entities.Product");

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

            modelBuilder.Entity("SmartTradeLib.Entities.Clothing", b =>
                {
                    b.HasBaseType("SmartTradeLib.Entities.Product");

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

            modelBuilder.Entity("SmartTradeLib.Entities.Nutrition", b =>
                {
                    b.HasBaseType("SmartTradeLib.Entities.Product");

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

            modelBuilder.Entity("SmartTradeLib.Entities.Toy", b =>
                {
                    b.HasBaseType("SmartTradeLib.Entities.Product");

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

            modelBuilder.Entity("SmartTradeLib.Entities.Admin", b =>
                {
                    b.HasBaseType("SmartTradeLib.Entities.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Consumer", b =>
                {
                    b.HasBaseType("SmartTradeLib.Entities.User");

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

            modelBuilder.Entity("SmartTradeLib.Entities.Seller", b =>
                {
                    b.HasBaseType("SmartTradeLib.Entities.User");

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

            modelBuilder.Entity("SmartTradeLib.Entities.Address", b =>
                {
                    b.HasOne("SmartTradeLib.Entities.Consumer", null)
                        .WithMany("Addresses")
                        .HasForeignKey("ConsumerEmail");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Alert", b =>
                {
                    b.HasOne("SmartTradeLib.Entities.Consumer", null)
                        .WithMany("Alerts")
                        .HasForeignKey("ConsumerEmail");

                    b.HasOne("SmartTradeLib.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartTradeLib.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.BizumInfo", b =>
                {
                    b.HasOne("SmartTradeLib.Entities.Consumer", null)
                        .WithMany("BizumAccounts")
                        .HasForeignKey("ConsumerEmail");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.CreditCardInfo", b =>
                {
                    b.HasOne("SmartTradeLib.Entities.Consumer", null)
                        .WithMany("CreditCards")
                        .HasForeignKey("ConsumerEmail");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Image", b =>
                {
                    b.HasOne("SmartTradeLib.Entities.Product", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Offer", b =>
                {
                    b.HasOne("SmartTradeLib.Entities.Post", "Post")
                        .WithMany("Offers")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartTradeLib.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.PayPalInfo", b =>
                {
                    b.HasOne("SmartTradeLib.Entities.Consumer", null)
                        .WithMany("PayPalAccounts")
                        .HasForeignKey("ConsumerEmail");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Post", b =>
                {
                    b.HasOne("SmartTradeLib.Entities.Admin", null)
                        .WithMany("ValidatedPosts")
                        .HasForeignKey("AdminEmail");

                    b.HasOne("SmartTradeLib.Entities.Product", null)
                        .WithMany("Posts")
                        .HasForeignKey("ProductId");

                    b.HasOne("SmartTradeLib.Entities.Seller", "Seller")
                        .WithMany("Posts")
                        .HasForeignKey("SellerEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Product", b =>
                {
                    b.HasOne("SmartTradeLib.Entities.Admin", null)
                        .WithMany("ValidatedProducts")
                        .HasForeignKey("AdminEmail");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Consumer", b =>
                {
                    b.HasOne("SmartTradeLib.Entities.Address", "BillingAddress")
                        .WithMany()
                        .HasForeignKey("BillingAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BillingAddress");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Post", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Admin", b =>
                {
                    b.Navigation("ValidatedPosts");

                    b.Navigation("ValidatedProducts");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Consumer", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Alerts");

                    b.Navigation("BizumAccounts");

                    b.Navigation("CreditCards");

                    b.Navigation("PayPalAccounts");
                });

            modelBuilder.Entity("SmartTradeLib.Entities.Seller", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
