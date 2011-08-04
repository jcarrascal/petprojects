using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace flash.display
{
    public static class BlendMode
    {
        // TODO: Use correct values for BlendModes.
        public static readonly BlendState ADD = BlendState.AlphaBlend;
        public static readonly BlendState ALPHA = BlendState.AlphaBlend;
        public static readonly BlendState DARKEN = BlendState.AlphaBlend;
        public static readonly BlendState DIFFERENCE = BlendState.AlphaBlend;
        public static readonly BlendState ERASE = BlendState.AlphaBlend;
        public static readonly BlendState HARDLIGHT = BlendState.AlphaBlend;
        public static readonly BlendState INVERT = BlendState.AlphaBlend;
        public static readonly BlendState LAYER = BlendState.AlphaBlend;
        public static readonly BlendState LIGHTEN = BlendState.AlphaBlend;
        public static readonly BlendState MULTIPLY = BlendState.AlphaBlend;
        public static readonly BlendState NORMAL = BlendState.AlphaBlend;
        public static readonly BlendState OVERLAY = BlendState.AlphaBlend;
        public static readonly BlendState SCREEN = BlendState.AlphaBlend;
        public static readonly BlendState SHADER = BlendState.AlphaBlend;
        public static readonly BlendState SUBTRACT = new BlendState()
        {
            ColorSourceBlend = Blend.SourceAlpha,
            ColorDestinationBlend = Blend.One,
            ColorBlendFunction = BlendFunction.ReverseSubtract,
            AlphaSourceBlend = Blend.SourceAlpha,
            AlphaDestinationBlend = Blend.One,
            AlphaBlendFunction = BlendFunction.ReverseSubtract,
        };
    }
}
