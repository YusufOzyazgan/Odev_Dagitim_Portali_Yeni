using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odev_Dagitim_Portali.Dtos;
using Odev_Dagitim_Portali.Models;

namespace Odev_Dagitim_Portali.Controllers
{
    [Route("api/Class/[Action]")]
    [ApiController]
    //[Authorize]
    public class ClassController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        ResultDto result = new ResultDto();
        public ClassController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        [HttpGet]
        public List<ClassDto> GetList()
        {
            var Class = _context.Classes.ToList();
            var ClassDtos = _mapper.Map<List<ClassDto>>(Class);
            return ClassDtos;
        }

        [HttpGet]
        [Route("{id}")]

        public ClassDto GetById(int id)
        {
            var Class = _context.Classes.Where(s => s.Class_id == id).SingleOrDefault();
            var ClassDto = _mapper.Map<ClassDto>(Class);
            return ClassDto;

        }
        [HttpGet]
        [Route("{userId}")]
        public List<UserClassGetDto> GetUserClasses(string userId)
        {

            var userClasses = from uc in _context.UserClasses
                              join c in _context.Classes on uc.ClassId equals c.Class_id
                              join u in _context.Users on uc.UserId equals u.Id
                              where uc.UserId == userId
                              select new UserClassGetDto
                              {
                                  Id = uc.Id,
                                  ClassName = c.Class_name,
                                  ClassId = uc.ClassId,
                              };
            var listUserClasses = userClasses.ToList();
            return listUserClasses;
        }
        [HttpPost]
        //[Authorize(Roles = "Teacher,Admin")]
        public async Task<ResultDto> Add(ClassDto dto)
        {


            try
            {

                var Class = _mapper.Map<Class>(dto);

                _context.Classes.Add(Class);
                _context.SaveChanges();
                result.Status = true;
                result.Message = "Sınıf Eklendi.";
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        public async Task<ResultDto> AddUserClass(UserClassDto dto)
        {

            try
            {
                bool isExisting = _context.UserClasses
            .Any(uc => uc.UserId == dto.UserId && uc.ClassId == dto.ClassId);

                if (isExisting)
                {
                    result.Status = false;
                    result.Message = "Sınıf zaten ekli !";
                    return result;
                } 


                var UserClass = _mapper.Map<UserClass>(dto);

                _context.UserClasses.Add(UserClass);
                _context.SaveChanges();
                result.Status = true;
                result.Message = "Sınıf Eklendi.";
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return result;
           
        }

        [HttpPut]
        //[Authorize(Roles = "Teacher,Admin")]
        public ResultDto Edit(ClassDto dto)
        {
            var Class = _context.Classes.Where(s => s.Class_id == dto.Class_id).SingleOrDefault();
            if (Class == null)
            {
                result.Status = false;
                result.Message = "Sınıf Bulunamadı!";
                return result;
            }
            Class.Class_name= dto.Class_name;


            _context.Classes.Update(Class);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Sınıf Düzenlendi";
            return result;
        }

        [HttpDelete]
        [Route("{id}")]
        public ResultDto DeleteUserClass(int id)
        {
            var UserClass = _context.UserClasses.Where(s => s.Id == id ).SingleOrDefault();
            if (UserClass == null)
            {
                result.Status = false;
                result.Message = "Sınıf Bulunamadı!";
                return result;
            }
            _context.UserClasses.Remove(UserClass);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Sınıf Silindi";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        //[Authorize(Roles = "Teacher,Admin")]
        public ResultDto Delete(int id)
        {
            var Class = _context.Classes.Where(s => s.Class_id == id).SingleOrDefault();
            if (Class == null)
            {
                result.Status = false;
                result.Message = "Sınıf Bulunamadı!";
                return result;
            }
            _context.Classes.Remove(Class);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Sınıf Silindi";
            return result;
        }


        
    }
}
