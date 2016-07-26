using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TestGame
{
	public class Camera
	{
		public Matrix transform;
		Viewport view;
		Vector2 centre;

		public Camera (Viewport newView)
		{
			view = newView;
		}

		public void Update(GameTime gameTime, Man man){
			centre = new Vector2 (man.position.X + 12 - 400, man.position.Y + 12 - 200) ;
			transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X,-centre.Y,0));
		}
	}
}

