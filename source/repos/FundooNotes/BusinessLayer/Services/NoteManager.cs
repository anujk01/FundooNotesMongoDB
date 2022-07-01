using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Services
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRL note;
        public NoteManager(INoteRL note)
        {
            this.note = note;

        }

        public async Task<NoteModel> AddNote(NoteModel addNote)
        {
            try
            {
                return await this.note.AddNote(addNote);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NoteModel> UpdateNote(NoteModel updateNote)
        {
            try
            {
                return await this.note.UpdateNote(updateNote);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteNote(NoteModel deleteNote)
        {
            try
            {
                return await this.note.DeleteNote(deleteNote);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NoteModel> ChangeColour(NoteModel changeColour)
        {
            try
            {
                return await this.note.ChangeColour(changeColour);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NoteModel> ArchiveNote(NoteModel archiveNote)
        {
            try
            {
                return await this.note.ArchiveNote(archiveNote);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NoteModel> Remainder(NoteModel remainder)
        {
            try
            {
                return await this.note.Remainder(remainder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NoteModel> Trash(NoteModel trash)
        {
            try
            {
                return await this.note.Trash(trash);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NoteModel> Pin(NoteModel pin)
        {
            try
            {
                return await this.note.Pin(pin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<NoteModel> GetAllNotes()
        {
            try
            {
                return this.note.GetAllNotes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
