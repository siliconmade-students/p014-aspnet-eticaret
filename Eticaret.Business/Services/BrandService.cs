using AutoMapper;
using Eticaret.Business.Dtos;
using Eticaret.Data;

namespace Eticaret.Business.Services
{
    public class BrandService
    {
        private readonly EticaretDbContext _context;
        private readonly IMapper _mapper;

        public BrandService(EticaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BrandDto> GetAll()
        {
            var entities = _context.Brands.ToList();

            return _mapper.Map<List<BrandDto>>(entities);
        }
    }
}
