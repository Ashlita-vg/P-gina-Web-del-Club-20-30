using Dapper;
using Interface_Activo2030;
using Microsoft.Data.SqlClient;
using Model_Activo2030;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Activo2030
{
    public class DA_Request : IDA_Request
    {
        private readonly AppSettings appSettings;
        public DA_Request(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }
        public async Task<BaseResponse> CreateRequest(Request request)
        {
            try
            {
                using var conn = new SqlConnection(this.appSettings.ConnectionString);
                var parameters = new DynamicParameters();

                // Parámetros según el SP [activo2030].[sp_CreateSolicitudes]
                parameters.Add("@Asunto", request.Subject, DbType.String);   // o request.Asunto
                parameters.Add("@Detalle", request.Details, DbType.String);   // o request.Detalle
                parameters.Add("@TipoServicio", request.ServiceTypeId, DbType.Int32);
                parameters.Add("@EstadoSolicitud", request.StatusId, DbType.Int32);
                parameters.Add("@IdUsuario", request.User.Id, DbType.Int32);
                parameters.Add("@FechaInicio", request.StartDate, DbType.DateTime);
                parameters.Add("@FechaFin", request.EndDate, DbType.DateTime);
                parameters.Add("@jsonResult", dbType: DbType.String,
                                                  direction: ParameterDirection.Output,
                                                  size: -1);

                await conn.ExecuteAsync(
                    "activo2030.sp_CreateSolicitudes",
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
                    Error = "Ocurrió un error al crear la solicitud",
                    StatusCode = 400
                };
            }
        }


        public async Task<BaseResponse> DeleteRequest(Request request)
        {
            try
            {
                using var conn = new SqlConnection(this.appSettings.ConnectionString);
                var parameters = new DynamicParameters();

                parameters.Add("@Id", request.Id, DbType.Int32);
                parameters.Add("@jsonResult", dbType: DbType.String,
                                              direction: ParameterDirection.Output,
                                              size: -1);

                await conn.ExecuteAsync(
                    "activo2030.sp_DeleteSolicitudes",
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
                    Error = "Ocurrió un error al eliminar la solicitud",
                    StatusCode = 400
                };
            }
        }


        public async Task<BaseResponse> GetRequest()
        {
            try
            {
                using var conn = new SqlConnection(this.appSettings.ConnectionString);

                var json = await conn.QueryFirstOrDefaultAsync<string>(
                    "activo2030.sp_GetSolicitudes",
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
                    Error = "Ocurrió un error al obtener las solicitudes",
                    StatusCode = 400
                };
            }
        }


        public async Task<BaseResponse> UpdateRequest(Request request)
        {
            try
            {
                using var conn = new SqlConnection(this.appSettings.ConnectionString);
                var parameters = new DynamicParameters();

                parameters.Add("@Id", request.Id, DbType.Int32);
                parameters.Add("@Subject", request.Subject, DbType.String);
                parameters.Add("@Details", request.Details, DbType.String);
                parameters.Add("@ServiceTypeId", request.ServiceTypeId, DbType.Int32);
                parameters.Add("@StatusId", request.StatusId, DbType.Int32);
                parameters.Add("@StartDate", request.StartDate, DbType.Date);
                parameters.Add("@EndDate", request.EndDate, DbType.Date);
                parameters.Add("@UserId", request.User.Id, DbType.Int32);
                parameters.Add("@jsonResult", dbType: DbType.String,
                                                direction: ParameterDirection.Output,
                                                size: -1);

                await conn.ExecuteAsync(
                    "activo2030.sp_UpdateSolicitudes",
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
                    Error = "Ocurrió un error al actualizar la solicitud",
                    StatusCode = 400
                };
            }
        }

    }
}
