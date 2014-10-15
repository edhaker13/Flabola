using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Flabola
{
	public class Obstacle
	{
		const float kGap = 200.0f;
		
		// Private variables.
		private SpriteUV[] sprites;
		private TextureInfo	textureInfoTop;
		private TextureInfo	textureInfoBottom;
		private float width;
		private float height;
		
		// Accessors.
		//public SpriteUV SpriteTop 	 { get{return sprites[0];} }
		//public SpriteUV SpriteBottom { get{return sprites[1];} }
		
		// Public functions.
		public Obstacle(float startX, Scene scene)
		{
			textureInfoTop = new TextureInfo("/Application/assets/toppipe.png");
			textureInfoBottom = new TextureInfo("/Application/assets/bottompipe.png");
			
			sprites = new SpriteUV[2];
			
			// Top
			sprites[0] = new SpriteUV(textureInfoTop);	
			sprites[0].Quad.S = textureInfoTop.TextureSizef;
			// Add to the current scene.
			scene.AddChild (sprites[0]);
			
			// Bottom
			sprites[1] = new SpriteUV(textureInfoBottom);	
			sprites[1].Quad.S = textureInfoBottom.TextureSizef;		
			// Add to the current scene.
			scene.AddChild (sprites[1]);
			
			// Get sprite bounds.
			Bounds2 b = sprites[0].Quad.Bounds2 ();
			width = b.Point10.X;
			height = b.Point01.Y;
			
			// Position pipes.
			sprites[0].Position = new Vector2(startX, AppMain.ScreenHeight * RandomPosition ());
			
			sprites[1].Position = new Vector2(startX, sprites[0].Position.Y - height - kGap);
		}
		
		public void Dispose()
		{
			textureInfoTop.Dispose ();
			textureInfoBottom.Dispose ();
		}
		
		public void Update(float deltaTime)
		{			
			sprites[0].Position = new Vector2(sprites[0].Position.X - 3, sprites[0].Position.Y);
			sprites[1].Position = new Vector2(sprites[1].Position.X - 3, sprites[1].Position.Y);
			
			// If off the left of the viewport, loop them around.
			if(sprites[0].Position.X < -width) {
				sprites[0].Position = new Vector2(AppMain.ScreenWidth, AppMain.ScreenHeight * RandomPosition ());
			
				sprites[1].Position = new Vector2(AppMain.ScreenWidth, sprites[0].Position.Y - height - kGap);
			}		
		}
		
		private float RandomPosition()
		{
			Random rand = new Random();
			float randomPosition = (float)rand.NextDouble ();
			randomPosition += 0.45f;
			
			if(randomPosition > 1.0f)
				randomPosition = 0.9f;
		
			return randomPosition;
		}
		
		public bool HasCollidedWith(SpriteBase sprite)
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
	}
}

