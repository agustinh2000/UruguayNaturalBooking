using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.ForResponseAndRequest
{
    public abstract class ModelBaseForRequestAndResponse<E, M>
    where E : class
    where M : ModelBaseForRequestAndResponse<E, M>, new()
    {
        public static IEnumerable<M> ToModel(IEnumerable<E> entities)
        {
            List<E> entitiesInList = entities.ToList();
            List<M> toReturn = new List<M>();
            foreach(E entity in entitiesInList)
            {
                toReturn.Add(ToModel(entity)); 
            }

            toReturn.AsEnumerable(); 
            return toReturn;
        }

        public static M ToModel(E entity)
        {
            if (entity == null) return null;
            return new M().SetModel(entity);
        }

        public static E ToEntity(M model)
        {
            if (model == null) return null;
            return model.ToEntity();
        }

        public abstract E ToEntity();

        protected abstract M SetModel(E entity);
    }
}
