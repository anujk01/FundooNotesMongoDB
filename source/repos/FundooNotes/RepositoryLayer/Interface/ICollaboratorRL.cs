using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRL
    {
        Task<CollaboratorModel> AddCollaborator(CollaboratorModel addCollaborator);
        Task<bool> DeleteCollaborator(CollaboratorModel updateCollaborator);
        IEnumerable<CollaboratorModel> GetAllCollaborator();
    }
}
