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
        public Texture2D GamepadTexture { get; private set; }

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
            GamepadTexture = Content.Load<Texture2D>("Graphics/Gamepad");
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
            spriteBatch.DrawString(CourierFont, "Released", legendPositions[1], Color.Green);
            spriteBatch.DrawString(CourierFont, "Pressed", legendPositions[2], Color.Red);
        }

        private readonly Rectangle[] gamepadRects = new Rectangle[]
        {
            new Rectangle(350, 200, 400, 200),
            new Rectangle(0, 0, 400, 200)
        };
        private readonly Rectangle[] dPadRects = new Rectangle[]
        {
            new Rectangle(10, 210, 18, 18),
            new Rectangle(40, 210, 18, 18),
            new Rectangle(35 + 350, 51 + 200, 18, 18),
            new Rectangle(35 + 350, 87 + 200, 18, 18),
            new Rectangle(17 + 350, 69 + 200, 18, 18),
            new Rectangle(53 + 350, 69 + 200, 18, 18)
        };
        private readonly Rectangle[] abxyButtonsRects = new Rectangle[]
        {
            new Rectangle(70, 210, 28, 28),
            new Rectangle(103, 210, 28, 28),
            new Rectangle(315 + 350, 105 + 200, 28, 28),
            new Rectangle(351 + 350, 67 + 200, 28, 28),
            new Rectangle(278 + 350, 67 + 200, 28, 28),
            new Rectangle(314 + 350, 30 + 200, 28, 28)
        };
        private readonly Rectangle[] startbackButtonsRects = new Rectangle[]
        {
            new Rectangle(140, 210, 30, 10),
            new Rectangle(180, 210, 30, 10),
            new Rectangle(130 + 350, 87 + 200, 30, 10),
            new Rectangle(221 + 350, 88 + 200, 30, 10)
        };
        private readonly Rectangle[] bigButtonRects = new Rectangle[]
        {
            new Rectangle(220, 210, 78, 54),
            new Rectangle(310, 210, 78, 54),
            new Rectangle(156 + 350, 14 + 200, 78, 54)
        };
        private readonly Rectangle[] stickRects = new Rectangle[]
        {
            new Rectangle(400, 210, 6, 6),
            new Rectangle(410, 210, 6, 6),
            new Rectangle(112 + 350, 159 + 200, 6, 6),
            new Rectangle(273 + 350, 159 + 200, 6, 6)
        };
        private readonly float stickRadius = 32f;
        private Rectangle ComputeStickRect(Rectangle baseRect, Vector2 stick)
        {
            Rectangle stickRect = baseRect;
            stickRect.X += (int)(stickRadius * stick.X);
            stickRect.Y -= (int)(stickRadius * stick.Y);
            return stickRect;
        }
        private void RenderPadFace()
        {
            // Gamepad
            spriteBatch.Draw(GamepadTexture, gamepadRects[0], gamepadRects[1], Color.White);
            // D-pad
            spriteBatch.Draw(GamepadTexture, dPadRects[2], CurrentPadState.DPad.Up == ButtonState.Released ? dPadRects[0] : dPadRects[1], Color.White);
            spriteBatch.Draw(GamepadTexture, dPadRects[3], CurrentPadState.DPad.Down == ButtonState.Released ? dPadRects[0] : dPadRects[1], Color.White);
            spriteBatch.Draw(GamepadTexture, dPadRects[4], CurrentPadState.DPad.Left == ButtonState.Released ? dPadRects[0] : dPadRects[1], Color.White);
            spriteBatch.Draw(GamepadTexture, dPadRects[5], CurrentPadState.DPad.Right == ButtonState.Released ? dPadRects[0] : dPadRects[1], Color.White);
            // A-B-X-Y buttons
            spriteBatch.Draw(GamepadTexture, abxyButtonsRects[2], CurrentPadState.Buttons.A == ButtonState.Released ? abxyButtonsRects[0] : abxyButtonsRects[1], Color.White);
            spriteBatch.Draw(GamepadTexture, abxyButtonsRects[3], CurrentPadState.Buttons.B == ButtonState.Released ? abxyButtonsRects[0] : abxyButtonsRects[1], Color.White);
            spriteBatch.Draw(GamepadTexture, abxyButtonsRects[4], CurrentPadState.Buttons.X == ButtonState.Released ? abxyButtonsRects[0] : abxyButtonsRects[1], Color.White);
            spriteBatch.Draw(GamepadTexture, abxyButtonsRects[5], CurrentPadState.Buttons.Y == ButtonState.Released ? abxyButtonsRects[0] : abxyButtonsRects[1], Color.White);
            // Start-Back buttons
            spriteBatch.Draw(GamepadTexture, startbackButtonsRects[2], CurrentPadState.Buttons.Back == ButtonState.Released ? startbackButtonsRects[0] : startbackButtonsRects[1], Color.White);
            spriteBatch.Draw(GamepadTexture, startbackButtonsRects[3], CurrentPadState.Buttons.Start == ButtonState.Released ? startbackButtonsRects[0] : startbackButtonsRects[1], Color.White);
            // "Big" button
            spriteBatch.Draw(GamepadTexture, bigButtonRects[2], CurrentPadState.Buttons.BigButton == ButtonState.Released ? bigButtonRects[0] : bigButtonRects[1], Color.White);
            // Sticks
            spriteBatch.Draw(GamepadTexture, ComputeStickRect(stickRects[2], CurrentPadState.ThumbSticks.Left), CurrentPadState.Buttons.LeftStick == ButtonState.Released ? stickRects[0] : stickRects[1], Color.White);
            spriteBatch.Draw(GamepadTexture, ComputeStickRect(stickRects[3], CurrentPadState.ThumbSticks.Right), CurrentPadState.Buttons.RightStick == ButtonState.Released ? stickRects[0] : stickRects[1], Color.White);
        }

        private void RenderPadTopView()
        {

        }
    }
}
