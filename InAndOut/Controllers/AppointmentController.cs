using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class AppointmentController : Controller
    {
        // This is the action result (view)
        // An Action result is the action of or return type of action methods. 
        // Action results:
        // Status Code Results
        // Status Code w/ Object Results
        // Redirect Results
        // File Results
        // Content Results

        // pass pareameter as part of an URL | 

        // pass


        [Route("Details/{id?}")]
        [Route("OtherRoute/{id?}")]
        public IActionResult SomeActionName(int id)
        {
            // you can have attribute based routes like this.
            return Ok("You have entered id =  " + id);
        }


        #region :: Status Codes Without Object Result   ::
        /// <summary>
        /// This is a statuscoderesult. When executed, it will produce an empty Status200OK
        /// </summary>
        /// 
        public IActionResult OKResult()
        {
            return Ok();
        }

        /// <summary>
        /// Returns a Created (201) response with a Location Header. 
        /// This indicateds the request has been fulfilled and has resulted. 
        /// in one or more new resources being created
        /// </summary>
        /// <returns></returns>
        public IActionResult CreatedResult() 
        {
            return Created("http:/example.org/myitem", new { name = "new item"});
        }


        /// <summary>
        /// it indicates a bad request by the user
        /// it does not take any argument. 
        /// <returns></returns>
        public IActionResult BadRequestResult()
        {
            return BadRequest();
        }

        /// <summary>
        /// Returns 401 Status, It's difference to 
        /// to ChallengeResults is that is just returns a status code and doesn't 
        /// do anything else. 
        /// In contrast with its counterpar that has many options for redirecting the user
        /// and options related to asp.net core identity
        /// </summary>
        /// <returns></returns>
        public IActionResult UnauthorizedResult()
        {
            return Unauthorized();
        }

        /// <summary>
        /// This means the request was unknown. 
        /// </summary>
        /// <returns></returns>
        public IActionResult NotFoundResult()
        {
            return NotFound();
        }

        /// <summary>
        /// THis action result returns 415 status code which means server cannont continue
        /// to process the request with the give payload. 
        /// It's doing this by inspecting the content-type or content-encoding of the current request or 
        /// inspecting the incoming data directly. 
        /// </summary>
        /// <returns></returns>
        public IActionResult UnsupportedMediaTypeResult()
        {
            return new UnsupportedMediaTypeResult();
        }

        #endregion


        #region :: Status Codes With Object Result ::

        /// <summary>
        /// When executed performs content negotiation, 
        /// formats the entity body, and will rpoduce a Status200OK response.
        /// </summary>
        /// <returns></returns>
        public IActionResult OkObjectResult()
        {
            var result = new OkObjectResult(new { message = "200 ok", currentDate = DateTime.Now });
            return result;

        }

        /// <summary>
        /// returns a 201 response with location header.
        /// </summary>
        /// <returns></returns>
        public IActionResult CreatedObjectResult()
        {
            var result = new CreatedAtActionResult(
                "CreatedObjectResult", 
                "statuscodeobjects", 
                "", 
                new {
                message = "201 Created",
                currentDate = DateTime.Now
                }
             );
            return result;
        }

        public IActionResult BadRequestObjectResult()
        {
            var result = new BadRequestObjectResult(new
            { message = "400 Bad Request", currentDate = DateTime.Now });
            return result;
        }

        /// <summary>
        /// This is similar to NotFoundREsult, with the difference being that 
        /// you can pass an object with the 404 response. 
        /// </summary>
        /// <returns></returns>
        public IActionResult NotFoundObjectResult()
        {
            var result = new NotFoundObjectResult(new { message = "404 Not Found", currentDate = DateTime.Now });
            return result;
        }


        #endregion

        #region :: Redirect Results ::

        public IActionResult RedirectResult()
        {
            return Redirect("https://www.google.com");
        }

        /// <summary>
        /// Redirect to spcified URL is it's local URL (also relative)
        /// if not it will throw an exception, permant 301 property set to false
        /// this is only on the internal site. 
        /// </summary>
        /// <returns></returns>
        public IActionResult LocalRedirectResult()
        {
            return LocalRedirect("/redirects/target");
        }

        /// <summary>
        ///  Can redirect us to an action,
        ///  It takes in the action name, controller name and route value.
        /// </summary>
        /// <returns></returns>
        public IActionResult RedirectToActionResult()
        {
            return RedirectToAction("target", "Appointment");

        }

        /// <summary>
        ///  Returns the file content as a pdf content
        /// </summary>
        /// <returns></returns>
        public IActionResult FileResult()
        {
            return File("!/downloads/pdf-sample.pdf", "application/pdf");  
        }

        /// <summary>
        /// Return the file as an array of bytes as you see in FileContentActionResult
        /// </summary>
        /// <returns></returns>
        public IActionResult FileContentResult()
        {
            var pdfBytes = System.IO.File.ReadAllBytes("wwwroot/downloads/pdf-sample.pdf");
            return new FileContentResult(pdfBytes, "application/pdf");
        }

        /// <summary>
        /// if you want to read a file form a vitrual address and return it. 
        /// </summary>
        /// <returns></returns>
        public IActionResult VirtualFileResult()
        {
            return new VirtualFileResult("/downloads/pdf-sample.pdf", "application/pdf");
        }

        #endregion

        #region :: Content Results :: 


        // Any public methods inside a controller is an action. 
        // Action methods cannot beoverloaded, or static. 
        public IActionResult Index()
        {
            return View();
            // string todaysDate = DateTime.Now.ToShortDateString();
            //return Ok(todaysDate);
        }

        /// <summary>
        ///  Renders a specified partial view to the response stream
        /// </summary>
        /// <returns></returns>
        public IActionResult PartialViewResult()
        {
            return PartialView();
        }
        /// <summary>
        /// Returns (JSON)
        /// </summary>
        /// <returns></returns>
        public IActionResult JsonResult()
        {
            return Json(new { 
                message = "This is a JSON result",
                date = DateTime.Now
            });
        }

        /// <summary>
        /// This is just straight content. 
        /// </summary>
        /// <returns></returns>
        public IActionResult ContentResult()
        {
            return Content("Here's The content");
        }

        #endregion

        #region Passing Parameters
        //// Passing parameters in a URL, you need to define a route that would contain a parameter. 
        //// Notice the id is part of the routing. So, valid URL would be like GET/1
        //public IActionResult GetId([FromRoute]int id)
        //{
        //    return Ok("You have entered id : " + id);
        //}

        //// This is a way to pass via Query String
        //public IActionResult GetSomeData([FromQuery] string values)
        //{
        //    return Ok("The value: " + values + "is from Query string");
        //}

        //// Getting attributes from the header record,
        //// if you want to pass tokens where user should not see any of the values. 
        //[HttpPost]
        //public IActionResult PostSomething([FromHeader] string parentRequestId)
        //{
        //    return Ok($"Got a header with parentRequestId: {parentRequestId}");
        //}
        #endregion

    }
}
