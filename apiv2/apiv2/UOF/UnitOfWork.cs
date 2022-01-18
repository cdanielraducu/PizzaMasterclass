using System;
using apiv2.Data;
using apiv2.Repositories.ApprenticeRepository;
using apiv2.Repositories.DepartmentRepository;

namespace apiv2.UOF
{
    public class UnitOfWork: IUnitOfWork
    {
        public readonly ProjectContext _context;
        public UnitOfWork(ProjectContext context): base()
        {
            _context = context;
            Apprentices = new ApprenticeRepository(_context);
            Departments = new DepartmentRepository(_context);
        }

        public IApprenticeRepository Apprentices { get; private set; }
        public IDepartmentRepository Departments { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}