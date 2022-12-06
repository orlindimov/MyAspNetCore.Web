namespace MyAspNetCore.Web.ViewModel
{
    public class VIsitorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public string Date => Created.ToShortDateString();

    }
}
