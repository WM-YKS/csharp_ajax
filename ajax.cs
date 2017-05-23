using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using System.Reflection;

namespace __YOUR_NAMESPACE
{
    public partial class ajax : System.Web.UI.Page{
        protected void Page_Load(object sender , EventArgs e)
        {
            // querystring으로 전달받은 함수명을 찾아 실행하도록합니다.
            methodInvoke();
        }
    }

    // 예제
    public void mockup(HttpRequest req){
        // req에 담긴 정보를 그대로 가져옵니다. 
        // 원하는 대로 코드를 작성한 후 Response Write해줍니다.
        // 되도록 ajax형태로 조작하여 넘겨줍니다.
        String jsonstr = "{ \"mesg\" : \"hello there\" }";
        Response.ContentType = "application/json";
        Response.Write(jsonstr);
        Response.Flush();
        Response.End();
    }


    ##region UTILITY

    /// <summary>
    /// queryString으로 함수명을 받아 
    /// 해당 함수를 실행합니다. 함수가 존재하지 않으면 err JSON을 넘깁니다.
    /// </summary>
    protected void methodInvoke()
    {
        if (!String.IsNullOrEmpty(Request.QueryString["method"]))
        {
            MethodInfo mi = this.GetType().GetMethod(Request.QueryString["method"]);
            if (mi != null)
            {
                object[] parameters = { Request };
                mi.Invoke(this, parameters);
            }
            else
            {
                flushErr("함수명이 잘못되었습니다");
            }
        }
        else
        {
            flushErr("쿼리 스트링에 함수명이 비어있습니다");
        }
    }
        
    #endregion
}
