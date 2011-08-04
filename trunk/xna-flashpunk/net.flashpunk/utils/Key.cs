namespace net.flashpunk.utils 
{
	/**
	 * Contains static key constants to be used by Input.
	 */
	public class Key 
	{
		public const int ANY = -1;
		
		public const int LEFT = 37;
		public const int UP = 38;
		public const int RIGHT = 39;
		public const int DOWN = 40;
		
		public const int ENTER = 13;
		public const int CONTROL = 17;
		public const int SPACE = 32;
		public const int SHIFT = 16;
		public const int BACKSPACE = 8;
		public const int CAPS_LOCK = 20;
		public const int DELETE = 46;
		public const int END = 35;
		public const int ESCAPE = 27;
		public const int HOME = 36;
		public const int INSERT = 45;
		public const int TAB = 9;
		public const int PAGE_DOWN = 34;
		public const int PAGE_UP = 33;
		public const int LEFT_SQUARE_BRACKET = 219;
		public const int RIGHT_SQUARE_BRACKET = 221;
		
		public const int A = 65;
		public const int B = 66;
		public const int C = 67;
		public const int D = 68;
		public const int E = 69;
		public const int F = 70;
		public const int G = 71;
		public const int H = 72;
		public const int I = 73;
		public const int J = 74;
		public const int K = 75;
		public const int L = 76;
		public const int M = 77;
		public const int N = 78;
		public const int O = 79;
		public const int P = 80;
		public const int Q = 81;
		public const int R = 82;
		public const int S = 83;
		public const int T = 84;
		public const int U = 85;
		public const int V = 86;
		public const int W = 87;
		public const int X = 88;
		public const int Y = 89;
		public const int Z = 90;
		
		public const int F1 = 112;
		public const int F2 = 113;
		public const int F3 = 114;
		public const int F4 = 115;
		public const int F5 = 116;
		public const int F6 = 117;
		public const int F7 = 118;
		public const int F8 = 119;
		public const int F9 = 120;
		public const int F10 = 121;
		public const int F11 = 122;
		public const int F12 = 123;
		public const int F13 = 124;
		public const int F14 = 125;
		public const int F15 = 126;
		
		public const int DIGIT_0 = 48;
		public const int DIGIT_1 = 49;
		public const int DIGIT_2 = 50;
		public const int DIGIT_3 = 51;
		public const int DIGIT_4 = 52;
		public const int DIGIT_5 = 53;
		public const int DIGIT_6 = 54;
		public const int DIGIT_7 = 55;
		public const int DIGIT_8 = 56;
		public const int DIGIT_9 = 57;
		
		public const int NUMPAD_0 = 96;
		public const int NUMPAD_1 = 97;
		public const int NUMPAD_2 = 98;
		public const int NUMPAD_3 = 99;
		public const int NUMPAD_4 = 100;
		public const int NUMPAD_5 = 101;
		public const int NUMPAD_6 = 102;
		public const int NUMPAD_7 = 103;
		public const int NUMPAD_8 = 104;
		public const int NUMPAD_9 = 105;
		public const int NUMPAD_ADD = 107;
		public const int NUMPAD_DECIMAL = 110;
		public const int NUMPAD_DIVIDE = 111;
		public const int NUMPAD_ENTER = 108;
		public const int NUMPAD_MULTIPLY = 106;
		public const int NUMPAD_SUBTRACT = 109;
		
		/**
		 * Returns the name of the key.
		 * @param	char		The key to name.
		 * @return	The name.
		 */
		public static string name(int char)
		{
			if (char >= A && char <= Z) return string.fromCharCode(char);
			if (char >= F1 && char <= F15) return "F" + string(char - 111);
			if (char >= 96 && char <= 105) return "NUMPAD " + string(char - 96);
			switch (char)
			{
				case LEFT:
				return "LEFT";
				
				case UP:
				return "UP";
				
				case RIGHT:
				return "RIGHT";
				
				case DOWN:
				return "DOWN";
				
				case ENTER:
				return "ENTER";
				
				case CONTROL:
				return "CONTROL";
				
				case SPACE:
				return "SPACE";
				
				case SHIFT:
				return "SHIFT";
				
				case BACKSPACE:
				return "BACKSPACE";
				
				case CAPS_LOCK:
				return "CAPS LOCK";
				
				case DELETE:
				return "DELETE";
				
				case END:
				return "END";
				
				case ESCAPE: 	
				return "ESCAPE";
				
				case HOME: 		
				return "HOME";
				
				case INSERT: 	
				return "INSERT";
				
				case TAB: 		
				return "TAB";
				
				case PAGE_DOWN:
				return "PAGE DOWN";
				
				case PAGE_UP: 	
				return "PAGE UP";
				
				case NUMPAD_ADD:		
				return "NUMPAD ADD";
				
				case NUMPAD_DECIMAL:	
				return "NUMPAD DECIMAL";
				
				case NUMPAD_DIVIDE:		
				return "NUMPAD DIVIDE";
				
				case NUMPAD_ENTER:		
				return "NUMPAD ENTER";
				
				case NUMPAD_MULTIPLY:	
				return "NUMPAD MULTIPLY";
				
				case NUMPAD_SUBTRACT:	
				return "NUMPAD SUBTRACT";
				
				default:
				return string.fromCharCode(char);
			}
			return string.fromCharCode(char);
		}
	}
}