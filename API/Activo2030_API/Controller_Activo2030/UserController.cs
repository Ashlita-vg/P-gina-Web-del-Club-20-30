using Dapper;
using Interface_Activo2030;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Model_Activo2030;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json;

namespace Controller_Activo2030
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly AppSettings appSettings;
        private readonly IBL_User _user;

        public UserController(AppSettings appSettings, IBL_User _user)
        {
            this.appSettings = appSettings;
            this._user = _user;
        }

        [HttpGet]
        [Route("SelectUser")]
        public async Task<IActionResult> SelectUser(string? username)
        {
            try
            {
                BaseResponse req = await _user.GetUser();

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

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(User? user)
        {
            try
            {
                BaseResponse req = await _user.CreateUser(user);
                

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
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User? user)
        {
            try
            {
                BaseResponse req = await _user.UpdateUser(user);

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
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(User? user)
        {
            try
            {
                BaseResponse req = await _user.DeleteUser(user);

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
