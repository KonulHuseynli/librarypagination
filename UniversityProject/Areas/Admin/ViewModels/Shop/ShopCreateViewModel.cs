namespace UniversityProject.Areas.Admin.ViewModels.Shop
{
    public class ShopCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }

        public IFormFile MainPhoto { get; set; }
        public string Price { get; set; }
    }
}
