using Swashbuckle.AspNetCore.Annotations;

namespace ProductManagement.Server.DTO
{
    public class CategoryDTO
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        public string CategoryName { get; set; }
    }
}
