using CommonLayer;
using Microsoft.Extensions.Configuration;
using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        private readonly IMongoCollection<NoteModel> Note;

        private readonly IConfiguration configuration;

        public NoteRL(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Note = database.GetCollection<NoteModel>("Note");
        }

        public async Task<NoteModel> AddNote(NoteModel addNote)
        {
            try
            {
                var check = this.Note.AsQueryable().Where(x => x.NoteID == addNote.NoteID).SingleOrDefault();
                if (check == null)
                {
                    await this.Note.InsertOneAsync(addNote);
                    return addNote;
                }
                return null;
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<NoteModel> UpdateNote(NoteModel updateNote)
        {
            try
            {
                var ifExists = await this.Note.Find(x => x.NoteID == updateNote.NoteID).FirstOrDefaultAsync();
                if (ifExists != null)
                {
                    await this.Note.UpdateOneAsync(x => x.NoteID == updateNote.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Title, updateNote.Title)
                        .Set(x => x.Description, updateNote.Description)
                        .Set(x => x.IsArchive, updateNote.IsArchive)
                        .Set(x => x.Color, updateNote.Color)
                        .Set(x => x.IsPin, updateNote.IsPin)
                        .Set(x => x.IsRemainder, updateNote.IsRemainder));
                    return ifExists;
                }
                else
                {
                    await this.Note.InsertOneAsync(updateNote);
                    return updateNote;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteNote(NoteModel deleteNote)
        {
            try
            {
                var ifExists = await this.Note.FindOneAndDeleteAsync(x => x.NoteID == deleteNote.NoteID);
                return true;
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
                var note = await this.Note.Find(x => x.NoteID == changeColour.NoteID).FirstOrDefaultAsync();
                if (note != null)
                {
                    await this.Note.UpdateOneAsync(x => x.NoteID == changeColour.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.Color, changeColour.Color));
                    return note;
                }
                else
                {
                    await this.Note.InsertOneAsync(changeColour);
                    return changeColour;
                }
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
                var note = await this.Note.Find(x => x.NoteID == archiveNote.NoteID).SingleOrDefaultAsync();
                if (note != null)
                {
                    if (note.IsArchive == true)
                    {
                        note.IsArchive = false;
                    }
                    if (note.IsArchive == false)
                    {
                        note.IsArchive = true;
                    }
                }
                return note;
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
                var ifExists = await this.Note.Find(x => x.NoteID == remainder.NoteID).FirstOrDefaultAsync();
                if (ifExists != null)
                {
                    await this.Note.UpdateOneAsync(x => x.NoteID == remainder.NoteID,
                        Builders<NoteModel>.Update.Set(x => x.IsRemainder, remainder.IsRemainder));
                    return ifExists;
                }
                else
                {
                    await this.Note.InsertOneAsync(remainder);
                    return remainder;
                }
                
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
                var note = await this.Note.Find(x => x.userID == trash.userID && x.NoteID == trash.NoteID).FirstOrDefaultAsync();
                if (note != null)
                {
                    if (note.IsTrash == true)
                    {
                        note.IsTrash = false;
                    }
                    if (note.IsTrash == false)
                    {
                        note.IsTrash = true;
                    }
                }
                return trash;
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
                var note = await this.Note.Find(x => x.NoteID == pin.NoteID).SingleOrDefaultAsync();
                if (note != null)
                {
                    if (note.IsPin == true)
                    {
                        note.IsPin = false;
                    }
                    if (note.IsPin == false)
                    {
                        note.IsPin = true;
                    }
                    return note;
                }
                else
                {
                    await this.Note.InsertOneAsync(pin);
                    return pin;
                }
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
                return Note.Find(FilterDefinition<NoteModel>.Empty).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


