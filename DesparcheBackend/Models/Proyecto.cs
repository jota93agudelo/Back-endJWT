using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesparcheBackend.Models
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
    }
}
