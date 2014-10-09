using System;
using System.Collections.Generic;

using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace Flabola
{
	public class AppMain
	{
		private static GraphicsContext graphics;
		private static int screenWidth, screenHeight;
		private static int playerScore;
		private static bool quit;
		private static MenuUI menuUI;
		private static GameUI gameUI;
		//private static HighscoreUI highscoreUI;
		
		public static void Main(string[] args)
		{
			Initialize ();

			while (!quit)
			{
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
			}
		}
		
		public static int PlayerScore
		{
			get{ return playerScore; }
		}
		
		public static bool Quit
		{
			get{ return quit; }
			set{ quit = value; }
		}
		
		public static MenuUI MenuScene
		{
			get{ return menuUI; }
		}
		
		public static GameUI GameScene
		{
			get{ return gameUI; }
		}
		
		public static void Initialize()
		{
			// Initial player score
			playerScore = 0;
			// Set up the graphics system
			graphics = new GraphicsContext ();
			// Initialize UI system
			UISystem.Initialize(graphics);
			// Retrieve screen width and height
			screenWidth = UISystem.FramebufferWidth;
			screenHeight = UISystem.FramebufferHeight;
			// Create menu scene
			menuUI = new MenuUI();
			
			// Create game scene
			gameUI = new GameUI();
			
			// Set initial scene
			UISystem.SetScene(menuUI, null);
		}

		public static void Update()
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
			// Query touch for current state
            List<TouchData> touchDataList = Touch.GetData (0);

            // Update UI Toolkit
            UISystem.Update(touchDataList);
		}

		public static void Render()
		{
			// Clear the screen
			graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			graphics.Clear ();
			// Render the UI screen
			UISystem.Render();
			// Present the screen
			graphics.SwapBuffers ();
		}
		
	    private static bool InsideRect(float pixelX, float pixelY, Rectangle hitTestArea)
	    {
			
	        if (hitTestArea.X <= pixelX && hitTestArea.X + hitTestArea.Width >= pixelX &&
	            hitTestArea.Y <= pixelY && hitTestArea.Y + hitTestArea.Height >= pixelY) {
	            return true;
	        }
	
	        return false;
	    }
	}
}
