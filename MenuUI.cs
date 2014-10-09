using System;
using Sce.PlayStation.HighLevel.UI;

namespace Flabola
{
	public class MenuUI: Scene
	{
		private static int screenWidth, screenHeight;
		public static Button playButton, exitButton, highscoreButton;
		
		public MenuUI() : base()
		{
			// Get screen dimensions
			screenWidth = UISystem.FramebufferWidth;
			screenHeight = UISystem.FramebufferHeight;
			
			// Set the scene title
			this.Title = "Menu UI";
			
			// Create title panel
			Panel titlePanel = new Panel();
			titlePanel.Width = screenWidth;
			
			// Create title text
			Label titleLabel = new Label();
			titleLabel.Text = "Good Luck, Ebola-chan!";
			titleLabel.SetPosition(screenWidth * .37f, screenHeight * .2f);
			titleLabel.Width = screenWidth * .5f;
			titlePanel.AddChildLast(titleLabel);
			
			this.RootWidget.AddChildLast(titlePanel);
			
			// Create buttons panel
			Panel buttonsPanel = new Panel();
			buttonsPanel.SetPosition(.0f, screenHeight * .5f);
			
			// Create menu play button
			playButton = new Button();
			playButton.ButtonAction += HandleButtonPress;
			playButton.IconImage = new ImageAsset("/Application/assets/play.png");
			playButton.Width = playButton.Height;
			playButton.X = screenWidth * .25f;
			
			buttonsPanel.AddChildLast(playButton);
			
			// Create menu exit button
			exitButton = new Button();
			exitButton.ButtonAction += HandleButtonPress;
			exitButton.IconImage = new ImageAsset("/Application/assets/exit.png");
			exitButton.Width = exitButton.Height;
			exitButton.X = screenWidth * .5f;
			
			buttonsPanel.AddChildLast(exitButton);
			
			//Create menu highscore button
			highscoreButton = new Button();
			highscoreButton.IconImage = new ImageAsset("/Application/assets/highscore.png");
			highscoreButton.Width = highscoreButton.Height;
			highscoreButton.X = screenWidth * .75f;
			buttonsPanel.AddChildLast(highscoreButton);
			
			this.RootWidget.AddChildLast(buttonsPanel);
		}
		
		public static void HandleButtonPress(object sender, TouchEventArgs e)
		{
			if(sender ==playButton)
			{
				UISystem.SetScene(AppMain.GameScene, null);
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

