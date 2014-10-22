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
			// Attach update function to Director scheduler
			this.ScheduleUpdate();
		}
		
		public override void Update(float dt)
		{
			base.Update(dt);
			// Update player if alive
			if (player.IsAlive)
			{
				foreach(Obstacle obstacle in obstacles)
				{
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
				if(player.IsDead)
				{
					Director.Instance.Pause();
				}
			}
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

