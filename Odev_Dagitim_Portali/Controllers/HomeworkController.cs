using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Odev_Dagitim_Portali.Dtos;
using Odev_Dagitim_Portali.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace Odev_Dagitim_Portali.Controllers
{
    [Authorize] 
    [Route("api/Homeworks/[Action]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        ResultDto result = new ResultDto();
        public HomeworkController(AppDbContext context, IMapper mapper,UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        
        [HttpGet]
        public List<HomeworkDto> GetList()
        {
            var homeworks = _context.Homeworks.ToList();
            var homeworkDtos = _mapper.Map<List<HomeworkDto>>(homeworks);
            return homeworkDtos;
        }
        [HttpGet]
        [Route("{id}")]
        
        public HomeworkDto GetById(int id)
        {
            var homework = _context.Homeworks.Where(s => s.Homework_id == id).SingleOrDefault();
            var homeworkDto = _mapper.Map<HomeworkDto>(homework);
            return homeworkDto;
        }


        [HttpGet]
        public List<HomeworkDto> GetHomeworksByTeacher(string id)
        {
            var homeworks = _context.Homeworks.Where(s => s.User_id == id).ToList();
            var homeworkDtos = _mapper.Map<List<HomeworkDto>>(homeworks);
            return homeworkDtos;
        }

        [HttpGet]
        public List<HomeworkDto> GetHomeworksByStudent(string id)
        {
            // UserId'ye ait tüm sınıfları alıyoruz
            var Classes = _context.UserClasses.Where(s => s.UserId == id).ToList();
            var ClassIds = Classes.Select(c => c.ClassId).ToList();

            List<Homework> allHomeworks = new List<Homework>();

            foreach (var classId in ClassIds)
            {
                // Her bir sınıf id'ye ait tüm dersleri alıyoruz
                var lessons = _context.Lessons.Where(l => l.Class_id == classId).ToList();
                var lessonIds = lessons.Select(l => l.Lesson_id).ToList();

                foreach (var lessonId in lessonIds)
                {
                    // Her bir ders id'ye ait tüm ödevleri alıyoruz
                    var homeworks = _context.Homeworks.Where(h => h.Lesson_id == lessonId).ToList();
                    allHomeworks.AddRange(homeworks);
                }
            }

            // Tüm ödevleri DTO'lara dönüştürüyoruz
            var homeworkDtos = _mapper.Map<List<HomeworkDto>>(allHomeworks);
            return homeworkDtos;

        }
        [HttpGet]
        [Route("{id}")]
        public List<HomeworkSubmissionDto> GetHomeworkSubmissions(int id)
        {
            var homework_submissions = _context.HomeworkSubmissions.Where(s => s.Homework_id == id).ToList();
            var homework_submissionDtos = _mapper.Map<List<HomeworkSubmissionDto>>(homework_submissions);
            return homework_submissionDtos;
        }
        [HttpPost]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<ResultDto> Add(HomeworkDto dto)
        {
           
            
            if (dto == null)
            {
                result.Status = false;
                result.Message = "Değerleri Doldurun !!!";
                return result;
            }
            try
            {

                var homework = _mapper.Map<Homework>(dto);
                homework.Updated = DateTime.Now;
                homework.Created = DateTime.Now;
              
               

                _context.Homeworks.Add(homework);
                _context.SaveChanges();
                result.Status = true;
                result.Message = "Ödev Eklendi.";
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message ;
            }
            return result;
        }

        [HttpPut]
        [Authorize(Roles = "Teacher,Admin")]
        public ResultDto Edit(HomeworkDto dto)
        {
            var homework= _context.Homeworks.Where(s => s.Homework_id == dto.Homework_id).SingleOrDefault();
            if (homework == null)
            {
                result.Status = false;
                result.Message = "Ödev Bulunamadı!";
                return result;
            }
            homework.Homework_title = dto.Homework_title;
            homework.Homework_content = dto.Homework_content;
            homework.Homework_deadline = dto.Homework_deadline;
            homework.Updated = DateTime.Now;
            homework.Lesson_id = dto.Lesson_id;
            _context.Homeworks.Update(homework);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Ödev Düzenlendi";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Teacher,Admin")]
        public ResultDto Delete(int id)
        {
            ResultDto result = new ResultDto();

            try
            {
                // Ödevi bulun
                var homework = _context.Homeworks.Where(s => s.Homework_id == id).SingleOrDefault();
                if (homework == null)
                {
                    result.Status = false;
                    result.Message = "Ödev Bulunamadı!";
                    return result;
                }

                // Önce HomeworkSubmissions tablosundaki ilgili kayıtları sil
                var submissions = _context.HomeworkSubmissions.Where(hs => hs.Homework_id == id);
                _context.HomeworkSubmissions.RemoveRange(submissions);

                // Daha sonra Homeworks tablosundaki kaydı sil
                _context.Homeworks.Remove(homework);
                _context.SaveChanges(); 

                result.Status = true;
                result.Message = "Ödev ve ilgili gönderimler başarıyla silindi.";
            }
            catch (Exception ex)
            {
                // Hata durumunda uygun bir hata mesajı döndür
                result.Status = false;
                result.Message = "Bir hata oluştu: " + ex.Message;
            }

            return result;
        }










    }
}
