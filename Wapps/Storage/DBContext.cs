using System;
using System.IO;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using SQLitePCL;

namespace Wapps.Core
{
    /// <summary>
    /// Represents a DB context (sqlconnection)
    /// </summary>
    public class DBContext
    {
        public static object Lock = new object();

        public string DBFileName { get; private set; }

        public SQLiteConnection Conn { get; set; }

        #region Private methods & properties

        [ThreadStatic]
        private static Dictionary<string, DBContext> _contexts = new Dictionary<string, DBContext>();

        /// <summary>
        /// The configurations of the different databases
        /// </summary>
        private static Dictionary<string, DBContextCfg> Cfgs = new Dictionary<string, DBContextCfg>();

        DBContext(string dbName, List<Type> types)
        {
            this.Conn = new SQLiteConnection(dbName);
            try
            {
                lock (Lock)
                {
                    foreach (Type type in types)
                    {
                        this.Conn.CreateTable(type);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DBContext constructor: " + ex.Message);
            }
        }

        private class DBContextCfg
        {
            public string DBName { get; set; }

            public List<Type> Types { get; set; }

            public DBContextCfg() { }
        }

        #endregion

        #region Public methods & properties

        /// <summary>
        /// Add the specified dbName and addTypesDelegate.
        /// </summary>
        public delegate void AddTypesDelegateHandler(List<Type> types);

        public static string DBPath { get; set; }

        /// <summary>
        /// Add the specified dbName and addTypesDelegate.
        /// </summary>
        public static void Add(string dbName, List<Type> types)
        {
            dbName = Path.Combine(DBPath, dbName);
            Cfgs[dbName] = new DBContextCfg { DBName = dbName, Types = types };
        }

        /// <summary>
        /// Attempts to retrieve an object with the given primary key from the table
        /// associated with the specified type. Use of this method requires that
        /// the given type have a designated PrimaryKey (using the PrimaryKeyAttribute).
        /// </summary>
        public static DBContext Get(string dbName)
        {
            if (_contexts == null)
            {
                _contexts = new Dictionary<string, DBContext>();
            }
            if (!_contexts.ContainsKey(dbName))
            {
                var cfg = Cfgs[dbName];
                _contexts[dbName] = new DBContext(dbName, cfg.Types);
                _contexts[dbName].DBFileName = dbName;
            }
            return _contexts[dbName];
        }

        /// <summary>
        /// Attempts to retrieve an object with the given primary key from the table
        /// associated with the specified type. Use of this method requires that
        /// the given type have a designated PrimaryKey (using the PrimaryKeyAttribute).
        /// </summary>
        public static DBContext Get(Type entityType)
        {
            string dbName = null;
            foreach (var cfg in Cfgs.Values)
            {
                if (cfg.Types.Contains(entityType))
                {
                    dbName = cfg.DBName;
                    break;
                }
            }

            if (dbName == null)
            {
                throw new Exception("That entity type isn't in any db");
            }
            return Get(dbName);
        }

        public static DBContext Get()
        {
            var enumerator = Cfgs.Values.GetEnumerator();
            enumerator.MoveNext();
            var dbName = enumerator.Current.DBName;
            return Get(dbName);
        }

        #endregion

        #region DBContext

        public void Delete(object entity)
        {
            lock (Lock)
            {
                this.Conn.Delete(entity);
            }
        }

        public void Delete<T>(object id)
        {
            lock (Lock)
            {
                this.Conn.Delete<T>(id);
            }
        }

        public T Find<T>(object id) where T : class, new()
        {
            lock (Lock)
            {
                var obj = Conn.Find<T>(id);
                return obj;
            }
        }

        public void InsertOrReplace(object obj)
        {
            lock (Lock)
            {
                Conn.InsertOrReplace(obj);
            }
        }

        public List<T> ToList<T>(TableQuery<T> query)
        {
            lock (Lock)
            {
                return query.ToList();
            }
        }

        public List<T> Query<T>(string query, object[] args) where T : class, new()
        {
            lock (Lock)
            {
                return Conn.Query<T>(query, args);
            }
        }

        public int Insert(object obj)
        {
            lock (Lock)
            {
                return Conn.Insert(obj);
            }
        }

        public void Update(object obj)
        {
            lock (Lock)
            {
                Conn.Update(obj);
            }
        }

        #endregion

    }
}

