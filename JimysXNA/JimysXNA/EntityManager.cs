///JimysXNA Created by James Goodbourn
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JimysXNA.Entities
{
    /// <summary>
    /// The entity manager is a class to provide easy management of all game entities.
    /// </summary>
    public class EntityManager
    {

        public EntityManager()
        {
            m_Entities = new List<EntityBase>();
        }

        /// <summary>
        /// create instance of the entity manager and set the drawing bounds
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        public EntityManager(int minX, int maxX, int minY, int maxY)
        {
            m_Entities = new List<EntityBase>();
            m_MinX = minX;
            m_MinY = minY;
            m_MaxX = maxX;
            m_MaxY = maxY;
        }

        /// <summary>
        /// Add entity to the manager
        /// </summary>
        /// <param name="entity"></param>
        public void Add(EntityBase entity)
        {
            entity.UID = AssignUID();
            m_NextUID++;
            m_Entities.Add(entity);
        }

        /// <summary>
        /// remove entity from the manager 
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(EntityBase entity)
        {
            m_Entities.Remove(entity);
        }

        /// <summary>
        /// remove entity with the given UID
        /// </summary>
        /// <param name="UID"></param>
        public void Remove(int UID)
        {
            var entity = m_Entities.Where(e => e.UID == UID).FirstOrDefault();
            if (entity != null)
            {
                m_Entities.Remove(entity);
            }
        }

        /// <summary>
        /// get entity with the given UID
        /// </summary>
        /// <param name="UID"></param>
        /// <returns>will return an empty EntityBase object if it doesnot exist</returns>
        public EntityBase Get(int UID)
        {
            var entity = m_Entities.Where(e => e.UID == UID).FirstOrDefault();
            if (entity != null)
            {
                return entity;
            }

            return new EntityBase();
        }

        /// <summary>
        /// Get entity fromthe manager with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>will return an empty EntityBase object if not found</returns>
        public EntityBase Get(string name)
        {
            var entity = m_Entities.Where(e => e.Name == name).FirstOrDefault();
            if (entity != null)
            {
                return entity;
            }

            return new EntityBase();
        }

        /// <summary>
        /// remove all entities from the manager
        /// </summary>
        public void Clear()
        {
            m_Entities.Clear();
        }

        /// <summary>
        /// update all entities in the manager
        /// </summary>
        public void UpdateEntities()
        {
            m_Entities.ForEach(l => l.Update());
        }

        /// <summary>
        /// draw allentities inthe manager
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawEntities(SpriteBatch spriteBatch)
        {
            foreach (var entity in m_Entities)
            {
                if (TestBounds(entity))
                {
                    entity.Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// set the drawing bounds
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        public void SetBounds(int minX, int maxX, int minY, int maxY)
        {
            m_MinX = minX;
            m_MinY = minY;
            m_MaxX = maxX;
            m_MaxY = maxY;
        }

        /// <summary>
        /// get all entities in the manager
        /// </summary>
        /// <returns></returns>
        public List<EntityBase> GetAllEntities()
        {
            return m_Entities;
        }

        /// <summary>
        /// Check to see if the entity is within the drawing bounds
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool TestBounds(EntityBase entity)
        {
            var x = entity.GetX();
            var y = entity.GetY();

            return (x > m_MinX && x < m_MaxX) && (y > m_MinY && y < m_MaxY);
        }

        private int AssignUID()
        {
            var uid = m_NextUID;
            m_NextUID++;
            return uid;
        }

        private int m_MinX = -100;
        private int m_MaxX = 2000;
        private int m_MinY = -100;
        private int m_MaxY = 2000;

        private List<EntityBase> m_Entities;

        private int m_NextUID = 0;
    }
}
