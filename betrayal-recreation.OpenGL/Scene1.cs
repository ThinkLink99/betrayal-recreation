using betrayal_recreation_shared;
using Collision;
using Collision.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace betrayal_recreation.OpenGL
{
    internal class Scene1 : Scene
    {
        Player testPlayer;
        public Scene1(CGame game, int screenWidth, int screenHeight, bool useTextureFiltering = true) 
            : base(game, screenWidth, screenHeight, useTextureFiltering)
        {
            testPlayer = new Player(0, "Test");
            testPlayer.AddComponent(null);
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void LateUpdate(GameTime gameTime)
    {
    }
    }
}
