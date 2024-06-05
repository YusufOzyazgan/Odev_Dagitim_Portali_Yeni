using AutoMapper;
using Odev_Dagitim_Portali.Dtos;
using Odev_Dagitim_Portali.Models;

namespace Odev_Dagitim_Portali.Mapping
{
    public class MapProfile: Profile
    {

        public MapProfile()
        {
            CreateMap<Homework, HomeworkDto>().ReverseMap();
            CreateMap<HomeworkSubmission, HomeworkSubmissionDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Class,ClassDto>().ReverseMap();
            CreateMap<UserClass, UserClassDto>().ReverseMap();

        }
    }
 
}
