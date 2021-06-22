using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.CQRS.Commands
{
    public class DeleteStudentByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Standard { get; set; }
        public int Rank { get; set; }
        public class DeleteStudentByIdCommandHandler : IRequestHandler<DeleteStudentByIdCommand, int>
        {
            private readonly IAppDbContext _context;

            public DeleteStudentByIdCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
            {
                var student = _context.Students.Where(a => a.Id == request.Id).FirstOrDefault();

                if (student == null)
                    return default;

                _context.Students.Remove(student);

                await _context.SaveChangesAsync();
                return student.Id;
            }
        }
    }
}
