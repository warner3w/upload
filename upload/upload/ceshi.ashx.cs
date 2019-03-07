using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace upload
{
    /// <summary>
    /// ceshi 的摘要说明
    /// </summary>
    public class ceshi : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["file"];
            string imgtype = Path.GetExtension(file.FileName);
            string dirFullPath = HttpContext.Current.Server.MapPath("/RecorderDoc/");
            if (!Directory.Exists(dirFullPath))//如果文件夹不存在，则先创建文件夹
            {
                Directory.CreateDirectory(dirFullPath);
            }
            file.SaveAs(dirFullPath + file.FileName);
            string str = "{\"code\":0,\"msg\":\"success\",\"src\":\"123\"}";
            context.Response.Write(str);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}