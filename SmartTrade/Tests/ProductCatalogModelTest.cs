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
        ProductCatalogModel _model = new ProductCatalogModel();

        [SetUp]
        public void SetUp()
        {

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

        }
            [Test]
            public void CalculateCategoryAndSellerScore_ShouldReturnCorrectScore_WhenBothMatch()
            {

            }
            [Test]
            public void IsRelated_ShouldReturnTrue_WhenCriteriaMet()
            {

            }

            [Test]
            public void IsRelated_ShouldReturnFalse_WhenCriteriaNotMet()
            {

            }
    }
}
