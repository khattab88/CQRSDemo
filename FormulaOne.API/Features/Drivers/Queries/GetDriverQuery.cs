using AutoMapper;
using FormulaOne.API.Dtos.Responses;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities;
using MediatR;

namespace FormulaOne.API.Features.Drivers.Queries
{
    public class GetDriverQuery : IRequest<DriverResponse>
    {
        public Guid DriverId { get; }

        public GetDriverQuery(Guid driverId)
        {
            DriverId = driverId;
        }
    }

    public class GetDriverQueryHandler : IRequestHandler<GetDriverQuery, DriverResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDriverQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DriverResponse> Handle(GetDriverQuery request, CancellationToken cancellationToken)
        {
            var driver = await _unitOfWork.Drivers.GetById(request.DriverId);

            if (driver is null)
                return null;

            var result = _mapper.Map<DriverResponse>(driver);

            return result;
        }
    }
}
