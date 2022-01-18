using System;
using apiv2.Repositories.ApprenticeRepository;
using apiv2.Repositories.DepartmentRepository;

namespace apiv2.UOF
{
    public interface IUnitOfWork: IDisposable
    {
        IApprenticeRepository Apprentices { get; }
        IDepartmentRepository Departments { get; }
        int Complete();
    }
}
