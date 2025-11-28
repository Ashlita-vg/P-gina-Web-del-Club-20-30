using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Activo2030
{
    public class Request
    {
        public int? Id { get; set; }
        
        public string? Subject { get; set; }
        
        public string? Details { get; set; }
        
        public int? ServiceTypeId { get; set; }
        
        public int? StatusId { get; set; }

        public User? User { get; set; }

        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
    }

    public class RequestDto
    {
        public int? Id { get; set; }

        public string? Subject { get; set; }

        public string? Details { get; set; }

        public int? ServiceTypeId { get; set; }

        public int? StatusId { get; set; }
        
        public List<User>? User { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

}
