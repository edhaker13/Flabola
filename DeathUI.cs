using System;
using Sce.PlayStation.HighLevel.UI;

namespace Flabola
{
	public class DeathUI: Scene
	{
		public static Button playButton, exitButton, highscoreButton;
		
		public DeathUI()
		{
			this.Title = "Death UI";
			this.Transition = new CrossFadeTransition();
			TextShadowSettings textShadow = new TextShadowSettings();
			textShadow.Color = new UIColor(0f, 0f, 0f, 0f);
			
			// Create a score label
			Label score = new Label();
			score.Text = AppMain.PlayerScore.ToString();
			score.TextColor = new UIColor(1f, 1f, 1f, 1f);
			score.TextShadow = textShadow;
			score.HorizontalAlignment = HorizontalAlignment.Center;
			score.VerticalAlignment = VerticalAlignment.Middle;
			score.X = AppMain.ScreenWidth * .5f - (score.Width / 2);
			score.Y = AppMain.ScreenHeight * .05f - (score.Height / 2);
			
			this.RootWidget.AddChildLast(score);
			
			// Create mesage label
			Label dead = new Label();
			dead.Text = "S-senpai...";
			dead.TextColor =  new UIColor(1f, 1f, 1f, 1f);
			dead.TextShadow = textShadow;
			dead.X = AppMain.ScreenWidth * .5f;
			dead.Y = AppMain.ScreenHeight * 0.2f;
			
			this.RootWidget.AddChildLast(dead);
			
			// Create buttons panel
			Panel buttonsPanel = new Panel();
			buttonsPanel.SetPosition(.0f, AppMain.ScreenHeight * .5f);
			
			// Create menu play button
			playButton = new Button();
			playButton.ButtonAction += HandleButtonPress;
			playButton.IconImage = new ImageAsset("/Application/assets/play.png");
			playButton.Width = playButton.Height;
			playButton.X = AppMain.ScreenWidth * .25f;
			
			buttonsPanel.AddChildLast(playButton);
			
			// Create menu exit button
			exitButton = new Button();
			exitButton.ButtonAction += HandleButtonPress;
			exitButton.IconImage = new ImageAsset("/Application/assets/exit.png");
			exitButton.Width = exitButton.Height;
			exitButton.X = AppMain.ScreenWidth * .5f;
			
			buttonsPanel.AddChildLast(exitButton);
			
			//Create menu highscore button
			highscoreButton = new Button();
			highscoreButton.IconImage = new ImageAsset("/Application/assets/highscore.png");
			highscoreButton.Width = highscoreButton.Height;
			highscoreButton.X = AppMain.ScreenWidth * .75f;
			buttonsPanel.AddChildLast(highscoreButton);
			
			this.RootWidget.AddChildLast(buttonsPanel);
		}
		~DeathUI()
		{
			playButton.Dispose();
			exitButton.Dispose();
			highscoreButton.Dispose();
		}
		
		public static void HandleButtonPress(object sender, TouchEventArgs e)
		{
			if(sender == playButton)
			{
				AppMain.PlayerScore = 0;
				UISystem.SetScene(new GameUI());
				Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.ReplaceScene(new GameScene());
				Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.Resume();
			}
			else if(sender == exitButton)
			{
				AppMain.Quit = true;
			}
			else if(sender == highscoreButton)
			{
			}
			else
			{	
			}
		}
	}
}

