using System;
using Sce.PlayStation.Core.Input;
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
			//spriteList = new SpriteList();
			// Create the background
			background = new ScrollingBackground(this);
			// Create obstacles
			obstacles = new Obstacle[2];
			obstacles[0] = new Obstacle(AppMain.ScreenWidth * .5f, this);
			obstacles[1] = new Obstacle(AppMain.ScreenWidth, this);
			// Create the player
			player = new Player(this);
			//this.AddChild(spriteList);
		}
		
		public void Update()
		{
			// If tap on screen, tell player
			if(Touch.GetData(0).Count > 0)
			{
				player.Tapped();
			}
			
			// Update player if alive
			if (player.IsAlive)
			{
				background.Update(.0f);
				foreach(Obstacle obstacle in obstacles)
				{
					obstacle.Update(.0f);
				}
			}
			else
			{
				//AppMain.Quit = true;
			}
		}
	}
}

