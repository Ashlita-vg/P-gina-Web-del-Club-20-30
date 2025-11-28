using Interface_Activo2030;
using Microsoft.AspNetCore.Mvc;
using Model_Activo2030;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller_Activo2030
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly AppSettings appSettings;
        private readonly IBL_Request _request;

        public RequestController(AppSettings appSettings, IBL_Request _request)
        {
            this.appSettings = appSettings;
            this._request = _request;
        }

        [HttpPost]
        [Route("SelectRequest")]
        public async Task<IActionResult> SelectRequest(Request? request)
        {
            try
            {
                BaseResponse req = await _request.GetRequest();

                if (req.StatusCode != 200)
                {
                    return BadRequest(req);
                }

                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var requests = System.Text.Json.JsonSerializer.Deserialize<List<Request>>(req.Result, options);
                return Ok(requests);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateRequest")]
        public async Task<IActionResult> CreateRequest(Request? request)
        {
            try
            {
                BaseResponse req = await _request.CreateRequest(request);


                if (req.StatusCode != 200)
                {
                    return BadRequest(req);
                }


                return Ok(req);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateRequest")]
        public async Task<IActionResult> UpdateRequest(Request? request)
        {
            try
            {
                BaseResponse req = await _request.UpdateRequest(request);

                if (req.StatusCode != 200)
                {
                    return BadRequest(req);
                }

                return Ok(req);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteRequest")]
        public async Task<IActionResult> DeleteRequest(Request? request)
        {
            try
            {
                BaseResponse req = await _request.DeleteRequest(request);

                if (req.StatusCode != 200)
                {
                    return BadRequest(req);
                }

                return Ok(req);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
