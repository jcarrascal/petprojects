using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace net.flashpunk
{
    public class Embed
    {
        string file;

        public Embed(string file)
        {
            this.file = file;
        }

        public string GetContentFile()
        {
            return Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file));
        }

        public Texture2D bitmapData
        {
            get { return FP.content.Load<Texture2D>(GetContentFile()); }
        }

        public static implicit operator Texture2D(Embed file)
        {
            return FP.content.Load<Texture2D>(file.GetContentFile());
        }
    }
}