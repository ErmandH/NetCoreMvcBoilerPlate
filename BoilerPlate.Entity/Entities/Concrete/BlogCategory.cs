using BoilerPlate.Entity.Entities.Abstract;

namespace BoilerPlate.Entity.Entities.Concrete
{
	public class BlogCategory : IEntity
	{
		public int BlogId { get; set; }
		public Blog Blog { get; set; }

		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}