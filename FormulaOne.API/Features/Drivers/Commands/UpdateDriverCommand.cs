using AutoMapper;
using FormulaOne.API.Dtos.Requests;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities;
using MediatR;

namespace FormulaOne.API.Features.Drivers.Commands
{
    public class UpdateDriverCommand : IRequest<bool>
    {
        public UpdateDriverRequest UpdateDriverRequest { get; }

        public UpdateDriverCommand(UpdateDriverRequest updateDriverRequest)
        {
            UpdateDriverRequest = updateDriverRequest;
        }
    }

    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDriverCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = _mapper.Map<Driver>(request.UpdateDriverRequest);

            await _unitOfWork.Drivers.Update(driver);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
