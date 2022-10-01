namespace Legislative.Models
{
    public class LmonAnalysis
    {
        public LmonAnalysis(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
    }
}
