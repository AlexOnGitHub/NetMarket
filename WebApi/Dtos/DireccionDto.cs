using Core.Entities;

namespace WebApi.Dtos
{
    public class DireccionDto
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string Departamento { get; set; }
        public string CP { get; set; }
        public string Pais { get; set; }
    }
}
