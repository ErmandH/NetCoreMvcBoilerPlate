namespace BoilerPlate.AdminPanel.Helpers
{
    public class PathHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public PathHelper(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public string GetPath(string path)
        {
            //if (_env.IsDevelopment())
            //{
            //    return path;
            //}
            //return $"/core{path}";
            return path;
        }
    }
}
