using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public class Man
	{
		public Texture2D myTexture;
		//public Rectangle myRectangle;
		public Vector2 origin = new Vector2(12, 12);
		public Vector2 position = new Vector2(50, 50);
		public float rotation;
		public Vector2 velocity;
		public float tangentialVelocity = 2f;
		public float friction = 0.3f;

		public Man ()
		{
		}

		public void Load(ContentManager content){
			myTexture = content.Load<Texture2D> ("Character");
			//myRectangle = new Rectangle (100, 100, 24, 24);
		}


	}
}