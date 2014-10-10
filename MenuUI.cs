using System;
using Sce.PlayStation.HighLevel.UI;

namespace Flabola
{
	public class MenuUI: Scene
	{
		public static Button playButton, exitButton, highscoreButton;
		
		public MenuUI() : base()
		{			
			// Set the scene title
			this.Title = "Menu UI";
			
			// Create title panel
			Panel titlePanel = new Panel();
			titlePanel.Width = AppMain.ScreenWidth;
			
			// Create title text
			Label titleLabel = new Label();
			titleLabel.Text = "Good Luck, Ebola-chan!";
			titleLabel.SetPosition(AppMain.ScreenWidth * .37f, AppMain.ScreenHeight * .2f);
			titleLabel.Width = AppMain.ScreenWidth * .5f;
			titlePanel.AddChildLast(titleLabel);
			
			this.RootWidget.AddChildLast(titlePanel);
			
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
		
		public static void HandleButtonPress(object sender, TouchEventArgs e)
		{
			if(sender == playButton)
			{
				UISystem.SetScene(AppMain.GameSceneUI, null);
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

