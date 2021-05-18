namespace Training.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly TrainingDbContext _context;

        public InitialHostDbBuilder(TrainingDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new DefaultCategoryCreator(_context).Create();
            new DefaultAuthorCreator(_context).Create();
            new DefaultPublisherCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
