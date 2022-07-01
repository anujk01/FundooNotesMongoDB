using CommonLayer;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager manager;

        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }


        [Authorize]
        [HttpPost]
        [Route("addLabel")]

        public async Task<IActionResult> AddLabel([FromBody] LabelModel addLabel)
        {
            try
            {
                var result = await this.manager.AddLabel(addLabel);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LabelModel> { Status = true, Message = "Label Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Label Not Added" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }

        }

        [Authorize]
        [HttpDelete("deleteLabel")]
        public async Task<IActionResult> DeleteLabel(LabelModel deleteLabel)
        {
            try
            {
                bool result = await this.manager.DeleteLabel(deleteLabel);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<LabelModel> { Status = true, Message = "Label Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<LabelModel> { Status = false, Message = "Label Not Deleted" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpGet("getAllLabel")]
        public IEnumerable<LabelModel> GetAllLabel()
        {
            try
            {
                var result = this.manager.GetAllLabel();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
