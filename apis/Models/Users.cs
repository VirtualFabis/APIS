namespace apis.Models
{
    public class Users
    {
        public int id { get; set; }
        public string wiw { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
    public class Task
    {
        public int idTask { get; set; }
        public int id { get; set; }
        public string task { get; set; }
        public string desc { get; set; }
        public bool activate { get; set; }
    }

}
