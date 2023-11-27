using AutoMapper;
using FormulaOne.API.Dtos.Requests;
using FormulaOne.API.Dtos.Responses;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FormulaOne.API.Features.Drivers.Commands
{
    public class CreateDriverCommand : IRequest<DriverResponse>
    {
        public CreateDriverRequest CreateDriverRequest { get; }

        public CreateDriverCommand(CreateDriverRequest createDriverRequest)
        {
            CreateDriverRequest = createDriverRequest;
        }
    }

    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, DriverResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDriverCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DriverResponse> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = _mapper.Map<Driver>(request.CreateDriverRequest);

            await _unitOfWork.Drivers.Add(driver);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<DriverResponse>(driver);

            return result;
        }
    }

}
