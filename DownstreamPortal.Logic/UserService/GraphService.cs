using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azure.Identity;
using System.Threading.Tasks;
using Microsoft.Graph.Models;
using Barebone.Logic.Models;
using Microsoft.Graph.Models.Security;
using static Microsoft.Azure.Amqp.Serialization.SerializableType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Microsoft.Kiota.Serialization;

namespace Barebone.Logic.UserService
{
    public class GraphService
    {
        private readonly GraphServiceClient _graphServiceClient;
        public GraphService(GraphServiceClient graphClient)
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var tenantId = "15f43b81-ddd1-4b7d-8cab-c9a9a5f54b0a";
            var clientId = "d0494a4d-aa4c-4b27-b813-2c5e506a6613";
            var clientSecret = "VQU8Q~dZ4n3NAZF9m~qi2l2pn2euX-HKCvUEjcm2";

            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            _graphServiceClient = graphClient;
        }

        //Fetch user from Azure AD
        public async Task<List<AadGroupMember>> FetchAzureUsers()
        {
            var allUsers = new List<AadGroupMember>();

            var usersPage = await _graphServiceClient.Users
               .GetAsync(requestConfiguration =>
               {
                   requestConfiguration.QueryParameters.Select = new[]
                   {
                        "id", "displayName", "mail", "givenName", "surname", "userPrincipalName", "jobTitle", "department", "officeLocation", "employeeType", "companyName", "mobilePhone", "businessPhones"
                   };
                   requestConfiguration.QueryParameters.Top = 999;
               });

            var GraphUser = usersPage.Value;
            foreach (var u in GraphUser)
            {
                var AadUser = new AadGroupMember
                {
                    ObjectId = Guid.Parse(u.Id),
                    Name = u.DisplayName,
                    Email = u.Mail ?? u.UserPrincipalName,
                    FirstName = u.GivenName,
                    LastName = u.Surname,
                    UserPrincipalName = u.UserPrincipalName,
                    JobTitle = u.JobTitle,
                    Department = u.Department,
                    OfficeLocation = u.OfficeLocation,
                    UserType = u.EmployeeType,
                    CompanyName = u.CompanyName,
                    BusinessPhone = u.BusinessPhones?.FirstOrDefault(),
                    MobilePhone = u.MobilePhone
                };
                allUsers.Add(AadUser);
            }

            return allUsers;
        }
    }
}