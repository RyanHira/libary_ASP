using LibaryASP_MVC.Models.Domain;

namespace LibaryASP_MVC.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Author> Authors { get; set; } 
    }
}
