using System;
using System.Collections.Generic;
using System.Linq;

namespace BuyLowSellHigh
{
    public class StockHistory
    {
        private readonly HashSet<TradePrice> _prices;

        public StockHistory(HashSet<TradePrice> prices)
        {
            if (prices == null)
            {
                throw new ArgumentNullException(nameof(prices), "Cannot be null.");
            }

            if (prices.Count == 0)
            {
                throw new ArgumentException(nameof(prices), "Cannot be empty.");
            }

            _prices = prices;
        }

        public ProfitAnalysis GetOptimalProfitPeriod()
        {
            var dateMaxProfitRange = new Dictionary<DateTime, TradePrice[]>();

            for (var i = 0; i < _prices.Count; i++)
            {
                var fromPrice = _prices.ElementAt(i);
                var dateMaxProfit = dateMaxProfitRange[fromPrice.Date] = new[] { fromPrice, fromPrice };

                for (var j = i + 1; j < _prices.Count; j++)
                {
                    var toPrice = _prices.ElementAt(j);

                    if (GetProfit(fromPrice, toPrice) > 
                        GetProfit(dateMaxProfit[0], dateMaxProfit[1]))
                    {
                        dateMaxProfit[1] = toPrice;
                    }
                }
            }

            var highestProfit =
                dateMaxProfitRange
                    .Aggregate((max, kvp) =>
                        GetProfit(kvp) > GetProfit(max)
                        ? kvp
                        : max
                    );

            return new ProfitAnalysis(
                purchaseDate:   highestProfit.Value[0].Date, 
                saleDate:       highestProfit.Value[1].Date,
                purchasePrice:  highestProfit.Value[0].OpeningPrice,
                salePrice:      highestProfit.Value[1].ClosingPrice);
        }

        private static decimal GetProfit(KeyValuePair<DateTime, TradePrice[]> kvp)
        {
            return GetProfit(kvp.Value[0], kvp.Value[1]);
        }

        private static decimal GetProfit(TradePrice opening, TradePrice closing)
        {
            return closing.ClosingPrice - opening.OpeningPrice;
        }
    }
}
