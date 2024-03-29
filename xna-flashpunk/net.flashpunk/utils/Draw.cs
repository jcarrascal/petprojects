﻿namespace net.flashpunk.utils 
{
	using flash.display.Texture2D;
	using flash.display.Graphics;
	using flash.display.LineScaleMode;
	using flash.geom.Matrix;
	using flash.geom.Vector2;
	using flash.geom.Rectangle;
	using net.flashpunk.Entity;
	using net.flashpunk.FP;
	using net.flashpunk.Graphic;
	
	/**
	 * Static class with access to miscellanious drawing functions.
	 * These functions are not meant to replace Graphic components
	 * for Entities, but rather to help with testing and debugging.
	 */
	public class Draw 
	{
		/**
		 * The blending mode used by Draw functions. This will not
		 * apply to Draw.line(), but will apply to Draw.linePlus().
		 */
		public static string blend;
		
		/**
		 * Sets the drawing target for Draw functions.
		 * @param	target		The buffer to draw to.
		 * @param	camera		The camera offset (use null for none).
		 * @param	blend		The blend mode to use.
		 */
		public static void setTarget(Texture2D target, Vector2 camera = null, string blend = null)
		{
			_target = target;
			_camera = camera ? camera : FP.zero;
			Draw.blend = blend;
		}
		
		/**
		 * Resets the drawing target to the default. The same as calling Draw.setTarget(FP.buffer, FP.camera).
		 */
		public static void resetTarget()
		{
			_target = FP.buffer;
			_camera = FP.camera;
			Draw.blend = null;
		}
		
		/**
		 * Draws a pixelated, non-antialiased line.
		 * @param	x1		Starting x position.
		 * @param	y1		Starting y position.
		 * @param	x2		Ending x position.
		 * @param	y2		Ending y position.
		 * @param	color	Color of the line.
		 */
		public static void line(int x1, int y1, int x2, int y2, uint color = 0xFFFFFF)
		{
			if (color < 0xFF000000) color = 0xFF000000 | color;
			
			// get the drawing positions
			x1 -= _camera.x;
			y1 -= _camera.y;
			x2 -= _camera.x;
			y2 -= _camera.y;
			
			// get the drawing difference
			Texture2D screen = _target,
				float X = Math.abs(x2 - x1),
				float Y = Math.abs(y2 - y1),
				int xx,
				int yy;
			
			// draw a single pixel
			if (X == 0)
			{
				if (Y == 0)
				{
					screen.setPixel32(x1, y1, color);
					return;
				}
				// draw a straight vertical line
				yy = y2 > y1 ? 1 : -1;
				while (y1 != y2)
				{
					screen.setPixel32(x1, y1, color);
					y1 += yy;
				}
				screen.setPixel32(x2, y2, color);
				return;
			}
			
			if (Y == 0)
			{
				// draw a straight horizontal line
				xx = x2 > x1 ? 1 : -1;
				while (x1 != x2)
				{
					screen.setPixel32(x1, y1, color);
					x1 += xx;
				}
				screen.setPixel32(x2, y2, color);
				return;
			}
			
			xx = x2 > x1 ? 1 : -1;
			yy = y2 > y1 ? 1 : -1;
			float c = 0,
				float slope;
			
			if (X > Y)
			{
				slope = Y / X;
				c = .5;
				while (x1 != x2)
				{
					screen.setPixel32(x1, y1, color);
					x1 += xx;
					c += slope;
					if (c >= 1)
					{
						y1 += yy;
						c -= 1;
					}
				}
				screen.setPixel32(x2, y2, color);
			}
			else
			{
				slope = X / Y;
				c = .5;
				while (y1 != y2)
				{
					screen.setPixel32(x1, y1, color);
					y1 += yy;
					c += slope;
					if (c >= 1)
					{
						x1 += xx;
						c -= 1;
					}
				}
				screen.setPixel32(x2, y2, color);
			}
		}
		
		/**
		 * Draws a smooth, antialiased line with optional alpha and thickness.
		 * @param	x1		Starting x position.
		 * @param	y1		Starting y position.
		 * @param	x2		Ending x position.
		 * @param	y2		Ending y position.
		 * @param	color	Color of the line.
		 * @param	alpha	Alpha of the line.
		 * @param	thick	The thickness of the line.
		 */
		public static void linePlus(int x1, int y1, int x2, int y2, uint color = 0xFF000000, float alpha = 1, float thick = 1)
		{
			_graphics.clear();
			_graphics.lineStyle(thick, color, alpha, false, LineScaleMode.NONE);
			_graphics.moveTo(x1 - _camera.x, y1 - _camera.y);
			_graphics.lineTo(x2 - _camera.x, y2 - _camera.y);
			_target.draw(FP.sprite, null, null, blend);
		}
		
		/**
		 * Draws a filled rectangle.
		 * @param	x			X position of the rectangle.
		 * @param	y			Y position of the rectangle.
		 * @param	width		Width of the rectangle.
		 * @param	height		Height of the rectangle.
		 * @param	color		Color of the rectangle.
		 * @param	alpha		Alpha of the rectangle.
		 */
		public static void rect(int x, int y, uint width, uint height, uint color = 0xFFFFFF, float alpha = 1)
		{
			if (alpha >= 1 && !blend)
			{
				if (color < 0xFF000000) color = 0xFF000000 | color;
				_rect.x = x - _camera.x;
				_rect.y = y - _camera.y;
				_rect.width = width;
				_rect.height = height;
				_target.fillRect(_rect, color);
				return;
			}
			if (color >= 0xFF000000) color = 0xFFFFFF & color;
			_graphics.clear();
			_graphics.beginFill(color, alpha);
			_graphics.drawRect(x - _camera.x, y - _camera.y, width, height);
			_target.draw(FP.sprite, null, null, blend);
		}
		
		/**
		 * Draws a non-filled, pixelated circle.
		 * @param	x			Center x position.
		 * @param	y			Center y position.
		 * @param	radius		Radius of the circle.
		 * @param	color		Color of the circle.
		 */
		public static void circle(int x, int y, int radius, uint color = 0xFFFFFF)
		{
			if (color < 0xFF000000) color = 0xFF000000 | color;
			x -= _camera.x;
			y -= _camera.y;
			int f = 1 - radius,
				int fx = 1,
				int fy = -2 * radius,
				int xx = 0,
				int yy = radius;
			_target.setPixel32(x, y + radius, color);
			_target.setPixel32(x, y - radius, color);
			_target.setPixel32(x + radius, y, color);
			_target.setPixel32(x - radius, y, color);
			while (xx < yy)
			{
				if (f >= 0) 
				{
					yy --;
					fy += 2;
					f += fy;
				}
				xx ++;
				fx += 2;
				f += fx;    
				_target.setPixel32(x + xx, y + yy, color);
				_target.setPixel32(x - xx, y + yy, color);
				_target.setPixel32(x + xx, y - yy, color);
				_target.setPixel32(x - xx, y - yy, color);
				_target.setPixel32(x + yy, y + xx, color);
				_target.setPixel32(x - yy, y + xx, color);
				_target.setPixel32(x + yy, y - xx, color);
				_target.setPixel32(x - yy, y - xx, color);
			}
		}
		
		/**
		 * Draws a circle to the screen.
		 * @param	x			X position of the circle's center.
		 * @param	y			Y position of the circle's center.
		 * @param	radius		Radius of the circle.
		 * @param	color		Color of the circle.
		 * @param	alpha		Alpha of the circle.
		 * @param	fill		If the circle should be filled with the color (true) or just an outline (false).
		 * @param	thick		How thick the outline should be (only applicable when fill = false).
		 */
		public static void circlePlus(int x, int y, float radius, uint color = 0xFFFFFF, float alpha = 1, bool fill = true, int thick = 1)
		{
			_graphics.clear();
			if (fill)
			{
				_graphics.beginFill(color & 0xFFFFFF, alpha);
				_graphics.drawCircle(x - _camera.x, y - _camera.y, radius);
				_graphics.endFill();
			}
			else
			{
				_graphics.lineStyle(thick, color & 0xFFFFFF, alpha);
				_graphics.drawCircle(x - _camera.x, y - _camera.y, radius);
			}
			_target.draw(FP.sprite, null, null, blend);
		}
		
		/**
		 * Draws the Entity's hitbox.
		 * @param	e			The Entity whose hitbox is to be drawn.
		 * @param	outline		If just the hitbox's outline should be drawn.
		 * @param	color		Color of the hitbox.
		 * @param	alpha		Alpha of the hitbox.
		 */
		public static void hitbox(Entity e, bool outline = true, uint color = 0xFFFFFF, float alpha = 1)
		{
			if (outline)
			{
				if (color < 0xFF000000) color = 0xFF000000 | color;
				int x = e.x - e.originX - _camera.x,
					int y = e.y - e.originY - _camera.y;
				_rect.x = x;
				_rect.y = y;
				_rect.width = e.width;
				_rect.height = 1;
				_target.fillRect(_rect, color);
				_rect.y += e.height - 1;
				_target.fillRect(_rect, color);
				_rect.y = y;
				_rect.width = 1;
				_rect.height = e.height;
				_target.fillRect(_rect, color);
				_rect.x += e.width - 1;
				_target.fillRect(_rect, color);
				return;
			}
			if (alpha >= 1 && !blend)
			{
				if (color < 0xFF000000) color = 0xFF000000 | color;
				_rect.x = e.x - e.originX - _camera.x;
				_rect.y = e.y - e.originY - _camera.y;
				_rect.width = e.width;
				_rect.height = e.height;
				_target.fillRect(_rect, color);
				return;
			}
			if (color >= 0xFF000000) color = 0xFFFFFF & color;
			_graphics.clear();
			_graphics.beginFill(color, alpha);
			_graphics.drawRect(e.x - e.originX - _camera.x, e.y - e.originY - _camera.y, e.width, e.height);
			_target.draw(FP.sprite, null, null, blend);
		}
		
		/**
		 * Draws a quadratic curve.
		 * @param	x1		X start.
		 * @param	y1		Y start.
		 * @param	x2		X control point, used to determine the curve.
		 * @param	y2		Y control point, used to determine the curve.
		 * @param	x3		X finish.
		 * @param	y3		Y finish.
		 * @param	color	Color of the curve
		 * @param	alpha	Alpha transparency.
		 */
		public static void curve(int x1, int y1, int x2, int y2, int x3, int y3, float thick = 1, uint color = 0, float alpha = 1)
		{
			_graphics.clear();
			_graphics.lineStyle(thick, color, alpha);
			_graphics.moveTo(x1 - _camera.x, y1 - _camera.y);
			_graphics.curveTo(x2 - _camera.x, y2 - _camera.y, x3 - _camera.x, y3 - _camera.y);
			_target.draw(FP.sprite, null, null, blend);
		}
		
		/**
		 * Draws a graphic object.
		 * @param	g		The Graphic to draw.
		 * @param	x		X position.
		 * @param	y		Y position.
		 */
		public static void graphic(Graphic g, int x = 0, int y = 0)
		{
			if (g.visible)
			{
				if (g.relative)
				{
					FP.point.x = x;
					FP.point.y = y;
				}
				else FP.point.x = FP.point.y = 0;
				FP.point2.x = FP.camera.x;
				FP.point2.y = FP.camera.y;
				g.render(_target, FP.point, FP.point2);
			}
		}
		
		/**
		 * Draws an Entity object.
		 * @param	e					The Entity to draw.
		 * @param	x					X position.
		 * @param	y					Y position.
		 * @param	addEntityPosition	Adds the Entity's x and y position to the target position.
		 */
		public static void entity(Entity e, int x = 0, int y = 0, bool addEntityPosition = false)
		{
			if (e.visible && e.graphic)
			{
				if (addEntityPosition) graphic(e.graphic, x + e.x, y + e.y);
				else graphic(e.graphic, x, y);
			}
		}
		
		// Drawing information.
		/** @private */ private static Texture2D _target;
		/** @private */ private static Vector2 _camera;
		/** @private */ private static Graphics _graphics = FP.sprite.graphics;
		/** @private */ private static Rectangle _rect = FP.rect;
		/** @private */ private static Matrix _matrix = new Matrix;
	}
}