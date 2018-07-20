using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Eto.Drawing;
using Eto.Forms;

using etoColor = Eto.Drawing.Color;
using ImageFormat = System.Drawing.Imaging.ImageFormat;
using Timer = System.Windows.Forms.Timer;

namespace Microsoft.Xna.Framework.Eto
{
    public class RenderSurface : Drawable
    {
        private Bitmap mgLogo;
        private Timer _autoDraw = new Timer();
        private Color _clearColor;
        private Viewport _view;

        /// <summary>
        /// If true, will automatically redraw the
        /// surface on the specified interval.
        /// </summary>
        public bool AutoDraw
        {
            get { return _autoDraw.Enabled; }
            set { _autoDraw.Enabled = value; }
        }

        /// <summary>
        /// Specifies the interval in milliseconds
        /// to redraw the surface by.
        /// </summary>
        public int AutoDrawInterval
        {
            get { return _autoDraw.Interval; }
            set { _autoDraw.Interval = value; }
        }


        /// <summary>
        /// Changes the backgroundcolor of the surface when in
        /// designer, and changes the clear color of the
        /// surface renderer when in runtime.
        /// </summary>
        public etoColor BackgroundColor
        {
            get { return base.BackgroundColor; }
            set
            {
                base.BackgroundColor = value;
                if (!DesignMode.Active)
                    _clearColor = new Color(
                        base.BackgroundColor.R,
                        base.BackgroundColor.G,
                        base.BackgroundColor.B,
                        base.BackgroundColor.A);
            }
        }

        public RenderSurface() { OnCreateControl(); }
        public RenderSurface(EventHandler renderEvent)
        {
            OnCreateControl();
            Render += renderEvent;
        }

        private void OnCreateControl()
        {
            Size = new Size(100, 100);

            if (DesignMode.Active) // Designer
                using (var stream = new MemoryStream())
                {
                    // Set pretty icon making it clear its a MonoGame Window
                    Eto.Properties.Resources.DesignerIcon.Save(stream, ImageFormat.Png);
                    stream.Position = 0;
                    mgLogo = new Bitmap(stream);
                }
            else // MonoGame Render
            {
                _view = new Viewport(0, 0, Width, Height);

                // Setup Events
                _autoDraw.Tick += (sender, e) => { Invalidate(); };
                Render += OnRender;
                SizeChanged += (sender, e) => { _view = new Viewport(0, 0, Width, Height); };
            }
        }

        protected override void Dispose(bool disposing)
        {
            _autoDraw.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode.Active)
            {
                base.OnPaint(e);

                // Draw Logo
                var x = Width / 2 - mgLogo.Width / 2;
                var y = Height / 2 - mgLogo.Height / 2;
                e.Graphics.DrawImage(mgLogo, x, y);

                return;
            }

            // Begin the drawing
            UniversalBackend.BeginDraw(Width, Height);

            // Clear device
            UniversalBackend.GraphicsDevice.Clear(_clearColor);
            
            // Setup default view
            UniversalBackend.GraphicsDevice.Viewport = _view;

            // Invoke event based render calls
            Render?.Invoke(null, EventArgs.Empty);

            // End the drawing
            UniversalBackend.EndDraw();

            // Present the graphics to our control
            e.Graphics.DrawImage(UniversalBackend.Present(), 0, 0, Width, Height);
        }

        public event EventHandler Render;
        protected virtual void OnRender(object sender, EventArgs e) { }
    }
}

