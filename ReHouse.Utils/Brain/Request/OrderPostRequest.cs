using System;

namespace ITfamily.Utils.Brain.Request
{
    public class OrderPostRequest : BaseBrainRequest
    {
        /// <summary>
        /// Consists json - list of DataPostOrder objects
        /// </summary>
        public String data { get; set; }
    }
}