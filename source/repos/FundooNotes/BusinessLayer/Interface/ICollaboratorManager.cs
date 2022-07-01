using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface ICollaboratorManager
    {
        Task<CollaboratorModel> AddCollaborator(CollaboratorModel addCollaborator);
        Task<bool> DeleteCollaborator(CollaboratorModel updateCollaborator);
        IEnumerable<CollaboratorModel> GetAllCollaborator();
    }
}
