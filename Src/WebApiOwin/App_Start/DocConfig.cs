using Sealee.Util;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml;
using WebApiOwin.App_Start;

namespace WebApiOwin
{
    public class DocConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Assembly _thisAssembly = typeof(DocConfig).Assembly;
            string _project = MethodBase.GetCurrentMethod().DeclaringType.Namespace;//项目命名空间

            //注释  我们的post提交都会创建Dto来申明，这边添加了注释，前台页面就会显示
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            string _xmlPath = string.Format("{0}/bin/{1}.XML", path, _project);
            string path2 = path.GetSlnPath(2);
            string _xmlPath2 = Path.Combine(path2, "My.Entity\\bin\\My.Entity.xml");


            config.EnableSwagger(c =>
            {
                c.MultipleApiVersions(
                      (apiDesc, targetApiVersion) =>
                      {
                          //控制器描述
                          IEnumerable<string> versions = apiDesc.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<MyVersionAttribute>().Select(x => x.Version);
                          return versions.Any(v => $"{v.ToString()}" == targetApiVersion);
                      },
                      (vc) =>
                      {
                          //MyVersionAttribute 标识了这个属性的都需要在这里加上  这边会配合index.html页面完成
                          vc.Version("Token", "");
                          vc.Version("JwtUser", "");
                      });

                //c.SingleApiVersion("Token", "Super duper API");
                //忽略标记为已删除的属性
                c.IgnoreObsoleteProperties();

                //多文档描述
                //文档描述
                c.IncludeXmlComments(_xmlPath);
                //文档描述
                c.IncludeXmlComments(_xmlPath2);

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.CustomProvider((defaultProvider) =>
                {
                    //获取描述
                    return new MySwaggerProvider(defaultProvider, _xmlPath);
                });

                //操作过滤
                c.OperationFilter<HttpHeadFilter>();
            }); //.EnableSwaggerUi()  重点：这边我们不需要他生成页面，使用我们刚刚下载dist中的页面

        }
    }

    /// <summary>
    /// 设置SwaggerDocument 显示的数据
    /// </summary>
    public class MySwaggerProvider : ISwaggerProvider
    {
        private static ConcurrentDictionary<string, SwaggerDocument> _cache =
               new ConcurrentDictionary<string, SwaggerDocument>();

        private readonly ISwaggerProvider _swaggerProvider;
        private readonly string _xmlPath;

        public MySwaggerProvider(ISwaggerProvider swaggerProvider, string xmlPath)
        {
            _swaggerProvider = swaggerProvider;
            _xmlPath = xmlPath;
        }

        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            string cacheKey = string.Format("{0}_{1}", rootUrl, apiVersion);
            //只读取一次
            if (!_cache.TryGetValue(cacheKey, out SwaggerDocument srcDoc))
            {
                srcDoc = _swaggerProvider.GetSwagger(rootUrl, apiVersion);
                ConcurrentDictionary<string, string> pairs = GetControllerDesc(apiVersion);
                //控制器描述
                IList<Tag> tagList = new List<Tag>();
                foreach (KeyValuePair<string, string> item in pairs)
                {
                    Tag tag = new Tag();
                    tag.name = item.Key;
                    tag.description = item.Value;
                    tagList.Add(tag);
                }
                srcDoc.tags = tagList;


                srcDoc.vendorExtensions = new Dictionary<string, object> {
                        { "ControllerDesc", pairs }
                    };
                _cache.TryAdd(cacheKey, srcDoc);
            }
            return srcDoc;
        }

        /// <summary>
        /// 从API文档中读取控制器描述
        /// </summary>
        private ConcurrentDictionary<string, string> GetControllerDesc(string apiVersion)
        {
            ConcurrentDictionary<string, string> controllerDescDict = new ConcurrentDictionary<string, string>();
            if (File.Exists(_xmlPath))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(_xmlPath);
                string type = string.Empty, path = string.Empty, controllerName = string.Empty;

                string[] arrPath;
                int length = -1, cCount = "Controller".Length;
                XmlNode summaryNode = null;
                foreach (XmlNode node in xmldoc.SelectNodes("//member"))
                {
                    type = node.Attributes["name"].Value;
                    if (type.StartsWith("T:"))
                    {
                        string[] typeName = type.Split(':');
                        Type t = Type.GetType(typeName[1]);

                        //控制器
                        arrPath = type.Split('.');
                        length = arrPath.Length;
                        controllerName = arrPath[length - 1];
                        if (controllerName.EndsWith("Controller"))
                        {
                            //如果分组了
                            if (t.GetCustomAttributes<MyVersionAttribute>().Count() != 0)
                            {
                                string version = t.GetCustomAttribute<MyVersionAttribute>().Version;
                                //上面tag和下面的需要同步过滤
                                if (version == apiVersion)
                                {
                                    //获取控制器注释
                                    summaryNode = node.SelectSingleNode("summary");
                                    string key = controllerName.Remove(controllerName.Length - cCount, cCount);
                                    if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !controllerDescDict.ContainsKey(key))
                                    {
                                        controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return controllerDescDict;
        }
    }


    public class HttpHeadFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            // if (operation.parameters == null)
            //     operation.parameters = new List<Parameter>();
            ////判断是否添加授权过滤器
            // var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
            // var isAuthorized = filterPipeline.Select(fileInfo => fileInfo.Instance).Any(filter => filter is IAuthorizationFilter);
            // var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            // if (isAuthorized&&!allowAnonymous)
            // {
            //     operation.parameters.Add(new Parameter { name = "Authorization", @in = "head", description = "token", required = false, type = "string" });

            // }                                                         

            if (operation == null)
            {
                return;
            }

            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }
            if (operation.security == null)
            {
                operation.security = new List<IDictionary<string, IEnumerable<string>>>();
            }

            Parameter parameter = new Parameter
            {
                description = "The authorization token",
                @in = "header",
                name = "Authorization",
                required = true,
                type = "string"
            };


            //var parameter = new Parameter
            //{
            //    description = "The authorization token",
            //    @in = "header",
            //    name = "Authorization",
            //    required = true,
            //    type = "string"
            //};                            
            //显示锁标识
            if (apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                //parameter.required = false;
            }
            else
            {
                Dictionary<string, IEnumerable<string>> oAuthRequirements = new Dictionary<string, IEnumerable<string>> { { "Authorization", new List<string>() } };
                operation.security.Add(oAuthRequirements);
            }
            // operation.parameters.Add(parameter);    
        }
    }
}