using SmartTrade.Entities;
using SmartTrade.ViewModels;
using SmartTradeDTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SmartTrade.Services;
using System.Reflection;

namespace Tests
{

    [TestFixture]
    public class ProductCatalogModelTest
    {
        ProductCatalogModel _model;
        private Mock<SmartTradeService> _serviceMock;

        [SetUp]
        public void SetUp()
        {
            _model = new ProductCatalogModel();
            SmartTradeService.Instance.InitializeCacheAsync().Wait();
            Environment.SetEnvironmentVariable("TEST_MODE", "true");
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
                    ProductName = "Producto Ecológico",
                    Title = "Producto Ecológico"
                }),
                new ProductModel(new SimplePostDTO
                {
                    Category = Category.Clothing,
                    EcologicPrint = "10",
                    SellerID = "456",
                    ProductName = "Ropa Regular",
                    Title = "Ropa Regular"
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
                new ProductModel(new SimplePostDTO { Id = 1, Category = Category.Nutrition, ProductName = "Apple", Title = "Apple", EcologicPrint = "5", SellerID = "seller@example.com"}),
                new ProductModel(new SimplePostDTO { Id = 2, Category = Category.Toy, ProductName = "Toy", Title = "Toy", EcologicPrint = "15", SellerID = "seller@example.com" }),
                new ProductModel(new SimplePostDTO { Id = 3, Category = Category.Book, ProductName = "Banana", Title = "Banana", EcologicPrint = "3", SellerID = "seller@example.com" }),
                new ProductModel(new SimplePostDTO { Id = 4, Category = Category.Clothing, ProductName = "T-Shirt", Title = "T-Shirt", EcologicPrint = "8", SellerID = "seller@example.com" })
            };

            _model.SortByCategory(null);

            var otherProducts = _model.OtherProducts;
            var recommendedProducts = _model.RecommendedProducts;
            var relatedProducts = _model.RelatedProducts;

            Assert.That(otherProducts.Count + recommendedProducts.Count + relatedProducts.Count, Is.EqualTo(_model.OriginalProducts.Count));
            Assert.That(recommendedProducts, Is.EquivalentTo(_model.OriginalProducts.Where(p => _model.IsEcologic(p.Post))));
        }

        [Test]
        public void SortByCategory_ShouldReturnProductsOfSpecificCategory_WhenCategoryIsProvided()
        {
            _model.OriginalProducts = new List<ProductModel>
            {
                new ProductModel(new SimplePostDTO { Id = 1, Category = Category.Nutrition, ProductName = "Apple", Title = "Apple", EcologicPrint = "5" , SellerID = "seller@example.com"}),
                new ProductModel(new SimplePostDTO { Id = 2, Category = Category.Toy, ProductName = "Toy", Title = "Toy", EcologicPrint = "15" , SellerID = "seller@example.com"}),
                new ProductModel(new SimplePostDTO { Id = 3, Category = Category.Book, ProductName = "Banana", Title = "Banana", EcologicPrint = "3" , SellerID = "seller@example.com"}),
                new ProductModel(new SimplePostDTO { Id = 4, Category = Category.Clothing, ProductName = "T-Shirt", Title = "T-Shirt", EcologicPrint = "8" , SellerID = "seller@example.com"})
            };

            int categoryId = (int)Category.Nutrition;
            _model.SortByCategory(categoryId);

            var filteredProducts = _model.OriginalProducts.Where(p => p.Post.Category == Category.Nutrition).ToList();

            var otherProducts = _model.OtherProducts;
            var recommendedProducts = _model.RecommendedProducts;
            var relatedProducts = _model.RelatedProducts;

            Assert.That(otherProducts.Count + recommendedProducts.Count + relatedProducts.Count, Is.EqualTo(filteredProducts.Count));
            Assert.That(recommendedProducts, Is.EquivalentTo(filteredProducts.Where(p => _model.IsEcologic(p.Post))));
        }

        [Test]
        public void SortByCategory_ShouldReturnEmptyLists_WhenNoProductMatchesCategory()
        {
            _model.OriginalProducts = new List<ProductModel>
            {
                new ProductModel(new SimplePostDTO { Id = 1, Category = Category.Nutrition, ProductName = "Apple", Title = "Apple", EcologicPrint = "5", SellerID = "seller@example.com" }),
                new ProductModel(new SimplePostDTO { Id = 2, Category = Category.Toy, ProductName = "Toy", Title = "Toy", EcologicPrint = "15", SellerID = "seller@example.com" }),
                new ProductModel(new SimplePostDTO { Id = 3, Category = Category.Book, ProductName = "Banana", Title = "Banana", EcologicPrint = "3", SellerID = "seller@example.com" }),
                new ProductModel(new SimplePostDTO { Id = 4, Category = Category.Clothing, ProductName = "T-Shirt", Title = "T-Shirt", EcologicPrint = "8", SellerID = "seller@example.com" })
            };

            int categoryId = (int)Category.Toy;
            _model.SortByCategory(categoryId);

            Assert.That(_model.OtherProducts, Is.Not.Empty);
            Assert.That(_model.RecommendedProducts, Is.Empty);
            Assert.That(_model.RelatedProducts, Is.Empty);
        }

        [Test]
        public void Filtering_ShouldReturnProductsFilteredByCategory()
        {
            _model.OriginalProducts = new List<ProductModel>
            {
                new ProductModel(new SimplePostDTO { Id = 1, Category = Category.Nutrition }),
                new ProductModel(new SimplePostDTO { Id = 2, Category = Category.Toy }),
                new ProductModel(new SimplePostDTO { Id = 3, Category = Category.Nutrition }),
                new ProductModel(new SimplePostDTO { Id = 4, Category = Category.Book })
            };

            Category categoryToFilter = Category.Nutrition;

            List<ProductModel> filteredProducts = _model.Filtering(categoryToFilter);

            Assert.That(filteredProducts, Is.Not.Null);

            foreach (var product in filteredProducts)
            {
                Assert.That(product.Post.Category, Is.EqualTo(categoryToFilter));
            }

            Assert.That(filteredProducts.Count, Is.EqualTo(2));
        }
        [Test]
        public void IsRelated_ShouldReturnTrue_WhenPostIsRelatedToPurchase()
        {
            var post = new SimplePostDTO
            {
                Id = 1,
                Category = Category.Nutrition,
                ProductName = "Apple",
                SellerID = "123",
                Title = "Fresh Apples"
            };

            var purchase = new PurchaseDTO
            {
                Post = new PostDTO { Id = 2, Category = Category.Nutrition, Title = "Fresh Apples", ProductName = "Apple" },
                EmailSeller = "123"
            };

            var postToCompare = new SimplePostDTO
            {
                Id = 2,
                Category = Category.Nutrition,
                ProductName = "Apple",
                Title = "Fresh Apples",
                SellerID = "123"
            };

            SmartTradeService.Instance.AddPurchaseTest(new List<PurchaseDTO>(){purchase});
            SmartTradeService.Instance.AddPostTest(new List<SimplePostDTO>(){postToCompare});
            bool isRelated = _model.IsRelated(post);

            Assert.That(isRelated,Is.True);
        }

        [Test]
        public void IsRelated_ShouldReturnFalse_WhenPostIsNotRelatedToPurchase()
        {
            var post = new SimplePostDTO
            {
                Id = 1,
                Category = Category.Nutrition,
                ProductName = "Apple",
                SellerID = "123",
                Title = "Fresh Apples"
            };

            var purchase = new PurchaseDTO
            {
                Post = new PostDTO { Id = 2, Category = Category.Clothing, ProductName = "T-Shirt",
                    Title = "T-Shirt",},
                EmailSeller = "seller@example.com"
            };

            var postToCompare = new SimplePostDTO
            {
                Id = 2,
                Category = Category.Clothing,
                ProductName = "T-Shirt",
                Title = "T-Shirt",
                SellerID = "seller@example.com"
            };
            SmartTradeService.Instance.AddPurchaseTest(new List<PurchaseDTO>() { purchase });
            SmartTradeService.Instance.AddPostTest(new List<SimplePostDTO>() { postToCompare });
            bool isRelated = _model.IsRelated(post);

            Assert.That(isRelated,Is.Not.True);
        }
        [Test]
        public async Task LoadProductsAsync_ShouldAddProductsToCollections()
        {
            // Arrange
            var posts = new List<SimplePostDTO>
            {
                new SimplePostDTO { Id = 1, Category = Category.Nutrition, ProductName = "Apple", EcologicPrint = "5" },
                new SimplePostDTO { Id = 2, Category = Category.Clothing, ProductName = "T-Shirt", EcologicPrint = "28"}
            };

            SmartTradeService.Instance.AddPostTest(posts);
            // Act
            _model.LoadProducts(posts);
            // Assert
            Assert.That(_model.OriginalProducts.Count, Is.EqualTo(posts.Count));
            Assert.That(_model.OtherProducts.Count, Is.EqualTo(1));
            Assert.That(_model.RecommendedProducts.Count, Is.EqualTo(1));
            Assert.That(_model.RelatedProducts.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task LoadProductsAsync_ShouldAddRelatedProducts_WhenUserIsLoggedIn()
        {
            SetUp();
            await SmartTradeService.Instance.LogInAsync("c0@gmail.com", "123");

            await _model.LoadProductsAsync();
            Assert.That(_model.RelatedProducts.Count, Is.GreaterThan(0));
        }
    }
}



