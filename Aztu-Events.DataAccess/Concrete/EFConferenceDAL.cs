using Aztu_Events.Core.Helper.EmailHelper;
using Aztu_Events.Core.Helper.FileHelper;
using Aztu_Events.Core.Helper.PageHelper;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.DataAccess.Abstarct;
using Aztu_Events.DataAccess.Concrete.SQLServer;
using Aztu_Events.Entities.Concrete;
using Aztu_Events.Entities.DTOs.Conferences;
using Aztu_Events.Entities.EnumClass;
using Microsoft.EntityFrameworkCore;

namespace Aztu_Events.DataAccess.Concrete
{
    public class EFConferenceDAL : IConfrenceDal
    {
        private readonly AppDbContext _context;
        private readonly IEmailHelper _emailHelper;

        public EFConferenceDAL(AppDbContext context, IEmailHelper emailHelper)
        {
            _context = context;
            _emailHelper = emailHelper;
        }

        public async Task<IResult> ApproveConfransAsync(Guid id, ConferanceStatus status, string ResponseMessage = null)
        {
            try
            {
                Confrans confrans = await _context.Confrans.Include(x=>x.Time).Include(x=>x.SpecialGuests).FirstOrDefaultAsync(x => x.Id == id);
                if (confrans == null) return new ErrorResult(message: "Data is NotFound");
                confrans.Status = status;
                _context.Confrans.Update(confrans);
                if (confrans.Status == ConferanceStatus.İmtina)
                {
                    await _emailHelper.DeclineConfransEmailAsync(userEmail: confrans.User.Email, name: confrans.User.FirstName + " " + confrans.User.LastName, responseMessage: ResponseMessage);
                }
                await _context.SaveChangesAsync();

                if (confrans.SpecialGuests != null && confrans.SpecialGuests.Count > 0 && confrans.Status == ConferanceStatus.Təsdiq)
                {
                    for (int i = 0; i < confrans.SpecialGuests.Count; i++)
                    {
                        if (confrans.Time.UpdateTime)
                        {

                            var emailResult = await _emailHelper.ApproveConfransSendEmailForGuest(userEmail: confrans.SpecialGuests[i].Email, name: confrans.SpecialGuests[i].Name, dateTime: DateTime.Parse(confrans.Time.Date.ToString() + confrans.Time.StartedTime.ToString()), AuditoriumNumber: confrans.Audutorium.AudutoriyaNumber, confransDetailUrl: $"https://localhost:7237/confranceDetail?id={confrans.Id}",UpdateDate:confrans.Time.UpdateTime,SendEmailGuest: confrans.SpecialGuests[i].SendEmail);

                            confrans.SpecialGuests[i].SendEmail = true;
                        }
                        else if (!confrans.SpecialGuests[i].SendEmail)
                        {
                            var emailResult = await _emailHelper.ApproveConfransSendEmailForGuest(userEmail: confrans.SpecialGuests[i].Email, name: confrans.SpecialGuests[i].Name, dateTime: DateTime.Parse(confrans.Time.Date.ToString() + confrans.Time.StartedTime.ToString()), AuditoriumNumber: confrans.Audutorium.AudutoriyaNumber, confransDetailUrl: $"https://localhost:7237/confranceDetail?id={confrans.Id}", UpdateDate:false, SendEmailGuest: confrans.SpecialGuests[i].SendEmail);

                            confrans.SpecialGuests[i].SendEmail = true;
                        }
                    }
                }

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult(message: ex.Message);
            }

        }

        public async Task<IDataResult<ConferenceGetAdminDTO>> ConferenceGetDetailForAdminAsync(Guid id, string lang)
        {
            try
            {
                var dto = await _context.Confrans
                    .Include(x=>x.User)
                    .Include(x=>x.ConfranceLaunguages)
                    .Include(x=>x.Audutorium)
                    .Include(x => x.Time)
                    .Include(x=>x.SpecialGuests)
                    .FirstOrDefaultAsync(x => x.Id == id);
                List<GETConfranceSpecialGuestDTO> gETConfranceSpecialGuestDTO = new List<GETConfranceSpecialGuestDTO>();
                foreach (var guest in dto.SpecialGuests)
                {
                    gETConfranceSpecialGuestDTO.Add(

                        new GETConfranceSpecialGuestDTO
                        {
                            Id = guest.Id,
                            Name = guest.Name,
                            Email = guest.Email,
                            SendEmail=guest.SendEmail
                        }

                        );
                }
            


                if (dto == null) return new ErrorDataResult<ConferenceGetAdminDTO>(message: "Data is NotFound");

                return new SuccessDataResult<ConferenceGetAdminDTO>(data:
                    
                    new ConferenceGetAdminDTO
                    {
                        StartedDate = dto.Time.StartedTime,
                        Status =dto.Status,
                        EndDate = dto.Time.EndTime,
                        AudutoriumId = dto.AudutoriumId,
                        AudutoriumName = dto.Audutorium.AudutoriyaNumber,
                        ConferenceContent = dto.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == lang).ConfransContent,
                        ConferenceName = dto.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == lang).ConfransName,
                        Id = dto.Id,
                        ImgUrl = dto.ImgUrl,
                        UserEmail = dto.User.Email,
                        UserFullname = dto.User.FirstName + " " + dto.User.LastName,
                        specialGuests= gETConfranceSpecialGuestDTO,
                        Day=dto.Time.Date

                    }



                    );
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<ConferenceGetAdminDTO>(message: ex.Message);
            }

        }

        public async Task<IDataResult<PaginatedList<ConferenceGetAdminListDTO>>> ConferenceGetListFilterAsync(FilterConferenceDto filter, string lang)
        {
            var conferenceQueries = _context.Confrans.AsNoTracking().AsSplitQuery().AsQueryable();
            if (filter.Status is not null)
            {
                conferenceQueries = conferenceQueries.Where(x => x.Status == filter.Status);
            }
            if (filter.AuditoriumId is not null)
            {
                conferenceQueries = conferenceQueries.Where(x => x.AudutoriumId == filter.AuditoriumId);
            }

            conferenceQueries = conferenceQueries.OrderByDescending(x => x.Time.Date).ThenByDescending(x => x.Time.StartedTime);

            var dto = conferenceQueries.Select(x => new ConferenceGetAdminListDTO()
            {
                StartedDate = x.Time.StartedTime,
                Status = x.Status,
                EndDate = x.Time.EndTime,
                Day = x.Time.Date,
                AudutoriumId = x.AudutoriumId,
                AudutoriumName = x.Audutorium.AudutoriyaNumber,
           
                ConferenceName = x.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == lang).ConfransName,
                Id = x.Id,
                ImgUrl = x.ImgUrl,

                UserEmail = x.User.Email,
                UserFullname = x.User.FirstName + " " + x.User.LastName
            });

            var list = await PaginatedList<ConferenceGetAdminListDTO>.CreateAsync(dto, filter.Page, filter.PageSize);

            return new SuccessDataResult<PaginatedList<ConferenceGetAdminListDTO>>(data: list);

        }

        public async Task<IResult> ConfrenceAddAsync(ConferenceAddDTO dto)
        {
            try
            {
                var checekAuditorium = _context.Audutoria.Include(x => x.Times).FirstOrDefault(x => x.Id == dto.AudutoriumId);
                if (checekAuditorium is null) return new ErrorResult(message: "Auditorium is NotFound!");
                if (_context.Times.Any(x => (x.StartedTime >= dto.StartedDate || dto.EndDate <= x.EndTime) && dto.Day == x.Date && x.AuditoriumId==checekAuditorium.Id)) return new ErrorResult(message: "Time Is Not Empty!");

                Confrans confrans = new()
                {
                    AudutoriumId = dto.AudutoriumId,
                  
                    ImgUrl = dto.ImgUrl,
                    UserId = dto.UserId,
                    Status = ConferanceStatus.Gözlənilir
                };
                _context.Confrans.Add(confrans);

                Time time = new Time()
                {
                    AuditoriumId = dto.AudutoriumId,
                    ConfransId = confrans.Id,
                    Date = dto.Day,
                    StartedTime = dto.StartedDate,
                    EndTime = dto.EndDate,
                };
                await _context.Times.AddAsync(time);
                confrans.TimeId = time.Id;


                for (int i = 0; i < dto.LangCode.Count; i++)
                {
                    ConfranceLaunguage confransLanguage = new()
                    {ConfransId=confrans.Id,
                        LangCode = dto.LangCode[i],
                        ConfransContent = dto.ConferenceContent[i],
                        ConfransName = dto.ConferenceName[i]
                    };
                   _context.ConfranceLaunguages.Add(confransLanguage);
                }

                for (int i = 0; i < dto.specialGuestsEmail.Count; i++)
                {
                    SpecialGuest specialGuest = new SpecialGuest()
                    {
                        ConfransId = confrans.Id,
                        Email = dto.specialGuestsEmail[i],
                        Name = dto.specialGuestsName[i],
                        SendEmail = false
                    };
                   await  _context.SpecialGuests.AddAsync(specialGuest);
                }




                await _context.SaveChangesAsync();
                return new Result(true, "Confrans əlave olundu");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public IResult ConfrenceRemove(string id)
        {
            try
            {
                var Conferance=_context.Confrans.FirstOrDefault(x=>x.Id.ToString()==id);
                if (Conferance is  null) return new ErrorResult(message: "Data Is Not Found!");
                var guest = _context.SpecialGuests.Where(x => x.ConfransId == Conferance.Id);
                _context.SpecialGuests.RemoveRange(guest);
                var launguage = _context.ConfranceLaunguages.Where(x => x.ConfransId == Conferance.Id);
                _context.ConfranceLaunguages.RemoveRange(launguage);
                FileHelper.RemoveFile(Conferance.ImgUrl);
                _context.Confrans.Remove(Conferance);
                var time = _context.Times.FirstOrDefault(x => x.ConfransId == Conferance.Id);
                _context.Times.Remove(time);

                _context.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public async Task<IResult> ConfrenceUpdateAsync(ConferenceUpdateDto dto)
        {
            try
            {


                var confrans = await _context.Confrans
                    .Include(x => x.ConfranceLaunguages)
                    .Include(x=>x.Time)
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (dto.ConferenceContent is not null)
                {
                    for (int i = 0; i < dto.ConferenceName.Count; i++)
                    {
                      
                        confrans.ConfranceLaunguages[i].ConfransContent = dto.ConferenceContent[i];
                        confrans.ConfranceLaunguages[i].ConfransName = dto.ConferenceName[i];
                    }
                }

                confrans.Time.Date = dto.Day == default ? confrans.Time.Date:dto.Day;
                confrans.Time.StartedTime = dto.StartedDate == default ? confrans.Time.StartedTime : dto.StartedDate; 
                confrans.Time.EndTime = dto.EndDate == default ? confrans.Time.EndTime : dto.EndDate;
            
                if (dto.specialGuestsEmail is not null)
                {

              for (int i = 0;i < dto.specialGuestsEmail.Count; i++)
                {
                    SpecialGuest specialGuest = new SpecialGuest()
                    {
                        ConfransId=confrans.Id,
                        Email = dto.specialGuestsEmail[i],
                        Name = dto.specialGuestsName[i],
                        SendEmail=false
                    };
                 await   _context.SpecialGuests.AddAsync(specialGuest);
                }
                }
                    await _context.SaveChangesAsync();
                confrans.AudutoriumId = dto.AudutoriumId;

                if (confrans.ImgUrl != dto.ImgUrl)
                {
                    FileHelper.RemoveFile(confrans.ImgUrl);
                    confrans.ImgUrl = dto.ImgUrl;
                }
                confrans.Status = ConferanceStatus.Gözlənilir;

                _context.Confrans.Update(confrans);
                await _context.SaveChangesAsync();
                return new SuccessResult("Yenilendi");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public IDataResult<List<ConferenceGetAdminListDTO>> GetAllConferanceForAdmin(string LangCode)
        {
            try
            {
                var data = _context.Confrans
                    .Include(x=>x.User)
                    .Include(x => x.ConfranceLaunguages)
                    .Include(x => x.Audutorium)
                    .Include(x => x.SpecialGuests)
                    .Include(y => y.Time);
                return new SuccessDataResult<List<ConferenceGetAdminListDTO>>(data:

                    data.Select(x => new ConferenceGetAdminListDTO
                    {
                        AudutoriumId = x.AudutoriumId,
                        AudutoriumName = x.Audutorium.AudutoriyaNumber,
                        ConferenceName = x.ConfranceLaunguages.FirstOrDefault(y => y.LangCode == LangCode).ConfransName,
                        Day = x.Time.Date,
                        EndDate = x.Time.EndTime,
                        Id = x.Id,
                        ImgUrl = x.ImgUrl,
                        StartedDate = x.Time.StartedTime,
                        Status = x.Status,
                        UserEmail = x.User.Email,
                        UserFullname = x.User.FirstName + " " + x.User.LastName,
                        UserId = Guid.Parse(x.UserId)



                    }).ToList());
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<ConferenceGetAdminListDTO>>(message: ex.Message);
            }
        }

        public IDataResult<List<GetALLConferenceUserDTO>> GetAllConferanceForUser(string UserId, string LangCode)
        {
            try
            {
                var data = _context.Confrans
                    .Include(x => x.User)
                    .Include(x=>x.ConfranceLaunguages)
                    .Include(x => x.Audutorium)
                    .Include(x => x.Time)
                    .Include(x => x.SpecialGuests)
                    .Where(x => x.UserId == UserId);
                return new SuccessDataResult<List<GetALLConferenceUserDTO>>(data: data.Select(x => new GetALLConferenceUserDTO
                {
                    Id = x.Id,
                    ImgUrl = x.ImgUrl,
                    ConferenceName = x.ConfranceLaunguages.FirstOrDefault(y => y.LangCode == LangCode).ConfransName,
                    AudutoriumId = x.AudutoriumId,
                    AudutoriumName = x.Audutorium.AudutoriyaNumber,
                    Status = x.Status,
                    UserEmail = x.User.Email,
                    UserFullname = x.User.FirstName + " " + x.User.LastName,
                    UserId = x.Id,
                    Day = x.Time.Date,
                    EndDate = x.Time.EndTime,
                    StartedDate = x.Time.StartedTime
                }).ToList());

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<GetALLConferenceUserDTO>>(message: ex.Message);
            }
        }

        public IDataResult<GetConferenceUserDTO> GetConferanceDetailForUser(string UserId, string ConfranceId, string LangCode)
        {
            try
            {
           var data = _context.Confrans
                    .Include(x => x.User)
                    .Include(x => x.Audutorium)
                    .Include(x => x.Time)
                    .Include(x=>x.ConfranceLaunguages)
                    .Include(x => x.SpecialGuests)                 
                    .FirstOrDefault(x => x.UserId == UserId &&x.Id.ToString()==ConfranceId);
                if (data is null)
                    return new SuccessDataResult<GetConferenceUserDTO>(data: null);

                List<GETConfranceSpecialGuestDTO> gETConfranceSpecialGuestDTOs = new List<GETConfranceSpecialGuestDTO>();
                foreach (var guest in data?.SpecialGuests)
                {
                    gETConfranceSpecialGuestDTOs.Add(new GETConfranceSpecialGuestDTO
                    {
                        Email = guest.Email,
                        Name = guest.Name,
                        Id = guest.Id,
                        SendEmail=guest.SendEmail

                    });
                }



                GetConferenceUserDTO getConferenceUserDTO=new GetConferenceUserDTO()
                {
                    AudutoriumId=data.AudutoriumId,
                    AudutoriumName=data.Audutorium.AudutoriyaNumber,
                    ConferenceContent=data.ConfranceLaunguages.FirstOrDefault(x=>x.LangCode==LangCode).ConfransContent,
                    ConferenceName=data.ConfranceLaunguages.FirstOrDefault(x=>x.LangCode==LangCode).ConfransName,
                    Day=data.Time.Date,
                    EndDate=data.Time.EndTime,
                    StartedDate=data.Time.StartedTime,
                    Id=data.Id,
                    ImgUrl=data.ImgUrl,
                    specialGuests= gETConfranceSpecialGuestDTOs,
                    Status=data.Status,
                    UserEmail=data.User.Email,
                    UserFullname=data.User.FirstName+" "+data.User.LastName,


                };
                return new SuccessDataResult<GetConferenceUserDTO>(data: getConferenceUserDTO);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetConferenceUserDTO>(message: ex.Message);
            }
        }

        public IDataResult<ConferenceUpdateDto> GetConferenceForUpdateUser(string UserId, string ConferenceId)
        {
            try
            {
                var data = _context.Confrans
                    .Include(x => x.User)
                    .Include(x => x.Audutorium)
                    .Include(x => x.Time)
                    .Include(x => x.ConfranceLaunguages)
                    .Include(x => x.SpecialGuests)
                    .FirstOrDefault(x => x.UserId == UserId && x.Id.ToString() == ConferenceId);
                if (data is null)
                    return new ErrorDataResult<ConferenceUpdateDto>(message: "Data Is NotFound!");
                return new SuccessDataResult<ConferenceUpdateDto>(data: new ConferenceUpdateDto
                {
                    AudutoriumId = data.AudutoriumId,
                    ConferenceContent = data.ConfranceLaunguages.Select(x => x.ConfransContent).ToList(),
                    ConferenceName = data.ConfranceLaunguages.Select(x => x.ConfransName).ToList(),
                    Day = data.Time.Date,
                    EndDate = data.Time.EndTime,
                    ImgUrl = data.ImgUrl,
                    LangCode = data.ConfranceLaunguages.Select(x => x.LangCode).ToList(),
                    specialGuestsEmail = data.SpecialGuests.Select(x => x.Email).ToList(),
                    specialGuestsName = data.SpecialGuests.Select(x => x.Name).ToList(),
                    Id = data.Id,
                    Status = data.Status,
                    StartedDate = data.Time.StartedTime
                });
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<ConferenceUpdateDto>(message: ex.Message);
            }
        }
    }
}
