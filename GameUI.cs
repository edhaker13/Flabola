using System;
using Sce.PlayStation.HighLevel.UI;

namespace Flabola
{
	public class GameUI: Scene
	{
		private Label score;
		
		public GameUI(): base()
		{
			this.Title = "Menu UI";
			this.Transition = new CrossFadeTransition();
			// Create a score label
			score = new Label();
			score.Text = AppMain.PlayerScore.ToString();
			score.TextColor = new UIColor(.5f, .5f, .5f, 1.0f);
			score.HorizontalAlignment = HorizontalAlignment.Center;
			score.VerticalAlignment = VerticalAlignment.Middle;
			score.X = AppMain.ScreenWidth * .5f - (score.Width / 2);
			score.Y = AppMain.ScreenHeight * .05f - (score.Height / 2);
			
			this.RootWidget.AddChildLast(score);
		}
		
		protected override void OnUpdate(float elapsedTime)
		{
			base.OnUpdate(elapsedTime);
			score.Text = AppMain.PlayerScore.ToString();
		}
	}
}

