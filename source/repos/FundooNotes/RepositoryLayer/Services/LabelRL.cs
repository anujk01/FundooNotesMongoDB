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
    public class LabelRL : ILabelRL
    {
        private readonly IMongoCollection<LabelModel> Label;

        private readonly IConfiguration configuration;

        public LabelRL(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Label = database.GetCollection<LabelModel>("Label");
        }


        public async Task<LabelModel> AddLabel(LabelModel addLabel)
        {
            try
            {
                var result = await this.Label.Find(x => x.NoteID == addLabel.NoteID).FirstOrDefaultAsync();
                if (result != null)
                {
                    await this.Label.InsertOneAsync(addLabel);
                }
                else
                {
                    return null;
                }
                var check = this.Label.AsQueryable().Where(x => x.LabelID == addLabel.LabelID).SingleOrDefault();
                if (check == null)
                {
                    await this.Label.InsertOneAsync(addLabel);
                    
                }
                return addLabel;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteLabel(LabelModel deleteLabel)
        {
            try
            {
                var ifExists = await this.Label.FindOneAndDeleteAsync(x => x.LabelID == deleteLabel.LabelID);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<LabelModel> GetAllLabel()
        {
            try
            {
                return Label.Find(FilterDefinition<LabelModel>.Empty).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
