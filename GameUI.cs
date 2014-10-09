using System;
using Sce.PlayStation.HighLevel.UI;

namespace Flabola
{
	public class GameUI: Scene
	{
		private static int screenWidth, screenHeight;
		private Label score;
		
		public GameUI(): base()
		{
			// Get screen dimensions
			screenWidth = UISystem.FramebufferWidth;
			screenHeight = UISystem.FramebufferHeight;
			this.Title = "Menu UI";
			// Create a score label
			score = new Label();
			score.Text = AppMain.PlayerScore.ToString();
			score.TextColor = new UIColor(.5f, .5f, 1.0f, 1.0f);
			score.X = screenWidth * .5f;
			score.Y = screenHeight * .1f - (score.Height / 2);
			
			this.RootWidget.AddChildLast(score);
			
			// Add the player (just to test)
			ImageBox player = new ImageBox();
			player.Image = new ImageAsset("/Application/assets/player-sprite.png");
			player.SetSize(player.Image.Width, player.Image.Height);
			player.X = screenWidth * .5f - (player.Width / 2);
			player.Y = screenHeight * .5f - (player.Height / 2);
			this.RootWidget.AddChildLast(player);
		}
		
		protected override void OnUpdate(float elapsedTime)
		{
			base.OnUpdate(elapsedTime);
			score.Text = AppMain.PlayerScore.ToString();
		}
	}
}

