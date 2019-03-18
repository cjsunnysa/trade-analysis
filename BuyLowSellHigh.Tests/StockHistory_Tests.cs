using System;
using System.Collections.Generic;
using System.Linq;
using BuyLowSellHigh;
using NUnit.Framework;

namespace Tests
{
    public class StockHistory_Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_ShouldTakeHashSetOfTradePrices()
        {
            var prices = CreatePriceSet();

            Assert.DoesNotThrow(() =>
                new StockHistory(prices)
            );
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionIfHashSetNull()
        {
            Assert.Catch(typeof(ArgumentNullException), () =>
                new StockHistory(null)
            );
        }

        [Test]
        public void Constructor_ShouldThrowArgumentExceptionIfHashSetEmpty()
        {
            Assert.Catch(typeof(ArgumentException), () =>
                new StockHistory(new HashSet<TradePrice>())
            );
        }

        [Test]
        public void GetOptimalProfitPeriod_ShouldPriceValuesWhenTradeHistoryOfOne()
        {
            var price = TradePrice.Create(new DateTime(2010, 01, 01), 56, 93);
            var set = new HashSet<TradePrice> { price };

            var stockHistory = new StockHistory(set);
            var optimalProfit = stockHistory.GetOptimalProfitPeriod();

            Assert.IsTrue(ValuesEqual(price, optimalProfit));
        }

        [Test]
        public void GetOptimalProfitPeriod_ShouldReturnSecondDetailsWhenSecondHistoryItemHigherProfit()
        {
            var prices = new TradePrice[]
            {
                TradePrice.Create(new DateTime(2010, 01, 01), 35, 30),
                TradePrice.Create(new DateTime(2010, 01, 02), 30, 35)
            };

            var optimalProfit = GetOptimalProfit(prices);

            Assert.IsTrue(ValuesEqual(prices.ElementAt(1), optimalProfit));
        }

        [Test]
        public void GetOptimalProfitPeriod_ShouldReturnFirstOpeningSecondClosingDetails()
        {
            var prices = new TradePrice[]
            {
                TradePrice.Create(new DateTime(2010, 01, 01), 30, 33),
                TradePrice.Create(new DateTime(2010, 01, 02), 33, 36)
            };

            var optimalProfit = GetOptimalProfit(prices);

            var correctProfit = new ProfitAnalysis(
                purchaseDate: prices[0].Date,
                saleDate: prices[1].Date,
                purchasePrice: prices[0].OpeningPrice,
                salePrice: prices[1].ClosingPrice
            );

            Assert.AreEqual(correctProfit, optimalProfit);
        }

        [Test]
        public void GetOptimalProfitPeriod_ShouldReturnCorrectRange()
        {
            var prices = CreatePriceSet();

            var optimalProfit = GetOptimalProfit(prices.ToArray());

            var correctProfit = new ProfitAnalysis(
                purchaseDate: prices.ElementAt(2).Date,
                saleDate: prices.ElementAt(5).Date,
                purchasePrice: prices.ElementAt(2).OpeningPrice,
                salePrice: prices.ElementAt(5).ClosingPrice
            );

            Assert.AreEqual(correctProfit, optimalProfit);
        }

        private static ProfitAnalysis GetOptimalProfit(params TradePrice[] prices)
        {
            var set = new HashSet<TradePrice>(prices);

            var stockHistory = new StockHistory(set);
            return stockHistory.GetOptimalProfitPeriod();
        }

        private bool ValuesEqual(TradePrice price, ProfitAnalysis optimalProfit)
        {
            return 
                price.Date.Equals(optimalProfit.PurchaseDate)
                && price.Date.Equals(optimalProfit.SaleDate)
                && price.OpeningPrice.Equals(optimalProfit.PurchasePrice)
                && price.ClosingPrice.Equals(optimalProfit.SalePrice);
        }

        private HashSet<TradePrice> CreatePriceSet()
        {
            return new HashSet<TradePrice>
            {
                TradePrice.Create(new DateTime(2009, 03, 05), 117, 112),
                TradePrice.Create(new DateTime(2009, 03, 06), 112, 110),
                TradePrice.Create(new DateTime(2009, 03, 07), 110, 122),
                TradePrice.Create(new DateTime(2009, 03, 08), 122, 129),
                TradePrice.Create(new DateTime(2009, 03, 09), 129, 124),
                TradePrice.Create(new DateTime(2009, 03, 10), 124, 134),
                TradePrice.Create(new DateTime(2009, 03, 11), 134, 133),
                TradePrice.Create(new DateTime(2009, 03, 12), 133, 132),
                TradePrice.Create(new DateTime(2009, 03, 13), 132, 131),
                TradePrice.Create(new DateTime(2009, 03, 14), 131, 130)
            };
        }
    }
}