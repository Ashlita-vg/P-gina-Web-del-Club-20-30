using Business_Logic_Activo2030;
using Interface_Activo2030;
using Model_Activo2030;

namespace Businnes_Logic_Activo2030
{
    public class BL_User : IBL_User
    {
        private readonly IDA_User _user;
        
        public BL_User(IDA_User _user)
        {
            this._user = _user;
        }
        public async Task<BaseResponse> CreateUser(User user)
        {
            try
            {   
                // Hasheamos la contraseña ANTES de guardar
                user.Password = PasswordHelper.HashPassword(user.Password);
                var response = await _user.CreateUser(user);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Error = "Ocurrio un error en el método IBL_User.CreateUser " + ex.Message,
                    StatusCode = 5000
                };
            }
        }

        public async Task<BaseResponse> DeleteUser(User user)
        {
            try
            {
                var response = await _user.DeleteUser(user);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Error = "Ocurrio un error en el método IBL_User.DeleteUser " + ex.Message,
                    StatusCode = 5000
                };
            }
        }        

        public async Task<BaseResponse> UpdateUser(User user)
        {
            try
            {
                var response = await _user.UpdateUser(user);
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Error = "Ocurrio un error en el método IBL_User.UpdateUser " + ex.Message,
                    StatusCode = 5000
                };
            }
        }

        public async Task<BaseResponse> GetUser()
        {
            try
            {
                var response = await _user.GetUser();
                return response;
            }
            catch(Exception ex)
            {
                return new BaseResponse
                {
                    Error = "Ocurrio un error en el método IBL_User.GetUser " + ex.Message,
                    StatusCode = 5000
                };
            }
        }
    }
}
