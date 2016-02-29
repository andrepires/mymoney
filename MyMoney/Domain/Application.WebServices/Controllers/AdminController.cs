using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyMoney.Domain.Application.WebServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminController : ApiController
    {

        [Route("api/Admin/{configurationId}")]
        [HttpGet]
        public IHttpActionResult GetConfiguration(string configurationId)
        {
            try
            {

                var settingValue = ConfigurationManager.AppSettings[configurationId];
                return Ok(settingValue);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/Admin/{configurationId}")]
        [HttpPut]
        public IHttpActionResult UpdateConfiguration(string configurationId, [FromBody]string value)
        {
            try
            {
                ConfigurationManager.AppSettings.Set(configurationId, value);
                ConfigurationManager.RefreshSection("appSettings");
                return Ok("Repository Type changed to " + value);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}