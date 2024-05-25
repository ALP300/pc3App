using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pc3App.Integration.jsonplaceholder.dto
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        [JsonPropertyName("first_name")]
        public string Nombre { get; set; }

        [JsonPropertyName("last_name")]
        public string Apellido { get; set; }
        public string? Avatar { get; set; }
    }
}