using AutoMapper;
using FormulaOne.API.Dtos.Responses;
using FormulaOne.Data.Repositories.Interfaces;
using MediatR;

namespace FormulaOne.API.Features.Drivers.Queries
{
    public class GetAllDriversQuery : IRequest<IEnumerable<DriverResponse>>
    {
    }

    public class GetAllDriversQueryHandler : IRequestHandler<GetAllDriversQuery, IEnumerable<DriverResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDriversQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DriverResponse>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
        {
            var drivers = await _unitOfWork.Drivers.GetAll();

            var result = _mapper.Map<IEnumerable<DriverResponse>>(drivers);

            return result;
        }
    }
}
