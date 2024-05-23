using SmartTrade.Entities;
using SmartTrade.ViewModels;
using SmartTradeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{

    [TestFixture]
    public class ProductCatalogModelTest
    {
        ProductCatalogModel _model;

        [SetUp]
        public void SetUp()
        {
            _model = new ProductCatalogModel();

        }

        [Test]
        public void IsEcologic_ShouldReturnTrue_WhenEcologicPrintIsLessThanTen()
        {
            var post = new SimplePostDTO { EcologicPrint = "9" };
            var result = _model.IsEcologic(post);
            Assert.That(result,Is.True);
        }

        [Test]
        public void IsEcologic_ShouldReturnFalse_WhenEcologicPrintIsTenOrMore()
        {
            var post = new SimplePostDTO { EcologicPrint = "10" };

            var result = _model.IsEcologic(post);

            Assert.That(result,Is.False);
        }

        [Test]
         public void CalculateProductNameScore_ShouldReturnCorrectScore_WhenSimilarityExceedsThreshold()
         {
            string nombreProductoPublicado = "Ejemplo Producto";
            string nombreCompra = "Ejemplo Producto";
            int umbral = 90;
            float resultado = _model.CalculateProductNameScore(nombreProductoPublicado, nombreCompra, umbral);

            Assert.That(resultado, Is.GreaterThan(0), "El resultado debería ser mayor que 0");
            Assert.That(resultado, Is.EqualTo(1), "El resultado debería ser 1"); 
        }

        [Test]
        public void TestCalculateCategoryAndSellerScore_CategoriasYNoVendedoresCoinciden()
        {
            Category categoriaPublicada = Category.Nutrition;
            Category categoriaCompra = Category.Nutrition;
            string idVendedorPublicado = "123";
            string correoElectronicoVendedorCompra = "123@example.com"; 

            float resultado = _model.CalculateCategoryAndSellerScore(categoriaPublicada, categoriaCompra, idVendedorPublicado, correoElectronicoVendedorCompra);

            Assert.That(resultado, Is.EqualTo(0.85f), "Las categorías y vendedores deben coincidir completamente.");
        }

        [Test]
        public void TestCalculateCategoryAndSellerScore_CategoriasNoCoinciden()
        {

            Category categoriaPublicada = Category.Nutrition;
            Category categoriaCompra = Category.Book; 

            float resultado = _model.CalculateCategoryAndSellerScore(categoriaPublicada, categoriaCompra, "", "");

            Assert.That(resultado, Is.EqualTo(0.15f), "Las categorías no deben coincidir.");
        }

        [Test]
        public void TestCalculateCategoryAndSellerScore_CoincideTodo()
        {
            Category categoriaPublicada = Category.Nutrition;
            Category categoriaCompra = Category.Nutrition; 
            string idVendedorPublicado = "456@example.com";

            float resultado = _model.CalculateCategoryAndSellerScore(categoriaPublicada, categoriaCompra, idVendedorPublicado, idVendedorPublicado);

            Assert.That(resultado, Is.GreaterThan(0.85f), "Solo la categoría debe coincidir.");
        }
        [Test]
        public void TestUpdateProducts_EcoFriendlyProductsAsRecommended()
        {
            var products = new List<ProductModel>
        {
        new ProductModel(new SimplePostDTO
        {
            Category = Category.Nutrition,
            EcologicPrint = "9",
            SellerID = "123",
            ProductName = "Producto Ecológico"
        }),
        new ProductModel(new SimplePostDTO
        {
            Category = Category.Clothing,
            EcologicPrint = "10",
            SellerID = "456",
            ProductName = "Ropa Regular"
        })
        };

            
             _model.UpdateProducts(products, null);

            Assert.That(_model.RecommendedProducts.Count,Is.EqualTo(1));
            Assert.That(_model.OtherProducts.Count, Is.EqualTo(1));
            Assert.That( _model.RelatedProducts.Count, Is.EqualTo(0));
        }
        [Test]
        public void SortByCategory_ShouldReturnAllProducts_WhenCategoryIsNull()
        {
            _model.OriginalProducts = new List<ProductModel>
            {
                new ProductModel(new SimplePostDTO { Id = 1, Category = Category.Nutrition, ProductName = "Apple", EcologicPrint = "5" }),
                new ProductModel(new SimplePostDTO { Id = 2, Category = Category.Toy, ProductName = "Toy", EcologicPrint = "15" }),
                new ProductModel(new SimplePostDTO { Id = 3, Category = Category.Book, ProductName = "Banana", EcologicPrint = "3" }),
                new ProductModel(new SimplePostDTO { Id = 4, Category = Category.Clothing, ProductName = "T-Shirt", EcologicPrint = "8" })
            };

            _model.SortByCategory(null);

            var otherProducts = _model.OtherProducts;
            var recommendedProducts = _model.RecommendedProducts;
            var relatedProducts = _model.RelatedProducts;

            Assert.That(otherProducts.Count + recommendedProducts.Count + relatedProducts.Count, Is.EqualTo(_model.OriginalProducts.Count));
            Assert.That(recommendedProducts, Is.EquivalentTo(_model.OriginalProducts.Where(p => _model.IsEcologic(p.Post))));
            Assert.That(relatedProducts, Is.EquivalentTo(_model.OriginalProducts.Where(p => _model.IsRelated(p.Post))));
        }

        [Test]
        public void SortByCategory_ShouldReturnProductsOfSpecificCategory_WhenCategoryIsProvided()
        {
            _model.OriginalProducts = new List<ProductModel>
            {
                new ProductModel(new SimplePostDTO { Id = 1, Category = Category.Nutrition, ProductName = "Apple", EcologicPrint = "5" }),
                new ProductModel(new SimplePostDTO { Id = 2, Category = Category.Toy, ProductName = "Toy", EcologicPrint = "15" }),
                new ProductModel(new SimplePostDTO { Id = 3, Category = Category.Book, ProductName = "Banana", EcologicPrint = "3" }),
                new ProductModel(new SimplePostDTO { Id = 4, Category = Category.Clothing, ProductName = "T-Shirt", EcologicPrint = "8" })
            };

            int categoryId = (int)Category.Nutrition;
            _model.SortByCategory(categoryId);

            var filteredProducts = _model.OriginalProducts.Where(p => p.Post.Category == Category.Nutrition).ToList();

            var otherProducts = _model.OtherProducts;
            var recommendedProducts = _model.RecommendedProducts;
            var relatedProducts = _model.RelatedProducts;

            Assert.That(otherProducts.Count + recommendedProducts.Count + relatedProducts.Count, Is.EqualTo(filteredProducts.Count));
            Assert.That(recommendedProducts, Is.EquivalentTo(filteredProducts.Where(p => _model.IsEcologic(p.Post))));
            Assert.That(relatedProducts, Is.EquivalentTo(filteredProducts.Where(p => _model.IsRelated(p.Post))));
        }

        [Test]
        public void SortByCategory_ShouldReturnEmptyLists_WhenNoProductMatchesCategory()
        {
            _model.OriginalProducts = new List<ProductModel>
            {
                new ProductModel(new SimplePostDTO { Id = 1, Category = Category.Nutrition, ProductName = "Apple", EcologicPrint = "5" }),
                new ProductModel(new SimplePostDTO { Id = 2, Category = Category.Toy, ProductName = "Toy", EcologicPrint = "15" }),
                new ProductModel(new SimplePostDTO { Id = 3, Category = Category.Book, ProductName = "Banana", EcologicPrint = "3" }),
                new ProductModel(new SimplePostDTO { Id = 4, Category = Category.Clothing, ProductName = "T-Shirt", EcologicPrint = "8" })
            };

            int categoryId = (int)Category.Toy; // Assuming this category does not exist in the products list
            _model.SortByCategory(categoryId);

            Assert.That(_model.OtherProducts, Is.Not.Empty);
            Assert.That(_model.RecommendedProducts, Is.Empty);
            Assert.That(_model.RelatedProducts, Is.Empty);
        }

    }
}
