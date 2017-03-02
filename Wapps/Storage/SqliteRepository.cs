using System;
using System.Linq;
using Wapps.Orm;
using SQLite;
using System.Collections.Generic;
using System.Linq.Expressions;
using SQLite.Net;

namespace Wapps
{
	public class SqliteRepository <TEntity, TPKey> where TEntity : class, new()
	{
		/// <summary>
		/// Gets the db.
		/// </summary>
		/// <value>The db.</value>
		protected DBContext Db 
		{ 
			get { return DBContext.Get (typeof(TEntity)); } 
		}
			
		/// <summary>
		/// Finds all.
		/// </summary>
		/// <returns>The all.</returns>
		public virtual IList<TEntity> FindAll () 
		{
			var query = Db.Conn.Table<TEntity> ();
			return Db.ToList <TEntity> (query);
		}

		/// <summary>
		/// Find the specified id.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public virtual TEntity Find (TPKey id) 
		{
			return Db.Find<TEntity> (id);
		}

		/// <summary>
		/// Finds all where param.
		/// </summary>
		protected virtual IList<TEntity> FindAllWhere (Expression<Func<TEntity, bool>> predExpr)
		{
			var query = Db.Conn.Table<TEntity> ().Where (predExpr);
			return Db.ToList <TEntity> (query);
		}

		/// <summary>
		/// Finds all where.
		/// </summary>
		/// <returns>The all where.</returns>
		/// <param name="predExpr">Pred expr.</param>
		/// <param name="orderBy">Order by.</param>
		/// <typeparam name="U">The 1st type parameter.</typeparam>
		protected virtual IList<TEntity> FindAllWhere <U> (Expression<Func<TEntity, bool>> predExpr, Expression<Func<TEntity, U>> orderBy)
		{
			if (predExpr == null) {
				var query = Db.Conn.Table<TEntity> ().OrderBy (orderBy);
				return Db.ToList <TEntity> (query);
			} else {
				var query = Db.Conn.Table<TEntity> ().Where (predExpr).OrderBy (orderBy);
				return Db.ToList <TEntity> (query);
			}
		}

		/// <summary>
		/// Query this instance.
		/// </summary>
		internal TableQuery <TEntity> Query () 
		{
			return Db.Conn.Table<TEntity> ();
		}

		/// <summary>
		/// Save this instance.
		/// </summary>
		public virtual void Save (TEntity entity) 
		{
			Db.InsertOrReplace (entity);
		}

		/// <summary>
		/// Delete this instance.
		/// </summary>
		public virtual void Delete(TEntity entity)
		{
			Db.Delete(entity);
		}
	}
}

