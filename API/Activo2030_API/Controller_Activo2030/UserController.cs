using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Model_Activo2030;
using System.Data;

namespace Controller_Activo2030
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly AppSettings appSettings;

        public UserController(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        [HttpGet]
        [Route("SelectUser")]
        public async Task<IActionResult> SelectUser(string username)
        {
            try
            {
                BaseResponse req = await getSomething();

                if(req.StatusCode != 200)
                {
                    return BadRequest(req);
                }

                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var requests = System.Text.Json.JsonSerializer.Deserialize<List<User>>(req.Result, options);
                return Ok(requests);

            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }  
        }


        private async Task<BaseResponse> getSomething()
        {
            try
            {
                using var conn = new SqlConnection(this.appSettings.ConnectionString);

                var json = await conn.QueryFirstOrDefaultAsync<string>(
                    "activo2030.sp_GetUser",
                    commandType: CommandType.StoredProcedure
                );                

                if (json is null)
                {
                    return new BaseResponse
                    {
                        Error = "No se encontraron datos",
                        StatusCode = 400
                    };
                }                

                return new BaseResponse
                {
                    StatusCode = 200,
                    Result = json,
                    Error = "0"
                };
            }
            catch
            {
                return new BaseResponse
                {
                    Error = "Ocurrio un error al obtener los datos",
                    StatusCode = 400
                };
            }

            return new BaseResponse
            {
                Error = "Ocurrio un error en el método getSomething",
                StatusCode = 400
            };

        }
    }
}
