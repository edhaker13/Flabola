using System;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Flabola
{
	public class GameScene: Scene
	{	
		//private SpriteList spriteList;
		private Obstacle[] obstacles;
		private Player player;
		private ScrollingBackground background;
		
		public GameScene(): base()
		{
			this.Camera.SetViewFromViewport();
			// Create the background
			background = new ScrollingBackground(this);
			// Create obstacles
			obstacles = new Obstacle[2];
			obstacles[0] = new Obstacle(AppMain.ScreenWidth * .5f, this);
			obstacles[1] = new Obstacle(AppMain.ScreenWidth, this);
			// Create the player
			player = new Player(this);
			//this.AddChild(spriteList);
			this.Schedule( (dt) => {				
				// Update player if alive
				if (player.IsAlive)
				{
					background.Update(.0f);
					foreach(Obstacle obstacle in obstacles)
					{
						obstacle.Update(.0f);
						if (obstacle.isCollidingWith(player.Sprite))
						{
							player.IsAlive = false;
						}
						else
						{
							if (obstacle.isInsideGap(player.Sprite))
							{
								obstacle.IsGapOccupied = true;
							}
							else if(obstacle.IsGapOccupied)
							{
								obstacle.IsGapOccupied = false;
								AppMain.PlayerScore += 1;
							}
						}
					}
				}
				else
				{
					AppMain.Quit = true;
				}
			});
		}
		~GameScene()
		{
			player.Dispose();
			background.Dispose();
			foreach(Obstacle obstacle in obstacles)
			{
				obstacle.Dispose();
			}
		}
	}
}

