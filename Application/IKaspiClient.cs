using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractProduct
{
    public interface IKaspiClient
    {
        [Get("/shop/rest/misc/product/mobile/specifications?productCode={code}")]
        Task<HttpResponseMessage> GetProduct(string code);
    }
}
