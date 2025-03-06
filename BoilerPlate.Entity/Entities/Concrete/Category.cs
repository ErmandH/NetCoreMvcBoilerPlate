using BoilerPlate.Entity.Entities.Abstract;
using System.Collections.Generic;

namespace BoilerPlate.Entity.Entities.Concrete
{
	public class Category : BaseEntity, IEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }

		// Blog ile çoka çok ilişki
		public ICollection<BlogCategory> BlogCategories { get; set; }
	}
}