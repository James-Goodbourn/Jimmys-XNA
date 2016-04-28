///JimysXNA Created by James Goodbourn

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JimysXNA.Sprites;

namespace JimysXNA.Entities
{
    /// <summary>
    /// Base entity class - Update and draw methods can be overridden
    /// </summary>
    public class EntityBase
    {
        public EntityBase()
        {

        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">screen position</param>
        /// <param name="scale">entity scale</param>
        /// <param name="file">name of file in content</param>
        /// <param name="content">the content</param>
        /// <param name="name">entity name (defaults to "") - this can be used for entity lookup</param>
        public EntityBase(Vector2 position, float scale, string file, ContentManager content, string name = "")
        {
            m_EntitySprite = new Sprite(position, scale, file, content);
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">screen position</param>
        /// <param name="scale">entity scale</param>
        /// <param name="file">name of file in content</param>
        /// <param name="content">the content</param>
        /// <param name="rectangle">drawing rectangle (spritesheet use)</param>
        /// <param name="name"> name of the entity (defaults to"") - this can be used for entity lookup</param>
        public EntityBase(Vector2 position, float scale, string file, ContentManager content, Rectangle rectangle, string name = "")
        {
            m_EntitySprite = new Sprite(position, scale, file, content, rectangle);
            Name = name;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            m_EntitySprite.Draw(spriteBatch);
        }

        public virtual void Update()
        { 
            
        }

        /// <summary>
        /// Set rotation of the entity
        /// </summary>
        /// <param name="rotation"></param>
        public void SetRotation(float rotation)
        {
            m_EntitySprite.Rotation = rotation;
        }

        /// <summary>
        /// set the scale of the entity (1.0f is oroginal)
        /// </summary>
        /// <param name="scale"></param>

        /// <summary>
        /// set the name of the file in the 'content'
        /// </summary>
        /// <param name="file"></param>
        public void SetFile(string file)
        {
            m_EntitySprite.TextureFile = file;
        }

        /// <summary>
        /// set the content  manager the entity uses
        /// </summary>
        /// <param name="content"></param>
        public void SetContent(ContentManager content)
        {
            m_EntitySprite.Content = content;
        }

        /// <summary>
        /// set the screen position of the sprite
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y"> y position</param>
        public void SetPosition(float x, float y)
        {
            m_EntitySprite.Position = new Vector2(x, y);
        }

        /// <summary>
        /// set the screen positionof the entity
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(Vector2 position)
        {
            m_EntitySprite.Position = position;
        }

        /// <summary>
        /// Set the x position of the entity
        /// </summary>
        /// <param name="x"></param>
        public void SetXPosition(float x)
        {
            var position = new Vector2(x, GetY());

            m_EntitySprite.Position = position;
        }

        /// <summary>
        /// set the y position of the entity
        /// </summary>
        /// <param name="y"></param>
        public void SetYPosition(float y)
        {
            var position = new Vector2(GetX(), y);

            m_EntitySprite.Position = position;
        }

        /// <summary>
        /// set thee scaleof the entity
        /// </summary>
        /// <param name="scale"></param>
        public void SetScale(float scale)
        {
            m_EntitySprite.Scale = scale;
        }

        /// <summary>
        /// set the sprite rectangle
        /// </summary>
        /// <param name="xCoord"></param>
        /// <param name="yCoord"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetRectangle(int xCoord, int yCoord, int width, int height)
        {
            var rectangle = new Rectangle(xCoord, yCoord, width, height);
            m_EntitySprite.Rectangle = rectangle;
        }

        /// <summary>
        /// get the sceen position of the entity
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPosition()
        {
            return m_EntitySprite.Position;
        }

        /// <summary>
        /// get the x position of the entity
        /// </summary>
        /// <returns></returns>
        public float GetX()
        {
            return m_EntitySprite.Position.X;
        }

        /// <summary>
        /// get the y position of the entity
        /// </summary>
        /// <returns></returns>
        public float GetY()
        {
            return m_EntitySprite.Position.Y;
        }

        /// <summary>
        /// get the scale of the entity
        /// </summary>
        /// <returns></returns>
        public float GetScale()
        {
            return m_EntitySprite.Scale;
        }

        /// <summary>
        /// get the rotation of the entity
        /// </summary>
        /// <returns></returns>
        public float GetRotation()
        {
            return m_EntitySprite.Rotation;
        }

        /// <summary>
        /// get the entity rectangle
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            return m_EntitySprite.Rectangle;
        }
       
        /// <summary>
        /// get the entity UID
        /// </summary>
        public int UID
        {
            get
            {
                return m_UID;
            }
            set
            {
                m_UID = value;
            }
        }

        public string Name { get; set; }

        private Sprite m_EntitySprite;

        private int m_UID;
    }
}
