using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Flabola
{
	public class Obstacle
	{
		const float kGap = 300.0f;
		
		// Private variables.
		private SpriteTile[] sprites;
		private TextureInfo	textureInfo;
		private Bounds2 pipeGap;
		private bool isGapOccupied;
		private float width;
		private float height;
		
		// Public functions.
		public Obstacle(float startX, Scene scene)
		{
			textureInfo = new TextureInfo(new Texture2D("/Application/assets/zombie_pipes.png", false), new Vector2i(4,8));
			
			sprites = new SpriteTile[2];
			
			// Top
			sprites[0] = new SpriteTile(textureInfo);	
			sprites[0].Quad.S = textureInfo.TileSizeInPixelsf;
			sprites[0].TileIndex2D = new Vector2i(0, (int)RandomPosition(0f, 4f));
			// Add to the current scene.
			scene.AddChild (sprites[0]);
			
			// Bottom
			sprites[1] = new SpriteTile(textureInfo);	
			sprites[1].Quad.S = textureInfo.TileSizeInPixelsf;
			sprites[1].TileIndex2D = new Vector2i(0, (int)RandomPosition(0f, 4f));
			// Add to the current scene.
			scene.AddChild (sprites[1]);
			
			// Get sprite bounds.
			Bounds2 b = sprites[0].Quad.Bounds2();
			width = b.Point10.X;
			height = b.Point11.Y;
			
			// Position pipes.
			sprites[0].Position = new Vector2(startX, AppMain.ScreenHeight * RandomPosition(0.5f, 1.0f - height/AppMain.ScreenHeight/2));
			sprites[1].Position = new Vector2(startX, AppMain.ScreenHeight * RandomPosition(-height/AppMain.ScreenHeight/2, 0.4f));
			// Position gap rect
			pipeGap = new Bounds2(new Vector2(sprites[1].Position.X, sprites[1].Position.Y + height),
			                      new Vector2(sprites[0].Position.X + width, sprites[0].Position.Y));
			isGapOccupied = false;
			
			sprites[0].Schedule(Update);
			// Animations
			sprites[0].ScheduleInterval((dt) => {
				int tileIndex = sprites[0].TileIndex2D.X < 4 ? sprites[0].TileIndex2D.X + 1: 0;
				sprites[0].TileIndex2D = new Vector2i(sprites[0].TileIndex2D.Y, tileIndex);
			}, 0.25f);
			sprites[1].Schedule(Update);
			sprites[0].ScheduleInterval((dt) => {
				int tileIndex = sprites[1].TileIndex2D.X < 4 ? sprites[1].TileIndex2D.X + 1: 0;
				sprites[1].TileIndex2D = new Vector2i(sprites[1].TileIndex2D.Y, tileIndex);
			}, 0.25f);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float dt)
		{			
			sprites[0].Position = new Vector2(sprites[0].Position.X - 3, sprites[0].Position.Y);
			sprites[1].Position = new Vector2(sprites[1].Position.X - 3, sprites[1].Position.Y);
			pipeGap.Min = new Vector2(pipeGap.Min.X - 3, pipeGap.Min.Y);
			
			// If off the left of the viewport, loop them around.
			if(sprites[0].Position.X < -width)
			{
				sprites[0].Position = new Vector2(AppMain.ScreenWidth, AppMain.ScreenHeight * RandomPosition(0.5f, 1.0f - height/AppMain.ScreenHeight/2));
				sprites[1].Position = new Vector2(AppMain.ScreenWidth, AppMain.ScreenHeight * RandomPosition(-height/AppMain.ScreenHeight/2, 0.5f));
				pipeGap.Min = new Vector2(sprites[1].Position.X, sprites[1].Position.Y + height);
			}		
		}
		
		private float RandomPosition(float min=0.5f, float max=1.0f)
		{
			Random rand = new Random();
			double range = max - min;
			double sample = rand.NextDouble();
			
			float randomPosition = (float)(sample * range) + min;
		
			return randomPosition;
		}
		
		public bool isCollidingWith(SpriteBase sprite)
		{
			Bounds2 topRect = sprites[0].GetlContentLocalBounds();
			sprites[0].GetContentWorldBounds(ref topRect);
			Bounds2 botRect = sprites[1].GetlContentLocalBounds();
			sprites[1].GetContentWorldBounds(ref botRect);
			Bounds2 spriteRect = sprite.GetlContentLocalBounds();
			sprite.GetContentWorldBounds(ref spriteRect);
			
			bool isTopCollision = topRect.Overlaps(spriteRect);
			bool isBottomCollision = botRect.Overlaps(spriteRect);
			
			return isTopCollision || isBottomCollision;			
		}
		
		public bool isInsideGap(SpriteBase sprite)
		{
			Bounds2 spriteBounds = sprite.GetlContentLocalBounds();
			sprite.GetContentWorldBounds(ref spriteBounds);
			return pipeGap.Overlaps(spriteBounds);
		}
		
		public bool IsGapOccupied
		{
			get{ return isGapOccupied; }
			set{ isGapOccupied = value; }
		}
	}
}

