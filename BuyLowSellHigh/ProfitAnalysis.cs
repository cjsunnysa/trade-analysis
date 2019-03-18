using System;
using System.Collections.Generic;
using System.Text;

namespace BuyLowSellHigh
{
    public class ProfitAnalysis
    {
        public DateTime PurchaseDate { get; private set; }
        public DateTime SaleDate { get; private set; }
        public decimal PurchasePrice { get; private set; }
        public decimal SalePrice { get; private set; }
        public decimal Profit => SalePrice - PurchasePrice;

        public ProfitAnalysis(DateTime purchaseDate, DateTime saleDate, decimal purchasePrice, decimal salePrice)
        {
            if (purchasePrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(purchasePrice), "Cannot be less than 0.");
            }

            if (salePrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(salePrice), "Cannot be less than 0.");
            }

            PurchaseDate = purchaseDate.Date;
            SaleDate = saleDate.Date;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
        }

        public override bool Equals(object o)
        {
            var obj = o as ProfitAnalysis;

            if (obj == null)
                return base.Equals(o);

            return
                PurchaseDate.Equals(obj.PurchaseDate)
                && SaleDate.Equals(obj.SaleDate)
                && Profit.Equals(obj.Profit);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
