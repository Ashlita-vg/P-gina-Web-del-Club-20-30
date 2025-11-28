using Interface_Activo2030;
using Model_Activo2030;

namespace Business_Logic_Activo2030
{
    public class BL_Request : IBL_Request
    {
        private readonly IDA_Request _request;
        public BL_Request(IDA_Request _request)
        {
            this._request = _request;
        }
        public async Task<BaseResponse> CreateRequest(Request request)
        {
            try
            {
                BaseResponse response = await _request.CreateRequest(request);

                return response;

            }
            catch (Exception ex) {

                return new BaseResponse
                {
                    Error = "Ocurrio un error en el método IBL_Request.CreateRequest " + ex.Message,
                    StatusCode = 5000
                };
            }
        }

        public async Task<BaseResponse> DeleteRequest(Request request)
        {
            try
            {
                BaseResponse response = await _request.DeleteRequest(request);

                return response;

            }
            catch (Exception ex)
            {

                return new BaseResponse
                {
                    Error = "Ocurrio un error en el método IBL_Request.DeleteRequest " + ex.Message,
                    StatusCode = 5000
                };
            }
        }

        public async Task<BaseResponse> GetRequest()
        {
            try
            {
                BaseResponse response = await _request.GetRequest();

                return response;

            }
            catch (Exception ex)
            {

                return new BaseResponse
                {
                    Error = "Ocurrio un error en el método IBL_Request.GetRequest " + ex.Message,
                    StatusCode = 5000
                };
            }
        }

        public async Task<BaseResponse> UpdateRequest(Request request)
        {
            try
            {
                BaseResponse response = await _request.UpdateRequest(request);

                return response;

            }
            catch (Exception ex)
            {

                return new BaseResponse
                {
                    Error = "Ocurrio un error en el método IBL_Request.UpdateRequest " + ex.Message,
                    StatusCode = 5000
                };
            }
        }
    }
}
