using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using net.flashpunk;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace flash.display
{
    public class Graphics
    {
        const float Tau = (float)(Math.PI * 2);
        const float CircleSections = 64f / 100f;
        readonly GraphicsDevice graphicsDevice;
        readonly RenderTarget2D renderTarget;
        readonly List<VertexPositionColor> vertexData = new List<VertexPositionColor>();

        Color fillColor;
        Color lineColor;
        float lineThickness;

        public Graphics(GraphicsDevice graphicsDevice)
            : this(graphicsDevice, null)
        {
        }

        public Graphics(GraphicsDevice graphicsDevice, RenderTarget2D renderTarget)
        {
            this.graphicsDevice = graphicsDevice;
            this.renderTarget = renderTarget;
            clear();
        }

        public void clear()
        {
            graphicsDevice.SetRenderTarget(renderTarget);
            fillColor = Color.Transparent;
            lineColor = Color.Transparent;
            lineThickness = float.NaN;
        }

        public void lineStyle(float thickness)
        {
            lineStyle(thickness, 0, 1);
        }

        public void lineStyle(float thickness, int color)
        {
            lineStyle(thickness, color, 1);
        }

        public void lineStyle(float thickness, int color, float alpha)
        {
            lineThickness = thickness;
            lineColor = Color.FromNonPremultiplied(color >> 16 & 0xFF, color >> 8 & 0xFF, color & 0xFF, (int)(alpha * 0xFF));
        }

        public void beginFill(int color)
        {
            beginFill(color, 1);
        }

        public void beginFill(int color, float alpha)
        {
            fillColor = Color.FromNonPremultiplied(color >> 16 & 0xFF, color >> 8 & 0xFF, color & 0xFF, (int)(alpha * 0xFF));
        }

        public void beginGradientFill(string type, int[] colors, float[] alphas, byte[] ratios)
        {
            beginGradientFill(type, colors, alphas, ratios, Matrix.Identity);
        }

        Func<int, int, Texture2D> renderFillTexture;
        readonly List<Color> gradientColors = new List<Color>();
        readonly List<float> gradientStops = new List<float>();

        public void beginGradientFill(string type, int[] colors, float[] alphas, byte[] ratios, Matrix matrix)
        {
        }

        public void beginBitmapFill(Texture2D bitmap)
        {
            beginBitmapFill(bitmap, Matrix.Identity, true);
        }

        public void beginBitmapFill(Texture2D bitmap, Matrix matrix)
        {
            beginBitmapFill(bitmap, matrix, true);
        }

        public void beginBitmapFill(Texture2D bitmap, Matrix matrix, bool repeat)
        {
        }

        public void drawCircle(float x, float y, float radius)
        {
            int sections = (int)(radius * CircleSections);
            float step = Tau / sections;

            // Fill the circle.
            if (fillColor != Color.Transparent)
            {
                vertexData.Clear();
                VertexPositionColor center = new VertexPositionColor(new Vector3(x, y, 0), fillColor);
                VertexPositionColor previous = new VertexPositionColor(new Vector3(x + radius, y, 0), fillColor);
                for (float angle = 0; angle < Tau; angle += step)
                {
                    vertexData.Add(center);
                    vertexData.Add(previous);
                    vertexData.Add(previous = new VertexPositionColor(new Vector3((float)(x + radius * Math.Cos(angle)),
                        (float)(y + radius * Math.Sin(angle)), 0), fillColor));
                }
                vertexData.Add(center);
                vertexData.Add(previous);
                vertexData.Add(new VertexPositionColor(new Vector3(x + radius, y, 0), fillColor));
                graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertexData.ToArray(), 0,
                    vertexData.Count / 3);
            }

            // Draw the circumference.
            if (!float.IsNaN(lineThickness) && lineColor != Color.Transparent)
            {
                vertexData.Clear();
                for (float angle = 0; angle < Tau; angle += step)
                {
                    vertexData.Add(new VertexPositionColor(new Vector3((float)(x + radius * Math.Cos(angle)),
                        (float)(y + radius * Math.Sin(angle)), 0), lineColor));
                }
                vertexData.Add(new VertexPositionColor(new Vector3(x + radius, y, 0), lineColor));
                graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vertexData.ToArray(), 0,
                    vertexData.Count - 1);
            }
        }

        public void drawRect(float x, float y, float width, float height)
        {
        }

        Texture2D RenderRadialGradientTexture(int width, int height, Color[] colors, float[] ratios)
        {
            return null;
        }

        Texture2D RenderLinearGradientTexture(int width, int height, Color[] colors, float[] ratios)
        {
            return null;
        }
    }

    public static class GradientType
    {
        public const string LINEAR = "linear";
        public const string RADIAL = "radial";
    }
}
