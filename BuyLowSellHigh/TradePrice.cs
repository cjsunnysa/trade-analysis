using System;
using System.Collections.Generic;
using System.Text;

namespace BuyLowSellHigh
{
    public class TradePrice
    {
        public DateTime Date { get; private set; }
        public decimal OpeningPrice { get; private set; }
        public decimal ClosingPrice { get; private set; }

        private TradePrice(DateTime date, decimal openingPrice, decimal closingPrice)
        {
            if (openingPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(openingPrice), "Opening price cannot be negative.");
            }

            if (closingPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(closingPrice), "Closing price cannot be negative.");
            }

            Date = date.Date;
            OpeningPrice = openingPrice;
            ClosingPrice = closingPrice;
        }

        public static TradePrice Create(DateTime date, decimal openingPrice, decimal closingPrice)
        {
            return new TradePrice(date, openingPrice, closingPrice);
        }

        public override int GetHashCode()
        {
            return Date.GetHashCode();
        }
    }
}
