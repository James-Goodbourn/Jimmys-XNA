///JimysXNA Created by James Goodbourn
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JimysXNA.Scale
{
    /// <summary>
    /// This class is for the scaling of sprites for changes with window/screen size
    /// </summary>
    public class SpriteScale
    {
        public SpriteScale(int width, int height, GraphicsDeviceManager graphics)
        {
            Width = width;
            Height = height;
            Graphics = graphics;
        }

        /// <summary>
        /// Sets the scale matrix on device reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeviceReset(object sender, EventArgs e)
        {
            Scale();
        }

        /// <summary>
        /// Sets the scale matrix using the current device settings
        /// </summary>
        public void Scale()
        {
            float scaleX =
            (float)Graphics.GraphicsDevice.Viewport.Width / Width;
            float scaleY =
            (float)Graphics.GraphicsDevice.Viewport.Height / Height;

            // Do not scale the sprite depth (Z=1).
            ScaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1);
        }

        private float Width;

        private float Height;

        public Matrix ScaleMatrix { get; private set; }

        public GraphicsDeviceManager Graphics { get; set; }
    }
}
