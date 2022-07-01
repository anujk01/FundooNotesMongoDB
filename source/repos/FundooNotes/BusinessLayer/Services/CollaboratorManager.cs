using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Services
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRL collaborator;
        public CollaboratorManager(ICollaboratorRL collaborator)
        {
            this.collaborator = collaborator;

        }

        public async Task<CollaboratorModel> AddCollaborator(CollaboratorModel addCollaborator)
        {
            try
            {
                return await this.collaborator.AddCollaborator(addCollaborator);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCollaborator(CollaboratorModel deleteCollaborator)
        {
            try
            {
                return await this.collaborator.DeleteCollaborator(deleteCollaborator);
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
                return this.collaborator.GetAllCollaborator();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
