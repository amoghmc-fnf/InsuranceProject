using Microsoft.EntityFrameworkCore;
using MyClientAppApi.Data;
using System.Net.Http;
using UserDbService.Data;

namespace UserDbService.Services
{
    public interface IPolicyHolderService
    {
        Task Add(PolicyHolderDto policyHolderDto);
        Task Delete(int id);
        Task<List<PolicyHolderDto>> GetAll();
        Task<PolicyHolderDto> GetById(int id);
        Task Update(PolicyHolderDto policyHolderDto);
        Task<LoginDto> ValidateUser(string email, string password);
        Task UpdateStatus(int id, int status);
        Task<PolicyHolder> GetPolicyHolderByEmailAsync(string email);
    }

    public class PolicyHolderService : IPolicyHolderService
    {
        private readonly FnfProjectContext context;

        public PolicyHolderService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<PolicyHolderDto>> GetAll()
        {
            List<PolicyHolderDto> policyHolderDtos = [];
            await foreach (var policyHolderTable in context.PolicyHolders)
            {
                var policyHolderDto = ConvertToDto(policyHolderTable);
                policyHolderDtos.Add(policyHolderDto);
            }
            return policyHolderDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.PolicyHolders.FirstOrDefaultAsync((policyHolderTable) =>
                policyHolderTable.PolicyHolderId == id);
            if (found != null)
            {
                context.PolicyHolders.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(PolicyHolderDto policyHolderDto)
        {
            PolicyHolder policyHolderTable = new();
            ConvertToTable(policyHolderDto, policyHolderTable);
            context.PolicyHolders.Add(policyHolderTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(PolicyHolderDto policyHolderDto)
        {
            var found = await context.PolicyHolders.FirstOrDefaultAsync((policyHolderTable) =>
                policyHolderTable.PolicyHolderId == policyHolderDto.PolicyHolderId);
            if (found != null)
            {
                ConvertToTable(policyHolderDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<PolicyHolderDto> GetById(int id)
        {
            var found = await context.PolicyHolders.FirstOrDefaultAsync((policyHolderTable) =>
                policyHolderTable.PolicyHolderId == id);

            if (found != null)
                return ConvertToDto(found);

            throw new NullReferenceException();
        }

        private PolicyHolderDto ConvertToDto(PolicyHolder policyHolderTable)
        {
            PolicyHolderDto policyHolderDto = new()
            {
                PolicyHolderId = policyHolderTable.PolicyHolderId,
                Name = policyHolderTable.Name,
                Address = policyHolderTable.Address,
                Email = policyHolderTable.Email,
                PasswordHash = policyHolderTable.PasswordHash,
                Phone = policyHolderTable.Phone,
                Status = policyHolderTable.Status,
            };
            return policyHolderDto;
        }

        private void ConvertToTable(PolicyHolderDto policyHolderDto, PolicyHolder policyHolderTable)
        {
            policyHolderTable.PolicyHolderId = policyHolderDto.PolicyHolderId;
            policyHolderTable.Name = policyHolderDto.Name;
            policyHolderTable.Address = policyHolderDto.Address;
            policyHolderTable.Email = policyHolderDto.Email;
            policyHolderTable.PasswordHash = policyHolderDto.PasswordHash;
            policyHolderTable.Phone = policyHolderDto.Phone;
            policyHolderTable.Status = policyHolderDto.Status;
            return;
        }

        public async Task UpdateStatus(int id, int status)
        {
            var found = await context.PolicyHolders.FirstOrDefaultAsync(ph => ph.PolicyHolderId == id);
            if (found != null)
            {
                found.Status = status;
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<LoginDto> ValidateUser(string email, string password)
        {

            // Fetch the user by email
            var user = await context.PolicyHolders.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }


            // Verify the password (assuming passwords are stored as hashes)
            if (user.PasswordHash != password)
            {
                return null;
            }
            var userDTo = new LoginDto
            {

                Email = user.Email,


            };
            return userDTo;
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }


        // Optional: Implement this method if you want to use JWT for authentication
        public string GenerateToken(LoginDto user)
        {
            return null;
            // Implementation for generating a JWT token
        }

        public async Task<PolicyHolderDto> GetPolicyHolderByEmailAsync(string email)
        {
            return await context.PolicyHolders
                .Where(ph => ph.Email == email)
                .Select(ph => new PolicyHolderDto
                {
                    PolicyHolderId = ph.PolicyHolderId,
                    Email = ph.Email,
                    // map other necessary fields
                })
                .FirstOrDefaultAsync();
        }

        async Task<PolicyHolder> IPolicyHolderService.GetPolicyHolderByEmailAsync(string email)
        {
            return await context.PolicyHolders.Where(ph => ph.Email == email).Select(ph => new PolicyHolder
            {
                PolicyHolderId = ph.PolicyHolderId,
                Email = ph.Email
            }).FirstOrDefaultAsync();
        }

        
    }
}

