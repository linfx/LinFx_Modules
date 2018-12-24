using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFx.Catalog.Api.Models
{
    public class CatalogType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Type { get; set; }
    }
}
