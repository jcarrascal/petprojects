namespace net.flashpunk.utils
{
	using flash.net.SharedObject;
	
	/**
	 * Static helper class used for saving and loading data from stored cookies.
	 */
	public class Data 
	{
		/**
		 * If you want to share data between different SWFs on the same host, use this id.
		 */
		public static string id = "";
		
		/**
		 * Overwrites the current data with the file.
		 * @param	file		The filename to load.
		 */
		public static void load(string file = "")
		{
			object data = loadData(file);
			_data = { };
			for (string i in data) _data[i] = data[i];
		}
		
		/**
		 * Overwrites the file with the current data. The current data will not be saved until this is called.
		 * @param	file		The filename to save.
		 */
		public static void save(string file = "")
		{
			if (_shared) _shared.clear();
			object data = loadData(file);
			for (string i in _data) data[i] = _data[i];
			_shared.flush(SIZE);
		}
		
		/**
		 * Reads an int from the current data.
		 * @param	name			Property to read.
		 * @param	defaultValue	Default value.
		 * @return	The property value, or defaultValue if the property is not assigned.
		 */
		public static int readInt(string name, int defaultValue = 0)
		{
			return int(read(name, defaultValue));
		}
		
		/**
		 * Reads a uint from the current data.
		 * @param	name			Property to read.
		 * @param	defaultValue	Default value.
		 * @return	The property value, or defaultValue if the property is not assigned.
		 */
		public static uint readUint(string name, uint defaultValue = 0)
		{
			return uint(read(name, defaultValue));
		}
		
		/**
		 * Reads a bool from the current data.
		 * @param	name			Property to read.
		 * @param	defaultValue	Default value.
		 * @return	The property value, or defaultValue if the property is not assigned.
		 */
		public static bool readBool(string name, bool defaultValue = true)
		{
			return bool(read(name, defaultValue));
		}
		
		/**
		 * Reads a string from the current data.
		 * @param	name			Property to read.
		 * @param	defaultValue	Default value.
		 * @return	The property value, or defaultValue if the property is not assigned.
		 */
		public static string readString(string name, string defaultValue = "")
		{
			return string(read(name, defaultValue));
		}
		
		/**
		 * Writes an int to the current data.
		 * @param	name		Property to write.
		 * @param	value		Value to write.
		 */
		public static void writeInt(string name, int value = 0)
		{
			_data[name] = value;
		}
		
		/**
		 * Writes a uint to the current data.
		 * @param	name		Property to write.
		 * @param	value		Value to write.
		 */
		public static void writeUint(string name, uint value = 0)
		{
			_data[name] = value;
		}
		
		/**
		 * Writes a bool to the current data.
		 * @param	name		Property to write.
		 * @param	value		Value to write.
		 */
		public static void writeBool(string name, bool value = true)
		{
			_data[name] = value;
		}
		
		/**
		 * Writes a string to the current data.
		 * @param	name		Property to write.
		 * @param	value		Value to write.
		 */
		public static void writeString(string name, string value = "")
		{
			_data[name] = value;
		}
		
		/** @private Reads a property from the data object. */
		private static read(string name, defaultValue:*):*
		{
			if (_data.hasOwnProperty(name)) return _data[name];
			return defaultValue;
		}
		
		/** @private Loads the data file, or return it if you're loading the same one. */
		private static object loadData(string file)
		{
			if (!file) file = DEFAULT_FILE;
			if (id) _shared = SharedObject.getLocal(PREFIX + "/" + id + "/" + file, "/");
			else _shared = SharedObject.getLocal(PREFIX + "/" + file);
			return _shared.data;
		}
		
		// Data information.
		/** @private */ private static SharedObject _shared;
		/** @private */ private static string _dir;
		/** @private */ private static object _data = { };
		/** @private */ private const string PREFIX = "FlashPunk";
		/** @private */ private const string DEFAULT_FILE = "_file";
		/** @private */ private const uint SIZE = 10000;
	}
}