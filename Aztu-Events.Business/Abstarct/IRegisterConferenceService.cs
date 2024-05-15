using Aztu_Events.Core.Utilities.Results.Abstract;

namespace Aztu_Events.Business.Abstarct
{
    public interface IRegisterConferenceService
    {
        public Task<IResult> RegisterConferenceAsync(string ConferenceId, string UserId);
    }
}
