namespace net.flashpunk.masks
{
	using flash.display.Bitmap;
	using flash.display.Texture2D;
	using flash.geom.Vector2;
	using flash.geom.Rectangle;
	using net.flashpunk.*;
	
	/**
	 * Uses a hash grid to determine collision, faster than
	 * using hundreds of Entities for tiled levels, etc.
	 */
	public class Grid : Hitbox
	{
		/**
		 * If x/y positions should be used instead of columns/rows.
		 */
		public bool usePositions;
		
		/**
		 * Constructor.
		 * @param	width			Width of the grid, in pixels.
		 * @param	height			Height of the grid, in pixels.
		 * @param	tileWidth		Width of a grid tile, in pixels.
		 * @param	tileHeight		Height of a grid tile, in pixels.
		 * @param	x				X offset of the grid.
		 * @param	y				Y offset of the grid.
		 */
		public Grid(uint width, uint height, uint tileWidth, uint tileHeight, int x = 0, int y = 0) 
		{
			// check for illegal grid size
			if (!width || !height || !tileWidth || !tileHeight) throw new Error("Illegal Grid, sizes cannot be 0.");
			
			// set grid properties
			_columns = width / tileWidth;
			_rows = height / tileHeight;
			_data = new Texture2D(_columns, _rows, true, 0);
			_tile = new Rectangle(0, 0, tileWidth, tileHeight);
			_x = x;
			_y = y;
			_width = width;
			_height = height;
			
			// set callback functions
			_check[Mask] = collideMask;
			_check[Hitbox] = collideHitbox;
			_check[Pixelmask] = collidePixelmask;
		}
		
		/**
		 * Sets the value of the tile.
		 * @param	column		Tile column.
		 * @param	row			Tile row.
		 * @param	solid		If the tile should be solid.
		 */
		public void setTile(uint column = 0, uint row = 0, bool solid = true)
		{
			if (usePositions)
			{
				column /= _tile.width;
				row /= _tile.height;
			}
			_data.setPixel32(column, row, solid ? 0xFFFFFFFF : 0);
		}
		
		/**
		 * Makes the tile non-solid.
		 * @param	column		Tile column.
		 * @param	row			Tile row.
		 */
		public void clearTile(uint column = 0, uint row = 0)
		{
			setTile(column, row, false);
		}
		
		/**
		 * Gets the value of a tile.
		 * @param	column		Tile column.
		 * @param	row			Tile row.
		 * @return	tile value.
		 */
		public bool getTile(uint column = 0, uint row = 0)
		{
			if (usePositions)
			{
				column /= _tile.width;
				row /= _tile.height;
			}
			return _data.getPixel32(column, row) > 0;
		}
		
		/**
		 * Sets the value of a rectangle region of tiles.
		 * @param	column		First column.
		 * @param	row			First row.
		 * @param	width		Columns to fill.
		 * @param	height		Rows to fill.
		 * @param	fill		Value to fill.
		 */
		public void setRect(uint column = 0, uint row = 0, int width = 1, int height = 1, bool solid = true)
		{
			if (usePositions)
			{
				column /= _tile.width;
				row /= _tile.height;
				width /= _tile.width;
				height /= _tile.height;
			}
			_rect.x = column;
			_rect.y = row;
			_rect.width = width;
			_rect.height = height;
			_data.fillRect(_rect, solid ? 0xFFFFFFFF : 0);
		}
		
		/**
		 * Makes the rectangular region of tiles non-solid.
		 * @param	column		First column.
		 * @param	row			First row.
		 * @param	width		Columns to fill.
		 * @param	height		Rows to fill.
		 */
		public void clearRect(uint column = 0, uint row = 0, int width = 1, int height = 1)
		{
			setRect(column, row, width, height, false);
		}
		
		/**
		* Loads the grid data from a string.
		* @param str			The string data, which is a set of tile values (0 or 1) separated by the columnSep and rowSep strings.
		* @param columnSep		The string that separates each tile value on a row, default is ",".
		* @param rowSep			The string that separates each row of tiles, default is "\n".
		*/
		public void loadFromString(string str, string columnSep = ",", string rowSep = "\n")
		{
			Array row = str.split(rowSep),
				int rows = row.length,
				Array col, int cols, int x, int y;
			for (y = 0; y < rows; y ++)
			{
				if (row[y] == '') continue;
				col = row[y].split(columnSep),
				cols = col.length;
				for (x = 0; x < cols; x ++)
				{
					if (col[x] == '') continue;
					setTile(x, y, uint(col[x]) > 0);
				}
			}
		}
		
		/**
		* Saves the grid data to a string.
		* @param columnSep		The string that separates each tile value on a row, default is ",".
		* @param rowSep			The string that separates each row of tiles, default is "\n".
		*/
		public saveToString(string columnSep = ",", string rowSep = "\n"): string
		{
			string s = '',
				int x, int y;
			for (y = 0; y < _rows; y ++)
			{
				for (x = 0; x < _columns; x ++)
				{
					s += string(getTile(x, y));
					if (x != _columns - 1) s += columnSep;
				}
				if (y != _rows - 1) s += rowSep;
			}
			return s;
		}
		
		/**
		 * The tile width.
		 */
		public uint tileWidth { get { return _tile.width; }
		
		/**
		 * The tile height.
		 */
		public uint tileHeight { get { return _tile.height; }
		
		/**
		 * How many columns the grid has
		 */
		public uint columns { get { return _columns; }
		
		/**
		 * How many rows the grid has.
		 */
		public uint rows { get { return _rows; }
		
		/**
		 * The grid data.
		 */
		public Texture2D data { get { return _data; }
		
		/** @private Collides against an Entity. */
		private bool collideMask(Mask other)
		{
			_rect.x = other.parent.x - other.parent.originX - parent.x + parent.originX;
			_rect.y = other.parent.y - other.parent.originY - parent.y + parent.originY;
			_point.x = int((_rect.x + other.parent.width - 1) / _tile.width) + 1;
			_point.y = int((_rect.y + other.parent.height -1) / _tile.height) + 1;
			_rect.x = int(_rect.x / _tile.width);
			_rect.y = int(_rect.y / _tile.height);
			_rect.width = _point.x - _rect.x;
			_rect.height = _point.y - _rect.y;
			return _data.hitTest(FP.zero, 1, _rect);
		}
		
		/** @private Collides against a Hitbox. */
		private bool collideHitbox(Hitbox other)
		{
			_rect.x = other.parent.x + other._x - parent.x - _x;
			_rect.y = other.parent.y + other._y - parent.y - _y;
			_point.x = int((_rect.x + other._width - 1) / _tile.width) + 1;
			_point.y = int((_rect.y + other._height -1) / _tile.height) + 1;
			_rect.x = int(_rect.x / _tile.width);
			_rect.y = int(_rect.y / _tile.height);
			_rect.width = _point.x - _rect.x;
			_rect.height = _point.y - _rect.y;
			return _data.hitTest(FP.zero, 1, _rect);
		}
		
		/** @private Collides against a Pixelmask. */
		private bool collidePixelmask(Pixelmask other)
		{
			int x1 = other.parent.x + other._x - parent.x - _x,
				int y1 = other.parent.y + other._y - parent.y - _y,
				int x2 = ((x1 + other._width - 1) / _tile.width),
				int y2 = ((y1 + other._height - 1) / _tile.height);
			_point.x = x1;
			_point.y = y1;
			x1 /= _tile.width;
			y1 /= _tile.height;
			_tile.x = x1 * _tile.width;
			_tile.y = y1 * _tile.height;
			int xx = x1;
			while (y1 <= y2)
			{
				while (x1 <= x2)
				{
					if (_data.getPixel32(x1, y1))
					{
						if (other._data.hitTest(_point, 1, _tile)) return true;
					}
					x1 ++;
					_tile.x += _tile.width;
				}
				x1 = xx;
				y1 ++;
				_tile.x = x1 * _tile.width;
				_tile.y += _tile.height;
			}
			return false;
		}
		
		// Grid information.
		/** @private */ private Texture2D _data;
		/** @private */ private uint _columns;
		/** @private */ private uint _rows;
		/** @private */ private Rectangle _tile;
		/** @private */ private Rectangle _rect = FP.rect;
		/** @private */ private Vector2 _point = FP.point;
		/** @private */ private Vector2 _point2 = FP.point2;
	}
}