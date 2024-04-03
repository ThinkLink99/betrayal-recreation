namespace betrayal_recreation_shared
{
    public abstract class GameObject : Collision.GameObject
    {
        protected GameObject(int id, string name, string description)
        {
            objectInformation = new BasicObjectInformation(id, name, description);
        }
        protected GameObject(BasicObjectInformation objectInformation)
        {
            this.objectInformation = objectInformation;
        }

        public BasicObjectInformation objectInformation { get; private set; }
    }
}
