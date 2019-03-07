using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace upload
{
    /// <summary>
    /// fileupload 的摘要说明
    /// </summary>
    public class fileupload : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["file"];
            string straa = context.Request.Form["appname"];
            string strbb = context.Request.Form["logoimg"];
         
            if (file == null)
            {
                context.Response.Write("hello world");
            }
            else
            {
                string imgtype = Path.GetExtension(file.FileName);
                string year =DateTime.Now.ToString("yyyy");
                string month = DateTime.Now.ToString("MM");
                string day = DateTime.Now.ToString("dd");
                string hour = DateTime.Now.ToString("hh");
                string second = DateTime.Now.ToString("ss");
                string returnURL=year + "/" + month + "/" + day + "/";
                String serverURL = "/RecorderDoc/" + returnURL;
                string dirFullPath = HttpContext.Current.Server.MapPath(serverURL);
                if (!Directory.Exists(dirFullPath))//如果文件夹不存在，则先创建文件夹
                {
                    Directory.CreateDirectory(dirFullPath);
                }
                string type="";
                string name = "";
                if (imgtype == ".apk")
                {
                     name = straa + file.FileName;
                }
                else
                {
                    name = strbb + file.FileName;
                }
                switch(file.FileName)
                {
                    case "居民端.apk" :
                        type="pe";
                        break;
                    case "维修端.apk":
                        type="de";
                        break;
                    default:
                        type = "no";
                        break;
                }

                //string name = hour + second;//生成hashname
               // string name = file.FileName;
                file.SaveAs(dirFullPath+name);
                string str = "{\"code\":0,\"msg\":\"success\",\"src\":\"" + returnURL + name + "\",\"type\":\"" + type + "\"}";
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.Write(str);
            }
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