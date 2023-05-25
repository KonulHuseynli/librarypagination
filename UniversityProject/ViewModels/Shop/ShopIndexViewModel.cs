namespace UniversityProject.ViewModels.Shop
{
    public class ShopIndexViewModel
    {
        public List<Models.ShopProduct> ShopProducts { get; set; }

        public int Page { get; set; } = 1;

        public int Take { get; set; } = 9;

        public int PageCount { get; set; } 
    }
}
