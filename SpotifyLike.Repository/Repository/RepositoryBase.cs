using Microsoft.EntityFrameworkCore;
using SpotifyLike.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Repository
{
    public abstract class RepositoryBase<T> where T : class, IIdentifier, new()
    {
        protected DbContext Context { get; set; }

        public RepositoryBase(DbContext context)
        {
            Context = context;
        } 

        public void Save(T entity)
        {
            this.Context.Add(entity);
            try
            {
                this.Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Erro ao atualizar o registro", ex);
            }
        }

        public void Update(T entity) 
        {
            this.Context.Update(entity);
            try
            {
                this.Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Erro ao atualizar o registro", ex);
            }
            
        }

        public void Delete(T entity)
        {
            var local = this.Context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));
            if (local != null)
            {
                this.Context.Entry(local).State = EntityState.Detached;
            }
            
            this.Context.Entry(entity).State = EntityState.Modified;

            this.Context.Remove(entity);
            this.Context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return this.Context.Set<T>().ToList();
        }

        public T GetById(Guid id)
        {
            return this.Context.Set<T>().Find(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression) 
        {
            return this.Context.Set<T>().Where(expression);
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return this.Find(expression).Any();
        }

    }
}
