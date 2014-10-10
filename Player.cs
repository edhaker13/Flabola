using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Flabola
{
	public class Player
	{
		// Private variables.
		private static SpriteTile sprite;
		private static TextureInfo textureInfo;
		private static float pushAmount = 5.0f;
		private static float maxPushAmount = 100.0f;
		private static float yPositionBeforePush;
		private static bool isAlive, isRising;
		
		// Public functions.
		public Player(Scene scene)
		{
			textureInfo = new TextureInfo("/Application/assets/player-sprite.png");
			isAlive = true;
			isRising = false;
			sprite = new SpriteTile(textureInfo);
			sprite.Position = new Vector2(AppMain.ScreenWidth * .2f, AppMain.ScreenHeight * .3f);
			sprite.Quad.S = textureInfo.TextureSizef * 2.0f;
			sprite.Pivot = new Vector2(.5f, .5f);
			
			sprite.Schedule( (dt) =>
            {
				Vector2 pos = sprite.Position;
				Vector2 size = new Vector2(sprite.Quad.S.X, sprite.Quad.S.X);
				// Adjust jump
				if(isRising)
				{
					if((pos.Y - yPositionBeforePush) < maxPushAmount)
						sprite.Position = new Vector2(pos.X, pos.Y + pushAmount);
					else
						isRising = false;
				}
				else
				{
					sprite.Position = new Vector2(pos.X, pos.Y - pushAmount);
				}
			});
			// Add to the current scene.
			scene.AddChild(sprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public bool IsAlive 
		{
			get{ return isAlive; }
			set{ isAlive = value; }
		}
		
		public void Tapped()
		{
			if(!isRising) {
				isRising = true;
				yPositionBeforePush = sprite.Position.Y;
			}
		}
	}
}

