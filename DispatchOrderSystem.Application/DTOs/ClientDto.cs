using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.DTOs
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
