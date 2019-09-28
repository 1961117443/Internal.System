using AutoMapper;
using Internal.Common.Core;
using Internal.Common.Data;
using Internal.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.Uility
{
    public static class DataModelExtendsion
    { 
        public static void AssignValuesToEntity<TEntity>(this BaseDto dto, BaseModel<TEntity> entity)
        {
            if (dto==null)
            {
                return;
            }
            Mapper.Instance.Map(dto, entity, dto.GetType(), entity.GetType());
        }

        public static void FetchValuesFromEntity<TEntity>(this BaseDto dto, BaseModel<TEntity> entity)
        {
            if (dto==null)
            {
                return;
            }
            Mapper.Instance.Map(dto, entity, dto.GetType(), entity.GetType());
        }

        public static IList<TDto> ToDtoList<TEntity, TDto>(this IList<BaseModel<TEntity>> entityList)
        {
            return Mapper.Instance.Map<IList<BaseModel<TEntity>>, IList<TDto>>(entityList);
        }
        public static IEnumerable<TDto> ToDtoList<TEntity, TDto>(this IEnumerable<TEntity> entityList)
        {
            return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(entityList);
        }


        /// <summary>
        /// 复制对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TEntity Clone<TEntity,T>(this BaseModel<T> entity) where TEntity:BaseModel<T>
        {
             return (TEntity)Mapper.Instance.Map(entity, entity.GetType(), typeof(TEntity));
            //return Mapper.Map<T,T>(entity);
        }

 
    }
}
