using CommonLayer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLayer
{
    public class CollaboratorModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string CollaboratorID { get; set; }

        [ForeignKey("RegisterModel")]
        public string userID { get; set; }
        public virtual RegisterModel registerModel { get; set; }

        [ForeignKey("NoteModel")]
        public string NoteID { get; set; }
        public virtual NoteModel noteModel { get; set; }
        public string Email { get; set; }
    }
}
