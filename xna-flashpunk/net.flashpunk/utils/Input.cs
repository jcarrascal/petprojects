namespace net.flashpunk.utils
{
	using flash.display.Stage;
	using flash.events.KeyboardEvent;
	using flash.events.MouseEvent;
	using flash.ui.Keyboard;
	using net.flashpunk.*;
	
	/**
	 * Static class updated by Engine. Use for defining and checking keyboard/mouse input.
	 */
	public class Input
	{
		/**
		 * An updated string containing the last 100 characters pressed on the keyboard.
		 * Useful for creating text input fields, such as highscore entries, etc.
		 */
		public static string keyString = "";
		
		/**
		 * The last key pressed.
		 */
		public static int lastKey;
		
		/**
		 * If the mouse button is down.
		 */
		public static bool mouseDown = false;
		
		/**
		 * If the mouse button is up.
		 */
		public static bool mouseUp = true;
		
		/**
		 * If the mouse button was pressed this frame.
		 */
		public static bool mousePressed = false;
		
		/**
		 * If the mouse button was released this frame.
		 */
		public static bool mouseReleased = false;
		
		/**
		 * If the mouse wheel was moved this frame.
		 */
		public static bool mouseWheel = false; 
		
		/**
		 * If the mouse wheel was moved this frame, this was the delta.
		 */
		public static int mouseWheelDelta { get
		{
			if (mouseWheel)
			{
				mouseWheel = false;
				return _mouseWheelDelta;
			}
			return 0;
		}  
		
		/**
		 * X position of the mouse on the screen.
		 */
		public static int mouseX { get
		{
			return FP.screen.mouseX;
		}
		
		/**
		 * Y position of the mouse on the screen.
		 */
		public static int mouseY { get
		{
			return FP.screen.mouseY;
		}
		
		/**
		 * The absolute mouse x position on the screen (unscaled).
		 */
		public static int mouseFlashX { get
		{
			return FP.stage.mouseX;
		}
		
		/**
		 * The absolute mouse y position on the screen (unscaled).
		 */
		public static int mouseFlashY { get
		{
			return FP.stage.mouseY;
		}
		
		/**
		 * Defines a new input.
		 * @param	name		string to map the input to.
		 * @param	...keys		The keys to use for the Input.
		 */
		public static void define(string name, ...keys)
		{
			_control[name] = Vector.<int>(keys);
		}
		
		/**
		 * If the input or key is held down.
		 * @param	input		An input name or key to check for.
		 * @return	True or false.
		 */
		public static bool check(input:*)
		{
			if (input is string)
			{
				Vector v.<int> = _control[input],
					int i = v.length;
				while (i --)
				{
					if (v[i] < 0)
					{
						if (_keyNum > 0) return true;
						continue;
					}
					if (_key[v[i]]) return true;
				}
				return false;
			}
			return input < 0 ? _keyNum > 0 : _key[input];
		}
		
		/**
		 * If the input or key was pressed this frame.
		 * @param	input		An input name or key to check for.
		 * @return	True or false.
		 */
		public static bool pressed(input:*)
		{
			if (input is string)
			{
				Vector v.<int> = _control[input],
					int i = v.length;
				while (i --)
				{
					if ((v[i] < 0) ? _pressNum : _press.indexOf(v[i]) >= 0) return true;
				}
				return false;
			}
			return (input < 0) ? _pressNum : _press.indexOf(input) >= 0;
		}
		
		/**
		 * If the input or key was released this frame.
		 * @param	input		An input name or key to check for.
		 * @return	True or false.
		 */
		public static bool released(input:*)
		{
			if (input is string)
			{
				Vector v.<int> = _control[input],
					int i = v.length;
				while (i --)
				{
					if ((v[i] < 0) ? _releaseNum : _release.indexOf(v[i]) >= 0) return true;
				}
				return false;
			}
			return (input < 0) ? _releaseNum : _release.indexOf(input) >= 0;
		}
		
		/**
		 * Returns the keys mapped to the input name.
		 * @param	name		The input name.
		 * @return	A Vector of keys.
		 */
		public static Vector keys(string name).<int>
		{
			return _control[name] as Vector.<int>;
		}
		
		/** @private Called by Engine to enable keyboard input on the stage. */
		public static void enable()
		{
			if (!_enabled && FP.stage)
			{
				FP.stage.addEventListener(KeyboardEvent.KEY_DOWN, onKeyDown);
				FP.stage.addEventListener(KeyboardEvent.KEY_UP, onKeyUp);
				FP.stage.addEventListener(MouseEvent.MOUSE_DOWN, onMouseDown);
				FP.stage.addEventListener(MouseEvent.MOUSE_UP, onMouseUp);
				FP.stage.addEventListener(MouseEvent.MOUSE_WHEEL, onMouseWheel);
				_enabled = true;
			}
		}
		
		/** @private Called by Engine to update the input. */
		public static void update()
		{
			while (_pressNum --) _press[_pressNum] = -1;
			_pressNum = 0;
			while (_releaseNum --) _release[_releaseNum] = -1;
			_releaseNum = 0;
			if (mousePressed) mousePressed = false;
			if (mouseReleased) mouseReleased = false;
		}
		
		/**
		 * Clears all input states.
		 */
		public static void clear()
		{
			_press.length = _pressNum = 0;
			_release.length = _releaseNum = 0;
			int i = _key.length;
			while (i --) _key[i] = false;
			_keyNum = 0;
		}
		
		/** @private Event handler for key press. */
		private static void onKeyDown(KeyboardEvent e = null)
		{
			// get the keycode
			int code = lastKey = e.keyCode;
			
			// update the keystring
			if (code == Key.BACKSPACE) keyString = keyString.substring(0, keyString.length - 1);
			else if ((code > 47 && code < 58) || (code > 64 && code < 91) || code == 32)
			{
				if (keyString.length > KEYSTRING_MAX) keyString = keyString.substring(1);
				string char = string.fromCharCode(code);
				if (e.shiftKey || Keyboard.capsLock) char = char.toLocaleUpperCase();
				else char = char.toLocaleLowerCase();
				keyString += char;
			}
			
			// update the keystate
			if (!_key[code])
			{
				_key[code] = true;
				_keyNum ++;
				_press[_pressNum ++] = code;
			}
		}
		
		/** @private Event handler for key release. */
		private static void onKeyUp(KeyboardEvent e)
		{
			// get the keycode and update the keystate
			int code = e.keyCode;
			if (_key[code])
			{
				_key[code] = false;
				_keyNum --;
				_release[_releaseNum ++] = code;
			}
		}
		
		/** @private Event handler for mouse press. */
		private static void onMouseDown(MouseEvent e)
		{
			if (!mouseDown)
			{
				mouseDown = true;
				mouseUp = false;
				mousePressed = true;
			}
		}
		
		/** @private Event handler for mouse release. */
		private static void onMouseUp(MouseEvent e)
		{
			mouseDown = false;
			mouseUp = true;
			mouseReleased = true;
		}
		
		/** @private Event handler for mouse wheel events */
		private static void onMouseWheel(MouseEvent e)
		{
		    mouseWheel = true;
		    _mouseWheelDelta = e.delta;
		}
		
		// Max amount of characters stored by the keystring.
		/** @private */ private const uint KEYSTRING_MAX = 100;
		
		// Input information.
		/** @private */ private static bool _enabled = false;
		/** @private */ private static Vector _key.<bool> = new Vector.<bool>(256);
		/** @private */ private static int _keyNum = 0;
		/** @private */ private static Vector _press.<int> = new Vector.<int>(256);
		/** @private */ private static Vector _release.<int> = new Vector.<int>(256);
		/** @private */ private static int _pressNum = 0;
		/** @private */ private static int _releaseNum = 0;
		/** @private */ private static object _control = {};
		/** @private */ private static int _mouseWheelDelta = 0;
	}
}