﻿using Application.Common.Models;
using Application.Identity.Account.Commands;
using Application.Identity.Account.Common;
using MediatR;

namespace Application.Identity.Account.Handlers;
public interface IChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Result<AccountResult>>
{
}
