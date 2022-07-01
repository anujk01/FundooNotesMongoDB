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
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorManager manager;

        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }


        [Authorize]
        [HttpPost]
        [Route("addCollaborator")]

        public async Task<IActionResult> AddCollaborator([FromBody] CollaboratorModel addCollaborator)
        {
            try
            {
                var result = await this.manager.AddCollaborator(addCollaborator);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<CollaboratorModel> { Status = true, Message = "Collaborator Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Collaborator Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }

        }

        [Authorize]
        [HttpDelete("deleteCollaborator")]
        public async Task<IActionResult> DeleteCollaborator(CollaboratorModel deleteCollaborator)
        {
            try
            {
                bool result = await this.manager.DeleteCollaborator(deleteCollaborator);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<CollaboratorModel> { Status = true, Message = "Collaborator Successfully" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<CollaboratorModel> { Status = false, Message = "Collaborator Not Deleted" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpGet("getAllCollaborator")]
        public IEnumerable<CollaboratorModel> GetAllCollaborator()
        {
            try
            {
                var result = this.manager.GetAllCollaborator();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
