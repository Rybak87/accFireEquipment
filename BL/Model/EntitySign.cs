using System;

namespace BL
{
    public class EntitySign
    {
        public Type typeEntity;
        public int idEntity;

        public EntitySign(Type typeEntity, int idEntity)
        {
            this.typeEntity = typeEntity;
            this.idEntity = idEntity;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
                return false;
            else if (typeEntity == ((EntitySign)obj).typeEntity && idEntity == ((EntitySign)obj).idEntity)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return typeEntity.GetHashCode() + idEntity.GetHashCode();
        }
        public static bool operator ==(EntitySign e1, object e2)
        {
            return Equals(e1, e2);
        }
        public static bool operator !=(EntitySign e1, object e2)
        {
            return !Equals(e1, e2);
        }
    }
}
