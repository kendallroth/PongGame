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
	public class CollisionManager : GameComponent
	{
		private List<Paddle> playerList;
		private Ball ball;

		/// <summary>
		/// Create a new CollisionManager object
		/// </summary>
		/// <param name="game">Game reference for the component</param>
		/// <param name="playerList">List of players (paddles)</param>
		/// <param name="ball">Ball reference</param>
		public CollisionManager(Game game, List<Paddle> playerList, Ball ball)
			: base(game)
		{
			this.playerList = playerList;
			this.ball = ball;
		}

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run.  This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			foreach (Paddle paddle in playerList)
			{
				//Get the collision boundaries of the paddle and ball
				Rectangle paddleBounds = paddle.CollisionBounds;
				Rectangle ballBounds = ball.CollisionBounds;

				//Find the intersection of the ball and paddle
				Rectangle collisionRectangle = Rectangle.Intersect(paddleBounds, ballBounds);

				//Store the post-collision position of the ball
				Vector2 collisionPosition = ball.Position;

				if (ballBounds.Intersects(paddleBounds))
				{
					if (collisionRectangle.Height > collisionRectangle.Width)
					{
						//Right/Left collision
						if (collisionRectangle.X > ball.Position.X)
						{
							//Left-paddle collision
							collisionPosition.X = paddleBounds.X - ballBounds.Width;
						}
						else
						{
							//Right-paddle collision
							collisionPosition.X = paddleBounds.X + paddleBounds.Width;
						}

						#region ChangeReturnAngle
						//Get the vertical distance between the ball and paddle centers (positive is ball center below paddle center)
						/*int verticalIntersectDistance =  (ballBounds.Y + ballBounds.Height / 2) - (paddleBounds.Y + paddleBounds.Height / 2);
						double normalizedVerticalIntersectDistance = verticalIntersectDistance / (paddleBounds.Height / 2);

						//double MAX_BOUNCE_ANGLE = (Math.PI * 180) * 75;
						double MAX_BOUNCE_ANGLE = 5 * Math.PI / 12;

						double bounceAngle = normalizedVerticalIntersectDistance * MAX_BOUNCE_ANGLE;

						int ballSpeedX = (int)(ball.Speed.X * Math.Cos(bounceAngle));
						int ballSpeedY = (int)(ball.Speed.X * Math.Cos(bounceAngle));
						ball.Speed = new Vector2(ballSpeedX, ballSpeedY);*/
						#endregion

						//Reverse the X-speed
						ball.Speed = new Vector2(ball.Speed.X * -1, ball.Speed.Y);
					}
					else
					{
						//Top/Bottom collision
						if (collisionRectangle.Y > ball.Position.Y)
						{
							//Top-paddle collision
							collisionPosition.Y = paddleBounds.Y - ballBounds.Height;
						}
						else
						{
							//Bottom-paddle collision
							collisionPosition.Y = paddleBounds.Y + paddleBounds.Height;
						}

						//Reverse the Y-speed
						ball.Speed = new Vector2(ball.Speed.X, ball.Speed.Y * -1);
					}

					//Update the position of the ball to the post-collision position
					ball.Position = collisionPosition;

					//Play a collision sound
					PongGame.soundManager.PlaySound("click");
				}
			}

			base.Update(gameTime);
		}
	}
}