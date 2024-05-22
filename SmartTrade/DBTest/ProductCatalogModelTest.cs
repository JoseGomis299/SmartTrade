using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SmartTrade.DTOs;
using SmartTrade.Entities;
using SmartTrade.ViewModels;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartTrade.nUnitTests
{
    [TestFixture]
    public class ProductCatalogModelTest
    {
        ProductCatalogModel _model = new ProductCatalogModel();

        [SetUp]
        public void SetUp()
        {
            _serviceMock = new Mock<IYourService>();
            _catalogModel = new ProductCatalogModel
            {
                Service = _serviceMock.Object
            };
            _serviceMock.Setup(service => service.RefreshPostsAsync())
                        .ReturnsAsync(new List<SimplePostDTO>
                         {
                             new SimplePostDTO { EcologicPrint = "9", Category = "Electrónica", SellerID = "123", ProductName = "Producto 1", Title = "Titulo 1" },
                             new SimplePostDTO { EcologicPrint = "8", Category = "Electrónica", SellerID = "456", ProductName = "Producto 2", Title = "Titulo 2" }
                         });
        }

        [TestMethod]
        public void IsEcologic_ShouldReturnTrue_WhenEcologicPrintIsLessThanTen()
        {
            var post = new SimplePostDTO { EcologicPrint = "9" };
            var result = _model.IsEcologic(post);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEcologic_ShouldReturnFalse_WhenEcologicPrintIsTenOrMore()
        {
            var post = new SimplePostDTO { EcologicPrint = "10" };

            var result = _model.IsEcologic(post);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CalculateProductNameScore_ShouldReturnCorrectScore_WhenSimilarityExceedsThreshold()
        {
            var productNamePost = "Producto";
            var namePurchase = "Prod";
            var threshold = 20;
            var score = _model.CalculateProductNameScore(productNamePost, namePurchase, threshold);
            Assert.Greater(score, 0);
        }

        [TestMethod]
        public void CalculateCategoryAndSellerScore_ShouldReturnCorrectScore_WhenBothMatch()
        {
            var categoryPost = new Category("Electrónica");
            var categoryPurchase = new Category("Electrónica");
            var sellerIdPost = "123";
            var emailSellerPurchase = "123@example.com";
            var score = _model.CalculateCategoryAndSellerScore(categoryPost, categoryPurchase, sellerIdPost, emailSellerPurchase);
            Assert.AreEqual(1.00f, score);
        }
        [TestMethod]
        public void IsRelated_ShouldReturnTrue_WhenCriteriaMet()
        {
            var post = new SimplePostDTO { EcologicPrint = "9", Category = "Electrónica", SellerID = "123", ProductName = "Producto 1", Title = "Titulo 1" };
            _mockService.Setup(s => s.Purchases).Returns(EcologicPrint = "9", Category = "Electrónica", SellerID = "123", ProductName = "Producto 1", Title = "Titulo 1");
            var result = _model.IsRelated(post);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsRelated_ShouldReturnFalse_WhenCriteriaNotMet()
        {
            var post = new SimplePostDTO { EcologicPrint = "9", Category = "Electrónica", SellerID = "123", ProductName = "Producto 1", Title = "Titulo 1" };
            _mockService.Setup(s => s.Purchases).Returns(EcologicPrint = "1", Category = "Juguete", SellerID = "hola", ProductName = "hola", Title = "hola");
            var result = _model.IsRelated(post);
            Assert.IsFalse(result);
        }



    }
}