using ManagerLayer.Interface;
using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Services
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRL label;
        public LabelManager(ILabelRL label)
        {
            this.label = label;

        }

        public async Task<LabelModel> AddLabel(LabelModel addLabel)
        {
            try
            {
                return await this.label.AddLabel(addLabel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteLabel(LabelModel deleteLabel)
        {
            try
            {
                return await this.label.DeleteLabel(deleteLabel);
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
                return this.label.GetAllLabel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
