using Dapper;
using Interface_Activo2030;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Model_Activo2030;
using Newtonsoft.Json;
using System.Data;
using System.Numerics;

namespace Data_Access_Activo2030
{
    public class DA_User : IDA_User
    {
        private readonly AppSettings appSettings;
        public DA_User(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }
        public async Task<BaseResponse> CreateUser(User user)
        {
            try
            {
                using var conn = new SqlConnection(this.appSettings.ConnectionString);
                var parameters = new DynamicParameters();

                parameters.Add("@Name", user.Name, DbType.String);
                parameters.Add("@SurName1", user.SurName1, DbType.String);
                parameters.Add("@SurName2", user.SurName2, DbType.String);
                parameters.Add("@Email", user.Email, DbType.String);
                parameters.Add("@Password", user.Password, DbType.String);
                parameters.Add("@Phone", user.Phone, DbType.String);
                parameters.Add("@UserTypeId", user.UserTypeId, DbType.Int32);
                parameters.Add("@jsonResult", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

                await conn.ExecuteAsync(
                    "activo2030.sp_CreateUser",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var json = parameters.Get<string>("@jsonResult");

                if (json is null)
                {
                    return new BaseResponse
                    {
                        Error = "No se encontraron datos",
                        StatusCode = 400
                    };
                }

                var spResult = JsonConvert.DeserializeObject<BaseResponse>(json);

                return spResult;
                
            }
            catch
            {
                return new BaseResponse
                {
                    Error = "Ocurrio un error al crear el usuario",
                    StatusCode = 400
                };
            }
        }

        public async Task<BaseResponse> DeleteUser(User user)
        {
            try
            {
                using var conn = new SqlConnection(this.appSettings.ConnectionString);
                var parameters = new DynamicParameters();

                parameters.Add("@Id", user.Id, DbType.Int64);
                parameters.Add("@jsonResult", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

                await conn.ExecuteAsync(
                    "activo2030.sp_DeleteUser",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var json = parameters.Get<string>("@jsonResult");

                if (json is null)
                {
                    return new BaseResponse
                    {
                        Error = "Ocurrio un error al eliminar el usuario",
                        StatusCode = 400
                    };
                }

                var spResult = JsonConvert.DeserializeObject<BaseResponse>(json);

                return spResult;
            }
            catch
            {
                return new BaseResponse
                {
                    Error = "Ocurrio un error al obtener los datos",
                    StatusCode = 400
                };
            }
        }

        public async Task<BaseResponse> GetUser()
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
            
        }

        public async Task<BaseResponse> UpdateUser(User user)
        {
            try
            {
                using var conn = new SqlConnection(this.appSettings.ConnectionString);
                var parameters = new DynamicParameters();

                parameters.Add("@Id", user.Id, DbType.Int32);
                parameters.Add("@Name", user.Name, DbType.String);
                parameters.Add("@SurName1", user.SurName1, DbType.String);
                parameters.Add("@SurName2", user.SurName2, DbType.String);
                parameters.Add("@Email", user.Email, DbType.String);                
                parameters.Add("@Phone", user.Phone, DbType.String);
                parameters.Add("@UserTypeId", user.UserTypeId, DbType.Int32);
                parameters.Add("@jsonResult", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

                await conn.ExecuteAsync(
                    "activo2030.sp_UpdateUser",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var json = parameters.Get<string>("@jsonResult");

                if (json is null)
                {
                    return new BaseResponse
                    {
                        Error = "No se encontraron datos",
                        StatusCode = 400
                    };
                }

                var spResult = JsonConvert.DeserializeObject<BaseResponse>(json);

                return spResult;
            }
            catch
            {
                return new BaseResponse
                {
                    Error = "Ocurrio un error al actualizar el usuario",
                    StatusCode = 400
                };
            }
        }
    }
}
