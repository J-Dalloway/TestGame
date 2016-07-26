using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public class Intelligence
	{
		Random random = new Random();
		Vector2 distance;




		public Intelligence ()
		{
		}


		public bool CheckSpace(float x, float y, int distance){
			foreach (Resource r in Game1.rocks) {
				if ((x <= r.position.X + distance && x >= r.position.X - distance) && (y <= r.position.Y + distance && y >= r.position.Y - distance)) {
					Console.WriteLine ("Failed");
					return false;

				}
			}
			foreach (Resource r in Game1.trees) {
				if ((x <= r.position.X + distance && x >= r.position.X - distance) && (y <= r.position.Y + distance && y >= r.position.Y - distance)) {
					Console.WriteLine ("Failed");
					return false;
				}
			}
			for(int i = 0; i <= Game1.fieldIndex;i++) {
				if (Game1.fields [i] != null) {
					if ((x <= Game1.fields [i].position.X + distance && x >= Game1.fields [i].position.X - distance) && (y <= Game1.fields [i].position.Y + distance && y >= Game1.fields [i].position.Y - distance)) {
						Console.WriteLine ("Failed");
						return false;

					}
				}
			}
			for(int i = 0; i <= Game1.smithIndex;i++) {
				if (Game1.smiths [i] != null) {
					if ((x <= Game1.smiths [i].position.X + distance && x >= Game1.smiths [i].position.X - distance) && (y <= Game1.smiths [i].position.Y + distance && y >= Game1.smiths [i].position.Y - distance)) {
						Console.WriteLine ("Failed");
						return false;

					}
				}
			}
			Console.WriteLine ("Position found at " + x + " " + y);
			return true;

		}

		public void PlantField(Manv2 manv2){
			manv2.hasTask = true;
			manv2.sillyTimerMax = 1000;

			do{ manv2.targetPosition.X = random.Next(48, 3952);
				manv2.targetPosition.Y = random.Next(48, 3952);
			}while(!CheckSpace(manv2.targetPosition.X, manv2.targetPosition.Y, 74));

			manv2.tools--;
			Game1.fieldIndex++;
			Game1.fields [Game1.fieldIndex].position.X = manv2.targetPosition.X;
			Game1.fields [Game1.fieldIndex].position.Y = manv2.targetPosition.Y;
			Game1.fields [Game1.fieldIndex].inPlay = true;

			manv2.field = Game1.fields [Game1.fieldIndex];
		}

		public void WorkField(Manv2 manv2){
			Console.WriteLine ("Farming");
			manv2.hasTask = true;
			manv2.sillyTimerMax = 100000;

			manv2.targetPosition.X = manv2.field.position.X;
			manv2.targetPosition.Y = manv2.field.position.Y;

			manv2.tools--;
			manv2.food += 10;
		}

		public void Farmer(Manv2 manv2, GameTime gameTime){
			if (manv2.field == null && manv2.tools > 0 && random.Next(0, 10000) + manv2.wisdom > 10000) {
				PlantField (manv2);
			}else if (!manv2.hasTask) {
				if(manv2.field != null && manv2.tools > 0 && random.Next(0, 10000) + manv2.wisdom > 10000){
					WorkField(manv2);
				}
				else if (random.Next (0, 80) == 0) {
					do {
						manv2.targetIndex = random.Next (0, Game1.MANCOUNT);
					} while(Game1.npcs [manv2.targetIndex] == null);

					Talk (manv2, Game1.npcs [manv2.targetIndex]);

				} else if (random.Next (0, 60) == 0) {
					Wander (manv2);
				} else {
					Wait (manv2);
				}
			} else {
				if(manv2.currentTask.Equals("talking")){
					UpdateDestination (manv2);
				}
				Move (manv2, gameTime);
			}
		
		}

		public void Miner(Manv2 manv2, GameTime gameTime){
			if (manv2.field == null && manv2.tools > 0 && random.Next(0, 10000) + manv2.wisdom > 10000) {
				MineRock (manv2);
			}else if (!manv2.hasTask) {
				if (random.Next (0, 80) == 0) {
					do {
						manv2.targetIndex = random.Next (0, Game1.MANCOUNT);
					} while(Game1.npcs [manv2.targetIndex] == null);

					Talk (manv2, Game1.npcs [manv2.targetIndex]);

				} else if (random.Next (0, 60) == 0) {
					Wander (manv2);
				} else {
					Wait (manv2);
				}
			} else {
				if(manv2.currentTask.Equals("talking")){
					UpdateDestination (manv2);
				}
				Move (manv2, gameTime);
			}
		}

		public void MineRock(Manv2 manv2){
			Console.WriteLine ("Mining");
			manv2.hasTask = true;
			manv2.sillyTimerMax = 1000;
			int index;
			do{index = random.Next (0, Game1.ROCKCOUNT);
			}while(Game1.rocks[index].amount <= 0);

			manv2.targetPosition.X = Game1.rocks[index].position.X;
			manv2.targetPosition.Y = Game1.rocks[index].position.Y;

			manv2.targetIndex = index;

			manv2.currentTask = "mining";

			manv2.tools--;
			manv2.stone += 10;
			Game1.rocks [index].amount -= 10;

		}

		public void Lumberjack(Manv2 manv2, GameTime gameTime){
			if (manv2.field == null && manv2.tools > 0 && random.Next(0, 10000) + manv2.wisdom > 10000) {
				CutTree (manv2);
			}else if (!manv2.hasTask) {
				if (random.Next (0, 80) == 0) {
					do {
						manv2.targetIndex = random.Next (0, Game1.MANCOUNT);
					} while(Game1.npcs [manv2.targetIndex] == null);

					Talk (manv2, Game1.npcs [manv2.targetIndex]);

				} else if (random.Next (0, 60) == 0) {
					Wander (manv2);
				} else {
					Wait (manv2);
				}
			} else {
				if(manv2.currentTask.Equals("talking")){
					UpdateDestination (manv2);
				}
				Move (manv2, gameTime);
			}
		}
		public void CutTree(Manv2 manv2){
			Console.WriteLine ("Lumber");
			manv2.hasTask = true;
			manv2.sillyTimerMax = 1000;
			int index;
			do{index = random.Next (0, Game1.TREECOUNT);
			}while(Game1.trees[index].amount <= 0);

			manv2.targetPosition.X = Game1.trees[index].position.X;
			manv2.targetPosition.Y = Game1.trees[index].position.Y;

			manv2.targetIndex = index;
			manv2.currentTask = "wooding";

			manv2.tools--;
			manv2.wood += 10;
			Game1.trees [index].amount -= 10;
		}

		public void Smithy(Manv2 manv2, GameTime gameTime){
			if (manv2.smith == null && manv2.tools > 1 && random.Next(0, 10000) + manv2.wisdom > 10000) {
				OrderBuild (0, manv2.ownIndex);
			}else if (!manv2.hasTask) {
				if (random.Next (0, 80) == 0) {
					do {
						manv2.targetIndex = random.Next (0, Game1.MANCOUNT);
					} while(Game1.npcs [manv2.targetIndex] == null);

					Talk (manv2, Game1.npcs [manv2.targetIndex]);

				} else if (random.Next (0, 60) == 0) {
					Wander (manv2);
				} else {
					Wait (manv2);
				}
			} else {
				if(manv2.currentTask.Equals("talking")){
					UpdateDestination (manv2);
				}
				Move (manv2, gameTime);
			}
		}

		public void BuildSmith(Manv2 manv2){
			manv2.hasTask = true;
			manv2.currentTask = "building smith";
			manv2.sillyTimerMax = 1000;

			do{ manv2.targetPosition.X = random.Next(48, 3830);
				manv2.targetPosition.Y = random.Next(48, 3830);
			}while(!CheckSpace(manv2.targetPosition.X, manv2.targetPosition.Y, 170));

			manv2.tools--;
			Game1.smithIndex++;
			Game1.smiths [Game1.smithIndex].position.X = manv2.targetPosition.X;
			Game1.smiths [Game1.smithIndex].position.Y = manv2.targetPosition.Y;


			Console.WriteLine ("Building smith at " + manv2.targetPosition.X + " " + manv2.targetPosition.Y);
		}

		public void OrderBuild(int type, int owner){
			Console.WriteLine ("build order placed");
			Console.WriteLine ("current orders = " + Game1.currentBuildOrders);
			Game1.buildIndex++;
			if (Game1.buildIndex == 100) {
				Game1.buildIndex = 0;
			}
			if (Game1.buildOrders[Game1.buildIndex] == null || Game1.buildOrders[Game1.buildIndex].completed == true) {
				Game1.buildOrders [Game1.buildIndex] = new BuildOrder (type, owner);
				Game1.currentBuildOrders++;
			}
		}

		public void CompleteBuild(Manv2 manv2){
			Console.WriteLine ("attempting build");
			if (Game1.buildOrders [Game1.startBuildIndex] != null) {
				manv2.buildNumber = Game1.startBuildIndex;
				Game1.startBuildIndex++;
				Game1.currentBuildOrders--;
				if (Game1.startBuildIndex == 100) {
					Game1.startBuildIndex = 0;
				}
				if (Game1.buildOrders [manv2.buildNumber].type == 0) {
					BuildSmith (manv2);
				}
			}
		}

		public void Builder(Manv2 manv2, GameTime gameTime){
			if (!manv2.hasTask && Game1.currentBuildOrders > 0 && random.Next(0, 15000) + manv2.wisdom > 10000) {
				CompleteBuild (manv2);
			}
			else if (!manv2.hasTask) {
				if (random.Next (0, 80) == 0) {
					do {
						manv2.targetIndex = random.Next (0, Game1.MANCOUNT);
					} while(Game1.npcs [manv2.targetIndex] == null);

					Talk (manv2, Game1.npcs [manv2.targetIndex]);

				} else if (random.Next (0, 60) == 0) {
					Wander (manv2);
				} else {
					Wait (manv2);
				}
			} else {
				if(manv2.currentTask.Equals("talking")){
					UpdateDestination (manv2);
				}
				Move (manv2, gameTime);
			}
		}

		public void FindTask(Manv2 manv2, GameTime gameTime){

			if (manv2.currentProfession == 0) {
				Farmer (manv2, gameTime);
			} else if (manv2.currentProfession == 1) {
				Miner (manv2, gameTime);
			} else if (manv2.currentProfession == 2) {
				Lumberjack (manv2, gameTime);
			} else if (manv2.currentProfession == 3) {
				Smithy (manv2, gameTime);
			} else if (manv2.currentProfession == 4) {
				Builder (manv2, gameTime);
			}else {

				if (!manv2.hasTask) {
					if (random.Next (0, 80) == 0) {
						do {
							manv2.targetIndex = random.Next (0, Game1.MANCOUNT);
						} while(Game1.npcs [manv2.targetIndex] == null);

						Talk (manv2, Game1.npcs [manv2.targetIndex]);

					} else if (random.Next (0, 60) == 0) {
						Wander (manv2);
					} else {
						Wait (manv2);
					}
				} else {
					if(manv2.currentTask.Equals("talking")){
						UpdateDestination (manv2);
					}
					Move (manv2, gameTime);
				}
			}
		}

		public void Wait(Manv2 manv2){
			manv2.targetPosition.X = manv2.position.X;
			manv2.targetPosition.Y = manv2.position.Y;

		}

		public void Wander(Manv2 manv2){

			manv2.hasTask = true;
			manv2.targetPosition.X = random.Next (0, 4000);
			manv2.targetPosition.Y = random.Next (0, 4000);
			manv2.sillyTimerMax = 50;

		}

		public void UpdateDestination(Manv2 manv2){
			if (manv2.targetIndex != -1 && manv2.targetIndex < Game1.MANCOUNT && Game1.npcs [manv2.targetIndex] != null && Game1.npcs [manv2.targetIndex].targetIndex != -1) {
				manv2.targetPosition.X = Game1.npcs [manv2.targetIndex].position.X;
				manv2.targetPosition.Y = Game1.npcs [manv2.targetIndex].position.Y;

			} else {
				manv2.currentTask = "";
				manv2.hasTask = false;
				manv2.targetIndex = -1;
				Wait (manv2);
			}
		}

		public void Talk(Manv2 manv2a, Manv2 manv2b){
			if (manv2b.hasTask) {
				manv2a.targetIndex = -1;
				Wander (manv2a);
			} else {
				Console.WriteLine ("talking");
				manv2a.hasTask = true;
				manv2b.hasTask = true;

				manv2a.currentTask = "talking";
				manv2b.currentTask = "talking";

				manv2a.targetPosition.X = manv2b.position.X;
				manv2a.targetPosition.Y = manv2b.position.Y;
				manv2b.targetPosition.X = manv2a.position.X;
				manv2b.targetPosition.Y = manv2a.position.Y;

				manv2a.targetIndex = manv2b.ownIndex;
				manv2b.targetIndex = manv2a.ownIndex;

				manv2a.sillyTimerMax = 500;
				manv2b.sillyTimerMax = 500;
			}
		}

		public void Move(Manv2 manv2, GameTime gameTime){
			manv2.rectangle = new Rectangle (manv2.currentFrame * manv2.frameWidth, 0, 24, 24);

			manv2.position = manv2.velocity + manv2.position;
			distance.X = manv2.targetPosition.X - manv2.position.X;
			distance.Y = manv2.targetPosition.Y - manv2.position.Y;

			manv2.rotation = (float)Math.Atan2 (distance.Y, distance.X);


			if (manv2.position.X >= manv2.targetPosition.X - 26 && manv2.position.X <= manv2.targetPosition.X + 26 && manv2.position.Y >= manv2.targetPosition.Y - 26 && manv2.position.Y <= manv2.targetPosition.Y + 26) {
				manv2.currentFrame = 0;

				manv2.velocity.X = 0;
				manv2.velocity.Y = 0;


				manv2.sillyTimer++;
				if (manv2.sillyTimer >= manv2.sillyTimerMax) {
					if (manv2.currentTask.Equals ("mining")) {
						if (Game1.rocks [manv2.targetIndex].amount <= 0) {
							Game1.rocks [manv2.targetIndex].inPlay = false;
							Game1.rocks [manv2.targetIndex].position.X = -2000;
							Game1.rocks [manv2.targetIndex].position.Y = -2000;
						}
					} else if (manv2.currentTask.Equals ("talking")) {
						

					}else if(manv2.currentTask.Equals ("building smith")){
						Game1.smiths [Game1.smithIndex].inPlay = true;

						Game1.npcs[Game1.buildOrders[manv2.buildNumber].owner].smith = Game1.smiths [Game1.smithIndex];
						Game1.buildOrders [manv2.buildNumber].completed = true;

					} else if (manv2.currentTask.Equals ("wooding")) {
						if (Game1.trees [manv2.targetIndex].amount <= 0) {
							Game1.trees [manv2.targetIndex].inPlay = false;
							Game1.trees [manv2.targetIndex].currentFrame = 0;
						}
					}
					manv2.hasTask = false;
					manv2.targetIndex = -1;
					manv2.sillyTimer = 0;
					manv2.currentTask = "";
				}

			} else {
				manv2.AnimateMove (gameTime);
				manv2.velocity.X = (float)Math.Cos (manv2.rotation) * manv2.dexterity;
				manv2.velocity.Y = (float)Math.Sin (manv2.rotation) * manv2.dexterity;
			}

		}

		public void FindProfession(Manv2 manv2){
			String result = "";
			if (manv2.strength >= 3 && manv2.knowledge >= 3)
			{
				result += "farmer ";
				manv2.professions[0] = 1;
				manv2.totalSkills++;
			}
			if (manv2.strength >= 5)
			{
				result += "miner ";
				manv2.professions[1] = 1;
				manv2.totalSkills++;
				result += "lumberjack ";
				manv2.professions[2] = 1;
				manv2.totalSkills++;
			}
			if (manv2.strength >= 6 && manv2.knowledge >= 5)
			{
				result += "blacksmith ";
				manv2.professions[3] = 1;
				manv2.totalSkills++;
			}
			if (manv2.strength >= 5 && manv2.knowledge >= 5)
			{
				result += "builder ";
				manv2.professions[4] = 1;
				manv2.totalSkills++;
			}
			if (manv2.strength >= 4 && manv2.dexterity >= 0.6f)
			{
				result += "laborer ";
				manv2.professions[5] = 1;
				manv2.totalSkills++;
			}
			if (manv2.charisma >= 4)
			{
				result += "barkeep ";
				manv2.professions[6] = 1;
				manv2.totalSkills++;
			}
			if (manv2.strength >= 7 && manv2.aggression >= 4)
			{
				result += "security ";
				manv2.professions[7] = 1;
				manv2.totalSkills++;
			}
			if (manv2.charisma >= 8 && manv2.lawful >= 7)
			{
				result += "politician ";
				manv2.professions[8] = 1;
				manv2.totalSkills++;
			}
			if (manv2.charisma >= 5 && manv2.wisdom >= 4 && manv2.strength >= 5 && manv2.lawful >= 6 && manv2.aggression >= 3)
			{
				result += "police ";
				manv2.professions[9] = 1;
				manv2.totalSkills++;
			}
			if (manv2.empathy >= 6 && manv2.knowledge >= 7 && manv2.wisdom >= 5)
			{
				result += "doctor ";
				manv2.professions[10] = 1;
				manv2.totalSkills++;
			}
			if (manv2.loyalty >= 6 && manv2.strength >= 3 && manv2.cooperation >= 4)
			{
				result += "servant ";
				manv2.professions[11] = 1;
				manv2.totalSkills++;
			}
			if (manv2.strength >= 7 && manv2.dexterity >= 1f && manv2.loyalty >= 6 && manv2.aggression >= 7)
			{
				result += "soldier ";
				manv2.professions[12] = 1;
				manv2.totalSkills++;
			}
			if (manv2.greed >= 7 && manv2.charisma >= 8 && manv2.lawful <= 4)
			{
				result += "ConMan ";
				manv2.professions[13] = 1;
				manv2.totalSkills++;
			}
			if (manv2.charisma >= 4 && manv2.wisdom >= 7 && manv2.knowledge >= 7)
			{
				result += "manager ";
				manv2.professions[14] = 1;
				manv2.totalSkills++;
			}
			if (manv2.charisma >= 7 && manv2.knowledge >= 5 && manv2.wisdom >= 4)
			{
				result += "trader ";
				manv2.professions[15] = 1;
				manv2.totalSkills++;
			}
			if (manv2.creativity >= 7)
			{
				result += "artist ";
				manv2.professions[16] = 1;
				manv2.totalSkills++;
			}
			if (manv2.creativity >= 5 && manv2.charisma >= 6 && manv2.confidence >= 5)
			{
				result += "performer ";
				manv2.professions[17] = 1;
				manv2.totalSkills++;
			}
			if (manv2.greed >= 7 && manv2.lawful <= 4 && manv2.empathy <= 4)
			{
				result += "thief ";
				manv2.professions[18] = 1;
				manv2.totalSkills++;
			}
			if (manv2.lawful <= 2 && manv2.aggression >= 8 && manv2.empathy <= 2)
			{
				result += "murderer ";
				manv2.professions[19] = 1;
				manv2.totalSkills++;
			}
			if (manv2.charisma >= 7 && manv2.wisdom >= 7 && manv2.lawful <= 3)
			{
				result += "smuggler ";
				manv2.professions[20] = 1;
				manv2.totalSkills++;
			}
			if (manv2.charisma >= 8 && manv2.lawful <= 2 && manv2.wisdom >= 7 && manv2.knowledge >= 6)
			{
				result += "mob boss ";
				manv2.professions[21] = 1;
				manv2.totalSkills++;
			}
			if (manv2.curiosity >= 7 && manv2.dexterity >= 1.25f)
			{
				result += "explorer ";
				manv2.professions[22] = 1;
				manv2.totalSkills++;
			}
			if (manv2.knowledge >= 8 && manv2.creativity >= 7 && manv2.wisdom >= 5)
			{
				result += "scientist ";
				manv2.professions[23] = 1;
				manv2.totalSkills++;
			}
			if (manv2.empathy >= 8 && manv2.lawful <= 3)
			{
				result += "vigilante ";
				manv2.professions[24] = 1;
				manv2.totalSkills++;
			}
			if (manv2.empathy >= 8 && manv2.lawful >= 5)
			{
				result += "welfare ";
				manv2.professions[25] = 1;
				manv2.totalSkills++;
			}
			if (manv2.knowledge >= 7)
			{
				result += "scholar ";
				manv2.professions[26] = 1;
				manv2.totalSkills++;
			}
			if (manv2.wisdom >= 8 && manv2.strength <= 5)
			{
				result += "wiseman ";
				manv2.professions[27] = 1;
				manv2.totalSkills++;
			}
			if (manv2.wisdom >= 5 && manv2.knowledge >= 7 && manv2.charisma >= 4)
			{
				result += "teacher ";
				manv2.professions[28] = 1;
				manv2.totalSkills++;
			}
			if (result.Equals (""))
				result += "NO PROFESSION";

			Console.WriteLine (result + " " + manv2.totalSkills);
			Console.WriteLine ();

			if (manv2.totalSkills == 1)
			{
				for (int i = 0; i < 29; i++)
				{
					if (manv2.professions[i] == 1)
					{
						manv2.currentProfession = i;
						Game1.totalProfessions[i]++;
					}
				}
			}
			
		}


	}
}

