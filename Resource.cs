using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public class Resource
	{
		public Texture2D myTexture;
		public Rectangle rectangle;
		public Vector2 origin = new Vector2(24, 24);
		public Vector2 position = new Vector2(50, 50);
		public float rotation;
		public int currentFrame = 0;
		public int frameHeight = 48;
		public int frameWidth = 48;
		public int type;
		public bool inPlay = false;
		public int amount;

		public Resource (int type, float x, float y)
		{
			this.type = type;
			position.X = x;
			position.Y = y;

		}


		public void Draw(SpriteBatch spriteBatch){
			rectangle = new Rectangle (currentFrame * frameWidth, 0, 48, 48);
			spriteBatch.Draw(myTexture, position, rectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
		}
			

		public void Load(ContentManager content){

			if (type == 0) {
				myTexture = content.Load<Texture2D> ("Tree");
				currentFrame = 1;
				amount = 10;
				inPlay = true;
			} else if (type == 1) {

				myTexture = content.Load<Texture2D> ("Rock");
				amount = 10;
				inPlay = true;

			} else if(type == 2) {
				myTexture = content.Load<Texture2D> ("Field");
			}
		}


	}
}