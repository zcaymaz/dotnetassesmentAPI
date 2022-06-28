namespace todo_mw.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }

    }
}