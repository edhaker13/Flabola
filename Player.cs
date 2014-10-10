using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Flabola
{
	public enum MoveStatus
	{
		Disabled = 0,
		Up,
		Down
	}
	
	public class Player
	{
		// Private variables.
		private static SpriteTile sprite;
		private static TextureInfo textureInfo;
		private static readonly float pushAmount = 5.0f;
		private static readonly float maxPushAmount = 100.0f;
		private static float yPositionBeforePush, rotateAngle;
		private static bool isAlive;
		private static MoveStatus moveState;
		
		// Public functions.
		public Player(Scene scene)
		{
			textureInfo = new TextureInfo("/Application/assets/player-sprite.png");
			sprite = new SpriteTile(textureInfo);
			sprite.Position = new Vector2(AppMain.ScreenWidth * .2f, AppMain.ScreenHeight * .5f);
			sprite.Quad.S = textureInfo.TextureSizef * 2.0f;
			sprite.Pivot = new Vector2(.5f, .5f);
			isAlive = true;
			moveState = MoveStatus.Disabled;
			rotateAngle = .0f;
			
			sprite.Schedule( (dt) =>
            {
				Vector2 pos = sprite.Position;
				// Check collision with floor
				if(pos.Y < 0)
				{
					IsAlive = false;
					return;
				}
				// Adjust jump
				switch(moveState)
				{
				case MoveStatus.Up:
					if((pos.Y - yPositionBeforePush) < maxPushAmount)
					{
						sprite.Position = new Vector2(pos.X, pos.Y + pushAmount);
						rotateAngle = dt;
					}
					else
					{
						moveState = MoveStatus.Down;
					}
					break;
				case MoveStatus.Down:
					sprite.Position = new Vector2(pos.X, pos.Y - pushAmount);
					rotateAngle = -dt;
					break;
				}
				sprite.Rotate(rotateAngle);
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
			if(moveState != MoveStatus.Up) {
				moveState = MoveStatus.Up;
				yPositionBeforePush = sprite.Position.Y;
			}
		}
	}
}

