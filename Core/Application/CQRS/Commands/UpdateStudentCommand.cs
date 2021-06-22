using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.CQRS.Commands
{
    public class UpdateStudentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Standard { get; set; }
        public int Rank { get; set; }
        public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, int>
        {
            private readonly IAppDbContext _context;

            public UpdateStudentCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
            {
                var student = _context.Students.Where(a => a.Id == request.Id).FirstOrDefault();

                if (student == null)
                    return default;

                student.Name = request.Name;
                student.Standard = request.Standard;
                student.Rank = request.Rank;

                await _context.SaveChangesAsync();
                return student.Id;
            }
        }
    }
}
