using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; 

/* List<String> 형태의 데이터는 바인딩시 소스로 프로퍼티 수정이 안된다.
 * 따라서 stirng 을 묶어주는 Wrapper 형태로 모델링 하여 데이터를 받는다. 
 * */

namespace WordToJson.Model
{
    public class StrWrapper
    {
        private string _inData;
        public string InData
        {
            get
            {
                return _inData;
            }
            set { _inData = value; }
        }
        public StrWrapper(string metaData)
        {
            InData = metaData;
        }
    }
}
