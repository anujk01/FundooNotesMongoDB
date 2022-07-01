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
    public class NoteController : ControllerBase
    {
        private readonly INoteManager manager;

        public NoteController(INoteManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("addNote")]

        public async Task<IActionResult> AddNote([FromBody] NoteModel addNote)
        {
            try
            {
                var result = await this.manager.AddNote(addNote);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Note Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note Not Added" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }

        }

        [Authorize]
        [HttpPut("updateNote")]
        public async Task<IActionResult> UpdateNote(NoteModel updateNote)
        {
            try
            {
                var result = await this.manager.UpdateNote(updateNote);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Note Updated Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Update Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpDelete("deleteNote")]
        public async Task<IActionResult> DeleteNote(NoteModel deleteNote)
        {
            try
            {
                bool result = await this.manager.DeleteNote(deleteNote);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Book Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<NoteModel> { Status = false, Message = "Book Not Deleted" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpPut("changeColour")]
        public async Task<IActionResult> ChangeColour(NoteModel changeColour)
        {
            try
            {
                var result = await this.manager.ChangeColour(changeColour);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Colour Changed Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Colour Change Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("archiveNote")]
        public async Task<IActionResult> ArchiveNote(NoteModel archiveNote)
        {
            try
            {
                var result = await this.manager.ArchiveNote(archiveNote);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Archived Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Archived Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("remainderNote")]
        public async Task<IActionResult> Remainder(NoteModel remainder)
        {
            try
            {
                var result = await this.manager.Remainder(remainder);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Remainder Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Remainder Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("trash")]
        public async Task<IActionResult> Trash(NoteModel trash)
        {
            try
            {
                var result = await this.manager.Trash(trash);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Trash Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Trash Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("pin")]

        public async Task<IActionResult> Pin(NoteModel pin)
        {
            try
            {
                var result = await this.manager.Pin(pin);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteModel> { Status = true, Message = "Pinned Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Pin Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpGet("getAllNotes")]
        public IEnumerable<NoteModel> GetAllNotes()
        {
            try
            {
                var result = this.manager.GetAllNotes();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
