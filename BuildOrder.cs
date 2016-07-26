using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public class BuildOrder
	{
		public int type;
		public int owner;
		public bool completed;


		public BuildOrder(int type, int owner)
		{
			this.type = type;
			this.owner = owner;
			completed = false;

		}





	}
}