using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase;

namespace ReHouse.Utils.BusinessOperations
{
    public class BaseOperation
    {
        /// <summary>
        /// Context
        /// </summary>
        public DbReHouse Context { get; set; }

        public BaseOperation()
        {
            Name = GetType().ToString();
            Errors = new Dictionary<string, string>();
        }

        /// <summary>
        /// Name of bissnes operation
        /// </summary>
        public String Name
        {
            get;
            protected set;
        }
        public String RussianName
        {
            get;
            protected set;
        }

        public IDictionary<String, String> Errors
        {
            get;
            protected set;
        }

        public Boolean Success
        {
            get
            {
                return Errors.Count > 0 ? false : true;
            }
        }

        protected virtual void InTransaction()
        { }

        protected virtual void OnBeginTransaction()
        { }

        protected virtual void CloseTransaction()
        { }

        /// <summary>
        /// Excecute all transaction
        /// </summary>
        public void ExcecuteTransaction()
        {

            Context = new DbReHouse();

            OnBeginTransaction();
            //отрытие тр.
            InTransaction();
            //выполнение тр.

            CloseTransaction();
        }
    }
}