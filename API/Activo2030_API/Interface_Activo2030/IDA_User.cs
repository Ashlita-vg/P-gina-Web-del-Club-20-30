using Model_Activo2030;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Activo2030
{
    public interface IDA_User
    {
        public Task<BaseResponse> GetUser();
        public Task<BaseResponse> CreateUser(User user);
        public Task<BaseResponse> UpdateUser(User user);
        public Task<BaseResponse> DeleteUser(User user);
    }
}
