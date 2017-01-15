﻿using System;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.Logging;

namespace ITfamily.Utils.AdditionalDataBase
{
    public class BaseOtherOperation
    {
        /// <summary>
        /// Context
        /// </summary>
        public OtherContext Context { get; set; }

        public BaseOtherOperation()
        {
            Name = GetType().ToString();
        }

        /// <summary>
        /// Name of bissnes operation
        /// </summary>
        public String Name
        {
            get; 
            private set; 
        }
        
        protected virtual void InTransaction(){}

        protected virtual void OnBeginTransaction(){}

        protected virtual void CloseTransaction() { }

        /// <summary>
        /// Excecute all transaction
        /// </summary>
        public void ExcecuteTransaction()
        {

            Context = new OtherContext();
            try
            {

                OnBeginTransaction();
                //отрытие тр.
                InTransaction();
                //отрытие тр.
                
                CloseTransaction();
            }
            catch (Exception ex)
            {
                Log.AddError(ex.Message);
                Log.AddError("Execute operation " + Name);
                Log.AddError(ex.StackTrace);
               
                throw;
            }
            finally
            {
                //Context.Dispose();
            }
        } 
    }
}