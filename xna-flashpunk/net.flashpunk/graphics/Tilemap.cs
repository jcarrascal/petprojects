namespace net.flashpunk.graphics 
{
	using flash.display.Texture2D;
	using flash.geom.Vector2;
	using flash.geom.Rectangle;
	using net.flashpunk.Graphic;
	using net.flashpunk.FP;
	using net.flashpunk.masks.Grid;
	
	/**
	 * A canvas to which Tiles can be drawn for fast multiple tile rendering.
	 */
	public class Tilemap : Canvas
	{
		/**
		 * If x/y positions should be used instead of columns/rows.
		 */
		public bool usePositions;
		
		/**
		 * Constructor.
		 * @param	tileset			The source tileset image.
		 * @param	width			Width of the tilemap, in pixels.
		 * @param	height			Height of the tilemap, in pixels.
		 * @param	tileWidth		Tile width.
		 * @param	tileHeight		Tile height.
		 */
		public Tilemap(tileset:*, uint width, uint height, uint tileWidth, uint tileHeight) 
		{
			// set some tilemap information
			_width = width - (width % tileWidth);
			_height = height - (height % tileHeight);
			_columns = _width / tileWidth;
			_rows = _height / tileHeight;
			_map = new Texture2D(_columns, _rows, false, 0);
			_temp = _map.clone();
			_tile = new Rectangle(0, 0, tileWidth, tileHeight);
			
			// create the canvas
			_maxWidth -= _maxWidth % tileWidth;
			_maxHeight -= _maxHeight % tileHeight;
			base(_width, _height);
			
			// load the tileset graphic
			if (tileset is Class) _set = FP.getBitmap(tileset);
			else if (tileset is Texture2D) _set = tileset;
			if (!_set) throw new Error("Invalid tileset graphic provided.");
			_setColumns = uint(_set.width / tileWidth);
			_setRows = uint(_set.height / tileHeight);
			_setCount = _setColumns * _setRows;
		}
		
		/**
		 * Sets the index of the tile at the position.
		 * @param	column		Tile column.
		 * @param	row			Tile row.
		 * @param	index		Tile index.
		 */
		public void setTile(uint column, uint row, uint index = 0)
		{
			if (usePositions)
			{
				column /= _tile.width;
				row /= _tile.height;
			}
			index %= _setCount;
			column %= _columns;
			row %= _rows;
			_tile.x = (index % _setColumns) * _tile.width;
			_tile.y = uint(index / _setColumns) * _tile.height;
			_map.setPixel(column, row, index);
			draw(column * _tile.width, row * _tile.height, _set, _tile);
		}
		
		/**
		 * Clears the tile at the position.
		 * @param	column		Tile column.
		 * @param	row			Tile row.
		 */
		public void clearTile(uint column, uint row)
		{
			if (usePositions)
			{
				column /= _tile.width;
				row /= _tile.height;
			}
			column %= _columns;
			row %= _rows;
			_tile.x = column * _tile.width;
			_tile.y = row * _tile.height;
			fill(_tile, 0, 0);
		}
		
		/**
		 * Gets the tile index at the position.
		 * @param	column		Tile column.
		 * @param	row			Tile row.
		 * @return	The tile index.
		 */
		public uint getTile(uint column, uint row)
		{
			if (usePositions)
			{
				column /= _tile.width;
				row /= _tile.height;
			}
			return _map.getPixel(column % _columns, row % _rows);
		}
		
		/**
		 * Sets a rectangular region of tiles to the index.
		 * @param	column		First tile column.
		 * @param	row			First tile row.
		 * @param	width		Width in tiles.
		 * @param	height		Height in tiles.
		 * @param	index		Tile index.
		 */
		public void setRect(uint column, uint row, uint width = 1, uint height = 1, uint index = 0)
		{
			if (usePositions)
			{
				column /= _tile.width;
				row /= _tile.height;
				width /= _tile.width;
				height /= _tile.height;
			}
			column %= _columns;
			row %= _rows;
			uint c = column,
				uint r = column + width,
				uint b = row + height,
				bool u = usePositions;
			usePositions = false;
			while (row < b)
			{
				while (column < r)
				{
					setTile(column, row, index);
					column ++;
				}
				column = c;
				row ++;
			}
			usePositions = u;
		}
		
		/**
		 * Clears the rectangular region of tiles.
		 * @param	column		First tile column.
		 * @param	row			First tile row.
		 * @param	width		Width in tiles.
		 * @param	height		Height in tiles.
		 */
		public void clearRect(uint column, uint row, uint width = 1, uint height = 1)
		{
			if (usePositions)
			{
				column /= _tile.width;
				row /= _tile.height;
				width /= _tile.width;
				height /= _tile.height;
			}
			column %= _columns;
			row %= _rows;
			uint c = column,
				uint r = column + width,
				uint b = row + height,
				bool u = usePositions;
			usePositions = false;
			while (row < b)
			{
				while (column < r)
				{
					clearTile(column, row);
					column ++;
				}
				column = c;
				row ++;
			}
			usePositions = u;
		}
		
		/**
		* Loads the Tilemap tile index data from a string.
		* @param str			The string data, which is a set of tile values separated by the columnSep and rowSep strings.
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
					setTile(x, y, uint(col[x]));
				}
			}
		}
		
		/**
		* Saves the Tilemap tile index data to a string.
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
		 * Gets the index of a tile, based on its column and row in the tileset.
		 * @param	tilesColumn		Tileset column.
		 * @param	tilesRow		Tileset row.
		 * @return	Index of the tile.
		 */
		public uint getIndex(uint tilesColumn, uint tilesRow)
		{
			return (tilesRow % _setRows) * _setColumns + (tilesColumn % _setColumns);
		}
		
		/**
		 * Shifts all the tiles in the tilemap.
		 * @param	columns		Horizontal shift.
		 * @param	rows		Vertical shift.
		 * @param	wrap		If tiles shifted off the canvas should wrap around to the other side.
		 */
		public void shiftTiles(int columns, int rows, bool wrap = false)
		{
			if (usePositions)
			{
				columns /= _tile.width;
				rows /= _tile.height;
			}
			
			if (!wrap) _temp.fillRect(_temp.rect, 0);
			
			if (columns != 0)
			{
				shift(columns * _tile.width, 0);
				if (wrap) _temp.copyPixels(_map, _map.rect, FP.zero);
				_map.scroll(columns, 0);
				_point.y = 0;
				_point.x = columns > 0 ? columns - _columns : columns + _columns;
				_map.copyPixels(_temp, _temp.rect, _point);
				
				_rect.x = columns > 0 ? 0 : _columns + columns;
				_rect.y = 0;
				_rect.width = Math.abs(columns);
				_rect.height = _rows;
				updateRect(_rect, !wrap);
			}
			
			if (rows != 0)
			{
				shift(0, rows * _tile.height);
				if (wrap) _temp.copyPixels(_map, _map.rect, FP.zero);
				_map.scroll(0, rows);
				_point.x = 0;
				_point.y = rows > 0 ? rows - _rows : rows + _rows;
				_map.copyPixels(_temp, _temp.rect, _point);
				
				_rect.x = 0;
				_rect.y = rows > 0 ? 0 : _rows + rows;
				_rect.width = _columns;
				_rect.height = Math.abs(rows);
				updateRect(_rect, !wrap);
			}
		}
		
		/** @private Used by shiftTiles to update a rectangle of tiles from the tilemap. */
		private void updateRect(Rectangle rect, bool clear)
		{
			int x = rect.x,
				int y = rect.y,
				int w = x + rect.width,
				int h = y + rect.height,
				bool u = usePositions;
			usePositions = false;
			if (clear)
			{
				while (y < h)
				{
					while (x < w) clearTile(x ++, y);
					x = rect.x;
					y ++;
				}
			}
			else
			{
				while (y < h)
				{
					while (x < w) updateTile(x ++, y);
					x = rect.x;
					y ++;
				}
			}
			usePositions = u;
		}
		
		/** @private Used by shiftTiles to update a tile from the tilemap. */
		private void updateTile(uint column, uint row)
		{
			setTile(column, row, _map.getPixel(column % _columns, row % _rows));
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
		 * How many columns the tilemap has.
		 */
		public uint columns { get { return _columns; }
		
		/**
		 * How many rows the tilemap has.
		 */
		public uint rows { get { return _rows; }
		
		// Tilemap information.
		/** @private */ private Texture2D _map;
		/** @private */ private Texture2D _temp;
		/** @private */ private uint _columns;
		/** @private */ private uint _rows;
		
		// Tileset information.
		/** @private */ private Texture2D _set;
		/** @private */ private uint _setColumns;
		/** @private */ private uint _setRows;
		/** @private */ private uint _setCount;
		/** @private */ private Rectangle _tile;
		
		// Global objects.
		/** @private */ private Rectangle _rect = FP.rect;
	}
}