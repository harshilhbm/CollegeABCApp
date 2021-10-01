using CollegeABCApp.Model;
using CollegeABCApp.Repository;
using CollegeABCApp.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CollegeABCApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Student")]
    public class StudentController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;

        public StudentController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
        }

        [HttpGet]
        [Route("GetAllStudent")]
        public IActionResult GetAllUsers()
        {
            var data = DbClientFactory<StudentDbClient>.Instance.GetAllStudent(appSettings.Value.DbConn);
            return Ok(data);
        }

        [HttpGet]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var msg = new Message<StudentModel>();
            if (id.Length >= 36)
            {
                var data = DbClientFactory<StudentDbClient>.Instance.GetStudent(id, appSettings.Value.DbConn);
                return Ok(data);
            }   
            else
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Invalid id";
                return Ok(msg);
            }

        }


        [HttpGet]
        [HttpGet("GetAllCountry")]
        public IActionResult GeCountry()
        {
            var data = DbClientFactory<StudentDbClient>.Instance.GetCountry(appSettings.Value.DbConn);
            return Ok(data);
        }

        [HttpGet]
        [HttpGet("Country/{id}")]
        public IActionResult GetByCountryId(string id)
        {
            var data = DbClientFactory<StudentDbClient>.Instance.GetCountryById(id, appSettings.Value.DbConn);
            return Ok(data);
        }

        [HttpGet]
        [HttpGet("Country/{id}/State")]
        public IActionResult GetByStateId(string id)
        {
            var msg = new Message<StudentModel>();
            if (id != null)
            {
                var data = DbClientFactory<StudentDbClient>.Instance.GetState(id, appSettings.Value.DbConn);
                return Ok(data);
            }
            else
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Invalid id";
                return Ok(msg);
            }
        }


        [HttpGet]
        [HttpGet("State/{id}/City")]
        public IActionResult GetByCityId(string id)
        {
            var msg = new Message<StudentModel>();
            if (id != null)
            {
                var data = DbClientFactory<StudentDbClient>.Instance.GetCity(id, appSettings.Value.DbConn);
                return Ok(data);
            }
            else
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Invalid id";
                return Ok(msg);
            }
        }

        [HttpPost]
        [Route("SaveStudent")]
        public IActionResult SaveUser([FromBody] StudentModel model)
        {
            var msg = new Message<StudentModel>();
            var data = DbClientFactory<StudentDbClient>.Instance.SaveStudent(model, appSettings.Value.DbConn);
            if (data == "C2002")
            {
                msg.IsSuccess = true;
                msg.ReturnMessage = "Student saved successfully";
            }
            else if (data == "C2001")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Student updated successfully";
            }
            else if (data == "C202")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Mobile Number already exists";
            }
            return Ok(msg);
        }

        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public IActionResult DeleteUser(string id)
        {
            var msg = new Message<StudentModel>();
            var data = DbClientFactory<StudentDbClient>.Instance.DeleteStudent(id, appSettings.Value.DbConn);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                msg.ReturnMessage = "Student deleted";
            }
            else if (data == "C203")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Invalid record";
            }
            return Ok(msg);
        }



    }
}
