using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public interface IProductSuggestRepository:IDisposable
    {
        void Add(ProductSuggest productSuggest);
        void Update(ProductSuggest productSuggest);
        IList<ProductSuggest> GetAll();
    }
}
