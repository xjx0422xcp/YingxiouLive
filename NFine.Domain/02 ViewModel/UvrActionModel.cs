using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.ViewModel
{
    public class UvrModel
    {
        public int UserID { get; set; }
        public string NickName { get; set; }//昵称 
        public string UserName { get; set; }//用户名
        public string ParentName { get; set; }//推荐人
        public int ExpiryDays { get; set; }
        public string Note { get; set; }//状态
        public int layer{ get; set; }
        public string SuperiorName { get; set; }//上级
        public string SuperiorID { get; set; }

        public int point { get; set; }
    }

    public class UvrActionModel
    {
        public List<UvrModel> UvrModelList { get; set; } = new List<UvrModel>();
    }
}
