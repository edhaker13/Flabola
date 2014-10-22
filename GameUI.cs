using System;
using Sce.PlayStation.HighLevel.UI;

namespace Flabola
{
	public class GameUI: Scene
	{
		private Label score;
		
		public GameUI(): base()
		{
			this.Title = "Game UI";
			this.Transition = new CrossFadeTransition();
			TextShadowSettings textShadow = new TextShadowSettings();
			textShadow.Color = new UIColor(0f, 0f, 0f, 0f);
			// Create a score label
			score = new Label();
			score.Text = AppMain.PlayerScore.ToString();
			score.TextColor = new UIColor(1f, 1f, 1f, 1f);
			score.TextShadow = textShadow;
			score.HorizontalAlignment = HorizontalAlignment.Center;
			score.VerticalAlignment = VerticalAlignment.Middle;
			score.X = AppMain.ScreenWidth * .5f - (score.Width / 2);
			score.Y = AppMain.ScreenHeight * .05f - (score.Height / 2);
			
			this.RootWidget.AddChildLast(score);
		}
		
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			score.Text = AppMain.PlayerScore.ToString();
		}
	}
}

