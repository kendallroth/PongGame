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


namespace Assignment4
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class Paddle : DrawableGameComponent
	{
		private SpriteBatch spriteBatch;
		private Texture2D texture;
		private Vector2 position;
		private Vector2 speed;
		private Rectangle collisionBounds;
		private Keys upKey;
		private Keys downKey;

		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public Rectangle CollisionBounds
		{
			get
			{
				collisionBounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
				return collisionBounds;
			}
		}

		public Paddle(Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Vector2 speed, Keys upKey, Keys downKey)
			: base(game)
		{
			this.spriteBatch = spriteBatch;
			this.texture = texture;
			this.position = position;
			this.speed = speed;

			this.upKey = upKey;
			this.downKey = downKey;
		}

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run.  This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
			// TODO: Add your initialization code here

			base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			KeyboardState keyboardState = Keyboard.GetState();

			if (keyboardState.IsKeyDown(upKey))
			{
				if (position.Y > 0)
				{
					position.Y -= 5; 
				}
				else
				{
					position.Y = 0;
				}
			}
			else if (keyboardState.IsKeyDown(downKey))
			{
				if (position.Y + texture.Height < PongGame.stage.Y)
				{
					position.Y += 5; 
				}
				else
				{
					position.Y = PongGame.stage.Y - texture.Height;
				}
			}

			base.Update(gameTime);
		}


		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(texture, position, Color.White);
			spriteBatch.End();
			
			base.Draw(gameTime);
		}
	}
}
