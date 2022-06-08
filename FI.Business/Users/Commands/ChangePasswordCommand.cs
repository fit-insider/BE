using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.Business.Users.Models;
using MediatR;

namespace FI.Business.Users.Commands
{
    public class ChangePasswordCommand : IRequest<Identifier>
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
