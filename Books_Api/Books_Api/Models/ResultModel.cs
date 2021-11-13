using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books_Api.Models
{
    public class ResultModel
    {
        #region [ Properties ]

        public bool Success { get; set; }

        public string Messages { get; set; }

        public object Data { get; set; }

        #endregion

        public ResultModel()
        {
            Success = true;
        }
    }
}
