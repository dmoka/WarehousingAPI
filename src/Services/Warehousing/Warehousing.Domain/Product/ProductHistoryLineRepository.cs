using System;
using System.Collections.Generic;
using System.Text;

namespace Warehousing.Domain.Product
{
    public interface IProductHistoryLineRepository
    {
         void Insert(ProductHistoryLine product);
    }
}
