using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Services
{
    public interface IBlacklistService
    {
        Task Add(BlacklistDto blacklistDto);
        Task Delete(int id);
        Task<List<BlacklistDto>> GetAll();
        Task<BlacklistDto> GetById(int id);
        Task Update(BlacklistDto blacklistDto);
    }

    public class BlacklistService : IBlacklistService
    {
        private readonly FnfProjectContext context;

        public BlacklistService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<BlacklistDto>> GetAll()
        {
            List<BlacklistDto> blacklistDtos = [];
            await foreach (var blacklistTable in context.Blacklists)
            {
                var blacklistDto = ConvertToDto(blacklistTable);
                blacklistDtos.Add(blacklistDto);
            }
            return blacklistDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.Blacklists.FirstOrDefaultAsync((blacklistTable) =>
                blacklistTable.BlacklistId == id);
            if (found != null)
            {
                context.Blacklists.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(BlacklistDto blacklistDto)
        {
            Blacklist blacklistTable = new();
            ConvertToTable(blacklistDto, blacklistTable);
            context.Blacklists.Add(blacklistTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(BlacklistDto blacklistDto)
        {
            var found = await context.Blacklists.FirstOrDefaultAsync((blacklistTable) =>
                blacklistTable.BlacklistId == blacklistDto.BlacklistId);
            if (found != null)
            {
                ConvertToTable(blacklistDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<BlacklistDto> GetById(int id)
        {
            var found = await context.Blacklists.FirstOrDefaultAsync((blacklistTable) =>
                blacklistTable.BlacklistId == id);

            if (found != null)
                return ConvertToDto(found);

            throw new NullReferenceException();
        }

        private BlacklistDto ConvertToDto(Blacklist blacklistTable)
        {
            BlacklistDto blacklistDto = new()
            {
                BlacklistId = blacklistTable.BlacklistId,
                PolicyHolderId = blacklistTable.PolicyHolderId,
                AdminId = blacklistTable.AdminId,
                BlacklistDate = DateTime.Parse(blacklistTable.BlacklistDate.ToString()),
                Reason = blacklistTable.Reason,
            };
            return blacklistDto;
        }

        private void ConvertToTable(BlacklistDto blacklistDto, Blacklist blacklistTable)
        {
            blacklistTable.BlacklistId = blacklistDto.BlacklistId;
            blacklistTable.PolicyHolderId = blacklistDto.PolicyHolderId;
            blacklistTable.AdminId = blacklistDto.AdminId;
            blacklistTable.BlacklistDate = DateOnly.FromDateTime(blacklistDto.BlacklistDate);
            blacklistTable.Reason = blacklistDto.Reason;
            return;
        }
    }
}
