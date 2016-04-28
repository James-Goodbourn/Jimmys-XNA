using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using JimysXNA.Entities;
using JimysXNA.Scale;
using JimysXNA.Pathfinding;

namespace TestEntities
{
    public class TestEntity : Microsoft.Xna.Framework.Game
    {
        #region Constructor

        public TestEntity()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = ORIGINAL_WIDTH;
            graphics.PreferredBackBufferHeight = ORIGINAL_HEIGHT;

            this.graphics.IsFullScreen = false;
            
            Content.RootDirectory = "Content";
        }

        #endregion


        #region XNA Game Overrides

        protected override void Initialize()
        {

            //initialise scaler - add event handler to the graphics device (resset) 
            SpriteScaler = new SpriteScale(ORIGINAL_WIDTH, ORIGINAL_HEIGHT, this.graphics);
            graphics.GraphicsDevice.DeviceReset +=
            new EventHandler<EventArgs>(SpriteScaler.DeviceReset);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            EntityTest();

            PathTest();

            SpriteScaler.Scale();


        }
        
        protected override void UnloadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            EntityUpdate();

            Fullscreen();
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Begin with the sprite scale matrix as the transform matrix parameter
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, SpriteScaler.ScaleMatrix);

            //draw all entities in the manager
            Manager.DrawEntities(this.spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion


        #region Entity Test

        private void EntityTest()
        {
            Manager = new EntityManager();
            //create 2 sprites and add to the manager
            var player = new EntityBase(new Vector2(100, 100), 1.0f, "front", this.Content, "Player1");
            var player2 = new EntityBase(new Vector2(100, 100), 1.0f, "front", this.Content, "Player2");
            Manager.Add(player);
            Manager.Add(player2);
        }

        private void EntityUpdate()
        {
            //get handle to one of the entities by name
            var p = Manager.Get("Player1");
            //entity movement via key presses
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                p.SetYPosition(p.GetY() - 1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                p.SetXPosition(p.GetX() - 1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                p.SetYPosition(p.GetY() + 1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                p.SetXPosition(p.GetX() + 1);
            }

            //update all entities in the manager
            Manager.UpdateEntities();
        }


        #endregion


        #region PathTest

        private void PathTest()
        {
            //generate a test map
            var map = new int[100, 100];
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    map[i, j] = i + j + 1;
                }
            }
            //initialise pathfinder
            Pathfinder = new Pathfinding(new Vector2(0, 0), new Vector2(2, 10), map, 100, 100);
          
            if (Pathfinder.isReachable())
            {
                //calculate paths and get lists
                Pathfinder.CalculatePath();
                var coords = Pathfinder.GetPathCoords();
                var directions = Pathfinder.GetPathDirections();
                var vectors = Pathfinder.GetPathVector();

            }
        }

        #endregion


        #region FullScreen Toggle

        private void ToggleFullScreen()
        {
            //set the backbuffer and apply changes and toogle fullscreen, will call the SpriteScaler graphics reset event
            if (!IsFull)
            {
                graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                graphics.ApplyChanges();
                graphics.ToggleFullScreen();

            }
            else
            {
                graphics.PreferredBackBufferWidth = ORIGINAL_WIDTH;
                graphics.PreferredBackBufferHeight = ORIGINAL_HEIGHT;
                graphics.ApplyChanges();
                graphics.ToggleFullScreen();
            }

            IsFull = !IsFull;
        }

        private void Fullscreen()
        {
            fullscreenTimer++;
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (fullscreenTimer > 100)
                {
                    ToggleFullScreen();
                    fullscreenTimer = 0;
                }
            }
        }

        #endregion


        #region Properties

        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        private EntityManager Manager;

        const int ORIGINAL_WIDTH = 832;
        const int ORIGINAL_HEIGHT = 704;

        private SpriteScale SpriteScaler;

        private Pathfinding Pathfinder; 

        private bool IsFull;

        private int fullscreenTimer = 0;

        #endregion
    }
}
