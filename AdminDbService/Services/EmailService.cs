using AdminDbService.Data;
using Microsoft.EntityFrameworkCore;

namespace AdminDbService.Services
{
    public interface IEmailRecordService
    {
        Task Add(EmailRecordDto emailRecordDto);
        Task Delete(int id);
        Task<List<EmailRecordDto>> GetAll();
        Task<EmailRecordDto> GetById(int id);
        Task Update(EmailRecordDto emailRecordDto);
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
            List<EmailRecordDto> emailRecordDtos = new List<EmailRecordDto>();
            await foreach (var emailRecordTable in context.EmailRecords)
            {
                EmailRecordDto emailRecordDto = ConvertToDto(emailRecordTable);
                emailRecordDtos.Add(emailRecordDto);
            }
            return emailRecordDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.EmailRecords.FirstOrDefaultAsync((emailRecordTable) => emailRecordTable.EmailRecordId == id);
            if (found != null)
            {
                context.EmailRecords.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(EmailRecordDto emailRecordDto)
        {
            EmailRecord emailRecordTable = new EmailRecord();
            ConvertToTable(emailRecordDto, emailRecordTable);
            context.EmailRecords.Add(emailRecordTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(EmailRecordDto emailRecordDto)
        {
            var found = await context.EmailRecords.FirstOrDefaultAsync((emailRecordTable) =>
                emailRecordTable.EmailRecordId == emailRecordDto.EmailRecordId);
            if (found != null)
            {
                ConvertToTable(emailRecordDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<EmailRecordDto> GetById(int id)
        {
            var found = await context.EmailRecords.FirstOrDefaultAsync((emailRecordTable) => emailRecordTable.EmailRecordId == id);
            if (found != null)
            {
                var emailRecordDto = ConvertToDto(found);
                return emailRecordDto;
            }
            throw new NullReferenceException();
        }

        private EmailRecordDto ConvertToDto(EmailRecord emailRecordTable)
        {
            EmailRecordDto emailRecordDto = new()
            {
                EmailRecordId = emailRecordTable.EmailRecordId,
                FromEmail = emailRecordTable.FromEmail,
                ToEmail = emailRecordTable.ToEmail,
                Subject = emailRecordTable.Subject,
                Content = emailRecordTable.Content,
            };
            return emailRecordDto;
        }

        private void ConvertToTable(EmailRecordDto emailRecordDto, EmailRecord emailRecordTable)
        {
            emailRecordTable.EmailRecordId = emailRecordDto.EmailRecordId;
            emailRecordTable.FromEmail = emailRecordDto.FromEmail;
            emailRecordTable.ToEmail = emailRecordDto.ToEmail;
            emailRecordTable.Subject = emailRecordDto.Subject;
            emailRecordTable.Content = emailRecordDto.Content;
            return;
        }
    }
}
