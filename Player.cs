using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

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
		private SpriteTile sprite;
		private static TextureInfo textureInfo;
		private static readonly float pushAmount = 5.0f;
		private static readonly float maxPushAmount = 100.0f;
		private float yPositionBeforePush, rotateAngle;
		private bool isAlive;
		private MoveStatus moveState;
		
		// Public functions.
		public Player(Scene scene)
		{
			textureInfo = new TextureInfo(new Texture2D("/Application/assets/player-spritemap.png", false), new Vector2i(13,1));
			sprite = new SpriteTile(textureInfo);
			sprite.Position = new Vector2(AppMain.ScreenWidth * .2f, AppMain.ScreenHeight * .5f);
			sprite.Quad.S = textureInfo.TileSizeInPixelsf;
			sprite.CenterSprite(TRS.Local.Center);
			// Player variables
			isAlive = true;
			moveState = MoveStatus.Disabled;
			rotateAngle = .0f;
			
			// Attach update fucntion to scheduler
			sprite.Schedule(Update);
			
			// Create animation function
			sprite.ScheduleInterval( (dt) => {
				if(IsAlive)
				{
					int tileIndex = sprite.TileIndex1D < 8 ? sprite.TileIndex1D + 1 : 1;
					sprite.TileIndex1D = tileIndex;
				}
			}, 0.2f);
			sprite.ScheduleInterval((dt) => {
				if(!IsAlive)
				{
					int tileIndex = sprite.TileIndex1D < 12 ? sprite.TileIndex1D + 1 : 12;
					sprite.TileIndex1D = tileIndex;
				}
			}, 0.16f);
			
			// Add to the current scene.
			scene.AddChild(sprite);
		}
		
		public void Update(float dt)
		{
			// If tap on screen, tell player
			if(Touch.GetData(0).Count > 0)
			{
				this.Tapped();
			}
			
			// Check collision with floor
			Vector2 pos = sprite.Position;
			if(pos.Y <= sprite.Scale.Y / 2)
			{
				IsAlive = false;
				return;
			}
			
			// Disable jump on end of death animation
			if (IsDead)
			{
				moveState = MoveStatus.Disabled;
			}
			
			// Jump variables
			float maxAngle = Math.Deg2Rad(15f);
			float minAngle = -maxAngle;
			// Adjust jump direction and angle
			switch(moveState)
			{
			case MoveStatus.Up:
				if((pos.Y - yPositionBeforePush) < maxPushAmount)
				{
					sprite.Position = new Vector2(pos.X, pos.Y + pushAmount);
					rotateAngle += dt;
				}
				else
				{
					moveState = MoveStatus.Down;
				}
				break;
			case MoveStatus.Down:
				sprite.Position = new Vector2(pos.X, pos.Y - pushAmount);
				rotateAngle -= dt;
				break;
			}
			
			// Clip angle bounds
			if(rotateAngle > maxAngle)
				rotateAngle -= dt;
			else if (rotateAngle < minAngle)
				rotateAngle += dt;
			
			// Apply the rotation
			sprite.Rotation = Vector2.Rotation(rotateAngle);
		}
		
		public bool IsAlive 
		{
			get{ return isAlive; }
			set{ isAlive = value; }
		}
		
		public bool IsDead
		{
			get{ return !isAlive && sprite.TileIndex1D == 12; }
		}
		
		public SpriteTile Sprite
		{
			get { return sprite; }
		}
		
		public void Tapped()
		{
			if(moveState != MoveStatus.Up)
			{
				moveState = MoveStatus.Up;
				yPositionBeforePush = sprite.Position.Y;
			}
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
	}
}

