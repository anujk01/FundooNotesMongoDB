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
    public class CollaboratorRL : ICollaboratorRL
    {
        private readonly IMongoCollection<CollaboratorModel> Collaborator;

        private readonly IConfiguration configuration;

        public CollaboratorRL(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Collaborator = database.GetCollection<CollaboratorModel>("Collaborator");
        }


        public async Task<CollaboratorModel> AddCollaborator(CollaboratorModel addCollaborator)
        {
            try
            {
                var check = this.Collaborator.AsQueryable().Where(x => x.CollaboratorID == addCollaborator.CollaboratorID).SingleOrDefault();
                if (check == null)
                {
                    await this.Collaborator.InsertOneAsync(addCollaborator);
                    return addCollaborator;
                }
                return null;
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCollaborator(CollaboratorModel deleteCollaborator)
        {
            try
            {
                var ifExists = await this.Collaborator.FindOneAndDeleteAsync(x => x.CollaboratorID == deleteCollaborator.CollaboratorID);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CollaboratorModel> GetAllCollaborator()
        {
            try
            {
                return Collaborator.Find(FilterDefinition<CollaboratorModel>.Empty).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
