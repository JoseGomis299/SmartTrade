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

namespace SmartTrade.Tests
{
    [TestFixture]
    public class ProductCatalogModelTest
    {
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
       

    }
}