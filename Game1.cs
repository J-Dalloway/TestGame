using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Storage;  Hopefully this will not be needed.
using Microsoft.Xna.Framework.Input;

namespace TestGame
{	
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		//Game World
		public static Man man = new Man();////<<
		public static int MANCOUNT = 100;
		public static int ROCKCOUNT = 50;
		public static int TREECOUNT = 200;
		public static int FIELDCOUNT = 500;
		public static int SMITHCOUNT = 30;
		public static int BUILDORDERCOUNT = 100;
		public static Manv2[] npcs = new Manv2[MANCOUNT];
		public static Building[] smiths = new Building[SMITHCOUNT];
		public static Resource[] rocks = new Resource[ROCKCOUNT];
		public static Resource[] trees = new Resource[TREECOUNT];
		public static Resource[] fields = new Resource[FIELDCOUNT];
		public static BuildOrder[] buildOrders = new BuildOrder[BUILDORDERCOUNT];
		public static int fieldIndex = -1;
		public static int smithIndex = -1;
		public static int buildIndex = -1;
		public static int startBuildIndex = 0;
		public static int currentBuildOrders = 0;
		Camera camera;
		Texture2D backgroundTexture;
		Vector2 backgroundPosition;
		Random random = new Random();
		Intelligence intelligence = new Intelligence();
		public static int[] totalProfessions = new int[29];

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
		}


		protected override void Initialize ()
		{

			camera = new Camera(GraphicsDevice.Viewport);
            
			base.Initialize ();
		}

		protected override void LoadContent ()
		{

			for (int i = 0; i < 29; i++)
			{
				totalProfessions[i] = 0;
			}
			for (int i = 0; i < SMITHCOUNT; i++) {
				smiths [i] = new Building (1, -2000, -2000);
			}
			for (int i = 0; i < ROCKCOUNT; i++) {
				rocks [i] = new Resource (1, random.Next (48, 3952), random.Next (48, 3952));
			}
			for (int i = 0; i < TREECOUNT; i++) {
				trees [i] = new Resource (0, random.Next (48, 3952), random.Next (48, 3952));
			}
			for (int i = 0; i < FIELDCOUNT; i++) {
				fields [i] = new Resource (2, -2000, -2000);
			}

			spriteBatch = new SpriteBatch (GraphicsDevice);
			man.Load (Content);////<<
			for (int i = 0; i < MANCOUNT; i++) {
				npcs [i] = new Manv2 ((float)random.Next (10, 201) / 100, random.Next (0, 4000), random.Next (0, 4000), random.Next(0, 11), random.Next(1, 11), 
					random.Next(1, 11), random.Next(1, 11), random.Next(0, 11), random.Next(0, 3), random.Next(0, 11), random.Next(1, 11), random.Next(0, 11), 
					random.Next(1, 11), random.Next(1, 11), random.Next(0, 11), random.Next(0, 11), random.Next(1, 11), random.Next(1, 11), 24, 24, i);
				intelligence.FindProfession (npcs[i]);
			}

			for (int i = 2; i < 29; i++)
			{
				foreach (Manv2 manv2 in npcs)
				{
					if (manv2.totalSkills == i)
					{


						int[] choices = new int[i];
						bool chosen = false;
						int tempIndex = 0;
						for (int p = 0; p < 29;p++)
						{
							if (!chosen)
							{
								if (manv2.professions[p] == 1)
								{
									if (totalProfessions[p] == 0)
									{
										manv2.currentProfession = p;
										totalProfessions[p]++;
										chosen = true;
									}
									else {
										choices[tempIndex] = p;
										tempIndex++;
									}
								}
							}
							else {

							}
						}
						tempIndex = random.Next(0, i);
						manv2.currentProfession = tempIndex;
						totalProfessions[tempIndex]++;

					}
				}
			}



			for (int i = 0; i < 29; i++)
			{
				Console.Write(i + " " +totalProfessions[i] + " ");
			}

			foreach (Manv2 manv2 in npcs) {
				manv2.Load (Content);
			}
			foreach (Building smith in smiths) {
				smith.Load (Content);
			}
			foreach (Resource resource in rocks) {
				resource.Load (Content);
			}
			foreach (Resource resource in trees) {
				resource.Load (Content);
			}
			foreach (Resource resource in fields) {
				resource.Load (Content);
			}
			backgroundTexture = Content.Load<Texture2D>("Background");
			backgroundPosition = new Vector2 (0, 0);



		}


		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
			#endif

			foreach (Manv2 manv2 in npcs) {

				intelligence.FindTask (manv2, gameTime);
			}



			man.position = man.velocity + man.position;
			if (Keyboard.GetState ().IsKeyDown (Keys.Right)) man.rotation += 0.05f;
			if (Keyboard.GetState ().IsKeyDown (Keys.Left)) man.rotation -= 0.05f;
			if (Keyboard.GetState ().IsKeyDown (Keys.Up)) {
				man.velocity.X = (float)Math.Cos (man.rotation) * man.tangentialVelocity;
				man.velocity.Y = (float)Math.Sin (man.rotation) * man.tangentialVelocity;
			} else if (man.velocity != Vector2.Zero) {
				float i = man.velocity.X;
				float j = man.velocity.Y;

				man.velocity.X = i -= man.friction * i;
				man.velocity.Y = j -= man.friction * j;
			}

			camera.Update(gameTime, man);


            
			base.Update (gameTime);
		}
			
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.LightSkyBlue);

			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,null,null,null,null,camera.transform);
			spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
			foreach (Resource resource in rocks) {
				resource.Draw (spriteBatch);
			}
			for (int i = 0; i <= fieldIndex && i >-1; i++) {
				if (fields [i].inPlay) {
					fields [i].Draw (spriteBatch);
				}
			}
			foreach (Resource resource in trees) {
				if (!resource.inPlay) {
					resource.Draw (spriteBatch);
				}
			}
			foreach (Building smith in smiths) {
				smith.DrawGround (spriteBatch);
			}
			foreach (Manv2 manv2 in npcs) {
				manv2.Draw (spriteBatch);
			}
			spriteBatch.Draw (man.myTexture, man.position, null, Color.White, man.rotation, man.origin, 1f, SpriteEffects.None, 0);//null could be man.rectangle
			foreach (Building smith in smiths) {
				smith.Draw (spriteBatch);
			}
			foreach (Resource resource in trees) {
				if (resource.inPlay) {
					resource.Draw (spriteBatch);
				}
			}

			spriteBatch.End();
            
            
			base.Draw (gameTime);
		}
	}
}