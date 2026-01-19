//using Assimp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using Infinite1942.Engine;
using Infinite1942.Engine.SceneManagement;

namespace Infinite1942
{
    public class Infinite1942Game : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Transform _shipTransform;
        private Model _shipModel;
        private Texture2D _shipTexture;

        // systems
        private Camera _camera;
        private SpriteRenderer _shipRenderer;

        private List<GameObject> _gameObjects = new List<GameObject>();

        public Infinite1942Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Components.Add(new InputManager(this));
            Components.Add(new SceneManager(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            var cameraGO = new GameObject();
            _camera = cameraGO.AddComponent<Camera>();
            _camera.View = Matrix.CreateLookAt(new Vector3(0, 0, 30), new Vector3(0, 0, 0), Vector3.UnitY);
            _camera.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 1280f / 720f, 0.1f, 1000f);

            var shipTexture = Content.Load<Texture2D>("Art/Textures/simpleSpace_sheet");
            var shipGO = new GameObject();
            _shipRenderer = shipGO.AddComponent<SpriteRenderer>();
            _shipRenderer.Initialize(GraphicsDevice);
            _shipRenderer.Sprite = new Sprite(shipTexture, new Rectangle(676, 448, 64, 48));
 
            shipGO.Transform.Translate(-25, 0, -30);
            shipGO.Transform.Scale(30, 30, 1);
        }

        protected override void UnloadContent()
        {
            _gameObjects.Clear();
            
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
/*
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _shipTransform.Rotate(new Vector3(0, (float)(5.0 * gameTime.ElapsedGameTime.TotalSeconds), 0));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _shipTransform.Rotate(new Vector3(0, (float)(-5.0 * gameTime.ElapsedGameTime.TotalSeconds), 0));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _shipTransform.Rotate(new Vector3((float)(5.0 * gameTime.ElapsedGameTime.TotalSeconds), 0, 0));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _shipTransform.Rotate(new Vector3((float)(-5.0 * gameTime.ElapsedGameTime.TotalSeconds), 0, 0));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _shipTransform.Translate(new Vector3((float)(5.0 * gameTime.ElapsedGameTime.TotalSeconds), 0, 0));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _shipTransform.Translate(new Vector3((float)(-5.0 * gameTime.ElapsedGameTime.TotalSeconds), 0, 0));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _shipTransform.Translate(new Vector3(0, (float)(-5.0 * gameTime.ElapsedGameTime.TotalSeconds), 0));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _shipTransform.Translate(new Vector3(0, (float)(5.0 * gameTime.ElapsedGameTime.TotalSeconds), 0));
            }
*/
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //RasterizerState rasterizerState = new RasterizerState();
            //rasterizerState.CullMode = CullMode.None;
            //GraphicsDevice.RasterizerState = rasterizerState;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            if (_shipRenderer != null)
            {
                _shipRenderer.Draw(gameTime, GraphicsDevice, _camera.View, _camera.Projection);
            }

            // _spriteBatch.Begin();
            //
            // _spriteBatch.Draw(_shipTexture, new Rectangle(0, 0, 100, 100), Color.White);
            //
            // _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Camera camera)
        {
            DrawModel(model, world, camera.View, camera.Projection);
        }
        
        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            if (model == null)
                return;
            
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
        }
    }
}
