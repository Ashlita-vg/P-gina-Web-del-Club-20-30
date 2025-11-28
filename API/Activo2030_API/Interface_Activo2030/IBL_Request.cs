using Model_Activo2030;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Activo2030
{
    public interface IBL_Request
    {
        public Task<BaseResponse> GetRequest();
        public Task<BaseResponse> CreateRequest(Request request);
        public Task<BaseResponse> UpdateRequest(Request request);
        public Task<BaseResponse> DeleteRequest(Request request);
    }
}
