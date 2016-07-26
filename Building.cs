using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public class Building
	{
		public Texture2D myTexture;
		public Rectangle rectangle;
		public Rectangle groundRectangle;
		public Texture2D groundTexture;
		public Vector2 origin = new Vector2(24, 24);
		public Vector2 position = new Vector2(50, 50);
		public float rotation;
		public int currentFrame = 0;
		public int frameHeight = 96;
		public int frameWidth = 144;
		public int groundFrameWidth = 52;
		public int type;
		public bool inPlay = false;
		public int amount;

		public Building(int type, float x, float y)
		{
			this.type = type;
			position.X = x;
			position.Y = y;

		}


		public void Draw(SpriteBatch spriteBatch){
			rectangle = new Rectangle (currentFrame * frameWidth, 0, 144, 96);
			spriteBatch.Draw(myTexture, position, rectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
		}

		public void DrawGround(SpriteBatch spriteBatch){
			groundRectangle = new Rectangle (currentFrame * groundFrameWidth, 0, 52, 96);
			spriteBatch.Draw(groundTexture, position, groundRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
		}


		public void Load(ContentManager content){

			if (type == 0) {
				myTexture = content.Load<Texture2D> ("House");
				inPlay = true;
			} else if (type == 1) {

				myTexture = content.Load<Texture2D> ("Smithy");
				groundTexture = content.Load<Texture2D> ("SmithyGround");
				inPlay = true;

			}
		}


	}
}