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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IResult> ApproveConfrans(Guid id, ConferanceStatus status)
        {
            Confrans confrans = await _context.Confrans.FirstOrDefaultAsync(x => x.Id == id);
            confrans.Status = status;
            _context.Confrans.Update(confrans);
            await _context.SaveChangesAsync();

            if (confrans.specialGuestsEmail != null && confrans.specialGuestsEmail.Count > 0)
            {
                for (int i = 0; i < confrans.specialGuestsEmail.Count; i++)
                {
                    var emailResult = await _emailHelper.ApproveConfransSendEmail(confrans.specialGuestsEmail[i], confrans.specialGuestsName[i]);
                }
            }

            return new SuccessResult();
        }

        public async Task<IDataResult<ConferenceGetAdminDTO>> ConferenceGetAdmin(Guid id, string lang)
        {
            ConferenceGetAdminDTO dto = await _context.Confrans.Select(x => new ConferenceGetAdminDTO
            {
                StartedDate = x.StartedDate,
                Status = x.Status,
                EndDate = x.EndDate,
                AudutoriumId = x.AudutoriumId,
                AudutoriumName = x.Audutorium.AudutoriyaNumber,
                ConferenceContent = x.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == lang).ConfransContent,
                ConferenceName = x.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == lang).ConfransName,
                Id = x.Id,
                ImgUrl = x.ImgUrl,
                LangCode = lang,
                UserEmail = x.User.Email,
                UserFullname = x.User.FirstName + " " + x.User.LastName,
                specialGuestsEmail = x.specialGuestsEmail,
                specialGuestsName = x.specialGuestsName

            }).FirstOrDefaultAsync(x => x.Id == id);

            return new SuccessDataResult<ConferenceGetAdminDTO>(dto);
        }

        public async Task<IDataResult<PaginatedList<ConferenceGetAdminListDTO>>> ConferenceGetAdminList(FilterConferenceDto filter, string lang)
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

            conferenceQueries = conferenceQueries.OrderByDescending(x => x.StartedDate);

            var dto = conferenceQueries.Select(x => new ConferenceGetAdminListDTO()
            {
                StartedDate = x.StartedDate,
                Status = x.Status,
                EndDate = x.EndDate,
                AudutoriumId = x.AudutoriumId,
                AudutoriumName = x.Audutorium.AudutoriyaNumber,
                ConferenceContent = x.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == lang).ConfransContent,
                ConferenceName = x.ConfranceLaunguages.FirstOrDefault(x => x.LangCode == lang).ConfransName,
                Id = x.Id,
                ImgUrl = x.ImgUrl,
                LangCode = lang,
                UserEmail = x.User.Email,
                UserFullname = x.User.FirstName + " " + x.User.LastName
            });

            var list = await PaginatedList<ConferenceGetAdminListDTO>.CreateAsync(dto, filter.Page, filter.PageSize);

            return new SuccessDataResult<PaginatedList<ConferenceGetAdminListDTO>>(data: list);

        }

        public async Task<IResult> ConfrenceAddAsync(ConferenceCreateDto dto)
        {
            try
            {
                Confrans confrans = new()
                {
                    AudutoriumId = dto.AudutoriumId,
                    EndDate = dto.EndDate,
                    specialGuestsEmail = dto.specialGuestsEmail,
                    specialGuestsName = dto.specialGuestsName,
                    StartedDate = dto.StartedDate,
                    ImgUrl = dto.ImgUrl,
                    UserId = dto.UserId,
                    Status = ConferanceStatus.Gözlənilir
                };

                List<ConfranceLaunguage> confransLanguages = new();

                for (int i = 0; i < dto.LangCode.Count; i++)
                {
                    ConfranceLaunguage confransLanguage = new()
                    {
                        LangCode = dto.LangCode[i],
                        ConfransContent = dto.ConferenceContent[i],
                        ConfransName = dto.ConferenceName[i]
                    };
                    confransLanguages.Add(confransLanguage);
                }

                confrans.ConfranceLaunguages = confransLanguages;
                await _context.Confrans.AddAsync(confrans);
                await _context.SaveChangesAsync();

                return new Result(true, "Confrans əlave olundu");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<IResult> ConfrenceUpdateAsync(ConferenceUpdateDto dto)
        {
            try
            {


                var confrans = await _context.Confrans
                    .Include(x => x.ConfranceLaunguages)
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (dto.ConferenceContent is not null)
                {
                    for (int i = 0; i < dto.ConferenceName.Count; i++)
                    {
                        confrans.ConfranceLaunguages[i].ConfransContent = dto.ConferenceContent[i];
                        confrans.ConfranceLaunguages[i].ConfransName = dto.ConferenceName[i];
                    }
                }

                confrans.StartedDate = dto.StartedDate;
                confrans.EndDate = dto.EndDate;
                confrans.specialGuestsName = dto.specialGuestsName;
                confrans.specialGuestsEmail = dto.specialGuestsEmail;
                confrans.AudutoriumId = dto.AudutoriumId;

                if (confrans.ImgUrl != dto.ImgUrl)
                {
                    FileHelper.RemoveFile(confrans.ImgUrl);
                    confrans.ImgUrl = dto.ImgUrl;
                }

                _context.Confrans.Update(confrans);
                await _context.SaveChangesAsync();
                return new SuccessResult("Yenilendi");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }


    }
}
