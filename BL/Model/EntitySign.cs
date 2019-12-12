using System;

namespace BL
{
    public class EntitySign
    {
        public Type Type { get;  }
        public int Id { get; }

        public EntitySign(Type typeEntity, int idEntity)
        {
            this.Type = typeEntity;
            this.Id = idEntity;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
                return false;
            else if (Type == ((EntitySign)obj).Type && Id == ((EntitySign)obj).Id)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return Type.GetHashCode() + Id.GetHashCode();
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
