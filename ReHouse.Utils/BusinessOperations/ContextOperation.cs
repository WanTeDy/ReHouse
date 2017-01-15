using System;
using ITfamily.Utils.DataBase;

namespace ITfamily.Utils.BusinessOperations
{
        /// <summary>
        /// Context operation. For all context operatio
        /// </summary>
        /// <typeparam name="TResult">Result of operation</typeparam>
        public class ContextOperation<TResult> : BaseOperation
        {
            private Func<DbItFamily, TResult> Function { get; set; }

            public TResult Result { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="function"></param>
            public ContextOperation(Func<DbItFamily, TResult> function)
            {
                Function = function;
            }

            protected override void InTransaction()
            {
                base.InTransaction();
                Result = Function(Context);
            }

            public new TResult ExcecuteTransaction()
            {
                base.ExcecuteTransaction();
                return Result;
            }
        }
}
