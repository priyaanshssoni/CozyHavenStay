using System;
using System.Linq.Expressions;

namespace CozyHavenStay.Api.RepositoryAbstractions
{
	public interface IRepository<K,T>
	{
        public Task<T> GetById(K key);
        public Task<List<T>> GetAll();
        public Task<T> Add(T item);
        public Task<T> Update(T item);
        public Task<T> Delete(K key);

    }
}

