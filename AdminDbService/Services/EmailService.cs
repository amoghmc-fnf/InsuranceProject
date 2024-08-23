using AdminDbService.Data;
using Microsoft.EntityFrameworkCore;

namespace AdminDbService.Services
{
    public interface IEmailRecordService
    {
        Task Add(EmailRecordDto adminDto);
        Task Delete(int id);
        Task<List<EmailRecordDto>> GetAll();
        Task<EmailRecordDto> GetById(int id);
        Task Update(EmailRecordDto adminDto);
    }

    public class EmailRecordService : IEmailRecordService
    {
        private readonly FnfProjectContext context;
        public EmailRecordService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<EmailRecordDto>> GetAll()
        {
            List<EmailRecordDto> adminDtos = new List<EmailRecordDto>();
            await foreach (var adminTable in context.EmailRecords)
            {
                EmailRecordDto adminDto = ConvertToDto(adminTable);
                adminDtos.Add(adminDto);
            }
            return adminDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.EmailRecords.FirstOrDefaultAsync((adminTable) => adminTable.EmailRecordId == id);
            if (found != null)
            {
                context.EmailRecords.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(EmailRecordDto adminDto)
        {
            EmailRecord adminTable = new EmailRecord();
            ConvertToTable(adminDto, adminTable);
            context.EmailRecords.Add(adminTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(EmailRecordDto adminDto)
        {
            var found = await context.EmailRecords.FirstOrDefaultAsync((adminTable) =>
                adminTable.EmailRecordId == adminDto.EmailRecordId);
            if (found != null)
            {
                ConvertToTable(adminDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<EmailRecordDto> GetById(int id)
        {
            var found = await context.EmailRecords.FirstOrDefaultAsync((adminTable) => adminTable.EmailRecordId == id);
            if (found != null)
            {
                var adminDto = ConvertToDto(found);
                return adminDto;
            }
            throw new NullReferenceException();
        }

        private EmailRecordDto ConvertToDto(EmailRecord adminTable)
        {
            EmailRecordDto adminDto = new()
            {
                EmailRecordId = adminTable.EmailRecordId,
                FromEmail = adminTable.FromEmail,
                ToEmail = adminTable.ToEmail,
                Subject = adminTable.Subject,
            };
            return adminDto;
        }

        private void ConvertToTable(EmailRecordDto adminDto, EmailRecord adminTable)
        {
            adminTable.EmailRecordId = adminDto.EmailRecordId;
            adminTable.FromEmail = adminDto.FromEmail;
            adminTable.ToEmail = adminDto.ToEmail;
            adminTable.Subject = adminDto.Subject;
            return;
        }
    }
}
