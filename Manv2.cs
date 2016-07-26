using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public class Manv2
	{
		public Texture2D myTexture;
		public Rectangle rectangle;
		public Vector2 origin = new Vector2(12, 12);
		public Vector2 position = new Vector2(50, 50);
		public Vector2 targetPosition = new Vector2 (0,0);
		public float rotation;
		public Vector2 velocity;
		public float dexterity = 0f;
		public float friction = 0.3f;
		public int charisma, wisdom, knowledge, strength, empathy, lawful, confidence, loyalty, independence, greed, cooperation, creativity, aggression, curiosity, health, ownIndex;
		public int targetIndex = -1;
		public int currentFrame = 0;
		public int frameHeight, frameWidth;
		public float timer;
		public float interval;
		public bool hasTask = false;
		public int sillyTimer, sillyTimerMax;
		public int totalSkills = 0;
		public int[] professions = new int[29];
		public int currentProfession =-1;
		public Resource field;
		public Building smith;
		public Building house;
		public int tools = 2;
		public int food = 5;
		public int stone = 50;
		public int metal = 0;
		public int wood = 200;
		public string currentTask = "";
		public int buildNumber;


		public void AnimateMove(GameTime gameTime){
			timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
			if (timer > interval) {
				currentFrame++;
				timer = 0;
				if (currentFrame > 30) {
					currentFrame = 0;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch){
			spriteBatch.Draw(myTexture, position, rectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
		}

		public Manv2 (float dex, int x, int y, int cha, int wis, int know, int str, int emp, int emp2, int law, 
			int con, int loy, int ind, int gre, int coo, int cre, int agro, int cur, int newFrameHeight, int newFrameWidth, int own)
		{
			dexterity = dex;
			if (dexterity == 0) {
				dexterity = 1f;
			}
			position.X = x;
			position.Y = y;
			targetPosition.X = x;
			targetPosition.Y = y;
			charisma = cha;
			wisdom = wis;
			knowledge = know;
			strength = str;
			if (emp > emp2) {
				empathy = emp;
			} else {
				empathy = emp2;
			}
			lawful = law;
			confidence = con;
			loyalty = loy;
			independence = ind;
			greed = gre;
			cooperation = coo;
			creativity = cre;
			aggression = agro;
			curiosity = cur;
			health = str * 10;
			ownIndex = own;

			Console.WriteLine ("dexterity: " + dexterity + ", charisma: " + charisma + ", wisdom: " + wisdom + ", knowledge: " + knowledge + ", strength: " + strength + ", empathy: " +empathy + 
				", lawfulness: " + lawful + ", confidence: " + confidence + ", loyalty: " + loyalty + ", independence: "+ independence + ", greed: " + greed + 
				", cooperation: " + cooperation + ", creativity: " + creativity + ", aggression: " + aggression + ", curiosity: " + curiosity);

			frameHeight = newFrameHeight;
			frameWidth = newFrameWidth;
			interval = 1/dexterity * 13;
		}

		public void Load(ContentManager content){
			myTexture = content.Load<Texture2D> ("ManSheet");
			//myRectangle = new Rectangle (100, 100, 24, 24);
		}


	}
}