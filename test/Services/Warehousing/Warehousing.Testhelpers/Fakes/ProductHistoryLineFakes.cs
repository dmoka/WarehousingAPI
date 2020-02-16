using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.Product;

namespace Warehousing.Testhelpers.Fakes
{
    public class ProductHistoryLineFakes
    {
        public static ProductHistoryLine ProductHistoryLineWithPick() => ProductHistoryLineWithPick(Guid.NewGuid());
        public static ProductHistoryLine ProductHistoryLineWithPick(Guid productId) => new ProductHistoryLine(productId, 3, ProductHistoryType.Pick, DateTime.Now);
        
        public static ProductHistoryLine ProductHistoryLineWithUnpick() => ProductHistoryLineWithUnpick(Guid.NewGuid());
        public static ProductHistoryLine ProductHistoryLineWithUnpick(Guid productId) => new ProductHistoryLine(productId, 4, ProductHistoryType.Unpick, DateTime.Now);
    }
}
