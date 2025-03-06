using System.Collections.Generic;

namespace BoilerPlate.Entity.Entities.Concrete.File
{
	public class ImageFile : BaseFile
	{
		public int Width { get; set; }
		public int Height { get; set; }

		// Blog ile çoka çok ilişki
		public ICollection<BlogImage> BlogImages { get; set; }
	}
}