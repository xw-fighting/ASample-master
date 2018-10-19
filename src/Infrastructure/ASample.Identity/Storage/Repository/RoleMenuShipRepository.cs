﻿using ASample.EntityFramework.Storage.Repository;
using ASample.Identity.Domain.Models.Entities;
using ASample.Identity.Domain.Repository;
using System;

namespace ASample.Identity.Storage.Repository
{
    public class RoleMenuShipRepository : BasicEntityFrameworkRepository<ASampleIdentityContext, RoleMenuShip, Guid>, IRoleMenuShipRepository
    {
    }
}
