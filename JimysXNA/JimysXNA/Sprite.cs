///JimysXNA Created by James Goodbourn
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimysXNA.Sprites
{
    public class Sprite
    {
        public Sprite(Vector2 position, float scale, string file, ContentManager theContentManager)
        {
            Position = position;
            Scale = scale;
            Content = theContentManager;
            TextureFile = file;
            m_Rectangle = new Rectangle(0, 0, m_SpriteTexture.Width, m_SpriteTexture.Height);
        }

        public Sprite(Vector2 position, float scale, string file, ContentManager theContentManager, Rectangle rectangle)
        {
            Position = position;
            Scale = scale;
            TextureFile = file;
            Content = theContentManager;
            TextureFile = file;
            m_Rectangle = rectangle;
        }
        
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(m_SpriteTexture, Position,
               m_Rectangle, Color.White,
               m_Rotation, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public Vector2 Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
            }
        }

        public float Scale
        {
            get
            {
                return m_Scale;
            }
            set
            {
                m_Scale = value;
            }

        }

        public string TextureFile
        {
            get
            {
                return m_File;
            }
            set
            {
                m_File = value;
                m_SpriteTexture = Content.Load<Texture2D>(m_File);
            }

        }

        public ContentManager Content
        {
            get
            {
                return m_Content;
            }
            set
            {
                m_Content = value;
            }

        }

        public float Rotation
        {
            get
            {
                return m_Rotation;
            }
            set
            {
                m_Rotation = value;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return m_Rectangle;
            }
            set
            {
                m_Rectangle = value;
            }
        }

        private Vector2 m_Position;
        private Texture2D m_SpriteTexture;
        private float m_Rotation;
        private float m_Scale;
        private string m_File;
        private ContentManager m_Content;
        private Rectangle m_Rectangle = new Rectangle();


    }
}
