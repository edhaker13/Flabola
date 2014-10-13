using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace Flabola
{
	public class AppMain
	{
		private static int screenWidth, screenHeight;
		private static int playerScore;
		private static bool quit;
		//private static MenuUI menuUI;
		//private static GameUI gameUI;
		//private static HighscoreUI highscoreUI;
		//private static GameScene gameScene;
		
		public static void Main(string[] args)
		{
			Initialize();

			while(!Quit) {
				SystemEvents.CheckEvents();
				Update();
				Render();
			}
			Director.Terminate();
			UISystem.Terminate();
		}
		
		public static void Initialize()
		{
			// Initial player score
			playerScore = 0;
			//Set up director
			Director.Initialize();
			// Initialize UI system
			UISystem.Initialize(Director.Instance.GL.Context);
			// Retrieve screen width and height
			screenWidth = UISystem.FramebufferWidth;
			screenHeight = UISystem.FramebufferHeight;
			// Set initial scene
			UISystem.SetScene(new MenuUI(), null);
			// Run the scene.
			Director.Instance.RunWithScene(new Sce.PlayStation.HighLevel.GameEngine2D.Scene(), true);
		}

		public static void Update()
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData(0);
			// Query touch for current state
			List<TouchData> touchDataList = Touch.GetData(0);
			
			// Update scene director
			Director.Instance.Update();

			// Update UI Toolkit
			UISystem.Update(touchDataList);
		}

		public static void Render()
		{
			// Render with the scene director
			Director.Instance.Render();
			// Render the UI screen
			UISystem.Render();
			// Present the screen (via director)
			Director.Instance.GL.Context.SwapBuffers();
			Director.Instance.PostSwap();
		}
		
		public static int PlayerScore
		{
			get{ return playerScore; }
			set{ playerScore = value; }
		}
		
		public static bool Quit
		{
			get{ return quit; }
			set{ quit = value; }
		}
		
		public static int ScreenWidth
		{
			get{ return screenWidth; }
		}
		
		public static int ScreenHeight
		{
			get{ return screenHeight; }
		}
		
		private static bool InsideRect(float pixelX, float pixelY, Rectangle hitTestArea)
		{
			
			if(hitTestArea.X <= pixelX && hitTestArea.X + hitTestArea.Width >= pixelX &&
	            hitTestArea.Y <= pixelY && hitTestArea.Y + hitTestArea.Height >= pixelY) {
				return true;
			}
	
			return false;
		}
	}
}
