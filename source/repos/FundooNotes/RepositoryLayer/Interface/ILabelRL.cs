using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        Task<LabelModel> AddLabel(LabelModel addLabel);
        Task<bool> DeleteLabel(LabelModel updateLabel);
        IEnumerable<LabelModel> GetAllLabel();
    }
}
