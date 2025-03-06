using BoilerPlate.Entity.Entities.Abstract;
using BoilerPlate.Entity.Entities.Concrete.File;

namespace BoilerPlate.Entity.Entities.Concrete
{
	public class BlogImage : IEntity
	{
		public int BlogId { get; set; }
		public Blog Blog { get; set; }

		public int ImageId { get; set; }
		public ImageFile Image { get; set; }
	}
}