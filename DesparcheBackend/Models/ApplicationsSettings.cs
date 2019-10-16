using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesparcheBackend.Models
{
    public class ApplicationsSettings
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
    }
}
