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
using Aztu_Events.Entities.DTOs.AlertDTOs;
using Aztu_Events.Entities.DTOs.CommentDTOs;
using Aztu_Events.Entities.DTOs.Conferences;
using Aztu_Events.Entities.EnumClass;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aztu_Events.DataAccess.Concrete
{
    public class EFConferenceDAL : IConfrenceDal
    {
        private readonly AppDbContext _context;
        private readonly IEmailHelper _emailHelper;
        private readonly UserManager<User> _userManager;
        public EFConferenceDAL(AppDbContext context, IEmailHelper emailHelper, UserManager<User> userManager)
        {
            _context = context;
            _emailHelper = emailHelper;
            _userManager = userManager;
        }

  

        public async Task<IResult> ApproveConfransAsync(Guid id, ConferanceStatus status, string ResponseMessage = null, bool IsFeatured = false)
        {
            try
            {
                Confrans confrans = await _context.Confrans
                    .Include(x=>x.User)
                    .Include(x=>x.ConfranceLaunguages)
                    .Include(x => x.Audutorium)
                    .Include(x => x.Time)
                    .Include(x => x.SpecialGuests)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (confrans == null) return new ErrorResult(message: "Data is NotFound");
                confrans.Status = status;
                _context.Confrans.Update(confrans);
                if (confrans.Status == ConferanceStatus.İmtina)
                {
                    await _emailHelper.DeclineConfransEmailAsync(userEmail: confrans.User.Email, name: confrans.User.FirstName + " " + confrans.User.LastName, responseMessage: ResponseMessage);
                }


                if (confrans.SpecialGuests != null && confrans.SpecialGuests.Count > 0 && confrans.Status == ConferanceStatus.Təsdiq)
                {
                    for (int i = 0; i < confrans.SpecialGuests.Count; i++)
                    {
                        if (confrans.Time.UpdateTime)
                        {

                            var emailResult = await _emailHelper.ApproveConfransSendEmailForGuest(userEmail: confrans.SpecialGuests[i].Email, name: confrans.SpecialGuests[i].Name, dateTime: new DateTime(confrans.Time.Date, confrans.Time.StartedTime).ToString("yyyy-MM-dd HH:mm"), AuditoriumNumber: confrans.Audutorium.AudutoriyaNumber, confransDetailUrl: $"https://localhost:7233/ConferenceDetail/index/{confrans.Id}", UpdateDate: confrans.Time.UpdateTime, SendEmailGuest: confrans.SpecialGuests[i].SendEmail);

                            confrans.SpecialGuests[i].SendEmail = true;
                        }
                        else if (!confrans.SpecialGuests[i].SendEmail)
                        {
                            var emailResult = await _emailHelper.ApproveConfransSendEmailForGuest(userEmail: confrans.SpecialGuests[i].Email, name: confrans.SpecialGuests[i].Name, dateTime: new DateTime(confrans.Time.Date, confrans.Time.StartedTime).ToString("yyyy-MM-dd HH:mm"), AuditoriumNumber: confrans.Audutorium.AudutoriyaNumber, confransDetailUrl: $"https://localhost:7233/ConferenceDetail/index/{confrans.Id}", UpdateDate: false, SendEmailGuest: confrans.SpecialGuests[i].SendEmail);

                            confrans.SpecialGuests[i].SendEmail = true;
                        }
                    }
                    confrans.Time.UpdateTime = false;
                }
                confrans.IsFeatured = IsFeatured;

                _context.Confrans.Update(confrans);
             Alert alert = new Alert()
             {
                 ConferenceId=confrans.Id.ToString(),
                 ForUser=true,
                 UserId=confrans.UserId.ToString(),
                 
                 
             };
               _context.Alerts.Add(alert);
             
                    AlertLaunguage alertLaunguageAz = new AlertLaunguage()
                    {
                        AlertId=alert.Id,
                        LangCode="az",
                        Content=$"{confrans.ConfranceLaunguages.FirstOrDefault(x=>x.LangCode=="az").ConfransName} Adlı Tədbirinizin statusu dəyişdirildi!"
                    };
               await _context.AlertLaunguages.AddAsync(alertLaunguageAz);
                AlertLaunguage alertLaunguageRu = new AlertLaunguage()
                {
                    AlertId = alert.Id,
                    LangCode = "ru",
                    Content = $"{confrans.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == "ru").ConfransName} Вашего мероприятия изменен!"
                };
                await _context.AlertLaunguages.AddAsync(alertLaunguageRu);
                AlertLaunguage alertLaunguageEn = new AlertLaunguage()
                {
                    AlertId = alert.Id,
                    LangCode = "en",
                    Content = $"{confrans.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == "en").ConfransName} The status of your event has been changed!"
                };
                await _context.AlertLaunguages.AddAsync(alertLaunguageEn);

                await _context.SaveChangesAsync();
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
                    .Include(x => x.User)
                    .Include(x => x.ConfranceLaunguages)
                    .Include(x => x.Audutorium)
                    .Include(x => x.Time)
                    .Include(x => x.SpecialGuests)
                    .Include(x => x.Category)
                    .ThenInclude(x => x.CategoryLaunguages)
                    .Include(x=>x.userConfrances)
                    .ThenInclude(x=>x.User)
                    .FirstOrDefaultAsync(x => x.Id == id);
                
                List<GetConferenceUserRegistrationDTO> getConferenceUserRegistrationDTOs = new List<GetConferenceUserRegistrationDTO>();
                foreach (var userConfrance in dto.userConfrances)
                {
                    var roles = await _userManager.GetRolesAsync(userConfrance.User);
                    getConferenceUserRegistrationDTOs.Add(new GetConferenceUserRegistrationDTO
                    {
                        Email = userConfrance.User.Email,
                        FirstName = userConfrance.User.FirstName,
                        LastName = userConfrance.User.LastName,
                        PhoneNumber = userConfrance.User.PhoneNumber,
                        Position = roles.ToList(),
                        UserId=userConfrance.User.Id
                        
                    });
                }


                List<GETConfranceSpecialGuestDTO> gETConfranceSpecialGuestDTO = new List<GETConfranceSpecialGuestDTO>();
                foreach (var guest in dto.SpecialGuests)
                {
                    gETConfranceSpecialGuestDTO.Add(

                        new GETConfranceSpecialGuestDTO
                        {
                            Id = guest.Id,
                            Name = guest.Name,
                            Email = guest.Email,
                            SendEmail = guest.SendEmail
                        }

                        );
                }



                if (dto == null) return new ErrorDataResult<ConferenceGetAdminDTO>(message: "Data is NotFound");

                return new SuccessDataResult<ConferenceGetAdminDTO>(data:

                    new ConferenceGetAdminDTO
                    {
                        StartedDate = dto.Time.StartedTime,
                        Status = dto.Status,
                        EndDate = dto.Time.EndTime,
                        AudutoriumId = dto.AudutoriumId,
                        AudutoriumName = dto.Audutorium.AudutoriyaNumber,
                        ConferenceContent = dto.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == lang).ConfransContent,
                        ConferenceName = dto.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == lang).ConfransName,
                        Id = dto.Id,
                        ImgUrl = dto.ImgUrl,
                        UserEmail = dto.User.Email,
                        UserFullname = dto.User.FirstName + " " + dto.User.LastName,
                        specialGuests = gETConfranceSpecialGuestDTO,
                        Day = dto.Time.Date,
                        CategoryId = dto.CategoryId.ToString(),
                        CategoryName = dto.Category.CategoryLaunguages.FirstOrDefault(x => x.LangCode == lang).CategoryName,
                        IsFeatured = dto.IsFeatured,
                        RegistrationUser= getConferenceUserRegistrationDTOs

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
                var currentDate = DateOnly.FromDateTime(DateTime.Now);
                var currentTime = TimeOnly.FromDateTime(DateTime.Now);

                conferenceQueries = conferenceQueries.Where(x => x.Status == filter.Status/* && (x.Time.Date > currentDate || (x.Time.Date == currentDate && x.Time.StartedTime > currentTime))*/);
            }
            if (filter.CategoryId is not null)
            {
                conferenceQueries = conferenceQueries.Where(x => x.CategoryId == filter.CategoryId);
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
                CategoryId = x.CategoryId.ToString(),
                CategoryName = x.Category.CategoryLaunguages.FirstOrDefault(y => y.LangCode == lang).CategoryName,
                ConferenceContent = x.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == lang).ConfransContent,
                ConferenceName = x.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == lang).ConfransName,
                Id = x.Id,
                ImgUrl = x.ImgUrl,
                IsFeatured = x.IsFeatured,
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
                var checekAuditorium = _context.Audutoriums
                    .Include(x => x.Times)
                    
                    .FirstOrDefault(x => x.Id == dto.AudutoriumId);
                if (checekAuditorium is null) return new ErrorResult(message: "Auditorium is NotFound!");
                if (_context.Times.Any(x => (x.StartedTime >= dto.StartedDate || dto.EndDate <= x.EndTime) && dto.Day == x.Date && x.AuditoriumId == checekAuditorium.Id)) return new ErrorResult(message: "Time Is Not Empty!");
                var checekedCategory = _context.Categories.FirstOrDefault(x => x.Id.ToString() == dto.CategoryId);
                Confrans confrans = new()
                {
                    AudutoriumId = dto.AudutoriumId.Value,
                    CategoryId = checekedCategory.Id,
                    ImgUrl = dto.ImgUrl,
                    UserId = dto.UserId,
                    Status = ConferanceStatus.Gözlənilir,

                };
                _context.Confrans.Add(confrans);

                Time time = new Time()
                {
                    AuditoriumId = dto.AudutoriumId.Value,
                    ConfransId = confrans.Id,
                    Date = dto.Day.Value,
                    StartedTime = dto.StartedDate.Value,
                    EndTime = dto.EndDate.Value,
                };
                await _context.Times.AddAsync(time);
                confrans.TimeId = time.Id;


                for (int i = 0; i < dto.LangCode.Count; i++)
                {
                    ConfranceLaunguage confransLanguage = new()
                    {
                        ConfransId = confrans.Id,
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
                    await _context.SpecialGuests.AddAsync(specialGuest);
                }

                await _context.SaveChangesAsync();
                var getConfrans=_context.Confrans
                    .Include(x=>x.ConfranceLaunguages)
                    .Include(x=>x.User)
                    .FirstOrDefault(x=>x.Id==confrans.Id);
                Alert alert = new Alert()
                {
                    ConferenceId = getConfrans.Id.ToString(),
                    ForUser = false,
                };
                _context.Alerts.Add(alert);

                AlertLaunguage alertLaunguageAz = new AlertLaunguage()
                {
                    AlertId = alert.Id,
                    LangCode = "az",
                    Content = $"Təşkilatçı {getConfrans.User.FirstName}  {getConfrans.User.LastName} tərəfindən {getConfrans.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == "az").ConfransName} Adlı Tədbir yaradıldı.Ətraflı kilikləyin."
                };
                await _context.AlertLaunguages.AddAsync(alertLaunguageAz);
                AlertLaunguage alertLaunguageRu = new AlertLaunguage()
                {
                    AlertId = alert.Id,
                    LangCode = "ru",
                    Content = $"Мероприятие под названием {getConfrans.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == "ru").ConfransName} создано организатором {getConfrans.User.FirstName} {getConfrans.User.LastName}. Для получения подробной информации нажмите здесь."
                };

                await _context.AlertLaunguages.AddAsync(alertLaunguageRu);
                AlertLaunguage alertLaunguageEn = new AlertLaunguage()
                {
                    AlertId = alert.Id,
                    LangCode = "en",
                    Content = $"The event named {getConfrans.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == "en").ConfransName} has been created by the organizer {getConfrans.User.FirstName} {getConfrans.User.LastName}. Click for more details."
                };

          
                await _context.AlertLaunguages.AddAsync(alertLaunguageEn);



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
                var Conferance = _context.Confrans.FirstOrDefault(x => x.Id.ToString() == id);
                if (Conferance is null) return new ErrorResult(message: "Data Is Not Found!");
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
                if (!_context.Categories.Any(x => x.Id.ToString() == dto.CategoryId))
                {
                    return new ErrorResult(message: "Category Is NotFound!");
                }

                var confrans = await _context.Confrans
                    .Include(x => x.ConfranceLaunguages)
                    .Include(x => x.Audutorium)
                    .Include(x => x.SpecialGuests)
                    .Include(x=>x.User)
                    .Include(x => x.Time)
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (dto.ConferenceContent is not null)
                {
                    for (int i = 0; i < dto.ConferenceName.Count; i++)
                    {

                        confrans.ConfranceLaunguages[i].ConfransContent = dto.ConferenceContent[i];
                        confrans.ConfranceLaunguages[i].ConfransName = dto.ConferenceName[i];
                    }
                }
                if (confrans.Time.Date != dto.Day && dto.Day != default)
                {
                    confrans.Time.Date = dto.Day;
                    confrans.Time.UpdateTime = true;

                }
                if (confrans.Time.StartedTime != dto.StartedDate && dto.StartedDate != default)
                {
                    confrans.Time.StartedTime = dto.StartedDate;
                    confrans.Time.UpdateTime = true;
                }
                if (confrans.Time.EndTime != dto.EndDate && dto.EndDate != default)
                {
                    confrans.Time.EndTime = dto.EndDate;
                    confrans.Time.UpdateTime = true;
                }
                _context.Times.Update(confrans.Time);


                if (dto.specialGuestsEmail is not null)
                {

                    for (int i = 0; i < dto.specialGuestsEmail.Count; i++)
                    {
                        SpecialGuest specialGuest = new SpecialGuest()
                        {
                            ConfransId = confrans.Id,
                            Email = dto.specialGuestsEmail[i],
                            Name = dto.specialGuestsName[i],
                            SendEmail = false
                        };
                        await _context.SpecialGuests.AddAsync(specialGuest);
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
                confrans.CategoryId = Guid.Parse(dto.CategoryId);
                _context.Confrans.Update(confrans);
                Alert alert = new Alert()
                {
                    ForUser = false,
                    ConferenceId=confrans.Id.ToString(),
                                    
                };
                _context.Alerts.Add(alert);
                AlertLaunguage alertLaunguageAz = new AlertLaunguage()
                {AlertId=alert.Id,
                LangCode="az",
                Content=$"{confrans.User.FirstName} {confrans.User.LastName} tətərfindən {confrans.ConfranceLaunguages.FirstOrDefault(x=>x.LangCode=="az").ConfransName} adlı tədbirdə düzəlişlər edildi",
                

                };
                await _context.AlertLaunguages.AddAsync(alertLaunguageAz);
                AlertLaunguage alertLaunguageRu = new AlertLaunguage()
                {
                    AlertId = alert.Id,
                    LangCode = "ru",
                    Content = $"{confrans.User.FirstName} {confrans.User.LastName} изменил мероприятие под названием {confrans.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == "ru").ConfransName}"
                };
                await _context.AlertLaunguages.AddAsync(alertLaunguageRu);
                AlertLaunguage alertLaunguageEn = new AlertLaunguage()
                {
                    AlertId = alert.Id,
                    LangCode = "en",
                    Content = $"{confrans.User.FirstName} {confrans.User.LastName} made changes to the event named {confrans.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == "en").ConfransName}"
                };
                await _context.AlertLaunguages.AddAsync(alertLaunguageEn);

                await _context.SaveChangesAsync();
                return new SuccessResult("Yenilendi");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public async Task< IResult> DeleteRegistretionUserAsync(string UserId, string ConferanceId)
        {
            try
            {
                var User=await _userManager.FindByIdAsync(UserId);
                if (User == null) return new ErrorResult(message: "User Is NotFound!");
                var Conference = _context.Confrans
                    .Include(x=>x.userConfrances)
                    .FirstOrDefault(x => x.Id.ToString() == ConferanceId);
                if (Conference is null)
                    return new ErrorResult(message: "Conference Is NotFound!");
                _context.UserConfrances.Remove(Conference.userConfrances.FirstOrDefault(x => x.UserId == UserId));
               await _context.SaveChangesAsync();
                return new SuccessResult();

            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message);
            }
        }

        public IDataResult<IQueryable<GetAlertDTO>> GetAlertsForConference(string? CurrentUserId ,string langCode)
        {
            try
            {
                if (CurrentUserId is not null)
                {

                var checkedUserId = _context.Users.FirstOrDefault(x => x.Id == CurrentUserId);
                    if (checkedUserId is null) return new ErrorDataResult<IQueryable<GetAlertDTO>>(message: "User Is NotFound!");
                 
                }
                var alerts = CurrentUserId is null ?
                    _context.Alerts.AsNoTracking().AsSplitQuery().AsQueryable().Where(x => x.ConferenceId != null).Select(x => new GetAlertDTO
                    {
                        AlertContent=x.AlertLaunguages.FirstOrDefault(y=>y.LangCode==langCode).Content,
                        AlertId=x.Id.ToString(),
                        ConferenceId=x.ConferenceId??null,
                        ForUser=x.ForUser,
                        UserId=x.UserId??null
                    })
                    :
                     _context.Alerts.AsNoTracking().AsSplitQuery().AsQueryable().Where(x => x.UserId == CurrentUserId).Select(x => new GetAlertDTO
                     {
                         AlertContent = x.AlertLaunguages.FirstOrDefault(y => y.LangCode == langCode).Content,
                         AlertId = x.Id.ToString(),
                         ConferenceId = x.ConferenceId ?? null,
                         ForUser = x.ForUser,
                         UserId = x.UserId ?? null
                     })
                    ;
                return new SuccessDataResult<IQueryable<GetAlertDTO>>(data: alerts);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<IQueryable<GetAlertDTO>>(message: ex.Message);
            }
        }

        public IDataResult<List<ConferenceGetAdminListDTO>> GetAllConferanceForAdmin(string LangCode)
        {
            try
            {
                var data = _context.Confrans
                    .Include(x => x.User)
                    .Include(x => x.ConfranceLaunguages)
                    .Include(x => x.Audutorium)
                    .Include(x => x.SpecialGuests)
                    .Include(x => x.Category)
                    .ThenInclude(x => x.CategoryLaunguages)
                    .Include(y => y.Time);
                return new SuccessDataResult<List<ConferenceGetAdminListDTO>>(data:

                    data.Select(x => new ConferenceGetAdminListDTO
                    {
                        AudutoriumId = x.AudutoriumId,
                        AudutoriumName = x.Audutorium.AudutoriyaNumber,
                        ConferenceName = x.ConfranceLaunguages.FirstOrDefault(y => y.LangCode == LangCode).ConfransName,
                        ConferenceContent = x.ConfranceLaunguages.FirstOrDefault(y => y.LangCode == LangCode).ConfransContent,
                        Day = x.Time.Date,
                        EndDate = x.Time.EndTime,
                        Id = x.Id,
                        ImgUrl = x.ImgUrl,
                        StartedDate = x.Time.StartedTime,
                        Status = x.Status,
                        CategoryId = x.CategoryId.ToString(),
                        CategoryName = x.Category.CategoryLaunguages.FirstOrDefault(y => y.LangCode == LangCode).CategoryName,
                        UserEmail = x.User.Email,
                        UserFullname = x.User.FirstName + " " + x.User.LastName,
                        UserId = Guid.Parse(x.UserId),
                        IsFeatured = x.IsFeatured,



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
                    .Include(x => x.ConfranceLaunguages)
                    .Include(x => x.Audutorium)
                    .Include(x => x.Time)
                    .Include(x => x.SpecialGuests)
                      .Include(x => x.Category)
                  .ThenInclude(x => x.CategoryLaunguages)
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
                    StartedDate = x.Time.StartedTime,
                    CategoryId = x.CategoryId.ToString(),
                    CategoryName = x.Category.CategoryLaunguages.FirstOrDefault(y => y.LangCode == LangCode).CategoryName,
                    IsFeatured = x.IsFeatured,
                    AlertSeen = x.AlertSeen
                }).ToList());

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<GetALLConferenceUserDTO>>(message: ex.Message);
            }
        }

        public async Task<IDataResult<GetConferenceUserDTO>> GetConferanceDetailForUserAsync(string UserId, string ConfranceId, string LangCode)
        {
            try
            {
                var data = _context.Confrans
                         .Include(x => x.User)
                         .Include(x => x.Audutorium)
                         .Include(x => x.Time)
                         .Include(x => x.ConfranceLaunguages)
                         .Include(x => x.SpecialGuests)
                           .Include(x => x.Category)
  .ThenInclude(x => x.CategoryLaunguages)
                              .Include(x => x.userConfrances)
                    .ThenInclude(x => x.User)
                         .FirstOrDefault(x => x.UserId == UserId && x.Id.ToString() == ConfranceId);
                if (data is null)
                    return new SuccessDataResult<GetConferenceUserDTO>(data: null);

                List<GetConferenceUserRegistrationDTO> getConferenceUserRegistrationDTOs = new List<GetConferenceUserRegistrationDTO>();
                foreach (var userConfrance in data.userConfrances)
                {
                    var roles = await _userManager.GetRolesAsync(userConfrance.User);
                    getConferenceUserRegistrationDTOs.Add(new GetConferenceUserRegistrationDTO
                    {
                        Email = userConfrance.User.Email,
                        FirstName = userConfrance.User.FirstName,
                        LastName = userConfrance.User.LastName,
                        PhoneNumber = userConfrance.User.PhoneNumber,
                        Position = roles.ToList(),
                        UserId = userConfrance.User.Id

                    });
                }

                List<GETConfranceSpecialGuestDTO> gETConfranceSpecialGuestDTOs = new List<GETConfranceSpecialGuestDTO>();
                foreach (var guest in data?.SpecialGuests)
                {
                    gETConfranceSpecialGuestDTOs.Add(new GETConfranceSpecialGuestDTO
                    {
                        Email = guest.Email,
                        Name = guest.Name,
                        Id = guest.Id,
                        SendEmail = guest.SendEmail

                    });
                }



                GetConferenceUserDTO getConferenceUserDTO = new GetConferenceUserDTO()
                {
                    AudutoriumId = data.AudutoriumId,
                    AudutoriumName = data.Audutorium.AudutoriyaNumber,
                    ConferenceContent = data.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == LangCode).ConfransContent,
                    ConferenceName = data.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == LangCode).ConfransName,
                    Day = data.Time.Date,
                    EndDate = data.Time.EndTime,
                    StartedDate = data.Time.StartedTime,
                    Id = data.Id,
                    ImgUrl = data.ImgUrl,
                    specialGuests = gETConfranceSpecialGuestDTOs,
                    Status = data.Status,
                    UserEmail = data.User.Email,
                    UserFullname = data.User.FirstName + " " + data.User.LastName,
                    CategoryId = data.CategoryId.ToString(),
                    CategoryName = data.Category.CategoryLaunguages.FirstOrDefault(y => y.LangCode == LangCode).CategoryName,
                    IsFeatured = data.IsFeatured,
                    RegistrationUser= getConferenceUserRegistrationDTOs
                };
                return new SuccessDataResult<GetConferenceUserDTO>(data: getConferenceUserDTO);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetConferenceUserDTO>(message: ex.Message);
            }
        }

        public IDataResult<ConferenceGetDetailForUIDTO> GetConferenceDetailForUI(string ConferenceId, string LangCode)
        {
            try
            {
                var data = _context.Confrans
                    .Include(x => x.ConfranceLaunguages)
                    .Include(x => x.Comments)
                    .ThenInclude(x => x.User)
                    .Include(x => x.Category)
                    .ThenInclude(x => x.CategoryLaunguages)
                    .Include(x => x.User)
                    .Include(x=>x.userConfrances)
                    .Include(x => x.SpecialGuests)
                    .Include(x => x.Audutorium)
                    .Include(x => x.Time)
                    .Include(x=>x.userConfrances)
                    .FirstOrDefault(x => x.Id.ToString() == ConferenceId);
                if (data is null) return new ErrorDataResult<ConferenceGetDetailForUIDTO>(message: "Conference is NotFound!");

                List<GETConfranceSpecialGuestDTO> specialGuestDTO = new List<GETConfranceSpecialGuestDTO>();



                foreach (var Guest in data.SpecialGuests)
                {
                    specialGuestDTO.Add(new GETConfranceSpecialGuestDTO
                    {
                        Email = Guest.Email,
                        Id = Guest.Id,
                        Name = Guest.Name,
                        SendEmail = Guest.SendEmail,
                    });
                }
                List<GetCommentForUIDTO> comments = new List<GetCommentForUIDTO>();
                foreach (var comment in data.Comments)
                {
                    comments.Add(new GetCommentForUIDTO
                    {
                        CommentId = comment.Id.ToString(),
                        Content = comment.Content,
                        CreatedDate = comment.CreatedDate,
                        UpdateDate = comment.UpdateDate,
                        UserFullName = comment.User.FirstName + " " + comment.User.LastName,
                        UserId = comment.UserId,
                        IsSafe=comment.IsSafe,
                    });
                }
                return new SuccessDataResult<ConferenceGetDetailForUIDTO>(data: new ConferenceGetDetailForUIDTO
                {
                    Id = data.Id,
                    AudutoriumName = data.Audutorium.AudutoriyaNumber,
                    CategoryName = data.Category.CategoryLaunguages.FirstOrDefault(x => x.LangCode == LangCode).CategoryName,
                    Comments = comments,
                    ConferenceContent = data.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == LangCode).ConfransName,
                    ConferenceName = data.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == LangCode).ConfransName,
                    Day = data.Time.Date,
                    StartedDate = data.Time.StartedTime,
                    EndDate = data.Time.EndTime,
                    ImgUrl = data.ImgUrl,
                    specialGuests = specialGuestDTO,
                    UserEmail = data.User.Email,
                    UserFullname = data.User.FirstName + " " + data.User.LastName,
                    CurrentPerson=data.SpecialGuests.Count+data.userConfrances.Count,
                    UsersId=data.userConfrances.Select(x=>x.UserId).ToList(),



                });

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<ConferenceGetDetailForUIDTO>(message: ex.Message);
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
                      .Include(x => x.Category)

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
                    StartedDate = data.Time.StartedTime,
                    CategoryId = data.CategoryId.ToString(),


                });
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<ConferenceUpdateDto>(message: ex.Message);
            }
        }
    }
}
