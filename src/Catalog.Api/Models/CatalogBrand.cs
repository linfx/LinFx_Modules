using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFx.Catalog.Api.Models
{
    public class CatalogBrand
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Brand { get; set; }
    }
}
