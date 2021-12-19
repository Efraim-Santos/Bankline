using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        protected BaseController(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }

        public string GetPathRootFiles()
        {
            return _appEnvironment.WebRootPath;
        }
    }
}
