using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        Task<NoteModel> AddNote(NoteModel addNote);
        Task<NoteModel> UpdateNote(NoteModel addNote);
        Task<bool> DeleteNote(NoteModel deleteNote);
        Task<NoteModel> ChangeColour(NoteModel changeColour);
        Task<NoteModel> ArchiveNote(NoteModel archiveNote);
        Task<NoteModel> Remainder(NoteModel remainder);
        Task<NoteModel> Trash(NoteModel trash);
        Task<NoteModel> Pin(NoteModel pin);
        IEnumerable<NoteModel> GetAllNotes();
    }
}
