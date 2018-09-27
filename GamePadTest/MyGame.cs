using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GamePadTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MyGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GamePadState CurrentPadState { get; private set; }
        public SpriteFont CourierFont { get; private set; }

        public MyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Gamepad Test / by zKnight79";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            CourierFont = Content.Load<SpriteFont>("Fonts/CourierFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here
            CurrentPadState = GamePad.GetState(PlayerIndex.One);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            RenderGamePadState();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void RenderGamePadState()
        {
            #region RENDER AS TEXT
            RenderGamePadPartState("Button", "A", CurrentPadState.Buttons.A);
            RenderGamePadPartState("Button", "B", CurrentPadState.Buttons.B);
            RenderGamePadPartState("Button", "X", CurrentPadState.Buttons.X);
            RenderGamePadPartState("Button", "Y", CurrentPadState.Buttons.Y);
            RenderGamePadPartState("Button", "LB", CurrentPadState.Buttons.LeftShoulder);
            RenderGamePadPartState("Button", "RB", CurrentPadState.Buttons.RightShoulder);
            RenderGamePadPartState("Button", "LS", CurrentPadState.Buttons.LeftStick);
            RenderGamePadPartState("Button", "RS", CurrentPadState.Buttons.RightStick);
            RenderGamePadPartState("Button", "Back", CurrentPadState.Buttons.Back);
            RenderGamePadPartState("Button", "Start", CurrentPadState.Buttons.Start);
            RenderGamePadPartState("Button", "Big", CurrentPadState.Buttons.BigButton);

            RenderGamePadPartState("Trigger", "LT", CurrentPadState.Triggers.Left);
            RenderGamePadPartState("Trigger", "RT", CurrentPadState.Triggers.Right);

            RenderGamePadPartState("D-Pad", "Up", CurrentPadState.DPad.Up);
            RenderGamePadPartState("D-Pad", "Down", CurrentPadState.DPad.Down);
            RenderGamePadPartState("D-Pad", "Left", CurrentPadState.DPad.Left);
            RenderGamePadPartState("D-Pad", "Right", CurrentPadState.DPad.Right);

            RenderGamePadPartState("L-Stick", "X-Axis-l", CurrentPadState.ThumbSticks.Left.X, "X-Axis");
            RenderGamePadPartState("L-Stick", "Y-Axis-l", CurrentPadState.ThumbSticks.Left.Y, "Y-Axis");
            RenderGamePadPartState("R-Stick", "X-Axis-r", CurrentPadState.ThumbSticks.Right.X, "X-Axis");
            RenderGamePadPartState("R-Stick", "Y-Axis-r", CurrentPadState.ThumbSticks.Right.Y, "Y-Axis");
            #endregion

            #region RENDER AS VIRTUAL GAMEPAD
            RenderVisualLegend();
            RenderPadFace();
            RenderPadTopView();
            #endregion
        }
        private readonly Dictionary<string, Vector2> PartsTextPositions = new Dictionary<string, Vector2>()
        {
            { "A", new Vector2(2, 2) },
            { "B", new Vector2(2, 22) },
            { "X", new Vector2(2, 42) },
            { "Y", new Vector2(2, 62) },
            { "LB", new Vector2(2, 82) },
            { "RB", new Vector2(2, 102) },
            { "LS", new Vector2(2, 122) },
            { "RS", new Vector2(2, 142) },
            { "Back", new Vector2(2, 162) },
            { "Start", new Vector2(2, 182) },
            { "Big", new Vector2(2, 202) },

            { "LT", new Vector2(2, 230) },
            { "RT", new Vector2(2, 250) },

            { "Up", new Vector2(2, 280) },
            { "Down", new Vector2(2, 300) },
            { "Left", new Vector2(2, 320) },
            { "Right", new Vector2(2, 340) },

            { "X-Axis-l", new Vector2(2, 370) },
            { "Y-Axis-l", new Vector2(2, 390) },
            { "X-Axis-r", new Vector2(2, 410) },
            { "Y-Axis-r", new Vector2(2, 430) }
        };
        private void RenderGamePadPartState(string partType, string partName, object partState, string displayName = null)
        {
            spriteBatch.DrawString(
                CourierFont,
                string.Format("{0,-7} {1,-6} : {2}", partType, displayName ?? partName, partState.ToString()),
                PartsTextPositions[partName],
                Color.White
            );
        }

        private readonly Vector2[] legendPositions = new Vector2[]
        {
            new Vector2(350,2),
            new Vector2(600,2),
            new Vector2(700,2)
        };
        private void RenderVisualLegend()
        {
            spriteBatch.DrawString(CourierFont, CurrentPadState.IsConnected ? "Connected" : "Disconnected", legendPositions[0], CurrentPadState.IsConnected ? Color.Green : Color.Red);
            //spriteBatch.DrawString(CourierFont, "Released", legendPositions[1], Color.Green);
            //spriteBatch.DrawString(CourierFont, "Pressed", legendPositions[2], Color.Red);
        }
        private void RenderPadFace()
        {
            
        }
        private void RenderPadTopView()
        {

        }
    }
}
