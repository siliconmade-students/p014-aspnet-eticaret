using AutoMapper;
using Eticaret.Data;
using Eticaret.Data.Entity;

namespace Eticaret.Business.Services
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAll();
        CategoryDto GetById(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly EticaretDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(EticaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CategoryDto> GetAll()
        {
            var entities = _context.Categories.ToList();

            return _mapper.Map<List<CategoryDto>>(entities);
        }

        public CategoryDto GetById(int id)
        {
            var entity = _context.Categories.FirstOrDefault(e => e.Id == id);

            return _mapper.Map<CategoryDto>(entity);
        }
    }
}
