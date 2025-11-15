using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Activo2030
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }

        public string? Result { get; set; }

        public string? Error { get; set; }
    }
}
