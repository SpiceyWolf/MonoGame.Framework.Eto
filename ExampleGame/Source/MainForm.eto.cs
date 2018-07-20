using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Eto;
using Microsoft.Xna.Framework.Graphics;
using Eto.Forms;
using Eto.Drawing;
using Color = Eto.Drawing.Color;

namespace Game1
{
    public class MainForm : Form
    {
        private SpriteBatch sb;
        private Texture2D tex1;
        private Texture2D tex2;

        private void InitializeComponents()
        {
            Title = "My Eto Form";
            ClientSize = new Size(400, 350);
            Padding = 10;

            var layout = new DynamicLayout();

            layout.AddRow(
            new RenderSurface(renderSurface1_Render)
            {
                AutoDraw = true,
                AutoDrawInterval = 10,
                BackgroundColor = new Color(90, 140, 255),
                Size = new Size(ClientSize.Width / 2, ClientSize.Height)
            },

            new RenderSurface(renderSurface2_Render)
            {
                AutoDraw = true,
                AutoDrawInterval = 1000,
                BackgroundColor = new Color(255, 140, 90),
                Size = new Size(ClientSize.Width / 2, ClientSize.Height)
            });

            Content = new StackLayout
            {
                Items =
                {
                    layout,
	                // add more controls here
	            }
            };
        }

        public MainForm()
        {
            InitializeComponents();
            if (DesignMode.Active) return;

            sb = new SpriteBatch(UniversalBackend.GraphicsDevice);
            tex1 = UniversalBackend.Content.Load<Texture2D>("Content/1");
            tex2 = UniversalBackend.Content.Load<Texture2D>("Content/2");
        }

        private void renderSurface1_Render(object sender, EventArgs e)
        {
            sb.Begin();
            sb.Draw(tex1, Vector2.Zero, Microsoft.Xna.Framework.Color.White);
            sb.End();
        }

        private void renderSurface2_Render(object sender, EventArgs e)
        {
            sb.Begin();
            sb.Draw(tex2, Vector2.Zero, Microsoft.Xna.Framework.Color.White);
            sb.End();
        }
    }
}
