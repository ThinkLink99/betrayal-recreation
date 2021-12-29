namespace betrayal_recreation_shared
{
    public class BasicObjectInformation
    {
        public BasicObjectInformation(int id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
