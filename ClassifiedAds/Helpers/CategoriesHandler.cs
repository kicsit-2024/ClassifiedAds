using ClassifiedAds.Data;

namespace ClassifiedAds.Helpers
{
    public class CategoriesHandler
    {
        private readonly AppDbContext _context;

        public CategoriesHandler(AppDbContext context)
        {
            _context = context;
        }

        public List<string> CategoriesNames()
        {
            return _context.Categories.Select(m => m.Name).ToList();
        }
    }
}
