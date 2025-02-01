using System.Collections.Generic;
using Assimp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Infinite1942;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Scene _ship;
    private BasicEffect _shipEffect;
    private VertexBuffer _shipVB;

    private Matrix world = Matrix.CreateTranslation(new Vector3(0, -5, -10)) * Matrix.CreateFromYawPitchRoll(0, MathHelper.ToRadians(-20), MathHelper.ToRadians(180));
    private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 30), new Vector3(0, 0, 0), -Vector3.UnitY);
    private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 1280f / 720f, 0.1f, 1000f);

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        var importer = new AssimpContext();
        _ship = importer.ImportFile("./Content/Art/Models/craft_speederA.fbx");
        
        List<VertexPositionColorNormal> shipVertices = new List<VertexPositionColorNormal>();
        foreach (var mesh in _ship.Meshes)
        {
            for (int i = 0; i < mesh.VertexCount; ++i)
            {
                var vertex = mesh.Vertices[i];
                var normal = mesh.Normals[i];
                // var color = mesh.VertexColorChannels[0];
               
                shipVertices.Add(new VertexPositionColorNormal(
                    new Vector3(vertex.X, vertex.Y, vertex.Z),
                    Color.White,
                    new Vector3(normal.X, normal.Y, normal.Z)));
            }
        }

        _shipEffect = new BasicEffect(GraphicsDevice);
        _shipEffect.World = world;
        _shipEffect.View = view;
        _shipEffect.Projection = projection;
        _shipEffect.VertexColorEnabled = true;
        _shipEffect.EnableDefaultLighting();

        _shipVB = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColorNormal), shipVertices.Count, BufferUsage.WriteOnly);
        _shipVB.SetData<VertexPositionColorNormal>(shipVertices.ToArray());
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        GraphicsDevice.SetVertexBuffer(_shipVB);

        RasterizerState rasterizerState = new RasterizerState();
        rasterizerState.CullMode = CullMode.None;
        GraphicsDevice.RasterizerState = rasterizerState;

        foreach (EffectPass pass in _shipEffect.CurrentTechnique.Passes)
        {
            pass.Apply();
            GraphicsDevice.DrawPrimitives(Microsoft.Xna.Framework.Graphics.PrimitiveType.TriangleList, 0, _shipVB.VertexCount / 3);
        }

        // DrawModel(_ship, world, view, projection);

        base.Draw(gameTime);
    }

    private void DrawModel(Scene model, Matrix world, Matrix view, Matrix projection)
    {
        foreach (var mesh in model.Meshes)
        {
            

            // foreach (BasicEffect effect in mesh.Effects)
            // {
            //     effect.World = world;
            //     effect.View = view;
            //     effect.Projection = projection;
            // }

            // mesh.Draw();


        }
    }
}
