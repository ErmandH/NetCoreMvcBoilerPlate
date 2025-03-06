using BoilerPlate.Entity.Entities.Abstract;

namespace BoilerPlate.Entity.Entities.Concrete.File
{
	public abstract class BaseFile : BaseEntity, IEntity
	{
		public string FileName { get; set; }
		public string FileType { get; set; }
		public string FilePath { get; set; }
		public long FileSize { get; set; }
		public string ContentType { get; set; }
	}
}