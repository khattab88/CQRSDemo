using AutoMapper;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities;
using MediatR;

namespace FormulaOne.API.Features.Drivers.Commands
{
    public class DeleteDriverCommand : IRequest<bool>
    {
        public Guid DriverId { get; }

        public DeleteDriverCommand(Guid driverId)
        {
            DriverId = driverId;
        }
    }

    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDriverCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Drivers.Delete(request.DriverId);
            await _unitOfWork.CompleteAsync();

            return result;
        }
    }
}
