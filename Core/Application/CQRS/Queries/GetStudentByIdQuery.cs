using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Queries
{
    public class GetStudentByIdQuery : IRequest<Student>
    {
        public int Id { get; set; }
        public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Student>
        {
            private readonly IAppDbContext _context;

            public GetStudentByIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
            {
                var student = await _context.Students.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (student == null)
                    return null;
                return student;
            }
        }
    }
}
